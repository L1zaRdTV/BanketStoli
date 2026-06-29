IF DB_ID(N'BanketStoliDb') IS NULL
    CREATE DATABASE BanketStoliDb;
GO
USE BanketStoliDb;
GO

IF OBJECT_ID(N'dbo.Bookings', N'U') IS NOT NULL DROP TABLE dbo.Bookings;
IF OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID(N'dbo.BanquetRooms', N'U') IS NOT NULL DROP TABLE dbo.BanquetRooms;
IF OBJECT_ID(N'dbo.Clients', N'U') IS NOT NULL DROP TABLE dbo.Clients;
IF OBJECT_ID(N'dbo.Managers', N'U') IS NOT NULL DROP TABLE dbo.Managers;
IF OBJECT_ID(N'dbo.PaymentStatuses', N'U') IS NOT NULL DROP TABLE dbo.PaymentStatuses;
IF OBJECT_ID(N'dbo.DecorationStyles', N'U') IS NOT NULL DROP TABLE dbo.DecorationStyles;
IF OBJECT_ID(N'dbo.ClientTypes', N'U') IS NOT NULL DROP TABLE dbo.ClientTypes;
IF OBJECT_ID(N'dbo.UserRoles', N'U') IS NOT NULL DROP TABLE dbo.UserRoles;
GO

CREATE TABLE dbo.UserRoles
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_UserRoles PRIMARY KEY,
    Name NVARCHAR(30) NOT NULL CONSTRAINT UQ_UserRoles_Name UNIQUE
);

CREATE TABLE dbo.ClientTypes
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_ClientTypes PRIMARY KEY,
    Name NVARCHAR(60) NOT NULL CONSTRAINT UQ_ClientTypes_Name UNIQUE
);

CREATE TABLE dbo.DecorationStyles
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_DecorationStyles PRIMARY KEY,
    Name NVARCHAR(80) NOT NULL CONSTRAINT UQ_DecorationStyles_Name UNIQUE
);

CREATE TABLE dbo.PaymentStatuses
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_PaymentStatuses PRIMARY KEY,
    Name NVARCHAR(80) NOT NULL CONSTRAINT UQ_PaymentStatuses_Name UNIQUE
);

CREATE TABLE dbo.Clients
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_Clients PRIMARY KEY,
    FullName NVARCHAR(120) NOT NULL,
    Phone NVARCHAR(30) NOT NULL,
    ClientTypeId INT NOT NULL CONSTRAINT FK_Clients_ClientTypes REFERENCES dbo.ClientTypes(Id),
    OrganizationName NVARCHAR(120) NULL,
    Email NVARCHAR(120) NOT NULL CONSTRAINT UQ_Clients_Email UNIQUE,
    CONSTRAINT CK_Clients_OrganizationName CHECK
    (
        (ClientTypeId = 1 AND OrganizationName IS NULL) OR
        (ClientTypeId <> 1 AND OrganizationName IS NOT NULL)
    )
);

CREATE TABLE dbo.Managers
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_Managers PRIMARY KEY,
    FullName NVARCHAR(120) NOT NULL,
    Phone NVARCHAR(30) NOT NULL,
    Email NVARCHAR(120) NOT NULL CONSTRAINT UQ_Managers_Email UNIQUE
);

CREATE TABLE dbo.BanquetRooms
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_BanquetRooms PRIMARY KEY,
    Name NVARCHAR(80) NOT NULL CONSTRAINT UQ_BanquetRooms_Name UNIQUE,
    StyleId INT NOT NULL CONSTRAINT FK_BanquetRooms_DecorationStyles REFERENCES dbo.DecorationStyles(Id),
    TableCount INT NOT NULL CONSTRAINT CK_BanquetRooms_TableCount CHECK (TableCount > 0),
    RentPricePerHour DECIMAL(10,2) NOT NULL CONSTRAINT CK_BanquetRooms_RentPrice CHECK (RentPricePerHour >= 0),
    ImagePath NVARCHAR(260) NULL,
    Description NVARCHAR(1000) NOT NULL
);

CREATE TABLE dbo.Users
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_Users PRIMARY KEY,
    Login NVARCHAR(50) NOT NULL CONSTRAINT UQ_Users_Login UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    RoleId INT NOT NULL CONSTRAINT FK_Users_UserRoles REFERENCES dbo.UserRoles(Id),
    ClientId INT NULL CONSTRAINT FK_Users_Clients REFERENCES dbo.Clients(Id),
    ManagerId INT NULL CONSTRAINT FK_Users_Managers REFERENCES dbo.Managers(Id),
    CONSTRAINT CK_Users_ProfileByRole CHECK
    (
        (RoleId = 1 AND ClientId IS NOT NULL AND ManagerId IS NULL) OR
        (RoleId = 2 AND ClientId IS NULL AND ManagerId IS NOT NULL)
    )
);

