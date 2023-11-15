CREATE VIEW etatdestock as
SELECT idmouvement,date,m.idarticle,
(SELECT SUM(m1.quantiteentree)-SUM(m1.quantitesortie) FROM mouvement m1 WHERE m1.date<=m.date AND m1.idarticle = m.idarticle AND m1.idmagasin = m.idmagasin) as quantitereste,
(SELECT SUM(m1.prixunitaire*m1.quantiteentree)/SUM(m1.quantiteentree) FROM mouvement m1 WHERE m1.quantitesortie = 0 AND m1.date<=m.date AND m1.idarticle = m.idarticle AND m1.idmagasin = m.idmagasin) as moyenneprixunitaire,
m.idmagasin,article.nomarticle,magasin.nommagasin
FROM 
mouvement as m
JOIN article ON m.idarticle = article.idarticle
JOIN magasin ON m.idmagasin = magasin.idmagasin
;


CREATE VIEW mouvement_reste as 
SELECT 
    m.*,
    COALESCE(
        (SELECT reste FROM sortie as s WHERE s.entree = m.idmouvement ORDER BY date desc LIMIT 1),
        m.quantiteentree
    ) as reste
FROM mouvement as m 
WHERE quantitesortie = 0;