

if object_id('DeleteStudents') is not null
drop proc dbo.DeleteStudents
go
create proc dbo.DeleteStudents 
@ClassId int
as
begin
	
	delete ug 
	from UcenikGrupa ug 
	join Ucenik u on u.Id = ug.ucenikId
	where u.razredId = @ClassId

	delete uo
	from UcenikOcena uo
	join Ucenik u on u.Id = uo.ucenikId
	where u.razredId = @ClassId

	delete Ucenik 
	where razredId = @ClassId

	Select 0 as ErrCode, 'OK' as ErrMessage

end
go

if object_id('DeleteMarks') is not null
drop proc dbo.DeleteMarks
go
create proc dbo.DeleteMarks 
@ClassId int
as
begin
	 

	update uo
		set uo.ocena = -1, uo.ocenaOpis = ''
	from UcenikOcena uo
	join Ucenik u on u.Id = uo.ucenikId
	where 
		u.razredId = @ClassId
	 
	Select 0 as ErrCode, 'OK' as ErrMessage

end
go

if object_id('SinhronizeStudentsSubjects') is not null
drop proc dbo.SinhronizeStudentsSubjects
go
create proc dbo.SinhronizeStudentsSubjects
	@ClassId int
as
begin
	
	select ucenikId,smerGodinaPredmetId,ocena,ocenaOpis into #UcenikOcena 
	from UcenikOcena 
	where 0>0

	insert into #UcenikOcena
	select u.Id, sgp.Id,-1 as ocena, '' as ocenaOpis
	from Ucenik u
	join Razred r on u.razredId = r.Id
	join SmerGodina sg on sg.Id = r.smerGodinaId
	join SmerGodinaPredmet sgp on sgp.smerGodinaId = sg.Id 
	where (@ClassId = 0 or r.Id = @ClassId)

	delete tuo
	from #UcenikOcena tuo 
	join UcenikOcena uo on uo.ucenikId = tuo.ucenikId and uo.smerGodinaPredmetId = tuo.smerGodinaPredmetId

	insert into UcenikOcena
		(
			ucenikId,
			smerGodinaPredmetId,
			ocena,
			ocenaOpis
		)
	select 
		ucenikId,
		smerGodinaPredmetId,
		ocena,
		ocenaOpis 
	from #UcenikOcena

	Select 0 as ErrCode, 'OK' as ErrMessage
end
go

if object_id('SinhronizeStudentsGroups') is not null
drop proc dbo.SinhronizeStudentsGroups
go
create proc dbo.SinhronizeStudentsGroups
	@ClassId int
as
begin
	
	delete uo 
	from UcenikOcena uo
	join Ucenik u on u.Id = uo.ucenikId
	join SmerGodinaPredmet sgp on sgp.Id = uo.smerGodinaPredmetId
	left join UcenikGrupa ug on ug.ucenikId = uo.ucenikId and ug.grupaId = sgp.grupaId 
	where u.razredId = @ClassId 
	  and  ug.Id  is null
	
	Select 0 as ErrCode, 'OK' as ErrMessage
end
go

--exec dbo.RetrieveSvedocanstvoData 1,1, 169
if object_id('RetrieveSvedocanstvoData') is not null
drop proc dbo.RetrieveSvedocanstvoData
go
create proc dbo.RetrieveSvedocanstvoData
	@DocumentTypeId int, 
	@ClassId int, 
	@StudentId int
as
begin
	create table #result (BookmarkName varchar(1000), BookmarkValue varchar(1000) collate database_default)

	declare @value varchar(1000)
	--select * from Config 

--	<root>
--  <_nazivskole>nazivskole</_nazivskole>
	select @value = param_value 
	from Config 
	where param_name = 'NAZIV_SKOLE'

	insert into #result 
	select '_nazivskole', @value
--  <_sediste>sediste</_sediste>
	select @value = param_value 
	from Config 
	where param_name = 'SEDISTE'

	insert into #result 
	select '_sediste', @value
--  <_resenje>resenje</_resenje>
--  <_datum>datum</_datum>
	insert into #result 
	select '_datum', convert(varchar,getdate(),104)
