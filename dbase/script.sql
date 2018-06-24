create table cliente
(
  idcliente integer NOT NULL,
  tdoc character varying(3),
  ndoc character varying(11),
  razon_social character varying(100),
  nombre_comercial character varying(100),
  direccion character varying(150),
  telefono character varying(50),
  correo character varying(50),
  constraint pk_cliente_idcliente primary key (idcliente),
  constraint uq_cliente_ndoc unique(tdoc,ndoc)
)
WITH (
OIDS=FALSE
);


CREATE OR REPLACE FUNCTION fu_cliente(
    id_ integer,
    tdoc_ character varying,
    ndoc_ character varying,
    razon_social_ character varying,
    nombre_comercial_ character varying,
    direccion_ character varying,
    telefono_ character varying,
    correo_ character varying,
    ope character varying)
returns integer as
$BODY$
BEGIN    
	if ope = 'INS' then
		id_=(select (coalesce(max(idcliente),0) + 1) from cliente);
		insert into cliente values(id_,tdoc_,ndoc_,razon_social_,nombre_comercial_,
		direccion_, telefono_, correo_);
	else
	 	update 	cliente Set 
			tdoc=tdoc_,
			ndoc=ndoc_,
			razon_social=razon_social_,
			nombre_comercial=nombre_comercial_,
			direccion=direccion_,
			telefono=telefono_,
			correo=correo_
		where	idcliente=id_;
	end if;
	return(id_);
END;
$BODY$
LANGUAGE plpgsql VOLATILE
COST 100;


create table producto
(
  idproducto integer not null,
  cbarras character varying(18),
  nombre character varying(80) not null,
  stock integer not null,
  stock_f character varying(8) not null,
  pcompra numeric(6,2) not null,
  pventa numeric(6,2) not null,
  CONSTRAINT pk_producto_idproducto primary key (idproducto)
)
WITH (
OIDS=FALSE
);


CREATE OR REPLACE FUNCTION fu_producto(
    id_ integer,
    cbarras_ character varying,
    nombre_ character varying,
    stock_ integer,
    stock_f_ character varying,
    pcompra_ numeric,
    pventa_ numeric,
    ope character varying)
returns integer as
$BODY$
BEGIN    
	if ope = 'INS' then
		id_=(select (coalesce(max(idproducto),0) + 1) from producto);
		insert into producto values(id_,cbarras_,nombre_,stock_,stock_f_,
		pcompra_, pventa_);
	else
	 	update 	producto Set 
			cbarras=cbarras_,
			nombre=nombre_,
			stock=stock_,
			stock_f=stock_f_,
			pcompra=pcompra_,
			pventa=pventa_
		where	idproducto=id_;
	end if;
	return(id_);
END;
$BODY$
LANGUAGE plpgsql VOLATILE
COST 100;


create table cotizacion
(
  idcotizacion integer not null,
  idcliente integer not null,
  fecha timestamp without time zone not null,
  total numeric(10,2) not null,
  constraint pk_cotizacion_idcotizacion primary key (idcotizacion),
  constraint fk_cotizacion_idcliente foreign key (idcliente)
      references cliente (idcliente) match simple
      on update no action on delete no action,
  constraint ck_cotizacion_total check(total > 0)
)
WITH (
  OIDS=FALSE
);


create table cotizacion_deta
(
  idcotizacion integer NOT NULL,
  idproducto integer NOT NULL,
  item integer not null,
  pventa numeric(6,2) not null,
  cantidad integer not null,
  monto numeric(6,2) not null,
  constraint pk_cotizacion_deta_idcotizacion primary key (idcotizacion),
  constraint fk_cotizacion_deta_idproducto foreign key (idproducto)
      references cotizacion (idcotizacion) match simple
      on update cascade on delete cascade
)
WITH (
  OIDS=FALSE
);

