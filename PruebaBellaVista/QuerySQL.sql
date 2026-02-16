Create database BellaVistaDB
use bellavistaDB

create table tipocafe(
id_tipocafe int primary key identity(1,1),
nombre_cafe VARCHAR(75)not null,
);

create table Producto(
id_producto int primary key identity(1,1),
nombre_producto VARCHAR(75),
presentacion VARCHAR(100),
precio decimal(10,2)not null,
id_tipocafe int not null,
constraint FK_producto_tipocafe foreign key (id_tipocafe) references tipocafe(id_tipocafe)
);

insert into 
tipocafe (nombre_cafe)
values
('Arábica'), ('Robusta'), ('Mezcla');

insert into
Producto (nombre_producto, presentacion, precio, id_tipocafe)
values 
('Café Premium Especial', 'Bolsa 250g', 45.00, 1)
ALTER TABLE Producto ADD Activo BIT DEFAULT 1 NOT NULL;

		
select * from producto
select * from tipocafe
drop table presentaciones