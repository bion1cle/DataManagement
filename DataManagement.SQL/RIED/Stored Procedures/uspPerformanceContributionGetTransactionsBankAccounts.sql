
CREATE PROCEDURE [RIED].[uspPerformanceContributionGetTransactionsBankAccounts]
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
	
			
	SET @fromDate = @startDate;
	SET @toDate = @endDate;
	DECLARE	@PortfolioScope PortfoliosForContribution;
	DECLARE	@argh TABLE
		(
			IdPortfolioGroup INT
			,PortfolioShortName VARCHAR(50)
			,Portfolio VARCHAR(255)
		);

	IF (@IdPortfolioList IS NOT NULL)
	BEGIN
					
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
	END;
	ELSE
	BEGIN
		INSERT	INTO @PortfolioScope
				(IdPortfolio)
		VALUES
				(@idPortfolio  -- IdPortfolio - int
					);
	END;	

	SELECT
		NULL AS ElementaryTransCodeShortName
		,NULL AS SecurityShortName
		,NULL AS BalanceNominalQC
		,NULL AS CurrentValuePC
		,
                 
                   --IIF(VSISGSTC.InstrumentTypeName = 'Devisentermingeschäft', SUM(t.CurrentValuePC * tc.Sign * -1 )  , SUM(t.PaymentAmountPC)) --- SUM(ISNULL(cost.CostTaxAmountPC,0.0)) 
		SUM(IIF(VSISGSTC.InstrumentTypeName = 'Devisentermingeschäft', t.CurrentValuePC * tc.Sign * -1, t.PaymentAmountPC)) --- SUM(ISNULL(cost.CostTaxAmountPC,0.0)) 
					AS PaymentAmountPC
		,NULL AS TradePriceQC
		,NULL AS BusinessTransCodeName
		,CASE BusinessTransCodeShortName
			WHEN 'Dividende' THEN t.PaymentDate
			WHEN 'Ausschüttung' THEN t.PaymentDate
					  --WHEN 'FäVfDvTmSw' THEN t.PaymentDate
			WHEN 'VfDTmDBD' THEN t.PaymentDate
			WHEN 'KfDTmDBD' THEN PaymentDate
			WHEN 'Kuponzahlung' THEN t.PaymentDate
			ELSE t.TradeDate  --t.PaymentDate--
			END AS TradeDate
		,NULL--t.HoldingKeyLocalGAAP AS HoldingKeyLocalGAAP
		,NULL AS FCName
		,t.IdBankAccount
		,CASE BusinessTransCodeShortName
			WHEN 'Dividende' THEN t.PaymentDate
			WHEN 'Ausschüttung' THEN t.PaymentDate
					  --WHEN 'FäVfDvTmSw' THEN t.PaymentDate
			WHEN 'VfDTmDBD' THEN t.PaymentDate
			WHEN 'KfDTmDBD' THEN PaymentDate
			WHEN 'Kuponzahlung' THEN t.PaymentDate
			ELSE t.TradeDate  --t.PaymentDate--
			END AS TradeDate--t.TradeDate
	FROM
		CCO.TransactionFact t
	INNER JOIN CCO.Portfolio p ON p.IdPortfolio = t.IdPortfolio
	INNER JOIN CCO.TransCode tc ON tc.IdTransCode = t.IdTransCode
	INNER JOIN CCO.Security s ON s.IdSecurity = t.IdSecurity
	INNER JOIN [IMDWH].dbo.vSecurityInstypeSecGroupSecTypeCurrent AS VSISGSTC ON VSISGSTC.IdSecurityType = s.IdSecurityType
	INNER JOIN CCO.SecurityToAssetClassification AS STAC ON STAC.idSecurity = t.IdSecurity
															AND STAC.isDeleted = 0
	INNER JOIN dbo.vAssetTreeContribution AS VATC ON STAC.idAssetClassification = VATC.IdAssetClassChild
	INNER JOIN @PortfolioScope AS PS ON PS.IdPortfolio = p.IdPortfolio
	LEFT JOIN CCO.AdditionalInformation a2 ON a2.TargetId = t.IdTransactionFact
												AND a2.TargetTable = 'CCO.TransactionFact'
												AND a2.FieldName = 'TRAFC42IK'
	LEFT JOIN CCO.FreeCodes fc2 ON fc2.ID = a2.FieldValue
	LEFT JOIN CCO.AdditionalInformation a1 ON a1.TargetId = t.IdTransactionFact
												AND a1.TargetTable = 'CCO.TransactionFact'
												AND a1.FieldName = 'TRAFC44IK'
	LEFT JOIN CCO.FreeCodes fc1 ON fc1.ID = a1.FieldValue
	WHERE
		--p.IdPortfolio = 32
                --AND 
		t.IdActualTransStatus = 22
		AND t.IdTransActivityLevel = 14
		AND VATC.AssetClassParent <> 'Liquidität'
		AND tc.BusinessTransCodeShortName NOT IN ('FäVfDvTmSw', 'FäKfDvTmSw')                        
					--AND 
					--	(
					--		fc1.FC <> 'FXS'
					--	)	
		AND CASE BusinessTransCodeShortName
				WHEN 'Dividende' THEN t.PaymentDate
				WHEN 'Ausschüttung' THEN t.PaymentDate	
						  --WHEN 'FäVfDvTmSw' THEN t.PaymentDate	
				WHEN 'VfDTmDBD' THEN t.PaymentDate
				WHEN 'KfDTmDBD' THEN PaymentDate
				WHEN 'Kuponzahlung' THEN t.PaymentDate
				ELSE t.TradeDate  --t.PaymentDate--
			END BETWEEN @fromDate
				AND		@toDate
	GROUP BY
		t.IdBankAccount
                   --,t.TradeDate
		,CASE BusinessTransCodeShortName
			WHEN 'Dividende' THEN t.PaymentDate
			WHEN 'Ausschüttung' THEN t.PaymentDate	
					  --WHEN 'FäVfDvTmSw' THEN t.PaymentDate			
			WHEN 'VfDTmDBD' THEN t.PaymentDate
			WHEN 'KfDTmDBD' THEN PaymentDate
			WHEN 'Kuponzahlung' THEN t.PaymentDate
			ELSE t.TradeDate
			END;

END;