Drop table Admin;
Drop table Person;
Drop table Projects;
Drop table EducationalMaterials;
Drop table Events;
Drop table Participants;
Drop table Events_Participants;
Drop table Guests;
Drop table Events_Guests;
Drop table Post;
Drop table Teacher;
Drop table Specialization;
Drop table Discipline;
Drop table TypeWork;
Drop table Specialization_Discipline;
Drop table TimeManage;
Drop table SupervisedGroup;
Drop table Grade;






create table Person(
PersonID INT  NOT NULL PRIMARY KEY IDENTITY, 
FirstName  NVARChar(50) NOT NULL,
LastName   NVARChar(50) NOT NULL,
Patronymic NVARChar(50)
);

/* �� ����� delete cascade. ����� on delete set null    */
create table Admin(
FKPersonID  INT  NOT NULL REFERENCES Person (PersonID) on delete cascade,

AdminID INT  NOT NULL PRIMARY KEY IDENTITY, 
Login  NVARChar(50) NOT NULL,
Password   NVARChar(50) NOT NULL
);

/*-------------Projects-----------------*/

create table Projects(
FKAdminID INT Foreign Key (FKAdminID) References Admin(AdminID),

ProjectsID INT  NOT NULL PRIMARY KEY IDENTITY, 
TypeOfProject  NVARChar(MAX) NOT NULL
);


/*--------------------------------------*/

create table EducationalMaterials(
FKAdminID INT Foreign Key (FKAdminID) References Admin(AdminID),

EducationalMaterialsID INT  NOT NULL PRIMARY KEY IDENTITY, 
MaterialType NVARChar(MAX) NOT NULL,
MaterialName NVARChar(MAX) NOT NULL,
MaterialAutor NVARChar(MAX) NOT NULL,
PublicationYear DATE not null
);
/*-----------------------------------*/


create table Events(
FKAdminID INT Foreign Key (FKAdminID) References Admin(AdminID),

EventsID INT  NOT NULL PRIMARY KEY IDENTITY, 
EventType NVARChar(50) NOT NULL,
EventName NVARChar(MAX) NOT NULL,
EventDate Date Not Null,
);

create table Participants(
ParticipantsID INT  NOT NULL PRIMARY KEY IDENTITY, 
FirstName  NVARChar(50) NOT NULL,
LastName   NVARChar(50) NOT NULL,
Patronymic NVARChar(50)

);

create table Events_Participants(
Events_ParticipantsID INT  NOT NULL PRIMARY KEY IDENTITY, 
FKParticipantsID INT Foreign Key (FKParticipantsID) References Participants(ParticipantsID),
FKEventsID INT Foreign Key (FKEventsID) References Events(EventsID)
);

----������ �������!!!----


CREATE FUNCTION GetParticipantEvents()
RETURNS TABLE 
AS
RETURN 
(
    SELECT EP.Events_ParticipantsID, P.LastName, P.FirstName, P.Patronymic, E.EventName, E.EventDate
    FROM Participants P
    INNER JOIN Events_Participants EP ON P.ParticipantsID = EP.FKParticipantsID
    INNER JOIN Events E ON EP.FKEventsID = E.EventsID
)

SELECT * FROM GetParticipantEvents()





--- ������ ��������� ��������

CREATE PROCEDURE DeleteEventParticipant
    @Events_ParticipantsID INT
AS
BEGIN
    DELETE FROM Events_Participants
    WHERE Events_ParticipantsID = @Events_ParticipantsID;
END


EXEC DeleteEventParticipant @Events_ParticipantsID = 1;





------------- ����� ��������!!

CREATE TRIGGER trg_After_Delete_Events
ON Events
AFTER DELETE
AS
BEGIN
    DELETE EP
    FROM Events_Participants EP
    JOIN deleted d ON EP.FKEventsID = d.EventsID
END


CREATE TRIGGER trg_After_Delete_Participants
ON Participants
AFTER DELETE
AS
BEGIN
    DELETE EP
    FROM Events_Participants EP
    JOIN deleted d ON EP.FKParticipantsID = d.ParticipantsID
END

----�������������� ����� �������� ������� �� ������ Events � Participants, � ��� ������ ��� ��������� ������ �� ������� Events_Participants
---------


