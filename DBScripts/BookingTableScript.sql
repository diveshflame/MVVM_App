-- Table: public.booking_table

-- DROP TABLE IF EXISTS public.booking_table;

CREATE TABLE IF NOT EXISTS public.booking_table
(
    booking_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 3001 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    userid integer NOT NULL,
    doctor_id integer NOT NULL,
    consultant_id integer NOT NULL,
    starttime timestamp without time zone NOT NULL,
    endtime timestamp without time zone NOT NULL,
    deleted_timestamp timestamp without time zone,
    CONSTRAINT booking_table_pkey PRIMARY KEY (booking_id),
    CONSTRAINT booking_table_consultant_id_fkey FOREIGN KEY (consultant_id)
        REFERENCES public.consultant_type (consultant_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT booking_table_doctor_id_fkey FOREIGN KEY (doctor_id)
        REFERENCES public.doctor_table (doctor_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT booking_table_userid_fkey FOREIGN KEY (userid)
        REFERENCES public.userdetails (userid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.booking_table
    OWNER to postgres;