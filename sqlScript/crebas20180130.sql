if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Area') and o.name = 'FK_AREA_RELATIONS_CITY')
alter table Area
   drop constraint FK_AREA_RELATIONS_CITY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('City') and o.name = 'FK_CITY_RELATIONS_PROVINCE')
alter table City
   drop constraint FK_CITY_RELATIONS_PROVINCE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CustomerPurchaseOrder') and o.name = 'FK_CUSTOMER_RELATIONS_CUSTOMER')
alter table CustomerPurchaseOrder
   drop constraint FK_CUSTOMER_RELATIONS_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CustomerPurchaseOrder') and o.name = 'FK_CUSTOMER_RELATIONS_STYLE')
alter table CustomerPurchaseOrder
   drop constraint FK_CUSTOMER_RELATIONS_STYLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CustomerPurchaseOrderColorItem') and o.name = 'FK_CUSTOMER_RELATIONS_CUSTOMER')
alter table CustomerPurchaseOrderColorItem
   drop constraint FK_CUSTOMER_RELATIONS_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CustomerPurchaseOrderColorItem') and o.name = 'FK_CUSTOMER_RELATIONS_POCOLOR')
alter table CustomerPurchaseOrderColorItem
   drop constraint FK_CUSTOMER_RELATIONS_POCOLOR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CustomerPurchaseOrderColorSizeItem') and o.name = 'FK_CUSTOMER_RELATIONS_CUSTOMER')
alter table CustomerPurchaseOrderColorSizeItem
   drop constraint FK_CUSTOMER_RELATIONS_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CustomerPurchaseOrderColorSizeItem') and o.name = 'FK_CUSTOMER_RELATIONS_PSIZE')
alter table CustomerPurchaseOrderColorSizeItem
   drop constraint FK_CUSTOMER_RELATIONS_PSIZE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_RELATIONS_ORGANIZA')
alter table Employee
   drop constraint FK_EMPLOYEE_RELATIONS_ORGANIZA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_RELATIONS_DEPARTME')
alter table Employee
   drop constraint FK_EMPLOYEE_RELATIONS_DEPARTME
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_RELATIONS_WORKTYPE')
alter table Employee
   drop constraint FK_EMPLOYEE_RELATIONS_WORKTYPE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_RELATIONS_AREA')
alter table Employee
   drop constraint FK_EMPLOYEE_RELATIONS_AREA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Employee') and o.name = 'FK_EMPLOYEE_RELATIONS_SITEGROU')
alter table Employee
   drop constraint FK_EMPLOYEE_RELATIONS_SITEGROU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EmployeePositions') and o.name = 'FK_EMPLOYEE_RELATIONS_EMPLOYEE')
alter table EmployeePositions
   drop constraint FK_EMPLOYEE_RELATIONS_EMPLOYEE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EmployeePositions') and o.name = 'FK_EMPLOYEE_RELATIONS_POSITION')
alter table EmployeePositions
   drop constraint FK_EMPLOYEE_RELATIONS_POSITION
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowStatingColor') and o.name = 'FK_FLOWSTAT_RELATIONS_PROCESSF')
alter table FlowStatingColor
   drop constraint FK_FLOWSTAT_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowStatingResume') and o.name = 'FK_FLOWSTAT_RELATIONS_STATING')
alter table FlowStatingResume
   drop constraint FK_FLOWSTAT_RELATIONS_STATING
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowStatingResume') and o.name = 'FK_FLOWSTAT_RELATIONS_PROCESSF')
alter table FlowStatingResume
   drop constraint FK_FLOWSTAT_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FlowStatingSize') and o.name = 'FK_FLOWSTAT_RELATIONS_PROCESSF')
alter table FlowStatingSize
   drop constraint FK_FLOWSTAT_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Modules') and o.name = 'FK_MODULES_RELATIONS_MODULES')
alter table Modules
   drop constraint FK_MODULES_RELATIONS_MODULES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrderProductItem') and o.name = 'FK_ORDERPRO_RELATIONS_PRODUCTO')
alter table OrderProductItem
   drop constraint FK_ORDERPRO_RELATIONS_PRODUCTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Organizations') and o.name = 'FK_ORGANIZA_RELATIONS_ORGANIZA')
alter table Organizations
   drop constraint FK_ORGANIZA_RELATIONS_ORGANIZA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Pipelining') and o.name = 'FK_PIPELINI_RELATIONS_PRODTYPE')
alter table Pipelining
   drop constraint FK_PIPELINI_RELATIONS_PRODTYPE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Pipelining') and o.name = 'FK_PIPELINI_RELATIONS_SITEGROU')
alter table Pipelining
   drop constraint FK_PIPELINI_RELATIONS_SITEGROU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlow') and o.name = 'FK_PROCESSF_RELATIONS_PROCESSF')
alter table ProcessFlow
   drop constraint FK_PROCESSF_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlow') and o.name = 'FK_PROCESSF_RELATIONS_BASICPRO')
alter table ProcessFlow
   drop constraint FK_PROCESSF_RELATIONS_BASICPRO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlowChart') and o.name = 'FK_PROCESSF_RELATIONS_PROCESSF')
alter table ProcessFlowChart
   drop constraint FK_PROCESSF_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlowChartFlowRelation') and o.name = 'FK_PROCESSF_RELATIONS_PROCESSF')
alter table ProcessFlowChartFlowRelation
   drop constraint FK_PROCESSF_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlowChartFlowRelation') and o.name = 'FK_PROCESSF_RELATIONS_PROCESSF')
alter table ProcessFlowChartFlowRelation
   drop constraint FK_PROCESSF_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlowChartGrop') and o.name = 'FK_PROCESSF_RELATIONS_PROCESSF')
alter table ProcessFlowChartGrop
   drop constraint FK_PROCESSF_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlowChartGrop') and o.name = 'FK_PROCESSF_RELATIONS_SITEGROU')
alter table ProcessFlowChartGrop
   drop constraint FK_PROCESSF_RELATIONS_SITEGROU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlowStatingItem') and o.name = 'FK_PROCESSF_RELATIONS_PROCESSF')
alter table ProcessFlowStatingItem
   drop constraint FK_PROCESSF_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlowStatingItem') and o.name = 'FK_PROCESSF_RELATIONS_STATING')
alter table ProcessFlowStatingItem
   drop constraint FK_PROCESSF_RELATIONS_STATING
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessFlowVersion') and o.name = 'FK_PROCESSF_RELATIONS_PROCESSO')
alter table ProcessFlowVersion
   drop constraint FK_PROCESSF_RELATIONS_PROCESSO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessOrder') and o.name = 'FK_PROCESSO_RELATIONS_STYLE')
alter table ProcessOrder
   drop constraint FK_PROCESSO_RELATIONS_STYLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessOrder') and o.name = 'FK_PROCESSO_RELATIONS_CUSTOMER')
alter table ProcessOrder
   drop constraint FK_PROCESSO_RELATIONS_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessOrderColorItem') and o.name = 'FK_PROCESSO_RELATIONS_POCOLOR')
alter table ProcessOrderColorItem
   drop constraint FK_PROCESSO_RELATIONS_POCOLOR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessOrderColorItem') and o.name = 'FK_PROCESSO_RELATIONS_PROCESSO')
alter table ProcessOrderColorItem
   drop constraint FK_PROCESSO_RELATIONS_PROCESSO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessOrderColorItem') and o.name = 'FK_PROCESSO_RELATIONS_CUSTOMER')
alter table ProcessOrderColorItem
   drop constraint FK_PROCESSO_RELATIONS_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessOrderColorSizeItem') and o.name = 'FK_PROCESSO_RELATIONS_PROCESSO')
alter table ProcessOrderColorSizeItem
   drop constraint FK_PROCESSO_RELATIONS_PROCESSO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProcessOrderColorSizeItem') and o.name = 'FK_PROCESSO_RELATIONS_PSIZE')
alter table ProcessOrderColorSizeItem
   drop constraint FK_PROCESSO_RELATIONS_PSIZE
go

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
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ProductsHangPieceResume') and o.name = 'FK_PRODUCTS_RELATIONS_PRODUCTS')
alter table ProductsHangPieceResume
   drop constraint FK_PRODUCTS_RELATIONS_PRODUCTS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RolesModules') and o.name = 'FK_ROLESMOD_RELATIONS_ROLES')
alter table RolesModules
   drop constraint FK_ROLESMOD_RELATIONS_ROLES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RolesModules') and o.name = 'FK_ROLESMOD_RELATIONS_MODULES')
alter table RolesModules
   drop constraint FK_ROLESMOD_RELATIONS_MODULES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Stating') and o.name = 'FK_STATING_RELATIONS_STATINGR')
alter table Stating
   drop constraint FK_STATING_RELATIONS_STATINGR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Stating') and o.name = 'FK_STATING_RELATIONS_SUSLANGU')
alter table Stating
   drop constraint FK_STATING_RELATIONS_SUSLANGU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Stating') and o.name = 'FK_STATING_RELATIONS_STATINGD')
alter table Stating
   drop constraint FK_STATING_RELATIONS_STATINGD
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Stating') and o.name = 'FK_STATING_RELATIONS_SITEGROU')
alter table Stating
   drop constraint FK_STATING_RELATIONS_SITEGROU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('StyleProcessFlowItem') and o.name = 'FK_STYLEPRO_RELATIONS_STYLE')
alter table StyleProcessFlowItem
   drop constraint FK_STYLEPRO_RELATIONS_STYLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('StyleProcessFlowItem') and o.name = 'FK_STYLEPRO_RELATIONS_BASICPRO')
alter table StyleProcessFlowItem
   drop constraint FK_STYLEPRO_RELATIONS_BASICPRO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('StyleProcessFlowSectionItem') and o.name = 'FK_STYLEPRO_RELATIONS_BASICPRO')
alter table StyleProcessFlowSectionItem
   drop constraint FK_STYLEPRO_RELATIONS_BASICPRO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('StyleProcessFlowSectionItem') and o.name = 'FK_STYLEPRO_RELATIONS_PROCESSF')
alter table StyleProcessFlowSectionItem
   drop constraint FK_STYLEPRO_RELATIONS_PROCESSF
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('StyleProcessFlowSectionItem') and o.name = 'FK_STYLEPRO_RELATIONS_STYLE')
alter table StyleProcessFlowSectionItem
   drop constraint FK_STYLEPRO_RELATIONS_STYLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UserOperateLogDetail') and o.name = 'FK_USEROPER_RELATIONS_USEROPER')
alter table UserOperateLogDetail
   drop constraint FK_USEROPER_RELATIONS_USEROPER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UserRoles') and o.name = 'FK_USERROLE_RELATIONS_ROLES')
alter table UserRoles
   drop constraint FK_USERROLE_RELATIONS_ROLES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UserRoles') and o.name = 'FK_USERROLE_RELATIONS_USERS')
alter table UserRoles
   drop constraint FK_USERROLE_RELATIONS_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Users') and o.name = 'FK_USERS_RELATIONS_EMPLOYEE')
