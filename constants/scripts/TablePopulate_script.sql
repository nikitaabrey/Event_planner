USE EventPlannerDB;
GO

INSERT INTO Users (FirstName, LastName, Email)
VALUES
    ('Ahmed','Campbell','AhmedC','tristique.pellentesque.tellus@icloud.edu'),
    ('Constance','Bartlett','ConstanceB','purus.maecenas@outlook.edu'),
    ('John','Booker','john','purus@google.net');
GO

INSERT INTO RecurringEvents (RecurringId, RecurringDesc)
VALUES
    ('D','Daily'),
    ('W','Weekly'),
    ('M','Monthly'),
    ('Y','Yearly'),
    ('N','None');
GO

INSERT INTO Event (UserID, RecurringID, StartDate, EndDate, EventName, StartTime, EndTime, EventDesc, isFullDay)
VALUES
    ( 1, 'N', '2022-04-19', '2022-04-19','Presentation','09:00', '10:00', 'A presentation on LO', 0),
    ( 1, 'D','2022-04-19', '2022-04-19', 'Stadup', '08:00', '08:30', 'Give a brief discription on what you did', 0),
    ( 2, 'Y', '2022-11-30', '2022-11-30', 'Conference', '08:00', '17:00', 'RealWorld',1),
    ( 2, 'N', '2022-05-24', '2022-05-24', 'Lungi''s Birthday', '00:00', '23:59', 'Make sure you get a gift', 1),
    ( 2, 'N', '2022-05-16', '2022-05-20', 'Vacation Time', '08:00', '17:00', 'Out of office wont able to respond to emails', 1),
	( 3, 'Y','2022-04-15', '2022-04-15', 'Good Friday', '00:00', '23:59', 'Get Easter eggs', 1),
	( 3, 'Y','2022-10-16', '2022-10-16', 'Anniversary', '12:00', '17:00', 'It''s Calebs anniversay celebration be there', 0),
	( 1, 'M', '2022-07-29', '2022-07-29', 'Interview', '13:00', '15:00', 'Radio interview on how to reach your goals', 0),
	( 2, 'D', '2022-02-10', '2022-02-10', 'Dance class', '17:00', '19:00', 'Become a better dancer', 0),
	( 1, 'N','2022-06-18', '2022-06-18', 'Research proposal due', '08:00', '23:00', 'Use this day to check errors', 1),
	( 2, 'M', '2022-01-05', '2022-01-05', 'Reviews', '09:00', '11:00', 'Code reviews', 0),
	( 3, 'W','2022-03-05', '2022-03-05', 'TechTalks', '13:00', '13:30', 'Do your research and prepare slides', 0),
	( 3, 'M', '2022-02-10', '2022-02-10', 'Dr''s appointment', '09:00', '10:00', 'Are you still healthy? Do your checkups', 0),
	( 1, 'N', '2022-04-25', '2022-04-29', 'Training', '08:00', '17:00', 'Orientation on new features', 1),
	( 1, 'D', '2022-03-5', '2022-03-05', 'Gym', '05:00', '07:00', 'A healthy body is a healthy mind', 0),
	( 3, 'Y', '2022-04-19', '2022-04-19', 'Halloween', '00:00', '23:59', 'Get a super scary costume', 1),
	( 2, 'N', '2022-06-10', '2022-06-10', 'Exam', '10:00', '12:00', 'AWS Certified Cloud Practitioner exam', 0),
	( 2, 'W', '2022-01-18', '2022-01-18', 'Presentations', '12:00', '13:00', 'Knowledge share', 0),
	( 1, 'D', '2022-04-02', '2022-04-02', 'Studying', '20:00', '23:00', 'Learn something new', 0),
	( 1, 'N', '2022-07-08', '2022-07-08', 'Marvel''s release', '00:00', '23:59', 'Thor Love And Thunder ', 1);
GO
