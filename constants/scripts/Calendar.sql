DROP TABLE IF EXISTS #temp
DROP TABLE IF EXISTS dbo.Calendar
DROP TABLE IF EXISTS dbo.DayOfWeek
DROP TABLE IF EXISTS dbo.DayOfMonth
DROP TABLE IF EXISTS dbo.CalendarMonth
DROP TABLE IF EXISTS dbo.CalendarYear

GO

CREATE TABLE  dbo.DayOfWeek (
	[DayOfWeek] INT PRIMARY KEY,
	[Name] VARCHAR(15) UNIQUE,
	isWeekend BIT,
	CHECK ([DayOfWeek] >=1 AND [DayOfWeek] <= 7)
);

CREATE TABLE  dbo.DayOfMonth (
	[DayOfMonth] INT PRIMARY KEY,
	Suffix  VARCHAR(2),
	CHECK ([DayOfMonth] >= 1 AND [DayOfMonth] <= 31)
);


CREATE TABLE dbo.CalendarMonth (
	[Month] INT PRIMARY KEY,
	[Name] VARCHAR(20) UNIQUE,
	CHECK ([Month] >= 1 AND [Month] <= 12)
);

CREATE TABLE dbo.CalendarYear (
	[Year] INT PRIMARY KEY,
	isLeapYear BIT
);

CREATE TABLE dbo.Calendar (
	[Year] INT FOREIGN KEY REFERENCES dbo.CalendarYear ([year]),
	[Month]  INT FOREIGN KEY REFERENCES dbo.CalendarMonth ([month]),
	[DayOfMonth] INT FOREIGN KEY REFERENCES dbo.DayOfMonth ([DayOfMonth]),
	[DayOfWeek] INT FOREIGN KEY REFERENCES dbo.DayOfWeek ([DayOfWeek]),
	[DayOfYear] INT NULL,
	[DayOfWeekInMonth] INT NULL,
	[WeekOfMonth] INT NULL,
	[WeekOfYear] INT NULL,
	PRIMARY KEY ([DayOfMonth],[Month],[Year]),
	CHECK ([DayOfYear] >= 1 AND [DayOfYear] <= 366),
	CHECK ([WeekOfMonth] >= 1 AND [WeekOfMonth] <= 6),
	CHECK ([WeekOfYear] >= 1 AND [WeekOfYear] <= 54),
	CHECK ([DayOfWeekInMonth] >= 1 AND [DayOfWeekInMonth] <= 5)

);


SET DATEFIRST  1, -- 1 = Monday, 7 = Sunday
    DATEFORMAT mdy, 
    LANGUAGE   US_ENGLISH;


DECLARE @StartDate  date = '20100101';

DECLARE @CutoffDate date = DATEADD(DAY, -1, DATEADD(YEAR, 30, @StartDate));

;WITH seq(n) AS 
(
  SELECT 0 UNION ALL SELECT n + 1 FROM seq
  WHERE n < DATEDIFF(DAY, @StartDate, @CutoffDate)
),
d(d) AS 
(
  SELECT DATEADD(DAY, n, @StartDate) FROM seq
),
src AS
(
  SELECT
    CalendarDate   		= CONVERT(date, d),
    [Day]          		= DATEPART(DAY,       d),
    [DayName]      		= DATENAME(WEEKDAY,   d),
	[DayOfWeek]    		= DATEPART(WEEKDAY,   d),
	[DayOfYear]    		= DATEPART(DAYOFYEAR, d),
    WeekOfYear         	= DATEPART(WEEK,      d),
    [Month]        		= DATEPART(MONTH,     d),
    [MonthName]    		= DATENAME(MONTH,     d),
	FirstOfMonth 		= DATEFROMPARTS(YEAR(d), MONTH(d), 1),
	[Year]         		= DATEPART(YEAR,      d),
    LastOfYear   		= DATEFROMPARTS(YEAR(d), 12, 31)
  FROM d
), 
addons as (
	
	SELECT
	CalendarDate,
	[Day],
	DaySuffix			= CONVERT(char(2), CASE WHEN [Day] / 10 = 1 THEN 'th' ELSE 
                            CASE RIGHT([Day], 1) WHEN '1' THEN 'st' WHEN '2' THEN 'nd' 
                            WHEN '3' THEN 'rd' ELSE 'th' END END),
	[DayName],
	[DayOfWeek],
	DayOfWeekInMonth 	= CONVERT(tinyint, ROW_NUMBER() OVER 
                            (PARTITION BY FirstOfMonth, [DayOfWeek] ORDER BY [CalendarDate])),
	[DayOfYear],
	IsWeekend           = CASE WHEN [DayOfWeek] IN (CASE @@DATEFIRST WHEN 1 THEN 6 WHEN 7 THEN 1 END,7) 
                            THEN 1 ELSE 0 END,
	WeekOfYear,
    FirstOfWeek      	= DATEADD(DAY, 1 - [DayOfWeek], CalendarDate),
    LastOfWeek       	= DATEADD(DAY, 6, DATEADD(DAY, 1 - [DayOfWeek], CalendarDate)),
	WeekOfMonth      	= CONVERT(tinyint, DENSE_RANK() OVER 
                            (PARTITION BY [Year], [Month] ORDER BY WeekOfYear)),

	[Month],
	FirstOfMonth,
	[MonthName],

    LastOfMonth      	= MAX([CalendarDate]) OVER (PARTITION BY [Year], [Month]),
	FirstOfNextMonth 	= DATEADD(MONTH, 1, FirstOfMonth),
    LastOfNextMonth  	= DATEADD(DAY, -1, DATEADD(MONTH, 2, FirstOfMonth)),
	[Year],
	FirstOfYear      	= DATEFROMPARTS([Year], 1,  1),
    LastOfYear,
    IsLeapYear          = CONVERT(bit, CASE WHEN ([Year] % 400 = 0) 
                            OR ([Year] % 4 = 0 AND [Year] % 100 <> 0) 
                            THEN 1 ELSE 0 END)
	from src
),
dim as (
	SELECT 
	CalendarDate,
	[Day],
	DaySuffix,
	[DayName],
	[DayOfWeek],
	DayOfWeekInMonth,
	[DayOfYear],
	IsWeekend,
	WeekOfYear,
    FirstOfWeek,
    LastOfWeek,
	WeekOfMonth,
	[Month],
	[MonthName],
	FirstOfMonth,
    LastOfMonth,
	DaysInMonth       = DATEDIFF(DAY, FirstOfMonth, LastOfMonth) + 1,
	FirstOfNextMonth,
    LastOfNextMonth,
	[Year],
	FirstOfYear,
    LastOfYear,
    IsLeapYear

	from addons
)
SELECT * INTO #temp FROM dim
  OPTION (MAXRECURSION 0);
 
GO

INSERT INTO dbo.DayOfWeek
SELECT DISTINCT [DayOfWeek], [DayName], [isWeekend] 
FROM #temp ORDER BY [DayOfWeek]

INSERT INTO dbo.DayOfMonth 
SELECT DISTINCT [Day],[DaySuffix] 
FROM #temp ORDER BY [Day]


INSERT INTO dbo.CalendarMonth
SELECT DISTINCT [Month], [MonthName] 
FROM #temp ORDER BY [Month]


INSERT INTO dbo.CalendarYear
SELECT DISTINCT [Year], IsLeapYear 
FROM #temp ORDER BY [Year]


INSERT INTO dbo.Calendar
SELECT [Year], [Month],[Day],[DayOfWeek],[DayOfYear],[DayOfWeekInMonth],[WeekOfMonth],[WeekOfYear] 
FROM #temp ORDER BY [CalendarDate]
