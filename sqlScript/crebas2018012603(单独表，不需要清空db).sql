if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Products') and o.name = 'FK_PRODUCTS_RELATIONS_PROCESSO')
alter table Products
   drop constraint FK_PRODUCTS_RELATIONS_PROCESSO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Products') and o.name = 'FK_PRODUCTS_RELATIONS_PROCESSF')
alter table Products
   drop constraint FK_PRODUCTS_RELATIONS_PROCESSF
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Products')
            and   name  = 'Relationship_67_FK'
            and   indid > 0
            and   indid < 255)
   drop index Products.Relationship_67_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Products')
            and   name  = 'Relationship_66_FK'
            and   indid > 0
            and   indid < 255)
   drop index Products.Relationship_66_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Products')
            and   type = 'U')
   drop table Products
go

/*==============================================================*/
/* Table: Products                                              */
/*==============================================================*/
create table Products (
   Id                   char(32)             not null,
   PROCESSFLOWCHART_Id  char(32)             null,
   PROCESSORDER_Id      char(32)             null,
   ProductionNumber     int                  null,
   ImplementDate        datetime             null,
   HangingPieceSiteNo   varchar(100)         null,
   ProcessOrderNo       varchar(200)         null,
   Status               tinyint              null,
   CustomerPurchaseOrderId char(32)             null,
   OrderNo              varchar(200)         null,
   StyleNo              varchar(200)         null,
   PColor               varchar(200)         null,
   PO                   varchar(200)         null,
   PSize                varchar(200)         null,
   LineName             varchar(200)         null,
   FlowSection          varchar(200)         null,
   Unit                 varchar(200)         null,
   TaskNum              int                  null,
   OnlineNum            int                  null,
   TodayHangingPieceSiteNum int                  null,
   TodayProdOutNum      int                  null,
   TodayBindCard        int                  null,
   TodayRework          int                  null,
   TotalHangingPieceSiteNum int                  null,
   TotalRework          int                  null,
   TotalBindNum         int                  null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PRODUCTS primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Products') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Products' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '��Ʒ', 
   'user', @CurrentUser, 'table', 'Products'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'Products', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductionNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'ProductionNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ų���',
   'user', @CurrentUser, 'table', 'Products', 'column', 'ProductionNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ImplementDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'ImplementDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'Products', 'column', 'ImplementDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangingPieceSiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'HangingPieceSiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��Ƭվ��',
   'user', @CurrentUser, 'table', 'Products', 'column', 'HangingPieceSiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ƶ���',
   'user', @CurrentUser, 'table', 'Products', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Status')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'Status'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '״̬:0:δ����;1:�ѷ���;3.����;4.�����',
   'user', @CurrentUser, 'table', 'Products', 'column', 'Status'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustomerPurchaseOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'CustomerPurchaseOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ͻ���ó��Id',
   'user', @CurrentUser, 'table', 'Products', 'column', 'CustomerPurchaseOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'OrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', @CurrentUser, 'table', 'Products', 'column', 'OrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StyleNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'StyleNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���',
   'user', @CurrentUser, 'table', 'Products', 'column', 'StyleNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ɫ',
   'user', @CurrentUser, 'table', 'Products', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PO')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'PO'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'PO��',
   'user', @CurrentUser, 'table', 'Products', 'column', 'PO'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'Products', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'Products', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowSection')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'FlowSection'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'Products', 'column', 'FlowSection'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Unit')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'Unit'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��λ',
   'user', @CurrentUser, 'table', 'Products', 'column', 'Unit'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TaskNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'TaskNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'Products', 'column', 'TaskNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OnlineNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'OnlineNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', @CurrentUser, 'table', 'Products', 'column', 'OnlineNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodayHangingPieceSiteNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'TodayHangingPieceSiteNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���չ�Ƭ',
   'user', @CurrentUser, 'table', 'Products', 'column', 'TodayHangingPieceSiteNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodayProdOutNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'TodayProdOutNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ղ���',
   'user', @CurrentUser, 'table', 'Products', 'column', 'TodayProdOutNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodayBindCard')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'TodayBindCard'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���հ�',
   'user', @CurrentUser, 'table', 'Products', 'column', 'TodayBindCard'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodayRework')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'TodayRework'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���շ���',
   'user', @CurrentUser, 'table', 'Products', 'column', 'TodayRework'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalHangingPieceSiteNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'TotalHangingPieceSiteNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ۼƹ�Ƭ',
   'user', @CurrentUser, 'table', 'Products', 'column', 'TotalHangingPieceSiteNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalRework')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'TotalRework'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ۼƷ���',
   'user', @CurrentUser, 'table', 'Products', 'column', 'TotalRework'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalBindNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'TotalBindNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ۼư�',
   'user', @CurrentUser, 'table', 'Products', 'column', 'TotalBindNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'Products', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'Products', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����û�',
   'user', @CurrentUser, 'table', 'Products', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�����û�',
   'user', @CurrentUser, 'table', 'Products', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ɾ����ʶ',
   'user', @CurrentUser, 'table', 'Products', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Products')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Products', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��˾Id',
   'user', @CurrentUser, 'table', 'Products', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_66_FK                                    */
/*==============================================================*/
create index Relationship_66_FK on Products (
PROCESSORDER_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_67_FK                                    */
/*==============================================================*/
create index Relationship_67_FK on Products (
PROCESSFLOWCHART_Id ASC
)
go

alter table Products
   add constraint FK_PRODUCTS_RELATIONS_PROCESSO foreign key (PROCESSORDER_Id)
      references ProcessOrder (Id)
go

alter table Products
   add constraint FK_PRODUCTS_RELATIONS_PROCESSF foreign key (PROCESSFLOWCHART_Id)
      references ProcessFlowChart (Id)
go
