delete from criterechoix;
delete from critere;
delete from besoin;
delete from poste;
delete from type_contrat;
delete from admin_service;
delete from useradmin;
delete from typeuser;
delete from fichier;
delete from choixcandidature;
delete from candidature;
delete from candidat;
delete from choix;
delete from typecritere;
delete from service;


alter sequence besoin_idbesoin_seq           restart with 1;
alter sequence choix_idchoix_seq             restart with 1;
alter sequence critere_idcritere_seq         restart with 1;
alter sequence poste_idposte_seq             restart with 1;
alter sequence service_idservice_seq         restart with 1;
alter sequence typecritere_idtypecritere_seq restart with 1;
alter sequence type_contrat_idtypecontrat_seq restart with 1;
alter sequence typeuser_idtypeuser_seq restart with 1;
alter sequence useradmin_idadmin_seq restart with 1;
alter sequence typeuser_idtypeuser_seq restart with 1;
alter sequence candidature_idcanditature_seq restart with 1;
alter sequence candidat_idcandidat_seq restart with 1;

DO $$ 
DECLARE 
    view_name text;
BEGIN
    FOR view_name IN (SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'VIEW') 
    LOOP
        EXECUTE 'DROP VIEW IF EXISTS ' || view_name || ' CASCADE';
    END LOOP;
END $$;