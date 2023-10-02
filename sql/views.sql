create view v_poste_besoin as 
    select * , ceil(b.heuresemaine / b.heurepersonne ) as nb_personne
    from poste as p 
    natural join besoin as b;
-- ito ny vaovao
create view v_criter_choix as 
    select *
    from typecritere as t
        natural join choix;

create view v_annonce as(
    select
    besoin.idbesoin, besoin.nb_personne, service.nomService, service.iconeService, poste.nomPoste
    from 
    v_poste_besoin as besoin 
    join poste 
        on besoin.idPoste = poste.idPoste
    join service on service.idService = poste.idService
);

-- vaovao ralph
create view v_choix_type as 
    select * 
    from choix as c 
        natural join typecritere as t;

create view v_critere_choix_type 
    select * 
    from v_choix_type as vct 
        natural join criterechoix  as cc ; 

create view v_critere_poste_besoin as 
    select *
    from v_poste_besoin as pb 
        natural join critere as c;

create view v_critere_details as 
    select * 
    from v_critere_choix_type as ct 
        natural join v_critere_poste_besoin as c ;

create view v_critere_service as 
    select * 
    from service as s 
        natural join v_critere_details as cd;