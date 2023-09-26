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
    accompli int
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

