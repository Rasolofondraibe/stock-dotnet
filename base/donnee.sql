INSERT INTO article VALUES ('v','vary','fifo'),
('vm','varymena','fifo'),
('vf','varyfotsy','fifo')
;

INSERT INTO magasin(nommagasin,lieu) VALUES ('magasin','ambatondrazaka')
;

INSERT INTO mouvement(date,idarticle,quantiteentree,quantitesortie,prixunitaire,idmagasin)
VALUES
('2023-11-01','vm',30,0,2000,'magasin1'),
('2023-11-05','vm',60,0,2500,'magasin1'),
('2023-11-07','vm',0,10,2600,'magasin1'),
('2023-11-08','vm',0,30,2600,'magasin1'),
('2023-11-12','vm',15,0,2200,'magasin1')
;


INSERT INTO sortie VALUES 
('mouvement13','mouvement11',10,20),
('mouvement14','mouvement11',20,0),
('mouvement14','mouvement12',10,50)
;



-------------------------------------------------------

INSERT INTO article VALUES 
('SV','vary','fifo'),
('SVM001','vary mena','fifo'),
('SVS001','vary stock','fifo'),
('SVT001','vary tsipaka','fifo'),
('SP','pate','lifo'),
('SPP001','pate presto','lifo'),
('SPS001','pate sedap','lifo')
;

INSERT INTO magasin(nommagasin,lieu) VALUES
('Magasin1','ambatondrazaka')
;

INSERT INTO mouvement(date,idarticle,quantiteentree,quantitesortie,prixunitaire,idmagasin)
VALUES
('2023-01-01','SVM001',30,0,500,'magasin2'),
('2023-01-10','SVM001',50,0,600,'magasin2'),
('2023-01-12','SVM001',10,0,700,'magasin2'),
('2023-01-02','SVT001',10,0,200,'magasin2'),
('2023-01-05','SVT001',60,0,300,'magasin2'),
('2023-01-10','SPS001',60,0,800,'magasin2'),
('2023-01-11','SPS001',10,0,1000,'magasin2'),
('2023-01-10','SPP001',20,0,900,'magasin2'),
('2023-01-11','SPP001',10,0,1000,'magasin2')
;