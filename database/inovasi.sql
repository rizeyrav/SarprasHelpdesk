--
-- PostgreSQL database dump
--

\restrict xWXkJLyenjNqWQuMcrKa7YzDa5PdGY9b9xJW2ixJ1huXxA8heFjBGe9lyzWErPi

-- Dumped from database version 18.1
-- Dumped by pg_dump version 18.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: laporan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.laporan (
    id integer NOT NULL,
    no_tiket character varying(50) NOT NULL,
    bidang character varying(50),
    foto character varying(255),
    kategori character varying(50),
    deskripsi text,
    status character varying(50),
    dibuat_pada timestamp without time zone NOT NULL,
    pelapor character varying(100) NOT NULL
);


ALTER TABLE public.laporan OWNER TO postgres;

--
-- Name: laporan_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.laporan_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.laporan_id_seq OWNER TO postgres;

--
-- Name: laporan_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.laporan_id_seq OWNED BY public.laporan.id;


--
-- Name: perbaikan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.perbaikan (
    id integer NOT NULL,
    id_laporan integer,
    tindakan text,
    selesai_pada timestamp without time zone
);


ALTER TABLE public.perbaikan OWNER TO postgres;

--
-- Name: perbaikan_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.perbaikan_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.perbaikan_id_seq OWNER TO postgres;

--
-- Name: perbaikan_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.perbaikan_id_seq OWNED BY public.perbaikan.id;


--
-- Name: laporan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.laporan ALTER COLUMN id SET DEFAULT nextval('public.laporan_id_seq'::regclass);


--
-- Name: perbaikan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.perbaikan ALTER COLUMN id SET DEFAULT nextval('public.perbaikan_id_seq'::regclass);


--
-- Data for Name: laporan; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.laporan (id, no_tiket, bidang, foto, kategori, deskripsi, status, dibuat_pada, pelapor) FROM stdin;
3	SPR-20260608193359	Pariwisata	78ddc660-41b2-49cd-b868-7b7c2797f8e9.jpg	Printer	Printer tidak dapat diperbaiki	Menunggu	2026-06-08 19:33:59.884147	Nurul
4	SPR-20260608193731	Pariwisata	e60b5ff3-2a43-4d2d-ab7e-fcc625afe671.jpg	Printer	Printer tidak dapat diperbaiki	Menunggu	2026-06-08 19:37:31.39964	Nurul
5	SPR-20260608193839	Pariwisata	d9c9034e-9472-403c-bb1c-4dd33bb069e9.jpg	Printer	Printer tidak dapat diperbaiki	Menunggu	2026-06-08 19:38:39.263547	Nurul
6	SPR-20260608193921	Pariwisata	4e0660c5-afa2-4629-951d-f844817ef92b.jpg	Jaringan	Jaringan tidak terhubung internet	Menunggu	2026-06-08 19:39:21.63431	Zahron
2	SPR-20260608183604	Pariwisata	0e811eba-f67c-4456-83a5-c75863915c5c.JPG	Komputer/Laptop	rkua	Selesai	2026-06-08 18:36:04.777186	nuning
7	SPR-20260608212917	Kebudayaan	56b0dda2-3b50-440c-b166-70fba6896e2f.JPG	Printer	Kerusakan printer	Menunggu	2026-06-08 21:29:17.961293	Anik
\.


--
-- Data for Name: perbaikan; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.perbaikan (id, id_laporan, tindakan, selesai_pada) FROM stdin;
\.


--
-- Name: laporan_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.laporan_id_seq', 7, true);


--
-- Name: perbaikan_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.perbaikan_id_seq', 1, false);


--
-- Name: laporan laporan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.laporan
    ADD CONSTRAINT laporan_pkey PRIMARY KEY (id);


--
-- Name: perbaikan perbaikan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.perbaikan
    ADD CONSTRAINT perbaikan_pkey PRIMARY KEY (id);


--
-- Name: laporan uq_laporan_no_tiket; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.laporan
    ADD CONSTRAINT uq_laporan_no_tiket UNIQUE (no_tiket);


--
-- Name: perbaikan perbaikan_id_laporan_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.perbaikan
    ADD CONSTRAINT perbaikan_id_laporan_fkey FOREIGN KEY (id_laporan) REFERENCES public.laporan(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict xWXkJLyenjNqWQuMcrKa7YzDa5PdGY9b9xJW2ixJ1huXxA8heFjBGe9lyzWErPi

