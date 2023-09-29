insert into service (nomService, iconeService) values
('Achats', 'achats.png'),
('Finance', 'finance.png'),
('Informatique', 'IT.png'),
('Logistique', 'logistique.png'),
('Maintenance et Reparation', 'maintenance.png'),
('Production industrielle', 'production.png'),
('Recherche et developpement', 'recherche.png'),
('Ressources humaines', 'rh.png'),

('Maintenance et Reparation', 'maintenance.png'),
('Marketing', 'marketing.png')
;

-- Insertion de données dans la table "poste"
INSERT INTO poste (idservice, nomposte)

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




-- ito ny vaovao
-- Insertion dans la table typecritere
delete from choix;
delete from typecritere;


alter sequence typecritere_idtypecritere_seq  restart with 1;
alter sequence choix_idchoix_seq restart with 1;

INSERT INTO typecritere (nomtypecritere)
VALUES
    ('genre'),
    ('nationalite'),
    ('diplome'),
    ('experience'),
    ('situation matrimoniale')
    ;

-- Insertion dans la table choix
INSERT INTO choix (idtypecritere, intitulechoix)
VALUES
    (1, 'homme'),
    (1, 'femme'),
    (2, 'Malagasy'),
    (2, 'Etranger'),
    (3, 'CEPE'),
    (3, 'BEPC'),
    (3, 'BACC'),
    (3, 'Licence'),
    (3, 'Master'),    
    (3, 'Doctorat'),
    (3, 'Master'), 
    (4, 'moins de 2 ans' ) , (4, 'entre 2 a 4 ans' ) , (4, 'entre 4 a 7 ans' ), (4, '7ans et plus' ),
    (5, 'marie(e)' ),           
    (5, 'divorce(e)' ),
    (5, 'en couple' )
    ;

-- Insertion de données dans la table "besoin"
INSERT INTO besoin (idposte, heurepersonne , heuresemaine , accompli)
VALUES
    (1, 40.0, 100.0, now()),
    (1, 35.0, 90.0, now()),
    (2, 38.0, 110.0, null),
    (3, 30.0, 80.0, null);

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