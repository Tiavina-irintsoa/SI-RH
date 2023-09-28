create view v_poste_besoin as 
    select * , ceil(b.heuresemaine / b.heurepersonne ) as nb_personne
    from poste as p 
    natural join besoin as b;
-- ito ny vaovao
create view v_criter_choix as 
    select *
    from typecritere as t
        natural join choix;

------vaovao 
create view v_annonce as(
    select
    besoin.idbesoin, besoin.nb_personne, service.nomService, service.iconeService, poste.nomPoste
    from 
    v_poste_besoin as besoin 
    join poste 
        on besoin.idPoste = poste.idPoste
    join service on service.idService = poste.idService
);
