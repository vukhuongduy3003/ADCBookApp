CREATE TABLE Item (
	idItem INT PRIMARY KEY IDENTITY,
	nameItem NVARCHAR(100) NOT NULL,
	typeItem NVARCHAR(100) NOT NULL,
	quantityItem INT NOT NULL,
	brandItem NVARCHAR(100) NOT NULL,
	releaseDateItem DATE NOT NULL,
	priceItem INT NOT NULL
);

CREATE TABLE Discount (
	idDiscount INT PRIMARY KEY IDENTITY,
	nameDiscount NVARCHAR(100) NOT NULL,
	startTimeDiscount DATE NOT NULL,
	endTimeDiscount DATE NOT NULL,
	typeDiscount NVARCHAR(100) NOT NULL,
	priceAmountDiscount INT NOT NULL,
	percentDiscount INT NOT NULL
);

INSERT INTO Item (nameItem, typeItem, quantityItem, brandItem, releaseDateItem, priceItem) VALUES (N'Vu', N'Khuong', 3, N'Duy', '2002-03-30', 4000);