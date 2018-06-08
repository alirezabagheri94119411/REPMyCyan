go
--First we need to convert Persian calendar date to Julian Calendar date
Create FUNCTION [dbo].[UDF_Persian_To_Julian](@iYear int,@iMonth int,@iDay int)
RETURNS bigint
AS
Begin
 
Declare @PERSIAN_EPOCH  as int
Declare @epbase as bigint
Declare @epyear as bigint
Declare @mdays as bigint
Declare @Jofst  as Numeric(18,2)
Declare @jdn bigint
 
Set @PERSIAN_EPOCH=1948321
Set @Jofst=2415020.5
 
If @iYear>=0 
    Begin
        Set @epbase=@iyear-474 
    End
Else
    Begin
        Set @epbase = @iYear - 473 
    End
    set @epyear=474 + (@epbase%2820) 
If @iMonth<=7
    Begin
        Set @mdays=(Convert(bigint,(@iMonth) - 1) * 31)
    End
Else
    Begin
        Set @mdays=(Convert(bigint,(@iMonth) - 1) * 30+6)
    End
    Set @jdn =Convert(int,@iday) + @mdays+ Cast(((@epyear * 682) - 110) / 2816 as int)  + (@epyear - 1) * 365 + Cast(@epbase / 2820 as int) * 1029983 + (@PERSIAN_EPOCH - 1) 
    RETURN @jdn
End
Go
--Secondly, convert Julian calendar date to Gregorian to achieve the target.
Create FUNCTION [dbo].[UDF_Julian_To_Gregorian] (@jdn bigint)
Returns nvarchar(11)
as
Begin
    Declare @Jofst  as Numeric(18,2)
    Set @Jofst=2415020.5
    Return Convert(nvarchar(11),Convert(datetime,(@jdn- @Jofst),113),110)
End
Go
 
-- Here is the example
--Select dbo.[UDF_Julian_To_Gregorian](dbo.[UDF_Persian_To_Julian](1391,1,30))
--Result is 04-18-2012


GO

/****** Object:  UserDefinedFunction [dbo].[ShamsiToMiladi]    Script Date: 1/19/2018 9:08:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --Optimize By Mamehdi
CREATE FUNCTION [dbo].[ShamsiToMiladi] (@intDate DATETIME , @format AS NVARCHAR(MAX))
 
RETURNS NVARCHAR(MAX)

BEGIN
/* Format Rules: (پنجشنبه 7 اردیبهشت 1394)
ChandShanbe -> پنجشنبه (روز هفته به حروف)
ChandShanbeAdadi -> 6 (روز هفته به عدد)
Rooz -> 7 (چندمین روز از ماه)
Rooz2 -> 07 (چندمین روز از ماه دو کاراکتری)
Maah -> 2 (چندمین ماه از سال)
Maah2 -> 02 (چندمین ماه از سال دو کاراکتری)
MaahHarfi -> اردیبهشت (نام ماه به حروف)
Saal -> 1394 (سال چهار کاراکتری)
Saal2 -> 94 (سال دو کاراکتری)
Saal4 -> 1394 (سال چهار کاراکتری)
SaalRooz -> 38 (چندمین روز سال)
Default Format -> 'ChandShanbe Rooz MaahHarfi Saal'
*/
DECLARE @YY Smallint=year(@intdate),@MM Tinyint=10,@DD Smallint=11,@DDCNT Tinyint,@YYDD Smallint=0,
        @SHMM NVARCHAR(8),@SHDD NVARCHAR(8)
DECLARE @SHDATE NVARCHAR(max)



IF @YY < 1000 SET @YY += 2000

IF (@Format IS NULL) OR NOT LEN(@Format)>0 SET @Format = 'ChandShanbe Rooz MaahHarfi Saal'

SET @YY -= 622

IF @YY % 4 = 3 and @yy > 1371 SET @dd = 12

SET @DD += DATEPART(DY,@intDate) - 1

