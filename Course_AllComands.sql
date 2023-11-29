



---- 

--Процедуры курсач


--CREATE PROCEDURE AddNewTeacher
--    @FirstName NVARCHAR(50),
--    @LastName NVARCHAR(50),
--    @Patronymic NVARCHAR(50),
--    @Login NVARCHAR(50),
--    @Password NVARCHAR(50),
--    @PostName NVARCHAR(50)
--AS
--BEGIN
--    DECLARE @PersonID INT, @PostID INT;

--    INSERT INTO Person (FirstName, LastName, Patronymic)
--    VALUES (@FirstName, @LastName, @Patronymic);

--    SET @PersonID = SCOPE_IDENTITY();

--    SELECT @PostID = PostID FROM Post WHERE PostName = @PostName;

--    INSERT INTO Teacher (FKPostID, FKPersonID, Login, Password)
--    VALUES (@PostID, @PersonID, @Login, @Password);
--END
drop procedure AddNewTeacher;

CREATE PROCEDURE AddNewTeacher
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Patronymic NVARCHAR(50),
    @Login NVARCHAR(50),
    @Password NVARCHAR(50),
    @PostID INT
AS
BEGIN
    DECLARE @PersonID INT;

    INSERT INTO Person (FirstName, LastName, Patronymic)
    VALUES (@FirstName, @LastName, @Patronymic);

    SET @PersonID = SCOPE_IDENTITY();

    INSERT INTO Teacher (FKPostID, FKPersonID, Login, Password)
    VALUES (@PostID, @PersonID, @Login, @Password);
END


CREATE PROCEDURE EditTeacher
    @TeacherID INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Patronymic NVARCHAR(50),
    @Login NVARCHAR(50),
    @Password NVARCHAR(50),
    @PostID INT
AS
BEGIN
    UPDATE Teacher
    SET Login = @Login,
        Password = @Password,
        FKPostID = @PostID
    WHERE TeacherID = @TeacherID;

    UPDATE Person
    SET FirstName = @FirstName,
        LastName = @LastName,
        Patronymic = @Patronymic
    WHERE PersonID = (SELECT FKPersonID FROM Teacher WHERE TeacherID = @TeacherID);
END


drop procedure DeleteTeacher;

--CREATE PROCEDURE DeleteTeacher
--    @TeacherID INT
--AS
--BEGIN
--    DECLARE @PersonID INT, @PostID INT;

--    SELECT @PersonID = FKPersonID, @PostID = FKPostID FROM Teacher WHERE TeacherID = @TeacherID;

--    DELETE FROM Teacher WHERE TeacherID = @TeacherID;
--    DELETE FROM Person WHERE PersonID = @PersonID;
--    DELETE FROM Post WHERE PostID = @PostID;
--END
CREATE PROCEDURE DeleteTeacher
    @TeacherID INT
AS
BEGIN
    DECLARE @PersonID INT;

    SELECT @PersonID = FKPersonID FROM Teacher WHERE TeacherID = @TeacherID;

    DELETE FROM Teacher WHERE TeacherID = @TeacherID;
    DELETE FROM Person WHERE PersonID = @PersonID;
END





drop PROCEDURE GetTeacherDisciplines;


CREATE PROCEDURE GetTeacherDisciplines @TeacherID INT AS
BEGIN
    SELECT 
        TWSD.TypeWork_Specialization_DisciplineID,
        D.DisciplineName, 
        TW.TypeWorkName, 
        S.SpecializationName, 
        TM.AwarageTime, 
        SG.GroupName,
        TM.FKGroupID AS GroupID  -- Добавлен этот столбец
    FROM TimeManage TM
    INNER JOIN Teacher T ON TM.FKTeacherID = T.TeacherID
    INNER JOIN TypeWork_Specialization_Discipline TWSD ON TM.FKTypeWork_Specialization_DisciplineID = TWSD.TypeWork_Specialization_DisciplineID
    INNER JOIN TypeWork TW ON TWSD.FKTypeWorkID = TW.TypeWorkID
    INNER JOIN Specialization_Discipline SD ON TWSD.FKSpecialization_DisciplineID = SD.Specialization_DisciplineID
    INNER JOIN Discipline D ON SD.FKDisciplineID = D.DisciplineID
    INNER JOIN Specialization S ON SD.FKSpecializationID = S.SpecializationID
    INNER JOIN SupervisedGroup SG ON TM.FKGroupID = SG.SupervisedGroupID
    WHERE T.TeacherID = @TeacherID
