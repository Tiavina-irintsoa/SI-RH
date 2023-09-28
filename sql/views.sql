create view v_poste_besoin as 
    select * , ceil(b.heuresemaine / b.heurepersonne ) as nb_personne
    from poste as p 
    natural join besoin as b;
-- ito ny vaovao
create view v_criter_choix as 
    select *
    from typecritere as t
        natural join choix;