WHILE 1 = 1
BEGIN

 SET @DDCNT =
    CASE
        WHEN @MM < 7 THEN 31
        WHEN @YY % 4 < 3 and @MM=12 and @YY > 1370 THEN 29
        WHEN @YY % 4 <> 2 and @MM=12 and @YY < 1375 THEN 29
        ELSE 30
    END
    IF @DD > @DDCNT
    BEGIN
        SET @DD -= @DDCNT
        SET @MM += 1
        SET @YYDD += @DDCNT
    END
    IF @MM > 12
    BEGIN
        SET @MM = 1
        SET @YY += 1
        SET @YYDD = 0
    END
    IF @MM < 7 AND @DD < 32 BREAK
    IF @MM BETWEEN 7 AND 11 AND @DD < 31 BREAK
    IF @MM = 12 AND @YY % 4 < 3 AND @YY > 1370 AND @DD < 30 BREAK
    IF @MM = 12 AND @YY % 4 <> 2 AND @YY < 1375 AND @DD < 30 BREAK
    IF @MM = 12 AND @YY % 4 = 2 AND @YY < 1371 AND @DD < 31 BREAK
    IF @MM = 12 AND @YY % 4 = 3 AND @YY > 1371 AND @DD < 31 BREAK

END

 SET @YYDD += @DD

SET @SHMM = 
    CASE 
        WHEN @MM=1 THEN N'فروردین'
        WHEN @MM=2 THEN N'اردیبهشت'
        WHEN @MM=3 THEN N'خرداد'
        WHEN @MM=4 THEN N'تیر'
        WHEN @MM=5 THEN N'مرداد'
        WHEN @MM=6 THEN N'شهریور'
        WHEN @MM=7 THEN N'مهر'
        WHEN @MM=8 THEN N'آبان'
        WHEN @MM=9 THEN N'آذر'
        WHEN @MM=10 THEN N'دی'
        WHEN @MM=11 THEN N'بهمن'
        WHEN @MM=12 THEN N'اسفند'
    END
    

SET @SHDD=
    CASE
        WHEN DATEPART(dw,@intdate)=7 THEN N'شنبه'
        WHEN DATEPART(dw,@intdate)=1 THEN N'یکشنبه'
        WHEN DATEPART(dw,@intdate)=2 THEN N'دوشنبه'
        WHEN DATEPART(dw,@intdate)=3 THEN N'سه شنبه'
        WHEN DATEPART(dw,@intdate)=4 THEN N'چهارشنبه'
        WHEN DATEPART(dw,@intdate)=5 THEN N'پنجشنبه'
        WHEN DATEPART(dw,@intdate)=6 THEN N'جمعه'
    END
SET @DDCNT=
    CASE
        WHEN @SHDD=N'شنبه' THEN 1
        WHEN @SHDD=N'یکشنبه' THEN 2
        WHEN @SHDD=N'دوشنبه' THEN 3
        WHEN @SHDD=N'سه شنبه' THEN 4
        WHEN @SHDD=N'چهارشنبه' THEN 5
        WHEN @SHDD=N'پنجشنبه' THEN 6
        WHEN @SHDD=N'جمعه' THEN 7
    END


SET @SHDATE =
 REPLACE(
 REPLACE(
 REPLACE(
 REPLACE(
 REPLACE(
 REPLACE(
 REPLACE(
 REPLACE(
 REPLACE(
 REPLACE(
 REPLACE(@Format,'MaahHarfi',@SHMM),'SaalRooz',LTRIM(STR(@YYDD,3))),'ChandShanbeAdadi',@DDCNT),'ChandShanbe',
         @SHDD),'Rooz2',REPLACE(STR(@DD,2), ' ', '0')),'Maah2',REPLACE(STR(@MM, 2), ' ', '0')),'Saal2',
         SUBSTRING(STR(@YY,4),3,2)),'Saal4',STR(@YY,4)),'Saal',LTRIM(STR(@YY,4))),'Maah',
         LTRIM(STR(@MM,2))),'Rooz',LTRIM(STR(@DD,2)))
/* Format Samples:
Format='ChandShanbe Rooz MaahHarfi Saal' -> پنجشنبه 17 اردیبهشت 1394
Format='Rooz MaahHarfi Saal' -> ـ 17 اردیبهشت 1394
Format='Rooz/Maah/Saal' -> 1394/2/17
Format='Rooz2/Maah2/Saal2' -> 94/02/17
Format='Rooz روز گذشته از MaahHarfi در سال Saal2' -> ـ 17 روز گذشته از اردیبهشت در سال 94
*/

RETURN @SHDATE
END
GO
