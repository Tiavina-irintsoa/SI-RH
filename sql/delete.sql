delete from criterechoix;
delete from critere;
delete from besoin;
delete from choix;
delete from typecritere;
delete from poste;
delete from service;

alter sequence besoin_idbesoin_seq           restart with 1;
alter sequence choix_idchoix_seq             restart with 1;
alter sequence critere_idcritere_seq         restart with 1;
alter sequence poste_idposte_seq             restart with 1;
alter sequence service_idservice_seq         restart with 1;
alter sequence typecritere_idtypecritere_seq restart with 1;