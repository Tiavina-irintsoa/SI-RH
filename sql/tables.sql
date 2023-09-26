create database rh ; 
\c rh
create table service(
    idService serial primary key, 
    nomService varchar, 
    iconeService varchar
);