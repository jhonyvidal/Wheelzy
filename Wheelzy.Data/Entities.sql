CREATE TABLE Car (
    CarID INT PRIMARY KEY,
    Year INT NOT NULL,
    Make VARCHAR(50) NOT NULL,
    Model VARCHAR(50) NOT NULL,
    Submodel VARCHAR(50) NOT NULL,
    ZipCode VARCHAR(10) NOT NULL,
    CurrentBuyerID INT,
    CONSTRAINT fk_CurrentBuyer FOREIGN KEY (CurrentBuyerID) REFERENCES Buyer(BuyerID)
);

CREATE TABLE Buyer (
    BuyerID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Quote (
    QuoteID INT PRIMARY KEY,
    CarID INT NOT NULL,
    BuyerID INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    IsCurrent BIT NOT NULL,
    CONSTRAINT fk_Car FOREIGN KEY (CarID) REFERENCES Car(CarID),
    CONSTRAINT fk_Buyer FOREIGN KEY (BuyerID) REFERENCES Buyer(BuyerID)
);

CREATE TABLE Status (
    StatusID INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE StatusHistory (
    StatusHistoryID INT PRIMARY KEY,
    CarID INT NOT NULL,
    StatusID INT NOT NULL,
    StatusDate DATETIME NOT NULL,
    ChangedBy VARCHAR(100) NOT NULL,
    CONSTRAINT fk_CarStatus FOREIGN KEY (CarID) REFERENCES Car(CarID),
    CONSTRAINT fk_Status FOREIGN KEY (StatusID) REFERENCES Status(StatusID)
);

CREATE TABLE customer(
	CustomerId int IDENTITY(1,1) NOT NULL,
	Name nvarchar(max) NOT NULL,
	Email nvarchar(max) NOT NULL,
    CONSTRAINT PK_customer PRIMARY KEY CLUSTERED 
);

CREATE TABLE Orders(
	OrderId int IDENTITY(1,1) NOT NULL,
	OrderDate datetime2(7) NOT NULL,
	CustomerId int NOT NULL,
	StatusId int NOT NULL,
	IsActive bit NOT NULL,
    CONSTRAINT PK_Orders PRIMARY KEY CLUSTERED 
);