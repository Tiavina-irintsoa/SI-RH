
-- ito ny vaovao
-- Insertion dans la table typecriterno
INSERT INTO typecritere (nomtypecritere)
VALUES
    ('genre'),
    ('nationalite'),
    ('diplome'),
    ('experience'),
    ('situation matrimoniale')
    ;

insert into type_contrat ( nomtype_contrat )
values( 'contrat  a duree determinee' ),
('contrat a  duree indeterminee'),
('contrat temporaire')
;

-- Insertion dans la table choix
INSERT INTO choix (idtypecritere, intitulechoix , valeurchoix)
VALUES
    (1, 'homme' , 1),
    (1, 'femme' , 1),
    (2, 'Malagasy' , 1),
    (2, 'Etranger' , 1),
    (3, 'CEPE' , 1),
    (3, 'BEPC' , 2),
    (3, 'BACC' , 3),
    (3, 'Licence', 4 ),
    (3, 'Master' , 5),    
    (3, 'Doctorat' , 6),
    (4, 'moins de 2 ans' , 1 ) , 
    (4, 'entre 2 a 4 ans' , 2  ) , 
    (4, 'entre 4 a 7 ans' , 3 ), 
    (4, '7ans et plus', 4 ),
    (5, 'marie(e)',1 ),           
    (5, 'divorce(e)',1 ),
    (5, 'en couple',1 )
    ;

insert into service (nomService, iconeService) values
('Achats', 'achats.png'),
('Finance', 'finance.png'),
('Informatique', 'IT.png'),
('Logistique', 'logistique.png'),
('Production industrielle', 'production.png'),
('Recherche et developpement', 'recherche.png'),
('Ressources humaines', 'rh.png'),
('Maintenance et Reparation', 'maintenance.png'),
('Marketing', 'marketing.png');

INSERT INTO poste (idservice, nomposte) VALUES
    (1, 'Responsable des achats'),
    (1, 'Analyste'),
    (1, 'Approvisionneur'),
    (1, 'Gestionnaire de la chaine d''approvisionnement'),
    (1, 'Specialiste des contrats'),
    (1, 'Responsable des relations avec les fournisseurs'),
    (1, 'Responsable des achats durables');

INSERT INTO poste (idservice, nomposte) VALUES
    (2, 'Analyste financier'),
    (2, 'Comptable'),
    (2, 'Controleur financier'),
    (2, 'Responsable financier'),
    (2, 'Analyste de credit'),
    (2, 'Gestionnaire de portefeuille'),
    (2, 'Analyste de risque financier'),
    (2, 'Gestionnaire de tresorerie'),
    (2, 'Analyste en fusion-acquisition'),
    (2, 'Auditeur financier'),
    (2, 'Planificateur financier'),
    (2, 'Specialiste des marches financiers'),
    (2, 'Analyste des operations bancaires'),
    (2, 'Specialiste en fiscalite'),
    (2, 'Actuaire');

INSERT INTO poste (idservice, nomposte) VALUES
    (3, 'Developpeur de logiciels'),
    (3, 'Ingenieur en securite informatique'),
    (3, 'Administrateur systeme'),
    (3, 'Analyste de donnees'),
    (3, 'Ingenieur en reseau'),
    (3, 'Developpeur web'),
    (3, 'Architecte de solutions cloud'),
    (3, 'Analyste en intelligence artificielle'),
    (3, 'Chef de projet informatique'),
    (3, 'Developpeur d''applications mobiles'),
    (3, 'Architecte de donnees'),
    (3, 'Testeur de logiciels'),
    (3, 'Specialiste en cybersecurite'),
    (3, 'Administrateur de bases de donnees'),
    (3, 'Analyste en assurance qualite logicielle'),
    (3, 'Developpeur DevOps'),
    (3, 'Consultant en technologies de l''information'),
    (3, 'Expert en analyse de donnees'),
    (3, 'Administrateur de systemes de gestion de contenu (CMS)'),
    (3, 'Developpeur de jeux video');

