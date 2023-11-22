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

/* НЕ везде delete cascade. Чекни on delete set null    */
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

----Курсач функция!!!----


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





--- Курсач процедура удаления

CREATE PROCEDURE DeleteEventParticipant
    @Events_ParticipantsID INT
AS
BEGIN
    DELETE FROM Events_Participants
    WHERE Events_ParticipantsID = @Events_ParticipantsID;
END


EXEC DeleteEventParticipant @Events_ParticipantsID = 1;





------------- Курсч Триггеры!!

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

----активироваться после удаления записей из таблиц Events и Participants, и они удалют все связанные записи из таблицы Events_Participants
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

--------ПОТОМ!!!!для вывода всех мероприятий, в которых участвует выбранный человек

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
VALUES ('Иван', 'Иванов', 'Иванович'),
       ('Екатерина', 'Петрова', 'Александровна'),
       ('Алексей', 'Смирнов', 'Сергеевич'),
	   ('Мария', 'Петрова', 'Игоревна'),
       ('Александр', 'Соколов', 'Андреевич'),
       ('Наталья', 'Коваленко', 'Владимировна'),
       ('Павел', 'Михайлов', 'Сергеевич');

INSERT INTO Admin (FKPersonID, Login, Password)
VALUES (1, 'admin1', 'password1'),
       (2, 'admin2', 'password2'),
       (3, 'admin3', 'password3'),
	   (4, 'admin4', 'password4'),
       (5, 'admin5', 'password5'),
       (6, 'admin6', 'password6'),
       (7, 'admin7', 'password7');

	   INSERT INTO Projects (FKAdminID, TypeOfProject)
VALUES (1, 'Исследовательский проект 1'),
       (2, 'Образовательный проект 1'),
       (3, 'Научный проект 1'),
	   (4, 'Исследовательский проект 2'),
       (5, 'Образовательный проект 2'),
       (6, 'Научный проект 2'),
       (7, 'Исследовательский проект 3');

INSERT INTO EducationalMaterials (FKAdminID, MaterialType, MaterialName, MaterialAutor, PublicationYear)
VALUES (1, 'Учебник', 'Математика для начинающих', 'Иванов И.И.', '2022-01-15'),
       (2, 'Статья', 'История искусства', 'Петрова Е.А.', '2023-03-20'),
       (3, 'Лекция', 'Физика', 'Смирнов А.С.', '2023-02-10'),
	   (4, 'Учебник', 'Химия для начинающих', 'Петрова Е.А.', '2023-07-10'),
       (5, 'Статья', 'Мировая история', 'Соколов А.А.', '2023-08-15'),
       (6, 'Лекция', 'Астрономия', 'Коваленко Н.В.', '2023-06-05'),
       (7, 'Учебник', 'Программирование на Python', 'Михайлов П.С.', '2023-09-20');

INSERT INTO Events (FKAdminID, EventType, EventName, EventDate)
VALUES (1, 'Конференция', 'Научная конференция по физике', '2023-04-25'),
       (2, 'Лекция', 'Лекция по истории искусства', '2023-03-10'),
       (3, 'Семинар', 'Семинар по математике', '2023-05-05'),
	   (4, 'Конференция', 'Встреча с Epam', '2023-04-12'),
       (5, 'Семинар', 'Семинар по истории искусства', '2023-03-15'),
       (6, 'Конференция', 'Конференция по информационным технологиям', '2023-05-20'),
       (7, 'Семинар', 'Семинар по физике', '2023-06-02');

INSERT INTO Participants (FirstName, LastName, Patronymic)
VALUES ('Анна', 'Смирнова', 'Александровна'),
       ('Дмитрий', 'Павлов', 'Иванович'),
       ('Ольга', 'Козлова', 'Петровна'),
	   ('Ирина', 'Сидорова', 'Сергеевна'),
       ('Максим', 'Козлов', 'Иванович'),
       ('Елена', 'Иванова', 'Андреевна'),
       ('Владимир', 'Петров', 'Михайлович');

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
VALUES ('Михаил', 'Кузнецов', 'Игоревич'),
       ('Елена', 'Морозова', 'Сергеевна'),
       ('Андрей', 'Иванов', 'Петрович'),
	   ('Анна', 'Коваленко', 'Ивановна'),
       ('Сергей', 'Смирнов', 'Александрович'),
       ('Татьяна', 'Павлова', 'Андреевна'),
       ('Денис', 'Морозов', 'Игоревич');

	   INSERT INTO Events_Guests (FKGuestsID, FKEventsID)