--  <_delovodnibroj>delovodnibroj</_delovodnibroj>
	declare 
		@imeprezime varchar(500),
		@imeroditelja varchar(500),
		@datrodj varchar(500),
		@mestorodj varchar(500),
		@opstina varchar(500),
		@drzava varchar(500),
		@razred varchar(500),
		@gimnazijesmer varchar(500),
		@zaobrazovaniprifil varchar(500),
		@zakojiobrazovanjetraje varchar(500),
		@uspeh varchar(500),
		@konuspeh varchar(500),
		@slrazred varchar(500)

	select 
		@imeprezime = u.prezime + ' ' +u.ime,
		@imeroditelja = u.imeRoditelja,
		@datrodj = convert(varchar(10),u.datum_rodjenja,104),
		@mestorodj = u.mestoRodjenja,
		@opstina = u.opstina,
		@drzava = u.drzava,
		@razred = cast(sg.godina as varchar),
		@gimnazijesmer = s.naziv,
		@zaobrazovaniprifil = s.stecenoZvanje,
		@zakojiobrazovanjetraje = (select max(godina) from SmerGodina where Id = sg.Id)
		
	from Ucenik u 
	join Razred r on r.Id = u.razredId
	join SmerGodina sg on sg.Id = r.smerGodinaId
	join Smer s on s.Id = sg.Id
	where u.Id = @StudentId
--  <_imeprezime>imeprezime</_imeprezime>
	insert into #result 
	select '_imeprezime', @imeprezime
--  <_imeroditelja>imeroditelja</_imeroditelja>
	insert into #result 
	select '_imeroditelja', @imeroditelja
--  <_datrodj>datrodj</_datrodj>
	insert into #result 
	select '_datrodj', @datrodj
--  <_mestorodj>mestorodj</_mestorodj>
	insert into #result 
	select '_mestorodj', @mestorodj
--  <_opstina>opstina</_opstina>
	insert into #result 
	select '_opstina', @opstina
--  <_drzava>drzava</_drzava>
	insert into #result 
	select '_drzava', @drzava
--  <_ut>ut</_ut>
--  <_razred>razred</_razred>
	insert into #result 
	select '_razred', @razred
--  <_gimnazijesmer>gimnazijesmer</_gimnazijesmer>
	insert into #result 
	select '_gimnazijesmer', @gimnazijesmer
--  <_zaobrazovaniprifil>zaobrazovaniprifil</_zaobrazovaniprifil>
	insert into #result 
	select '_zaobrazovaniprifil', @zaobrazovaniprifil
--  <_zakojiobrazovanjetraje>zakojiobrazovanjetraje</_zakojiobrazovanjetraje>
	insert into #result 
	select '_zakojiobrazovanjetraje', @zakojiobrazovanjetraje
--  <_uspeh>uspeh</_uspeh>
	insert into #result 
	select '_uspeh', @uspeh
	
	insert into #result
	select '_predmet'+cast(sgp.redniBroj as varchar), p.naziv
	from UcenikOcena uo
	join SmerGodinaPredmet sgp on sgp.Id = uo.smerGodinaPredmetId
	join Predmet p on p.Id = sgp.predmetId
	where uo.ucenikId = @StudentId

	insert into #result
	select '_ocena'+cast(sgp.redniBroj as varchar), cast(uo.ocena as varchar)
	from UcenikOcena uo
	join SmerGodinaPredmet sgp on sgp.Id = uo.smerGodinaPredmetId
	join Predmet p on p.Id = sgp.predmetId
	where uo.ucenikId = @StudentId

	insert into #result
	select '_slovima'+cast(sgp.redniBroj as varchar),  isnull(nullif(uo.ocenaOpis,''), cast(' ' as char(1)))
	from UcenikOcena uo
	join SmerGodinaPredmet sgp on sgp.Id = uo.smerGodinaPredmetId
	join Predmet p on p.Id = sgp.predmetId
	where uo.ucenikId = @StudentId

	declare @maxrb int
	select @maxrb = max(sgp.redniBroj) 
	from UcenikOcena uo
	join SmerGodinaPredmet sgp on sgp.Id = uo.smerGodinaPredmetId
	join Predmet p on p.Id = sgp.predmetId
	where uo.ucenikId = @StudentId and sgp.redniBroj < 20
	
	set @maxrb = @maxrb + 1
	while (@maxrb <= 19)
	begin
		insert into #result
		select '_predmet'+cast(@maxrb as varchar), cast(' ' as char(1))

		insert into #result
		select '_ocena'+cast(@maxrb as varchar), cast(' ' as char(1))

		insert into #result
		select '_slovima'+cast(@maxrb as varchar), cast(' ' as char(1))

		set @maxrb = @maxrb + 1
	end
	
	select BookmarkName, BookmarkValue from #result
end
go
