delete from criterechoix;
delete from critere;
delete from besoin;
delete from personnel_poste;
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
delete from fichier;
delete from choixcandidature;
delete from candidature;
delete from candidat;
delete from option; 
delete from question; 
delete from questionnaire; 
delete from conge;
delete from raison;
delete from note_entretien;
delete from question_entretien;

-- Réinitialisation individuelle de chaque séquence à 1
ALTER SEQUENCE besoin_idbesoin_seq RESTART WITH 1;
ALTER SEQUENCE candidat_idcandidat_seq RESTART WITH 1;
ALTER SEQUENCE candidature_idcanditature_seq RESTART WITH 1;
ALTER SEQUENCE choix_idchoix_seq RESTART WITH 1;
ALTER SEQUENCE conge_idconge_seq RESTART WITH 1;
ALTER SEQUENCE critere_idcritere_seq RESTART WITH 1;
ALTER SEQUENCE option_idoption_seq RESTART WITH 1;
ALTER SEQUENCE personnel_embauche_idpersonnel_embauche_seq RESTART WITH 1;
ALTER SEQUENCE personnel_idpersonnel_seq RESTART WITH 1;
ALTER SEQUENCE personnel_salaire_idpersonnel_salaire_seq RESTART WITH 1;
ALTER SEQUENCE poste_idposte_seq RESTART WITH 1;
ALTER SEQUENCE question_entretien_idquestion_entretien_seq RESTART WITH 1;
ALTER SEQUENCE question_idquestion_seq RESTART WITH 1;
ALTER SEQUENCE questionnaire_idquestionnaire_seq RESTART WITH 1;
ALTER SEQUENCE raison_idraison_seq RESTART WITH 1;
ALTER SEQUENCE service_idservice_seq RESTART WITH 1;
ALTER SEQUENCE type_contrat_idtypecontrat_seq RESTART WITH 1;
ALTER SEQUENCE typecritere_idtypecritere_seq RESTART WITH 1;
ALTER SEQUENCE typeuser_idtypeuser_seq RESTART WITH 1;
ALTER SEQUENCE useradmin_idadmin_seq RESTART WITH 1;

-- DO $$ 
-- DECLARE 
--     view_name text;
-- BEGIN
--     FOR view_name IN (SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'VIEW') 
--     LOOP
--         EXECUTE 'DROP VIEW IF EXISTS ' || view_name || ' CASCADE';
--     END LOOP;
-- END $$;