alter table Users
   drop constraint FK_USERS_RELATIONS_EMPLOYEE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Area')
            and   name  = 'Relationship_46_FK'
            and   indid > 0
            and   indid < 255)
   drop index Area.Relationship_46_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Area')
            and   type = 'U')
   drop table Area
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BasicProcessFlow')
            and   type = 'U')
   drop table BasicProcessFlow
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('City')
            and   name  = 'Relationship_45_FK'
            and   indid > 0
            and   indid < 255)
   drop index City.Relationship_45_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('City')
            and   type = 'U')
   drop table City
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ClassesInfo')
            and   type = 'U')
   drop table ClassesInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ClientMachines')
            and   type = 'U')
   drop table ClientMachines
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Company')
            and   type = 'U')
   drop table Company
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Customer')
            and   type = 'U')
   drop table Customer
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CustomerPurchaseOrder')
            and   name  = 'Relationship_65_FK'
            and   indid > 0
            and   indid < 255)
   drop index CustomerPurchaseOrder.Relationship_65_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CustomerPurchaseOrder')
            and   name  = 'Relationship_58_FK'
            and   indid > 0
            and   indid < 255)
   drop index CustomerPurchaseOrder.Relationship_58_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CustomerPurchaseOrder')
            and   type = 'U')
   drop table CustomerPurchaseOrder
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CustomerPurchaseOrderColorItem')
            and   name  = 'Relationship_63_FK'
            and   indid > 0
            and   indid < 255)
   drop index CustomerPurchaseOrderColorItem.Relationship_63_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CustomerPurchaseOrderColorItem')
            and   name  = 'Relationship_61_FK'
            and   indid > 0
            and   indid < 255)
   drop index CustomerPurchaseOrderColorItem.Relationship_61_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CustomerPurchaseOrderColorItem')
            and   type = 'U')
   drop table CustomerPurchaseOrderColorItem
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CustomerPurchaseOrderColorSizeItem')
            and   name  = 'Relationship_64_FK'
            and   indid > 0
            and   indid < 255)
   drop index CustomerPurchaseOrderColorSizeItem.Relationship_64_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CustomerPurchaseOrderColorSizeItem')
            and   name  = 'Relationship_62_FK'
            and   indid > 0
            and   indid < 255)
   drop index CustomerPurchaseOrderColorSizeItem.Relationship_62_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CustomerPurchaseOrderColorSizeItem')
            and   type = 'U')
   drop table CustomerPurchaseOrderColorSizeItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DefectCodeTable')
            and   type = 'U')
   drop table DefectCodeTable
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Department')
            and   type = 'U')
   drop table Department
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Employee')
            and   name  = 'Relationship_74_FK'
            and   indid > 0
            and   indid < 255)
   drop index Employee.Relationship_74_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Employee')
            and   name  = 'Relationship_47_FK'
            and   indid > 0
            and   indid < 255)
   drop index Employee.Relationship_47_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Employee')
            and   name  = 'Relationship_29_FK'
            and   indid > 0
            and   indid < 255)
   drop index Employee.Relationship_29_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Employee')
            and   name  = 'Relationship_28_FK'
            and   indid > 0
            and   indid < 255)
   drop index Employee.Relationship_28_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Employee')
            and   name  = 'Relationship_12_FK'
            and   indid > 0
            and   indid < 255)
   drop index Employee.Relationship_12_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Employee')
            and   type = 'U')
   drop table Employee
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EmployeeGrade')
            and   type = 'U')
   drop table EmployeeGrade
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EmployeePositions')
            and   name  = 'Relationship_35_FK'
            and   indid > 0
            and   indid < 255)
   drop index EmployeePositions.Relationship_35_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EmployeePositions')
            and   name  = 'Relationship_31_FK'
            and   indid > 0
            and   indid < 255)
   drop index EmployeePositions.Relationship_31_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EmployeePositions')
            and   type = 'U')
   drop table EmployeePositions
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EmployeeScheduling')
            and   type = 'U')
   drop table EmployeeScheduling
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowAction')
            and   type = 'U')
   drop table FlowAction
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('FlowStatingColor')
            and   name  = 'Relationship_50_FK'
            and   indid > 0
            and   indid < 255)
   drop index FlowStatingColor.Relationship_50_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowStatingColor')
            and   type = 'U')
   drop table FlowStatingColor
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('FlowStatingResume')
            and   name  = 'Relationship_53_FK'
            and   indid > 0
            and   indid < 255)
   drop index FlowStatingResume.Relationship_53_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('FlowStatingResume')
            and   name  = 'Relationship_54_FK'
            and   indid > 0
            and   indid < 255)
   drop index FlowStatingResume.Relationship_54_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowStatingResume')
            and   type = 'U')
   drop table FlowStatingResume
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('FlowStatingSize')
            and   name  = 'Relationship_56_FK'
            and   indid > 0
            and   indid < 255)
   drop index FlowStatingSize.Relationship_56_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FlowStatingSize')
            and   type = 'U')
   drop table FlowStatingSize
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Hanger')
            and   type = 'U')
   drop table Hanger
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HangerProductItem')
            and   type = 'U')
   drop table HangerProductItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HangerReworkRecord')
            and   type = 'U')
   drop table HangerReworkRecord
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HolidayInfo')
            and   type = 'U')
   drop table HolidayInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ID_GENERATOR')
            and   type = 'U')
   drop table ID_GENERATOR
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LackMaterialsTable')
            and   type = 'U')
   drop table LackMaterialsTable
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Modules')
            and   name  = 'Relationship_21_FK'
            and   indid > 0
            and   indid < 255)
   drop index Modules.Relationship_21_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Modules')
            and   type = 'U')
   drop table Modules
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('OrderProductItem')
            and   name  = 'Relationship_5_FK'
            and   indid > 0
            and   indid < 255)
   drop index OrderProductItem.Relationship_5_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderProductItem')
            and   type = 'U')
   drop table OrderProductItem
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Organizations')
            and   name  = 'Relationship_11_FK'
            and   indid > 0
            and   indid < 255)
   drop index Organizations.Relationship_11_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Organizations')
            and   type = 'U')
   drop table Organizations
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PSize')
            and   type = 'U')
   drop table PSize
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Pipelining')
            and   name  = 'Relationship_69_FK'
            and   indid > 0
            and   indid < 255)
   drop index Pipelining.Relationship_69_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Pipelining')
            and   name  = 'Relationship_68_FK'
            and   indid > 0
            and   indid < 255)
   drop index Pipelining.Relationship_68_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pipelining')
            and   type = 'U')
   drop table Pipelining
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PoColor')
            and   type = 'U')
   drop table PoColor
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Position')
            and   type = 'U')
   drop table Position
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessCraftAction')
            and   type = 'U')
   drop table ProcessCraftAction
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlow')
            and   name  = 'Relationship_57_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlow.Relationship_57_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlow')
            and   name  = 'Relationship_34_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlow.Relationship_34_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessFlow')
            and   type = 'U')
   drop table ProcessFlow
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlowChart')
            and   name  = 'Relationship_33_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlowChart.Relationship_33_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessFlowChart')
            and   type = 'U')
   drop table ProcessFlowChart
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlowChartFlowRelation')
            and   name  = 'Relationship_18_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlowChartFlowRelation.Relationship_18_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlowChartFlowRelation')
            and   name  = 'Relationship_17_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlowChartFlowRelation.Relationship_17_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessFlowChartFlowRelation')
            and   type = 'U')
   drop table ProcessFlowChartFlowRelation
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlowChartGrop')
            and   name  = 'Relationship_51_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlowChartGrop.Relationship_51_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlowChartGrop')
            and   name  = 'Relationship_48_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlowChartGrop.Relationship_48_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessFlowChartGrop')
            and   type = 'U')
   drop table ProcessFlowChartGrop
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessFlowSection')
            and   type = 'U')
   drop table ProcessFlowSection
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlowStatingItem')
            and   name  = 'Relationship_55_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlowStatingItem.Relationship_55_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlowStatingItem')
            and   name  = 'Relationship_52_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlowStatingItem.Relationship_52_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessFlowStatingItem')
            and   type = 'U')
   drop table ProcessFlowStatingItem
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessFlowVersion')
            and   name  = 'Relationship_32_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessFlowVersion.Relationship_32_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessFlowVersion')
            and   type = 'U')
   drop table ProcessFlowVersion
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessOrder')
            and   name  = 'Relationship_37_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessOrder.Relationship_37_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessOrder')
            and   name  = 'Relationship_36_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessOrder.Relationship_36_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessOrder')
            and   type = 'U')
   drop table ProcessOrder
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessOrderColorItem')
            and   name  = 'Relationship_59_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessOrderColorItem.Relationship_59_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessOrderColorItem')
            and   name  = 'Relationship_39_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessOrderColorItem.Relationship_39_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessOrderColorItem')
            and   name  = 'Relationship_4_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessOrderColorItem.Relationship_4_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessOrderColorItem')
            and   type = 'U')
   drop table ProcessOrderColorItem
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessOrderColorSizeItem')
            and   name  = 'Relationship_40_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessOrderColorSizeItem.Relationship_40_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProcessOrderColorSizeItem')
            and   name  = 'Relationship_38_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProcessOrderColorSizeItem.Relationship_38_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProcessOrderColorSizeItem')
            and   type = 'U')
   drop table ProcessOrderColorSizeItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProdType')
            and   type = 'U')
   drop table ProdType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductGroup')
            and   type = 'U')
   drop table ProductGroup
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductOrder')
            and   type = 'U')
   drop table ProductOrder
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

if exists (select 1
            from  sysindexes
           where  id    = object_id('ProductsHangPieceResume')
            and   name  = 'Relationship_60_FK'
            and   indid > 0
            and   indid < 255)
   drop index ProductsHangPieceResume.Relationship_60_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ProductsHangPieceResume')
            and   type = 'U')
   drop table ProductsHangPieceResume
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Province')
            and   type = 'U')
   drop table Province
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Roles')
            and   type = 'U')
   drop table Roles
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RolesModules')
            and   name  = 'Relationship_27_FK'
            and   indid > 0
            and   indid < 255)
   drop index RolesModules.Relationship_27_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('RolesModules')
            and   name  = 'Relationship_26_FK'
            and   indid > 0
            and   indid < 255)
   drop index RolesModules.Relationship_26_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RolesModules')
            and   type = 'U')
   drop table RolesModules
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SiteGroup')
            and   type = 'U')
   drop table SiteGroup
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Stating')
            and   name  = 'Relationship_72_FK'
            and   indid > 0
            and   indid < 255)
   drop index Stating.Relationship_72_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Stating')
            and   name  = 'Relationship_71_FK'
            and   indid > 0
            and   indid < 255)
   drop index Stating.Relationship_71_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Stating')
            and   name  = 'Relationship_49_FK'
            and   indid > 0
            and   indid < 255)
   drop index Stating.Relationship_49_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Stating')
            and   name  = 'Relationship_9_FK'
            and   indid > 0
            and   indid < 255)
   drop index Stating.Relationship_9_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Stating')
            and   type = 'U')
   drop table Stating
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StatingDirection')
            and   type = 'U')
   drop table StatingDirection
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StatingHangerProductItem')
            and   type = 'U')
   drop table StatingHangerProductItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StatingRoles')
            and   type = 'U')
   drop table StatingRoles
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Style')
            and   type = 'U')
   drop table Style
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('StyleProcessFlowItem')
            and   name  = 'Relationship_41_FK'
            and   indid > 0
            and   indid < 255)
   drop index StyleProcessFlowItem.Relationship_41_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('StyleProcessFlowItem')
            and   name  = 'Relationship_3_FK'
            and   indid > 0
            and   indid < 255)
   drop index StyleProcessFlowItem.Relationship_3_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StyleProcessFlowItem')
            and   type = 'U')
   drop table StyleProcessFlowItem
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('StyleProcessFlowSectionItem')
            and   name  = 'Relationship_44_FK'
            and   indid > 0
            and   indid < 255)
   drop index StyleProcessFlowSectionItem.Relationship_44_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('StyleProcessFlowSectionItem')
            and   name  = 'Relationship_42_FK'
            and   indid > 0
            and   indid < 255)
   drop index StyleProcessFlowSectionItem.Relationship_42_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('StyleProcessFlowSectionItem')
            and   name  = 'Relationship_43_FK'
            and   indid > 0
            and   indid < 255)
   drop index StyleProcessFlowSectionItem.Relationship_43_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('StyleProcessFlowSectionItem')
            and   type = 'U')
   drop table StyleProcessFlowSectionItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SuccessStatingHangerProductItem')
            and   type = 'U')
   drop table SuccessStatingHangerProductItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SucessHangerProductItem')
            and   type = 'U')
   drop table SucessHangerProductItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SucessHangerReworkRecord')
            and   type = 'U')
   drop table SucessHangerReworkRecord
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SucessProcessOrderHanger')
            and   type = 'U')
   drop table SucessProcessOrderHanger
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SucessProducts')
            and   type = 'U')
   drop table SucessProducts
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SusLanguage')
            and   type = 'U')
   drop table SusLanguage
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TestSiteTable')
            and   type = 'U')
   drop table TestSiteTable
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserClientMachines')
            and   type = 'U')
   drop table UserClientMachines
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserLoginInfo')
            and   type = 'U')
   drop table UserLoginInfo
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('UserOperateLogDetail')
            and   name  = 'Relationship_75_FK'
            and   indid > 0
            and   indid < 255)
   drop index UserOperateLogDetail.Relationship_75_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserOperateLogDetail')
            and   type = 'U')
   drop table UserOperateLogDetail
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserOperateLogs')
            and   type = 'U')
   drop table UserOperateLogs
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('UserRoles')
            and   name  = 'Relationship_73_FK'
            and   indid > 0
            and   indid < 255)
   drop index UserRoles.Relationship_73_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('UserRoles')
            and   name  = 'Relationship_70_FK'
            and   indid > 0
            and   indid < 255)
   drop index UserRoles.Relationship_70_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserRoles')
            and   type = 'U')
   drop table UserRoles
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserSiteGroup')
            and   type = 'U')
   drop table UserSiteGroup
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserWorkshops')
            and   type = 'U')
   drop table UserWorkshops
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Users')
            and   name  = 'Relationship_76_FK'
            and   indid > 0
            and   indid < 255)
   drop index Users.Relationship_76_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Users')
            and   type = 'U')
   drop table Users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WaitProcessOrderHanger')
            and   type = 'U')
   drop table WaitProcessOrderHanger
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WorkType')
            and   type = 'U')
   drop table WorkType
go

/*==============================================================*/
/* Table: Area                                                  */
/*==============================================================*/
create table Area (
   Id                   char(32)             not null,
   CITY_Id              char(32)             null,
   AreaName             varchar(100)         null,
   AreaCode             char(10)             null,
   Addess               varchar(500)         null,
   Memo                 varchar(500)         null,
   constraint PK_AREA primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Area') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Area' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '地区', 
   'user', @CurrentUser, 'table', 'Area'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Area')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Area', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'Area', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Area')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AreaName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Area', 'column', 'AreaName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '地区',
   'user', @CurrentUser, 'table', 'Area', 'column', 'AreaName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Area')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Addess')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Area', 'column', 'Addess'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '详细地址',
   'user', @CurrentUser, 'table', 'Area', 'column', 'Addess'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Area')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Area', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Area', 'column', 'Memo'
go

/*==============================================================*/
/* Index: Relationship_46_FK                                    */
/*==============================================================*/
create index Relationship_46_FK on Area (
CITY_Id ASC
)
go

/*==============================================================*/
/* Table: BasicProcessFlow                                      */
/*==============================================================*/
create table BasicProcessFlow (
   Id                   char(32)             not null,
   ProcessCode          char(30)             null,
   ProcessStatus        tinyint              null,
   ProcessName          char(30)             null,
   SortNo               char(20)             null,
   SAM                  char(20)             null,
   StanardHours         char(20)             null,
   StandardPrice        decimal(8,3)         null,
   PrcocessRmark        char(100)            null,
   DefaultFlowNo        int                  null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_BASICPROCESSFLOW primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('BasicProcessFlow') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'BasicProcessFlow' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '基本工序库', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'ProcessCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'ProcessCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'ProcessStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序状态
   0:待走流程
   1:流程已经完成
   ',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'ProcessStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'ProcessName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'ProcessName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SortNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'SortNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序号',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'SortNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SAM')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'SAM'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SAM',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'SAM'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StanardHours')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'StanardHours'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标准工时',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'StanardHours'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StandardPrice')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'StandardPrice'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标准工价',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'StandardPrice'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrcocessRmark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'PrcocessRmark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺说明',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'PrcocessRmark'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefaultFlowNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'DefaultFlowNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '默认工序号',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'DefaultFlowNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('BasicProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'BasicProcessFlow', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: City                                                  */
