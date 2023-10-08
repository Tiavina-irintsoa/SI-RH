create or replace view v_poste_besoin as 
    select * , ceil(b.heuresemaine / b.heurepersonne ) as nb_personne
    from poste as p 
    natural join besoin as b;
-- ito ny vaovao
create or replace view v_criter_choix as 
    select *
    from typecritere as t
        natural join choix;

create or replace view v_choix_type as 
    select * 
    from choix as c 
        natural join typecritere as t;

create or replace view v_critere_choix_type as
    select * 
    from v_choix_type as vct 
        natural join criterechoix  as cc ; 

create or replace view v_critere_poste_besoin as 
    select *
    from v_poste_besoin as pb 
        natural join critere as c;

create or replace view v_critere_details as 
    select * 
    from v_critere_choix_type as ct 
        natural join v_critere_poste_besoin as c ;

create or replace view v_critere_service as 
    select * 
    from service as s 
        natural join v_critere_details as cd;



create or replace view v_choix_candidature as 
    select * 
    from candidature as c 
        natural join choixcandidature as cd;

create or replace view v_choix_candidature_type as 
    select * 
    from v_choix_candidature as vc 
        natural join v_choix_type as ct;

create or replace view v_candidat_candidature as
    select * from candidat as c 
        natural join candidature as cd
        join v_poste_besoin as pb ;

-- vaovao ralph
create or replace view v_all_annonce as(
    select
    besoin.idbesoin, besoin.nb_personne, service.nomService, service.iconeService, poste.nomPoste
    from 
    v_poste_besoin as besoin 
    join poste 
        on besoin.idPoste = poste.idPoste
    join service on service.idService = poste.idService
);

create or replace view v_annonce as(
    select
    besoin.idbesoin, besoin.nb_personne, service.nomService, service.iconeService, poste.nomPoste
    from 
    v_poste_besoin as besoin 
    join poste 
        on besoin.idPoste = poste.idPoste
    join service on service.idService = poste.idService
    where accompli is null
);

create or replace view v_admin_typeuser as 
    select * 
    from typeuser as t 
        natural join useradmin as a;

create or replace view v_assiociation_admin_service as 
    select * 
    from admin_service as a 
        natural join service as  s ;

create or replace view v_admin_service as 
    select * 
    from v_admin_typeuser 
        natural join v_assiociation_admin_service;
