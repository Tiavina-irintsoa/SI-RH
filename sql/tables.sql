\c  postgres
drop database rh;
create database rh ; 
\c rh
create table service(
    idService serial primary key, 
    nomService varchar, 
    iconeService varchar
);


-- ito ny vaovao
create table typecritere(
    idtypecritere serial primary key,
    nomtypecritere varchar
);

create table choix(
    idchoix serial primary key,
    idtypecritere int references typecritere(idtypecritere) ,
    intitulechoix varchar
);

alter table  choix 
add column valeurchoix int;

create table poste(
    idposte serial primary key,
    idservice integer,
    nomposte varchar
);

create table besoin (
    idbesoin serial primary key,
    idposte integer references poste(idposte) ,
    heuresemaine numeric,
    heurepersonne numeric,
    accompli date
);

create table critere(
    idcritere serial primary key,
    idbesoin int references besoin(idbesoin),
    idtypecritere int references typecritere(idtypecritere),
    coefficient numeric
);

create table criterechoix(
    idcritere int references critere(idcritere),
    idchoix int references choix(idchoix)
);


-- vaovao
create table candidat(
    idcandidat serial primary key,
    nomcandidat varchar,
    prenomcandidat varchar,
    dtn date,
    mail varchar,
    contact int 
);

create table candidature(
    idcanditature serial primary key,
    idcandidat int references candidat(idcandidat),
    datecandidature timestamp default now(),
    validation int,
    code varchar,
    idbesoin
);

create table choixcandidat(
    idcanditature int references candidat(idcandidat),
    idchoix int references choix(idchoix)
);

create table fichier(
    idcanditature int references candidature(idcanditature),
    lienfichierdiplome varchar,
    lienfichierexperience varchar
);