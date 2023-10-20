
alter table service
add column superieur integer REFERENCES personnel(idpersonnel);

update service set superieur = 6;