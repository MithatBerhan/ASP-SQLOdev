CREATE TABLE Personel (
  pid int NOT NULL PRIMARY KEY IDENTITY,
  pname varchar (100) NOT NULL,
  pemail varchar(100) NOT NULL,
  pphone varchar(100) NOT NULL,
  ptarih datetime NOT NULL,
);

INSERT INTO Personel(pname, pemail, pphone, ptarih) 
VALUES
('Memoli', 'memoli@gmail.com', '123456798', '2022-02-01')