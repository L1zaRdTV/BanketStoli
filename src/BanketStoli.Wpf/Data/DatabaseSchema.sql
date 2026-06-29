CREATE DATABASE BanketStoliDb;
GO
USE BanketStoliDb;
GO
CREATE TABLE UserRoles (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(30) NOT NULL UNIQUE);
CREATE TABLE Users (Id INT IDENTITY PRIMARY KEY, FullName NVARCHAR(120) NOT NULL, Phone NVARCHAR(30) NOT NULL, Email NVARCHAR(120) NOT NULL UNIQUE, Login NVARCHAR(50) NOT NULL UNIQUE, Password NVARCHAR(50) NOT NULL, RoleId INT NOT NULL REFERENCES UserRoles(Id));
CREATE TABLE DecorationStyles (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(80) NOT NULL UNIQUE);
CREATE TABLE BanquetRooms (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(80) NOT NULL, StyleId INT NOT NULL REFERENCES DecorationStyles(Id), TableCount INT NOT NULL CHECK (TableCount > 0), RentPricePerHour DECIMAL(10,2) NOT NULL CHECK (RentPricePerHour >= 0), ImagePath NVARCHAR(260) NULL, Description NVARCHAR(1000) NOT NULL);
CREATE TABLE BookingStatuses (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(80) NOT NULL UNIQUE);
CREATE TABLE Bookings (Id INT IDENTITY PRIMARY KEY, ClientId INT NOT NULL REFERENCES Users(Id), RoomId INT NOT NULL REFERENCES BanquetRooms(Id), StartAt DATETIME2 NOT NULL, EndAt DATETIME2 NOT NULL, StatusId INT NOT NULL REFERENCES BookingStatuses(Id), PrepaymentAmount DECIMAL(10,2) NULL, PrepaymentDate DATE NULL, CHECK (EndAt > StartAt));
GO
INSERT INTO UserRoles (Name) VALUES (N'Клиент'), (N'Менеджер');
INSERT INTO DecorationStyles (Name) VALUES (N'японский стиль'), (N'европейский стиль'), (N'мексиканский стиль');
INSERT INTO BookingStatuses (Name) VALUES (N'Полная предоплата'), (N'Частичная предоплата'), (N'Не оплачено');
INSERT INTO Users (FullName, Phone, Email, Login, Password, RoleId) VALUES
(N'Иванов Максим Иванович', N'8911-132-15-15', N'ivan@ya.ru', N'client1', N'client1', 1),
(N'Гулькина Ольга Петровна', N'8923-526-86-97', N'petrofon@mail.ru', N'client2', N'client2', 1),
(N'Васильев Иван Олегович', N'523-12-23', N'romashka@rambler.ru', N'client3', N'client3', 1),
(N'Кирилова Марина Артемовна', N'8923-987-65-32', N'marina@gmail.com', N'client4', N'client4', 1),
(N'Смирнов Игорь Андреевич', N'8921-963-32-65', N'smirnov@ya.ru', N'manager1', N'manager1', 2),
(N'Петрова Олеся Юрьевна', N'8911-125-45-65', N'olesya@mail.ru', N'manager2', N'manager2', 2);
INSERT INTO BanquetRooms (Name, StyleId, TableCount, RentPricePerHour, ImagePath, Description) VALUES
(N'Красный зал', 1, 20, 500, N'redroom.jpg', N'Уютный зал в японском стиле с лаконичным интерьером и спокойной атмосферой, идеально подходящий для семейных праздников и деловых встреч.'),
(N'Зелёный зал', 2, 32, 1000, N'greenroom.jpg', N'Просторный зал в классическом европейском стиле, отлично подходящий для свадеб, корпоративных мероприятий и крупных торжеств.'),
(N'Жёлтый зал', 3, 20, 750, N'yellowroom.jpg', N'Яркий зал в мексиканском стиле с тёплой атмосферой и оригинальным оформлением, идеально подходящий для весёлых праздников и тематических мероприятий.');
INSERT INTO Bookings (ClientId, RoomId, StartAt, EndAt, StatusId, PrepaymentAmount, PrepaymentDate) VALUES
(1, 1, '2026-01-23T13:00:00', '2026-01-23T20:00:00', 1, 3500, '2026-01-21'),
(2, 2, '2026-02-26T09:00:00', '2026-02-26T18:00:00', 2, 5000, '2026-02-24'),
(3, 2, '2026-03-03T10:00:00', '2026-03-03T19:00:00', 3, NULL, NULL),
(4, 3, '2026-03-04T11:00:00', '2026-03-04T18:00:00', 3, NULL, NULL);