INSERT INTO poste (idservice, nomposte) VALUES
    (4, 'Gestionnaire de la chaine d''approvisionnement'),
    (4, 'Responsable logistique'),
    (4, 'Gestionnaire d''entrepot'),
    (4, 'Planificateur de la demande'),
    (4, 'Responsable des operations de transport'),
    (4, 'Coordinateur de la chaine d''approvisionnement'),
    (4, 'Analyste de la chaine d''approvisionnement'),
    (4, 'Planificateur de la production'),
    (4, 'Specialiste en gestion des stocks'),
    (4, 'Analyste en logistique internationale'),
    (4, 'Gestionnaire de la qualite logistique'),
    (4, 'Coordonnateur de la distribution'),
    (4, 'Expert en gestion des retours'),
    (4, 'Responsable de la logistique inversee'),
    (4, 'Analyste en optimisation des itineraires'),
    (4, 'Coordonnateur des operations de transport international'),
    (4, 'Planificateur de la logistique e-commerce'),
    (4, 'Gestionnaire de la logistique de la sante'),
    (4, 'Responsable de la gestion des fournisseurs'),
    (4, 'Analyste en coûts logistiques');

INSERT INTO poste (idservice, nomposte)
VALUES
    (5, 'Operateur de machine industrielle'),
    (5, 'Technicien de production'),
    (5, 'Superviseur de production'),
    (5, 'Ingenieur de production'),
    (5, 'Operateur de CNC'),
    (5, 'Ouvrier'),
    (5, 'Soudeur'),
    (5, 'Planificateur de production'),
    (5, 'Electromecanicien'),
    (6, 'Ingenieur de recherche'),
    (6, 'Chercheur Scientifique'),
    (6, 'Ingenieur en conception produit'),
    (6, 'Chimiste de recherche'),
    (7, 'Responsable des ressources humaines'),
    (7, 'Recruteeur'),
    (7, 'Gestionnaire de la paie'); 

-- Insertion de données dans la table "poste"
INSERT INTO poste (idservice, nomposte) values

    (8, 'Technicien de Maintenance Industrielle'),
    (8, 'Electricien de Batiment'),
    (8, 'Plombier'),
    (8, 'Mecanicien Automobile'),
    (8, 'Technicien en Climatisation et Chauffage'),
    (8, 'Technicien en Informatique et Réseaux'),
    (8, 'Chef d''Equipe de Maintenance'),
    (9, 'Specialiste en Marketing Numerique'),
    (9, 'Responsable des Medias Sociaux'),
    (9, 'Chef de Produit'),
    (9, 'Analyste Marketing'),
    (9, 'Responsable du Marketing Produit')
    ;

-- Insertion de données dans la table "besoin"
-- INSERT INTO besoin (idposte, heurepersonne , heuresemaine , accompli)
-- VALUES
--     (1, 40.0, 100.0, now()),
--     (1, 35.0, 90.0, now()),
--     (2, 38.0, 110.0, null),
--     (3, 30.0, 80.0, null);


insert into typeuser values
        ( 1 , 'Achats' ) ,                  
        ( 2 , 'Finance' ),                  
        ( 3 , 'Informatique'),              
        ( 4 , 'Logistique'    ),            
        ( 5 , 'Production industrielle' ),  
        ( 6 , 'Recherche et developpement'),
        ( 7 , 'Ressources humaines'     ),  
        ( 8 , 'Maintenance et Reparation'), 
        (9 , 'Marketing'),
        ( 10 , 'directeur' );

insert into useradmin ( nom , mdp , idtypeuser )
values( 'directeur' , 'directeur' , 10 ),
( 'finance' , 'finance' , 2 ),
( 'info' , 'info' , 3 );

insert into  admin_service
values( 1 , 1 ),
(1,2),
(2,1),
(2,2),
(2,3),
(3,4),
(3,5);