create table Guests(
GuestsID INT  NOT NULL PRIMARY KEY IDENTITY, 
FirstName  NVARChar(50) NOT NULL,
LastName   NVARChar(50) NOT NULL,
Patronymic NVARChar(50)
);

create table Events_Guests(
Events_GuestsID INT  NOT NULL PRIMARY KEY IDENTITY, 
FKGuestsID INT Foreign Key (FKGuestsID) References Guests(GuestsID),
FKEventsID INT Foreign Key (FKEventsID) References Events(EventsID)
);


CREATE FUNCTION GetGuestsEvents()
RETURNS TABLE 
AS
RETURN 
(
    SELECT EP.Events_GuestsID, P.LastName, P.FirstName, P.Patronymic, E.EventName, E.EventDate
    FROM Guests P
    INNER JOIN Events_Guests EP ON P.GuestsID = EP.FKGuestsID
    INNER JOIN Events E ON EP.FKEventsID = E.EventsID
)

Select * from GetGuestsEvents()


CREATE PROCEDURE DeleteEventGuest
    @Events_GuestsID INT
AS
BEGIN
    DELETE FROM Events_Guests
    WHERE Events_GuestsID = @Events_GuestsID;
END


EXEC DeleteEventGuest @Events_GuestsID = 1;

--------�����!!!!��� ������ ���� �����������, � ������� ��������� ��������� �������

CREATE PROCEDURE GetEventsForParticipant
    @ParticipantID INT
AS
BEGIN
    SELECT E.EventType, E.EventName, E.EventDate
    FROM Events E
    INNER JOIN Events_Participants EP ON E.EventsID = EP.FKEventsID
    WHERE EP.FKParticipantsID = @ParticipantID;
END;


EXEC GetEventsForParticipant @ParticipantID = 1;







-----------------------------------------------------------------------------------------------------------------
/*-------------------------------------*/

create table Post(
PostID   INT  NOT NULL PRIMARY KEY IDENTITY, 
PostName  NVARChar(50) NOT NULL,
);

create table Teacher(
FKPostID INT Foreign Key (FKPostID) References Post(PostID),
FKPersonID  INT  NOT NULL REFERENCES Person (PersonID) on delete cascade,

TeacherID INT  NOT NULL PRIMARY KEY IDENTITY, 
Login  NVARChar(50) NOT NULL,
Password   NVARChar(50) NOT NULL
);


/*-----------------------*/

create table Specialization(
SpecializationID INT  NOT NULL PRIMARY KEY IDENTITY,
SpecializationName NVARChar(50) NOT NULL
);

create table Discipline(
DisciplineID   INT  NOT NULL PRIMARY KEY IDENTITY,
DisciplineName  NVARChar(max) NOT NULL,
AwarageTime INT NOT NULL  /*NVARChar(50)*/
);

create table TypeWork(
TypeWorkID INT  NOT NULL PRIMARY KEY IDENTITY,
TypeWorkName NVARChar(50) NOT NULL
);

create table Specialization_Discipline(
FKSpecializationID INT Foreign Key (FKSpecializationID) References Specialization(SpecializationID),
FKDisciplineID INT Foreign Key (FKDisciplineID) References Discipline(DisciplineID),

Specialization_DisciplineID INT  NOT NULL PRIMARY KEY IDENTITY

);



create table TimeManage(
FKTeacherID INT Foreign Key (FKTeacherID) References Teacher(TeacherID),
FKTypeWorkID   INT Foreign Key (FKTypeWorkID) References TypeWork(TypeWorkID),
FKDisciplineID INT Foreign Key (FKDisciplineID) References Discipline(DisciplineID),

TimeManageID   INT PRIMARY KEY Identity NOT NULL,
);

/***********************************************/

create table SupervisedGroup(
FKTeacherID INT Foreign Key (FKTeacherID) References Teacher(TeacherID),
FKSpecializationID INT Foreign Key (FKSpecializationID) References Specialization(SpecializationID),

SupervisedGroupID   INT PRIMARY KEY Identity NOT NULL,
GroupName   NVARChar(50) NOT NULL,
StudentsCount int not null
);

