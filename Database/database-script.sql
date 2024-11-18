CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE restaurant (
    "Id" UUID primary key DEFAULT uuid_generate_v4() NOT NULL UNIQUE, 
    "Name" VARCHAR(255) NOT NULL
);

CREATE TABLE menu (
    "Id" UUID primary key default uuid_generate_v4() NOT NULL UNIQUE,
    "RestaurantId" UUID NOT NULL
);

CREATE TABLE dish (
    "Id" UUID primary key default uuid_generate_v4() NOT NULL UNIQUE,
    "Name" VARCHAR(255) NOT NULL,
    "Price" INT NOT NULL,
    "MenuId" UUID NOT NULL
);

ALTER TABLE menu ADD CONSTRAINT fk_restaurant FOREIGN KEY ("RestaurantId") REFERENCES restaurant("Id");
ALTER TABLE dish ADD CONSTRAINT fk_Menu FOREIGN KEY ("MenuId") REFERENCES menu("Id");