SELECT gl.GLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
INNER JOIN dbo.GLs AS gl ON gl.GLId = tl.GLId
GROUP BY gl.GLCode

SELECT tl.TLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) AS SumCredit FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
GROUP BY tl.TLCode 

SELECT sl.SLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) AS SumCredit FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON  sl.SLId = di.SLId
GROUP BY sl.SLCode


SELECT dl.DLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) AS SumCredit FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.DLs AS dl ON di.DL1Id=dl.DLId
GROUP BY dl.DLCode

SELECT dl.DLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) AS SumCredit FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.DLs AS dl ON di.DL2Id=dl.DLId
GROUP BY dl.DLCode

SELECT cu.CurrencyTitle,SUM(di.Debit) AS SumDebit,SUM(di.Credit) AS SumCredit FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.Currencies cu ON cu.CurrencyId = di.CurrencyId
GROUP BY cu.CurrencyTitle


SELECT di.TrackingNumber,SUM(di.Debit) AS SumDebit,SUM(di.Credit) AS SumCredit FROM dbo.AccDocumentItems AS di
GROUP BY di.TrackingNumber

----------------------------------------------------------------------------------------------------------------------
SELECT tl.tlcode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
INNER JOIN dbo.GLs AS gl ON gl.GLId = tl.GLId
WHERE gl.glcode=10
GROUP BY gl.GLCode,tl.TLCode,sl.SLCode
--HAVING tl.TLCode=1001


SELECT sl.slcode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
INNER JOIN dbo.GLs AS gl ON gl.GLId = tl.GLId
WHERE gl.glcode=10
GROUP BY gl.GLCode,tl.TLCode,sl.SLCode
--HAVING tl.TLCode=1001

SELECT dl.DLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
INNER JOIN dbo.GLs AS gl ON gl.GLId = tl.GLId
INNER JOIN dbo.DLs AS dl ON dl.DLId=di.DL1Id
WHERE gl.glcode=10
GROUP BY gl.GLCode,tl.TLCode,sl.SLCode,dl.DLCode


SELECT dl.DLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
INNER JOIN dbo.GLs AS gl ON gl.GLId = tl.GLId
INNER JOIN dbo.DLs AS dl ON dl.DLId=di.DL2Id
WHERE gl.glcode=10
GROUP BY gl.GLCode,tl.TLCode,sl.SLCode,dl.DLCode

--------------------------------------------------------
SELECT sl.slcode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
WHERE tl.TLCode=1001
GROUP BY tl.TLCode,sl.SLCode

SELECT dl.DLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
INNER JOIN dbo.DLs AS dl ON dl.DLId=di.DL1Id
WHERE tl.TLCode=1001
GROUP BY tl.TLCode,sl.SLCode,dl.DLCode


SELECT dl.DLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
INNER JOIN dbo.DLs AS dl ON dl.DLId=di.DL2Id
WHERE tl.TLCode=1001
GROUP BY tl.TLCode,sl.SLCode,dl.DLCode

----------------------------------------------------------------------------------------------


SELECT dl.DLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.DLs AS dl ON dl.DLId=di.DL1Id
WHERE sl.SLCode=100101
GROUP BY sl.SLCode,dl.DLCode


SELECT dl.DLCode,SUM(di.Debit) AS SumDebit,SUM(di.Credit) FROM dbo.AccDocumentItems AS di
INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
INNER JOIN dbo.DLs AS dl ON dl.DLId=di.DL2Id
WHERE sl.SLCode=100101
GROUP BY sl.SLCode,dl.DLCode

--------------------------------------
SELECT * FROM dbo.AccDocumentItems

