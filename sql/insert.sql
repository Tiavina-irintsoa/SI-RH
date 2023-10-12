
-- ito ny vaovao
-- Insertion dans la table typecritere
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



insert into typeuser( description )
values( 'directeur' ),
('finance'),
('informatique')
;

insert into useradmin ( nom , mdp , idtypeuser )
values( 'directeur' , 'directeur' , 1 ),
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


