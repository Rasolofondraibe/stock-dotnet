INSERT INTO article VALUES ('article1000','riz','fifo'),('article1100','rizdeluxe','fifo'),
('article2000','vetement','lifo'),('article2100','t-shirt','lifo')
;

INSERT INTO magasin(nommagasin,lieu) VALUES ('magasin','ambatondrazaka')
;


INSERT INTO mouvement(date,idarticle,quantiteentree,quantitesortie,prixunitaire,idmagasin) 
VALUES 
('2023-11-14','article1100',20,0,2000,'magasin1'),
('2023-11-05','article1100',10,0,2000,'magasin1'),
('2023-11-06','article1100',0,25,2000,'magasin1')
;

INSERT INTO sortie VALUES 
('mouvement3','mouvement1',20,0),
('mouvement3','mouvement2',5,5)
;