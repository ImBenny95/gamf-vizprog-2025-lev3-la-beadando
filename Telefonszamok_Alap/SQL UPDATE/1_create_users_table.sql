CREATE TABLE Felhasznalok (
    id INT PRIMARY KEY IDENTITY,
    Felhasznalonev NVARCHAR(50) NOT NULL,
    Jelszo NVARCHAR(50) NOT NULL -- teszteléshez sima szöveg, élesben hash!
);
