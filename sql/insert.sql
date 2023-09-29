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

-- Insertion de données dans la table "poste"
INSERT INTO poste (idservice, nomposte)
VALUES
    (1, 'Poste 1'),
    (1, 'Poste 2'),
    (2, 'Poste 3'),
    (2, 'Poste 4');




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
    (2, 'gasy'),
    (2, 'tsy gasy'),
    (3, 'CEPE'),
    (3, 'BEPEC'),
    (3, 'BACC'),
    (3, 'Licence'),
    (3, 'Master'),    
    (3, 'Doctorat'),
    (3, 'Master'), 
    (4, '1' ) , (4, '2' ) , (4, '3' ), (4, '4' ), (4, '5' ), (4, '6' ) , (4, '7 ou +' ),
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

---------tita
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

    


