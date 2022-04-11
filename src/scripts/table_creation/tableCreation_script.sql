--USE database;
--GO

--EventPlanner Tables
--=============================================
--User Table

CREATE TABLE Users(
	UserID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	FirstName VARCHAR (31) NOT NULL,
	LastName VARCHAR (31) NOT NULL,
	Email VARCHAR(31) NOT NULL
--Validate email
CONSTRAINT CHK_emailRegex CHECK (Email LIKE '%___@___%')
);
GO


--Events Table
CREATE TABLE Event(
	EventID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
       UserID  INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
       RecurringID INT NOT NULL FOREIGN KEY REFERENCES RecurringEvents(recurringID),
       EventName VARCHAR (31) NOT NULL,
	EventDesc VARCHAR (31) NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate   DATETIME NULL, --Users can leave out end date for recurring events
	isFullDay BIT NOT NULL,
	isRecurring BIT NOT NULL
);
GO


--RecurringEvents
CREATE TABLE RecurringEvents(
	RecurringID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	OccurenceType INT NOT NULL DEFAULT 0
	CONSTRAINT CHK_OccurenceType CHECK (OccurenceType=1 OR OccurenceType=2 OR OccurenceType=3 OR OccurenceType=4)
);
GO
--//--DEFAULT IS 0, 1-DAILY, 2-WEEKLY, 3-MONTHLY, 4-YEARLY for occurenceType
