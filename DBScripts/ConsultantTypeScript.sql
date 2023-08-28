-- Table: public.consultant_type

-- DROP TABLE IF EXISTS public.consultant_type;

CREATE TABLE IF NOT EXISTS public.consultant_type
(
    consultant_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 501 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    consultant_desc character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT consultant_type_pkey PRIMARY KEY (consultant_id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.consultant_type
    OWNER to postgres;