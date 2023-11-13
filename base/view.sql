CREATE VIEW etatdestock as
SELECT idmouvement,date,m.idarticle,
(SELECT SUM(m1.quantiteentree)-m.quantitesortie FROM mouvement m1 WHERE m1.date<=m.date AND m1.idarticle = m.idarticle AND m1.idmagasin = m.idmagasin) as quantitereste,
(SELECT SUM(m1.prixunitaire)/COUNT(*) FROM mouvement m1 WHERE m1.date<=m.date AND m1.idarticle = m.idarticle AND m1.idmagasin = m.idmagasin) as moyenneprixunitaire,
m.idmagasin,article.nomarticle,magasin.nommagasin
FROM 
mouvement as m
JOIN article ON m.idarticle = article.idarticle
JOIN magasin ON m.idmagasin = magasin.idmagasin
;


CREATE VIEW mouvement_reste AS
SELECT 
m.*,
(SELECT reste FROM reste WHERE reste.idmouvement = m.idmouvement ORDER BY date DESC LIMIT 1) as reste
FROM mouvement as m
WHERE quantitesortie = 0
;