END



EXEC GetTeacherDisciplines @TeacherID = 1


CREATE PROCEDURE AssignTeacherToDiscipline
    @TeacherID INT,
    @TypeWork_Specialization_DisciplineID INT,
    @AwarageTime INT,
    @GroupID INT
AS
BEGIN
    INSERT INTO TimeManage (FKTeacherID, FKTypeWork_Specialization_DisciplineID, AwarageTime, FKGroupID)
    VALUES (@TeacherID, @TypeWork_Specialization_DisciplineID, @AwarageTime, @GroupID);
END


EXEC AssignTeacherToDiscipline @TeacherID = 1, @TypeWork_Specialization_DisciplineID = 3, @AwarageTime = 60, @GroupID = 2;


drop procedure GetFullInfoForTypeWork_Specialization_Discipline

CREATE PROCEDURE GetFullInfoForTypeWork_Specialization_Discipline
AS
BEGIN
    SELECT 
        TWSD.TypeWork_Specialization_DisciplineID, 
        TW.TypeWorkName, 
        D.DisciplineName, 
        S.SpecializationName,
        CONCAT(S.SpecializationName, ', ',TW.TypeWorkName , ', ', D.DisciplineName ) AS FullInfo     
    FROM TypeWork_Specialization_Discipline TWSD
    INNER JOIN TypeWork TW ON TWSD.FKTypeWorkID = TW.TypeWorkID
    INNER JOIN Specialization_Discipline SD ON TWSD.FKSpecialization_DisciplineID = SD.Specialization_DisciplineID
    INNER JOIN Discipline D ON SD.FKDisciplineID = D.DisciplineID
    INNER JOIN Specialization S ON SD.FKSpecializationID = S.SpecializationID
END



CREATE PROCEDURE RemoveTeacherAssignment
    @TeacherID INT,
    @TypeWork_Specialization_DisciplineID INT,
    @GroupID INT
AS
BEGIN
    DELETE FROM TimeManage
    WHERE FKTeacherID = @TeacherID AND FKTypeWork_Specialization_DisciplineID = @TypeWork_Specialization_DisciplineID AND FKGroupID = @GroupID;
END


----- Кураторство

drop PROCEDURE GetGroupsByTeacher;


CREATE PROCEDURE GetGroupsByTeacher
    @TeacherID INT
AS
BEGIN
    SELECT SG.SupervisedGroupID, SG.GroupName, SG.StudentsCount
    FROM SupervisedGroup SG
    WHERE SG.FKTeacherID = @TeacherID;
END;



EXEC GetGroupsByTeacher @TeacherID = 1;

CREATE PROCEDURE AssignGroupToTeacher
    @TeacherID INT,
    @SupervisedGroupID INT
AS
BEGIN
    UPDATE SupervisedGroup
    SET FKTeacherID = @TeacherID
    WHERE SupervisedGroupID = @SupervisedGroupID;
END;


CREATE PROCEDURE RemoveGroupFromTeacher
    @SupervisedGroupID INT
AS
BEGIN
    UPDATE SupervisedGroup
    SET FKTeacherID = NULL
    WHERE SupervisedGroupID = @SupervisedGroupID;
END;


----------------------------------
-- Специальности/дисциплины и т.д

-- Функция для создания записи
CREATE PROCEDURE CreateSpecialization
    @SpecializationName NVARCHAR(50)
AS
BEGIN
    INSERT INTO Specialization(SpecializationName)
    VALUES (@SpecializationName)
END
GO

-- Функция для удаления записи
CREATE PROCEDURE DeleteSpecialization
    @SpecializationID INT
AS
BEGIN
    DELETE FROM Specialization
    WHERE SpecializationID = @SpecializationID
END
GO

-- Функция для редактирования записи
CREATE PROCEDURE UpdateSpecialization
    @SpecializationID INT,
    @SpecializationName NVARCHAR(50)
AS
BEGIN
    UPDATE Specialization
    SET SpecializationName = @SpecializationName
    WHERE SpecializationID = @SpecializationID
END
GO



---------------------------Дисциплины

-- Функция для создания записи
CREATE PROCEDURE CreateDiscipline
    @DisciplineName NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Discipline(DisciplineName)
    VALUES (@DisciplineName)
END
GO

-- Функция для удаления записи
CREATE PROCEDURE DeleteDiscipline
    @DisciplineID INT
