create database Ecommerce

use Ecommerce


create table Cars (
    car_id int primary key identity (1, 1),
    brand nvarchar(10) NOT NULL unique check (brand is NOT NULL),
    model nvarchar(20) NOT NULL check (model is NOT NULL),
    year int NOT NULL check (year is NOT NULL),
    fuel_type_id int foreign key references FuelTypes(fuel_type_id),
    body_type_id int foreign key references BodyTypes(body_type_id),
    color_id int foreign key references Colors(color_id),
    image_link nvarchar(200)
);

create table Users (
    user_id int primary key identity (1, 1),
    username nvarchar(100) NOT NULL unique check (username is NOT NULL),
    password nvarchar(250) NOT NULL check (password is NOT NULL),
    email nvarchar(100) NOT NULL unique check (email is NOT NULL)
);

create table ProductList (
    product_id int primary key identity (1, 1),
    car_id int foreign key references Cars(car_id),
    seller_id int foreign key references Sellers(seller_id),
    price decimal NOT NULL check (price is NOT NULL),
    quantity int NOT NULL check (quantity is NOT NULL)
);

create table ManufacturingCountries (
    country_id int primary key identity (1, 1),
    country_name nvarchar(100) NOT NULL check (country_name is NOT NULL)
);

create table FuelTypes (
    fuel_type_id int primary key identity (1, 1),
    fuel_type nvarchar(100) NOT NULL check (fuel_type is NOT NULL),
);

create table BodyTypes (
    body_type_id int primary key identity (1, 1),
    body_type nvarchar(100) NOT NULL check (body_type is NOT NULL)
);

create table Colors (
    color_id int primary key identity (1, 1),
    color_name nvarchar(100) NOT NULL check(color_name is NOT NULL)
);

create table Sellers (
    seller_id int primary key identity (1, 1),
    users_id int foreign key references Users(user_id),
    company_name nvarchar(100) NOT NULL check (company_name is NOT NULL),
    contact_number nvarchar(20) NOT NULL check (contact_number is NOT NULL),
    country_id int foreign key references ManufacturingCountries(country_id)
);

SET IDENTITY_INSERT FuelTypes ON
insert into FuelTypes (fuel_type_id, fuel_type)
values (1, 'Дизель'),
       (2, 'Бензин'),
       (3, 'Электрический'),
       (4, 'Газ')
SET IDENTITY_INSERT FuelTypes OFF

SET IDENTITY_INSERT BodyTypes ON
insert into BodyTypes(body_type_id, body_type)
values (1, 'SUV'),
       (2, 'Седан'),
       (3, 'Хэтчбек')
SET IDENTITY_INSERT BodyTypes OFF

SET IDENTITY_INSERT Colors ON
insert into Colors(color_id, color_name)
values (1, 'Красный'),
       (2 ,'Синий'),
       (3, 'Черный'),
       (4, 'Оранжевый')
SET IDENTITY_INSERT Colors OFF


insert into Cars(brand, model, year, fuel_type_id, body_type_id, color_id, image_link)
values ('Audi', 'RS6', 2022, 2, 3, 3, 'https://www.carpixel.net/w/73ae8927038d1a41d750ed6f12e06cce/audi-rs-6-avant-performance-wallpaper-hd-117228.jpg'),
       ('Toyota', 'Prius', 2023, 3, 2, 2, 'https://avatars.dzeninfra.ru/get-zen_doc/1596193/pub_63da32783a663b6c8aa60679_63da32899e99e03fc14b0e54/scale_1200'),
       ('Mercedes', 'E200d', 2022, 1, 2, 3, 'https://1gai.ru/uploads/posts/2020-03/1583237123_16.jpg'),
       ('KIA', 'Sorento', 2016, 1, 1, 3, 'https://ymimg1.b8cdn.com/resized/car_version/5072/pictures/3128939/listing_main_10256_st1280_116.jpg'),
       ('Opel', 'Astra', 2015, 1, 2, 1, 'https://img.razrisyika.ru/kart/52/207959-astra-opel-36.jpg')


insert into Users(username, password, email)
VALUES ('gagulik', 'varavskoy228', 'babushka21232@yahoo.com'),
       ('polkastravelogue', '9WhJX0GWqe', 'polkastravelogue2011@mail.com'),
       ('betterfrancophil', '2P2sfYMIRP', 'betterfranchophil@mail.com'),
       ('tonantseychelles', 'kg200jmu', 'tonant100@gmail.com'),
       ('agamasneogenesis', 'ndYci8HwK', 'delaeme63scls63@gmail.com')


insert into ProductList(car_id, price, quantity)
VALUES (1, 200000, 1),
       (2, 12300, 3),
       (3, 34000, 2),
       (4, 26500, 4),
       (5, 34000, 4)

SET IDENTITY_INSERT ManufacturingCountries ON
insert into ManufacturingCountries(country_id, country_name)
VALUES (1, 'Германия'),
       (2, 'Америка'),
       (3, 'Германия'),
       (4, 'Корея'),
       (5, 'Америка')
SET IDENTITY_INSERT ManufacturingCountries OFF

insert into Sellers(users_id, company_name, contact_number, country_id)
VALUES (1, 'Audi', '+49 171 08853282', 1),
       (2, 'Toyota', '+81 90-9520-9103', 2),
       (3, 'Mercedes', '+49 15585 709840', 3),
       (4, 'KIA', '+994124250151', 4),
       (5, 'Opel', '+994123100785', 5)

select U.username as 'Имя пользователя', U.email as 'Электронная Почта'
from Users U

select C.car_id as 'ID', C.brand as 'Бренд', C.model as 'Модель',
       P.price as 'Цена',
       M.country_name as 'Страна', F.fuel_type as 'Тип Топлива', B.body_Type as 'Тип Кузова',
       S.contact_number as 'Номер дилера'
from Cars C
inner join FuelTypes F on C.fuel_type_id = F.fuel_type_id
inner join BodyTypes B on C.body_type_id = B.body_type_id
inner join Sellers S on B.body_type_id = S.country_id
inner join ManufacturingCountries M on S.country_id = M.country_id
inner join ProductList P on C.car_id = P.car_id

insert into ProductList (car_id, price, quantity)
VALUES (1, 28000, 2)

drop table ManufacturingCountries
drop table Sellers
drop table ProductList


select C.car_id as 'ID', C.brand as 'Бренд', C.model as 'Модель', P.price as 'Цена', C2.color_name as 'Цвет',
       M.country_name as 'Страна', F.fuel_type as 'Тип Топлива', B.body_Type as 'Тип Кузова',
       S.contact_number as 'Номер дилера'
from Cars C
inner join FuelTypes F on C.fuel_type_id = F.fuel_type_id
inner join BodyTypes B on C.body_type_id = B.body_type_id
inner join Sellers S on B.body_type_id = S.country_id
inner join ManufacturingCountries M on S.country_id = M.country_id
inner join ProductList P on C.car_id = P.car_id
inner join Colors C2 on C.color_id = C2.color_id


se