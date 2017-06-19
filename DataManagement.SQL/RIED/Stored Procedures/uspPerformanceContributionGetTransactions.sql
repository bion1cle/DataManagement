CREATE PROCEDURE RIED.uspPerformanceContributionGetTransactions
	(
		@startDate DATE
		,@endDate DATE
		,@idPortfolio INT
		,@IdPortfolioList INT
	)
AS
BEGIN	

	DECLARE	@idPortfolioInner INT;
	DECLARE	@fromDate DATE;
	DECLARE	@toDate DATE;
	IF (@idPortfolio IS NOT NULL)
	BEGIN	

		SET @idPortfolioInner = @idPortfolio;
		SET @fromDate = @startDate;
		SET @toDate = @endDate;


		SELECT
			NULL AS ElementaryTransCodeShortName
			,NULL AS SecurityShortName
			,NULL AS BalanceNominalQC
			,NULL AS CurrentValuePC
                   --,SUM(t.PaymentAmountPC) - SUM(ISNULL(cost.CostTaxAmountPC,
                   --                                     0.0)) AS PaymentAmountPC
			,SUM(IIF(tc.BusinessTransCodeShortName IN ('Einzahlen', 'Auszahlen')
					AND t.SumCostTaxPC IS NOT NULL, t.CurrentValuePC * tc.Sign, t.PaymentAmountPC)) - SUM(ISNULL(cost.CostTaxAmountPC, 0.0)) AS PaymentAmountPC
			,NULL AS TradePriceQC
			,NULL AS BusinessTransCodeName
			,CASE BusinessTransCodeShortName
				WHEN 'Dividende' THEN t.PaymentDate
				WHEN 'Ausschüttung' THEN t.PaymentDate
				WHEN 'FäVfDvTmSw' THEN t.PaymentDate
				WHEN 'Kuponzahlung' THEN t.PaymentDate
				ELSE t.TradeDate  --t.PaymentDate--
				END AS TradeDate
			,t.HoldingKeyLocalGAAP AS HoldingKeyLocalGAAP
			,NULL AS FCName
			,s.IdSecurity
			,NULL--t.PaymentDate
			,t.LegNumber
			,NULL--t.IdBankAccount
				   --, SUM(t.BalanceNominalQC) AS BalanceNominalQC
		FROM
			CCO.TransactionFact t
		INNER JOIN CCO.Portfolio p ON p.IdPortfolio = t.IdPortfolio
		INNER JOIN CCO.TransCode tc ON tc.IdTransCode = t.IdTransCode
		INNER JOIN CCO.Security s ON s.IdSecurity = t.IdSecurity
		INNER JOIN [IMDWH].dbo.vSecurityInstypeSecGroupSecTypeCurrent AS VSISGSTC ON VSISGSTC.IdSecurityType = s.IdSecurityType
                --INNER JOIN @PortfolioScope AS PS ON PS.IdPortfolio = p.IdPortfolio
		LEFT JOIN CCO.AdditionalInformation a2 ON a2.TargetId = t.IdTransactionFact
													AND a2.TargetTable = 'CCO.TransactionFact'
													AND a2.FieldName = 'TRAFC42IK'
		LEFT JOIN CCO.FreeCodes fc2 ON fc2.ID = a2.FieldValue
		LEFT JOIN CCO.AdditionalInformation a3 ON a3.TargetId = t.IdTransactionFact
													AND a3.TargetTable = 'CCO.TransactionFact'
													AND a3.FieldName = 'TRAFC44IK'
		LEFT JOIN CCO.FreeCodes fc3 ON fc3.ID = a3.FieldValue
		LEFT JOIN (
					SELECT
						CT.IdTransactionFact
                                   --,CTT.CostTaxShortName
						,SUM(CT.CostTaxAmountPC) AS CostTaxAmountPC
					FROM
						CCO.CostTax AS CT
					INNER JOIN CCO.CostTaxType AS CTT ON CTT.IdCostTaxType = CT.IdCostTaxType
															AND CTT.CostTaxShortName IN ('ABG_ST', 'BP DIF QUE', 'QST', 'QST_DIV', 'SOLI', 'SOLI_DIV', 'SOLZAST', 'ZAST', 'LOCAL_TAX', 'KEST'--, 'KEST_CH','CASH_KEST','CASH_SOLI','CASH_SPES' 
															  )
					GROUP BY
						CT.IdTransactionFact
					) cost ON cost.IdTransactionFact = t.IdTransactionFact
								AND tc.ElementaryTransCodeShortName IN ('Dividende', 'Ausschüttung', 'Kuponzahlung')
		WHERE
			p.IdPortfolio = @idPortfolioInner
			AND t.IdActualTransStatus = 22
			AND t.IdTransActivityLevel = 14
			AND (
					fc2.FCName = 'Nicht Performance relevant'--'Performance relevant'
					OR fc2.FCName IS NULL
				)	
						--AND 
						--(
						--	fc3.FC <> 'FXS' OR fc3.FC IS NULL
						--)					
          --AND tc.BusinessTransCodeShortName NOT IN ('FäVfDvTmSw','FäVfDvTmSw')
			AND CASE BusinessTransCodeShortName
					WHEN 'Dividende' THEN t.PaymentDate
					WHEN 'Ausschüttung' THEN t.PaymentDate
					WHEN 'FäVfDvTmSw' THEN t.PaymentDate
					WHEN 'Kuponzahlung' THEN t.PaymentDate
					ELSE t.TradeDate  --t.PaymentDate--
				END BETWEEN @fromDate
					AND		@toDate
		GROUP BY --t.TradeDate ,
			t.HoldingKeyLocalGAAP
			,s.IdSecurity
                   --,t.PaymentDate
			,t.LegNumber
                   --,t.IdBankAccount /*Am 10.2. rausgenommen wegen Problem beim Coupon Tilgung etc bei MR CS am 2.2.17 und Papier XS0739410164_AllAssetClass_0835-737212-85_NOK_0_20170202_L_0_RunId1_0*/
			,CASE BusinessTransCodeShortName
				WHEN 'Dividende' THEN t.PaymentDate
				WHEN 'Ausschüttung' THEN t.PaymentDate
				WHEN 'FäVfDvTmSw' THEN t.PaymentDate
				WHEN 'Kuponzahlung' THEN t.PaymentDate
				ELSE t.TradeDate  --t.PaymentDate--
				END;
	END;
	ELSE
		IF (@IdPortfolioList IS NOT NULL)
		BEGIN
			
			SET @fromDate = @startDate;
			SET @toDate = @endDate;
			DECLARE	@PortfolioScope PortfoliosForContribution;
			DECLARE	@argh TABLE
				(
					IdPortfolioGroup INT
					,PortfolioShortName VARCHAR(50)
					,Portfolio VARCHAR(255)
				);
			DELETE FROM
				@argh;
			INSERT	INTO @argh
					EXEC [IMDWH].RIED.uspSSRSPortfolioList @IdPortfolioList;
			--DECLARE	@PortfolioListScope TABLE
			--	(
			--		Id INT
			--		,PortfolioList INT
			--	);
			DELETE FROM
				@PortfolioScope;
			INSERT	INTO @PortfolioScope
					(
						IdPortfolio
					)
			SELECT
				P.IdPortfolio
			FROM
				CCO.Portfolio AS P
			INNER JOIN @argh AS A ON A.PortfolioShortName = P.PortfolioShortName;


			SELECT
				NULL AS ElementaryTransCodeShortName
				,NULL AS SecurityShortName
				,NULL AS BalanceNominalQC
				,NULL AS CurrentValuePC
				,SUM(IIF(tc.BusinessTransCodeShortName IN ('Einzahlen', 'Auszahlen')
						AND t.SumCostTaxPC IS NOT NULL, t.CurrentValuePC * tc.Sign, t.PaymentAmountPC)) - SUM(ISNULL(cost.CostTaxAmountPC, 0.0)) AS PaymentAmountPC
				,NULL AS TradePriceQC
				,NULL AS BusinessTransCodeName
				,CASE BusinessTransCodeShortName
					WHEN 'Dividende' THEN t.PaymentDate
					WHEN 'Ausschüttung' THEN t.PaymentDate
					WHEN 'FäVfDvTmSw' THEN t.PaymentDate
					WHEN 'Kuponzahlung' THEN t.PaymentDate
					ELSE t.TradeDate  --t.PaymentDate--
					END AS TradeDate
				,t.HoldingKeyLocalGAAP AS HoldingKeyLocalGAAP
				,NULL AS FCName
				,s.IdSecurity
				,NULL--t.PaymentDate
				,t.LegNumber
				,NULL--t.IdBankAccount
			FROM
				CCO.TransactionFact t
			INNER JOIN CCO.Portfolio p ON p.IdPortfolio = t.IdPortfolio
			INNER JOIN CCO.TransCode tc ON tc.IdTransCode = t.IdTransCode
			INNER JOIN CCO.Security s ON s.IdSecurity = t.IdSecurity
			INNER JOIN [IMDWH].dbo.vSecurityInstypeSecGroupSecTypeCurrent AS VSISGSTC ON VSISGSTC.IdSecurityType = s.IdSecurityType
			INNER JOIN @PortfolioScope AS PS ON PS.IdPortfolio = p.IdPortfolio
			LEFT JOIN CCO.AdditionalInformation a2 ON a2.TargetId = t.IdTransactionFact
														AND a2.TargetTable = 'CCO.TransactionFact'
														AND a2.FieldName = 'TRAFC42IK'
			LEFT JOIN CCO.FreeCodes fc2 ON fc2.ID = a2.FieldValue
			LEFT JOIN (
						SELECT
							CT.IdTransactionFact
                                   --,CTT.CostTaxShortName
							,SUM(CT.CostTaxAmountPC) AS CostTaxAmountPC
						FROM
							CCO.CostTax AS CT
						INNER JOIN CCO.CostTaxType AS CTT ON CTT.IdCostTaxType = CT.IdCostTaxType
																AND CTT.CostTaxShortName IN ('ABG_ST', 'BP DIF QUE', 'QST', 'QST_DIV', 'SOLI', 'SOLI_DIV', 'SOLZAST', 'ZAST', 'LOCAL_TAX', 'KEST'--, 'KEST_CH','CASH_KEST','CASH_SOLI','CASH_SPES' 
															  )
						GROUP BY
							CT.IdTransactionFact
						) cost ON cost.IdTransactionFact = t.IdTransactionFact
									AND tc.ElementaryTransCodeShortName IN ('Dividende', 'Ausschüttung', 'Kuponzahlung')
			WHERE
				t.IdActualTransStatus = 22
				AND t.IdTransActivityLevel = 14
				AND (
						fc2.FCName = 'Nicht Performance relevant'--'Performance relevant'
						OR fc2.FCName IS NULL
					)
          --AND tc.BusinessTransCodeShortName NOT IN ('FäVfDvTmSw','FäVfDvTmSw')
				AND CASE BusinessTransCodeShortName
						WHEN 'Dividende' THEN t.PaymentDate
						WHEN 'Ausschüttung' THEN t.PaymentDate
						WHEN 'FäVfDvTmSw' THEN t.PaymentDate
						WHEN 'Kuponzahlung' THEN t.PaymentDate
						ELSE t.TradeDate  --t.PaymentDate--
					END BETWEEN @fromDate
						AND		@toDate
			GROUP BY --t.TradeDate ,
				t.HoldingKeyLocalGAAP
				,s.IdSecurity
                   --,t.PaymentDate
				,t.LegNumber
                   --,t.IdBankAccount /*Am 10.2. rausgenommen wegen Problem beim Coupon Tilgung etc bei MR CS am 2.2.17 und Papier XS0739410164_AllAssetClass_0835-737212-85_NOK_0_20170202_L_0_RunId1_0*/
				,CASE BusinessTransCodeShortName
					WHEN 'Dividende' THEN t.PaymentDate
					WHEN 'Ausschüttung' THEN t.PaymentDate
					WHEN 'FäVfDvTmSw' THEN t.PaymentDate
					WHEN 'Kuponzahlung' THEN t.PaymentDate
					ELSE t.TradeDate  --t.PaymentDate--
					END;


		END;	
END;