AS
BEGIN
    DELETE FROM Discipline
    WHERE DisciplineID = @DisciplineID
END
GO

-- Функция для редактирования записи
CREATE PROCEDURE UpdateDiscipline
    @DisciplineID INT,
    @DisciplineName NVARCHAR(MAX)
AS
BEGIN
    UPDATE Discipline
    SET DisciplineName = @DisciplineName
    WHERE DisciplineID = @DisciplineID
END
GO




-- позволяет закрепить специальность за дисциплиной и тип работы за специализацией и дисциплиной в одном шаге
CREATE PROCEDURE AssignWorkToSpecializationDiscipline
    @FKSpecializationID INT,
    @FKDisciplineID INT,
    @FKTypeWorkID INT
AS
BEGIN
    DECLARE @Specialization_DisciplineID INT

    -- Создаем запись в таблице Specialization_Discipline
    INSERT INTO Specialization_Discipline(FKSpecializationID, FKDisciplineID)
    VALUES (@FKSpecializationID, @FKDisciplineID)

    -- Получаем ID только что созданной записи
    SET @Specialization_DisciplineID = SCOPE_IDENTITY()

    -- Создаем запись в таблице TypeWork_Specialization_Discipline
    INSERT INTO TypeWork_Specialization_Discipline(FKTypeWorkID, FKSpecialization_DisciplineID)
    VALUES (@FKTypeWorkID, @Specialization_DisciplineID)
END
GO

EXEC AssignWorkToSpecializationDiscipline @FKSpecializationID = 1, @FKDisciplineID = 1, @FKTypeWorkID = 1


drop procedure GetDisciplinesAndTypeWorksForSpecialization

--CREATE PROCEDURE GetDisciplinesAndTypeWorksForSpecialization
--    @FKSpecializationID INT
--AS
--BEGIN
--    SELECT 
--        D.DisciplineName, 
--        TW.TypeWorkName
--    FROM 
--        Specialization_Discipline SD 
--        JOIN Discipline D ON SD.FKDisciplineID = D.DisciplineID
--        JOIN TypeWork_Specialization_Discipline TWSD ON SD.Specialization_DisciplineID = TWSD.FKSpecialization_DisciplineID
--        JOIN TypeWork TW ON TWSD.FKTypeWorkID = TW.TypeWorkID
--    WHERE 
--        SD.FKSpecializationID = @FKSpecializationID
--END
--GO

--CREATE PROCEDURE GetDisciplinesAndTypeWorksForSpecialization
--    @FKSpecializationID INT
--AS
--BEGIN
--    SELECT 
--        SD.FKSpecializationID,
--        D.DisciplineID,
--        TW.TypeWorkID,
--        D.DisciplineName, 
--        TW.TypeWorkName
--    FROM 
--        Specialization_Discipline SD 
--        JOIN Discipline D ON SD.FKDisciplineID = D.DisciplineID
--        JOIN TypeWork_Specialization_Discipline TWSD ON SD.Specialization_DisciplineID = TWSD.FKSpecialization_DisciplineID
--        JOIN TypeWork TW ON TWSD.FKTypeWorkID = TW.TypeWorkID
--    WHERE 
--        SD.FKSpecializationID = @FKSpecializationID
--END
--GO

CREATE PROCEDURE GetDisciplinesAndTypeWorksForSpecialization
    @FKSpecializationID INT
AS
BEGIN
    SELECT 
        SD.FKSpecializationID,
        SD.FKDisciplineID, 
        TWSD.FKTypeWorkID,
        D.DisciplineName, 
        TW.TypeWorkName
    FROM 
        Specialization_Discipline SD 
        JOIN Discipline D ON SD.FKDisciplineID = D.DisciplineID
        JOIN TypeWork_Specialization_Discipline TWSD ON SD.Specialization_DisciplineID = TWSD.FKSpecialization_DisciplineID
        JOIN TypeWork TW ON TWSD.FKTypeWorkID = TW.TypeWorkID
    WHERE 
        SD.FKSpecializationID = @FKSpecializationID
END
GO



EXEC GetDisciplinesAndTypeWorksForSpecialization @FKSpecializationID = 3


CREATE PROCEDURE UnassignWorkFromSpecializationDiscipline
    @FKSpecializationID INT,
    @FKDisciplineID INT,
    @FKTypeWorkID INT
