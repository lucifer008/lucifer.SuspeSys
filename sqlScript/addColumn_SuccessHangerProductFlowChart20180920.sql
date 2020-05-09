if exists (select 1
            from  sysobjects
           where  id = object_id('SuccessHangerProductFlowChart')
            and   type = 'U')
   drop table SuccessHangerProductFlowChart
go

/*==============================================================*/
/* Table: SuccessHangerProductFlowChart                         */
/*==============================================================*/
create table SuccessHangerProductFlowChart (
   Id                   char(32)             not null,
   MainTrackNumber      smallint             null,
   ProductsId           char(32)             null,
   BatchNo              bigint               null,
   HangerNo             varchar(200)         null,
   IsHangerSucess       bit                  null,
   PO                   varchar(50)          null,
   ProcessOrderNo       varchar(50)          null,
   ProcessChartId       char(32)             null,
   FlowIndex            int                  null,
   FlowId               char(32)             null,
   FlowNo               varchar(20)          null,
   FlowCode             varchar(200)         null,
   FlowName             varchar(200)         null,
   StatingId            char(32)             null,
   StatingNo            smallint             null,
   StatingCapacity      bigint               null,
   NextStatingNo        smallint             null,
   FlowRealyProductStatingNo smallint             null,
   Status               smallint             null,
   FlowType             tinyint              null,
   IsFlowSucess         bit                  null,
   IsReworkSourceStating bit                  null default 0,
   DefectCode           varchar(200)         null,
   DefcectName          varchar(500)         null,
   PColor               varchar(200)         null,
   PSize                varchar(200)         null,
   EmployeeName         varchar(20)          null,
   CardNo               varchar(50)          null,
   IncomeSiteDate       datetime             null,
   CompareDate          datetime             null,
   OutSiteDate          datetime             null,
   ReworkEmployeeNo     varchar(20)          null,
   ReworkEmployeeName   varchar(50)          null,
   ReworkDate           datetime             null,
   ReworkMaintrackNumber smallint             null,
   ReworkStatingNo      smallint             null,
   Memo2                varchar(500)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_SUCCESSHANGERPRODUCTFLOWCHA primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('SuccessHangerProductFlowChart') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '��ɵ��¼���������ͼ', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MainTrackNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'MainTrackNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����(0-255)',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'MainTrackNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductsId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ProductsId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ƷId',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ProductsId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BatchNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'BatchNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'BatchNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�¼ܺ�',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsHangerSucess')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'IsHangerSucess'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�¼��Ƿ��������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'IsHangerSucess'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PO')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'PO'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'PO��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'PO'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƶ���',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessChartId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ProcessChartId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ͼId',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ProcessChartId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����Id',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'StatingId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������վ��Id',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'StatingId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'StatingNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������վ��(���Թ���·��ͼ�е�վ��)',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'StatingNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingCapacity')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'StatingCapacity'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������վ������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'StatingCapacity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NextStatingNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'NextStatingNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������һվ��(���������һ����������վ��)',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'NextStatingNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowRealyProductStatingNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowRealyProductStatingNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʵ������վ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowRealyProductStatingNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Status')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'Status'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����״̬(0:������:1:������:2:�������)',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'Status'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '0:��������;1:��������:2:����',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'FlowType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowSucess')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'IsFlowSucess'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����Ƿ��������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'IsFlowSucess'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsReworkSourceStating')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'IsReworkSourceStating'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ��Ƿ�������վ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'IsReworkSourceStating'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefectCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'DefectCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�õ����',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'DefectCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefcectName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'DefcectName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�õ�����',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'DefcectName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ɫ',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EmployeeName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'EmployeeName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����������Ա������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'EmployeeName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CardNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'CardNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Ա������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'CardNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IncomeSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'IncomeSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��վʱ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'IncomeSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompareDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'CompareDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƚ�ʱ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'CompareDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OutSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'OutSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��սʱ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'OutSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ReworkEmployeeNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkEmployeeNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����Ա������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkEmployeeNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ReworkEmployeeName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkEmployeeName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����Ա������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkEmployeeName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ReworkDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������ʱ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ReworkMaintrackNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkMaintrackNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������������',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkMaintrackNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ReworkStatingNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkStatingNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������վ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'ReworkStatingNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo2')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'Memo2'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע2',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'Memo2'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����û�',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����û�',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ɾ����ʶ',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessHangerProductFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��˾Id',
   'user', @CurrentUser, 'table', 'SuccessHangerProductFlowChart', 'column', 'CompanyId'
go

