insert into Grupa (naziv) select 'Подразумевано'
insert into Grupa (naziv) select 'Група грађанско'
insert into Grupa (naziv) select 'Група верско'


insert into  Predmet (naziv) select 'Владање'
select * from Predmet where id  =10
select * from SmerGodinaPredmet

update SmerGodinaPredmet set predmetId = 2 where Id = 11
select * from Ucenik
delete UcenikGrupa

delete Ucenik 

select * from UcenikGrupa 

select * from UcenikOcena

select * from SmerGodinaPredmet where smerGodinaId = 1 and predmetId = (select Id from Predmet where upper(naziv) like 'СРПСКИ ЈЕЗИК%')

select * from DokumentTip

select * from SmerGodina 

select * from SmerGodinaDokument 
-- insert Svedoanstvo
insert into SmerGodinaDokument (smerGodinaId, dokumentTipId)
select Id, 1 
from SmerGodina

--Diploma 3 stepen
insert into SmerGodinaDokument (smerGodinaId, dokumentTipId)
select Id, 3 
from SmerGodina
where smerId = 5 and godina = 3


--Diploma 4 stepen
insert into SmerGodinaDokument (smerGodinaId, dokumentTipId)
select Id, 2 
from SmerGodina
where smerId != 5 and godina = 4


--4b
--Uverenje 4b za trgovca
insert into SmerGodinaDokument (smerGodinaId, dokumentTipId)
select Id, 5 
from SmerGodina
where smerId = 5 and godina = 3

insert into SmerGodinaDokument (smerGodinaId, dokumentTipId)
select Id, 6 
from SmerGodina
where smerId = 5 and godina = 3

--4a
--Uverenje 4a za komercijaliste
insert into SmerGodinaDokument (smerGodinaId, dokumentTipId)
select Id, 7 
from SmerGodina
where smerId = 3 and godina = 4

insert into SmerGodinaDokument (smerGodinaId, dokumentTipId)
select Id, 8 
from SmerGodina
where smerId = 3 and godina = 4