AS
BEGIN
    -- Находим ID записи в таблице Specialization_Discipline
    DECLARE @Specialization_DisciplineID INT
    SELECT @Specialization_DisciplineID = Specialization_DisciplineID 
    FROM Specialization_Discipline 
    WHERE FKSpecializationID = @FKSpecializationID AND FKDisciplineID = @FKDisciplineID

    -- Удаляем запись из таблицы TypeWork_Specialization_Discipline
    DELETE FROM TypeWork_Specialization_Discipline 
    WHERE FKTypeWorkID = @FKTypeWorkID AND FKSpecialization_DisciplineID = @Specialization_DisciplineID

    -- Удаляем запись из таблицы Specialization_Discipline
    DELETE FROM Specialization_Discipline 
    WHERE Specialization_DisciplineID = @Specialization_DisciplineID
END
GO


---- Группы

drop procedure GetGroupInfo

CREATE PROCEDURE GetGroupInfo
    @GroupID INT
AS
BEGIN
    SELECT 
        g.SupervisedGroupID,
        g.GroupName, 
        g.StudentsCount, 
        g.FKTeacherID,
        t.Login AS TeacherLogin, 
        p.FirstName,
        p.LastName,
        p.Patronymic,
        g.FKSpecializationID,
        s.SpecializationName
    FROM 
        SupervisedGroup g
    INNER JOIN 
        Teacher t ON g.FKTeacherID = t.TeacherID
    INNER JOIN 
        Person p ON t.FKPersonID = p.PersonID
    INNER JOIN 
        Specialization s ON g.FKSpecializationID = s.SpecializationID
    WHERE 
        g.SupervisedGroupID = @GroupID
END

EXEC GetGroupInfo @GroupID = 1









CREATE PROCEDURE CreateNewGroup
    @TeacherID INT,
    @SpecializationID INT,
    @GroupName NVARCHAR(50),
    @StudentsCount INT
AS
BEGIN
    INSERT INTO SupervisedGroup (FKTeacherID, FKSpecializationID, GroupName, StudentsCount)
    VALUES (@TeacherID, @SpecializationID, @GroupName, @StudentsCount)
END


EXEC CreateNewGroup @TeacherID = 1, @SpecializationID = 1, @GroupName = 'Новая группа', @StudentsCount = 20


CREATE PROCEDURE EditGroup
    @GroupID INT,
    @TeacherID INT = NULL,
    @SpecializationID INT = NULL,
    @GroupName NVARCHAR(50) = NULL,
    @StudentsCount INT = NULL
AS
BEGIN
    UPDATE SupervisedGroup
    SET FKTeacherID = ISNULL(@TeacherID, FKTeacherID),
        FKSpecializationID = ISNULL(@SpecializationID, FKSpecializationID),
        GroupName = ISNULL(@GroupName, GroupName),
        StudentsCount = ISNULL(@StudentsCount, StudentsCount)
    WHERE SupervisedGroupID = @GroupID
END


EXEC EditGroup @GroupID = 1, @TeacherID = 2, @SpecializationID = 3, @GroupName = 'Новое имя группы', @StudentsCount = 25


CREATE PROCEDURE DeleteGroup
    @GroupID INT
AS
BEGIN
    DELETE FROM SupervisedGroup
    WHERE SupervisedGroupID = @GroupID
END

EXEC DeleteGroup @GroupID = 1





----------------АДМИНЫ

-- Вывод информации об админах
CREATE PROCEDURE GetAdmins
AS
BEGIN
    SELECT A.AdminID, A.Login, A.Password, P.FirstName, P.LastName, P.Patronymic
    FROM Admin A
    INNER JOIN Person P ON A.FKPersonID = P.PersonID;
END
GO


-- Создание админа
CREATE PROCEDURE CreateAdmin
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Patronymic NVARCHAR(50),
    @Login NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    DECLARE @PersonID INT;

    INSERT INTO Person(FirstName, LastName, Patronymic)
    VALUES (@FirstName, @LastName, @Patronymic);

    SET @PersonID = SCOPE_IDENTITY();

    INSERT INTO Admin(FKPersonID, Login, Password)
    VALUES (@PersonID, @Login, @Password);
END
GO

-- Редактирование админа
CREATE PROCEDURE EditAdmin
    @AdminID INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Patronymic NVARCHAR(50),
    @Login NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    DECLARE @PersonID INT;

    SELECT @PersonID = FKPersonID FROM Admin WHERE AdminID = @AdminID;

    UPDATE Person
    SET FirstName = @FirstName, LastName = @LastName, Patronymic = @Patronymic
    WHERE PersonID = @PersonID;

    UPDATE Admin
    SET Login = @Login, Password = @Password
    WHERE AdminID = @AdminID;
