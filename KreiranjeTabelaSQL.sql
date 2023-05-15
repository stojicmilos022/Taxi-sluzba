create database DotNet18_Test1_Milos_Stojic

use  DotNet18_Test1_Milos_Stojic
--drop table kurs
create table Vozilo
(
id int primary key identity(1,1),
Registracija nvarchar(50) not null,
)
--drop table adrese
create table Adresa
(
id int primary key identity(1,1),
Ulica nvarchar(50),
Broj nvarchar (5),
Mesto nvarchar(50)
)

--drop table voznja

create table Voznja
(
id int primary key identity(1,1),
id_vozilo int,
id_polazak int,
id_dolazak int,
ZavrsenaDN nvarchar(1),
	foreign key (id_vozilo) references vozilo(id),
	foreign key (id_polazak) references adresa(id),
	foreign key (id_dolazak) references adresa(id),
CONSTRAINT CHK_Zavrsena CHECK (ZavrsenaDN in ('D','N'))
)

