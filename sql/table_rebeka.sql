create or replace view v_essai_fin as(
    SELECT *
    FROM contrat_essai
    NATURAL JOIN essai
    NATURAL JOIN candidat
    WHERE (debut::date + (duree || ' month')::interval) <= current_date
    AND idcontrat_essai NOT IN (SELECT idcontrat_essai FROM travail)
);

select * from v_essai_fin;