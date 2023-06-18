CREATE TABLE Urun (
  urunid int NOT NULL PRIMARY KEY IDENTITY,
  uname varchar (100) NOT NULL,
  urunstok varchar(100) NOT NULL,
  urunkat varchar(100) NOT NULL,
  urunkatid int NOT NULL,
  uruntanim varchar(150) NOT NULL,
  uruntarih datetime NOT NULL,
);

INSERT INTO Urun(uname, urunstok, urunkat, urunkatid, uruntanim, uruntarih) 
VALUES
('Futbol T.', '150', 'Futbol', '1', 'mac topu', '2022-05-03')