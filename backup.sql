--
-- PostgreSQL database dump
--

-- Dumped from database version 10.22
-- Dumped by pg_dump version 10.22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: get_latest_hire_date(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_latest_hire_date(idpersonnel_param integer) RETURNS date
    LANGUAGE plpgsql
    AS $$
DECLARE
    latest_hire_date date;
BEGIN
    SELECT MAX(date_embauche)
    INTO latest_hire_date
    FROM personnel_embauche
    WHERE idpersonnel = idpersonnel_param;
    
    RETURN latest_hire_date;
END;
$$;


ALTER FUNCTION public.get_latest_hire_date(idpersonnel_param integer) OWNER TO postgres;

--
-- Name: get_latest_salary(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_latest_salary(idpersonnel_param integer) RETURNS TABLE(salaire_brut numeric, salaire_net numeric, date_insert date)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT COALESCE(personnel_salaire.salaire_base , 0), COALESCE(personnel_salaire.salaire_net , 0), personnel_salaire.date_insert
    FROM personnel_salaire
    WHERE idpersonnel = idpersonnel_param
    ORDER BY date_insert DESC
    LIMIT 1;
END;
$$;


ALTER FUNCTION public.get_latest_salary(idpersonnel_param integer) OWNER TO postgres;

--
-- Name: getserivce(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.getserivce(idpersonnel_param integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    idservice int;
BEGIN
    execute 'select idservice
        from v_service_poste
        natural join personnel_poste 
        where idpersonnel = '|| idpersonnel_param
        as  idservice;
    RETURN idservice;
END;
$$;


ALTER FUNCTION public.getserivce(idpersonnel_param integer) OWNER TO postgres;

--
-- Name: getservice(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.getservice(idpersonnel_param integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$
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
$$;


ALTER FUNCTION public.getservice(idpersonnel_param integer) OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: admin_service; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.admin_service (
    idtypeuser integer,
    idservice integer
);


ALTER TABLE public.admin_service OWNER TO postgres;

--
-- Name: avantage; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.avantage (
    idavantage integer NOT NULL,
    nomavantage character varying
);


ALTER TABLE public.avantage OWNER TO postgres;

--
-- Name: avantage_idavantage_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.avantage_idavantage_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.avantage_idavantage_seq OWNER TO postgres;

--
-- Name: avantage_idavantage_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.avantage_idavantage_seq OWNED BY public.avantage.idavantage;


--
-- Name: besoin; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.besoin (
    idbesoin integer NOT NULL,
    idposte integer,
    heuresemaine numeric,
    heurepersonne numeric,
    accompli date,
    idtypecontrat integer DEFAULT 1
);


ALTER TABLE public.besoin OWNER TO postgres;

--
-- Name: besoin_idbesoin_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.besoin_idbesoin_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.besoin_idbesoin_seq OWNER TO postgres;

--
-- Name: besoin_idbesoin_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.besoin_idbesoin_seq OWNED BY public.besoin.idbesoin;


--
-- Name: candidat; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.candidat (
    idcandidat integer NOT NULL,
    nomcandidat character varying,
    prenomcandidat character varying,
    dtn date,
    mail character varying,
    contact character varying
);


ALTER TABLE public.candidat OWNER TO postgres;

--
-- Name: candidat_idcandidat_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.candidat_idcandidat_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.candidat_idcandidat_seq OWNER TO postgres;

--
-- Name: candidat_idcandidat_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.candidat_idcandidat_seq OWNED BY public.candidat.idcandidat;


--
-- Name: candidature; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.candidature (
    idcandidature integer NOT NULL,
    idcandidat integer,
    datecandidature timestamp without time zone DEFAULT now(),
    validation integer DEFAULT 0,
    code character varying,
    idbesoin integer
);


ALTER TABLE public.candidature OWNER TO postgres;

--
-- Name: candidature_idcanditature_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.candidature_idcanditature_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.candidature_idcanditature_seq OWNER TO postgres;

--
-- Name: candidature_idcanditature_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.candidature_idcanditature_seq OWNED BY public.candidature.idcandidature;


--
-- Name: choix; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.choix (
    idchoix integer NOT NULL,
    idtypecritere integer,
    intitulechoix character varying,
    valeurchoix integer
);


ALTER TABLE public.choix OWNER TO postgres;

--
-- Name: choix_idchoix_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.choix_idchoix_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.choix_idchoix_seq OWNER TO postgres;

--
-- Name: choix_idchoix_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.choix_idchoix_seq OWNED BY public.choix.idchoix;


--
-- Name: choixcandidature; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.choixcandidature (
    idcandidature integer,
    idchoix integer
);


ALTER TABLE public.choixcandidature OWNER TO postgres;

--
-- Name: conge; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.conge (
    idconge integer NOT NULL,
    idpersonnel integer,
    datedebut timestamp without time zone,
    datefin timestamp without time zone,
    reeldatefin timestamp without time zone,
    accepte integer DEFAULT 1,
    idraison integer,
    autre_raison character varying
);


ALTER TABLE public.conge OWNER TO postgres;

--
-- Name: conge_idconge_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.conge_idconge_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.conge_idconge_seq OWNER TO postgres;

--
-- Name: conge_idconge_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.conge_idconge_seq OWNED BY public.conge.idconge;


--
-- Name: contrat_essai; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.contrat_essai (
    idcontrat_essai integer NOT NULL,
    idessai integer,
    signessai date
);


ALTER TABLE public.contrat_essai OWNER TO postgres;

--
-- Name: contrat_essai_idcontrat_essai_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.contrat_essai_idcontrat_essai_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.contrat_essai_idcontrat_essai_seq OWNER TO postgres;

--
-- Name: contrat_essai_idcontrat_essai_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.contrat_essai_idcontrat_essai_seq OWNED BY public.contrat_essai.idcontrat_essai;


--
-- Name: contrat_travail; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.contrat_travail (
    idcontrat_travail integer NOT NULL,
    idtravail integer,
    signetravail date
);


ALTER TABLE public.contrat_travail OWNER TO postgres;

--
-- Name: contrat_travail_idcontrat_travail_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.contrat_travail_idcontrat_travail_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.contrat_travail_idcontrat_travail_seq OWNER TO postgres;

--
-- Name: contrat_travail_idcontrat_travail_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.contrat_travail_idcontrat_travail_seq OWNED BY public.contrat_travail.idcontrat_travail;


--
-- Name: critere; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.critere (
    idcritere integer NOT NULL,
    idbesoin integer,
    idtypecritere integer,
    coefficient numeric
);


ALTER TABLE public.critere OWNER TO postgres;

--
-- Name: critere_idcritere_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.critere_idcritere_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.critere_idcritere_seq OWNER TO postgres;

--
-- Name: critere_idcritere_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.critere_idcritere_seq OWNED BY public.critere.idcritere;


--
-- Name: criterechoix; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.criterechoix (
    idcritere integer,
    idchoix integer
);


ALTER TABLE public.criterechoix OWNER TO postgres;

--
-- Name: essai; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.essai (
    idessai integer NOT NULL,
    idbesoin integer,
    idcandidat integer,
    duree real,
    debut date,
    salaire_base real
);


ALTER TABLE public.essai OWNER TO postgres;

--
-- Name: essai_avantage; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.essai_avantage (
    idessai integer,
    idavantage integer
);


ALTER TABLE public.essai_avantage OWNER TO postgres;

--
-- Name: essai_idessai_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.essai_idessai_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.essai_idessai_seq OWNER TO postgres;

--
-- Name: essai_idessai_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.essai_idessai_seq OWNED BY public.essai.idessai;


--
-- Name: fichier; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.fichier (
    idcandidature integer,
    lienfichierdiplome character varying,
    lienfichierexperience character varying
);


ALTER TABLE public.fichier OWNER TO postgres;

--
-- Name: info; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.info (
    idinfo integer NOT NULL,
    idcandidat integer,
    cin character varying,
    adresse character varying,
    pere character varying,
    mere character varying,
    nbenfant integer
);


ALTER TABLE public.info OWNER TO postgres;

--
-- Name: info_idinfo_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.info_idinfo_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.info_idinfo_seq OWNER TO postgres;

--
-- Name: info_idinfo_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.info_idinfo_seq OWNED BY public.info.idinfo;


--
-- Name: note_entretien; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.note_entretien (
    idquestion_entretien integer,
    idcandidature integer,
    note numeric
);


ALTER TABLE public.note_entretien OWNER TO postgres;

--
-- Name: option; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.option (
    idoption integer NOT NULL,
    idquestion integer,
    option character varying,
    points integer
);


ALTER TABLE public.option OWNER TO postgres;

--
-- Name: option_idoption_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.option_idoption_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.option_idoption_seq OWNER TO postgres;

--
-- Name: option_idoption_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.option_idoption_seq OWNED BY public.option.idoption;


--
-- Name: personnel; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.personnel (
    idpersonnel integer NOT NULL,
    nom character varying,
    prenom character varying,
    mail character varying,
    matricule character varying,
    nationalite integer,
    adresse text,
    genre integer,
    travailleur integer,
    dtn date,
    contact character varying
);


ALTER TABLE public.personnel OWNER TO postgres;

--
-- Name: personnel_embauche; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.personnel_embauche (
    idpersonnel_embauche integer NOT NULL,
    idpersonnel integer,
    date_embauche date
);


ALTER TABLE public.personnel_embauche OWNER TO postgres;

--
-- Name: personnel_embauche_idpersonnel_embauche_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.personnel_embauche_idpersonnel_embauche_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.personnel_embauche_idpersonnel_embauche_seq OWNER TO postgres;

--
-- Name: personnel_embauche_idpersonnel_embauche_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.personnel_embauche_idpersonnel_embauche_seq OWNED BY public.personnel_embauche.idpersonnel_embauche;


--
-- Name: personnel_idpersonnel_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.personnel_idpersonnel_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.personnel_idpersonnel_seq OWNER TO postgres;

--
-- Name: personnel_idpersonnel_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.personnel_idpersonnel_seq OWNED BY public.personnel.idpersonnel;


--
-- Name: personnel_poste; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.personnel_poste (
    idposte integer,
    idpersonnel integer,
    date_embauche date
);


ALTER TABLE public.personnel_poste OWNER TO postgres;

--
-- Name: personnel_salaire; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.personnel_salaire (
    idpersonnel_salaire integer NOT NULL,
    idpersonnel integer,
    salaire_base numeric,
    salaire_net numeric,
    date_insert date DEFAULT now()
);


ALTER TABLE public.personnel_salaire OWNER TO postgres;

--
-- Name: personnel_salaire_idpersonnel_salaire_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.personnel_salaire_idpersonnel_salaire_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.personnel_salaire_idpersonnel_salaire_seq OWNER TO postgres;

--
-- Name: personnel_salaire_idpersonnel_salaire_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.personnel_salaire_idpersonnel_salaire_seq OWNED BY public.personnel_salaire.idpersonnel_salaire;


--
-- Name: planning_visible; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.planning_visible (
    idpv integer NOT NULL,
    idservice integer,
    idvisible integer
);


ALTER TABLE public.planning_visible OWNER TO postgres;

--
-- Name: planning_visible_idpv_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.planning_visible_idpv_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.planning_visible_idpv_seq OWNER TO postgres;

--
-- Name: planning_visible_idpv_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.planning_visible_idpv_seq OWNED BY public.planning_visible.idpv;


--
-- Name: poste; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.poste (
    idposte integer NOT NULL,
    idservice integer,
    nomposte character varying
);


ALTER TABLE public.poste OWNER TO postgres;

--
-- Name: poste_idposte_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.poste_idposte_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.poste_idposte_seq OWNER TO postgres;

--
-- Name: poste_idposte_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.poste_idposte_seq OWNED BY public.poste.idposte;


--
-- Name: question; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.question (
    idquestion integer NOT NULL,
    idquestionnaire integer,
    question character varying,
    points integer
);


ALTER TABLE public.question OWNER TO postgres;

--
-- Name: question_entretien; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.question_entretien (
    idquestion_entretien integer NOT NULL,
    question character varying,
    coeff numeric,
    idbesoin integer
);


ALTER TABLE public.question_entretien OWNER TO postgres;

--
-- Name: question_entretien_idquestion_entretien_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.question_entretien_idquestion_entretien_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.question_entretien_idquestion_entretien_seq OWNER TO postgres;

--
-- Name: question_entretien_idquestion_entretien_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.question_entretien_idquestion_entretien_seq OWNED BY public.question_entretien.idquestion_entretien;


--
-- Name: question_idquestion_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.question_idquestion_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.question_idquestion_seq OWNER TO postgres;

--
-- Name: question_idquestion_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.question_idquestion_seq OWNED BY public.question.idquestion;


--
-- Name: questionnaire; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.questionnaire (
    idquestionnaire integer NOT NULL,
    idbesoin integer
);


ALTER TABLE public.questionnaire OWNER TO postgres;

--
-- Name: questionnaire_idquestionnaire_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.questionnaire_idquestionnaire_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.questionnaire_idquestionnaire_seq OWNER TO postgres;

--
-- Name: questionnaire_idquestionnaire_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.questionnaire_idquestionnaire_seq OWNED BY public.questionnaire.idquestionnaire;


--
-- Name: raison; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.raison (
    idraison integer NOT NULL,
    nomraison character varying
);


ALTER TABLE public.raison OWNER TO postgres;

--
-- Name: raison_idraison_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.raison_idraison_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.raison_idraison_seq OWNER TO postgres;

--
-- Name: raison_idraison_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.raison_idraison_seq OWNED BY public.raison.idraison;


--
-- Name: refus; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.refus (
    idrefus integer NOT NULL,
    idconge integer,
    raison_refus text,
    idservice integer
);


ALTER TABLE public.refus OWNER TO postgres;

--
-- Name: refus_idrefus_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.refus_idrefus_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.refus_idrefus_seq OWNER TO postgres;

--
-- Name: refus_idrefus_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.refus_idrefus_seq OWNED BY public.refus.idrefus;


--
-- Name: sante; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sante (
    idsante integer NOT NULL,
    nomsante character varying
);


ALTER TABLE public.sante OWNER TO postgres;

--
-- Name: sante_idsante_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.sante_idsante_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.sante_idsante_seq OWNER TO postgres;

--
-- Name: sante_idsante_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.sante_idsante_seq OWNED BY public.sante.idsante;


--
-- Name: service; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.service (
    idservice integer NOT NULL,
    nomservice character varying,
    iconeservice character varying,
    superieur integer
);


ALTER TABLE public.service OWNER TO postgres;

--
-- Name: service_idservice_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.service_idservice_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.service_idservice_seq OWNER TO postgres;

--
-- Name: service_idservice_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.service_idservice_seq OWNED BY public.service.idservice;


--
-- Name: travail; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.travail (
    idtravail integer NOT NULL,
    idcontrat_essai integer,
    duree real,
    debut date
);


ALTER TABLE public.travail OWNER TO postgres;

--
-- Name: travail_idtravail_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.travail_idtravail_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.travail_idtravail_seq OWNER TO postgres;

--
-- Name: travail_idtravail_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.travail_idtravail_seq OWNED BY public.travail.idtravail;


--
-- Name: travail_sante; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.travail_sante (
    idcontrat_travail integer,
    idsante integer
);


ALTER TABLE public.travail_sante OWNER TO postgres;

--
-- Name: type_contrat; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.type_contrat (
    idtypecontrat integer NOT NULL,
    nomtype_contrat character varying
);


ALTER TABLE public.type_contrat OWNER TO postgres;

--
-- Name: type_contrat_idtypecontrat_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.type_contrat_idtypecontrat_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.type_contrat_idtypecontrat_seq OWNER TO postgres;

--
-- Name: type_contrat_idtypecontrat_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.type_contrat_idtypecontrat_seq OWNED BY public.type_contrat.idtypecontrat;


--
-- Name: typecritere; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.typecritere (
    idtypecritere integer NOT NULL,
    nomtypecritere character varying
);


ALTER TABLE public.typecritere OWNER TO postgres;

--
-- Name: typecritere_idtypecritere_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.typecritere_idtypecritere_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.typecritere_idtypecritere_seq OWNER TO postgres;

--
-- Name: typecritere_idtypecritere_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.typecritere_idtypecritere_seq OWNED BY public.typecritere.idtypecritere;


--
-- Name: typeuser; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.typeuser (
    idtypeuser integer NOT NULL,
    description character varying
);


ALTER TABLE public.typeuser OWNER TO postgres;

--
-- Name: typeuser_idtypeuser_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.typeuser_idtypeuser_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.typeuser_idtypeuser_seq OWNER TO postgres;

--
-- Name: typeuser_idtypeuser_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.typeuser_idtypeuser_seq OWNED BY public.typeuser.idtypeuser;


--
-- Name: useradmin; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.useradmin (
    idadmin integer NOT NULL,
    nom character varying,
    mdp character varying,
    idtypeuser integer,
    idpersonnel integer
);


ALTER TABLE public.useradmin OWNER TO postgres;

--
-- Name: useradmin_idadmin_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.useradmin_idadmin_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.useradmin_idadmin_seq OWNER TO postgres;

--
-- Name: useradmin_idadmin_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.useradmin_idadmin_seq OWNED BY public.useradmin.idadmin;


--
-- Name: v_admin_typeuser; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_admin_typeuser AS
 SELECT t.idtypeuser,
    t.description,
    a.idadmin,
    a.nom,
    a.mdp,
    a.idpersonnel
   FROM (public.typeuser t
     JOIN public.useradmin a USING (idtypeuser));


ALTER TABLE public.v_admin_typeuser OWNER TO postgres;

--
-- Name: v_assiociation_admin_service; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_assiociation_admin_service AS
 SELECT a.idservice,
    a.idtypeuser,
    s.nomservice,
    s.iconeservice,
    s.superieur
   FROM (public.admin_service a
     JOIN public.service s USING (idservice));


ALTER TABLE public.v_assiociation_admin_service OWNER TO postgres;

--
-- Name: v_admin_service; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_admin_service AS
 SELECT v_admin_typeuser.idtypeuser,
    v_admin_typeuser.description,
    v_admin_typeuser.idadmin,
    v_admin_typeuser.nom,
    v_admin_typeuser.mdp,
    v_admin_typeuser.idpersonnel,
    v_assiociation_admin_service.idservice,
    v_assiociation_admin_service.nomservice,
    v_assiociation_admin_service.iconeservice,
    v_assiociation_admin_service.superieur
   FROM (public.v_admin_typeuser
     JOIN public.v_assiociation_admin_service USING (idtypeuser));


ALTER TABLE public.v_admin_service OWNER TO postgres;

--
-- Name: v_poste_besoin; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_poste_besoin AS
 SELECT p.idposte,
    p.idservice,
    p.nomposte,
    b.idbesoin,
    b.heuresemaine,
    b.heurepersonne,
    b.accompli,
    b.idtypecontrat,
    ceil((b.heuresemaine / b.heurepersonne)) AS nb_personne
   FROM (public.poste p
     JOIN public.besoin b USING (idposte));


ALTER TABLE public.v_poste_besoin OWNER TO postgres;

--
-- Name: v_all_annonce; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_all_annonce AS
 SELECT besoin.idbesoin,
    besoin.nb_personne,
    service.nomservice,
    service.iconeservice,
    poste.nomposte
   FROM ((public.v_poste_besoin besoin
     JOIN public.poste ON ((besoin.idposte = poste.idposte)))
     JOIN public.service ON ((service.idservice = poste.idservice)));


ALTER TABLE public.v_all_annonce OWNER TO postgres;

--
-- Name: v_diff_conge; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_diff_conge AS
 SELECT sum(date_part('day'::text, (conge.reeldatefin - conge.datedebut))) AS nbconge,
    conge.idpersonnel
   FROM public.conge
  WHERE ((conge.accepte = 3) AND (conge.idraison IS NULL))
  GROUP BY conge.idpersonnel;


ALTER TABLE public.v_diff_conge OWNER TO postgres;

--
-- Name: v_all_diff_conge; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_all_diff_conge AS
 SELECT p.idpersonnel,
    COALESCE(dc.nbconge, (0)::double precision) AS nbconge
   FROM (public.personnel p
     LEFT JOIN public.v_diff_conge dc ON ((p.idpersonnel = dc.idpersonnel)));


ALTER TABLE public.v_all_diff_conge OWNER TO postgres;

--
-- Name: v_annonce; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_annonce AS
 SELECT besoin.idbesoin,
    besoin.nb_personne,
    service.nomservice,
    service.iconeservice,
    poste.nomposte
   FROM ((public.v_poste_besoin besoin
     JOIN public.poste ON ((besoin.idposte = poste.idposte)))
     JOIN public.service ON ((service.idservice = poste.idservice)))
  WHERE (besoin.accompli IS NULL);


ALTER TABLE public.v_annonce OWNER TO postgres;

--
-- Name: v_besoin_accompli; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_besoin_accompli AS
 SELECT v_poste_besoin.idposte,
    v_poste_besoin.idservice,
    v_poste_besoin.nomposte,
    v_poste_besoin.idbesoin,
    v_poste_besoin.heuresemaine,
    v_poste_besoin.heurepersonne,
    v_poste_besoin.accompli,
    v_poste_besoin.idtypecontrat,
    v_poste_besoin.nb_personne
   FROM public.v_poste_besoin
  WHERE (v_poste_besoin.accompli IS NULL);


ALTER TABLE public.v_besoin_accompli OWNER TO postgres;

--
-- Name: v_candidat_candidature; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_candidat_candidature AS
 SELECT cd.idbesoin,
    c.idcandidat,
    c.nomcandidat,
    c.prenomcandidat,
    c.dtn,
    c.mail,
    c.contact,
    cd.idcandidature,
    cd.datecandidature,
    cd.validation,
    cd.code,
    pb.idposte,
    pb.idservice,
    pb.nomposte,
    pb.heuresemaine,
    pb.heurepersonne,
    pb.accompli,
    pb.idtypecontrat,
    pb.nb_personne
   FROM ((public.candidat c
     JOIN public.candidature cd USING (idcandidat))
     JOIN public.v_poste_besoin pb USING (idbesoin));


ALTER TABLE public.v_candidat_candidature OWNER TO postgres;

--
-- Name: v_candidat_entretien; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_candidat_entretien AS
 SELECT candidature.idcandidature,
    candidature.idcandidat,
    candidature.datecandidature,
    candidature.validation,
    candidature.code,
    candidature.idbesoin
   FROM public.candidature
  WHERE (candidature.validation = 2);


ALTER TABLE public.v_candidat_entretien OWNER TO postgres;

--
-- Name: v_choix_candidature; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_choix_candidature AS
 SELECT c.idcandidature,
    c.idcandidat,
    c.datecandidature,
    c.validation,
    c.code,
    c.idbesoin,
    cd.idchoix
   FROM (public.candidature c
     JOIN public.choixcandidature cd USING (idcandidature));


ALTER TABLE public.v_choix_candidature OWNER TO postgres;

--
-- Name: v_choix_type; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_choix_type AS
 SELECT c.idtypecritere,
    c.idchoix,
    c.intitulechoix,
    c.valeurchoix,
    t.nomtypecritere
   FROM (public.choix c
     JOIN public.typecritere t USING (idtypecritere));


ALTER TABLE public.v_choix_type OWNER TO postgres;

--
-- Name: v_choix_candidature_type; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_choix_candidature_type AS
 SELECT vc.idchoix,
    vc.idcandidature,
    vc.idcandidat,
    vc.datecandidature,
    vc.validation,
    vc.code,
    vc.idbesoin,
    ct.idtypecritere,
    ct.intitulechoix,
    ct.valeurchoix,
    ct.nomtypecritere
   FROM (public.v_choix_candidature vc
     JOIN public.v_choix_type ct USING (idchoix));


ALTER TABLE public.v_choix_candidature_type OWNER TO postgres;

--
-- Name: v_personnel_poste_association; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_personnel_poste_association AS
 SELECT pp.idposte,
    pp.idpersonnel,
    pp.date_embauche,
    p.idservice,
    p.nomposte
   FROM (public.personnel_poste pp
     JOIN public.poste p USING (idposte));


ALTER TABLE public.v_personnel_poste_association OWNER TO postgres;

--
-- Name: v_conge_service; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_conge_service AS
 SELECT v_personnel_poste_association.idpersonnel,
    v_personnel_poste_association.idposte,
    v_personnel_poste_association.date_embauche,
    v_personnel_poste_association.idservice,
    v_personnel_poste_association.nomposte,
    conge.idconge,
    conge.datedebut,
    conge.datefin,
    conge.reeldatefin,
    conge.accepte,
    conge.idraison,
    conge.autre_raison
   FROM (public.v_personnel_poste_association
     JOIN public.conge USING (idpersonnel));


ALTER TABLE public.v_conge_service OWNER TO postgres;

--
-- Name: v_conge_refus; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_conge_refus AS
 SELECT cs.idconge,
    cs.idpersonnel,
    cs.idposte,
    cs.date_embauche,
    cs.idservice,
    cs.nomposte,
    cs.datedebut,
    cs.datefin,
    cs.reeldatefin,
    cs.accepte,
    cs.idraison,
    ra.nomraison,
    r.idrefus,
    r.raison_refus,
    r.idservice AS superieur,
    cs.autre_raison
   FROM ((public.v_conge_service cs
     LEFT JOIN public.refus r ON ((cs.idconge = r.idconge)))
     LEFT JOIN public.raison ra ON ((ra.idraison = cs.idraison)));


ALTER TABLE public.v_conge_refus OWNER TO postgres;

--
-- Name: v_criter_choix; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_criter_choix AS
 SELECT t.idtypecritere,
    t.nomtypecritere,
    choix.idchoix,
    choix.intitulechoix,
    choix.valeurchoix
   FROM (public.typecritere t
     JOIN public.choix USING (idtypecritere));


ALTER TABLE public.v_criter_choix OWNER TO postgres;

--
-- Name: v_critere_choix_type; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_critere_choix_type AS
 SELECT vct.idchoix,
    vct.idtypecritere,
    vct.intitulechoix,
    vct.valeurchoix,
    vct.nomtypecritere,
    cc.idcritere
   FROM (public.v_choix_type vct
     JOIN public.criterechoix cc USING (idchoix));


ALTER TABLE public.v_critere_choix_type OWNER TO postgres;

--
-- Name: v_critere_poste_besoin; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_critere_poste_besoin AS
 SELECT pb.idbesoin,
    pb.idposte,
    pb.idservice,
    pb.nomposte,
    pb.heuresemaine,
    pb.heurepersonne,
    pb.accompli,
    pb.idtypecontrat,
    pb.nb_personne,
    c.idcritere,
    c.idtypecritere,
    c.coefficient
   FROM (public.v_poste_besoin pb
     JOIN public.critere c USING (idbesoin));


ALTER TABLE public.v_critere_poste_besoin OWNER TO postgres;

--
-- Name: v_critere_details; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_critere_details AS
 SELECT ct.idtypecritere,
    ct.idcritere,
    ct.idchoix,
    ct.intitulechoix,
    ct.valeurchoix,
    ct.nomtypecritere,
    c.idbesoin,
    c.idposte,
    c.idservice,
    c.nomposte,
    c.heuresemaine,
    c.heurepersonne,
    c.accompli,
    c.idtypecontrat,
    c.nb_personne,
    c.coefficient
   FROM (public.v_critere_choix_type ct
     JOIN public.v_critere_poste_besoin c USING (idtypecritere, idcritere));


ALTER TABLE public.v_critere_details OWNER TO postgres;

--
-- Name: v_critere_service; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_critere_service AS
 SELECT s.idservice,
    s.nomservice,
    s.iconeservice,
    s.superieur,
    cd.idtypecritere,
    cd.idcritere,
    cd.idchoix,
    cd.intitulechoix,
    cd.valeurchoix,
    cd.nomtypecritere,
    cd.idbesoin,
    cd.idposte,
    cd.nomposte,
    cd.heuresemaine,
    cd.heurepersonne,
    cd.accompli,
    cd.idtypecontrat,
    cd.nb_personne,
    cd.coefficient
   FROM (public.service s
     JOIN public.v_critere_details cd USING (idservice));


ALTER TABLE public.v_critere_service OWNER TO postgres;

--
-- Name: v_personnel_poste; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_personnel_poste AS
 SELECT ppa.idpersonnel,
    ppa.idposte,
    ppa.date_embauche,
    ppa.idservice,
    ppa.nomposte,
    personnel.nom,
    personnel.prenom,
    personnel.mail,
    personnel.matricule,
    personnel.nationalite,
    personnel.adresse,
    personnel.genre,
    personnel.travailleur,
    personnel.dtn,
    personnel.contact
   FROM (public.v_personnel_poste_association ppa
     JOIN public.personnel USING (idpersonnel));


ALTER TABLE public.v_personnel_poste OWNER TO postgres;

--
-- Name: v_personnel_information; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_personnel_information AS
 SELECT vpp.idpersonnel,
    vpp.idposte,
    vpp.date_embauche,
    vpp.idservice,
    vpp.nomposte,
    vpp.nom,
    vpp.prenom,
    vpp.mail,
    vpp.matricule,
    vpp.nationalite,
    vpp.adresse,
    vpp.genre,
    vpp.travailleur,
    vpp.dtn,
    vpp.contact,
    COALESCE(latest_salary.salaire_brut, (0)::numeric) AS latest_salary_base,
    latest_salary.salaire_net AS latest_salary_net,
    latest_salary.date_insert AS latest_salary_date,
    COALESCE(public.get_latest_hire_date(vpp.idpersonnel), '1970-01-01'::date) AS latest_hire_date,
    date_part('year'::text, age(now(), (vpp.dtn)::timestamp with time zone)) AS age
   FROM (public.v_personnel_poste vpp
     LEFT JOIN LATERAL ( SELECT get_latest_salary.salaire_brut,
            get_latest_salary.salaire_net,
            get_latest_salary.date_insert
           FROM public.get_latest_salary(vpp.idpersonnel) get_latest_salary(salaire_brut, salaire_net, date_insert)) latest_salary ON (true));


ALTER TABLE public.v_personnel_information OWNER TO postgres;

--
-- Name: v_nbjours_personnel; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_nbjours_personnel AS
 SELECT
        CASE
            WHEN (date_part('day'::text, (now() - (v_personnel_information.latest_hire_date)::timestamp with time zone)) > (90)::double precision) THEN (90)::double precision
            ELSE date_part('day'::text, (now() - (v_personnel_information.latest_hire_date)::timestamp with time zone))
        END AS nbjours,
    v_personnel_information.idpersonnel
   FROM public.v_personnel_information;


ALTER TABLE public.v_nbjours_personnel OWNER TO postgres;

--
-- Name: v_nbj_conge_personnel; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_nbj_conge_personnel AS
 SELECT nbj.idpersonnel,
    (nbj.nbjours - dc.nbconge) AS difference
   FROM (public.v_nbjours_personnel nbj
     LEFT JOIN public.v_all_diff_conge dc ON ((nbj.idpersonnel = dc.idpersonnel)));


ALTER TABLE public.v_nbj_conge_personnel OWNER TO postgres;

--
-- Name: v_nbheure_conge_personnel; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_nbheure_conge_personnel AS
 SELECT (v_nbj_conge_personnel.difference * (0.67)::double precision) AS nbheure,
    v_nbj_conge_personnel.idpersonnel
   FROM public.v_nbj_conge_personnel;


ALTER TABLE public.v_nbheure_conge_personnel OWNER TO postgres;

--
-- Name: v_points_entretien; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_points_entretien AS
 SELECT note_entretien.idcandidature,
    sum((note_entretien.note * question_entretien.coeff)) AS points
   FROM ((public.note_entretien
     JOIN public.question_entretien ON ((question_entretien.idquestion_entretien = note_entretien.idquestion_entretien)))
     JOIN public.v_candidat_entretien candidature ON ((candidature.idcandidature = note_entretien.idcandidature)))
  GROUP BY note_entretien.idcandidature;


ALTER TABLE public.v_points_entretien OWNER TO postgres;

--
-- Name: v_points_entretien_candidat; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_points_entretien_candidat AS
 SELECT v_candidat_candidature.idbesoin,
    v_candidat_candidature.idcandidat,
    v_candidat_candidature.nomcandidat,
    v_candidat_candidature.prenomcandidat,
    v_candidat_candidature.dtn,
    v_candidat_candidature.mail,
    v_candidat_candidature.contact,
    v_candidat_candidature.idcandidature,
    v_candidat_candidature.datecandidature,
    v_candidat_candidature.validation,
    v_candidat_candidature.code,
    v_candidat_candidature.idposte,
    v_candidat_candidature.idservice,
    v_candidat_candidature.nomposte,
    v_candidat_candidature.heuresemaine,
    v_candidat_candidature.heurepersonne,
    v_candidat_candidature.accompli,
    v_candidat_candidature.idtypecontrat,
    v_candidat_candidature.nb_personne,
    v_points_entretien.points
   FROM (public.v_points_entretien
     JOIN public.v_candidat_candidature ON ((v_candidat_candidature.idcandidature = v_points_entretien.idcandidature)));


ALTER TABLE public.v_points_entretien_candidat OWNER TO postgres;

--
-- Name: v_service_poste; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_service_poste AS
 SELECT p.idservice,
    p.idposte,
    p.nomposte,
    s.nomservice,
    s.iconeservice,
    s.superieur
   FROM (public.poste p
     JOIN public.service s USING (idservice));


ALTER TABLE public.v_service_poste OWNER TO postgres;

--
-- Name: avantage idavantage; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.avantage ALTER COLUMN idavantage SET DEFAULT nextval('public.avantage_idavantage_seq'::regclass);


--
-- Name: besoin idbesoin; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.besoin ALTER COLUMN idbesoin SET DEFAULT nextval('public.besoin_idbesoin_seq'::regclass);


--
-- Name: candidat idcandidat; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.candidat ALTER COLUMN idcandidat SET DEFAULT nextval('public.candidat_idcandidat_seq'::regclass);


--
-- Name: candidature idcandidature; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.candidature ALTER COLUMN idcandidature SET DEFAULT nextval('public.candidature_idcanditature_seq'::regclass);


--
-- Name: choix idchoix; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.choix ALTER COLUMN idchoix SET DEFAULT nextval('public.choix_idchoix_seq'::regclass);


--
-- Name: conge idconge; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conge ALTER COLUMN idconge SET DEFAULT nextval('public.conge_idconge_seq'::regclass);


--
-- Name: contrat_essai idcontrat_essai; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contrat_essai ALTER COLUMN idcontrat_essai SET DEFAULT nextval('public.contrat_essai_idcontrat_essai_seq'::regclass);


--
-- Name: contrat_travail idcontrat_travail; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contrat_travail ALTER COLUMN idcontrat_travail SET DEFAULT nextval('public.contrat_travail_idcontrat_travail_seq'::regclass);


--
-- Name: critere idcritere; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.critere ALTER COLUMN idcritere SET DEFAULT nextval('public.critere_idcritere_seq'::regclass);


--
-- Name: essai idessai; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.essai ALTER COLUMN idessai SET DEFAULT nextval('public.essai_idessai_seq'::regclass);


--
-- Name: info idinfo; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.info ALTER COLUMN idinfo SET DEFAULT nextval('public.info_idinfo_seq'::regclass);


--
-- Name: option idoption; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.option ALTER COLUMN idoption SET DEFAULT nextval('public.option_idoption_seq'::regclass);


--
-- Name: personnel idpersonnel; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel ALTER COLUMN idpersonnel SET DEFAULT nextval('public.personnel_idpersonnel_seq'::regclass);


--
-- Name: personnel_embauche idpersonnel_embauche; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel_embauche ALTER COLUMN idpersonnel_embauche SET DEFAULT nextval('public.personnel_embauche_idpersonnel_embauche_seq'::regclass);


--
-- Name: personnel_salaire idpersonnel_salaire; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel_salaire ALTER COLUMN idpersonnel_salaire SET DEFAULT nextval('public.personnel_salaire_idpersonnel_salaire_seq'::regclass);


--
-- Name: planning_visible idpv; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.planning_visible ALTER COLUMN idpv SET DEFAULT nextval('public.planning_visible_idpv_seq'::regclass);


--
-- Name: poste idposte; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.poste ALTER COLUMN idposte SET DEFAULT nextval('public.poste_idposte_seq'::regclass);


--
-- Name: question idquestion; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question ALTER COLUMN idquestion SET DEFAULT nextval('public.question_idquestion_seq'::regclass);


--
-- Name: question_entretien idquestion_entretien; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_entretien ALTER COLUMN idquestion_entretien SET DEFAULT nextval('public.question_entretien_idquestion_entretien_seq'::regclass);


--
-- Name: questionnaire idquestionnaire; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.questionnaire ALTER COLUMN idquestionnaire SET DEFAULT nextval('public.questionnaire_idquestionnaire_seq'::regclass);


--
-- Name: raison idraison; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.raison ALTER COLUMN idraison SET DEFAULT nextval('public.raison_idraison_seq'::regclass);


--
-- Name: refus idrefus; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refus ALTER COLUMN idrefus SET DEFAULT nextval('public.refus_idrefus_seq'::regclass);


--
-- Name: sante idsante; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sante ALTER COLUMN idsante SET DEFAULT nextval('public.sante_idsante_seq'::regclass);


--
-- Name: service idservice; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service ALTER COLUMN idservice SET DEFAULT nextval('public.service_idservice_seq'::regclass);


--
-- Name: travail idtravail; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.travail ALTER COLUMN idtravail SET DEFAULT nextval('public.travail_idtravail_seq'::regclass);


--
-- Name: type_contrat idtypecontrat; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.type_contrat ALTER COLUMN idtypecontrat SET DEFAULT nextval('public.type_contrat_idtypecontrat_seq'::regclass);


--
-- Name: typecritere idtypecritere; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.typecritere ALTER COLUMN idtypecritere SET DEFAULT nextval('public.typecritere_idtypecritere_seq'::regclass);


--
-- Name: typeuser idtypeuser; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.typeuser ALTER COLUMN idtypeuser SET DEFAULT nextval('public.typeuser_idtypeuser_seq'::regclass);


--
-- Name: useradmin idadmin; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.useradmin ALTER COLUMN idadmin SET DEFAULT nextval('public.useradmin_idadmin_seq'::regclass);


--
-- Data for Name: admin_service; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.admin_service VALUES (1, 1);
INSERT INTO public.admin_service VALUES (1, 2);
INSERT INTO public.admin_service VALUES (2, 1);
INSERT INTO public.admin_service VALUES (2, 2);
INSERT INTO public.admin_service VALUES (2, 3);
INSERT INTO public.admin_service VALUES (3, 4);
INSERT INTO public.admin_service VALUES (3, 5);
INSERT INTO public.admin_service VALUES (7, 1);
INSERT INTO public.admin_service VALUES (7, 2);
INSERT INTO public.admin_service VALUES (7, 3);
INSERT INTO public.admin_service VALUES (7, 4);
INSERT INTO public.admin_service VALUES (7, 5);
INSERT INTO public.admin_service VALUES (7, 6);
INSERT INTO public.admin_service VALUES (7, 7);
INSERT INTO public.admin_service VALUES (5, 2);
INSERT INTO public.admin_service VALUES (5, 3);
INSERT INTO public.admin_service VALUES (10, 1);
INSERT INTO public.admin_service VALUES (10, 2);
INSERT INTO public.admin_service VALUES (10, 3);
INSERT INTO public.admin_service VALUES (10, 4);
INSERT INTO public.admin_service VALUES (10, 5);
INSERT INTO public.admin_service VALUES (10, 6);
INSERT INTO public.admin_service VALUES (10, 7);
INSERT INTO public.admin_service VALUES (10, 8);
INSERT INTO public.admin_service VALUES (10, 9);
INSERT INTO public.admin_service VALUES (10, 10);


--
-- Data for Name: avantage; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.avantage VALUES (1, 'Voiture');
INSERT INTO public.avantage VALUES (2, 'Maison');
INSERT INTO public.avantage VALUES (3, 'Secretaire');


--
-- Data for Name: besoin; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: candidat; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: candidature; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: choix; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.choix VALUES (1, 1, 'homme', 1);
INSERT INTO public.choix VALUES (2, 1, 'femme', 1);
INSERT INTO public.choix VALUES (3, 2, 'Malagasy', 1);
INSERT INTO public.choix VALUES (4, 2, 'Etranger', 1);
INSERT INTO public.choix VALUES (5, 3, 'CEPE', 1);
INSERT INTO public.choix VALUES (6, 3, 'BEPC', 2);
INSERT INTO public.choix VALUES (7, 3, 'BACC', 3);
INSERT INTO public.choix VALUES (8, 3, 'Licence', 4);
INSERT INTO public.choix VALUES (9, 3, 'Master', 5);
INSERT INTO public.choix VALUES (10, 3, 'Doctorat', 6);
INSERT INTO public.choix VALUES (11, 4, 'moins de 2 ans', 1);
INSERT INTO public.choix VALUES (12, 4, 'entre 2 a 4 ans', 2);
INSERT INTO public.choix VALUES (13, 4, 'entre 4 a 7 ans', 3);
INSERT INTO public.choix VALUES (14, 4, '7ans et plus', 4);
INSERT INTO public.choix VALUES (15, 5, 'marie(e)', 1);
INSERT INTO public.choix VALUES (16, 5, 'divorce(e)', 1);
INSERT INTO public.choix VALUES (17, 5, 'en couple', 1);


--
-- Data for Name: choixcandidature; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: conge; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.conge VALUES (32, 5, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -1, 1, NULL);
INSERT INTO public.conge VALUES (24, 2, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', -2, 1, 'Vacances entre famille');
INSERT INTO public.conge VALUES (27, 3, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 3, 1, 'Vacances entre famille');
INSERT INTO public.conge VALUES (51, 11, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 3, 1, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (48, 10, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 3, NULL, 'Vacances entre famille');
INSERT INTO public.conge VALUES (1, 5, '2023-03-22 08:00:00', '2023-03-25 12:00:00', '2023-03-25 12:00:00', 3, NULL, NULL);
INSERT INTO public.conge VALUES (2, 5, '2023-03-23 09:30:00', '2023-03-26 15:45:00', '2023-03-26 15:45:00', 3, 1, NULL);
INSERT INTO public.conge VALUES (33, 5, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 3, NULL, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (23, 2, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', 3, 2, 'Vacances entre famille');
INSERT INTO public.conge VALUES (47, 10, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', 3, 1, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (29, 4, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', 3, 1, 'Vacances entre famille');
INSERT INTO public.conge VALUES (25, 3, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 3, 1, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (46, 10, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 3, NULL, NULL);
INSERT INTO public.conge VALUES (49, 11, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 3, NULL, NULL);
INSERT INTO public.conge VALUES (22, 2, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 3, NULL, 'Vacances entre famille');
INSERT INTO public.conge VALUES (13, 5, '2023-11-25 08:00:00', '2023-11-26 17:00:00', '2023-11-26 17:00:00', -2, 1, NULL);
INSERT INTO public.conge VALUES (5, 5, '2023-03-26 16:00:00', '2023-03-29 16:30:00', '2023-03-29 16:30:00', 2, 3, NULL);
INSERT INTO public.conge VALUES (6, 5, '2023-12-14 08:00:00', '2023-12-15 17:00:00', '2023-12-20 08:00:00', 3, NULL, 'oui oui');
INSERT INTO public.conge VALUES (3, 5, '2023-03-24 10:15:00', '2023-03-27 11:30:00', '2023-03-27 11:30:00', 3, NULL, NULL);
INSERT INTO public.conge VALUES (4, 5, '2023-03-25 12:30:00', '2023-03-28 14:15:00', '2023-03-30 14:10:00', 3, 1, NULL);
INSERT INTO public.conge VALUES (19, 1, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', -2, NULL, 'Vacances entre famille');
INSERT INTO public.conge VALUES (20, 1, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -1, 1, 'Vacances entre famille');
INSERT INTO public.conge VALUES (21, 1, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 1, 1, 'Vacances entre famille');
INSERT INTO public.conge VALUES (26, 3, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -2, 1, NULL);
INSERT INTO public.conge VALUES (28, 4, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 0, NULL, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (30, 4, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', -2, NULL, 'Vacances entre famille');
INSERT INTO public.conge VALUES (31, 5, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', -1, NULL, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (34, 6, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', -1, 1, NULL);
INSERT INTO public.conge VALUES (35, 6, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -2, 1, NULL);
INSERT INTO public.conge VALUES (36, 6, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 1, 3, NULL);
INSERT INTO public.conge VALUES (37, 7, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 1, 1, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (38, 7, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -2, 1, NULL);
INSERT INTO public.conge VALUES (39, 7, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 1, 1, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (40, 8, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 0, 2, NULL);
INSERT INTO public.conge VALUES (41, 8, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -1, 1, NULL);
INSERT INTO public.conge VALUES (42, 8, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 0, NULL, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (43, 9, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 2, NULL, NULL);
INSERT INTO public.conge VALUES (44, 9, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -2, 2, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (45, 9, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 0, NULL, 'Vacances entre famille');
INSERT INTO public.conge VALUES (50, 11, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -1, 2, 'Vacances entre famille');
INSERT INTO public.conge VALUES (52, 12, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', -2, 3, 'Vacances entre famille');
INSERT INTO public.conge VALUES (53, 12, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -1, NULL, NULL);
INSERT INTO public.conge VALUES (54, 12, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 0, 2, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (55, 13, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', -2, NULL, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (56, 13, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -2, 1, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (57, 13, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', 1, 2, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (58, 14, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 0, 1, NULL);
INSERT INTO public.conge VALUES (59, 14, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', 1, NULL, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (60, 14, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', -1, NULL, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (61, 15, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', -1, 3, 'Vacances entre famille');
INSERT INTO public.conge VALUES (62, 15, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -1, 2, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (63, 15, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', -2, 3, NULL);
INSERT INTO public.conge VALUES (64, 16, '2023-09-01 00:00:00', '2023-09-03 00:00:00', '2023-09-03 00:00:00', 0, 2, NULL);
INSERT INTO public.conge VALUES (65, 16, '2023-09-04 00:00:00', '2023-09-06 00:00:00', '2023-09-06 00:00:00', -2, 3, 'Visite chez ma grand-mŠre');
INSERT INTO public.conge VALUES (66, 16, '2023-09-07 00:00:00', '2023-09-09 00:00:00', '2023-09-09 00:00:00', -1, 3, NULL);


--
-- Data for Name: contrat_essai; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: contrat_travail; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: critere; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: criterechoix; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: essai; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: essai_avantage; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: fichier; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: info; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: note_entretien; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: option; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: personnel; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.personnel VALUES (15, 'Tita', 'Goore', 'tita.johnson@email.com', '37651', 3, '789 Oak St', 1, 0, '1985-07-10', '032 46 234 43');
INSERT INTO public.personnel VALUES (16, 'Dupont', 'Jean', 'jean.dupont@example.com', '123456', 3, '123 Rue de la Paix, Paris', 1, 1, '1972-01-02', '032 46 234 43');
INSERT INTO public.personnel VALUES (2, 'Smith', 'Jane', 'jane.smith@email.com', '67890', 4, '456 Elm St', 2, 1, '1995-03-15', '032 46 234 43');
INSERT INTO public.personnel VALUES (5, 'Garcia', 'Carlos', 'carlos.garcia@email.com', '13579', 4, '890 Maple St', 1, 1, '1998-04-25', '032 46 234 43');
INSERT INTO public.personnel VALUES (8, 'Martinez', 'Luis', 'luis.martinez@email.com', '11223', 4, '456 Oak St', 1, 1, '1989-06-30', '032 46 234 43');
INSERT INTO public.personnel VALUES (11, 'Nguyen', 'Thi', 'thi.nguyen@email.com', '44444', 4, '789 Cedar St', 2, 0, '1986-02-19', '032 46 234 43');
INSERT INTO public.personnel VALUES (1, 'Doe', 'John', 'john.doe@email.com', '12345', 3, '123 Main St', 1, 1, '1990-01-01', '032 46 234 43');
INSERT INTO public.personnel VALUES (3, 'Johnson', 'Robert', 'robert.johnson@email.com', '54321', 3, '789 Oak St', 1, 0, '1985-07-10', '032 46 234 43');
INSERT INTO public.personnel VALUES (4, 'Brown', 'Sarah', 'sarah.brown@email.com', '98765', 3, '567 Pine St', 2, 1, '1988-11-20', '032 46 234 43');
INSERT INTO public.personnel VALUES (6, 'Wilson', 'Emily', 'emily.wilson@email.com', '24680', 3, '234 Birch St', 2, 0, '1993-09-05', '032 46 234 43');
INSERT INTO public.personnel VALUES (7, 'Chen', 'Wei', 'wei.chen@email.com', '10101', 3, '345 Cedar St', 1, 1, '1992-12-12', '032 46 234 43');
INSERT INTO public.personnel VALUES (9, 'Davis', 'Susan', 'susan.davis@email.com', '22222', 3, '567 Elm St', 2, 1, '1987-03-07', '032 46 234 43');
INSERT INTO public.personnel VALUES (10, 'Kim', 'Min-Ji', 'minji.kim@email.com', '33333', 3, '678 Pine St', 2, 1, '1991-08-14', '032 46 234 43');
INSERT INTO public.personnel VALUES (12, 'Jackson', 'William', 'william.jackson@email.com', '55555', 3, '890 Birch St', 1, 1, '1984-05-28', '032 46 234 43');
INSERT INTO public.personnel VALUES (13, 'Ralph', 'Yoan', 'ralph.doe@email.com', '48235', 3, '123 Main St', 1, 1, '1992-01-01', '032 46 234 43');
INSERT INTO public.personnel VALUES (14, 'Rebeka', 'Ravalison', 'Rebeka.smith@email.com', '10853', 4, '456 Elm St', 2, 1, '1991-03-15', '032 46 234 43');


--
-- Data for Name: personnel_embauche; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.personnel_embauche VALUES (29, 16, '2010-10-23');


--
-- Data for Name: personnel_poste; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.personnel_poste VALUES (14, 1, NULL);
INSERT INTO public.personnel_poste VALUES (28, 2, NULL);
INSERT INTO public.personnel_poste VALUES (10, 3, NULL);
INSERT INTO public.personnel_poste VALUES (75, 8, NULL);
INSERT INTO public.personnel_poste VALUES (38, 9, NULL);
INSERT INTO public.personnel_poste VALUES (62, 10, NULL);
INSERT INTO public.personnel_poste VALUES (10, 11, NULL);
INSERT INTO public.personnel_poste VALUES (66, 12, NULL);
INSERT INTO public.personnel_poste VALUES (76, 13, NULL);
INSERT INTO public.personnel_poste VALUES (77, 14, NULL);
INSERT INTO public.personnel_poste VALUES (78, 15, NULL);
INSERT INTO public.personnel_poste VALUES (41, 5, NULL);
INSERT INTO public.personnel_poste VALUES (92, 16, NULL);
INSERT INTO public.personnel_poste VALUES (1, 4, NULL);
INSERT INTO public.personnel_poste VALUES (79, 6, NULL);
INSERT INTO public.personnel_poste VALUES (86, 7, NULL);


--
-- Data for Name: personnel_salaire; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.personnel_salaire VALUES (1, 1, 212.596743367612, 2683.69556963444, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (2, 2, 4099.44796003401, 2767.45133846998, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (3, 3, 5159.44009181112, 639.864359050989, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (4, 4, 5861.20076477528, 5535.61900928617, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (5, 5, 8914.11107964814, 4591.20953455567, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (6, 6, 6553.00550628453, 5070.32980397344, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (7, 7, 6228.07593084872, 2601.2824960053, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (8, 8, 182.403484359384, 3864.17101323605, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (9, 9, 5005.83817716688, 7835.78831329942, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (10, 10, 7721.16759326309, 3402.20143646002, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (11, 11, 2290.75343813747, 7954.65090870857, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (12, 12, 532.283848151565, 1312.51744925976, '2023-10-20');
INSERT INTO public.personnel_salaire VALUES (13, 1, 2557.70004354417, 5778.7604406476, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (14, 2, 9419.07985601574, 3061.59798428416, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (15, 3, 3913.53609506041, 7955.97526431084, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (16, 4, 5187.16021440923, 371.510699391365, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (17, 5, 5706.82640187442, 6734.32401195169, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (18, 6, 4547.44686372578, 638.868857175112, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (19, 7, 5416.87051299959, 327.035043388605, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (20, 8, 2858.9819278568, 6493.17969754338, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (21, 9, 8492.22559016198, 2185.78658252954, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (22, 10, 7999.36844501644, 3313.07819858193, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (23, 11, 3658.54287985712, 3116.36643856764, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (24, 12, 8463.79973459989, 2193.93846020103, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (25, 13, 1890.43654128909, 1072.45395332575, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (26, 14, 4547.85443842411, 4861.04905605316, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (27, 15, 9556.32508732378, 5627.22841277719, '2023-10-21');
INSERT INTO public.personnel_salaire VALUES (28, 16, 10023045.00, NULL, '2023-10-23');


--
-- Data for Name: planning_visible; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.planning_visible VALUES (1, 1, 1);
INSERT INTO public.planning_visible VALUES (2, 2, 1);
INSERT INTO public.planning_visible VALUES (3, 2, 2);
INSERT INTO public.planning_visible VALUES (4, 3, 3);
INSERT INTO public.planning_visible VALUES (5, 3, 2);
INSERT INTO public.planning_visible VALUES (6, 4, 1);
INSERT INTO public.planning_visible VALUES (7, 4, 3);
INSERT INTO public.planning_visible VALUES (8, 4, 4);
INSERT INTO public.planning_visible VALUES (9, 5, 1);
INSERT INTO public.planning_visible VALUES (10, 5, 2);
INSERT INTO public.planning_visible VALUES (11, 5, 3);
INSERT INTO public.planning_visible VALUES (12, 6, 1);
INSERT INTO public.planning_visible VALUES (13, 7, 1);
INSERT INTO public.planning_visible VALUES (14, 7, 2);
INSERT INTO public.planning_visible VALUES (15, 7, 3);
INSERT INTO public.planning_visible VALUES (16, 7, 4);
INSERT INTO public.planning_visible VALUES (17, 7, 5);
INSERT INTO public.planning_visible VALUES (18, 7, 6);
INSERT INTO public.planning_visible VALUES (19, 7, 7);
INSERT INTO public.planning_visible VALUES (20, 10, 1);
INSERT INTO public.planning_visible VALUES (21, 10, 2);
INSERT INTO public.planning_visible VALUES (22, 10, 3);
INSERT INTO public.planning_visible VALUES (23, 10, 4);
INSERT INTO public.planning_visible VALUES (24, 10, 5);
INSERT INTO public.planning_visible VALUES (25, 10, 6);
INSERT INTO public.planning_visible VALUES (26, 10, 7);
INSERT INTO public.planning_visible VALUES (27, 10, 8);
INSERT INTO public.planning_visible VALUES (28, 10, 9);
INSERT INTO public.planning_visible VALUES (29, 10, 10);
INSERT INTO public.planning_visible VALUES (30, 8, 1);
INSERT INTO public.planning_visible VALUES (31, 8, 2);
INSERT INTO public.planning_visible VALUES (32, 8, 3);
INSERT INTO public.planning_visible VALUES (33, 8, 8);
INSERT INTO public.planning_visible VALUES (34, 9, 3);
INSERT INTO public.planning_visible VALUES (35, 9, 2);
INSERT INTO public.planning_visible VALUES (36, 9, 4);
INSERT INTO public.planning_visible VALUES (37, 9, 5);
INSERT INTO public.planning_visible VALUES (38, 9, 9);


--
-- Data for Name: poste; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.poste VALUES (1, 1, 'Responsable des achats');
INSERT INTO public.poste VALUES (2, 1, 'Analyste');
INSERT INTO public.poste VALUES (3, 1, 'Approvisionneur');
INSERT INTO public.poste VALUES (4, 1, 'Gestionnaire de la chaine d''approvisionnement');
INSERT INTO public.poste VALUES (5, 1, 'Specialiste des contrats');
INSERT INTO public.poste VALUES (6, 1, 'Responsable des relations avec les fournisseurs');
INSERT INTO public.poste VALUES (7, 1, 'Responsable des achats durables');
INSERT INTO public.poste VALUES (8, 2, 'Analyste financier');
INSERT INTO public.poste VALUES (9, 2, 'Comptable');
INSERT INTO public.poste VALUES (10, 2, 'Controleur financier');
INSERT INTO public.poste VALUES (11, 2, 'Responsable financier');
INSERT INTO public.poste VALUES (12, 2, 'Analyste de credit');
INSERT INTO public.poste VALUES (13, 2, 'Gestionnaire de portefeuille');
INSERT INTO public.poste VALUES (14, 2, 'Analyste de risque financier');
INSERT INTO public.poste VALUES (15, 2, 'Gestionnaire de tresorerie');
INSERT INTO public.poste VALUES (16, 2, 'Analyste en fusion-acquisition');
INSERT INTO public.poste VALUES (17, 2, 'Auditeur financier');
INSERT INTO public.poste VALUES (18, 2, 'Planificateur financier');
INSERT INTO public.poste VALUES (19, 2, 'Specialiste des marches financiers');
INSERT INTO public.poste VALUES (20, 2, 'Analyste des operations bancaires');
INSERT INTO public.poste VALUES (21, 2, 'Specialiste en fiscalite');
INSERT INTO public.poste VALUES (22, 2, 'Actuaire');
INSERT INTO public.poste VALUES (23, 3, 'Developpeur de logiciels');
INSERT INTO public.poste VALUES (24, 3, 'Ingenieur en securite informatique');
INSERT INTO public.poste VALUES (25, 3, 'Administrateur systeme');
INSERT INTO public.poste VALUES (26, 3, 'Analyste de donnees');
INSERT INTO public.poste VALUES (27, 3, 'Ingenieur en reseau');
INSERT INTO public.poste VALUES (28, 3, 'Developpeur web');
INSERT INTO public.poste VALUES (29, 3, 'Architecte de solutions cloud');
INSERT INTO public.poste VALUES (30, 3, 'Analyste en intelligence artificielle');
INSERT INTO public.poste VALUES (31, 3, 'Chef de projet informatique');
INSERT INTO public.poste VALUES (32, 3, 'Developpeur d''applications mobiles');
INSERT INTO public.poste VALUES (33, 3, 'Architecte de donnees');
INSERT INTO public.poste VALUES (34, 3, 'Testeur de logiciels');
INSERT INTO public.poste VALUES (35, 3, 'Specialiste en cybersecurite');
INSERT INTO public.poste VALUES (36, 3, 'Administrateur de bases de donnees');
INSERT INTO public.poste VALUES (37, 3, 'Analyste en assurance qualite logicielle');
INSERT INTO public.poste VALUES (38, 3, 'Developpeur DevOps');
INSERT INTO public.poste VALUES (39, 3, 'Consultant en technologies de l''information');
INSERT INTO public.poste VALUES (40, 3, 'Expert en analyse de donnees');
INSERT INTO public.poste VALUES (41, 3, 'Administrateur de systemes de gestion de contenu (CMS)');
INSERT INTO public.poste VALUES (42, 3, 'Developpeur de jeux video');
INSERT INTO public.poste VALUES (43, 4, 'Gestionnaire de la chaine d''approvisionnement');
INSERT INTO public.poste VALUES (44, 4, 'Responsable logistique');
INSERT INTO public.poste VALUES (45, 4, 'Gestionnaire d''entrepot');
INSERT INTO public.poste VALUES (46, 4, 'Planificateur de la demande');
INSERT INTO public.poste VALUES (47, 4, 'Responsable des operations de transport');
INSERT INTO public.poste VALUES (48, 4, 'Coordinateur de la chaine d''approvisionnement');
INSERT INTO public.poste VALUES (49, 4, 'Analyste de la chaine d''approvisionnement');
INSERT INTO public.poste VALUES (50, 4, 'Planificateur de la production');
INSERT INTO public.poste VALUES (51, 4, 'Specialiste en gestion des stocks');
INSERT INTO public.poste VALUES (52, 4, 'Analyste en logistique internationale');
INSERT INTO public.poste VALUES (53, 4, 'Gestionnaire de la qualite logistique');
INSERT INTO public.poste VALUES (54, 4, 'Coordonnateur de la distribution');
INSERT INTO public.poste VALUES (55, 4, 'Expert en gestion des retours');
INSERT INTO public.poste VALUES (56, 4, 'Responsable de la logistique inversee');
INSERT INTO public.poste VALUES (57, 4, 'Analyste en optimisation des itineraires');
INSERT INTO public.poste VALUES (58, 4, 'Coordonnateur des operations de transport international');
INSERT INTO public.poste VALUES (59, 4, 'Planificateur de la logistique e-commerce');
INSERT INTO public.poste VALUES (60, 4, 'Gestionnaire de la logistique de la sante');
INSERT INTO public.poste VALUES (61, 4, 'Responsable de la gestion des fournisseurs');
INSERT INTO public.poste VALUES (62, 4, 'Analyste en co–ts logistiques');
INSERT INTO public.poste VALUES (63, 5, 'Operateur de machine industrielle');
INSERT INTO public.poste VALUES (64, 5, 'Technicien de production');
INSERT INTO public.poste VALUES (65, 5, 'Superviseur de production');
INSERT INTO public.poste VALUES (66, 5, 'Ingenieur de production');
INSERT INTO public.poste VALUES (67, 5, 'Operateur de CNC');
INSERT INTO public.poste VALUES (68, 5, 'Ouvrier');
INSERT INTO public.poste VALUES (69, 5, 'Soudeur');
INSERT INTO public.poste VALUES (70, 5, 'Planificateur de production');
INSERT INTO public.poste VALUES (71, 5, 'Electromecanicien');
INSERT INTO public.poste VALUES (72, 6, 'Ingenieur de recherche');
INSERT INTO public.poste VALUES (73, 6, 'Chercheur Scientifique');
INSERT INTO public.poste VALUES (74, 6, 'Ingenieur en conception produit');
INSERT INTO public.poste VALUES (75, 6, 'Chimiste de recherche');
INSERT INTO public.poste VALUES (76, 7, 'Responsable des ressources humaines');
INSERT INTO public.poste VALUES (77, 7, 'Recruteeur');
INSERT INTO public.poste VALUES (78, 7, 'Gestionnaire de la paie');
INSERT INTO public.poste VALUES (79, 8, 'Technicien de Maintenance Industrielle');
INSERT INTO public.poste VALUES (80, 8, 'Electricien de Batiment');
INSERT INTO public.poste VALUES (81, 8, 'Plombier');
INSERT INTO public.poste VALUES (82, 8, 'Mecanicien Automobile');
INSERT INTO public.poste VALUES (83, 8, 'Technicien en Climatisation et Chauffage');
INSERT INTO public.poste VALUES (84, 8, 'Technicien en Informatique et R‚seaux');
INSERT INTO public.poste VALUES (85, 8, 'Chef d''Equipe de Maintenance');
INSERT INTO public.poste VALUES (86, 9, 'Specialiste en Marketing Numerique');
INSERT INTO public.poste VALUES (87, 9, 'Responsable des Medias Sociaux');
INSERT INTO public.poste VALUES (88, 9, 'Chef de Produit');
INSERT INTO public.poste VALUES (89, 9, 'Analyste Marketing');
INSERT INTO public.poste VALUES (90, 9, 'Responsable du Marketing Produit');
INSERT INTO public.poste VALUES (91, 10, 'Directeur G‚n‚ral');
INSERT INTO public.poste VALUES (92, 10, 'Directeur des Ventes');


--
-- Data for Name: question; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: question_entretien; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: questionnaire; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: raison; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.raison VALUES (1, 'maternite');
INSERT INTO public.raison VALUES (2, 'paternite');
INSERT INTO public.raison VALUES (3, 'maladie');


--
-- Data for Name: refus; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.refus VALUES (1, 6, 'suhfifshoSICBDCBDICBDSJBDIJVBBVCIJ BJB JBVSJDBCSKDJBCDSKJBCQDSLKJBCLDJKBLJC  BDKJBSDKCJDSCKJDBCKJDBCKJBDSKJB', 7);
INSERT INTO public.refus VALUES (2, 32, 'tsy tafa', 3);
INSERT INTO public.refus VALUES (3, 24, 'tsy tafa koa ', 7);


--
-- Data for Name: sante; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.sante VALUES (1, 'Funhece');
INSERT INTO public.sante VALUES (2, 'AMIT');
INSERT INTO public.sante VALUES (3, 'Ostie');


--
-- Data for Name: service; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.service VALUES (10, 'direction', 'recherche.png', 16);
INSERT INTO public.service VALUES (1, 'Achats', 'achats.png', 4);
INSERT INTO public.service VALUES (3, 'Informatique', 'IT.png', 9);
INSERT INTO public.service VALUES (4, 'Logistique', 'logistique.png', 10);
INSERT INTO public.service VALUES (5, 'Production industrielle', 'production.png', 12);
INSERT INTO public.service VALUES (6, 'Recherche et developpement', 'recherche.png', 8);
INSERT INTO public.service VALUES (7, 'Ressources humaines', 'rh.png', 15);
INSERT INTO public.service VALUES (8, 'Maintenance et Reparation', 'maintenance.png', 6);
INSERT INTO public.service VALUES (9, 'Marketing', 'marketing.png', 7);
INSERT INTO public.service VALUES (2, 'Finance', 'finance.png', 11);


--
-- Data for Name: travail; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: travail_sante; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: type_contrat; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.type_contrat VALUES (1, 'contrat  a duree determinee');
INSERT INTO public.type_contrat VALUES (2, 'contrat a  duree indeterminee');
INSERT INTO public.type_contrat VALUES (3, 'contrat temporaire');


--
-- Data for Name: typecritere; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.typecritere VALUES (1, 'genre');
INSERT INTO public.typecritere VALUES (2, 'nationalite');
INSERT INTO public.typecritere VALUES (3, 'diplome');
INSERT INTO public.typecritere VALUES (4, 'experience');
INSERT INTO public.typecritere VALUES (5, 'situation matrimoniale');


--
-- Data for Name: typeuser; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.typeuser VALUES (1, 'Achats');
INSERT INTO public.typeuser VALUES (2, 'Finance');
INSERT INTO public.typeuser VALUES (3, 'Informatique');
INSERT INTO public.typeuser VALUES (4, 'Logistique');
INSERT INTO public.typeuser VALUES (5, 'Production industrielle');
INSERT INTO public.typeuser VALUES (6, 'Recherche et developpement');
INSERT INTO public.typeuser VALUES (7, 'Ressources humaines');
INSERT INTO public.typeuser VALUES (8, 'Maintenance et Reparation');
INSERT INTO public.typeuser VALUES (9, 'Marketing');
INSERT INTO public.typeuser VALUES (10, 'directeur');


--
-- Data for Name: useradmin; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.useradmin VALUES (4, 'Garcia', '12345', 3, 5);
INSERT INTO public.useradmin VALUES (5, 'Ralph', '12345', 7, 13);
INSERT INTO public.useradmin VALUES (6, 'Wilson', '12345', 5, 6);
INSERT INTO public.useradmin VALUES (3, 'info', 'info', 3, 2);
INSERT INTO public.useradmin VALUES (1, 'directeur', 'directeur', 10, 16);
INSERT INTO public.useradmin VALUES (2, 'finance', 'finance', 2, 1);
INSERT INTO public.useradmin VALUES (18, 'John', '12345', 2, 1);
INSERT INTO public.useradmin VALUES (19, 'Robert', '12345', 2, 3);
INSERT INTO public.useradmin VALUES (20, 'Sarah', '12345', 5, 4);
INSERT INTO public.useradmin VALUES (21, 'Wei', '12345', 5, 7);
INSERT INTO public.useradmin VALUES (22, 'Luis', '12345', 6, 8);
INSERT INTO public.useradmin VALUES (23, 'Susan', '12345', 3, 9);
INSERT INTO public.useradmin VALUES (24, 'Min-Ji', '12345', 4, 10);
INSERT INTO public.useradmin VALUES (25, 'Thi', '12345', 2, 11);
INSERT INTO public.useradmin VALUES (26, 'William', '12345', 5, 12);
INSERT INTO public.useradmin VALUES (27, 'Ravalison', '12345', 7, 14);
INSERT INTO public.useradmin VALUES (28, 'Goore', '12345', 7, 15);


--
-- Name: avantage_idavantage_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.avantage_idavantage_seq', 3, true);


--
-- Name: besoin_idbesoin_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.besoin_idbesoin_seq', 1, false);


--
-- Name: candidat_idcandidat_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.candidat_idcandidat_seq', 1, false);


--
-- Name: candidature_idcanditature_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.candidature_idcanditature_seq', 1, false);


--
-- Name: choix_idchoix_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.choix_idchoix_seq', 17, true);


--
-- Name: conge_idconge_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.conge_idconge_seq', 66, true);


--
-- Name: contrat_essai_idcontrat_essai_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.contrat_essai_idcontrat_essai_seq', 1, false);


--
-- Name: contrat_travail_idcontrat_travail_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.contrat_travail_idcontrat_travail_seq', 1, false);


--
-- Name: critere_idcritere_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.critere_idcritere_seq', 1, false);


--
-- Name: essai_idessai_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.essai_idessai_seq', 1, false);


--
-- Name: info_idinfo_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.info_idinfo_seq', 1, false);


--
-- Name: option_idoption_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.option_idoption_seq', 1, false);


--
-- Name: personnel_embauche_idpersonnel_embauche_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.personnel_embauche_idpersonnel_embauche_seq', 29, true);


--
-- Name: personnel_idpersonnel_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.personnel_idpersonnel_seq', 16, true);


--
-- Name: personnel_salaire_idpersonnel_salaire_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.personnel_salaire_idpersonnel_salaire_seq', 28, true);


--
-- Name: planning_visible_idpv_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.planning_visible_idpv_seq', 38, true);


--
-- Name: poste_idposte_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.poste_idposte_seq', 92, true);


--
-- Name: question_entretien_idquestion_entretien_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_entretien_idquestion_entretien_seq', 1, false);


--
-- Name: question_idquestion_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_idquestion_seq', 1, false);


--
-- Name: questionnaire_idquestionnaire_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.questionnaire_idquestionnaire_seq', 1, false);


--
-- Name: raison_idraison_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.raison_idraison_seq', 3, true);


--
-- Name: refus_idrefus_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.refus_idrefus_seq', 3, true);


--
-- Name: sante_idsante_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.sante_idsante_seq', 3, true);


--
-- Name: service_idservice_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.service_idservice_seq', 10, true);


--
-- Name: travail_idtravail_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.travail_idtravail_seq', 1, false);


--
-- Name: type_contrat_idtypecontrat_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.type_contrat_idtypecontrat_seq', 3, true);


--
-- Name: typecritere_idtypecritere_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.typecritere_idtypecritere_seq', 5, true);


--
-- Name: typeuser_idtypeuser_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.typeuser_idtypeuser_seq', 1, false);


--
-- Name: useradmin_idadmin_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.useradmin_idadmin_seq', 28, true);


--
-- Name: avantage avantage_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.avantage
    ADD CONSTRAINT avantage_pkey PRIMARY KEY (idavantage);


--
-- Name: besoin besoin_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.besoin
    ADD CONSTRAINT besoin_pkey PRIMARY KEY (idbesoin);


--
-- Name: candidat candidat_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.candidat
    ADD CONSTRAINT candidat_pkey PRIMARY KEY (idcandidat);


--
-- Name: candidature candidature_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.candidature
    ADD CONSTRAINT candidature_pkey PRIMARY KEY (idcandidature);


--
-- Name: choix choix_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.choix
    ADD CONSTRAINT choix_pkey PRIMARY KEY (idchoix);


--
-- Name: conge conge_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conge
    ADD CONSTRAINT conge_pkey PRIMARY KEY (idconge);


--
-- Name: contrat_essai contrat_essai_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contrat_essai
    ADD CONSTRAINT contrat_essai_pkey PRIMARY KEY (idcontrat_essai);


--
-- Name: contrat_travail contrat_travail_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contrat_travail
    ADD CONSTRAINT contrat_travail_pkey PRIMARY KEY (idcontrat_travail);


--
-- Name: critere critere_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.critere
    ADD CONSTRAINT critere_pkey PRIMARY KEY (idcritere);


--
-- Name: essai essai_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.essai
    ADD CONSTRAINT essai_pkey PRIMARY KEY (idessai);


--
-- Name: info info_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.info
    ADD CONSTRAINT info_pkey PRIMARY KEY (idinfo);


--
-- Name: option option_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.option
    ADD CONSTRAINT option_pkey PRIMARY KEY (idoption);


--
-- Name: personnel_embauche personnel_embauche_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel_embauche
    ADD CONSTRAINT personnel_embauche_pkey PRIMARY KEY (idpersonnel_embauche);


--
-- Name: personnel personnel_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel
    ADD CONSTRAINT personnel_pkey PRIMARY KEY (idpersonnel);


--
-- Name: personnel_salaire personnel_salaire_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel_salaire
    ADD CONSTRAINT personnel_salaire_pkey PRIMARY KEY (idpersonnel_salaire);


--
-- Name: planning_visible planning_visible_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.planning_visible
    ADD CONSTRAINT planning_visible_pkey PRIMARY KEY (idpv);


--
-- Name: poste poste_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.poste
    ADD CONSTRAINT poste_pkey PRIMARY KEY (idposte);


--
-- Name: question_entretien question_entretien_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_entretien
    ADD CONSTRAINT question_entretien_pkey PRIMARY KEY (idquestion_entretien);


--
-- Name: question question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question
    ADD CONSTRAINT question_pkey PRIMARY KEY (idquestion);


--
-- Name: questionnaire questionnaire_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.questionnaire
    ADD CONSTRAINT questionnaire_pkey PRIMARY KEY (idquestionnaire);


--
-- Name: raison raison_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.raison
    ADD CONSTRAINT raison_pkey PRIMARY KEY (idraison);


--
-- Name: refus refus_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refus
    ADD CONSTRAINT refus_pkey PRIMARY KEY (idrefus);


--
-- Name: sante sante_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sante
    ADD CONSTRAINT sante_pkey PRIMARY KEY (idsante);


--
-- Name: service service_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service
    ADD CONSTRAINT service_pkey PRIMARY KEY (idservice);


--
-- Name: travail travail_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.travail
    ADD CONSTRAINT travail_pkey PRIMARY KEY (idtravail);


--
-- Name: type_contrat type_contrat_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.type_contrat
    ADD CONSTRAINT type_contrat_pkey PRIMARY KEY (idtypecontrat);


--
-- Name: typecritere typecritere_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.typecritere
    ADD CONSTRAINT typecritere_pkey PRIMARY KEY (idtypecritere);


--
-- Name: typeuser typeuser_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.typeuser
    ADD CONSTRAINT typeuser_pkey PRIMARY KEY (idtypeuser);


--
-- Name: useradmin useradmin_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.useradmin
    ADD CONSTRAINT useradmin_pkey PRIMARY KEY (idadmin);


--
-- Name: useradmin admin_service_idadmin_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.useradmin
    ADD CONSTRAINT admin_service_idadmin_fkey FOREIGN KEY (idtypeuser) REFERENCES public.typeuser(idtypeuser);


--
-- Name: admin_service admin_service_idadmin_fkey2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin_service
    ADD CONSTRAINT admin_service_idadmin_fkey2 FOREIGN KEY (idtypeuser) REFERENCES public.service(idservice);


--
-- Name: admin_service admin_service_idservice_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin_service
    ADD CONSTRAINT admin_service_idservice_fkey FOREIGN KEY (idservice) REFERENCES public.service(idservice);


--
-- Name: besoin besoin_idposte_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.besoin
    ADD CONSTRAINT besoin_idposte_fkey FOREIGN KEY (idposte) REFERENCES public.poste(idposte);


--
-- Name: besoin besoin_idtypecontrat_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.besoin
    ADD CONSTRAINT besoin_idtypecontrat_fkey FOREIGN KEY (idtypecontrat) REFERENCES public.type_contrat(idtypecontrat);


--
-- Name: candidature candidature_idbesoin_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.candidature
    ADD CONSTRAINT candidature_idbesoin_fkey FOREIGN KEY (idbesoin) REFERENCES public.besoin(idbesoin);


--
-- Name: candidature candidature_idcandidat_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.candidature
    ADD CONSTRAINT candidature_idcandidat_fkey FOREIGN KEY (idcandidat) REFERENCES public.candidat(idcandidat);


--
-- Name: choix choix_idtypecritere_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.choix
    ADD CONSTRAINT choix_idtypecritere_fkey FOREIGN KEY (idtypecritere) REFERENCES public.typecritere(idtypecritere);


--
-- Name: choixcandidature choixcandidature_idcanditature_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.choixcandidature
    ADD CONSTRAINT choixcandidature_idcanditature_fkey FOREIGN KEY (idcandidature) REFERENCES public.candidature(idcandidature);


--
-- Name: choixcandidature choixcandidature_idchoix_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.choixcandidature
    ADD CONSTRAINT choixcandidature_idchoix_fkey FOREIGN KEY (idchoix) REFERENCES public.choix(idchoix);


--
-- Name: conge conge_idpersonnel_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conge
    ADD CONSTRAINT conge_idpersonnel_fkey FOREIGN KEY (idpersonnel) REFERENCES public.personnel(idpersonnel);


--
-- Name: conge conge_idraison_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conge
    ADD CONSTRAINT conge_idraison_fkey FOREIGN KEY (idraison) REFERENCES public.raison(idraison);


--
-- Name: contrat_essai contrat_essai_idessai_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contrat_essai
    ADD CONSTRAINT contrat_essai_idessai_fkey FOREIGN KEY (idessai) REFERENCES public.essai(idessai);


--
-- Name: contrat_travail contrat_travail_idtravail_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.contrat_travail
    ADD CONSTRAINT contrat_travail_idtravail_fkey FOREIGN KEY (idtravail) REFERENCES public.travail(idtravail);


--
-- Name: critere critere_idbesoin_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.critere
    ADD CONSTRAINT critere_idbesoin_fkey FOREIGN KEY (idbesoin) REFERENCES public.besoin(idbesoin);


--
-- Name: critere critere_idtypecritere_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.critere
    ADD CONSTRAINT critere_idtypecritere_fkey FOREIGN KEY (idtypecritere) REFERENCES public.typecritere(idtypecritere);


--
-- Name: criterechoix criterechoix_idchoix_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.criterechoix
    ADD CONSTRAINT criterechoix_idchoix_fkey FOREIGN KEY (idchoix) REFERENCES public.choix(idchoix);


--
-- Name: criterechoix criterechoix_idcritere_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.criterechoix
    ADD CONSTRAINT criterechoix_idcritere_fkey FOREIGN KEY (idcritere) REFERENCES public.critere(idcritere);


--
-- Name: essai_avantage essai_avantage_idavantage_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.essai_avantage
    ADD CONSTRAINT essai_avantage_idavantage_fkey FOREIGN KEY (idavantage) REFERENCES public.avantage(idavantage);


--
-- Name: essai_avantage essai_avantage_idessai_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.essai_avantage
    ADD CONSTRAINT essai_avantage_idessai_fkey FOREIGN KEY (idessai) REFERENCES public.essai(idessai);


--
-- Name: essai essai_idbesoin_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.essai
    ADD CONSTRAINT essai_idbesoin_fkey FOREIGN KEY (idbesoin) REFERENCES public.besoin(idbesoin);


--
-- Name: essai essai_idcandidat_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.essai
    ADD CONSTRAINT essai_idcandidat_fkey FOREIGN KEY (idcandidat) REFERENCES public.candidat(idcandidat);


--
-- Name: fichier fichier_idcanditature_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fichier
    ADD CONSTRAINT fichier_idcanditature_fkey FOREIGN KEY (idcandidature) REFERENCES public.candidature(idcandidature);


--
-- Name: info info_idcandidat_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.info
    ADD CONSTRAINT info_idcandidat_fkey FOREIGN KEY (idcandidat) REFERENCES public.candidat(idcandidat);


--
-- Name: note_entretien note_entretien_idcandidature_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.note_entretien
    ADD CONSTRAINT note_entretien_idcandidature_fkey FOREIGN KEY (idcandidature) REFERENCES public.candidature(idcandidature);


--
-- Name: note_entretien note_entretien_idquestion_entretien_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.note_entretien
    ADD CONSTRAINT note_entretien_idquestion_entretien_fkey FOREIGN KEY (idquestion_entretien) REFERENCES public.question_entretien(idquestion_entretien);


--
-- Name: option option_idquestion_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.option
    ADD CONSTRAINT option_idquestion_fkey FOREIGN KEY (idquestion) REFERENCES public.question(idquestion);


--
-- Name: personnel_embauche personnel_embauche_idpersonnel_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel_embauche
    ADD CONSTRAINT personnel_embauche_idpersonnel_fkey FOREIGN KEY (idpersonnel) REFERENCES public.personnel(idpersonnel);


--
-- Name: personnel_poste personnel_poste_idpersonnel_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel_poste
    ADD CONSTRAINT personnel_poste_idpersonnel_fkey FOREIGN KEY (idpersonnel) REFERENCES public.personnel(idpersonnel);


--
-- Name: personnel_poste personnel_poste_idposte_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel_poste
    ADD CONSTRAINT personnel_poste_idposte_fkey FOREIGN KEY (idposte) REFERENCES public.poste(idposte);


--
-- Name: personnel_salaire personnel_salaire_idpersonnel_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.personnel_salaire
    ADD CONSTRAINT personnel_salaire_idpersonnel_fkey FOREIGN KEY (idpersonnel) REFERENCES public.personnel(idpersonnel);


--
-- Name: planning_visible planning_visible_idservice_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.planning_visible
    ADD CONSTRAINT planning_visible_idservice_fkey FOREIGN KEY (idservice) REFERENCES public.service(idservice);


--
-- Name: planning_visible planning_visible_idvisible_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.planning_visible
    ADD CONSTRAINT planning_visible_idvisible_fkey FOREIGN KEY (idvisible) REFERENCES public.service(idservice);


--
-- Name: question_entretien question_entretien_idbesoin_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_entretien
    ADD CONSTRAINT question_entretien_idbesoin_fkey FOREIGN KEY (idbesoin) REFERENCES public.besoin(idbesoin);


--
-- Name: question question_idquestionnaire_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question
    ADD CONSTRAINT question_idquestionnaire_fkey FOREIGN KEY (idquestionnaire) REFERENCES public.questionnaire(idquestionnaire);


--
-- Name: questionnaire questionnaire_idbesoin_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.questionnaire
    ADD CONSTRAINT questionnaire_idbesoin_fkey FOREIGN KEY (idbesoin) REFERENCES public.besoin(idbesoin);


--
-- Name: refus refus_idconge_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refus
    ADD CONSTRAINT refus_idconge_fkey FOREIGN KEY (idconge) REFERENCES public.conge(idconge);


--
-- Name: refus refus_idservice_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refus
    ADD CONSTRAINT refus_idservice_fkey FOREIGN KEY (idservice) REFERENCES public.service(idservice);


--
-- Name: service service_superieur_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service
    ADD CONSTRAINT service_superieur_fkey FOREIGN KEY (superieur) REFERENCES public.personnel(idpersonnel);


--
-- Name: travail travail_idcontrat_essai_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.travail
    ADD CONSTRAINT travail_idcontrat_essai_fkey FOREIGN KEY (idcontrat_essai) REFERENCES public.contrat_essai(idcontrat_essai);


--
-- Name: travail_sante travail_sante_idcontrat_travail_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.travail_sante
    ADD CONSTRAINT travail_sante_idcontrat_travail_fkey FOREIGN KEY (idcontrat_travail) REFERENCES public.contrat_travail(idcontrat_travail);


--
-- Name: travail_sante travail_sante_idsante_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.travail_sante
    ADD CONSTRAINT travail_sante_idsante_fkey FOREIGN KEY (idsante) REFERENCES public.sante(idsante);


--
-- Name: useradmin useradmin_idpersonnel_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.useradmin
    ADD CONSTRAINT useradmin_idpersonnel_fkey FOREIGN KEY (idpersonnel) REFERENCES public.personnel(idpersonnel);


--
-- Name: useradmin useradmin_idtypeuser_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.useradmin
    ADD CONSTRAINT useradmin_idtypeuser_fkey FOREIGN KEY (idtypeuser) REFERENCES public.typeuser(idtypeuser);


--
-- PostgreSQL database dump complete
--

