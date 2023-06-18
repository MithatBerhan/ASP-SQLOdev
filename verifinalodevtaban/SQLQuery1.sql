CREATE TABLE Musteri (
  mid int NOT NULL PRIMARY KEY IDENTITY,
  mname varchar (100) NOT NULL,
  email varchar(100) NOT NULL,
  phone varchar(100) NOT NULL,
  madres varchar(100) NOT NULL,
);

INSERT INTO Musteri (mname, email, phone, madres) 
VALUES
('Mehmet', 'mehmet@gmail.com', '123456789', 'Hidayet mah.')