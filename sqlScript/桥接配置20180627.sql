if exists (select 1
            from  sysobjects
           where  id = object_id('BridgeSet')
            and   type = 'U')
   drop table BridgeSet
go

/*==============================================================*/
/* Table: BridgeSet                                             */
/*==============================================================*/
create table BridgeSet (
   Id                   char(32)             not null,
   BIndex               smallint             null,
   AMainTrackNumber     smallint             null,
   ASiteNo              smallint             null,
   Direction            smallint             null,
   DirectionTxt         varchar(20)          null,
   BMainTrackNumber     smallint             null,
   BSiteNo              smallint             null,
   Enabled              bit                  null,
   constraint PK_BRIDGESET primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('BridgeSet') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'BridgeSet' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '桥接配置', 
   'user', @CurrentUser, 'table', 'BridgeSet'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BridgeSet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'BIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '顺序编号',
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'BIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BridgeSet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AMainTrackNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'AMainTrackNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'A线主轨',
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'AMainTrackNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BridgeSet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ASiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'ASiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'ASiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BridgeSet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Direction')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'Direction'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '方向(0:双向;1:从右到左;2:从左到右)',
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'Direction'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BridgeSet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DirectionTxt')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'DirectionTxt'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '方向描述',
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'DirectionTxt'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BridgeSet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BMainTrackNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'BMainTrackNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B线主轨',
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'BMainTrackNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BridgeSet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BSiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'BSiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B线主轨站号',
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'BSiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BridgeSet')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Enabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'Enabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否启用',
   'user', @CurrentUser, 'table', 'BridgeSet', 'column', 'Enabled'
go
