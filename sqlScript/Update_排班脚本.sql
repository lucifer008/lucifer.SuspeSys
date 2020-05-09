if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ClassesEmployee') and o.name = 'FK_CLASSESEMPLOYEE_RELATIONS_CLASSESINFO')
alter table ClassesEmployee
   drop constraint FK_CLASSESEMPLOYEE_RELATIONS_CLASSESINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ClassesEmployee') and o.name = 'FK_CLASSESEMPLOYEE_RELATIONS_EMPLOYEE')
alter table ClassesEmployee
   drop constraint FK_CLASSESEMPLOYEE_RELATIONS_EMPLOYEE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ClassesEmployee')
            and   name  = 'Relationship_84_FK'
            and   indid > 0
            and   indid < 255)
   drop index ClassesEmployee.Relationship_84_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ClassesEmployee')
            and   name  = 'Relationship_85_FK'
            and   indid > 0
            and   indid < 255)
   drop index ClassesEmployee.Relationship_85_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ClassesEmployee')
            and   type = 'U')
   drop table ClassesEmployee
go


if exists (select 1
            from  sysobjects
           where  id = object_id('ClassesInfo')
            and   type = 'U')
   drop table ClassesInfo
go

/*==============================================================*/
/* Table: ClassesInfo                                           */
/*==============================================================*/
create table ClassesInfo (
   Id                   char(32)             not null,
   Num                  varchar(50)          null,
   CType                varchar(200)         null,
   Time1GoToWorkDate    datetime             null,
   Time1GoOffWorkDate   datetime             null,
   Time2GoToWorkDate    datetime             null,
   Time2GoOffWorkDate   datetime             null,
   Time3GoToWorkDate    datetime             null,
   Time3GoOffWorkDate   datetime             null,
   Time3IsOverTime      bit                  null,
   OverTimeIn           datetime             null,
   OverTimeOut          datetime             null,
   IsEnabled            bit                  null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_CLASSESINFO primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ClassesInfo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ClassesInfo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '班次信息', 
   'user', @CurrentUser, 'table', 'ClassesInfo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Num')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Num'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '班次',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Num'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'CType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '班次类型(0:正常班次;1.加班班次;2.假日班次)',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'CType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Time1GoToWorkDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time1GoToWorkDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时段1上班时间',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time1GoToWorkDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Time1GoOffWorkDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time1GoOffWorkDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时段1下班时间',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time1GoOffWorkDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Time2GoToWorkDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time2GoToWorkDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时段2上班时间',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time2GoToWorkDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Time2GoOffWorkDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time2GoOffWorkDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时段2下班时间',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time2GoOffWorkDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Time3GoToWorkDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time3GoToWorkDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时段3上班时间',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time3GoToWorkDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Time3GoOffWorkDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time3GoOffWorkDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时段3下班时间',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Time3GoOffWorkDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsEnabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'IsEnabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否启用(0:启用;1:禁用)',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'IsEnabled'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ClassesInfo', 'column', 'CompanyId'
go


/*==============================================================*/
/* Table: ClassesEmployee                                       */
/*==============================================================*/
create table ClassesEmployee (
   Id                   char(32)             not null,
   EMPLOYEE_Id          char(32)             null,
   CLASSESINFO_Id       char(32)             null,
   AttendanceDate       datetime             null,
   Week                 smallint             null default 0,
   EffectDate           datetime             null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_CLASSESEMPLOYEE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ClassesEmployee') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ClassesEmployee' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '班次员工', 
   'user', @CurrentUser, 'table', 'ClassesEmployee'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CLASSESINFO_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'CLASSESINFO_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'CLASSESINFO_Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AttendanceDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'AttendanceDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出勤日期',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'AttendanceDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EffectDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'EffectDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生效日期',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'EffectDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClassesEmployee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ClassesEmployee', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_85_FK                                    */
/*==============================================================*/
create index Relationship_85_FK on ClassesEmployee (
EMPLOYEE_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_84_FK                                    */
/*==============================================================*/
create index Relationship_84_FK on ClassesEmployee (
CLASSESINFO_Id ASC
)
go

alter table ClassesEmployee
   add constraint FK_CLASSESEMPLOYEE_RELATIONS_CLASSESINFO foreign key (CLASSESINFO_Id)
      references ClassesInfo (Id)
go

alter table ClassesEmployee
   add constraint FK_CLASSESEMPLOYEE_RELATIONS_EMPLOYEE foreign key (EMPLOYEE_Id)
      references Employee (Id)
go
