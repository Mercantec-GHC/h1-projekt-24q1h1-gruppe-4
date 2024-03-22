CREATE TABLE Books (
Id SERIAL PRIMARY KEY,
Title TEXT,
Author TEXT,
Condition TEXT,
Category TEXT,
Price FLOAT,
ImagePath TEXT,
Language TEXT,
ReleaseDate TEXT,
Format TEXT,
ISBN BIGINT,
Weight FLOAT,
Pages INT,
Description TEXT,
Stars FLOAT,
Reviews TEXT[],
Type TEXT
);
