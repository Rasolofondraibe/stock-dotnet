/*
    Base de donnee  : stock
*/

CREATE TABLE article (
    idarticle VARCHAR(50) PRIMARY KEY,
    nomarticle VARCHAR(50),
    type VARCHAR(10)
);

CREATE TABLE magasin (
    idmagasin VARCHAR(50) DEFAULT 'magasin' || nextval('magasinsequence')::TEXT PRIMARY KEY,
    nommagasin VARCHAR(50),
    lieu VARCHAR(50)
);

CREATE TABLE mouvement(
    idmouvement VARCHAR(50) DEFAULT 'mouvement' || nextval('mouvementsequence')::TEXT PRIMARY KEY,
    date DATE,
    idarticle VARCHAR(50),
    quantiteentree DOUBLE PRECISION,
    quantitesortie DOUBLE PRECISION,
    prixunitaire DOUBLE PRECISION,
    idmagasin VARCHAR(50),
    FOREIGN KEY(idarticle) REFERENCES article(idarticle),
    FOREIGN KEY(idmagasin) REFERENCES magasin(idmagasin)
);

CREATE TABLE sortie (
    sortie VARCHAR(50),
    entree VARCHAR(50),
    difference DOUBLE PRECISION,
    reste DOUBLE PRECISION,
    FOREIGN KEY(sortie) REFERENCES mouvement(idmouvement),
    FOREIGN KEY(entree) REFERENCES mouvement(idmouvement)
);