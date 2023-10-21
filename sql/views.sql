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
        natural join v_poste_besoin as pb ;

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

create or replace view v_personnel_poste_association as 
    select * 
    from personnel_poste as pp 
        natural join poste as p;


create or replace view v_personnel_poste as 
    select * 
    from v_personnel_poste_association as ppa 
        natural join personnel;

create or replace view v_personnel_information as 
    SELECT
        vpp.*,
        ((get_latest_salary(vpp.idpersonnel))).salaire_brut AS latest_salary_brut,
        ((get_latest_salary(vpp.idpersonnel))).salaire_net AS latest_salary_net,
        ((get_latest_salary(vpp.idpersonnel))).date_insert AS latest_salary_date,
        get_latest_hire_date(vpp.idpersonnel) AS latest_hire_date,
        extract ( year from age( now() , vpp.dtn )  ) as age
    FROM
        v_personnel_poste AS vpp;


create or replace view v_nbjours_personnel as  
    SELECT
    CASE
        WHEN EXTRACT(day FROM (now() - latest_hire_date)) > 90 THEN 90
        ELSE EXTRACT(day FROM (now() - latest_hire_date))
    END AS nbjours,
    idpersonnel
    FROM v_personnel_information;


create or replace view v_diff_conge as 
    select sum( extract (day from  (reeldatefin - datedebut)) ) as nbconge , idpersonnel
    from conge 
    where accepte = 3 
    and idraison is null
    group by idpersonnel;

create or replace view v_all_diff_conge as 
    SELECT p.idpersonnel,COALESCE(dc.nbconge, 0) AS nbconge
    FROM personnel p
    LEFT JOIN v_diff_conge dc ON p.idpersonnel = dc.idpersonnel;


create or replace view v_nbj_conge_personnel as 
    SELECT
        nbj.idpersonnel,
        (nbj.nbjours - dc.nbconge) AS difference
    FROM v_nbjours_personnel nbj
    LEFT JOIN v_all_diff_conge dc ON nbj.idpersonnel = dc.idpersonnel;

create or replace view v_nbheure_conge_personnel as 
    select difference * 0.67 as nbheure , idpersonnel
        from v_nbj_conge_personnel;


-- ralph
create or replace view v_conge_service as 
    SELECT *
    from v_personnel_poste_association 
        natural join conge;

create or replace view v_points_entretien as 
    select note_entretien.idcandidature,sum(note*coeff) as points 
    from note_entretien
    join question_entretien
        on question_entretien.idquestion_entretien = note_entretien.idquestion_entretien
    join candidature
        on candidature.idcandidature = note_entretien.idcandidature
    group by note_entretien.idcandidature;

create or replace view v_points_entretien_candidat as 
    select
    v_candidat_candidature.*, v_points_entretien.points
    from v_points_entretien
    join v_candidat_candidature
        on v_candidat_candidature.idcandidature = v_points_entretien.idcandidature;


-- vaovao 
create or replace view v_conge_refus as 
    select cs.idconge , idpersonnel , idposte , date_embauche , cs.idservice , nomposte , datedebut , datefin , reeldatefin , accepte , cs.idraison , ra.nomraison  , idrefus , raison_refus  , r.idservice as superieur 
    from v_conge_service as cs 
        left  join refus as r
        on cs.idconge = r.idconge 
        left join raison as ra 
        on ra.idraison = cs.idraison
        ;