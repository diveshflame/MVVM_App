-- Table: public.userdetails

-- DROP TABLE IF EXISTS public.userdetails;

CREATE TABLE IF NOT EXISTS public.userdetails
(
    userid integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 101 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    username character varying(20) COLLATE pg_catalog."default" NOT NULL,
    name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    age integer NOT NULL,
    gender character varying(20) COLLATE pg_catalog."default" NOT NULL,
    email character varying(30) COLLATE pg_catalog."default" NOT NULL,
    phone_number character varying(10) COLLATE pg_catalog."default" NOT NULL,
    password character varying(20) COLLATE pg_catalog."default" NOT NULL,
    active_session integer DEFAULT 0,
    super_user integer DEFAULT 0,
    CONSTRAINT userdetails_pkey PRIMARY KEY (userid),
    CONSTRAINT userdetails_username_key UNIQUE (username),
    CONSTRAINT userdetails_age_check CHECK (age > 15 AND age < 120)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.userdetails
    OWNER to postgres;