-- insert into candidat( nomcandidat, prenomcandidat , dtn, mail, contact) values
--     ('Razafindrakoto', 'Tsiory', '2003-04-19', 'tsiorySrbd@gmail.com', '032 86 459 68'),
--     ('Andrianatoandro', 'Soahery', '2003-09-12', 'herysoa@gmail.com', '032 86 359 48'),
--     ('Rajaonasitera', 'Mihaja', '2004-06-10', 'mihajaraj@gmail.com', '033 86 672 11'),
--     ('Rasoavololona', 'Tiana', '2005-01-07', 'soatiana@gmail.com', '034 86 439 25'),
--     ('Andriantefy', 'Hasina', '2006-08-21', 'hasina@gmail.com', '032 86 459 13');





-- vaovao ralph 
-- Insertion de données dans la table "personnel"
INSERT INTO personnel (nom, prenom, mail, matricule, nationalite, adresse, genre, travailleur, dtn)
VALUES
    ('Doe', 'John', 'john.doe@email.com', ( 'PERS' || nextval('idpersonnel')), 1, '123 Main St', 1, 1, '1990-01-01'),
    ('Smith', 'Jane', 'jane.smith@email.com', ( 'PERS' || nextval('idpersonnel')), 2, '456 Elm St', 2, 1, '1995-03-15'),
    ('Johnson', 'Robert', 'robert.johnson@email.com', ( 'PERS' || nextval('idpersonnel')), 3, '789 Oak St', 1, 0, '1985-07-10'),
    -- Ajoutez ici d'autres lignes d'insertion pour un total d'au moins 20 lignes
    ('Brown', 'Sarah', 'sarah.brown@email.com', ( 'PERS' || nextval('idpersonnel')), 1, '567 Pine St', 2, 1, '1988-11-20'),
    ('Garcia', 'Carlos', 'carlos.garcia@email.com', ( 'PERS' || nextval('idpersonnel')), 2, '890 Maple St', 1, 1, '1998-04-25'),
    ('Wilson', 'Emily', 'emily.wilson@email.com', ( 'PERS' || nextval('idpersonnel')), 3, '234 Birch St', 2, 0, '1993-09-05'),
    ('Chen', 'Wei', 'wei.chen@email.com', ( 'PERS' || nextval('idpersonnel')), 1, '345 Cedar St', 1, 1, '1992-12-12'),
    ('Martinez', 'Luis', 'luis.martinez@email.com', ( 'PERS' || nextval('idpersonnel')), 2, '456 Oak St', 1, 1, '1989-06-30'),
    ('Davis', 'Susan', 'susan.davis@email.com', ( 'PERS' || nextval('idpersonnel')), 3, '567 Elm St', 2, 1, '1987-03-07'),
    ('Kim', 'Min-Ji', 'minji.kim@email.com', ( 'PERS' || nextval('idpersonnel')), 1, '678 Pine St', 2, 1, '1991-08-14'),
    ('Nguyen', 'Thi', 'thi.nguyen@email.com', ( 'PERS' || nextval('idpersonnel')), 2, '789 Cedar St', 2, 0, '1986-02-19'),
    ('Jackson', 'William', 'william.jackson@email.com', ( 'PERS' || nextval('idpersonnel')), 3, '890 Birch St', 1, 1, '1984-05-28');
    ('Doe', 'John', 'john.doe@email.com', '12345', 1 , '123 Main St', 1 , 1, '1990-01-01'),
    ('Smith', 'Jane', 'jane.smith@email.com', '67890', 2, '456 Elm St', 2, 1, '1995-03-15'),
    ('Johnson', 'Robert', 'robert.johnson@email.com', '54321', 3, '789 Oak St', 1, 0, '1985-07-10'),
    ('Brown', 'Sarah', 'sarah.brown@email.com', '98765', 1, '567 Pine St', 2, 1, '1988-11-20'),
    ('Garcia', 'Carlos', 'carlos.garcia@email.com', '13579', 2, '890 Maple St', 1, 1, '1998-04-25'),
    ('Wilson', 'Emily', 'emily.wilson@email.com', '24680', 3, '234 Birch St', 2, 0, '1993-09-05'),
    ('Chen', 'Wei', 'wei.chen@email.com', '10101', 1, '345 Cedar St', 1, 1, '1992-12-12'),
    ('Martinez', 'Luis', 'luis.martinez@email.com', '11223', 2, '456 Oak St', 1, 1, '1989-06-30'),
    ('Davis', 'Susan', 'susan.davis@email.com', '22222', 3, '567 Elm St', 2, 1, '1987-03-07'),
    ('Kim', 'Min-Ji', 'minji.kim@email.com', '33333', 1, '678 Pine St', 2, 1, '1991-08-14'),
    ('Nguyen', 'Thi', 'thi.nguyen@email.com', '44444', 2, '789 Cedar St', 2, 0, '1986-02-19'),
    ('Jackson', 'William', 'william.jackson@email.com', '55555', 3, '890 Birch St', 1, 1, '1984-05-28');



