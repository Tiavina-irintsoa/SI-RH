select * from v_personnel_information where  ( nom like '%john%' or prenom like '%john%'  )  and (travailleur is not null and extract( year from latest_hire_date ) <=  2022)

select * 
from v_personnel_information
where travailleur is not null and extract( year from latest_hire_date ) <=  2022;

select * 
from v_personnel_information where   UPPER(nom) like UPPER('%john%') or upper(prenom) like UPPER('%john%')  ;

select * 
from v_personnel_information where  ( (nom) like ('%John%') or prenom like ('%John%')  );


select upper(prenom) , upper('%john%')
from v_personnel_information;