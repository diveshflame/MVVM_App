-- Table: public.demo_time

-- DROP TABLE IF EXISTS public.demo_time;

CREATE TABLE IF NOT EXISTS public.demo_time
(
    demostarttime time without time zone,
    demoendtime time without time zone
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.demo_time
    OWNER to postgres;
	
	
	insert into demo_time values('10:00','11:00'),('11:00','12:00'),('12:00','13:00'),('13:00','14:00'),('14:00','15:00'),('15:00','16:00'),('16:00','17:00'),('17:00','18:00');