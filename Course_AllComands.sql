



---- 

--Процедуры курсач


CREATE PROCEDURE AddNewTeacher
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Patronymic NVARCHAR(50),
    @Login NVARCHAR(50),
    @Password NVARCHAR(50),
    @PostName NVARCHAR(50)
AS
BEGIN
    DECLARE @PersonID INT, @PostID INT;

    INSERT INTO Person (FirstName, LastName, Patronymic)
    VALUES (@FirstName, @LastName, @Patronymic);

    SET @PersonID = SCOPE_IDENTITY();

    SELECT @PostID = PostID FROM Post WHERE PostName = @PostName;

    INSERT INTO Teacher (FKPostID, FKPersonID, Login, Password)
    VALUES (@PostID, @PersonID, @Login, @Password);
END