-- Insertion de données aléatoires dans la table "personnel_poste" avec des idposte aléatoires entre 1 et 180
-- INSERT INTO personnel_poste (idposte, idpersonnel)
-- SELECT floor(random() * 90) + 1, idpersonnel
-- FROM personnel;

insert into personnel_poste 
values 
    (14, 1), 
    (28, 2), 
    (10, 3), 
    (70, 4), 
    (63, 6), 
    (71, 7), 
    (75, 8), 
    (38, 9), 
    (62,10), 
    (10,11), 
    (66,12), 
    (41, 5), 
    (92,16);

INSERT INTO personnel (nom, prenom, mail, matricule, nationalite, adresse, genre, travailleur, dtn)
VALUES
    ('Ralph', 'Yoan', 'ralph.doe@email.com', '12345', 1, '123 Main St', 1, 1, '1992-01-01'),
    ('Rebeka', 'Ravalison', 'Rebeka.smith@email.com', '67890', 2, '456 Elm St', 2, 1, '1991-03-15'),
    ('Tita', 'Goore', 'tita.johnson@email.com', '54321', 3, '789 Oak St', 1, 0, '1985-07-10');

INSERT INTO personnel_poste (idposte, idpersonnel)
values ( 76 , 13 ),
( 77 , 14 ),
( 78 , 15 );

-- Insertion de données aléatoires dans la table "personnel_salaire"
INSERT INTO personnel_salaire (idpersonnel, salaire_brut, salaire_net)
SELECT idpersonnel, random() * 10000, random() * 8000
FROM personnel;



-- Insertion de données aléatoires dans la table "personnel_embauche"
-- INSERT INTO personnel_embauche (idpersonnel, date_embauche)
-- SELECT idpersonnel, CURRENT_DATE - (floor(random() * 365) + 1)::integer * interval '1 day'
-- FROM personnel;

INSERT INTO personnel_embauche (idpersonnel, date_embauche)
    ( 1 , '2013-03-01'),
    ( 2 , '2013-02-09'),
    ( 3 , '2013-03-08'),
    ( 4 , '2012-12-31'),
    ( 5 , '2013-07-03'),
    ( 6 , '2013-09-16'),
    ( 7 , '2013-06-20'),
    ( 8 , '2012-11-07'),
    ( 9 , '2013-09-07'),
    (10 , '2013-09-25'),
    (11 , '2013-10-03'),
    (12 , '2013-01-06'),
    ( 1 , '2013-02-20'),
    ( 2 , '2013-03-28'),
    ( 3 , '2012-11-08'),
    ( 4 , '2013-08-03'),
    ( 5 , '2013-05-24'),
    ( 6 , '2013-05-01'),
    ( 7 , '2013-05-06'),
    ( 8 , '2013-05-08'),
    ( 9 , '2013-10-11'),
    (10 , '2013-10-09'),
    (11 , '2013-09-07'),
    (12 , '2012-11-05'),
    (13 , '2013-01-24'),
    (14 , '2012-10-27'),
    (15 , '2013-04-25');

