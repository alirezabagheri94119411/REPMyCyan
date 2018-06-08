WITH t (SLId, SLCode, DL1, DL2, DLCode, CurrencyTitle, ParityRate, SumDebit, SumCredit, SumAmountCurrency, TypeDocumentId)
AS (SELECT sl.SLId,sl.SLCode,sl.DLType1,sl.DLType2, dl.DLCode,c.CurrencyTitle,(
               SELECT TOP 1  e.ParityRate FROM dbo.ExchangeRates e
               WHERE e.CurrencyId = c.CurrencyId
               ORDER BY e.ExchangeRateId DESC) ParityRate,SUM(di.Debit) SumDebit,SUM(di.Credit) SumCredit, SUM(di.AmountCurrency) SumAmountCurrency, h.TypeDocumentId
    FROM dbo.AccDocumentItems di
        LEFT JOIN dbo.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
        LEFT JOIN dbo.SLs sl ON sl.SLId = di.SLId
        LEFT JOIN dbo.DLs dl ON dl.DLId = di.DL1Id
        LEFT JOIN dbo.Currencies c ON c.CurrencyId = di.CurrencyId
    
    GROUP BY sl.SLId,sl.SLCode,sl.DLType1,sl.DLType2,dl.DLCode,c.CurrencyTitle, c.CurrencyId,h.TypeDocumentId)
SELECT t.SLId, t.SLCode, t.DL1, t.DL2, t.DLCode, t.CurrencyTitle, t.ParityRate, t.SumDebit, t.SumCredit, t.SumAmountCurrency
FROM t
WHERE NOT EXISTS(SELECT 1 FROM t t2 WHERE t2.SLId = t.SLId AND t2.TypeDocumentId = 6)


--WITH t(SLId,TypeDocumentId) AS(SELECT di.SLId,h.TypeDocumentId FROM dbo.AccDocumentItems di
--INNER JOIN dbo.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId)
--SELECT t.SLId,t.TypeDocumentId FROM t
--WHERE NOT EXISTS(SELECT 1 FROM t t2 WHERE t2.SLId = t.SLId AND t2.TypeDocumentId = 6)