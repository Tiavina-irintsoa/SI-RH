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

ALTER TABLE candidature
ALTER COLUMN validation SET DEFAULT 0;


-- vaovao ralph
create table personnel(
    idpersonnel serial primary key,
    nom varchar,
    prenom varchar,
    mail varchar,
    matricule varchar,
    nationalite int,
    adresse text,
    genre int,
    travailleur int,
    dtn date
);

create table personnel_poste(
    idposte int references poste(idposte),
    idpersonnel int references personnel(idpersonnel)
);

create table personnel_salaire(
    idpersonnel_salaire serial primary key,
    idpersonnel int references personnel(idpersonnel),
    salaire_brut numeric,
    salaire_net numeric,
    date_insert date default now()
);


create table personnel_embauche(
    idpersonnel_embauche serial primary key,
    idpersonnel int references personnel(idpersonnel),
    date_embauche date
);

alter table personnel 
add column  contact varchar;

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

ALTER TABLE candidature
ALTER COLUMN validation SET DEFAULT 0;
   
alter table question add column points integer; 
alter table option add column points integer;
------------ Test et entretien 
create table raison (
    idraison serial primary key,
    nomraison varchar
);

create table conge(
    idconge serial primary key,
    idpersonnel int references personnel(idpersonnel),
    datedebut timestamp,
    datefin timestamp,
    reeldatefin timestamp,
    accepte int,
    idraison int references raison(idraison)
);


ALTER TABLE conge
ALTER COLUMN accepte SET DEFAULT 1;


alter table useradmin 
add column idpersonnel int REFERENCES personnel(idpersonnel);

alter table personnel_poste
add column date_embauche date ;

alter table service
add column superieur integer REFERENCES personnel(idpersonnel);


ALTER TABLE admin_service
DROP CONSTRAINT admin_service_idadmin_fkey;

ALTER TABLE admin_service
RENAME COLUMN idadmin TO idtypeuser;


ALTER TABLE admin_service
ADD CONSTRAINT admin_service_idadmin_fkey2
FOREIGN KEY (idtypeuser)
REFERENCES service(idservice);
>>>>>>> a87f48d10ce22acc333c0ab8b47519335830f336

create table question_entretien(
    idquestion_entretien serial primary key,
    question varchar,
    coeff numeric,
    idbesoin integer references besoin(idbesoin)
);
create table note_entretien (
    idquestion_entretien integer references question_entretien(idquestion_entretien),
    idcandidature integer references candidature(idcandidature),
    note numeric 
);

create table refus(
    idrefus serial primary key,
    idconge int references conge(idconge),
    raison_refus text
);