create table Grade(
FKSupervisedGroupID INT Foreign Key (FKSupervisedGroupID) References SupervisedGroup(SupervisedGroupID),
FKDisciplineID INT Foreign Key (FKDisciplineID) References Discipline(DisciplineID),
FKTypeWorkID   INT Foreign Key (FKTypeWorkID) References TypeWork(TypeWorkID),


GradeID  INT PRIMARY KEY Identity NOT NULL,
AverageRating float 
);


INSERT INTO Person (FirstName, LastName, Patronymic)
VALUES ('����', '������', '��������'),
       ('���������', '�������', '�������������'),
       ('�������', '�������', '���������'),
	   ('�����', '�������', '��������'),
       ('���������', '�������', '���������'),
       ('�������', '���������', '������������'),
       ('�����', '��������', '���������');

INSERT INTO Admin (FKPersonID, Login, Password)
VALUES (1, 'admin1', 'password1'),
       (2, 'admin2', 'password2'),
       (3, 'admin3', 'password3'),
	   (4, 'admin4', 'password4'),
       (5, 'admin5', 'password5'),
       (6, 'admin6', 'password6'),
       (7, 'admin7', 'password7');

	   INSERT INTO Projects (FKAdminID, TypeOfProject)
VALUES (1, '����������������� ������ 1'),
       (2, '��������������� ������ 1'),
       (3, '������� ������ 1'),
	   (4, '����������������� ������ 2'),
       (5, '��������������� ������ 2'),
       (6, '������� ������ 2'),
       (7, '����������������� ������ 3');

INSERT INTO EducationalMaterials (FKAdminID, MaterialType, MaterialName, MaterialAutor, PublicationYear)
VALUES (1, '�������', '���������� ��� ����������', '������ �.�.', '2022-01-15'),
       (2, '������', '������� ���������', '������� �.�.', '2023-03-20'),
       (3, '������', '������', '������� �.�.', '2023-02-10'),
	   (4, '�������', '����� ��� ����������', '������� �.�.', '2023-07-10'),
       (5, '������', '������� �������', '������� �.�.', '2023-08-15'),
       (6, '������', '����������', '��������� �.�.', '2023-06-05'),
       (7, '�������', '���������������� �� Python', '�������� �.�.', '2023-09-20');

INSERT INTO Events (FKAdminID, EventType, EventName, EventDate)
VALUES (1, '�����������', '������� ����������� �� ������', '2023-04-25'),
       (2, '������', '������ �� ������� ���������', '2023-03-10'),
       (3, '�������', '������� �� ����������', '2023-05-05'),
	   (4, '�����������', '������� � Epam', '2023-04-12'),
       (5, '�������', '������� �� ������� ���������', '2023-03-15'),
       (6, '�����������', '����������� �� �������������� �����������', '2023-05-20'),
       (7, '�������', '������� �� ������', '2023-06-02');

INSERT INTO Participants (FirstName, LastName, Patronymic)
VALUES ('����', '��������', '�������������'),
       ('�������', '������', '��������'),
       ('�����', '�������', '��������'),
	   ('�����', '��������', '���������'),
       ('������', '������', '��������'),
       ('�����', '�������', '���������'),
       ('��������', '������', '����������');

INSERT INTO Events_Participants (FKParticipantsID, FKEventsID)
VALUES (1, 1),
       (2, 1),
       (3, 2),
	   (4, 1),
       (5, 1),
       (6, 3),
       (7, 4),
	   (1, 5),
		(2, 2),
		(3, 6),
		(4, 7),
		(5, 3),
		(6, 1),		
		(7, 4),
		(1, 4),
		(2, 7),
		(3, 5);

INSERT INTO Guests (FirstName, LastName, Patronymic)
VALUES ('������', '��������', '��������'),
       ('�����', '��������', '���������'),
       ('������', '������', '��������'),
	   ('����', '���������', '��������'),
       ('������', '�������', '�������������'),
       ('�������', '�������', '���������'),
       ('�����', '�������', '��������');

	   INSERT INTO Events_Guests (FKGuestsID, FKEventsID)
VALUES (1, 1),
       (2, 1),
       (3, 3),
	   (4, 1),
       (5, 2),
       (6, 4),
       (7, 4);

	   INSERT INTO Post (PostName)
VALUES ('���������'),
       ('������'),
       ('���������'),
	   ('�������');


