--UNCOMMENT TO CREATE DATABASE

--CREATE DATABASE PhoneBook;
--GO
--CREATE TABLE PhoneBook.dbo.People(
--name		VARCHAR(20), 
--surname		VARCHAR(20), 
--id			VARCHAR(36),
--CONSTRAINT people_primary_key PRIMARY KEY (id)
--);
--GO
--CREATE TABLE PhoneBook.dbo.Locations(
--id			INT,
--city		VARCHAR(20),
--zipcode		VARCHAR(20),
--);
--GO
--CREATE TABLE PhoneBook.dbo.Numbers(
--id			INT,
--number		VARCHAR(20),
--CONSTRAINT numbers_primary_key PRIMARY KEY (id)
--);
--GO
--CREATE TABLE PhoneBook.dbo.Contacts(
--id_person		VARCHAR(36),
--id_location		INT,
--id_number		INT,
--CONSTRAINT foreign_key_people FOREIGN KEY (id_person) REFERENCES PhoneBook.dbo.People (id),
--CONSTRAINT foreign_key_numbers FOREIGN KEY (id_number) REFERENCES PhoneBook.dbo.Numbers (id)
--);
--GO

