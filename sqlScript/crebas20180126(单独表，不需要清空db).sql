if exists (select 1
            from  sysobjects
           where  id = object_id('HangerProductItem')
            and   type = 'U')
   drop table HangerProductItem
go

/*==============================================================*/
/* Table: HangerProductItem                                     */
/*==============================================================*/
create table HangerProductItem (
   Id                   char(32)             not null,
   HangerNo             varchar(200)         null,
   ProcessOrderId       char(32)             null,
   ProcessOrderNo       varchar(200)         null,
   PColor               varchar(200)         null,
   PSize                varchar(200)         null,
   FlowChartd           char(32)             null,
   LineName             varchar(200)         null,
   SizeNum              int                  null,
   ProcessFlowId        char(32)             null,
   ProcessFlowCode      varchar(200)         null,
   ProcessFlowName      varchar(200)         null,
   FlowIndex            smallint             null,
   SiteId               char(32)             null,
   SiteNo               varchar(50)          null,
   IsFlowChatChange     tinyint              null,
   IsIncomeSite         tinyint              null,
   Memo                 varchar(100)         null,
   ClientMachineId      char(32)             null,
   SusLineId            char(32)             null,
   IncomeSiteDate       datetime             null,
   CompareDate          datetime             null,
   OutSiteDate          datetime             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_HANGERPRODUCTITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('HangerProductItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'HangerProductItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '衣架生产明细', 
   'user', @CurrentUser, 'table', 'HangerProductItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单Id',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowChartd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'FlowChartd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺路线图Id',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'FlowChartd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'SizeNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'SizeNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序Id',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ProcessFlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序索引',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'SiteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站Id',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'SiteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'SiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站号',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'SiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowChatChange')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IsFlowChatChange'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线图是否改变',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IsFlowChatChange'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsIncomeSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IsIncomeSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否进站',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IsIncomeSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientMachineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ClientMachineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户机Id',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'ClientMachineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SusLineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'SusLineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '吊挂线Id',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'SusLineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IncomeSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IncomeSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '进站时间',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IncomeSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompareDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'CompareDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '比较时间',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'CompareDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OutSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'OutSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出战时间',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'OutSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'CompanyId'
go