INSERT INTO Teacher (FKPostID, FKPersonID, Login, Password)
VALUES (1, 1, 'teacher1', 'password1'),
       (2, 2, 'teacher2', 'password2'),
       (3, 3, 'teacher3', 'password3'),
	   (4, 4, 'teacher4', 'password4'),
       (1, 5, 'teacher5', 'password5'),
       (2, 6, 'teacher6', 'password6'),
       (3, 7, 'teacher7', 'password7');

INSERT INTO Specialization (SpecializationName)
VALUES ('�������������� ������� � ����������'),
       ('����������� ���������'),
       ('������������ ������������'),
	   ('��������'),
       ('��������������� ��������� �������'),
       ('�������������� ������� � ����'),
       ('����������� ����������� �������������� ����������');


INSERT INTO Discipline (DisciplineName, AwarageTime)
VALUES ('���� ������', 90),
       ('���������������� ��� ���� ��������', 120),
       ('���������������� � .Net', 60),
	   ('��������� ����������������', 90),
       ('��������-��������������� ���������� ���������������� � ��������� ��������������', 120),
       ('���� ������', 60),
       ('�������������', 90);

INSERT INTO TypeWork (TypeWorkName)
VALUES ('������'),
       ('������������ �������'),
       ('������������ ������');

INSERT INTO Specialization_Discipline (FKSpecializationID, FKDisciplineID)
VALUES (1, 1),
       (2, 2),
       (3, 3),
	   (3, 4),
	   (6, 7),
	   (5, 5),
	   (5, 6),
	   (7, 1),
       (7, 2),
       (7, 3),
       (7, 4);

INSERT INTO TimeManage (FKTeacherID, FKTypeWorkID, FKDisciplineID)
VALUES (1, 1, 1),
       (2, 2, 2),
       (3, 3, 3),
	   (4, 1, 1),
       (5, 2, 2),
       (6, 3, 3),
       (7, 1, 4);

INSERT INTO SupervisedGroup (FKTeacherID, FKSpecializationID, GroupName, StudentsCount)
VALUES (1, 1, '��-1', 25),
       (2, 2, '��-3', 30),
       (3, 3, '��-1', 20),
	   (4, 4, '���-1', 25),
       (5, 5, '��-1', 30),
       (6, 6, '��-1', 20),

       (7, 7, '��-1', 25),
	   (2, 7, '��-2', 25);


INSERT INTO Grade (FKSupervisedGroupID, FKDisciplineID, FKTypeWorkID, AverageRating)
VALUES (1, 1, 1, 4.5),
       (2, 2, 2, 4.0),
       (3, 3, 3, 4.8),
	   (1, 2, 1, 4.8),
       (2, 3, 2, 4.2),
       (3, 2, 3, 4.6),
       (4, 2, 1, 4.7);



SELECT *
FROM SupervisedGroup sg
INNER JOIN Teacher t ON sg.FKTeacherID = t.TeacherID
LEFT JOIN Grade g ON sg.SupervisedGroupID = g.FKSupervisedGroupID
WHERE sg.StudentsCount >= 25
ORDER BY sg.FKTeacherID;




     --SELECT * FROM Person;
     --INSERT INTO Person (FirstName, LastName, Patronymic) VALUES ('����� ���', '����� �������', '����� ��������');
     --UPDATE Person SET FirstName = '���������� ���' WHERE PersonID = 1;
     --DELETE FROM Person WHERE PersonID = 1;


     --SELECT * FROM Person;
     --INSERT INTO Admin (FKPersonID, Login, Password) VALUES (4, 'newadmin', 'newpassword');
     --UPDATE Projects SET TypeOfProject = '���������� ������' WHERE ProjectsID = 1;
     --DELETE FROM EducationalMaterials WHERE EducationalMaterialsID = 2;
     --SELECT * FROM Events;
     --INSERT INTO Participants (FirstName, LastName, Patronymic) VALUES ('�����', '��������', '��������');
     --UPDATE Guests SET LastName = '���������� �������' WHERE GuestsID = 1;
     --DELETE FROM Teacher WHERE TeacherID = 2;
     --SELECT * FROM Specialization_Discipline;
     --INSERT INTO TimeManage (FKTeacherID, FKTypeWorkID, FKDisciplineID) VALUES (3, 2, 1);
     --UPDATE SupervisedGroup SET StudentsCount = 35 WHERE SupervisedGroupID = 1;
     --DELETE FROM Grade WHERE GradeID = 3;
     











