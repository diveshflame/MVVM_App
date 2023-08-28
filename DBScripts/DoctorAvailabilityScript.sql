-- Table: public.doctor_availability

-- DROP TABLE IF EXISTS public.doctor_availability;

CREATE TABLE IF NOT EXISTS public.doctor_availability
(
    doctor_id integer NOT NULL,
    available_starttime timestamp without time zone NOT NULL,
    available_endtime timestamp without time zone NOT NULL,
    CONSTRAINT doctor_availability_doctor_id_fkey FOREIGN KEY (doctor_id)
        REFERENCES public.doctor_table (doctor_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.doctor_availability
    OWNER to postgres;