update personnel 
set contact = '032 46 234 43';

--c ralph encore 
insert into raison ( nomraison )
values ( 'maternite' ) , ('paternite') , ('maladie') ;

INSERT INTO conge (idpersonnel, datedebut, datefin, reeldatefin, accepte, idraison)
VALUES
  (5, '2023-03-22 08:00:00', '2023-03-25 12:00:00', '2023-03-25 12:00:00', 3, null),
  (5, '2023-03-23 09:30:00', '2023-03-26 15:45:00', '2023-03-26 15:45:00', 3, 1),
  (5, '2023-03-24 10:15:00', '2023-03-27 11:30:00', '2023-03-27 11:30:00', 3, null),
  (5, '2023-03-25 12:30:00', '2023-03-28 14:15:00', '2023-03-28 14:15:00', 3, 1),
  (5, '2023-03-26 16:00:00', '2023-03-29 16:30:00', '2023-03-29 16:30:00', 3, 3);

insert into useradmin(nom,mdp,idtypeuser,idpersonnel)
values ( 'Garcia' , '12345' , 3 , 5 );
update service set superieur = 6;

-- Insertion de données de test dans la table question_entretien
-- INSERT INTO question_entretien (question, coeff,idbesoin)
-- VALUES
--     ('Quelles sont vos compétences en programmation?', 1.5,2),
--     ('Parlez-moi de votre expérience précédente en gestion de projets.', 2.0,2),
--     ('Comment gérez-vous les situations de conflit au sein de l''équipe?', 1.8,2);

-- Insertion de données de test dans la table reponse_entretien
-- INSERT INTO note_entretien (idquestion_entretien, idcandidature, note)
-- VALUES
--     (1, 5, 1.0),
--     (2, 5, 1.5),
--     (3, 5, 1.2);

update service 
set superieur = 13
where idservice = 7;

insert into useradmin ( nom,mdp,idtypeuser,idpersonnel )
values( 'Ralph' , '12345' , 7 , 13 );

insert into  admin_service
values( 7 , 1 ),
(7,2),
(7,3),
(7,4),
(7,5),
(7,6),
(7,7);



insert into conge ( idpersonnel ,datedebut, reeldatefin ,  datefin, accepte  )
values(5,'2023-12-14 08:00:00' , '2023-12-15 17:00:00' , '2023-12-15 17:00:00',2  );

insert into avantage (nomavantage) values
    ('Voiture'), ('Maison'), ('Secretaire');

insert into sante (nomsante) values
    ('Funhece'), ('AMIT'), ('Ostie');

insert into info (idcandidat, cin, adresse, pere, mere, nbenfant) values (@idcandidat, @cin, @adresse, @pere, @mere, @nbenfant)

insert into contrat_essai (idessai, net, signessai) values (@idessai, @net, @signessai)

insert into travail (idcontrat_essai, duree, debut) values (@idcontrat_essai, @duree, @debut)

insert into contrat_travail (idtravail, signetravail) values (@idtravail, @signetravail)

insert into travail_sante values (@idcontrat_travail, @idsante)

-- vaovao ralph
update personnel_poste set idposte = 41
where idpersonnel = 5;

insert into useradmin  (nom,mdp,idtypeuser,idpersonnel)
values( 'Wilson' , '12345' , 5 , 6 );

insert into admin_service ( idtypeuser , idservice )
values( 5 , 2 ),
( 5 , 3 )
;

update useradmin 
set idpersonnel = 2
where idadmin = 3;

insert into planning_visible( idservice , idvisible )
values
( 1 , 1  ),
( 2 , 1  ),
( 2 , 2  ),
( 3 , 3  ),
( 3 , 2  ),
( 4 , 1  ),
( 4 , 3  ),
( 4 , 4  ),
( 5 , 1  ),
( 5 , 2  ),
( 5 , 3  ),
( 6 , 1  ),
( 7 , 1  ),
( 7 , 2  ),
( 7 , 3  ),
( 7 , 4  ),
( 7 , 5  ),
( 7 , 6  ),
( 7 , 7  )
;

