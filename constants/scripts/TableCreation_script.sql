DROP TABLE IF EXISTS dbo.[Event]
DROP TABLE IF EXISTS dbo.RecurringEvents
DROP TABLE IF EXISTS dbo.Users
DROP TABLE IF EXISTS dbo.Calendar
DROP TABLE IF EXISTS #temp
GO


CREATE TABLE dbo.Users(
	UserId INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	FirstName VARCHAR (31) NOT NULL,
	LastName VARCHAR (31) NOT NULL,
	Email VARCHAR(200) NOT NULL
);
GO

CREATE TABLE dbo.Calendar (
	[FullDate] DATE PRIMARY KEY,
	[Year] INT NOT NULL,
	[Month]  INT NOT NULL,
	[Day] INT NOT NULL,
	[DayOfWeek] INT NULL,
	[WeekOfMonth] INT NULL,
	[FirstOfWeek] DATE NULL,
	[LastOfWeek] DATE NULL,
	[DayOfYear] INT NULL,
	CHECK ([Month] >= 1 AND [Month] <= 12),
	CHECK ([Day] >= 1 AND [Day] <= 31),
	CHECK ([DayOfWeek] >= 1 AND [DayOfWeek] <= 7),
	CHECK ([WeekOfMonth] >= 1 AND [WeekOfMonth] <= 6),
	CHECK ([DayOfYear] >= 1 AND [DayOfYear] <= 366)
);


CREATE TABLE dbo.RecurringEvents (
	RecurringId CHAR(1) NOT NULL PRIMARY KEY,
	RecurringDesc VARCHAR(30) NOT NULL
);



CREATE TABLE dbo.[Event] (
	EventId INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    UserId  INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    RecurringId CHAR(1) NOT NULL FOREIGN KEY REFERENCES RecurringEvents(RecurringId),
	StartDate DATE NOT NULL FOREIGN KEY REFERENCES Calendar(FullDate),
	EndDate DATE NOT NULL FOREIGN KEY REFERENCES Calendar(FullDate),
    EventName VARCHAR (31) NOT NULL,
	StartTime TIME NULL,
	EndTime TIME NULL,
	EventDesc VARCHAR (500) NOT NULL,
	IsFullDay BIT NOT NULL
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
    FullDate   		= CONVERT(date, d),
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
	FullDate,
	[Day],
	DaySuffix			= CONVERT(char(2), CASE WHEN [Day] / 10 = 1 THEN 'th' ELSE 
                            CASE RIGHT([Day], 1) WHEN '1' THEN 'st' WHEN '2' THEN 'nd' 
                            WHEN '3' THEN 'rd' ELSE 'th' END END),
	[DayName],
	[DayOfWeek],
	DayOfWeekInMonth 	= CONVERT(tinyint, ROW_NUMBER() OVER 
                            (PARTITION BY FirstOfMonth, [DayOfWeek] ORDER BY [FullDate])),
	[DayOfYear],
	IsWeekend           = CASE WHEN [DayOfWeek] IN (CASE @@DATEFIRST WHEN 1 THEN 6 WHEN 7 THEN 1 END,7) 
                            THEN 1 ELSE 0 END,
	WeekOfYear,
    FirstOfWeek      	= DATEADD(DAY, 1 - [DayOfWeek], FullDate),
    LastOfWeek       	= DATEADD(DAY, 6, DATEADD(DAY, 1 - [DayOfWeek], FullDate)),
	WeekOfMonth      	= CONVERT(tinyint, DENSE_RANK() OVER 
                            (PARTITION BY [Year], [Month] ORDER BY WeekOfYear)),

	[Month],
	FirstOfMonth,
	[MonthName],

    LastOfMonth      	= MAX([FullDate]) OVER (PARTITION BY [Year], [Month]),
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
	FullDate,
	[Day],
	[DayOfWeek],
	WeekOfMonth,
	[Month],
	[Year],
	 [FirstOfWeek],
	 [LastOfWeek],
	 [DayOfYear] 
	from addons
)
SELECT * INTO #temp FROM dim
  OPTION (MAXRECURSION 0);
 GO

INSERT INTO dbo.Calendar
SELECT [FullDate],[Year], [Month],[Day],[DayOfWeek],[WeekOfMonth], [FirstOfWeek],[LastOfWeek],[DayOfYear]
FROM #temp ORDER BY [FullDate];

