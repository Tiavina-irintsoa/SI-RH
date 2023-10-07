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
    idbesoin int references besoin(idbesoin)
);

create table choixcandidature(
    idcanditature int references candidat(idcandidat),
    idchoix int references choix(idchoix)
);
  
create table fichier(
    idcanditature int references candidature(idcanditature),
    lienfichierdiplome varchar,
    lienfichierexperience varchar
);

create table type_contrat(
    idtypecontrat serial primary key,
    nomtype_contrat varchar
);


alter table besoin 
add column idtypecontrat integer references type_contrat(idtypecontrat);

alter table besoin 
alter column idtypecontrat set default 1;


create table typeuser(
    idtypeuser serial primary key,
    description varchar
);

create table useradmin(
    idadmin serial primary key,
    nom varchar, 
    mdp varchar,
    idtypeuser int references typeuser(idtypeuser)
);

create table admin_service(
    idadmin int references useradmin(idadmin),
    idservice int references service(idservice)
);

-- vaovao
alter table candidat 
    alter column contact type varchar;

create table questionnaire(
    idquestionnaire serial primary key, 
    idbesoin integer references besoin(idbesoin)
);

create table question(
    idquestion serial primary key, 
    idquestionnaire integer references questionnaire(idquestionnaire), 
    question varchar
);
create table option(
    idoption serial primary key, 
    idquestion integer references question(idquestion) , 
    option varchar
);

ALTER TABLE candidature RENAME COLUMN idcanditature TO idcandidature;

ALTER TABLE choixcandidature RENAME COLUMN idcanditature TO idcandidature;

ALTER TABLE fichier RENAME COLUMN idcanditature TO idcandidature;

-- vaovao 2
ALTER TABLE choixcandidature DROP CONSTRAINT choixcandidature_idcanditature_fkey;

ALTER TABLE choixcandidature
ADD CONSTRAINT choixcandidature_idcanditature_fkey FOREIGN KEY (idcandidature) REFERENCES candidature(idcandidature);