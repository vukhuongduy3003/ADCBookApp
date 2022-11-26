CREATE TABLE Company (
	idCompany INT PRIMARY KEY IDENTITY,
	nameCompany NVARCHAR(100) NOT NULL,
	addressCompany NVARCHAR(255) NOT NULL,
	phoneNumber NVARCHAR(10) NOT NULL
);

SELECT *
FROM Company
ORDER BY Company.nameCompany ASC;

SELECT *
FROM Company
WHERE Company.idCompany = 85;

CREATE TABLE Author (
	idAuthor INT PRIMARY KEY IDENTITY,
	nameAuthor NVARCHAR(100) NOT NULL,
	birthYear INT NOT NULL,
	homeTown NVARCHAR(100) NOT NULL
);

CREATE TABLE [Type] (
	idType INT PRIMARY KEY IDENTITY,
	nameType NVARCHAR(100) NOT NULL,
);

CREATE TABLE Book (
	idBook INT PRIMARY KEY IDENTITY,
	nameBook NVARCHAR(100) NOT NULL,
	idCompany INT NOT NULL,
	FOREIGN KEY(idCompany) REFERENCES Company(idCompany),
	idAuthor INT NOT NULL,
	FOREIGN KEY(idAuthor) REFERENCES Author(idAuthor),
	idType INT NOT NULL,
	FOREIGN KEY(idType) REFERENCES [Type](idType),
	amount INT NOT NULL,
	price INT NOT NULL,
);