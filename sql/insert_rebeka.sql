insert into avantage (nomavantage) values
    ('Voiture'), ('Maison'), ('Secretaire');

insert into sante (nomsante) values
    ('Funhece'), ('AMIT'), ('Ostie');

insert into info (idcandidat, cin, adresse, pere, mere, nbenfant) values (@idcandidat, @cin, @adresse, @pere, @mere, @nbenfant)

insert into contrat_essai (idessai, net, signessai) values (@idessai, @net, @signessai)

insert into travail (idcontrat_essai, duree, debut) values (@idcontrat_essai, @duree, @debut)

insert into contrat_travail (idtravail, signetravail) values (@idtravail, @signetravail)

insert into travail_sante values (@idcontrat_travail, @idsante)