if exists (select 1
            from  sysobjects
           where  id = object_id('SystemLogs')
            and   type = 'U')
   drop table SystemLogs
go

/*==============================================================*/
/* Table: SystemLogs                                            */
/*==============================================================*/
create table SystemLogs (
   Id                   char(32)             not null,
   Name                 varchar(50)          null,
   LogInfo              text                 null,
   CreatedDate          datetime             null,
   constraint PK_SYSTEMLOGS primary key nonclustered (Id)
)
go