Select * from SupervisedGroup
where StudentsCount >= 25
order by FKTeacherID




/* ��� ������� � ��������, ������� � ����������� */
SELECT Person.FirstName, Person.LastName, Teacher.Login, Post.PostName AS Position
FROM Teacher
INNER JOIN Person ON Teacher.FKPersonID = Person.PersonID
INNER JOIN Post ON Teacher.FKPostID = Post.PostID;




/* ������ - �������*/
SELECT SupervisedGroup.GroupName, SupervisedGroup.StudentsCount, Grade.AverageRating, Discipline.DisciplineName
FROM SupervisedGroup
INNER JOIN Grade ON Grade.FKSupervisedGroupID = SupervisedGroup.SupervisedGroupID
INNER JOIN Discipline ON Discipline.DisciplineID = Grade.FKDisciplineID;

/*����� ��������������, �� ������ � �������� �����, �������� ��� ���������:*/
select Person.LastName, Person.FirstName, Teacher.Login, SupervisedGroup.GroupName
from Person
Inner join Teacher on Teacher.FKPersonID = Person.PersonID
inner join SupervisedGroup on SupervisedGroup.FKTeacherID = Teacher.TeacherID;


/* ������������� � ����������*/
SELECT Specialization.SpecializationName, Discipline.DisciplineName
FROM Specialization_Discipline
INNER JOIN Specialization ON Specialization_Discipline.FKSpecializationID = Specialization.SpecializationID
INNER JOIN Discipline ON Specialization_Discipline.FKDisciplineID = Discipline.DisciplineID;


/* ����������� - �������*/
SELECT Events.EventName, Participants.FirstName, Participants.LastName
FROM Events
INNER JOIN Events_Participants ON Events.EventsID = Events_Participants.FKEventsID
INNER JOIN Participants ON Events_Participants.FKParticipantsID = Participants.ParticipantsID;


/* ������ - �����*/
SELECT Projects.TypeOfProject, Admin.Login
FROM Projects
INNER JOIN Admin ON Projects.FKAdminID = Admin.AdminID;


/* ��������� ��������� ��������� */


/* ��������� ������� */

-- ������ ���� ���������� �����������

CREATE FUNCTION GetParticipantsList()
RETURNS TABLE
AS
RETURN (
    SELECT FirstName, LastName, Patronymic
    FROM Participants
);


SELECT * FROM GetParticipantsList();



CREATE FUNCTION GetProjectsForAdmin(@AdminID INT)
RETURNS TABLE
AS
RETURN (
    SELECT P.TypeOfProject, P.ProjectsID
    FROM Projects AS P
    WHERE P.FKAdminID = @AdminID
);

SELECT * FROM GetProjectsForAdmin(3);


/* ��������� */

-- ���������� ���������� �� �����������
CREATE FUNCTION GetParticipantsCount(@EventID INT)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    
    SELECT @Count = COUNT(*)
    FROM Events_Participants
    WHERE FKEventsID = @EventID;

    RETURN @Count;
END;

SELECT dbo.GetParticipantsCount(1) AS ParticipantsCount;






CREATE FUNCTION GetTeacherFullName(@TeacherID INT)
RETURNS NVARCHAR(150)
AS
BEGIN
    DECLARE @FullName NVARCHAR(150);
    SELECT @FullName = FirstName + ' ' + LastName + ' ' + ISNULL(Patronymic, '')
    FROM Person
    WHERE PersonID = (SELECT FKPersonID FROM Teacher WHERE TeacherID = @TeacherID);
    
    RETURN @FullName;
END;

SELECT dbo.GetTeacherFullName(4); 


/* ��������� */

--������� - �����, �� � ������������ �����������


/*
('���� ������', 90),
       ('���������������� ��� ���� ��������', 120),
       ('���������������� � .Net', 60),
     ('��������� ����������������', 90),
       ('��������-��������������� ���������� ���������������� � ��������� ��������������', 120),
       ('���� ������', 60),
       ('�������������', 90);*/