CREATE TABLE dbo.Bookings
(
    Id INT IDENTITY(1,1) CONSTRAINT PK_Bookings PRIMARY KEY,
    ClientId INT NOT NULL CONSTRAINT FK_Bookings_Clients REFERENCES dbo.Clients(Id),
    RoomId INT NOT NULL CONSTRAINT FK_Bookings_BanquetRooms REFERENCES dbo.BanquetRooms(Id),
    ManagerId INT NULL CONSTRAINT FK_Bookings_Managers REFERENCES dbo.Managers(Id),
    StartAt DATETIME2(0) NOT NULL,
    EndAt DATETIME2(0) NOT NULL,
    PaymentStatusId INT NOT NULL CONSTRAINT FK_Bookings_PaymentStatuses REFERENCES dbo.PaymentStatuses(Id),
    PrepaymentAmount DECIMAL(10,2) NOT NULL CONSTRAINT DF_Bookings_PrepaymentAmount DEFAULT (0),
    PaymentStatusDate DATE NULL,
    CONSTRAINT CK_Bookings_Period CHECK (EndAt > StartAt),
    CONSTRAINT CK_Bookings_Prepayment CHECK (PrepaymentAmount >= 0),
    CONSTRAINT UQ_Bookings_Room_Period UNIQUE (RoomId, StartAt, EndAt)
);
GO

INSERT INTO dbo.UserRoles (Name) VALUES (N'Клиент'), (N'Менеджер');
INSERT INTO dbo.ClientTypes (Name) VALUES (N'Физическое лицо'), (N'Организация');
INSERT INTO dbo.DecorationStyles (Name) VALUES (N'японский стиль'), (N'европейский стиль'), (N'мексиканский стиль');
INSERT INTO dbo.PaymentStatuses (Name) VALUES (N'Полная предоплата'), (N'Частичная предоплата'), (N'Не оплачено');

INSERT INTO dbo.Clients (FullName, Phone, ClientTypeId, OrganizationName, Email) VALUES
(N'Иванов Максим Иванович', N'8911-132-15-15', 1, NULL, N'ivan@ya.ru'),
(N'Гулькина Ольга Петровна', N'8923-526-86-97', 1, NULL, N'petrofon@mail.ru'),
(N'Васильев Иван Олегович', N'523-12-23', 2, N'ООО "Ромашка"', N'romashka@rambler.ru'),
(N'Кирилова Марина Артемовна', N'8923-987-65-32', 1, NULL, N'marina@gmail.com');

INSERT INTO dbo.Managers (FullName, Phone, Email) VALUES
(N'Смирнов Игорь Андреевич', N'8921-963-32-65', N'smirnov@ya.ru'),
(N'Петрова Олеся Юрьевна', N'8911-125-45-65', N'olesya@mail.ru');

INSERT INTO dbo.BanquetRooms (Name, StyleId, TableCount, RentPricePerHour, ImagePath, Description) VALUES
(N'Красный зал', 1, 20, 500, N'redroom.jpg', N'Уютный зал в японском стиле с лаконичным интерьером и спокойной атмосферой, идеально подходящий для семейных праздников и деловых встреч.'),
(N'Зелёный зал', 2, 32, 1000, N'greenroom.jpg', N'Просторный зал в классическом европейском стиле, отлично подходящий для свадеб, корпоративных мероприятий и крупных торжеств.'),
(N'Жёлтый зал', 3, 20, 750, N'yellowroom.jpg', N'Яркий зал в мексиканском стиле с тёплой атмосферой и оригинальным оформлением, идеально подходящий для весёлых праздников и тематических мероприятий.');

INSERT INTO dbo.Users (Login, Password, RoleId, ClientId, ManagerId) VALUES
(N'client1', N'client1', 1, 1, NULL),
(N'client2', N'client2', 1, 2, NULL),
(N'client3', N'client3', 1, 3, NULL),
(N'client4', N'client4', 1, 4, NULL),
(N'manager1', N'manager1', 2, NULL, 1),
(N'manager2', N'manager2', 2, NULL, 2);

INSERT INTO dbo.Bookings (ClientId, RoomId, ManagerId, StartAt, EndAt, PaymentStatusId, PrepaymentAmount, PaymentStatusDate) VALUES
(1, 1, 1, '2026-01-23T13:00:00', '2026-01-23T20:00:00', 1, 3500, '2026-01-21'),
(2, 2, 2, '2026-02-26T09:00:00', '2026-02-26T18:00:00', 2, 5000, '2026-02-24'),
(3, 2, NULL, '2026-03-03T10:00:00', '2026-03-03T19:00:00', 3, 0, NULL),
(4, 3, NULL, '2026-03-04T11:00:00', '2026-03-04T18:00:00', 3, 0, NULL);
GO
