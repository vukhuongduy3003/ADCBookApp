CREATE DATABASE DataADCBook;
USE DataADCBook;
CREATE TABLE Company (
	idCompany INT PRIMARY KEY IDENTITY,
	nameCompany NVARCHAR(100) NOT NULL,
	addressCompany NVARCHAR(255) NOT NULL,
	phoneNumber NVARCHAR(10) NOT NULL
);

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

CREATE TABLE ExchangeBook (
	idExchangeBook INT PRIMARY KEY IDENTITY,
	idBook INT NOT NULL,
	nameBook NVARCHAR(100) NOT NULL,
	number INT NOT NULL,
	reason NVARCHAR(100) NOT NULL,
	[status] NVARCHAR(100) NOT NULL,
	startDay DATETIME NOT NULL,
	endDay DATETIME
);

CREATE TABLE Discount (
	idDiscount INT PRIMARY KEY IDENTITY,
	nameDiscount NVARCHAR(100) NOT NULL,
	StartDiscountDate DATE NOT NULL,
	EndDiscountDate DATE NOT NULL,
	DiscountValue INT NOT NULL
)

CREATE TABLE Custommer (
	idCustommer INT PRIMARY KEY IDENTITY,
	nameCustommer NVARCHAR(100) NOT NULL,
	BirstDay DATE NOT NULL,
	[Address] NVARCHAR(100) NOT NULL,
	phoneNumber NVARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	CreateAccount DATE NOT NULL
)

CREATE TABLE Book (
	idBook INT PRIMARY KEY IDENTITY,
	nameBook NVARCHAR(100) NOT NULL,
	Company_idCompany INT NOT NULL,
	Author_idAuthor INT NOT NULL,
	Type_idType INT NOT NULL,
	number INT NOT NULL,
	price FLOAT NOT NULL,
	FOREIGN KEY (Author_idAuthor)
    REFERENCES Author (idAuthor),
	FOREIGN KEY (Company_idCompany)
    REFERENCES Company (idCompany),
	FOREIGN KEY (Type_idType)
    REFERENCES [Type] (idType)
);

CREATE TABLE BillBook (
	idBill INT,
	idBook INT,
	FOREIGN KEY (idBill)
    REFERENCES Bill (idBill),
	FOREIGN KEY (idBook)
    REFERENCES Book (idBook),
	numberBook INT NOT NULL
);

CREATE TABLE Bill (
	idBill INT PRIMARY KEY IDENTITY,
	Custommer_idCustommer INT,
	Discount_idDiscount INT,
	PayTotal FLOAT,
	PayAfterDiscount FLOAT,
	CreateDate DATETIME,
	FOREIGN KEY (Discount_idDiscount)
	REFERENCES Discount (idDiscount),
	FOREIGN KEY (Custommer_idCustommer)
	REFERENCES Custommer (idCustommer),
)

CREATE TABLE [Order] (
	idOrder INT PRIMARY KEY IDENTITY,
	nameOrder VARCHAR(100) NOT NULL,
	CreateDateOrder DATETIME NOT NULL,
	BillTotal INT NOT NULL,
	StatusOrder VARCHAR(100) NOT NULL,
	BillDate DATETIME
)

SELECT idBook, nameBook, number, price FROM Book WHERE nameBook LIKE N'duy';