CREATE FUNCTION GetTeachersForDiscipline(@DisciplineName NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN (
    SELECT T.Login
    FROM Teacher T
    INNER JOIN TimeManage TM ON T.TeacherID = TM.FKTeacherID
    INNER JOIN Discipline D ON TM.FKDisciplineID = D.DisciplineID
    WHERE D.DisciplineName = @DisciplineName
);

DECLARE @DisciplineName NVARCHAR(MAX) = '���������������� ��� ���� ��������'; 
SELECT Login FROM GetTeachersForDiscipline(@DisciplineName);






CREATE FUNCTION GetAverageGradeForGroup(
    @SupervisedGroupID INT,
    @DisciplineID INT,
    @TypeWorkID INT
)
RETURNS FLOAT
AS
BEGIN
    DECLARE @AvgGrade FLOAT;
    
    SELECT @AvgGrade = AVG(G.AverageRating)
    FROM Grade AS G
    WHERE G.FKSupervisedGroupID = @SupervisedGroupID
    AND G.FKDisciplineID = @DisciplineID
    AND G.FKTypeWorkID = @TypeWorkID;
    
    RETURN @AvgGrade;
END;

SELECT dbo.GetAverageGradeForGroup(1, 1, 1);








/*------------------------------��������------------------------------------------------------*/



CREATE TRIGGER CreateTeacherAndAdminAccounts
ON Person
AFTER INSERT
AS
BEGIN
    DECLARE @PersonID INT
    SELECT @PersonID = PersonID FROM inserted

    -- ������� ������ �������������
    INSERT INTO Teacher (FKPersonID, Login, Password)
    VALUES (@PersonID, 'DefaultTeacherLogin', 'DefaultTeacherPassword')

    -- ������� ������ �������������� (����� ������)
    INSERT INTO Admin (FKPersonID, Login, Password)
    VALUES (@PersonID, 'DefaultAdminLogin', 'DefaultAdminPassword')
END


INSERT INTO Person (FirstName, LastName, Patronymic)
VALUES ('���������', '�����������', '�������������')







-- �������� �������� ��� ���������� �������� ��������
CREATE TRIGGER ManageProjectStatus
ON Projects
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @ProjectID INT;
    
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- ��������� ������� "��������" ��� ������� ��� ���������� �������
        SELECT @ProjectID = ProjectsID FROM inserted;
        UPDATE Projects SET Status = 'Active' WHERE ProjectsID = @ProjectID;
    END
END;

UPDATE Projects
SET TypeOfProject = '����������������� ������ 3'
WHERE ProjectsID = 7;







/* ������������ ������ ��������� � ������� "Events" 
� �������, ��������, ��� � ����� ���� ���������.*/



-- �������� ������� ��� ������� ������
CREATE TABLE AuditLog (
    LogID INT IDENTITY(1, 1) PRIMARY KEY,
    TableName NVARCHAR(255),
    Action NVARCHAR(10),
    Username NVARCHAR(50),
    ActionDate DATETIME
);
ALTER TABLE AuditLog
ALTER COLUMN Action NVARCHAR(50);

-- �������� �������� ��� ������
CREATE TRIGGER AuditEvents
ON Events
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @TableName NVARCHAR(255)
    SET @TableName = 'Events'
    
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        INSERT INTO AuditLog (TableName, Action, Username, ActionDate)
        SELECT @TableName, 'INSERT/UPDATE', SYSTEM_USER, GETDATE()
    END
    ELSE
    BEGIN
        INSERT INTO AuditLog (TableName, Action, Username, ActionDate)
        SELECT @TableName, 'DELETE', SYSTEM_USER, GETDATE()
    END
END;

-- ������ ���������� ������ � ������� "Events", ���������� �������
UPDATE Events
SET EventName = '������� �� �����������'
WHERE EventsID = 6;










-- �������� ��������
CREATE TRIGGER CheckGradeIntegrity
ON Grade
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted WHERE AverageRating > 10 OR AverageRating < 0)
    BEGIN
        THROW 51000, '������ ������ ���� ����� 0 � 10.', 1;
    END
END;

-- ������ ������� ������, ���������� �������
INSERT INTO Grade (FKSupervisedGroupID, FKDisciplineID, FKTypeWorkID, AverageRating)
VALUES (1, 2, 3, 11); -- ��� ������� ������ ��-�� ������ 11