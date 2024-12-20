

create table Products(
    ProductId serial primary key,
    ProductName varchar(150),
    Price decimal,
    Stock int
);

create table Orders(
    OrderId serial primary key,
    ProductId int references Products(ProductId) on delete cascade,
    Quantity int,
    TotalPrice decimal,
    OrderDate timestamp
);