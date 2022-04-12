--USE Master;

--CREATE DATABASE EventPlannerDB;
--GO

USE EventPlannerDB;
GO

--DROP DATABASE IF EXISTS EventPlannerDB;
--DROP TABLE IF EXISTS dbo.Users
--DROP TABLE IF EXISTS dbo.Calendar
--DROP TABLE IF EXISTS #temp
--DROP TABLE IF EXISTS dbo.RecurringEvents
--DROP TABLE IF EXISTS dbo.[Event]
--GO

--*****************************************************************************************************

CREATE TABLE Users(
	UserID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	FirstName VARCHAR (31) NOT NULL,
	LastName VARCHAR (31) NOT NULL,
	Email VARCHAR(31) NOT NULL
);
GO

CREATE TABLE dbo.Calendar (
	[FullDate] DATE PRIMARY KEY,
	[Year] INT,
	[Month]  INT,
	[Day] INT,
	[DayOfWeek] INT NULL,
	[WeekOfMonth] INT NULL,
	CHECK ([Month] >= 1 AND [Month] <= 12),
	CHECK ([Day] >= 1 AND [Day] <= 31),
	CHECK ([DayOfWeek] >= 1 AND [DayOfWeek] <= 7),
	CHECK ([WeekOfMonth] >= 1 AND [WeekOfMonth] <= 6)
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
	[Year]
	from addons
)
SELECT * INTO #temp FROM dim
  OPTION (MAXRECURSION 0);
 GO

INSERT INTO dbo.Calendar
SELECT [FullDate],[Year], [Month],[Day],[DayOfWeek],[WeekOfMonth]
FROM #temp ORDER BY [FullDate];

CREATE TABLE RecurringEvents(
	RecurringID CHAR(1) NOT NULL PRIMARY KEY,
	RecurringDesc VARCHAR(31) NOT NULL
);
GO

CREATE TABLE [Event](
	EventID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
        UserID  INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
        RecurringID CHAR(1) NOT NULL FOREIGN KEY REFERENCES RecurringEvents(RecurringID),
	FullDate DATE NOT NULL FOREIGN KEY REFERENCES Calendar(FullDate),
        EventName VARCHAR (31) NOT NULL,
	StartTime TIMESTAMP NULL,
	EndTime TIME NULL,
	EventDesc VARCHAR (500) NOT NULL,
	isFullDay BIT NOT NULL,
	isRecurring BIT NOT NULL,
	
);
GO

--*****************************************************************************************************
