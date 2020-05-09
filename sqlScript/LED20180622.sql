if exists (select 1
            from  sysobjects
           where  id = object_id('LEDScreenConfig')
            and   type = 'U')
   drop table LEDScreenConfig
go

/*==============================================================*/
/* Table: LEDScreenConfig                                       */
/*==============================================================*/
create table LEDScreenConfig (
   Id                   char(32)             not null,
   ScreenNo             varchar(20)          null,
   ControllerTypeTxt    varchar(20)          null,
   ControllerKey        varchar(20)          null,
   CommunicationWay     int                  null,
   CommunicationWayTxt  varchar(20)          null,
   SWidth               int                  null,
   SHeight              int                  null,
   ColorType            int                  null,
   ColorTypeTxt         varchar(20)          null,
   IPAddress            varchar(20)          null,
   Port                 int                  null,
   GroupNo              varchar(20)          null,
   Enable               bit                  null,
   constraint PK_LEDSCREENCONFIG primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LEDScreenConfig') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LEDScreenConfig' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'LED屏配置', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ScreenNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ScreenNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '屏号',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ScreenNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ControllerTypeTxt')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ControllerTypeTxt'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '控制器类型(EQ2011)',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ControllerTypeTxt'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ControllerKey')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ControllerKey'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '控制器类型Key',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ControllerKey'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CommunicationWay')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'CommunicationWay'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通信方式(0:串口;1:网络)',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'CommunicationWay'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CommunicationWayTxt')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'CommunicationWayTxt'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通信方式描述',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'CommunicationWayTxt'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SWidth')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'SWidth'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '显示屏宽度',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'SWidth'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SHeight')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'SHeight'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '显示屏高度',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'SHeight'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ColorType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色类型',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ColorType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorTypeTxt')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ColorTypeTxt'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色类型描述',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'ColorTypeTxt'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IPAddress')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'IPAddress'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'IP地址',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'IPAddress'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Port')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'Port'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '端口号',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'Port'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GroupNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'GroupNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生产组',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'GroupNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Enable')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'Enable'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '启用(0：否:1:是)',
   'user', @CurrentUser, 'table', 'LEDScreenConfig', 'column', 'Enable'
go
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LEDScreenPage') and o.name = 'FK_LEDScreenPage_RELATIONS_LEDScreenConfig')
alter table LEDScreenPage
   drop constraint FK_LEDScreenPage_RELATIONS_LEDScreenConfig
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LEDScreenPage')
            and   name  = 'Relationship_91_FK'
            and   indid > 0
            and   indid < 255)
   drop index LEDScreenPage.Relationship_91_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LEDScreenPage')
            and   type = 'U')
   drop table LEDScreenPage
go

/*==============================================================*/
/* Table: LEDScreenPage                                         */
/*==============================================================*/
create table LEDScreenPage (
   ID                   char(32)             not null,
   LEDSCREENCONFIG_Id   char(32)             null,
   PageNo               int                  null,
   InfoType             int                  null,
   InfoTypeTxt          varchar(20)          null,
   CusContent           varchar(200)         null,
   Times                int                  null,
   RefreshCycle         int                  null,
   Enabled              bit                  null,
   InsertTime           datetime             null,
   constraint PK_LEDSCREENPAGE primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LEDScreenPage') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LEDScreenPage' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'LED显示屏页面', 
   'user', @CurrentUser, 'table', 'LEDScreenPage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LEDSCREENCONFIG_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'LEDSCREENCONFIG_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'LEDSCREENCONFIG_Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PageNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'PageNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '页面序号',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'PageNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InfoType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'InfoType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '信息类别',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'InfoType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InfoTypeTxt')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'InfoTypeTxt'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '信息类别描述',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'InfoTypeTxt'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CusContent')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'CusContent'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '自定义信息内容',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'CusContent'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Times')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'Times'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时长(秒)',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'Times'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RefreshCycle')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'RefreshCycle'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '刷新周期(秒)',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'RefreshCycle'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Enabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'Enabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生效',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'Enabled'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDScreenPage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'InsertTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '录入时间',
   'user', @CurrentUser, 'table', 'LEDScreenPage', 'column', 'InsertTime'
go

/*==============================================================*/
/* Index: Relationship_91_FK                                    */
/*==============================================================*/
create index Relationship_91_FK on LEDScreenPage (
LEDSCREENCONFIG_Id ASC
)
go

alter table LEDScreenPage
   add constraint FK_LEDScreenPage_RELATIONS_LEDScreenConfig foreign key (LEDSCREENCONFIG_Id)
      references LEDScreenConfig (Id)
go
if exists (select 1
            from  sysobjects
           where  id = object_id('LEDHoursPlanTableItem')
            and   type = 'U')
   drop table LEDHoursPlanTableItem
go

/*==============================================================*/
/* Table: LEDHoursPlanTableItem                                 */
/*==============================================================*/
create table LEDHoursPlanTableItem (
   Id                   char(32)             not null,
   GroupNo              varchar(20)          null,
   BeginDate            datetime             null,
   EndDate              datetime             null,
   PlanNum              int                  null,
   InsertDate           datetime             null,
   Enabled              bit                  null,
   constraint PK_LEDHOURSPLANTABLEITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LEDHoursPlanTableItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'LED小时计划表明细', 
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDHoursPlanTableItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GroupNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'GroupNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组号',
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'GroupNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDHoursPlanTableItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BeginDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'BeginDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开始时间',
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'BeginDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDHoursPlanTableItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EndDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'EndDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结束时间',
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'EndDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDHoursPlanTableItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PlanNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'PlanNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计划数量',
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'PlanNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDHoursPlanTableItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'InsertDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '录入时间',
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'InsertDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LEDHoursPlanTableItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Enabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'Enabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否启用',
   'user', @CurrentUser, 'table', 'LEDHoursPlanTableItem', 'column', 'Enabled'
go
