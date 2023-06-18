CREATE TABLE Kategori (
  kid int NOT NULL PRIMARY KEY IDENTITY,
  kname varchar (100) NOT NULL,
  kattanim varchar(150) NOT NULL,
);

INSERT INTO Kategori(kname, kattanim) 
VALUES
('Futbol', 'Futbol urunleri')