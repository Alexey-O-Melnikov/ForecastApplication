USE ForecastApplication
GO

--Crate table Users
CREATE TABLE Users (
	UserId int IDENTITY(1,1),
	Login varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	RegistarionDate datetime NOT NULL
)
GO

ALTER TABLE Users ADD  CONSTRAINT DF_Users_RegistarionDate  DEFAULT (getdate()) FOR RegistarionDate
GO

ALTER TABLE Users ADD CONSTRAINT PK_Users_UserId PRIMARY KEY CLUSTERED (UserId)
GO

ALTER TABLE Users ADD CONSTRAINT DF_Users_Login_Unique UNIQUE (Login)
GO

ALTER TABLE Users ADD CONSTRAINT DF_Users_Email_Unique UNIQUE (Email)
GO

INSERT INTO Users (Login, Password, Email)
VALUES ('Melnikov', 'pass12345', 'alex@mel.ru')

--Create table Favorites
CREATE TABLE Favorites (
	UserId int NOT NULL,
	CityId int NOT NULL
)
GO

ALTER TABLE Favorites ADD CONSTRAINT PK_Favorites_UserId_CityId PRIMARY KEY (UserId, CityId)
GO

ALTER TABLE Favorites ADD CONSTRAINT FK_Favorites_User FOREIGN KEY (UserId) REFERENCES Users (UserId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

INSERT INTO Favorites (UserId, CityId)
VALUES (1, 2643743), (1, 535121)