insert into personnel ( nom,prenom,mail,matricule,nationalite , adresse , genre , travailleur ,    dtn     ,    contact )
values
('Dupont', 'Jean', 'jean.dupont@example.com', '123456', 1 , '123 Rue de la Paix, Paris', 1 , 1 , '2022-10-22', '0612345678');



insert into service ( nomservice  ,  iconeservice , superieur )
values( 'direction' , 'recherche.png' ,  16 );

INSERT INTO poste (idservice, nomposte) VALUES
(10, 'Directeur Général'),
(10, 'Directeur des Ventes');

insert into personnel_poste ( idpersonnel , idposte )
values ( 16 , 92 );

insert into planning_visible( idservice , idvisible )
values
( 10 , 1  ),
( 10 , 2  ),
( 10 , 3  ),
( 10 , 4  ),
( 10 , 5  ),
( 10 , 6  ),
( 10 , 7  ),
( 10 , 8  ),
( 10 , 9  ),
( 10 , 10  )
;

insert into admin_service( idtypeuser , idservice )
values
( 10 , 1  ),
( 10 , 2  ),
( 10 , 3  ),
( 10 , 4  ),
( 10 , 5  ),
( 10 , 6  ),
( 10 , 7  ),
( 10 , 8  ),
( 10 , 9  ),
( 10 , 10  )
;

update useradmin 
set idpersonnel = 16 
where idadmin = 1;

update personnel set contact = '032 46 234 43';

update useradmin set idpersonnel = 1
where idadmin = 2;

insert into useradmin(nom,mdp ,idtypeuser,idpersonnel)
values ( 'John' , '12345'  , 2 , 1 ),
((select prenom from personnel where idpersonnel =  3) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 3 )    ,3),
((select prenom from personnel where idpersonnel =  4) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 4 ) ,4),
((select prenom from personnel where idpersonnel =  7) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 7 ) ,7),
((select prenom from personnel where idpersonnel =  8) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 8 ) ,8),
((select prenom from personnel where idpersonnel =  9) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 9 ) ,9),
((select prenom from personnel where idpersonnel = 10) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 10 ) ,10),
((select prenom from personnel where idpersonnel = 11) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 11 ) ,11),
((select prenom from personnel where idpersonnel = 12) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 12 ) ,12),
((select prenom from personnel where idpersonnel = 14) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 14 ) ,14),
((select prenom from personnel where idpersonnel = 15) ,'12345', ( select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = 15 ) ,15)
;

insert into  admin_service
values( 8 , 1 ),
(8,2),
(8,3)
(8,8),
(9,3),
(9,2),
(9,4),
(9,5),
(9,9)
;
insert into  planning_visible (idservice,idvisible)
values( 8 , 1 ),
(8,2),
(8,3),
(8,8),
(9,3),
(9,2),
(9,4),
(9,5),
(9,9)
;


update  personnel_poste 
set idposte = 1
where idpersonnel = 4;

update  personnel_poste 
set idposte = 79
where idpersonnel = 6;

update  personnel_poste 
set idposte = 86
where idpersonnel = 7;


update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 1 
         )
where idservice = 1;

update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 2 
         )
where idservice = 2;

update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 3 
         )
where idservice = 3;

update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 4 
         )
where idservice = 4;

update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 5 
         )
where idservice = 5;

update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 6 
         )
where idservice = 6;

update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 7 
         )
where idservice = 7;

update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 8 
         )
where idservice = 8;

update service set superieur = ( select max(idpersonnel)
        from v_personnel_information
        where idservice = 9 
         )
where idservice = 9;



