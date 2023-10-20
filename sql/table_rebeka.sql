create table avantage(
    idavantage serial primary key,
    nomavantage varchar
);

create table sante(
    idsante serial primary key,
    nomsante varchar
);

create table essai(
    idessai serial primary key,
    idbesoin integer references besoin(idbesoin),
    idcandidat integer references candidat(idcandidat),
    duree real,
    debut date,
    salaire_base real
);

create table essai_avantage(
    idessai integer references essai (idessai),
    idavantage int references avantage (idavantage)
);

create table info(
    idinfo serial primary key,
    idcandidat integer references candidat(idcandidat),
    cin varchar,
    adresse varchar,
    pere varchar,
    mere varchar,
    nbenfant int
);

create table contrat_essai(
    idcontrat_essai serial primary key,
    idessai int references essai(idessai),
    signessai date
);

create table travail(
    idtravail serial primary key,
    idcontrat_essai integer references contrat_essai(idcontrat_essai),
    duree real,
    debut date
);

create table contrat_travail(
    idcontrat_travail serial primary key,
    idtravail int references travail(idtravail),
    signetravail date
);

create table travail_sante(
    idcontrat_travail integer references contrat_travail(idcontrat_travail),
    idsante int references sante (idsante)
);

-- drop table travail_sante;
-- drop table contrat_travail;
-- drop table travail;
-- drop table contrat_essai;
-- drop table essai_avantage;
-- drop table essai;

-- create table personnel(
--     idpersonnel serial primary key,
--     nom varchar,
--     prenom varchar,
--     mail varchar,
--     matricule varchar,
--     nationalite int,
--     adresse text,
--     genre int,
--     travailleur int,
--     dtn date
-- );

-- create table personnel_poste(
--     idposte int references poste(idposte),
--     idpersonnel int references personnel(idpersonnel)
-- );

-- create table personnel_salaire(
--     idpersonnel_salaire serial primary key,
--     idpersonnel int references personnel(idpersonnel),
--     salaire_brut numeric,
--     salaire_net numeric,
--     date_insert date default now()
-- );


-- create table personnel_embauche(
--     idpersonnel_embauche serial primary key,
--     idpersonnel int references personnel(idpersonnel),
--     date_embauche date
-- );

-- alter table personnel 
-- add column  contact varchar;
-- =======
-- alter table question add column points integer; 
-- alter table option add column points integer;