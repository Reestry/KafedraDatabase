



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


CREATE PROCEDURE GetFullInfoForTypeWork_Specialization_Discipline
AS
BEGIN
    SELECT 
        TWSD.TypeWork_Specialization_DisciplineID, 
        TW.TypeWorkName, 
        D.DisciplineName, 
        S.SpecializationName,
        CONCAT(TW.TypeWorkName, ', ', D.DisciplineName, ', ', S.SpecializationName) AS FullInfo
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
