CREATE TABLE Siparis (
  sipid int NOT NULL PRIMARY KEY IDENTITY,
  musid int NOT NULL,
  sipadres varchar (100) NOT NULL,
  sipicerik varchar(100) NOT NULL,
  siptanim varchar(150) NOT NULL,
  siptarih datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
);

INSERT INTO Siparis(musid, sipadres, sipicerik, siptanim, siptarih) 
VALUES
('1', 'hidayet mah.', 'Futbol topu', '1 adet futbol topu', '2022-05-03')