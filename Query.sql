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

UPDATE ExchangeBook SET ExchangeBook.number = 1, ExchangeBook.reason = N'dấdasd', ExchangeBook.[status] = N'adsadsa', ExchangeBook.endDay = '2022-11-30 03:41:10.000' WHERE ExchangeBook.IdExchangeBook = 1;

SELECT * FROM ExchangeBook WHERE ExchangeBook.[status] = N'Chưa đổi';

UPDATE ExchangeBook SET number = 4, reason = N'd', [status] = N'Đã đổi' WHERE IdExchangeBook = '1';

UPDATE Book SET number = (SELECT number FROM Book WHERE idBook = 1) - 1 WHERE idBook = 1;

CREATE TABLE Discount (
	idDiscount INT PRIMARY KEY IDENTITY,
	nameDiscount NVARCHAR(100) NOT NULL,
	StartDiscountDate DATE NOT NULL,
	EndDiscountDate DATE NOT NULL,
	Discount INT NOT NULL
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

SELECT * FROM Book, Company, [Type], Author WHERE Book.Company_idCompany = Company.idCompany AND Book.Author_idAuthor = Author.idAuthor AND Book.Type_idType = [Type].idType;
UPDATE Book SET nameBook = N'TORO', Company_idCompany = (SELECT idCompany FROM Company WHERE nameCompany = N'Tuổi trẻ'), Author_idAuthor = (SELECT idAuthor FROM Author WHERE nameAuthor = N'Vũ Khương Duy'), Type_idType = (SELECT idType FROM Type WHERE nameType = N'Giải trí'), number = 2, price = 9999 WHERE idBook = 1;

CREATE TABLE Bill (
	idBill INT PRIMARY KEY IDENTITY,
	nameInvoice NVARCHAR(100) NOT NULL,
	Custommer_idCustommer INT NOT NULL,
	Discount_idDiscount INT NOT NULL,
	PayTotal FLOAT NOT NULL,
	PayAfterDiscount FLOAT NOT NULL,
	CreateDate DATETIME NOT NULL,
	Book_idBook INT NOT NULL,
	FOREIGN KEY (Discount_idDiscount)
	REFERENCES Discount (idDiscount),
	FOREIGN KEY (Custommer_idCustommer)
	REFERENCES Custommer (idCustommer),
	FOREIGN KEY (Book_idBook)
    REFERENCES Book (idBook)
)


CREATE TABLE [Order] (
	idOrder INT PRIMARY KEY IDENTITY,
	nameOrder VARCHAR(100) NOT NULL,
	CreateDateOrder DATETIME NOT NULL,
	BillTotal INT NOT NULL,
	StatusOrder VARCHAR(100) NOT NULL,
	BillDate DATETIME
)