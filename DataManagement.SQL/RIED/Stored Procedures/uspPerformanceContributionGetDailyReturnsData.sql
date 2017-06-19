

CREATE PROCEDURE [RIED].[uspPerformanceContributionGetDailyReturnsData]
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

	;
	WITH	DataSourceGroup
				AS (
					SELECT
						ROW_NUMBER() OVER (PARTITION BY x.Date ORDER BY IdDataSource) rowcnt
						,x.Date
						,x.IdDataSource
					FROM
						(
							SELECT DISTINCT
								P.IdDataSource
								,PF.Date
							FROM
								CCO.Position AS P
							INNER JOIN CCO.PositionFact AS PF ON PF.IdPosition = P.IdPosition --AND p.CurrentFlag = 1
							INNER JOIN [IMDWH].dbo.Time AS T ON T.Date = PF.Date
																AND (
																		T.DayNumberOfWeek NOT IN (1, 7)
																		OR (T.IsLastDayOfMonth = 1)
																	)
							WHERE
								PF.Date >= DATEADD(dd, -32, @fromDate)
								AND PF.Date >= '20151231'
						) AS x
					)
		
		SELECT
			s.SecurityShortName
			,NULL AS weightyest
			,pf.Date
			,COALESCE(pf.DirtyValuePC, 0.0)
			,pf.CleanValuePC--pf.DirtyValuePC
			,NULL AS DailyPerformance --performance: heute zu gestern
			,p.HOLKEYIK
			,ISNULL(pf.BalanceNominalNumber, 0.0)
			,NULL AS PortfolioValue
			,--UGPVPD.PortfolioValue ,
			pf.PriceCleanQC
			,p.IdPortfolio
			,s.IdSecurity
			,AssetTree.AssetClassParent AS AssetKlasse
			,AssetTree.AssetClassChild AS InvestmentTyp
			,PG.PortfolioGroupShortName
			,po.PortfolioShortName
			,VSISGSTC.InstrumentTypeName
			,p.IdPositionCurrency
			,p.LegNumber
			,pf.FXRateQCPC
		FROM
			CCO.Position p
		INNER JOIN CCO.PositionFact pf ON pf.IdPosition = p.IdPosition
		INNER JOIN CCO.Portfolio po ON po.IdPortfolio = p.IdPortfolio
		INNER JOIN CCO.PortfolioGroup AS PG ON PG.IdPortfolioGroup = po.IdPortfolioGroup
		INNER JOIN CCO.Security s ON s.IdSecurity = p.IdSecurity
		INNER JOIN [IMDWH].dbo.vSecurityInstypeSecGroupSecTypeCurrent AS VSISGSTC ON VSISGSTC.IdSecurityType = s.IdSecurityType
		INNER JOIN CCO.SecurityToAssetClassification AS STAC ON STAC.idSecurity = s.IdSecurity
																AND STAC.isDeleted = 0
		INNER JOIN dbo.vAssetTreeContribution AS AssetTree ON STAC.idAssetClassification = AssetTree.IdAssetClassChild
		INNER JOIN @PortfolioScope AS PS ON PS.IdPortfolio = po.IdPortfolio
		INNER JOIN DataSourceGroup ON DataSourceGroup.Date = pf.Date
										AND DataSourceGroup.IdDataSource = p.IdDataSource
		--RIGHT	 JOIN #tradesIntheCurrentMonth AS TICM ON TICM.HoldingKeyLocalGAAP = p.HOLKEYIK
		--													AND TICM.TradeDate = pf.Date
		--													AND TICM.LegNumber = p.LegNumber
						--INNER JOIN IMDWH.dbo.Time AS T ON T.Date = pf.Date AND (t.DayNumberOfWeek NOT IN (1,7) OR (T.IsLastDayOfMonth = 1))
		WHERE
			pf.Date >= DATEADD(dd, -32, @fromDate)--@fromDate
			AND (
					pf.BalanceNominalNumber = 0
					OR pf.BalanceNominalNumber IS NULL
				)
			AND p.IdCalculationType = 2
			AND DataSourceGroup.rowcnt = 1
			AND p.IdPositionView = 5
			AND AssetTree.AssetClassChild <> 'Absicherungsgeschäfte';
		--ORDER BY
		--	pf.Date;

END;