CREATE VIEW etatdestock as
SELECT idmouvement,date,m.idarticle,
(SELECT SUM(m1.quantiteentree)-m.quantitesortie FROM mouvement m1 WHERE m1.date<=m.date) as quantitereste,
(SELECT SUM(m1.prixunitaire)/COUNT(*) FROM mouvement m1 WHERE m1.date<=m.date) as moyenneprixunitaire,
m.idmagasin,article.nomarticle,magasin.nommagasin
FROM 
mouvement as m
JOIN article ON m.idarticle = article.idarticle
JOIN magasin ON m.idmagasin = magasin.idmagasin
;