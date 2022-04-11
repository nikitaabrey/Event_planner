--USE database;
--GO

--EventPlanner Tables
--=============================================
--User Table

CREATE TABLE Users(
	userID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	firstName VARCHAR (31) NOT NULL,
	lastName VARCHAR (31) NOT NULL,
	email VARCHAR(31) NOT NULL
--Validate email
CONSTRAINT CHK_emailRegex CHECK (email LIKE '%___@___%')
);
GO


--Events Table
CREATE TABLE Event(
	eventID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    userID  INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    recurringID INT NOT NULL FOREIGN KEY REFERENCES RecurringEvents(recurringID),
	eventName VARCHAR (31) NOT NULL,
	eventDesc VARCHAR (31) NOT NULL,
	startDate DATETIME NOT NULL,
	endDate   DATETIME NULL, --Users can leave out end date for recurring events
	isFullDay BIT NOT NULL,
	isRecurring BIT NOT NULL
);
GO


--RecurringEvents
CREATE TABLE RecurringEvents(
	recurringID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	occurenceType INT NOT NULL DEFAULT 0
	CONSTRAINT CHK_occurenceType CHECK (occurenceType=1 OR occurenceType=2 OR occurenceType=3 OR occurenceType=4)
);
GO
--//--DEFAULT IS 0, 1-DAILY, 2-WEEKLY, 3-MONTHLY, 4-YEARLY for occurenceType
