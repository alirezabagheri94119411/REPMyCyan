WITH table1 (slid,SLCode,SLTitle,DL1Id,DL2Id,DL1Code,DL2Code,DLTitle,CurrencyTitle,ParityRate,SumDebit,SumCredit, TypeDocumentId,DocumentDate)As  
(SELECT sl.SLId,sl.SLCode,sl.Title SLTitle,dl1.DLId DL1Id,dl2.DLId DL2Id,dl1.DLCode DL1Code ,dl2.DLCode DL2Code,dl1.Title DLTitle  ,c.CurrencyTitle,
(SELECT TOP 1 e.ParityRate FROM dbo.ExchangeRates e WHERE e.CurrencyId = c.CurrencyId ORDER BY e.ExchangeRateId DESC) ParityRate,    
SUM(di.Debit) SumDebit,SUM(di.Credit) SumCredit,h.TypeDocumentId,h.DocumentDate 
FROM dbo.AccDocumentItems di    LEFT JOIN dbo.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId    left JOIN dbo.SLs sl ON sl.SLId = di.SLId  
left JOIN dbo.DLs dl1 ON dl1.DLId = di.DL1Id    left JOIN dbo.DLs dl2 ON dl2.DLId = di.DL2Id    left JOIN dbo.Currencies c  ON c.CurrencyId = di.CurrencyId 
GROUP BY sl.SLId,sl.SLCode,sl.Title,dl1.DLId,dl2.DLId,dl1.DLCode  ,dl2.DLCode,dl1.Title,c.CurrencyTitle,c.CurrencyId,h.TypeDocumentId,h.DocumentDate )
SELECT slid,SLCode,SLTitle,DL1Id,DL2Id,DL1Code,DL2Code  ,DLTitle,     CurrencyTitle,      ParityRate,       SumDebit,          SumCredit,   TypeDocumentId, DocumentDate FROM table1
WHERE NOT EXISTS(    SELECT 1    FROM table1 t2    WHERE t2.SLCode = table1.SLCode          AND t2.TypeDocumentId = 6)AND table1.DocumentDate<='2/5/2018'