VALUES (1, 1),
       (2, 1),
       (3, 3),
	   (4, 1),
       (5, 2),
       (6, 4),
       (7, 4);

	   INSERT INTO Post (PostName)
VALUES ('Профессор'),
       ('Доцент'),
       ('Ассистент'),
	   ('Инженер');


INSERT INTO Teacher (FKPostID, FKPersonID, Login, Password)
VALUES (1, 1, 'teacher1', 'password1'),
       (2, 2, 'teacher2', 'password2'),
       (3, 3, 'teacher3', 'password3'),
	   (4, 4, 'teacher4', 'password4'),
       (1, 5, 'teacher5', 'password5'),
       (2, 6, 'teacher6', 'password6'),
       (3, 7, 'teacher7', 'password7');

INSERT INTO Specialization (SpecializationName)
VALUES ('Информационные системы и технологии'),
       ('Программная инженерия'),
       ('Компьютерная безопасность'),
	   ('Геодезия'),
       ('Программируемые мобильные системи'),
       ('Вычислительные системы и сети'),
       ('Программное обеспечение информационных технологий');


INSERT INTO Discipline (DisciplineName, AwarageTime)
VALUES ('Базы данных', 90),
       ('Программирование для сети интернет', 120),
       ('Программирование в .Net', 60),
	   ('Системное программирование', 90),
       ('Объектно-ориентированные технологии программирования и стандарты проектирования', 120),
       ('Базы данных', 60),
       ('Термодинамика', 90);

INSERT INTO TypeWork (TypeWorkName)
VALUES ('Лекция'),
       ('Практическое занятие'),
       ('Лабораторная работа');

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
VALUES (1, 1, 'СТ-1', 25),
       (2, 2, 'ПИ-3', 30),
       (3, 3, 'КБ-1', 20),
	   (4, 4, 'ГЕО-1', 25),
       (5, 5, 'МС-1', 30),
       (6, 6, 'ВС-1', 20),

       (7, 7, 'ИТ-1', 25),
	   (2, 7, 'ИТ-2', 25);


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
     --INSERT INTO Person (FirstName, LastName, Patronymic) VALUES ('Новое имя', 'Новая фамилия', 'Новое отчество');
     --UPDATE Person SET FirstName = 'Измененное имя' WHERE PersonID = 1;
     --DELETE FROM Person WHERE PersonID = 1;


     --SELECT * FROM Person;
     --INSERT INTO Admin (FKPersonID, Login, Password) VALUES (4, 'newadmin', 'newpassword');
     --UPDATE Projects SET TypeOfProject = 'Измененный проект' WHERE ProjectsID = 1;
     --DELETE FROM EducationalMaterials WHERE EducationalMaterialsID = 2;
     --SELECT * FROM Events;
     --INSERT INTO Participants (FirstName, LastName, Patronymic) VALUES ('Новый', 'Участник', 'Иванович');
     --UPDATE Guests SET LastName = 'Измененная фамилия' WHERE GuestsID = 1;
     --DELETE FROM Teacher WHERE TeacherID = 2;
     --SELECT * FROM Specialization_Discipline;
     --INSERT INTO TimeManage (FKTeacherID, FKTypeWorkID, FKDisciplineID) VALUES (3, 2, 1);
     --UPDATE SupervisedGroup SET StudentsCount = 35 WHERE SupervisedGroupID = 1;
     --DELETE FROM Grade WHERE GradeID = 3;
     











Select * from SupervisedGroup
where StudentsCount >= 25
order by FKTeacherID




/* Все учителя с логинами, именами и должностями */
SELECT Person.FirstName, Person.LastName, Teacher.Login, Post.PostName AS Position
FROM Teacher
INNER JOIN Person ON Teacher.FKPersonID = Person.PersonID
INNER JOIN Post ON Teacher.FKPostID = Post.PostID;




/* группы - рейтинг*/
SELECT SupervisedGroup.GroupName, SupervisedGroup.StudentsCount, Grade.AverageRating, Discipline.DisciplineName
FROM SupervisedGroup
INNER JOIN Grade ON Grade.FKSupervisedGroupID = SupervisedGroup.SupervisedGroupID
INNER JOIN Discipline ON Discipline.DisciplineID = Grade.FKDisciplineID;