insert into conge (idpersonnel,datedebut,datefin,reeldatefin,accepte,idraison,autre_raison)
values 
    (19 , 1 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00',-2 , null , 'Vacances entre famille'),         
    (21 , 1 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 1 ,1 , 'Vacances entre famille'), 
    (23 , 2 ,'2023-10-04 08:00:00','2023-10-06 08:00:00','2023-10-06 08:00:00', 2 ,2 , 'Vacances entre famille'),             
    (24 , 2 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 1 ,1 , 'Vacances entre famille'),
    (25 , 3 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00', 2 ,1 , 'Visite chez ma grand-mère'),
    (27 , 3 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 2 ,1 , 'Vacances entre famille'),
    (28 , 4 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00', 0 , null , 'Visite chez ma grand-mère'),
    (30 , 4 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00',-2 , null , 'Vacances entre famille'),
    (32 , 5 ,'2023-10-04 08:00:00','2023-10-06 08:00:00','2023-10-06 08:00:00', 1 ,1 , null ),
    (33 , 5 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 2 , null , 'Visite chez ma grand-mère'),
    (35 , 6 ,'2023-10-04 08:00:00','2023-10-06 08:00:00','2023-10-06 08:00:00',-2 ,1 , null ),
    (36 , 6 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 1 ,3 , null ),
    (37 , 7 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00', 1 ,1 , 'Visite chez ma grand-mère'),
    (38 , 7 ,'2023-10-04 08:00:00','2023-10-06 08:00:00','2023-10-06 08:00:00',-2 ,1 , null ),
    (40 , 8 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00', 0 ,2 , null ),
    (42 , 8 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 0 , null , 'Visite chez ma grand-mère'),
    (43 , 9 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00', 2 , null , null ),
    (46 ,10 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00', 2 , null , null ),
    (48 ,10 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 2 , null , 'Vacances entre famille'),
    (49 ,11 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00', 2 , null , null ),
    (50 ,11 ,'2023-10-04 08:00:00','2023-10-06 08:00:00','2023-10-06 08:00:00',-1 ,2 , 'Vacances entre famille'),
    (52 ,12 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00',-2 ,3 , 'Vacances entre famille'),
    (53 ,12 ,'2023-10-04 08:00:00','2023-10-06 08:00:00','2023-10-06 08:00:00',-1 , null , null ),
    (54 ,12 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 0 ,2 , 'Visite chez ma grand-mère'),
    (55 ,13 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00',-2 , null , 'Visite chez ma grand-mère'),
    (57 ,13 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00', 1 ,2 , 'Visite chez ma grand-mère'),
    (59 ,14 ,'2023-10-04 08:00:00','2023-10-06 08:00:00','2023-10-06 08:00:00', 1 , null , 'Visite chez ma grand-mère'),
    (60 ,14 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00',-1 , null , 'Visite chez ma grand-mère'),
    (61 ,15 ,'2023-10-01 08:00:00','2023-10-03 08:00:00','2023-10-03 08:00:00',-1 ,3 , 'Vacances entre famille'),
    (63 ,15 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00',-2 ,3 , null ),
    (65 ,16 ,'2023-10-04 08:00:00','2023-10-06 08:00:00','2023-10-06 08:00:00',-2 ,3 , 'Visite chez ma grand-mère'),
    (66 ,16 ,'2023-10-07 08:00:00','2023-10-09 08:00:00','2023-10-09 08:00:00',-1 ,3 , null );


update personnel 
set  nationalite = 4 
where nationalite = 2;

update personnel 
set  nationalite = 3 
where nationalite !=  4;

update personnel 
set  nationalite = 3
where idpersonnel = ;


update personnel 
set matricule = '48235'
where idpersonnel = 13;

update personnel 
set matricule = '10853'
where idpersonnel = 14;

update personnel 
set matricule = '37651'
where idpersonnel = 15;

update personnel 
set dtn = '1972-01-02'
where idpersonnel = 16;


INSERT INTO personnel_embauche (idpersonnel, date_embauche)
values( 16 , '2010-10-23' );

insert into personnel_salaire ( idpersonnel , salaire_base )
values( 16 , 10023045.00 );
