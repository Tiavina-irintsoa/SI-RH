CREATE OR REPLACE FUNCTION get_latest_salary(idpersonnel_param int)
RETURNS TABLE (salaire_brut numeric, salaire_net numeric, date_insert date) AS $$
BEGIN
    RETURN QUERY
    SELECT COALESCE(personnel_salaire.salaire_brut , 0), COALESCE(personnel_salaire.salaire_net , 0), personnel_salaire.date_insert
    FROM personnel_salaire
    WHERE idpersonnel = idpersonnel_param
    ORDER BY date_insert DESC
    LIMIT 1;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_latest_hire_date(idpersonnel_param int)
RETURNS date AS $$
DECLARE
    latest_hire_date date;
BEGIN
    SELECT MAX(date_embauche)
    INTO latest_hire_date
    FROM personnel_embauche
    WHERE idpersonnel = idpersonnel_param;
    
    RETURN latest_hire_date;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION getService(idpersonnel_param int)
RETURNS int AS $$
DECLARE
    idservice int;
BEGIN
    execute 'select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = ' || idpersonnel_param
        into idservice;
    RETURN idservice;
END;
$$ LANGUAGE plpgsql;

