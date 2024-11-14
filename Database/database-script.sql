CREATE TABLE restaurant (
    id UUID primary key NOT NULL,
    "name" VARCHAR(255) NOT NULL
);

CREATE TABLE menu (
    id UUID primary key NOT NULL,
    restaurant UUID NOT NULL
);

CREATE TABLE dish (
    id UUID primary key NOT NULL,
    "name" VARCHAR(255) NOT NULL,
    Menu UUID NOT NULL
);

ALTER TABLE menu ADD CONSTRAINT fk_restaurant FOREIGN KEY (restaurant) REFERENCES restaurant(id);
ALTER TABLE dish ADD CONSTRAINT fk_Menu FOREIGN KEY (Menu) REFERENCES menu(id);