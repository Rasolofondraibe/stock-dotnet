INSERT INTO article VALUES ('article1000','riz','fifo'),('article1100','rizdeluxe','fifo'),
('article2000','vetement','lifo'),('article2100','t-shirt','lifo')
;

INSERT INTO magasin(nommagasin,lieu) VALUES ('magasin','ambatondrazaka')
;


INSERT INTO mouvement(date,idarticle,quantiteentree,quantitesortie,prixunitaire,idmagasin) 
VALUES 
('2023-11-01','article1100',20,0,2000,'magasin1'),
('2023-11-05','article1100',10,0,2000,'magasin1'),
('2023-11-06','article1100',0,25,2000,'magasin1')
;

INSERT INTO reste VALUES 
('mouvement11','2023-11-01',20),
('mouvement12','2023-11-05',10),
('mouvement11','2023-11-06',0),
('mouvement12','2023-11-06',5),
('mouvement13','2023-11-06',5)
;