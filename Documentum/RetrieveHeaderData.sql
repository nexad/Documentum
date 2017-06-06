if object_id ('ReadConfigString') is not null
drop function dbo.ReadConfigString
go

create function dbo.ReadConfigString(@param_name varchar(100))
returns varchar(100)
as
begin
	declare @res varchar(100)

	select @res = param_value 
	from Config 
	where param_name = @param_name


	return @res
end
go


USE [documentum]
GO
if object_id('RetrieveHeaderData') is not null
drop proc [dbo].RetrieveHeaderData
go
CREATE proc [dbo].RetrieveHeaderData
	@DocumentTypeId int = 0, 
	@ClassId int = 0, 
	@StudentId int = 0
as
begin
	create table #result (BookmarkName varchar(1000), BookmarkValue varchar(1000) collate database_default)

	declare @value varchar(1000)
	--select * from Config 

--	<root>
--  <_nazivskole>nazivskole</_nazivskole>

	insert into #result 
	select '_nazivskole', dbo.ReadConfigString('NAZIV_SKOLE')
--  <_sediste>sediste</_sediste>

	insert into #result 
	select '_sediste', dbo.ReadConfigString('SEDISTE')
--  <_resenje>resenje</_resenje>

	insert into #result 
	select '_resenje', dbo.ReadConfigString('RESENJE')

--  <_datum>datum</_datum>

	insert into #result 
	select '_datum', dbo.ReadConfigString('DATUM')
--  <_delovodnibroj>delovodnibroj</_delovodnibroj>

	insert into #result 
	select '_delovodnibroj', dbo.ReadConfigString('DELOVODNIBROJ')

	select * from #result 

end

go