END
GO

-- Удаление админа
CREATE PROCEDURE DeleteAdmin
    @AdminID INT
AS
BEGIN
    DELETE FROM Admin
    WHERE AdminID = @AdminID;
END
GO

-- Триггер на дефолтные значения
CREATE TRIGGER DefaultValues
ON Person
AFTER INSERT
AS
BEGIN
    UPDATE Person
    SET FirstName = ISNULL(FirstName, 'Default First Name'),
        LastName = ISNULL(LastName, 'Default Last Name'),
        Patronymic = ISNULL(Patronymic, 'Default Patronymic')
    WHERE PersonID IN (SELECT PersonID FROM inserted);
END
GO


-- Создание админа
EXEC CreateAdmin @FirstName = 'Иван', @LastName = 'Иванов', @Patronymic = 'Иванович', @Login = 'ivanov', @Password = 'password123';



-- Редактирование админа
EXEC EditAdmin @AdminID = 1, @FirstName = 'Петр', @LastName = 'Петров', @Patronymic = 'Петрович', @Login = 'petrov', @Password = 'password456';

-- Удаление админа
EXEC DeleteAdmin @AdminID = 12;

-- Вывод информации об админах
EXEC GetAdmins;



------------------------------------------------------------------------------------------------------------------------------------------------------------



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

/*---------Курсач */


CREATE PROCEDURE GetTeacherInfo
AS
BEGIN
    SELECT 
        Person.PersonID,
        Person.FirstName,
        Person.LastName,
        Person.Patronymic,
        Teacher.TeacherID,
        Teacher.Login,
        Teacher.Password,
        Post.PostID,
        Post.PostName
    FROM 
        Teacher
    INNER JOIN 
        Person ON Teacher.FKPersonID = Person.PersonID
    INNER JOIN 
        Post ON Teacher.FKPostID = Post.PostID
END

EXEC GetTeacherInfo














---------------- Какое-то обязательное Г для курсача
-- принимает год в качестве параметра и возвращает все обучающие материалы, выпущенные после этого года.
CREATE PROCEDURE GetMaterialsAfterYear @Year INT
AS
BEGIN
    SELECT * FROM EducationalMaterials
    WHERE YEAR(PublicationYear) > @Year
END

EXEC GetMaterialsAfterYear @Year = 2020


drop procedure GetGradesByGroupID

--CREATE PROCEDURE GetGradesByGroupID
--    @GroupID INT
--AS
--BEGIN
--    SELECT 
--        g.GradeID,
--        g.FKDisciplineID,
--        d.DisciplineName, 
--        g.FKTypeWorkID,
--        tw.TypeWorkName, 
--        g.AverageRating
--    FROM 
--        Grade g
--    JOIN 
--        Discipline d ON g.FKDisciplineID = d.DisciplineID
--    JOIN 
--        TypeWork tw ON g.FKTypeWorkID = tw.TypeWorkID
--    WHERE 
--        g.FKSupervisedGroupID = @GroupID
--END

CREATE PROCEDURE GetGradesByGroupID
    @GroupID INT
AS
BEGIN
    SELECT 
        g.GradeID,
        g.FKSupervisedGroupID,
        g.FKDisciplineID,
        d.DisciplineName, 
        g.FKTypeWorkID,
        tw.TypeWorkName, 
        g.AverageRating
    FROM 
        Grade g
    JOIN 
        Discipline d ON g.FKDisciplineID = d.DisciplineID
    JOIN 
        TypeWork tw ON g.FKTypeWorkID = tw.TypeWorkID
    WHERE 
        g.FKSupervisedGroupID = @GroupID
END




EXEC GetGradesByGroupID @GroupID = 1


CREATE PROCEDURE UpdateGradeByGroupID
    @GroupID INT,
    @GradeID INT,
    @NewDisciplineID INT,
    @NewTypeWorkID INT,
    @NewRating FLOAT
AS
BEGIN
    UPDATE Grade
    SET FKDisciplineID = @NewDisciplineID, FKTypeWorkID = @NewTypeWorkID, AverageRating = @NewRating
    WHERE FKSupervisedGroupID = @GroupID AND GradeID = @GradeID
END