/*имена преподавателей, их логины и названия групп, которыми они руководят:*/
select Person.LastName, Person.FirstName, Teacher.Login, SupervisedGroup.GroupName
from Person
Inner join Teacher on Teacher.FKPersonID = Person.PersonID
inner join SupervisedGroup on SupervisedGroup.FKTeacherID = Teacher.TeacherID;


/* специализация и дисциплина*/
SELECT Specialization.SpecializationName, Discipline.DisciplineName
FROM Specialization_Discipline
INNER JOIN Specialization ON Specialization_Discipline.FKSpecializationID = Specialization.SpecializationID
INNER JOIN Discipline ON Specialization_Discipline.FKDisciplineID = Discipline.DisciplineID;


/* мероприятие - человек*/
SELECT Events.EventName, Participants.FirstName, Participants.LastName
FROM Events
INNER JOIN Events_Participants ON Events.EventsID = Events_Participants.FKEventsID
INNER JOIN Participants ON Events_Participants.FKParticipantsID = Participants.ParticipantsID;


/* проект - админ*/
SELECT Projects.TypeOfProject, Admin.Login
FROM Projects
INNER JOIN Admin ON Projects.FKAdminID = Admin.AdminID;


/* табличные скалярные смешанные */


/* ТАБЛИЧНАЯ ФУНКЦИЯ */

-- список всех участников мероприятия

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


/* Скалярная */

-- количество участников на мероприятии
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


/* СМЕШАННАЯ */

--учитель - логин, но с определенной дисциплиной


/*
('Базы данных', 90),
       ('Программирование для сети интернет', 120),
       ('Программирование в .Net', 60),
     ('Системное программирование', 90),
       ('Объектно-ориентированные технологии программирования и стандарты проектирования', 120),
       ('Базы данных', 60),
       ('Термодинамика', 90);*/

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

DECLARE @DisciplineName NVARCHAR(MAX) = 'Программирование для сети интернет'; 
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








/*------------------------------триггеры------------------------------------------------------*/



CREATE TRIGGER CreateTeacherAndAdminAccounts
ON Person
AFTER INSERT
AS
BEGIN
    DECLARE @PersonID INT
    SELECT @PersonID = PersonID FROM inserted

    -- Учетная запись преподавателя
    INSERT INTO Teacher (FKPersonID, Login, Password)
    VALUES (@PersonID, 'DefaultTeacherLogin', 'DefaultTeacherPassword')

    -- Учетная запись администратора (можно убрать)
    INSERT INTO Admin (FKPersonID, Login, Password)
    VALUES (@PersonID, 'DefaultAdminLogin', 'DefaultAdminPassword')
END


INSERT INTO Person (FirstName, LastName, Patronymic)
VALUES ('Александр', 'Александров', 'Александрович')







-- Создание триггера для управления статусом проектов
CREATE TRIGGER ManageProjectStatus
ON Projects
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @ProjectID INT;
    
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        -- Установка статуса "Активный" при вставке или обновлении проекта
        SELECT @ProjectID = ProjectsID FROM inserted;
        UPDATE Projects SET Status = 'Active' WHERE ProjectsID = @ProjectID;
    END
END;

UPDATE Projects
SET TypeOfProject = 'Исследовательский проект 3'
WHERE ProjectsID = 7;







/* регистрирует каждое изменение в таблице "Events" 
в журнале, указывая, кто и когда внес изменения.*/



-- Создание таблицы для журнала аудита
CREATE TABLE AuditLog (
    LogID INT IDENTITY(1, 1) PRIMARY KEY,
    TableName NVARCHAR(255),
    Action NVARCHAR(10),
    Username NVARCHAR(50),
    ActionDate DATETIME
);
ALTER TABLE AuditLog
ALTER COLUMN Action NVARCHAR(50);

-- Создание триггера для аудита
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

-- Пример обновления данных в таблице "Events", вызывающий триггер
UPDATE Events
SET EventName = 'Семинар по ИНФОРМАТИКЕ'
WHERE EventsID = 6;










-- Создание триггера
CREATE TRIGGER CheckGradeIntegrity
ON Grade
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted WHERE AverageRating > 10 OR AverageRating < 0)
    BEGIN
        THROW 51000, 'Оценка должна быть между 0 и 10.', 1;
    END
END;

-- Пример вставки данных, вызывающий триггер
INSERT INTO Grade (FKSupervisedGroupID, FKDisciplineID, FKTypeWorkID, AverageRating)
VALUES (1, 2, 3, 11); -- Это вызовет ошибку из-за оценки 11