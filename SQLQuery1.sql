create database S_G_H_DB
USE S_G_H_DB
AKJ40123/@_@/abc
insert into Chambres(Nombre,Type,Statu,PatientMatricule) values (101,'Examen','Disponible',5)
insert into Chambres(Nombre,Type,Statu,PatientMatricule) values (102,'Chambre de Patient','Disponible',6)
insert into Chambres(Nombre,Type,Statu,PatientMatricule) values (103,'Chambre des Opérations','Disponible',7)
insert into Chambres(Nombre,Type,Statu,PatientMatricule) values (104,'Salle de Radiologie','Disponible',8)

insert into Docteurs values ('7mida','Masculin','Tbib o safi','photo 1')
insert into Patients values ('nom 1','prenom 1','m','1-1-2021','email1','09483490','dcdjckd',3)

create table Docteurs
(
   Matricule int primary key identity(1,1),
   FullName nvarchar(max),
   Genre nvarchar(max),
   Spécialité nvarchar(max),
   Photo nvarchar(max)
)
go
create table Patients
(
    Matricule int primary key identity(1,1),
	Nom nvarchar(max) ,
	Prenom nvarchar(max),
	Genre nvarchar(max),
	DateNaissance nvarchar(max),
	Email nvarchar(max),
	Telephone nvarchar(max),
	Adresse nvarchar(max),
	DocteurMatricule int,
	constraint FK_Patients_Docteurs_DocteurMatricule foreign key (DocteurMatricule) references Docteurs(Matricule) on delete set null
)
go 
create table Chambres
(
    Id int primary key identity(1,1),
	Nombre int,
	Type nvarchar(max),
	Statu nvarchar(max),
	PatientMatricule int,
	constraint FK_Chambres_Patients_PatientMatricule foreign key (PatientMatricule) references Patients(Matricule) on delete set null
)
go
create table Factures
(
    Id int primary key identity(1,1),
	Nombre int,
	Montant money,
	DatePaiement date,
	TypePaiement nvarchar(max),
	PatientMatricule int,
	constraint FK_Factures_Patients_PatientMatricule foreign key (PatientMatricule) references Patients(Matricule) on delete set null
)
go

select * from Patients
select * from Docteurs
select * from Chambres
select * from Factures
select * from AspNetUsers

delete from Docteurs where Matricule = 4
delete from Patients where Matricule = 2
delete from Chambres where Id in (28,29)

select * from Books
select * from Publisher 

Update Chambres 
set PatientMatricule = 6 where Id = 21


Admin	
ADMIN
EMAIL
EMAIL
True	
AQAAAAEAACcQAAAAEAPIF6fdyKih1ZqT9dY06wjMMQEavEAG0wqbR0Sn1VgyKNHPbS99IySL/ToBPLZpSg==
SE6AOSD46UKY4NB6YQVBQF3VVOMKTBNB
2306d929-6e31-4fff-a9a3-794cbe19c637