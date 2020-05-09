if exists (select 1
            from  sysobjects
           where  id = object_id('StatingHangerProductItem')
            and   type = 'U')
   drop table StatingHangerProductItem
go

/*==============================================================*/
/* Table: StatingHangerProductItem                              */
/*==============================================================*/
create table StatingHangerProductItem (
   Id                   char(32)             not null,
   SiteNo               varchar(50)          null,
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
   IsFlowChatChange     tinyint              null,
   IsIncomeSite         tinyint              null,
   Memo                 varchar(100)         null,
   ClientMachineId      char(32)             null,
   SusLineId            char(32)             null,
   IncomeSiteDate       datetime             null,
   OutSiteDate          datetime             null,
   CompareDate          datetime             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_STATINGHANGERPRODUCTITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StatingHangerProductItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StatingHangerProductItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '站点衣架生产明细', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'SiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站号',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'SiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单Id',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowChartd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'FlowChartd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺路线图Id',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'FlowChartd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'SizeNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'SizeNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序Id',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ProcessFlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序索引',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'SiteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站Id',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'SiteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowChatChange')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'IsFlowChatChange'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线图是否改变',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'IsFlowChatChange'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsIncomeSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'IsIncomeSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否进站',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'IsIncomeSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientMachineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ClientMachineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户机Id',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'ClientMachineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SusLineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'SusLineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '吊挂线Id',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'SusLineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IncomeSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'IncomeSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '进站时间',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'IncomeSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OutSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'OutSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出战时间',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'OutSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompareDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'CompareDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '比较时间',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'CompareDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'CompanyId'
go
