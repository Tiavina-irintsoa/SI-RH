create or replace view v_conge_service as 
    SELECT *
    from v_personnel_poste_association 
        natural join conge;