/*==============================================================*/
create table City (
   Id                   char(32)             not null,
   PROVINCE_Id          char(32)             null,
   CityCode             varchar(30)          null,
   CityName             varchar(30)          null,
   constraint PK_CITY primary key nonclustered (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('City')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PROVINCE_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'City', 'column', 'PROVINCE_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'City', 'column', 'PROVINCE_Id'
go

/*==============================================================*/
/* Index: Relationship_45_FK                                    */
/*==============================================================*/
create index Relationship_45_FK on City (
PROVINCE_Id ASC
)
go

/*==============================================================*/
/* Table: ClassesInfo                                           */
/*==============================================================*/
create table ClassesInfo (
   Id                   char(32)             not null,
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
/* Table: ClientMachines                                        */
/*==============================================================*/
create table ClientMachines (
   Id                   char(32)             not null,
   ClientMachineName
   
   客户机
   
   ClientMachine varchar(20)          not null,
   AuthorizationInformation text                 not null,
   Description          varchar(200)         null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   constraint PK_CLIENTMACHINES primary key nonclustered (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClientMachines')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClientMachines')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClientMachines')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClientMachines')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ClientMachines')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ClientMachines', 'column', 'Deleted'
go

/*==============================================================*/
/* Table: Company                                               */
/*==============================================================*/
create table Company (
   Id                   char(32)             not null,
   CompanyCode          varchar(100)         null,
   CompanyName          varchar(200)         null,
   Address              varchar(500)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   constraint PK_COMPANY primary key nonclustered (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'Company', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'CompanyCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司编号',
   'user', @CurrentUser, 'table', 'Company', 'column', 'CompanyCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'CompanyName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司名称',
   'user', @CurrentUser, 'table', 'Company', 'column', 'CompanyName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Address')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'Address'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司地址',
   'user', @CurrentUser, 'table', 'Company', 'column', 'Address'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Company', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Company', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Company', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Company', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Company')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Company', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Company', 'column', 'Deleted'
go

/*==============================================================*/
/* Table: Customer                                              */
/*==============================================================*/
create table Customer (
   Id                   char(32)             not null,
   CusNo                varchar(100)         null,
   CusName              varchar(100)         null,
   PurchaseOrderNo      varchar(200)         null,
   Address              varchar(100)         null,
   LinkMan              varchar(100)         null,
   Tel                  varchar(100)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_CUSTOMER primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Customer') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Customer' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '客户', 
   'user', @CurrentUser, 'table', 'Customer'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CusNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'CusNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户编号',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'CusNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CusName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'CusName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户名称',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'CusName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PurchaseOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'PurchaseOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'PO编号(外贸订单号)',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'PurchaseOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Address')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'Address'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户地址',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'Address'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LinkMan')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'LinkMan'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '联系人',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'LinkMan'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Tel')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'Tel'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电话',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'Tel'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: CustomerPurchaseOrder                                 */
/*==============================================================*/
create table CustomerPurchaseOrder (
   Id                   char(32)             not null,
   CUSTOMER_Id          char(32)             null,
   STYLE_Id             char(32)             null,
   CusNo                varchar(100)         null,
   CusName              varchar(100)         null,
   CusPurOrderNum       bigint               null,
   PurchaseOrderNo      varchar(200)         null,
   OrderNo              varchar(500)         null,
   GeneratorDate        datetime             null,
   DeliveryDate         datetime             null,
   Mobile               varchar(20)          null,
   DeliverAddress       varchar(100)         null,
   Address              varchar(500)         null,
   LinkMan              varchar(100)         null,
   Tel                  varchar(100)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_CUSTOMERPURCHASEORDER primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('CustomerPurchaseOrder') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '客户外贸单', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CusNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'CusNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户编号',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'CusNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CusName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'CusName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户名称',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'CusName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CusPurOrderNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'CusPurOrderNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'CusPurOrderNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PurchaseOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'PurchaseOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'PO编号(外贸订单号)',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'PurchaseOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'OrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户订单号',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'OrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GeneratorDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'GeneratorDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '下单日期',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'GeneratorDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeliveryDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'DeliveryDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '交货日期',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'DeliveryDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Mobile')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'Mobile'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发货地址',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'Mobile'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeliverAddress')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'DeliverAddress'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司地址',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'DeliverAddress'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Address')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'Address'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司地址',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'Address'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LinkMan')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'LinkMan'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '联系人',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'LinkMan'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Tel')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'Tel'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电话',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'Tel'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrder', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_58_FK                                    */
/*==============================================================*/
create index Relationship_58_FK on CustomerPurchaseOrder (
CUSTOMER_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_65_FK                                    */
/*==============================================================*/
create index Relationship_65_FK on CustomerPurchaseOrder (
STYLE_Id ASC
)
go

/*==============================================================*/
/* Table: CustomerPurchaseOrderColorItem                        */
/*==============================================================*/
create table CustomerPurchaseOrderColorItem (
   Id                   char(32)             not null,
   POCOLOR_Id           char(32)             null,
   CUSTOMERPURCHASEORDER_Id char(32)             null,
   MOrderItemNo         varchar(50)          null,
   Color                char(20)             null,
   ColorDescription     varchar(50)          null,
   Total                char(50)             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_CUSTOMERPURCHASEORDERCOLORI primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('CustomerPurchaseOrderColorItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '客户外贸订单明细颜色', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MOrderItemNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'MOrderItemNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编号',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'MOrderItemNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Color')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'Color'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'Color'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorDescription')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'ColorDescription'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色描述',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'ColorDescription'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Total')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'Total'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'Total'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_61_FK                                    */
/*==============================================================*/
create index Relationship_61_FK on CustomerPurchaseOrderColorItem (
CUSTOMERPURCHASEORDER_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_63_FK                                    */
/*==============================================================*/
create index Relationship_63_FK on CustomerPurchaseOrderColorItem (
POCOLOR_Id ASC
)
go

/*==============================================================*/
/* Table: CustomerPurchaseOrderColorSizeItem                    */
/*==============================================================*/
create table CustomerPurchaseOrderColorSizeItem (
   Id                   char(32)             not null,
   CUSTOMERPURCHASEORDERCOLORITEM_Id char(32)             null,
   PSIZE_Id             char(32)             null,
   SizeDesption         varchar(50)          null,
   Total                varchar(50)          null,
   Memo                 varchar(100)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_CUSTOMERPURCHASEORDERCOLORS primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('CustomerPurchaseOrderColorSizeItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '客户外贸订单明细颜色尺码明细', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeDesption')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'SizeDesption'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'SizeDesption'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Total')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'Total'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'Total'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('CustomerPurchaseOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'CustomerPurchaseOrderColorSizeItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_62_FK                                    */
/*==============================================================*/
create index Relationship_62_FK on CustomerPurchaseOrderColorSizeItem (
CUSTOMERPURCHASEORDERCOLORITEM_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_64_FK                                    */
/*==============================================================*/
create index Relationship_64_FK on CustomerPurchaseOrderColorSizeItem (
PSIZE_Id ASC
)
go

/*==============================================================*/
/* Table: DefectCodeTable                                       */
/*==============================================================*/
create table DefectCodeTable (
   Id                   char(32)             not null,
   DefectNo             char(20)             null,
   DefectCode           char(50)             null,
   DefectName           char(100)            null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   constraint PK_DEFECTCODETABLE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('DefectCodeTable') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'DefectCodeTable' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '疵点代码表', 
   'user', @CurrentUser, 'table', 'DefectCodeTable'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('DefectCodeTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefectNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'DefectNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'DefectNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('DefectCodeTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefectCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'DefectCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '疵点代码',
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'DefectCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('DefectCodeTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefectName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'DefectName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '疵点名称',
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'DefectName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('DefectCodeTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('DefectCodeTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('DefectCodeTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('DefectCodeTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('DefectCodeTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'DefectCodeTable', 'column', 'Deleted'
go

/*==============================================================*/
/* Table: Department                                            */
/*==============================================================*/
create table Department (
   Id                   char(32)             not null,
   DepNo                varchar(100)         null,
   DepName              varchar(200)         null,
   Memo                 varchar(1000)        null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_DEPARTMENT primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Department') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Department' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '部门', 
   'user', @CurrentUser, 'table', 'Department'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'Department', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DepNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'DepNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门编号',
   'user', @CurrentUser, 'table', 'Department', 'column', 'DepNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DepName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'DepName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门名称',
   'user', @CurrentUser, 'table', 'Department', 'column', 'DepName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Department', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Department', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Department', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Department', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Department', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Department', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Department')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Department', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Department', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: Employee                                              */
/*==============================================================*/
create table Employee (
   Id                   char(32)             not null,
   DEPARTMENT_Id        char(32)             null,
   AREA_Id              char(32)             null,
   ORGANIZATIONS_Id     char(32)             null,
   WORKTYPE_Id          char(32)             null,
   SITEGROUP_Id         char(32)             null,
   Code                 varchar(20)          null,
   Password             varchar(200)         null,
   RealName             varchar(20)          null,
   Birthday             datetime             null,
   HeadImage            image                null,
   Sex                  tinyint              null,
   Email                varchar(30)          null,
   CardNo               varchar(30)          null,
   Phone                varchar(11)          null,
   Mobile               varchar(16)          null,
   Valid                bit                  null,
   EmploymentDate       datetime             null,
   Address              varchar(200)         null,
   StartingDate         datetime             null,
   LeaveDate            datetime             null,
   BankCardNo           varchar(16)          null,
   Memo                 varchar(200)         null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_EMPLOYEE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Employee') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Employee' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '员工', 
   'user', @CurrentUser, 'table', 'Employee'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DEPARTMENT_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'DEPARTMENT_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'DEPARTMENT_Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AREA_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'AREA_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'AREA_Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'WORKTYPE_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'WORKTYPE_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'WORKTYPE_Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Code')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Code'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '员工编号',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Code'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Password')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Password'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Password'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RealName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'RealName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '员工姓名',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'RealName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Sex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Sex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '性别',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Sex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Email')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Email'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Email',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Email'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CardNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'CardNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '卡号',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'CardNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Phone')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Phone'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '手机号',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Phone'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Mobile')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Mobile'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电话',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Mobile'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Valid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Valid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否有效',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Valid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EmploymentDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'EmploymentDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '录用日期',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'EmploymentDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Address')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Address'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '住址',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Address'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StartingDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'StartingDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '入职时间',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'StartingDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LeaveDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'LeaveDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '离职日期',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'LeaveDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BankCardNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'BankCardNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '银行卡号',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'BankCardNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Employee')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Employee', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Employee', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_12_FK                                    */
/*==============================================================*/
create index Relationship_12_FK on Employee (
ORGANIZATIONS_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_28_FK                                    */
/*==============================================================*/
create index Relationship_28_FK on Employee (
DEPARTMENT_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_29_FK                                    */
/*==============================================================*/
create index Relationship_29_FK on Employee (
WORKTYPE_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_47_FK                                    */
/*==============================================================*/
create index Relationship_47_FK on Employee (
AREA_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_74_FK                                    */
/*==============================================================*/
create index Relationship_74_FK on Employee (
SITEGROUP_Id ASC
)
go

/*==============================================================*/
/* Table: EmployeeGrade                                         */
/*==============================================================*/
create table EmployeeGrade (
   Id                   char(32)             not null,
   GradeCode            varchar(20)          null,
   GradeName            varchar(30)          null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_EMPLOYEEGRADE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('EmployeeGrade') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'EmployeeGrade' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '员工等级', 
   'user', @CurrentUser, 'table', 'EmployeeGrade'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeGrade')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeGrade')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeGrade')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeGrade')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeGrade')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeGrade')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeGrade')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'EmployeeGrade', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: EmployeePositions                                     */
/*==============================================================*/
create table EmployeePositions (
   Id                   char(32)             not null,
   POSITION_Id          char(32)             null,
   EMPLOYEE_Id          char(32)             null,
   constraint PK_EMPLOYEEPOSITIONS primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('EmployeePositions') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'EmployeePositions' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '员工职务', 
   'user', @CurrentUser, 'table', 'EmployeePositions'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeePositions')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'POSITION_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeePositions', 'column', 'POSITION_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'EmployeePositions', 'column', 'POSITION_Id'
go

/*==============================================================*/
/* Index: Relationship_31_FK                                    */
/*==============================================================*/
create index Relationship_31_FK on EmployeePositions (
EMPLOYEE_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_35_FK                                    */
/*==============================================================*/
create index Relationship_35_FK on EmployeePositions (
POSITION_Id ASC
)
go

/*==============================================================*/
/* Table: EmployeeScheduling                                    */
/*==============================================================*/
create table EmployeeScheduling (
   Id                   char(32)             not null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_EMPLOYEESCHEDULING primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('EmployeeScheduling') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'EmployeeScheduling' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '员工排班信息', 
   'user', @CurrentUser, 'table', 'EmployeeScheduling'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeScheduling')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeScheduling')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeScheduling')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeScheduling')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeScheduling')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeScheduling')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记',
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('EmployeeScheduling')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'EmployeeScheduling', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: FlowAction                                            */
/*==============================================================*/
create table FlowAction (
   Id                   char(32)             not null,
   ActionCode           varchar(20)          null,
   ActionName           varchar(50)          null,
   IsEnabled            tinyint              null,
   constraint PK_FLOWACTION primary key nonclustered (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowAction', 'column', 'ActionCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作编号',
   'user', @CurrentUser, 'table', 'FlowAction', 'column', 'ActionCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowAction', 'column', 'ActionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作名称',
   'user', @CurrentUser, 'table', 'FlowAction', 'column', 'ActionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsEnabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowAction', 'column', 'IsEnabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否启用',
   'user', @CurrentUser, 'table', 'FlowAction', 'column', 'IsEnabled'
go

/*==============================================================*/
/* Table: FlowStatingColor                                      */
/*==============================================================*/
create table FlowStatingColor (
   Id                   char(32)             not null,
   PROCESSFLOWSTATINGITEM_Id char(32)             null,
   StatingName          char(20)             null,
   StatingNo            char(20)             null,
   ColorValue           char(20)             null,
   ColorDesption        char(50)             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_FLOWSTATINGCOLOR primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('FlowStatingColor') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'FlowStatingColor' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工艺工序制作站点指定的颜色', 
   'user', @CurrentUser, 'table', 'FlowStatingColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PROCESSFLOWSTATINGITEM_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'PROCESSFLOWSTATINGITEM_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'PROCESSFLOWSTATINGITEM_Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'StatingName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点名称',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'StatingName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'StatingNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点编号',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'StatingNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'ColorValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'ColorValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorDesption')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'ColorDesption'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码描述',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'ColorDesption'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'FlowStatingColor', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_50_FK                                    */
/*==============================================================*/
create index Relationship_50_FK on FlowStatingColor (
PROCESSFLOWSTATINGITEM_Id ASC
)
go

/*==============================================================*/
/* Table: FlowStatingResume                                     */
/*==============================================================*/
create table FlowStatingResume (
   Id                   char(32)             not null,
   STATING_Id           char(32)             null,
   PROCESSFLOWCHARTFLOWRELATION_Id char(32)             null,
   Memo                 varchar(100)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_FLOWSTATINGRESUME primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('FlowStatingResume') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'FlowStatingResume' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工序制作站变更履历', 
   'user', @CurrentUser, 'table', 'FlowStatingResume'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'FlowStatingResume', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_54_FK                                    */
/*==============================================================*/
create index Relationship_54_FK on FlowStatingResume (
PROCESSFLOWCHARTFLOWRELATION_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_53_FK                                    */
/*==============================================================*/
create index Relationship_53_FK on FlowStatingResume (
STATING_Id ASC
)
go

/*==============================================================*/
/* Table: FlowStatingSize                                       */
/*==============================================================*/
create table FlowStatingSize (
   Id                   char(32)             not null,
   PROCESSFLOWSTATINGITEM_Id char(32)             null,
   StatingName          char(20)             null,
   StatingNo            char(20)             null,
   ColorValue           varchar(50)          null,
   ColorDesption        char(50)             null,
   Total                char(50)             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_FLOWSTATINGSIZE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('FlowStatingSize') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'FlowStatingSize' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工艺工序制作站点指定的尺码', 
   'user', @CurrentUser, 'table', 'FlowStatingSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PROCESSFLOWSTATINGITEM_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'PROCESSFLOWSTATINGITEM_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'PROCESSFLOWSTATINGITEM_Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'StatingName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点名称',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'StatingName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'StatingNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点编号',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'StatingNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'ColorValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'ColorValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorDesption')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'ColorDesption'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码描述',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'ColorDesption'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Total')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'Total'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'Total'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('FlowStatingSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'FlowStatingSize', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_56_FK                                    */
/*==============================================================*/
create index Relationship_56_FK on FlowStatingSize (
PROCESSFLOWSTATINGITEM_Id ASC
)
go

/*==============================================================*/
/* Table: Hanger                                                */
/*==============================================================*/
create table Hanger (
   Id                   char(32)             not null,
   constraint PK_HANGER primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Hanger') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Hanger' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '衣架', 
   'user', @CurrentUser, 'table', 'Hanger'
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
   IsFlowChatChange     bit                  null,
   IsIncomeSite         bit                  null,
   IsSucessedFlow       bit                  null,
   IsReturnWorkFlow     bit                  null,
   returnWorkSiteNo     char(10)             null,
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
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSucessedFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IsSucessedFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序是否完成',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IsSucessedFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsReturnWorkFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IsReturnWorkFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否是返工工序',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'IsReturnWorkFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'returnWorkSiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'returnWorkSiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '返工来源站号',
   'user', @CurrentUser, 'table', 'HangerProductItem', 'column', 'returnWorkSiteNo'
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

/*==============================================================*/
/* Table: HangerReworkRecord                                    */
/*==============================================================*/
create table HangerReworkRecord (
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
   IsFlowChatChange     bit                  null,
   IsIncomeSite         bit                  null,
   IsSucessedFlow       bit                  null,
   returnWorkSiteNo     char(10)             null,
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
   DefectCode           varchar(50)          null,
   constraint PK_HANGERREWORKRECORD primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('HangerReworkRecord') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'HangerReworkRecord' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '衣架返工记录', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单Id',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowChartd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'FlowChartd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺路线图Id',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'FlowChartd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'SizeNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'SizeNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序Id',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ProcessFlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序索引',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'SiteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站Id',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'SiteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'SiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站号',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'SiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowChatChange')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'IsFlowChatChange'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线图是否改变',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'IsFlowChatChange'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsIncomeSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'IsIncomeSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否进站',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'IsIncomeSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSucessedFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'IsSucessedFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序是否完成',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'IsSucessedFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'returnWorkSiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'returnWorkSiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '返工来源站号',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'returnWorkSiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientMachineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ClientMachineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户机Id',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'ClientMachineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SusLineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'SusLineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '吊挂线Id',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'SusLineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IncomeSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'IncomeSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '进站时间',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'IncomeSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompareDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'CompareDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '比较时间',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'CompareDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OutSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'OutSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出战时间',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'OutSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'CompanyId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefectCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'DefectCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '返工疵点代码',
   'user', @CurrentUser, 'table', 'HangerReworkRecord', 'column', 'DefectCode'
go

/*==============================================================*/
/* Table: HolidayInfo                                           */
/*==============================================================*/
create table HolidayInfo (
   Id                   char(32)             not null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_HOLIDAYINFO primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('HolidayInfo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'HolidayInfo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '假日信息', 
   'user', @CurrentUser, 'table', 'HolidayInfo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HolidayInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HolidayInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HolidayInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HolidayInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HolidayInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HolidayInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('HolidayInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'HolidayInfo', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: ID_GENERATOR                                          */
/*==============================================================*/
create table ID_GENERATOR (
   ID                   char(32)             not null,
   FLAG_NO              varchar(50)          not null,
   BEGIN_VALUE          bigint               not null,
   CURRENT_VALUE        bigint               not null,
   END_VALUE            bigint               not null,
   SORT_VALUE           bigint               null,
   MEMO                 nvarchar(200)        null,
   CompanyId            char(32)             null,
   constraint PK_ID_GENERATOR primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ID_GENERATOR') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ID_GENERATOR' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '号码段的分类
   @needMeta=false', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ID_GENERATOR')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '@generateColumnConfig=sequence',
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ID_GENERATOR')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLAG_NO')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'FLAG_NO'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标示符',
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'FLAG_NO'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ID_GENERATOR')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BEGIN_VALUE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'BEGIN_VALUE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开始值',
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'BEGIN_VALUE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ID_GENERATOR')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CURRENT_VALUE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'CURRENT_VALUE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '目前值',
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'CURRENT_VALUE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ID_GENERATOR')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'END_VALUE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'END_VALUE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '结束值',
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'END_VALUE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ID_GENERATOR')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SORT_VALUE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'SORT_VALUE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序值',
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'SORT_VALUE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ID_GENERATOR')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MEMO')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'MEMO'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'MEMO'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ID_GENERATOR')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '@needUpdate=false
   @generateColumnConfig=fix:data=1',
   'user', @CurrentUser, 'table', 'ID_GENERATOR', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: LackMaterialsTable                                    */
/*==============================================================*/
create table LackMaterialsTable (
   Id                   char(32)             not null,
   LackMaterialsCode    char(30)             null,
   LackMaterialsName    char(50)             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   constraint PK_LACKMATERIALSTABLE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('LackMaterialsTable') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'LackMaterialsTable' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '缺料代码表', 
   'user', @CurrentUser, 'table', 'LackMaterialsTable'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LackMaterialsTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LackMaterialsCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'LackMaterialsCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '缺料代码',
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'LackMaterialsCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LackMaterialsTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LackMaterialsName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'LackMaterialsName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '缺料名称',
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'LackMaterialsName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LackMaterialsTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LackMaterialsTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LackMaterialsTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LackMaterialsTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('LackMaterialsTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'LackMaterialsTable', 'column', 'Deleted'
go

/*==============================================================*/
/* Table: Modules                                               */
/*==============================================================*/
create table Modules (
   Id                   char(32)             not null,
   MODULES_Id           char(32)             null,
   ActionName           varchar(256)         not null,
   ActionKey            varchar(50)          not null,
   Description          varchar(256)         null,
   ModulesType          int                  not null,
   ModuleLevel          int                  not null,
   OrderField           decimal(5,2)         not null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_MODULES primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Modules') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Modules' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '菜单 模块表', 
   'user', @CurrentUser, 'table', 'Modules'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'ActionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作名称',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'ActionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionKey')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'ActionKey'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作Key',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'ActionKey'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Description')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'Description'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '描述',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'Description'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ModulesType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'ModulesType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1:菜单
   2:按钮',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'ModulesType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Modules')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Modules', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Modules', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_21_FK                                    */
/*==============================================================*/
create index Relationship_21_FK on Modules (
MODULES_Id ASC
)
go

/*==============================================================*/
/* Table: OrderProductItem                                      */
/*==============================================================*/
create table OrderProductItem (
   Id                   char(32)             not null,
   PRODUCTORDER_Id      char(32)             null,
   SequenceNumber       char(20)             null,
   ProductNo            char(20)             null,
   ProductName          char(50)             null,
   Color                char(20)             null,
   "Rule"               char(20)             null,
   ProductNum           bigint               null,
   ProductUnit          char(20)             null,
   BoxNum               int                  null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_ORDERPRODUCTITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('OrderProductItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'OrderProductItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '加工单产品明细', 
   'user', @CurrentUser, 'table', 'OrderProductItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SequenceNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'SequenceNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'SequenceNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'ProductNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产品编号',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'ProductNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'ProductName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产品名称',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'ProductName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Color')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'Color'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'Color'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Rule')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'Rule'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'Rule'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'ProductNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产品数量',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'ProductNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductUnit')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'ProductUnit'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'ProductUnit'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BoxNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'BoxNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '每箱数量',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'BoxNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('OrderProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'OrderProductItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_5_FK                                     */
/*==============================================================*/
create index Relationship_5_FK on OrderProductItem (
PRODUCTORDER_Id ASC
)
go

/*==============================================================*/
/* Table: Organizations                                         */
/*==============================================================*/
create table Organizations (
   Id                   char(32)             not null,
   ORGANIZATIONS_Id     char(32)             null,
   Code                 varchar(20)          null,
   ParentCode           varchar(20)          null,
   ActionName           varchar(256)         null,
   Description          varchar(256)         null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_ORGANIZATIONS primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Organizations') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Organizations' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '组织机构', 
   'user', @CurrentUser, 'table', 'Organizations'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Organizations')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'ActionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作名称',
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'ActionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Organizations')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Description')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'Description'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '描述',
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'Description'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Organizations')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Organizations')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Organizations')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Organizations')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Organizations')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Organizations')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Organizations', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_11_FK                                    */
/*==============================================================*/
create index Relationship_11_FK on Organizations (
ORGANIZATIONS_Id ASC
)
go

/*==============================================================*/
/* Table: PSize                                                 */
/*==============================================================*/
create table PSize (
   Id                   char(32)             not null,
   PSNo                 varchar(50)          null,
   Size                 varchar(100)         null,
   SizeDesption         varchar(100)         null,
   Memo                 varchar(100)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PSIZE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PSize') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PSize' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '尺码', 
   'user', @CurrentUser, 'table', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'PSNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'PSNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Size')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'Size'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'Size'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeDesption')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'SizeDesption'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码描述',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'SizeDesption'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PSize')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PSize', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'PSize', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: Pipelining                                            */
/*==============================================================*/
create table Pipelining (
   Id                   char(32)             not null,
   SITEGROUP_Id         char(32)             null,
   PRODTYPE_Id          char(32)             null,
   PipeliNo             varchar(50)          null,
   PushRodNum           int                  null,
   Memo                 varchar(100)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PIPELINING primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Pipelining') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Pipelining' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '流水线', 
   'user', @CurrentUser, 'table', 'Pipelining'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PipeliNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'PipeliNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '流水线号',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'PipeliNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PushRodNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'PushRodNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '推杆数量',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'PushRodNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Pipelining')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Pipelining', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_68_FK                                    */
/*==============================================================*/
create index Relationship_68_FK on Pipelining (
PRODTYPE_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_69_FK                                    */
/*==============================================================*/
create index Relationship_69_FK on Pipelining (
SITEGROUP_Id ASC
)
go

/*==============================================================*/
/* Table: PoColor                                               */
/*==============================================================*/
create table PoColor (
   Id                   char(32)             not null,
   SNo                  varchar(50)          null,
   ColorValue           varchar(50)          null,
   ColorDescption       varchar(50)          null,
   Rmark                char(100)            null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_POCOLOR primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PoColor') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PoColor' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '颜色', 
   'user', @CurrentUser, 'table', 'PoColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'SNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'SNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'ColorValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'ColorValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorDescption')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'ColorDescption'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码描述',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'ColorDescption'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Rmark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'Rmark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'Rmark'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PoColor')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'PoColor', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: Position                                              */
/*==============================================================*/
create table Position (
   Id                   char(32)             not null,
   PosCode              varchar(100)         null,
   PosName              varchar(100)         null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_POSITION primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Position') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Position' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '职务', 
   'user', @CurrentUser, 'table', 'Position'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'Position', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PosCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'PosCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '职务编号',
   'user', @CurrentUser, 'table', 'Position', 'column', 'PosCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PosName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'PosName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '职务名称',
   'user', @CurrentUser, 'table', 'Position', 'column', 'PosName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Position', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Position', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Position', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Position', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Position', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Position')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Position', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Position', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: ProcessCraftAction                                    */
/*==============================================================*/
create table ProcessCraftAction (
   Id                   char(32)             not null,
   ActionNo             char(20)             null,
   ActionDesc           char(50)             null,
   IsEnabled            tinyint              null,
   ProSectionNo         char(20)             null,
   ProSectionCode       char(50)             null,
   ProSectionName       char(100)            null,
   Remark2              char(500)            null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSCRAFTACTION primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessCraftAction') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessCraftAction' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工艺图制作动作', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ActionNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作编号',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ActionNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionDesc')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ActionDesc'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作描述',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ActionDesc'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsEnabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'IsEnabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否生效',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'IsEnabled'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProSectionNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ProSectionNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序段序号',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ProSectionNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProSectionCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ProSectionCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序段代码',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ProSectionCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProSectionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ProSectionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序段名称',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'ProSectionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Remark2')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'Remark2'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'Remark2'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记：
   1：已删除；0正常',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessCraftAction')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessCraftAction', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: ProcessFlow                                           */
/*==============================================================*/
create table ProcessFlow (
   Id                   char(32)             not null,
   PROCESSFLOWVERSION_Id char(32)             null,
   BASICPROCESSFLOW_Id  char(32)             null,
   ProcessNo            char(20)             null,
   ProcessName          char(200)            null,
   ProcessCode          char(20)             null,
   ProcessStatus        tinyint              null,
   StanardHours         varchar(20)          null,
   StandardPrice        decimal(8,3)         null,
   DefaultFlowNo        int                  null,
   PrcocessRemark       char(100)            null,
   ProcessColor         varchar(20)          null,
   EffectiveDate        datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSFLOW primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessFlow') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessFlow' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '制单工序', 
   'user', @CurrentUser, 'table', 'ProcessFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序编号',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序状态
   0:待走流程
   1:流程已经完成
   ',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StanardHours')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'StanardHours'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标准工时',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'StanardHours'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StandardPrice')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'StandardPrice'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标准工价',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'StandardPrice'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefaultFlowNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'DefaultFlowNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '默认工序号',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'DefaultFlowNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrcocessRemark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'PrcocessRemark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺说明',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'PrcocessRemark'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'ProcessColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EffectiveDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'EffectiveDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生效日期',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'EffectiveDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记:0正常，1:删除',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlow')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessFlow', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_34_FK                                    */
/*==============================================================*/
create index Relationship_34_FK on ProcessFlow (
PROCESSFLOWVERSION_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_57_FK                                    */
/*==============================================================*/
create index Relationship_57_FK on ProcessFlow (
BASICPROCESSFLOW_Id ASC
)
go

/*==============================================================*/
/* Table: ProcessFlowChart                                      */
/*==============================================================*/
create table ProcessFlowChart (
   Id                   char(32)             not null,
   PROCESSFLOWVERSION_Id char(32)             null,
   LinkName             char(500)            null,
   pFlowChartNum        bigint               null,
   ProductPosition      char(50)             null,
   TargetNum            int                  null,
   OutputProcessFlowId  char(32)             null,
   BoltProcessFlowId    char(32)             null,
   Remark               varchar(200)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSFLOWCHART primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessFlowChart') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessFlowChart' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工艺路线图:
   产品在吊挂生产线的生产工序排序及员工任务分配的数据集合', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LinkName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'LinkName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线名称',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'LinkName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'pFlowChartNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'pFlowChartNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'pFlowChartNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductPosition')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'ProductPosition'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产品部位',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'ProductPosition'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TargetNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'TargetNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '目标产量',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'TargetNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OutputProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'OutputProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产出工序',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'OutputProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'BoltProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'BoltProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '挂片开始生产工序',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'BoltProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Remark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'Remark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'Remark'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChart')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessFlowChart', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_33_FK                                    */
/*==============================================================*/
create index Relationship_33_FK on ProcessFlowChart (
PROCESSFLOWVERSION_Id ASC
)
go

/*==============================================================*/
/* Table: ProcessFlowChartFlowRelation                          */
/*==============================================================*/
create table ProcessFlowChartFlowRelation (
   Id                   char(32)             not null,
   PROCESSFLOW_Id       char(32)             null,
   PROCESSFLOWCHART_Id  char(32)             null,
   CraftFlowNo          varchar(20)          null,
   IsEnabled            smallint             null,
   EnabledText          varchar(20)          null,
   IsMergeForward       tinyint              null,
   MergeForwardText     varchar(20)          null,
   FlowNo               varchar(20)          null,
   FlowCode             varchar(20)          null,
   FlowName             varchar(40)          null,
   IsProduceFlow        tinyint              null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSFLOWCHARTFLOWRELATIO primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessFlowChartFlowRelation') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工艺路线图制单工序', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CraftFlowNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'CraftFlowNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '顺序编号',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'CraftFlowNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsEnabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'IsEnabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '顺序编号',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'IsEnabled'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EnabledText')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'EnabledText'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生效文本',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'EnabledText'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsMergeForward')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'IsMergeForward'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否往前合并',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'IsMergeForward'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MergeForwardText')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'MergeForwardText'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '往前合并文本',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'MergeForwardText'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'FlowNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序号',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'FlowNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'FlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'FlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'FlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'FlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsProduceFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'IsProduceFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否是产出工序',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'IsProduceFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartFlowRelation')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessFlowChartFlowRelation', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_17_FK                                    */
/*==============================================================*/
create index Relationship_17_FK on ProcessFlowChartFlowRelation (
PROCESSFLOWCHART_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_18_FK                                    */
/*==============================================================*/
create index Relationship_18_FK on ProcessFlowChartFlowRelation (
PROCESSFLOW_Id ASC
)
go

/*==============================================================*/
/* Table: ProcessFlowChartGrop                                  */
/*==============================================================*/
create table ProcessFlowChartGrop (
   Id                   char(32)             not null,
   SITEGROUP_Id         char(32)             null,
   PROCESSFLOWCHART_Id  char(32)             null,
   GroupNo              varchar(50)          null,
   GroupName            varchar(100)         null,
   constraint PK_PROCESSFLOWCHARTGROP primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessFlowChartGrop') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessFlowChartGrop' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工艺路线图使用组', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartGrop'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartGrop')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartGrop', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'ProcessFlowChartGrop', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartGrop')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GroupNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartGrop', 'column', 'GroupNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组编号',
   'user', @CurrentUser, 'table', 'ProcessFlowChartGrop', 'column', 'GroupNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowChartGrop')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GroupName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowChartGrop', 'column', 'GroupName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组名称',
   'user', @CurrentUser, 'table', 'ProcessFlowChartGrop', 'column', 'GroupName'
go

/*==============================================================*/
/* Index: Relationship_48_FK                                    */
/*==============================================================*/
create index Relationship_48_FK on ProcessFlowChartGrop (
PROCESSFLOWCHART_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_51_FK                                    */
/*==============================================================*/
create index Relationship_51_FK on ProcessFlowChartGrop (
SITEGROUP_Id ASC
)
go

/*==============================================================*/
/* Table: ProcessFlowSection                                    */
/*==============================================================*/
create table ProcessFlowSection (
   Id                   char(32)             not null,
   ProSectionNo         char(20)             null,
   ProSectionName       char(100)            null,
   ProSectionCode       char(50)             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   InsertDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSFLOWSECTION primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessFlowSection') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessFlowSection' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工序段', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProSectionNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'ProSectionNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序段序号',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'ProSectionNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProSectionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'ProSectionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序段名称',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'ProSectionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProSectionCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'ProSectionCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序段代码',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'ProSectionCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowSection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessFlowSection', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: ProcessFlowStatingItem                                */
/*==============================================================*/
create table ProcessFlowStatingItem (
   Id                   char(32)             not null,
   PROCESSFLOWCHARTFLOWRELATION_Id char(32)             null,
   STATING_Id           char(32)             null,
   IsReceivingHanger    tinyint              null,
   ReceingContent       varchar(50)          null,
   No                   varchar(20)          null,
   StatingRoleName      varchar(20)          null,
   Memo                 varchar(100)         null,
   IsReceivingAllSize   bit                  null,
   IsReceivingAllColor  bit                  null,
   IsEndStating         bit                  null,
   Proportion           decimal(3,2)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSFLOWSTATINGITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessFlowStatingItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工序制作站点明细', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsReceivingHanger')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'IsReceivingHanger'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否接收衣架',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'IsReceivingHanger'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ReceingContent')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'ReceingContent'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '接收内容(默认是衣架)',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'ReceingContent'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'No')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'No'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '顺序',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'No'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingRoleName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'StatingRoleName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点角色(备用查询字段)',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'StatingRoleName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsReceivingAllSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'IsReceivingAllSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否全部接收尺码',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'IsReceivingAllSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsReceivingAllColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'IsReceivingAllColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否全部接收颜色',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'IsReceivingAllColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsEndStating')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'IsEndStating'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否是收尾站',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'IsEndStating'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Proportion')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'Proportion'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '接收比例',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'Proportion'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowStatingItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessFlowStatingItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_52_FK                                    */
/*==============================================================*/
create index Relationship_52_FK on ProcessFlowStatingItem (
PROCESSFLOWCHARTFLOWRELATION_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_55_FK                                    */
/*==============================================================*/
create index Relationship_55_FK on ProcessFlowStatingItem (
STATING_Id ASC
)
go

/*==============================================================*/
/* Table: ProcessFlowVersion                                    */
/*==============================================================*/
create table ProcessFlowVersion (
   Id                   char(32)             not null,
   PROCESSORDER_Id      char(32)             null,
   ProVersionNum        varchar(1000)        null,
   ProVersionNo         char(50)             null,
   ProcessVersionName   char(200)            null,
   EffectiveDate        datetime             null,
   TotalStandardPrice   char(32)             null,
   TotalSAM             char(32)             null,
   UpdateDateTime       datetime             null,
   InsertDate           datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSFLOWVERSION primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessFlowVersion') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessFlowVersion' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '制单工序版本', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProVersionNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'ProVersionNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '版本序号',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'ProVersionNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProVersionNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'ProVersionNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '版本编号',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'ProVersionNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessVersionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'ProcessVersionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '版本名称',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'ProcessVersionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EffectiveDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'EffectiveDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生效日期',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'EffectiveDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalStandardPrice')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'TotalStandardPrice'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '总工价',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'TotalStandardPrice'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalSAM')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'TotalSAM'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '总SAM',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'TotalSAM'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'InsertDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'InsertDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessFlowVersion')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessFlowVersion', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_32_FK                                    */
/*==============================================================*/
create index Relationship_32_FK on ProcessFlowVersion (
PROCESSORDER_Id ASC
)
go

/*==============================================================*/
/* Table: ProcessOrder                                          */
/*==============================================================*/
create table ProcessOrder (
   Id                   char(32)             not null,
   STYLE_Id             char(32)             null,
   CUSTOMER_Id          char(32)             null,
   POrderNo             char(200)            null,
   POrderNum            bigint               null,
   MOrderNo             char(20)             null,
   POrderType           tinyint              null,
   POrderTypeDesption   char(50)             null,
   ProductNoticeOrderNo char(20)             null,
   Num                  int                  null,
   Status               tinyint              null,
   StyleCode            char(20)             null,
   StyleName            varchar(100)         null,
   CustomerNO           varchar(100)         null,
   CustomerName         varchar(100)         null,
   CustomerStyle        varchar(100)         null,
   CustOrderNo          varchar(100)         null,
   CustPurchaseOrderNo  varchar(100)         null,
   DeliveryDate         datetime             null,
   GenaterOrderDate     datetime             null,
   OrderNo              char(20)             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSORDER primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessOrder') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessOrder' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '制单', 
   'user', @CurrentUser, 'table', 'ProcessOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'POrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'POrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'POrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'POrderNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'POrderNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'POrderNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'MOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'MOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'POrderType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'POrderType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单类型(1.散客;2.包装;3.无客户制单)',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'POrderType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'POrderTypeDesption')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'POrderTypeDesption'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单类型描述',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'POrderTypeDesption'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductNoticeOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'ProductNoticeOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生成通知单号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'ProductNoticeOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Num')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'Num'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'Num'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Status')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'Status'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单状态
   1:已审核
   2:制作完成
   3:生产中
   4：完成',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'Status'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StyleCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'StyleCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '款号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'StyleCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StyleName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'StyleName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '款式描述',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'StyleName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustomerNO')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustomerNO'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户编号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustomerNO'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustomerName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustomerName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户名称',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustomerName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustomerStyle')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustomerStyle'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户款号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustomerStyle'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户订单号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustPurchaseOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustPurchaseOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户PO号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CustPurchaseOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DeliveryDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'DeliveryDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '交货日期',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'DeliveryDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GenaterOrderDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'GenaterOrderDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '下单日期',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'GenaterOrderDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'OrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单号',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'OrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记:0正常，1:删除',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessOrder', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_36_FK                                    */
/*==============================================================*/
create index Relationship_36_FK on ProcessOrder (
STYLE_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_37_FK                                    */
/*==============================================================*/
create index Relationship_37_FK on ProcessOrder (
CUSTOMER_Id ASC
)
go

/*==============================================================*/
/* Table: ProcessOrderColorItem                                 */
/*==============================================================*/
create table ProcessOrderColorItem (
   Id                   char(32)             not null,
   PROCESSORDER_Id      char(32)             null,
   POCOLOR_Id           char(32)             null,
   CUSTOMERPURCHASEORDER_Id char(32)             null,
   MOrderItemNo         varchar(50)          null,
   Color                varchar(50)          null,
   ColorDescription     varchar(50)          null,
   Total                int                  null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSORDERCOLORITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessOrderColorItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '制单明细颜色', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MOrderItemNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'MOrderItemNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编号',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'MOrderItemNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Color')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'Color'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'Color'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorDescription')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'ColorDescription'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色描述',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'ColorDescription'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Total')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'Total'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码合计',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'Total'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记：0正常，1删除',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessOrderColorItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_4_FK                                     */
/*==============================================================*/
create index Relationship_4_FK on ProcessOrderColorItem (
PROCESSORDER_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_39_FK                                    */
/*==============================================================*/
create index Relationship_39_FK on ProcessOrderColorItem (
POCOLOR_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_59_FK                                    */
/*==============================================================*/
create index Relationship_59_FK on ProcessOrderColorItem (
CUSTOMERPURCHASEORDER_Id ASC
)
go

/*==============================================================*/
/* Table: ProcessOrderColorSizeItem                             */
/*==============================================================*/
create table ProcessOrderColorSizeItem (
   Id                   char(32)             not null,
   PROCESSORDERCOLORITEM_Id char(32)             null,
   PSIZE_Id             char(32)             null,
   SizeDesption         varchar(50)          null,
   Total                varchar(50)          null,
   Memo                 varchar(50)          null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PROCESSORDERCOLORSIZEITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProcessOrderColorSizeItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '制单明细颜色尺码明细', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeDesption')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'SizeDesption'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'SizeDesption'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Total')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'Total'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'Total'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProcessOrderColorSizeItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProcessOrderColorSizeItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_38_FK                                    */
/*==============================================================*/
create index Relationship_38_FK on ProcessOrderColorSizeItem (
PROCESSORDERCOLORITEM_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_40_FK                                    */
/*==============================================================*/
create index Relationship_40_FK on ProcessOrderColorSizeItem (
PSIZE_Id ASC
)
go

/*==============================================================*/
/* Table: ProdType                                              */
/*==============================================================*/
create table ProdType (
   Id                   char(32)             not null,
   PorTypeCode          varchar(50)          null,
   PorTypeName          varchar(100)         null,
   Memo                 varchar(200)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PRODTYPE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProdType') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProdType' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '产线类别', 
   'user', @CurrentUser, 'table', 'ProdType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProdType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProdType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProdType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProdType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProdType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProdType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProdType', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: ProductGroup                                          */
/*==============================================================*/
create table ProductGroup (
   Id                   char(32)             not null,
   ProductGroupCode     varchar(20)          null,
   ProductGroupName     varchar(30)          null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PRODUCTGROUP primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProductGroup') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProductGroup' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '生产组别', 
   'user', @CurrentUser, 'table', 'ProductGroup'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProductGroup', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: ProductOrder                                          */
/*==============================================================*/
create table ProductOrder (
   Id                   char(32)             not null,
   OrderName            char(20)             null,
   OrderPackgeType      numeric              null,
   OrderType            numeric              null,
   VersionNo            char(30)             null,
   ProcessDate          datetime             null,
   ProcessPerson        char(30)             null,
   SystemOrderNo        char(50)             null,
   CustomerOrderNo      char(50)             null,
   ProcessOrderNo       char(50)             null,
   CustomerNo           char(50)             null,
   SucessDate           datetime             null,
   Remark               varchar(200)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PRODUCTORDER primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProductOrder') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProductOrder' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '生产加工单', 
   'user', @CurrentUser, 'table', 'ProductOrder'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OrderName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'OrderName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单名称',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'OrderName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OrderPackgeType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'OrderPackgeType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '包装类型
   1：允许布匹混装
   2:允许产品混装
   3：允许超装
   4：允许工单混装',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'OrderPackgeType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OrderType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'OrderType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '加工单类型
   1:货单
   2：样品单',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'OrderType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'VersionNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'VersionNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '版本号',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'VersionNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'ProcessDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单日期',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'ProcessDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessPerson')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'ProcessPerson'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单人',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'ProcessPerson'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SystemOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'SystemOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统单号',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'SystemOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustomerOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'CustomerOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户单号',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'CustomerOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '加工单号',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustomerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'CustomerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户编号',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'CustomerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SucessDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'SucessDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '完成日期',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'SucessDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Remark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'Remark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'Remark'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductOrder')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProductOrder', 'column', 'CompanyId'
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
   '制品', 
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
   '排产号',
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
   '上线日期',
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
   '挂片站点',
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
   '制单号',
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
   '状态:0:未分配;1:已分配;3.上线;4.已完成',
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
   '客户外贸单Id',
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
   '订单号',
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
   '款号',
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
   '颜色',
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
   'PO号',
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
   '尺码',
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
   '尺码',
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
   '工段',
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
   '单位',
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
   '任务数量',
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
   '在线数',
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
   '今日挂片',
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
   '今日产出',
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
   '今日绑卡',
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
   '今日返工',
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
   '累计挂片',
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
   '累计返工',
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
   '累计绑卡',
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
   '插入时间',
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
   '更新时间',
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
   '插入用户',
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
   '更新用户',
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
   '删除标识',
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
   '公司Id',
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

/*==============================================================*/
/* Table: ProductsHangPieceResume                               */
/*==============================================================*/
create table ProductsHangPieceResume (
   Id                   char(32)             not null,
   PRODUCTS_Id          char(32)             null,
   HangPieceNo          varchar(200)         null,
   HangName             varchar(200)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_PRODUCTSHANGPIECERESUME primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ProductsHangPieceResume') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '制品挂片履历', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PRODUCTS_Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'PRODUCTS_Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'PRODUCTS_Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangPieceNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'HangPieceNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '挂片站编号',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'HangPieceNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'HangName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '挂片站名称',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'HangName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ProductsHangPieceResume')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'ProductsHangPieceResume', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_60_FK                                    */
/*==============================================================*/
create index Relationship_60_FK on ProductsHangPieceResume (
PRODUCTS_Id ASC
)
go

/*==============================================================*/
/* Table: Province                                              */
/*==============================================================*/
create table Province (
   Id                   char(32)             not null,
   ProvinceCode         varchar(20)          null,
   ProvinceName         varchar(20)          null,
   constraint PK_PROVINCE primary key nonclustered (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Province')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Province', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'Province', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Province')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProvinceCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Province', 'column', 'ProvinceCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '省编号',
   'user', @CurrentUser, 'table', 'Province', 'column', 'ProvinceCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Province')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProvinceName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Province', 'column', 'ProvinceName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'Province', 'column', 'ProvinceName'
go

/*==============================================================*/
/* Table: Roles                                                 */
/*==============================================================*/
create table Roles (
   Id                   char(32)             not null,
   ActionName           varchar(256)         null,
   Description          varchar(256)         null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_ROLES primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Roles') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Roles' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '权限的角色', 
   'user', @CurrentUser, 'table', 'Roles'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Roles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ActionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Roles', 'column', 'ActionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作名称',
   'user', @CurrentUser, 'table', 'Roles', 'column', 'ActionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Roles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Description')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Roles', 'column', 'Description'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '描述',
   'user', @CurrentUser, 'table', 'Roles', 'column', 'Description'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Roles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Roles', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Roles', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Roles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Roles', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Roles', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Roles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Roles', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Roles', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Roles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Roles', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Roles', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Roles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Roles', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Roles', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Roles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Roles', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Roles', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: RolesModules                                          */
/*==============================================================*/
create table RolesModules (
   Id                   char(32)             not null,
   ROLES_Id             char(32)             null,
   MODULES_Id           char(32)             null,
   constraint PK_ROLESMODULES primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('RolesModules') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'RolesModules' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '角色模块', 
   'user', @CurrentUser, 'table', 'RolesModules'
go

/*==============================================================*/
/* Index: Relationship_26_FK                                    */
/*==============================================================*/
create index Relationship_26_FK on RolesModules (
ROLES_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_27_FK                                    */
/*==============================================================*/
create index Relationship_27_FK on RolesModules (
MODULES_Id ASC
)
go

/*==============================================================*/
/* Table: SiteGroup                                             */
/*==============================================================*/
create table SiteGroup (
   Id                   char(32)             not null,
   GroupNO              varchar(50)          null,
   GroupName            varchar(200)         null,
   Factory              varchar(200)         null,
   Workshop             varchar(200)         null,
   MainTrackNumber      smallint             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_SITEGROUP primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('SiteGroup') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'SiteGroup' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '站点组:
   为客户企业管理中的行政划分单位，与吊挂生产线可以是一对一关系，也可以是一对多，多对多的关系，视实际情况安装', 
   'user', @CurrentUser, 'table', 'SiteGroup'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GroupNO')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'GroupNO'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组编号',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'GroupNO'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GroupName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'GroupName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组名称',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'GroupName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Factory')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'Factory'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工厂',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'Factory'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Workshop')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'Workshop'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车间',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'Workshop'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MainTrackNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'MainTrackNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '一个组对应一个主轨号(范围1--255)',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'MainTrackNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记：0正常，1删除',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SiteGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'SiteGroup', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: Stating                                               */
/*==============================================================*/
create table Stating (
   Id                   char(32)             not null,
   SUSLANGUAGE_Id       char(32)             null,
   STATINGROLES_Id      char(32)             null,
   SITEGROUP_Id         char(32)             null,
   STATINGDIRECTION_Id  char(32)             null,
   StatingName          char(20)             null,
   StatingNo            char(20)             null,
   Language             char(20)             null,
   MainTrackNumber      smallint             null,
   Capacity             int                  null,
   ColorValue           char(20)             null,
   IsLoadMonitor        bit                  null,
   IsChainHoist         bit                  null,
   IsPromoteTripCachingFull bit                  null,
   SiteBarCode          varchar(100)         null,
   IsEnabled            bit                  null,
   Direction            varchar(100)         null,
   Memo                 varchar(100)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_STATING primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Stating') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Stating' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '站点:
   即一个包括进站、出站功能，有终端显示操作面板的集合，一般安装一名生产员工及一台衣车', 
   'user', @CurrentUser, 'table', 'Stating'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'StatingName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点名称',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'StatingName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'StatingNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点编号',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'StatingNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Language')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Language'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点语言',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Language'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'MainTrackNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'MainTrackNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '一个组对应一个主轨号(范围1--255)',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'MainTrackNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Capacity')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Capacity'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '容量',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Capacity'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ColorValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'ColorValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'ColorValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsLoadMonitor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'IsLoadMonitor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '负载监(:0正常，1:不可用)',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'IsLoadMonitor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsChainHoist')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'IsChainHoist'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '链式提升',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'IsChainHoist'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsPromoteTripCachingFull')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'IsPromoteTripCachingFull'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '提升行程缓存满',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'IsPromoteTripCachingFull'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteBarCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'SiteBarCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点条码',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'SiteBarCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsEnabled')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'IsEnabled'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否可用',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'IsEnabled'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Direction')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Direction'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '方向',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Direction'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记:0正常，1:删除',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Stating')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Stating', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Stating', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_9_FK                                     */
/*==============================================================*/
create index Relationship_9_FK on Stating (
SITEGROUP_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_49_FK                                    */
/*==============================================================*/
create index Relationship_49_FK on Stating (
STATINGROLES_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_71_FK                                    */
/*==============================================================*/
create index Relationship_71_FK on Stating (
SUSLANGUAGE_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_72_FK                                    */
/*==============================================================*/
create index Relationship_72_FK on Stating (
STATINGDIRECTION_Id ASC
)
go

/*==============================================================*/
/* Table: StatingDirection                                      */
/*==============================================================*/
create table StatingDirection (
   Id                   char(32)             not null,
   DirectionKey         varchar(20)          null,
   DirectionDesc        varchar(20)          null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_STATINGDIRECTION primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StatingDirection') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StatingDirection' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '站点方向', 
   'user', @CurrentUser, 'table', 'StatingDirection'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingDirection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DirectionKey')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'DirectionKey'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '方向Key',
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'DirectionKey'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingDirection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DirectionDesc')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'DirectionDesc'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '方向描述',
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'DirectionDesc'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingDirection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingDirection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingDirection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingDirection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingDirection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingDirection')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'StatingDirection', 'column', 'CompanyId'
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
   IsSucessedFlow       bit                  null,
   SiteId               char(32)             null,
   IsFlowChatChange     bit                  null,
   IsIncomeSite         bit                  null,
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
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSucessedFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'IsSucessedFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序是否完成',
   'user', @CurrentUser, 'table', 'StatingHangerProductItem', 'column', 'IsSucessedFlow'
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

/*==============================================================*/
/* Table: StatingRoles                                          */
/*==============================================================*/
create table StatingRoles (
   Id                   char(32)             not null,
   RoleCode             varchar(20)          null,
   RoleName             varchar(50)          null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_STATINGROLES primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StatingRoles') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StatingRoles' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '站点角色', 
   'user', @CurrentUser, 'table', 'StatingRoles'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingRoles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingRoles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingRoles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingRoles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingRoles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StatingRoles')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'StatingRoles', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: Style                                                 */
/*==============================================================*/
create table Style (
   Id                   char(32)             not null,
   StyleNo              char(20)             null,
   StyleName            char(200)            null,
   Rmark                char(100)            null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_STYLE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Style') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Style' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '款式工艺', 
   'user', @CurrentUser, 'table', 'Style'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StyleNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'StyleNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '款号',
   'user', @CurrentUser, 'table', 'Style', 'column', 'StyleNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StyleName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'StyleName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '款式名称',
   'user', @CurrentUser, 'table', 'Style', 'column', 'StyleName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Rmark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'Rmark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'Style', 'column', 'Rmark'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Style', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Style', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Style', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Style', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记：
   1：已删除；0正常',
   'user', @CurrentUser, 'table', 'Style', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Style')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Style', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Style', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: StyleProcessFlowItem                                  */
/*==============================================================*/
create table StyleProcessFlowItem (
   Id                   char(32)             not null,
   BASICPROCESSFLOW_Id  char(32)             null,
   STYLE_Id             char(32)             null,
   ProVersionNo         char(50)             null,
   ProcessVersionName   char(200)            null,
   FlowNo               int                  null,
   ProcessCode          char(30)             null,
   ProcessStatus        tinyint              null,
   SAM                  char(20)             null,
   StanardHours         char(20)             null,
   StandardPrice        decimal(8,3)         null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_STYLEPROCESSFLOWITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StyleProcessFlowItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '款式工序明细', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProVersionNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'ProVersionNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '版本编号',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'ProVersionNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessVersionName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'ProcessVersionName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '版本名称',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'ProcessVersionName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'FlowNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序号',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'FlowNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'ProcessCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'ProcessCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'ProcessStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序状态
   0:待走流程
   1:流程已经完成
   ',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'ProcessStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SAM')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'SAM'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SAM',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'SAM'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StanardHours')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'StanardHours'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标准工时',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'StanardHours'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StandardPrice')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'StandardPrice'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标准工价',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'StandardPrice'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标记',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'StyleProcessFlowItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_3_FK                                     */
/*==============================================================*/
create index Relationship_3_FK on StyleProcessFlowItem (
STYLE_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_41_FK                                    */
/*==============================================================*/
create index Relationship_41_FK on StyleProcessFlowItem (
BASICPROCESSFLOW_Id ASC
)
go

/*==============================================================*/
/* Table: StyleProcessFlowSectionItem                           */
/*==============================================================*/
create table StyleProcessFlowSectionItem (
   Id                   char(32)             not null,
   BASICPROCESSFLOW_Id  char(32)             null,
   STYLE_Id             char(32)             null,
   PROCESSFLOWSECTION_Id char(32)             null,
   Memo                 varchar(100)         null,
   FlowNo               int                  null,
   ProcessCode          char(30)             null,
   ProcessStatus        tinyint              null,
   ProcessName          char(30)             null,
   SortNo               char(20)             null,
   SAM                  char(20)             null,
   StanardHours         char(20)             null,
   StandardPrice        decimal(8,3)         null,
   PrcocessRmark        char(100)            null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_STYLEPROCESSFLOWSECTIONITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('StyleProcessFlowSectionItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工序段工序明细', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'FlowNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序号',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'FlowNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'ProcessCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'ProcessCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessStatus')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'ProcessStatus'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序状态
   0:待走流程
   1:流程已经完成
   ',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'ProcessStatus'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'ProcessName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'ProcessName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SortNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'SortNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序号',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'SortNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SAM')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'SAM'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SAM',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'SAM'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StanardHours')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'StanardHours'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标准工时',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'StanardHours'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StandardPrice')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'StandardPrice'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标准工价',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'StandardPrice'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrcocessRmark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'PrcocessRmark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺说明',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'PrcocessRmark'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('StyleProcessFlowSectionItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'StyleProcessFlowSectionItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_43_FK                                    */
/*==============================================================*/
create index Relationship_43_FK on StyleProcessFlowSectionItem (
PROCESSFLOWSECTION_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_42_FK                                    */
/*==============================================================*/
create index Relationship_42_FK on StyleProcessFlowSectionItem (
BASICPROCESSFLOW_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_44_FK                                    */
/*==============================================================*/
create index Relationship_44_FK on StyleProcessFlowSectionItem (
STYLE_Id ASC
)
go

/*==============================================================*/
/* Table: SuccessStatingHangerProductItem                       */
/*==============================================================*/
create table SuccessStatingHangerProductItem (
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
   IsSucessedFlow       bit                  null,
   SiteId               char(32)             null,
   IsFlowChatChange     bit                  null,
   IsIncomeSite         bit                  null,
   Memo                 varchar(100)         null,
   ClientMancheId       char(32)             null,
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
   constraint PK_SUCCESSSTATINGHANGERPRODUCT primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('SuccessStatingHangerProductItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '完成的站点衣架生产明细', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'SiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站号',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'SiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单Id',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowChartd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'FlowChartd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺路线图Id',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'FlowChartd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'SizeNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'SizeNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序Id',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'ProcessFlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序索引',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSucessedFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'IsSucessedFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序是否完成',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'IsSucessedFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'SiteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站Id',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'SiteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowChatChange')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'IsFlowChatChange'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线图是否改变',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'IsFlowChatChange'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsIncomeSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'IsIncomeSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否进站',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'IsIncomeSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SusLineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'SusLineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '吊挂线Id',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'SusLineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IncomeSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'IncomeSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '进站时间',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'IncomeSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OutSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'OutSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出战时间',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'OutSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompareDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'CompareDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '比较时间',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'CompareDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SuccessStatingHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'SuccessStatingHangerProductItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: SucessHangerProductItem                               */
/*==============================================================*/
create table SucessHangerProductItem (
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
   IsFlowChatChange     bit                  null,
   IsIncomeSite         bit                  null,
   IsSucessedFlow       bit                  null,
   IsReturnWorkFlow     bit                  null,
   returnWorkSiteNo     char(10)             null,
   Memo                 varchar(100)         null,
   ClientMancheId       char(32)             null,
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
   constraint PK_SUCESSHANGERPRODUCTITEM primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('SucessHangerProductItem') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'SucessHangerProductItem' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '完成的衣架生产明细', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单Id',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowChartd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'FlowChartd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺路线图Id',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'FlowChartd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'SizeNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'SizeNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序Id',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'ProcessFlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序索引',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'SiteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站Id',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'SiteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'SiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站号',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'SiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowChatChange')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IsFlowChatChange'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线图是否改变',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IsFlowChatChange'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsIncomeSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IsIncomeSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否进站',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IsIncomeSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSucessedFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IsSucessedFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序是否完成',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IsSucessedFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsReturnWorkFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IsReturnWorkFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否是返工工序',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IsReturnWorkFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'returnWorkSiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'returnWorkSiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '返工来源站号',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'returnWorkSiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SusLineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'SusLineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '吊挂线Id',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'SusLineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IncomeSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IncomeSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '进站时间',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'IncomeSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompareDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'CompareDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '比较时间',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'CompareDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OutSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'OutSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出战时间',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'OutSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerProductItem')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'SucessHangerProductItem', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: SucessHangerReworkRecord                              */
/*==============================================================*/
create table SucessHangerReworkRecord (
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
   IsFlowChatChange     bit                  null,
   IsIncomeSite         bit                  null,
   IsSucessedFlow       bit                  null,
   returnWorkSiteNo     char(10)             null,
   Memo                 varchar(100)         null,
   ClientMancheId       char(32)             null,
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
   DefectCode           varchar(50)          null,
   constraint PK_SUCESSHANGERREWORKRECORD primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('SucessHangerReworkRecord') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '完成的衣架返工记录', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单Id',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowChartd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'FlowChartd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺路线图Id',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'FlowChartd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'SizeNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'SizeNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序Id',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'ProcessFlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序索引',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'SiteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站Id',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'SiteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'SiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站号',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'SiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowChatChange')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'IsFlowChatChange'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线图是否改变',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'IsFlowChatChange'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsIncomeSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'IsIncomeSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否进站',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'IsIncomeSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsSucessedFlow')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'IsSucessedFlow'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序是否完成',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'IsSucessedFlow'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'returnWorkSiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'returnWorkSiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '返工来源站号',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'returnWorkSiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SusLineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'SusLineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '吊挂线Id',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'SusLineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IncomeSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'IncomeSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '进站时间',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'IncomeSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompareDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'CompareDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '比较时间',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'CompareDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OutSiteDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'OutSiteDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出战时间',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'OutSiteDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'CompanyId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessHangerReworkRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DefectCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'DefectCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '返工疵点代码',
   'user', @CurrentUser, 'table', 'SucessHangerReworkRecord', 'column', 'DefectCode'
go

/*==============================================================*/
/* Table: SucessProcessOrderHanger                              */
/*==============================================================*/
create table SucessProcessOrderHanger (
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
   IsFlowChatChange     bit                  null,
   Memo                 varchar(200)         null,
   ClientMachineId      char(32)             null,
   SusLineId            char(32)             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   IsIncomeSite         bit                  null,
   constraint PK_SUCESSPROCESSORDERHANGER primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('SucessProcessOrderHanger') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '生产完成制单衣架', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单Id',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowChartd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'FlowChartd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺路线图Id',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'FlowChartd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'SizeNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'SizeNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序Id',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ProcessFlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序索引',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'SiteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站Id',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'SiteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'SiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站号',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'SiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowChatChange')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'IsFlowChatChange'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线图是否改变',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'IsFlowChatChange'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientMachineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ClientMachineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户机Id',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'ClientMachineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SusLineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'SusLineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '吊挂线Id',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'SusLineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'CompanyId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsIncomeSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'IsIncomeSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否进站',
   'user', @CurrentUser, 'table', 'SucessProcessOrderHanger', 'column', 'IsIncomeSite'
go

/*==============================================================*/
/* Table: SucessProducts                                        */
/*==============================================================*/
create table SucessProducts (
   Id                   char(32)             not null,
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
   constraint PK_SUCESSPRODUCTS primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('SucessProducts') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'SucessProducts' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '生产完成制品', 
   'user', @CurrentUser, 'table', 'SucessProducts'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProductionNumber')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'ProductionNumber'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排产号',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'ProductionNumber'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ImplementDate')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'ImplementDate'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上线日期',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'ImplementDate'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangingPieceSiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'HangingPieceSiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '挂片站点',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'HangingPieceSiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Status')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'Status'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态:0:未分配;1:已分配;3.上线;4.已完成',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'Status'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CustomerPurchaseOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'CustomerPurchaseOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户外贸单Id',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'CustomerPurchaseOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'OrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单号',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'OrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StyleNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'StyleNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '款号',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'StyleNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PO')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'PO'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'PO号',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'PO'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowSection')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'FlowSection'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工段',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'FlowSection'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Unit')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'Unit'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'Unit'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TaskNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TaskNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务数量',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TaskNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OnlineNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'OnlineNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '在线数',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'OnlineNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodayHangingPieceSiteNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TodayHangingPieceSiteNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '今日挂片',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TodayHangingPieceSiteNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodayProdOutNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TodayProdOutNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '今日产出',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TodayProdOutNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodayBindCard')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TodayBindCard'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '今日绑卡',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TodayBindCard'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TodayRework')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TodayRework'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '今日返工',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TodayRework'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalHangingPieceSiteNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TotalHangingPieceSiteNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '累计挂片',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TotalHangingPieceSiteNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalRework')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TotalRework'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '累计返工',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TotalRework'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TotalBindNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TotalBindNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '累计绑卡',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'TotalBindNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SucessProducts')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'SucessProducts', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: SusLanguage                                           */
/*==============================================================*/
create table SusLanguage (
   Id                   char(32)             not null,
   LanguageKey          varchar(20)          null,
   LanguageValue        varchar(30)          null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_SUSLANGUAGE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('SusLanguage') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'SusLanguage' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '语言', 
   'user', @CurrentUser, 'table', 'SusLanguage'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SusLanguage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LanguageKey')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'LanguageKey'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '语言简称',
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'LanguageKey'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SusLanguage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LanguageValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'LanguageValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '语言',
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'LanguageValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SusLanguage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SusLanguage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SusLanguage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SusLanguage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SusLanguage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('SusLanguage')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'SusLanguage', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: TestSiteTable                                         */
/*==============================================================*/
create table TestSiteTable (
   Id                   char(32)             not null,
   StatingNo            char(20)             null,
   HangerNo             varchar(200)         null,
   ProcessOrderNo       varchar(200)         null,
   PColor               varchar(200)         null,
   PSize                varchar(200)         null,
   ProcessFlowCode      varchar(200)         null,
   ProcessFlowName      varchar(200)         null,
   constraint PK_TESTSITETABLE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('TestSiteTable') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'TestSiteTable' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '测试站点', 
   'user', @CurrentUser, 'table', 'TestSiteTable'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TestSiteTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'StatingNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'StatingNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站点编号',
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'StatingNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TestSiteTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TestSiteTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TestSiteTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TestSiteTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TestSiteTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('TestSiteTable')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'TestSiteTable', 'column', 'ProcessFlowName'
go

/*==============================================================*/
/* Table: UserClientMachines                                    */
/*==============================================================*/
create table UserClientMachines (
   Id                   char(32)             not null,
   UserId               char(32)             not null,
   ClientMancheId       char(32)             not null,
   constraint PK_USERCLIENTMACHINES primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: UserLoginInfo                                         */
/*==============================================================*/
create table UserLoginInfo (
   SessionId            char(32)             not null,
   UserId               char(32)             not null,
   UserName             varchar(20)          not null,
   EmployeeId           char(32)             not null,
   EmployeeName         varchar(20)          not null,
   LoginDate            datetime             not null,
   LoginOutDate         datetime             null,
   constraint PK_USERLOGININFO primary key nonclustered (SessionId)
)
go

/*==============================================================*/
/* Table: UserOperateLogDetail                                  */
/*==============================================================*/
create table UserOperateLogDetail (
   Id                   char(32)             not null,
   USEROPERATELOGS_Id   char(32)             not null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   constraint PK_USEROPERATELOGDETAIL primary key nonclustered (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogDetail', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'UserOperateLogDetail', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogDetail', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'UserOperateLogDetail', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogDetail', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'UserOperateLogDetail', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogDetail', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'UserOperateLogDetail', 'column', 'UpdateUser'
go

/*==============================================================*/
/* Index: Relationship_75_FK                                    */
/*==============================================================*/
create index Relationship_75_FK on UserOperateLogDetail (
USEROPERATELOGS_Id ASC
)
go

/*==============================================================*/
/* Table: UserOperateLogs                                       */
/*==============================================================*/
create table UserOperateLogs (
   Id                   char(32)             not null,
   OpFormName           varchar(50)          null,
   OpFormCode           varchar(50)          null,
   OpType               int                  not null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   constraint PK_USEROPERATELOGS primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('UserOperateLogs') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'UserOperateLogs' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '用户操作日志', 
   'user', @CurrentUser, 'table', 'UserOperateLogs'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogs')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'OpType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'OpType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1: insert
   2: delete
   3: select
   4: update',
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'OpType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogs')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogs')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogs')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserOperateLogs')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'UserOperateLogs', 'column', 'UpdateUser'
go

/*==============================================================*/
/* Table: UserRoles                                             */
/*==============================================================*/
create table UserRoles (
   Id                   char(32)             not null,
   ROLES_Id             char(32)             null,
   USERS_Id             char(32)             null,
   constraint PK_USERROLES primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('UserRoles') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'UserRoles' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '用户角色表', 
   'user', @CurrentUser, 'table', 'UserRoles'
go

/*==============================================================*/
/* Index: Relationship_70_FK                                    */
/*==============================================================*/
create index Relationship_70_FK on UserRoles (
ROLES_Id ASC
)
go

/*==============================================================*/
/* Index: Relationship_73_FK                                    */
/*==============================================================*/
create index Relationship_73_FK on UserRoles (
USERS_Id ASC
)
go

/*==============================================================*/
/* Table: UserSiteGroup                                         */
/*==============================================================*/
create table UserSiteGroup (
   Id                   char(32)             not null,
   UserId               char(32)             null,
   SiteGroupNo          varchar(30)          null,
   constraint PK_USERSITEGROUP primary key nonclustered (Id)
)
go

/*==============================================================*/
/* Table: UserWorkshops                                         */
/*==============================================================*/
create table UserWorkshops (
   ID                   char(32)             not null,
   WorkshopCode         varchar(200)         not null,
   UserId               char(32)             not null,
   constraint PK_USERWORKSHOPS primary key nonclustered (ID)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserWorkshops')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserWorkshops', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '@generateColumnConfig=sequence',
   'user', @CurrentUser, 'table', 'UserWorkshops', 'column', 'ID'
go

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
create table Users (
   Id                   char(32)             not null,
   EMPLOYEE_Id          char(32)             null,
   UserName             varchar(20)          not null,
   Password             char(36)             not null,
   CardNO               char(10)             null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_USERS primary key nonclustered (Id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Users')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Users', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'Users', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Users')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Users', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'Users', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Users')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Users', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'Users', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Users')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Users', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'Users', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Users')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Users', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'Users', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Users')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Users', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'Users', 'column', 'CompanyId'
go

/*==============================================================*/
/* Index: Relationship_76_FK                                    */
/*==============================================================*/
create index Relationship_76_FK on Users (
EMPLOYEE_Id ASC
)
go

/*==============================================================*/
/* Table: WaitProcessOrderHanger                                */
/*==============================================================*/
create table WaitProcessOrderHanger (
   Id                   char(32)             not null,
   HangerNo             varchar(200)         null,
   ProcessOrderId       char(32)             null,
   ProcessOrderNo       varchar(200)         null,
   PColor               varchar(200)         null,
   PSize                varchar(200)         null,
   FlowChartd           char(32)             null,
   LineName             varchar(200)         null,
   FlowIndex            smallint             null,
   SizeNum              int                  null,
   ProcessFlowId        char(32)             null,
   ProcessFlowCode      varchar(200)         null,
   ProcessFlowName      varchar(200)         null,
   SiteId               char(32)             null,
   SiteNo               varchar(50)          null,
   IsFlowChatChange     bit                  null,
   IsIncomeSite         bit                  null,
   Memo                 varchar(200)         null,
   ClientMachineId      char(32)             null,
   SusLineId            char(32)             null,
   InsertDateTime       datetime             null,
   UpdateDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_WAITPROCESSORDERHANGER primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('WaitProcessOrderHanger') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '待生产制单衣架', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HangerNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'HangerNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '衣架号',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'HangerNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessOrderId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单Id',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessOrderId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessOrderNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessOrderNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单号',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessOrderNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PColor')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'PColor'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '颜色',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'PColor'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PSize')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'PSize'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '尺码',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'PSize'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowChartd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'FlowChartd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺路线图Id',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'FlowChartd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LineName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'LineName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工艺图名称',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'LineName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlowIndex')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'FlowIndex'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序索引',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'FlowIndex'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SizeNum')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'SizeNum'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '数量',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'SizeNum'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessFlowId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '制单工序Id',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessFlowId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowCode')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessFlowCode'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序代码',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessFlowCode'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ProcessFlowName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessFlowName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '工序名称',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ProcessFlowName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'SiteId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站Id',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'SiteId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SiteNo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'SiteNo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '站号',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'SiteNo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsFlowChatChange')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'IsFlowChatChange'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路线图是否改变',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'IsFlowChatChange'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsIncomeSite')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'IsIncomeSite'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否进站',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'IsIncomeSite'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Memo')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'Memo'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'Memo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ClientMachineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ClientMachineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户机Id',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'ClientMachineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SusLineId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'SusLineId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '吊挂线Id',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'SusLineId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WaitProcessOrderHanger')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'WaitProcessOrderHanger', 'column', 'CompanyId'
go

/*==============================================================*/
/* Table: WorkType                                              */
/*==============================================================*/
create table WorkType (
   Id                   char(32)             not null,
   WTypeCode            varchar(100)         null,
   WTypeName            varchar(200)         null,
   InsertDateTime       datetime             null,
   InsertUser           char(32)             null,
   UpdateDateTime       datetime             null,
   UpdateUser           char(32)             null,
   Deleted              tinyint              null,
   CompanyId            char(32)             null,
   constraint PK_WORKTYPE primary key nonclustered (Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('WorkType') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'WorkType' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '工种信息', 
   'user', @CurrentUser, 'table', 'WorkType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WorkType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Id')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'Id'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识Id',
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'Id'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WorkType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'InsertDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入时间',
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'InsertDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WorkType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'InsertUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'InsertUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '插入用户',
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'InsertUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WorkType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateDateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'UpdateDateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新时间',
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'UpdateDateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WorkType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpdateUser')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'UpdateUser'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新用户',
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'UpdateUser'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WorkType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Deleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'Deleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '删除标识',
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'Deleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('WorkType')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CompanyId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'CompanyId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司Id',
   'user', @CurrentUser, 'table', 'WorkType', 'column', 'CompanyId'
go

alter table Area
   add constraint FK_AREA_RELATIONS_CITY foreign key (CITY_Id)
      references City (Id)
go

alter table City
   add constraint FK_CITY_RELATIONS_PROVINCE foreign key (PROVINCE_Id)
      references Province (Id)
go

alter table CustomerPurchaseOrder
   add constraint FK_CUSTOMER_RELATIONS_CUSTOMER foreign key (CUSTOMER_Id)
      references Customer (Id)
go

alter table CustomerPurchaseOrder
   add constraint FK_CUSTOMER_RELATIONS_STYLE foreign key (STYLE_Id)
      references Style (Id)
go

alter table CustomerPurchaseOrderColorItem
   add constraint FK_CUSTOMER_RELATIONS_CUSTOMER foreign key (CUSTOMERPURCHASEORDER_Id)
      references CustomerPurchaseOrder (Id)
go

alter table CustomerPurchaseOrderColorItem
   add constraint FK_CUSTOMER_RELATIONS_POCOLOR foreign key (POCOLOR_Id)
      references PoColor (Id)
go

alter table CustomerPurchaseOrderColorSizeItem
   add constraint FK_CUSTOMER_RELATIONS_CUSTOMER foreign key (CUSTOMERPURCHASEORDERCOLORITEM_Id)
      references CustomerPurchaseOrderColorItem (Id)
go

alter table CustomerPurchaseOrderColorSizeItem
   add constraint FK_CUSTOMER_RELATIONS_PSIZE foreign key (PSIZE_Id)
      references PSize (Id)
go

alter table Employee
   add constraint FK_EMPLOYEE_RELATIONS_ORGANIZA foreign key (ORGANIZATIONS_Id)
      references Organizations (Id)
go

alter table Employee
   add constraint FK_EMPLOYEE_RELATIONS_DEPARTME foreign key (DEPARTMENT_Id)
      references Department (Id)
go

alter table Employee
   add constraint FK_EMPLOYEE_RELATIONS_WORKTYPE foreign key (WORKTYPE_Id)
      references WorkType (Id)
go

alter table Employee
   add constraint FK_EMPLOYEE_RELATIONS_AREA foreign key (AREA_Id)
      references Area (Id)
go

alter table Employee
   add constraint FK_EMPLOYEE_RELATIONS_SITEGROU foreign key (SITEGROUP_Id)
      references SiteGroup (Id)
go

alter table EmployeePositions
   add constraint FK_EMPLOYEE_RELATIONS_EMPLOYEE foreign key (EMPLOYEE_Id)
      references Employee (Id)
go

alter table EmployeePositions
   add constraint FK_EMPLOYEE_RELATIONS_POSITION foreign key (POSITION_Id)
      references Position (Id)
go

alter table FlowStatingColor
   add constraint FK_FLOWSTAT_RELATIONS_PROCESSF foreign key (PROCESSFLOWSTATINGITEM_Id)
      references ProcessFlowStatingItem (Id)
go

alter table FlowStatingResume
   add constraint FK_FLOWSTAT_RELATIONS_STATING foreign key (STATING_Id)
      references Stating (Id)
go

alter table FlowStatingResume
   add constraint FK_FLOWSTAT_RELATIONS_PROCESSF foreign key (PROCESSFLOWCHARTFLOWRELATION_Id)
      references ProcessFlowChartFlowRelation (Id)
go

alter table FlowStatingSize
   add constraint FK_FLOWSTAT_RELATIONS_PROCESSF foreign key (PROCESSFLOWSTATINGITEM_Id)
      references ProcessFlowStatingItem (Id)
go

alter table Modules
   add constraint FK_MODULES_RELATIONS_MODULES foreign key (MODULES_Id)
      references Modules (Id)
go

alter table OrderProductItem
   add constraint FK_ORDERPRO_RELATIONS_PRODUCTO foreign key (PRODUCTORDER_Id)
      references ProductOrder (Id)
go

alter table Organizations
   add constraint FK_ORGANIZA_RELATIONS_ORGANIZA foreign key (ORGANIZATIONS_Id)
      references Organizations (Id)
go

alter table Pipelining
   add constraint FK_PIPELINI_RELATIONS_PRODTYPE foreign key (PRODTYPE_Id)
      references ProdType (Id)
go

alter table Pipelining
   add constraint FK_PIPELINI_RELATIONS_SITEGROU foreign key (SITEGROUP_Id)
      references SiteGroup (Id)
go

alter table ProcessFlow
   add constraint FK_PROCESSF_RELATIONS_PROCESSF foreign key (PROCESSFLOWVERSION_Id)
      references ProcessFlowVersion (Id)
go

alter table ProcessFlow
   add constraint FK_PROCESSF_RELATIONS_BASICPRO foreign key (BASICPROCESSFLOW_Id)
      references BasicProcessFlow (Id)
go

alter table ProcessFlowChart
   add constraint FK_PROCESSF_RELATIONS_PROCESSF foreign key (PROCESSFLOWVERSION_Id)
      references ProcessFlowVersion (Id)
go

alter table ProcessFlowChartFlowRelation
   add constraint FK_PROCESSF_RELATIONS_PROCESSF foreign key (PROCESSFLOWCHART_Id)
      references ProcessFlowChart (Id)
go

alter table ProcessFlowChartFlowRelation
   add constraint FK_PROCESSF_RELATIONS_PROCESSF foreign key (PROCESSFLOW_Id)
      references ProcessFlow (Id)
go

alter table ProcessFlowChartGrop
   add constraint FK_PROCESSF_RELATIONS_PROCESSF foreign key (PROCESSFLOWCHART_Id)
      references ProcessFlowChart (Id)
go

alter table ProcessFlowChartGrop
   add constraint FK_PROCESSF_RELATIONS_SITEGROU foreign key (SITEGROUP_Id)
      references SiteGroup (Id)
go

alter table ProcessFlowStatingItem
   add constraint FK_PROCESSF_RELATIONS_PROCESSF foreign key (PROCESSFLOWCHARTFLOWRELATION_Id)
      references ProcessFlowChartFlowRelation (Id)
go

alter table ProcessFlowStatingItem
   add constraint FK_PROCESSF_RELATIONS_STATING foreign key (STATING_Id)
      references Stating (Id)
go

alter table ProcessFlowVersion
   add constraint FK_PROCESSF_RELATIONS_PROCESSO foreign key (PROCESSORDER_Id)
      references ProcessOrder (Id)
go

alter table ProcessOrder
   add constraint FK_PROCESSO_RELATIONS_STYLE foreign key (STYLE_Id)
      references Style (Id)
go

alter table ProcessOrder
   add constraint FK_PROCESSO_RELATIONS_CUSTOMER foreign key (CUSTOMER_Id)
      references Customer (Id)
go

alter table ProcessOrderColorItem
   add constraint FK_PROCESSO_RELATIONS_POCOLOR foreign key (POCOLOR_Id)
      references PoColor (Id)
go

alter table ProcessOrderColorItem
   add constraint FK_PROCESSO_RELATIONS_PROCESSO foreign key (PROCESSORDER_Id)
      references ProcessOrder (Id)
go

alter table ProcessOrderColorItem
   add constraint FK_PROCESSO_RELATIONS_CUSTOMER foreign key (CUSTOMERPURCHASEORDER_Id)
      references CustomerPurchaseOrder (Id)
go

alter table ProcessOrderColorSizeItem
   add constraint FK_PROCESSO_RELATIONS_PROCESSO foreign key (PROCESSORDERCOLORITEM_Id)
      references ProcessOrderColorItem (Id)
go

alter table ProcessOrderColorSizeItem
   add constraint FK_PROCESSO_RELATIONS_PSIZE foreign key (PSIZE_Id)
      references PSize (Id)
go

alter table Products
   add constraint FK_PRODUCTS_RELATIONS_PROCESSO foreign key (PROCESSORDER_Id)
      references ProcessOrder (Id)
go

alter table Products
   add constraint FK_PRODUCTS_RELATIONS_PROCESSF foreign key (PROCESSFLOWCHART_Id)
      references ProcessFlowChart (Id)
go

alter table ProductsHangPieceResume
   add constraint FK_PRODUCTS_RELATIONS_PRODUCTS foreign key (PRODUCTS_Id)
      references Products (Id)
go

alter table RolesModules
   add constraint FK_ROLESMOD_RELATIONS_ROLES foreign key (ROLES_Id)
      references Roles (Id)
go

alter table RolesModules
   add constraint FK_ROLESMOD_RELATIONS_MODULES foreign key (MODULES_Id)
      references Modules (Id)
go

alter table Stating
   add constraint FK_STATING_RELATIONS_STATINGR foreign key (STATINGROLES_Id)
      references StatingRoles (Id)
go

alter table Stating
   add constraint FK_STATING_RELATIONS_SUSLANGU foreign key (SUSLANGUAGE_Id)
      references SusLanguage (Id)
go

alter table Stating
   add constraint FK_STATING_RELATIONS_STATINGD foreign key (STATINGDIRECTION_Id)
      references StatingDirection (Id)
go

alter table Stating
   add constraint FK_STATING_RELATIONS_SITEGROU foreign key (SITEGROUP_Id)
      references SiteGroup (Id)
go

alter table StyleProcessFlowItem
   add constraint FK_STYLEPRO_RELATIONS_STYLE foreign key (STYLE_Id)
      references Style (Id)
go

alter table StyleProcessFlowItem
   add constraint FK_STYLEPRO_RELATIONS_BASICPRO foreign key (BASICPROCESSFLOW_Id)
      references BasicProcessFlow (Id)
go

alter table StyleProcessFlowSectionItem
   add constraint FK_STYLEPRO_RELATIONS_BASICPRO foreign key (BASICPROCESSFLOW_Id)
      references BasicProcessFlow (Id)
go

alter table StyleProcessFlowSectionItem
   add constraint FK_STYLEPRO_RELATIONS_PROCESSF foreign key (PROCESSFLOWSECTION_Id)
      references ProcessFlowSection (Id)
go

alter table StyleProcessFlowSectionItem
   add constraint FK_STYLEPRO_RELATIONS_STYLE foreign key (STYLE_Id)
      references Style (Id)
go

alter table UserOperateLogDetail
   add constraint FK_USEROPER_RELATIONS_USEROPER foreign key (USEROPERATELOGS_Id)
      references UserOperateLogs (Id)
go

alter table UserRoles
   add constraint FK_USERROLE_RELATIONS_ROLES foreign key (ROLES_Id)
      references Roles (Id)
go

alter table UserRoles
   add constraint FK_USERROLE_RELATIONS_USERS foreign key (USERS_Id)
      references Users (Id)
go

alter table Users
   add constraint FK_USERS_RELATIONS_EMPLOYEE foreign key (EMPLOYEE_Id)
      references Employee (Id)
go
