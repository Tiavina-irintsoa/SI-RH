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
('Marketing', 'marketing.png');

-- Insertion de données dans la table "poste"
INSERT INTO poste (idservice, nomposte)
VALUES
    (1, 'Poste 1'),
    (1, 'Poste 2'),
    (2, 'Poste 3'),
    (2, 'Poste 4');

-- Insertion de données dans la table "besoin"
INSERT INTO besoin (idposte, heurepersonne , heuresemaine , accompli)
VALUES
    (1, 40.0, 100.0, 1),
    (1, 35.0, 90.0, 1),
    (2, 38.0, 110.0, 1),
    (3, 30.0, 80.0, 1);


-- ito ny vaovao
-- Insertion dans la table typecritere
INSERT INTO typecritere (nomtypecritere)
VALUES
    ('Type 1'),
    ('Type 2'),
    ('Type 3');

-- Insertion dans la table choix
INSERT INTO choix (idtypecritere, intitulechoix)
VALUES
    (1, 'Choix 1A'),
    (1, 'Choix 1B'),
    (1, 'Choix 1C'),
    (2, 'Choix 2A'),
    (2, 'Choix 2B'),
    (3, 'Choix 3A'),
    (3, 'Choix 3B');









