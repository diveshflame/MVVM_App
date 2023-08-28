-- Table: public.doctor_table

-- DROP TABLE IF EXISTS public.doctor_table;

CREATE TABLE IF NOT EXISTS public.doctor_table
(
    doctor_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1001 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    doctor_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    consultant_id integer NOT NULL,
    CONSTRAINT doctor_table_pkey PRIMARY KEY (doctor_id),
    CONSTRAINT doctor_table_consultant_id_fkey FOREIGN KEY (consultant_id)
        REFERENCES public.consultant_type (consultant_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.doctor_table
    OWNER to postgres;