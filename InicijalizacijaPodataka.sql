-- inicijalno popunjavanje vozila

INSERT INTO Vozilo (Registracija)
VALUES
    ('1234AB'),
    ('5678CD'),
    ('9012EF'),
    ('3456GH'),
    ('7890IJ'),
    ('2345KL'),
    ('6789MN'),
    ('0123OP'),
    ('4567QR'),
    ('8901ST');


INSERT INTO Adresa (Ulica, Broj, Mesto)
VALUES
    ('Bulevar Oslobođenja', '12', 'Novi Sad'),
    ('Zmaj Jovina ulica', '1', 'Novi Sad'),
    ('Bulevar Despota Stefana', '15', 'Novi Sad'),
    ('Dunavska ulica', '10', 'Novi Sad'),
    ('Petrovaradinska tvrđava', '1', 'Novi Sad'),
    ('Bulevar Cara Lazara', '7', 'Novi Sad'),
    ('Trg slobode', '20', 'Novi Sad'),
    ('Futoška ulica', '5', 'Novi Sad'),
    ('Bulevar Evrope', '100', 'Novi Sad'),
    ('Njegoševa ulica', '1', 'Novi Sad');

INSERT INTO Voznja (id_vozilo, id_polazak, id_dolazak, ZavrsenaDN)
VALUES
    (1, 2, 3, 'D'),
    (2, 4, 5, 'N'),
    (3, 6, 7, 'D'),
    (4, 8, 9, 'N'),
    (5, 9, 8, 'D'),
    (6, 7, 6, 'N'),
    (7, 5, 4, 'D'),
    (8, 3, 2, 'N'),
    (9, 1, 2, 'D'),
    (10, 5, 7, 'N');