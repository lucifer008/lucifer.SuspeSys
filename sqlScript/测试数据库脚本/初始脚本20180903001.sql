USE [SyspeSysV2.0]
GO
/****** Object:  Table [dbo].[ApplicationProfile]    Script Date: 2018/9/3 21:01:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationProfile](
	[Id] [char](32) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[ParaValue] [varchar](1000) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Memo] [varchar](1000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[Id] [char](32) NOT NULL,
	[CITY_Id] [char](32) NULL,
	[AreaName] [varchar](100) NULL,
	[AreaCode] [char](10) NULL,
	[Addess] [varchar](500) NULL,
	[Memo] [varchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicProcessFlow]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicProcessFlow](
	[Id] [char](32) NOT NULL,
	[ProcessCode] [char](30) NULL,
	[ProcessStatus] [tinyint] NULL,
	[ProcessName] [char](30) NULL,
	[SortNo] [char](20) NULL,
	[StanardSecond] [int] NULL,
	[SAM] [char](20) NULL,
	[StanardHours] [char](20) NULL,
	[StandardPrice] [decimal](8, 4) NULL,
	[PrcocessRmark] [char](100) NULL,
	[DefaultFlowNo] [int] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BridgeSet]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BridgeSet](
	[Id] [char](32) NOT NULL,
	[BIndex] [smallint] NULL,
	[AMainTrackNumber] [smallint] NULL,
	[ASiteNo] [smallint] NULL,
	[Direction] [smallint] NULL,
	[DirectionTxt] [varchar](20) NULL,
	[BMainTrackNumber] [smallint] NULL,
	[BSiteNo] [smallint] NULL,
	[Enabled] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardInfo]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardInfo](
	[Id] [char](32) NOT NULL,
	[CardNo] [varchar](20) NULL,
	[CardType] [smallint] NULL,
	[CardDescription] [varchar](50) NULL,
	[IsEnabled] [bit] NULL,
	[IsMultiLogin] [bit] NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardLoginInfo]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardLoginInfo](
	[Id] [char](32) NOT NULL,
	[CARDINFO_Id] [char](32) NULL,
	[MainTrackNumber] [int] NULL,
	[LoginStatingNo] [varchar](20) NULL,
	[LoginDate] [datetime] NULL,
	[LogOutDate] [datetime] NULL,
	[IsOnline] [bit] NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangeCardResume]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangeCardResume](
	[Id] [char](32) NOT NULL,
	[CardNo] [varchar](20) NULL,
	[ChangeCardReason] [varchar](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [char](32) NOT NULL,
	[PROVINCE_Id] [char](32) NULL,
	[CityCode] [varchar](30) NULL,
	[CityName] [varchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassesEmployee]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassesEmployee](
	[Id] [char](32) NOT NULL,
	[EMPLOYEE_Id] [char](32) NULL,
	[CLASSESINFO_Id] [char](32) NULL,
	[AttendanceDate] [datetime] NULL,
	[EffectDate] [datetime] NULL,
	[Time1GoToWorkDate] [datetime] NULL,
	[Time1GoOffWorkDate] [datetime] NULL,
	[Time2GoToWorkDate] [datetime] NULL,
	[Time2GoOffWorkDate] [datetime] NULL,
	[Time3GoToWorkDate] [datetime] NULL,
	[Time3GoOffWorkDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassesInfo]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassesInfo](
	[Id] [char](32) NOT NULL,
	[Num] [varchar](50) NULL,
	[CType] [tinyint] NULL,
	[Time1GoToWorkDate] [datetime] NULL,
	[Time1GoOffWorkDate] [datetime] NULL,
	[Time2GoToWorkDate] [datetime] NULL,
	[Time2GoOffWorkDate] [datetime] NULL,
	[Time3GoToWorkDate] [datetime] NULL,
	[Time3GoOffWorkDate] [datetime] NULL,
	[IsEnabled] [bit] NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientMachines]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientMachines](
	[Id] [char](32) NOT NULL,
	[ClientMachineName] [varchar](20) NOT NULL,
	[AuthorizationInformation] [text] NOT NULL,
	[Description] [varchar](200) NULL,
	[ClientMachineType] [smallint] NOT NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [char](32) NOT NULL,
	[CompanyCode] [varchar](100) NULL,
	[CompanyName] [varchar](200) NULL,
	[Address] [varchar](500) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [char](32) NOT NULL,
	[CusNo] [varchar](100) NULL,
	[CusName] [varchar](100) NULL,
	[PurchaseOrderNo] [varchar](200) NULL,
	[Address] [varchar](100) NULL,
	[LinkMan] [varchar](100) NULL,
	[Tel] [varchar](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPurchaseOrder]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPurchaseOrder](
	[Id] [char](32) NOT NULL,
	[CUSTOMER_Id] [char](32) NULL,
	[STYLE_Id] [char](32) NULL,
	[CusNo] [varchar](100) NULL,
	[CusName] [varchar](100) NULL,
	[CusPurOrderNum] [bigint] NULL,
	[PurchaseOrderNo] [varchar](200) NULL,
	[OrderNo] [varchar](500) NULL,
	[GeneratorDate] [datetime] NULL,
	[DeliveryDate] [datetime] NULL,
	[Mobile] [varchar](20) NULL,
	[DeliverAddress] [varchar](100) NULL,
	[Address] [varchar](500) NULL,
	[LinkMan] [varchar](100) NULL,
	[Tel] [varchar](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPurchaseOrderColorItem]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPurchaseOrderColorItem](
	[Id] [char](32) NOT NULL,
	[POCOLOR_Id] [char](32) NULL,
	[CUSTOMERPURCHASEORDER_Id] [char](32) NULL,
	[MOrderItemNo] [varchar](50) NULL,
	[Color] [char](20) NULL,
	[ColorDescription] [varchar](50) NULL,
	[Total] [char](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPurchaseOrderColorSizeItem]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPurchaseOrderColorSizeItem](
	[Id] [char](32) NOT NULL,
	[CUSTOMERPURCHASEORDERCOLORITEM_Id] [char](32) NULL,
	[PSIZE_Id] [char](32) NULL,
	[SizeDesption] [varchar](50) NULL,
	[Total] [varchar](50) NULL,
	[Memo] [varchar](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DefectCodeTable]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DefectCodeTable](
	[Id] [char](32) NOT NULL,
	[DefectNo] [char](20) NULL,
	[DefectCode] [char](50) NULL,
	[DefectName] [char](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [char](32) NOT NULL,
	[DepNo] [varchar](100) NULL,
	[DepName] [varchar](200) NULL,
	[Memo] [varchar](1000) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [char](32) NOT NULL,
	[DEPARTMENT_Id] [char](32) NULL,
	[AREA_Id] [char](32) NULL,
	[ORGANIZATIONS_Id] [char](32) NULL,
	[WORKTYPE_Id] [char](32) NULL,
	[SITEGROUP_Id] [char](32) NULL,
	[Code] [varchar](20) NULL,
	[Password] [varchar](200) NULL,
	[RealName] [varchar](20) NULL,
	[Birthday] [datetime] NULL,
	[HeadImage] [image] NULL,
	[Sex] [tinyint] NULL,
	[Email] [varchar](30) NULL,
	[CardNo] [varchar](30) NULL,
	[Phone] [varchar](11) NULL,
	[Mobile] [varchar](16) NULL,
	[Valid] [bit] NULL,
	[EmploymentDate] [datetime] NULL,
	[Address] [varchar](200) NULL,
	[StartingDate] [datetime] NULL,
	[LeaveDate] [datetime] NULL,
	[BankCardNo] [varchar](16) NULL,
	[Memo] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeCardRelation]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeCardRelation](
	[Id] [char](32) NOT NULL,
	[EMPLOYEE_Id] [char](32) NULL,
	[CARDINFO_Id] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeFlowProduction]    Script Date: 2018/9/3 21:01:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeFlowProduction](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[EmployeeId] [char](32) NULL,
	[EmployeeName] [varchar](20) NULL,
	[CardNo] [varchar](50) NULL,
	[SiteNo] [varchar](50) NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[SiteId] [char](32) NULL,
	[IsFlowChatChange] [bit] NULL,
	[HangerType] [tinyint] NULL,
	[Status] [tinyint] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeGrade]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeGrade](
	[Id] [char](32) NOT NULL,
	[GradeCode] [varchar](20) NULL,
	[GradeName] [varchar](30) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePositions]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePositions](
	[Id] [char](32) NOT NULL,
	[POSITION_Id] [char](32) NULL,
	[EMPLOYEE_Id] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeScheduling]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeScheduling](
	[Id] [char](32) NOT NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factory]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factory](
	[Id] [char](32) NOT NULL,
	[FacCode] [varchar](20) NULL,
	[FacName] [varchar](50) NULL,
	[Memo] [varchar](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlowAction]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlowAction](
	[Id] [char](32) NOT NULL,
	[ActionCode] [varchar](20) NULL,
	[ActionName] [varchar](50) NULL,
	[IsEnabled] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlowStatingColor]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlowStatingColor](
	[Id] [char](32) NOT NULL,
	[PROCESSFLOWSTATINGITEM_Id] [char](32) NULL,
	[StatingName] [char](20) NULL,
	[StatingNo] [char](20) NULL,
	[ColorValue] [char](20) NULL,
	[ColorDesption] [char](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlowStatingResume]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlowStatingResume](
	[Id] [char](32) NOT NULL,
	[STATING_Id] [char](32) NULL,
	[PROCESSFLOWCHARTFLOWRELATION_Id] [char](32) NULL,
	[Memo] [varchar](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlowStatingSize]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlowStatingSize](
	[Id] [char](32) NOT NULL,
	[PROCESSFLOWSTATINGITEM_Id] [char](32) NULL,
	[StatingName] [char](20) NULL,
	[StatingNo] [char](20) NULL,
	[ColorValue] [varchar](50) NULL,
	[ColorDesption] [char](50) NULL,
	[Total] [char](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hanger]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hanger](
	[Id] [char](32) NOT NULL,
	[HangerNo] [bigint] NULL,
	[RegisterDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangerProductFlowChart]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangerProductFlowChart](
	[Id] [char](32) NOT NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[IsHangerSucess] [bit] NULL,
	[PO] [varchar](50) NULL,
	[ProcessOrderNo] [varchar](50) NULL,
	[ProcessChartId] [char](32) NULL,
	[FlowIndex] [int] NULL,
	[FlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[FlowCode] [varchar](200) NULL,
	[FlowName] [varchar](200) NULL,
	[StatingId] [char](32) NULL,
	[StatingNo] [smallint] NULL,
	[StatingCapacity] [bigint] NULL,
	[NextStatingNo] [smallint] NULL,
	[FlowRealyProductStatingNo] [smallint] NULL,
	[Status] [smallint] NULL,
	[FlowType] [tinyint] NULL,
	[IsFlowSucess] [bit] NULL,
	[IsReworkSourceStating] [bit] NULL,
	[DefectCode] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[EmployeeName] [varchar](20) NULL,
	[CardNo] [varchar](50) NULL,
	[IncomeSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangerProductItem]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangerProductItem](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowId] [char](32) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[HPIndex] [int] NULL,
	[SiteId] [char](32) NULL,
	[SiteNo] [varchar](50) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsIncomeSite] [bit] NULL,
	[IsSucessedFlow] [bit] NULL,
	[IsReturnWorkFlow] [bit] NULL,
	[returnWorkSiteNo] [char](10) NULL,
	[Memo] [varchar](100) NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[IncomeSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangerReworkRecord]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangerReworkRecord](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[SiteId] [char](32) NULL,
	[SiteNo] [varchar](50) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsIncomeSite] [bit] NULL,
	[IsSucessedFlow] [bit] NULL,
	[returnWorkSiteNo] [char](10) NULL,
	[Memo] [varchar](100) NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[IncomeSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL,
	[DefectCode] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangerReworkRequest]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangerReworkRequest](
	[Id] [char](32) NOT NULL,
	[HangerNo] [varchar](50) NULL,
	[MainTrackNumber] [smallint] NULL,
	[StatingNo] [varchar](20) NULL,
	[Status] [smallint] NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangerReworkRequestItem]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangerReworkRequestItem](
	[Id] [char](32) NOT NULL,
	[HANGERREWORKREQUEST_Id] [char](32) NULL,
	[HangerNo] [varchar](50) NULL,
	[MainTrackNumber] [smallint] NULL,
	[StatingNo] [varchar](20) NULL,
	[FlowNo] [varchar](20) NULL,
	[FlowCode] [varchar](20) NULL,
	[DefectCode] [varchar](20) NULL,
	[FlowStatingNo] [varchar](20) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangerReworkRequestQueue]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangerReworkRequestQueue](
	[Id] [char](32) NOT NULL,
	[HangerNo] [varchar](50) NULL,
	[MainTrackNumber] [smallint] NULL,
	[StatingNo] [varchar](20) NULL,
	[Status] [smallint] NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangerReworkRequestQueueItem]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangerReworkRequestQueueItem](
	[Id] [char](32) NOT NULL,
	[HANGERREWORKREQUESTQUEUE_Id] [char](32) NULL,
	[HangerNo] [varchar](50) NULL,
	[MainTrackNumber] [smallint] NULL,
	[StatingNo] [varchar](20) NULL,
	[FlowNo] [varchar](20) NULL,
	[FlowCode] [varchar](20) NULL,
	[DefectCode] [varchar](20) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangerStatingAllocationItem]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangerStatingAllocationItem](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[SiteId] [char](32) NULL,
	[SiteNo] [varchar](50) NULL,
	[NextSiteNo] [varchar](50) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsIncomeSite] [bit] NULL,
	[IsSucessedFlow] [bit] NULL,
	[IsReturnWorkFlow] [bit] NULL,
	[returnWorkSiteNo] [char](10) NULL,
	[HSAINdex] [int] NULL,
	[Memo] [varchar](100) NULL,
	[IsReworkSourceStating] [bit] NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[AllocatingStatingDate] [datetime] NULL,
	[IncomeSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[HangerType] [tinyint] NULL,
	[Status] [tinyint] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HolidayInfo]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HolidayInfo](
	[Id] [char](32) NOT NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ID_GENERATOR]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ID_GENERATOR](
	[ID] [char](32) NOT NULL,
	[FLAG_NO] [varchar](50) NOT NULL,
	[BEGIN_VALUE] [bigint] NOT NULL,
	[CURRENT_VALUE] [bigint] NOT NULL,
	[END_VALUE] [bigint] NOT NULL,
	[SORT_VALUE] [bigint] NULL,
	[MEMO] [nvarchar](200) NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LackMaterialsTable]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LackMaterialsTable](
	[Id] [char](32) NOT NULL,
	[LackMaterialsCode] [char](30) NULL,
	[LackMaterialsName] [char](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LEDHoursPlanTableItem]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LEDHoursPlanTableItem](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](20) NULL,
	[BeginDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[PlanNum] [int] NULL,
	[InsertDate] [datetime] NULL,
	[Enabled] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LEDScreenConfig]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LEDScreenConfig](
	[Id] [char](32) NOT NULL,
	[ScreenNo] [varchar](20) NULL,
	[ControllerTypeTxt] [varchar](20) NULL,
	[ControllerKey] [varchar](20) NULL,
	[CommunicationWay] [int] NULL,
	[CommunicationWayTxt] [varchar](20) NULL,
	[SWidth] [int] NULL,
	[SHeight] [int] NULL,
	[ColorType] [int] NULL,
	[ColorTypeTxt] [varchar](20) NULL,
	[IPAddress] [varchar](20) NULL,
	[Port] [int] NULL,
	[GroupNo] [varchar](20) NULL,
	[Enable] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LEDScreenPage]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LEDScreenPage](
	[ID] [char](32) NOT NULL,
	[LEDSCREENCONFIG_Id] [char](32) NULL,
	[PageNo] [int] NULL,
	[InfoType] [int] NULL,
	[InfoTypeTxt] [varchar](20) NULL,
	[CusContent] [varchar](200) NULL,
	[Times] [int] NULL,
	[RefreshCycle] [int] NULL,
	[Enabled] [bit] NULL,
	[InsertTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainTrack]    Script Date: 2018/9/3 21:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainTrack](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](200) NULL,
	[Num] [smallint] NULL,
	[Status] [tinyint] NULL,
	[StartDateTime] [datetime] NULL,
	[EmergencyStopDateTime] [datetime] NULL,
	[StopDateTime] [datetime] NULL,
	[Memo] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainTrackOperateRecord]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainTrackOperateRecord](
	[Id] [char](32) NOT NULL,
	[MAINTRACK_Id] [char](32) NULL,
	[MType] [smallint] NULL,
	[Memo] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[Id] [char](32) NOT NULL,
	[MODULES_Id] [char](32) NULL,
	[ActionName] [varchar](256) NOT NULL,
	[ActionKey] [varchar](50) NOT NULL,
	[Description] [varchar](256) NULL,
	[ModulesType] [int] NOT NULL,
	[ModuleLevel] [int] NOT NULL,
	[OrderField] [decimal](5, 2) NOT NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProductItem]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProductItem](
	[Id] [char](32) NOT NULL,
	[PRODUCTORDER_Id] [char](32) NULL,
	[SequenceNumber] [char](20) NULL,
	[ProductNo] [char](20) NULL,
	[ProductName] [char](50) NULL,
	[Color] [char](20) NULL,
	[Rule] [char](20) NULL,
	[ProductNum] [bigint] NULL,
	[ProductUnit] [char](20) NULL,
	[BoxNum] [int] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[Id] [char](32) NOT NULL,
	[ORGANIZATIONS_Id] [char](32) NULL,
	[Code] [varchar](20) NULL,
	[ParentCode] [varchar](20) NULL,
	[ActionName] [varchar](256) NULL,
	[Description] [varchar](256) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pipelining]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pipelining](
	[Id] [char](32) NOT NULL,
	[SITEGROUP_Id] [char](32) NULL,
	[PRODTYPE_Id] [char](32) NULL,
	[PipeliNo] [varchar](50) NULL,
	[PushRodNum] [int] NULL,
	[Memo] [varchar](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PoColor]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PoColor](
	[Id] [char](32) NOT NULL,
	[SNo] [varchar](50) NULL,
	[ColorValue] [varchar](50) NULL,
	[ColorDescption] [varchar](50) NULL,
	[Rmark] [char](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[Id] [char](32) NOT NULL,
	[PosCode] [varchar](100) NULL,
	[PosName] [varchar](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessCraftAction]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessCraftAction](
	[Id] [char](32) NOT NULL,
	[ActionNo] [char](20) NULL,
	[ActionDesc] [char](50) NULL,
	[IsEnabled] [tinyint] NULL,
	[ProSectionNo] [char](20) NULL,
	[ProSectionCode] [char](50) NULL,
	[ProSectionName] [char](100) NULL,
	[Remark2] [char](500) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessFlow]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessFlow](
	[Id] [char](32) NOT NULL,
	[PROCESSFLOWVERSION_Id] [char](32) NULL,
	[BASICPROCESSFLOW_Id] [char](32) NULL,
	[ProcessNo] [char](20) NULL,
	[ProcessOrderField] [varchar](20) NULL,
	[ProcessName] [char](200) NULL,
	[ProcessCode] [char](20) NULL,
	[ProcessStatus] [tinyint] NULL,
	[StanardSecond] [int] NULL,
	[StanardMinute] [decimal](8, 4) NULL,
	[StanardHours] [varchar](20) NULL,
	[StandardPrice] [decimal](8, 4) NULL,
	[DefaultFlowNo] [int] NULL,
	[PrcocessRemark] [char](100) NULL,
	[ProcessColor] [varchar](20) NULL,
	[EffectiveDate] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessFlowChart]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessFlowChart](
	[Id] [char](32) NOT NULL,
	[PROCESSFLOWVERSION_Id] [char](32) NULL,
	[LinkName] [char](500) NULL,
	[pFlowChartNum] [bigint] NULL,
	[ProductPosition] [char](50) NULL,
	[TargetNum] [int] NULL,
	[OutputProcessFlowId] [char](32) NULL,
	[BoltProcessFlowId] [char](32) NULL,
	[Remark] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessFlowChartFlowRelation]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessFlowChartFlowRelation](
	[Id] [char](32) NOT NULL,
	[PROCESSFLOW_Id] [char](32) NULL,
	[PROCESSFLOWCHART_Id] [char](32) NULL,
	[CraftFlowNo] [varchar](20) NULL,
	[IsEnabled] [smallint] NULL,
	[EnabledText] [varchar](20) NULL,
	[IsMergeForward] [bit] NULL,
	[MergeForwardText] [varchar](20) NULL,
	[FlowNo] [varchar](20) NULL,
	[FlowCode] [varchar](20) NULL,
	[FlowName] [varchar](40) NULL,
	[IsProduceFlow] [tinyint] NULL,
	[MergeProcessFlowChartFlowRelationId] [char](32) NULL,
	[MergeFlowNo] [varchar](20) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessFlowChartGrop]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessFlowChartGrop](
	[Id] [char](32) NOT NULL,
	[SITEGROUP_Id] [char](32) NULL,
	[PROCESSFLOWCHART_Id] [char](32) NULL,
	[GroupNo] [varchar](50) NULL,
	[GroupName] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessFlowSection]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessFlowSection](
	[Id] [char](32) NOT NULL,
	[ProSectionNo] [char](20) NULL,
	[ProSectionName] [char](100) NULL,
	[ProSectionCode] [char](50) NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessFlowStatingItem]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessFlowStatingItem](
	[Id] [char](32) NOT NULL,
	[PROCESSFLOWCHARTFLOWRELATION_Id] [char](32) NULL,
	[STATING_Id] [char](32) NULL,
	[IsReceivingHanger] [tinyint] NULL,
	[ReceingContent] [varchar](50) NULL,
	[SiteGroupNo] [varchar](20) NULL,
	[mainTrackNumber] [int] NULL,
	[No] [varchar](20) NULL,
	[StatingRoleName] [varchar](20) NULL,
	[Memo] [varchar](100) NULL,
	[IsReceivingAllSize] [bit] NULL,
	[IsReceivingAllColor] [bit] NULL,
	[isReceivingAllPONumber] [bit] NULL,
	[IsEndStating] [bit] NULL,
	[Proportion] [decimal](3, 2) NULL,
	[ReceivingColor] [varchar](20) NULL,
	[ReceivingSize] [varchar](20) NULL,
	[ReceivingPONumber] [varchar](20) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessFlowVersion]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessFlowVersion](
	[Id] [char](32) NOT NULL,
	[PROCESSORDER_Id] [char](32) NULL,
	[ProVersionNum] [varchar](1000) NULL,
	[ProVersionNo] [char](50) NULL,
	[ProcessVersionName] [char](200) NULL,
	[EffectiveDate] [datetime] NULL,
	[TotalStandardPrice] [char](32) NULL,
	[TotalSAM] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertDate] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessOrder]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessOrder](
	[Id] [char](32) NOT NULL,
	[STYLE_Id] [char](32) NULL,
	[CUSTOMER_Id] [char](32) NULL,
	[POrderNo] [char](200) NULL,
	[POrderNum] [bigint] NULL,
	[MOrderNo] [char](20) NULL,
	[POrderType] [tinyint] NULL,
	[POrderTypeDesption] [char](50) NULL,
	[ProductNoticeOrderNo] [char](20) NULL,
	[Num] [int] NULL,
	[Status] [tinyint] NULL,
	[StyleCode] [char](20) NULL,
	[StyleName] [varchar](100) NULL,
	[CustomerNO] [varchar](100) NULL,
	[CustomerName] [varchar](100) NULL,
	[CustomerStyle] [varchar](100) NULL,
	[CustOrderNo] [varchar](100) NULL,
	[CustPurchaseOrderNo] [varchar](100) NULL,
	[DeliveryDate] [datetime] NULL,
	[GenaterOrderDate] [datetime] NULL,
	[OrderNo] [char](20) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessOrderColorItem]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessOrderColorItem](
	[Id] [char](32) NOT NULL,
	[PROCESSORDER_Id] [char](32) NULL,
	[POCOLOR_Id] [char](32) NULL,
	[CUSTOMERPURCHASEORDER_Id] [char](32) NULL,
	[MOrderItemNo] [varchar](50) NULL,
	[Color] [varchar](50) NULL,
	[ColorDescription] [varchar](50) NULL,
	[Total] [int] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProcessOrderColorSizeItem]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProcessOrderColorSizeItem](
	[Id] [char](32) NOT NULL,
	[PROCESSORDERCOLORITEM_Id] [char](32) NULL,
	[PSIZE_Id] [char](32) NULL,
	[SizeDesption] [varchar](50) NULL,
	[Total] [varchar](50) NULL,
	[Memo] [varchar](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdType]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdType](
	[Id] [char](32) NOT NULL,
	[PorTypeCode] [varchar](50) NULL,
	[PorTypeName] [varchar](100) NULL,
	[Memo] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductGroup]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductGroup](
	[Id] [char](32) NOT NULL,
	[ProductGroupCode] [varchar](20) NULL,
	[ProductGroupName] [varchar](30) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductOrder]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOrder](
	[Id] [char](32) NOT NULL,
	[OrderName] [char](20) NULL,
	[OrderPackgeType] [numeric](18, 0) NULL,
	[OrderType] [numeric](18, 0) NULL,
	[VersionNo] [char](30) NULL,
	[ProcessDate] [datetime] NULL,
	[ProcessPerson] [char](30) NULL,
	[SystemOrderNo] [char](50) NULL,
	[CustomerOrderNo] [char](50) NULL,
	[ProcessOrderNo] [char](50) NULL,
	[CustomerNo] [char](50) NULL,
	[SucessDate] [datetime] NULL,
	[Remark] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [char](32) NOT NULL,
	[PROCESSFLOWCHART_Id] [char](32) NULL,
	[PROCESSORDER_Id] [char](32) NULL,
	[GroupNo] [varchar](100) NULL,
	[ProductionNumber] [int] NULL,
	[ImplementDate] [datetime] NULL,
	[HangingPieceSiteNo] [varchar](100) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[Status] [tinyint] NULL,
	[CustomerPurchaseOrderId] [char](32) NULL,
	[OrderNo] [varchar](200) NULL,
	[StyleNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PO] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[LineName] [varchar](200) NULL,
	[FlowSection] [varchar](200) NULL,
	[Unit] [varchar](200) NULL,
	[TaskNum] [int] NULL,
	[OnlineNum] [int] NULL,
	[TodayHangingPieceSiteNum] [int] NULL,
	[TodayProdOutNum] [int] NULL,
	[TotalProdOutNum] [int] NULL,
	[TodayBindCard] [int] NULL,
	[TodayRework] [int] NULL,
	[TotalHangingPieceSiteNum] [int] NULL,
	[TotalRework] [int] NULL,
	[TotalBindNum] [int] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductsHangPieceResume]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsHangPieceResume](
	[Id] [char](32) NOT NULL,
	[PRODUCTS_Id] [char](32) NULL,
	[HangPieceNo] [varchar](200) NULL,
	[HangName] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Province]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[Id] [char](32) NOT NULL,
	[ProvinceCode] [varchar](20) NULL,
	[ProvinceName] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PSize]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PSize](
	[Id] [char](32) NOT NULL,
	[PSNo] [varchar](50) NULL,
	[Size] [varchar](100) NULL,
	[SizeDesption] [varchar](100) NULL,
	[Memo] [varchar](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [char](32) NOT NULL,
	[ActionName] [varchar](256) NULL,
	[Description] [varchar](256) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolesModules]    Script Date: 2018/9/3 21:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolesModules](
	[Id] [char](32) NOT NULL,
	[ROLES_Id] [char](32) NULL,
	[MODULES_Id] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteGroup]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteGroup](
	[Id] [char](32) NOT NULL,
	[WORKSHOP_Id] [char](32) NULL,
	[GroupNO] [varchar](50) NULL,
	[GroupName] [varchar](200) NULL,
	[FactoryCode] [varchar](200) NULL,
	[WorkshopCode] [varchar](200) NULL,
	[MainTrackNumber] [smallint] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stating]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stating](
	[Id] [char](32) NOT NULL,
	[SUSLANGUAGE_Id] [char](32) NULL,
	[STATINGROLES_Id] [char](32) NULL,
	[SITEGROUP_Id] [char](32) NULL,
	[STATINGDIRECTION_Id] [char](32) NULL,
	[StatingName] [char](20) NULL,
	[StatingNo] [char](20) NULL,
	[Language] [char](20) NULL,
	[MainTrackNumber] [smallint] NULL,
	[Capacity] [int] NULL,
	[IsReceivingHanger] [bit] NULL,
	[ColorValue] [char](20) NULL,
	[IsLoadMonitor] [bit] NULL,
	[IsChainHoist] [bit] NULL,
	[IsPromoteTripCachingFull] [bit] NULL,
	[SiteBarCode] [varchar](100) NULL,
	[IsEnabled] [bit] NULL,
	[Direction] [smallint] NULL,
	[Memo] [varchar](100) NULL,
	[MainboardNumber] [varchar](50) NULL,
	[SerialNumber] [varchar](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatingDirection]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatingDirection](
	[Id] [char](32) NOT NULL,
	[DirectionKey] [varchar](20) NULL,
	[DirectionDesc] [varchar](20) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatingHangerProductItem]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatingHangerProductItem](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[SiteNo] [varchar](50) NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[IsSucessedFlow] [bit] NULL,
	[SiteId] [char](32) NULL,
	[IsFlowChatChange] [bit] NULL,
	[Memo] [varchar](100) NULL,
	[IsIncomeSite] [bit] NULL,
	[IsReturnWorkFlow] [bit] NULL,
	[IsReworkSourceStating] [bit] NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[IncomeSiteDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatingRoles]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatingRoles](
	[Id] [char](32) NOT NULL,
	[RoleCode] [varchar](20) NULL,
	[RoleName] [varchar](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Style]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Style](
	[Id] [char](32) NOT NULL,
	[StyleNo] [char](20) NULL,
	[StyleName] [char](200) NULL,
	[Rmark] [char](100) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StyleProcessFlowItem]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StyleProcessFlowItem](
	[Id] [char](32) NOT NULL,
	[BASICPROCESSFLOW_Id] [char](32) NULL,
	[STYLE_Id] [char](32) NULL,
	[ProVersionNo] [char](50) NULL,
	[ProcessVersionName] [char](200) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessCode] [char](30) NULL,
	[ProcessStatus] [tinyint] NULL,
	[SAM] [char](20) NULL,
	[StanardHours] [char](20) NULL,
	[StandardPrice] [decimal](8, 4) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StyleProcessFlowSectionItem]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StyleProcessFlowSectionItem](
	[Id] [char](32) NOT NULL,
	[BASICPROCESSFLOW_Id] [char](32) NULL,
	[STYLE_Id] [char](32) NULL,
	[PROCESSFLOWSECTION_Id] [char](32) NULL,
	[Memo] [varchar](100) NULL,
	[FlowNo] [int] NULL,
	[ProcessCode] [char](30) NULL,
	[ProcessStatus] [tinyint] NULL,
	[ProcessName] [char](30) NULL,
	[SortNo] [char](20) NULL,
	[SAM] [char](20) NULL,
	[StanardHours] [char](20) NULL,
	[StandardPrice] [decimal](8, 4) NULL,
	[PrcocessRmark] [char](100) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuccessHangerProductFlowChart]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuccessHangerProductFlowChart](
	[Id] [char](32) NOT NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[IsHangerSucess] [bit] NULL,
	[PO] [varchar](50) NULL,
	[ProcessOrderNo] [varchar](50) NULL,
	[ProcessChartId] [char](32) NULL,
	[FlowIndex] [int] NULL,
	[FlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[FlowCode] [varchar](200) NULL,
	[FlowName] [varchar](200) NULL,
	[StatingId] [char](32) NULL,
	[StatingNo] [smallint] NULL,
	[StatingCapacity] [bigint] NULL,
	[NextStatingNo] [smallint] NULL,
	[FlowRealyProductStatingNo] [smallint] NULL,
	[Status] [smallint] NULL,
	[FlowType] [tinyint] NULL,
	[IsFlowSucess] [bit] NULL,
	[IsReworkSourceStating] [bit] NULL,
	[DefectCode] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[EmployeeName] [varchar](20) NULL,
	[CardNo] [varchar](50) NULL,
	[IncomeSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuccessHangerStatingAllocationItem]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuccessHangerStatingAllocationItem](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[SiteId] [char](32) NULL,
	[SiteNo] [varchar](50) NULL,
	[NextSiteNo] [varchar](50) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsIncomeSite] [bit] NULL,
	[IsSucessedFlow] [bit] NULL,
	[IsReturnWorkFlow] [bit] NULL,
	[returnWorkSiteNo] [char](10) NULL,
	[HSAINdex] [int] NULL,
	[Memo] [varchar](100) NULL,
	[IsReworkSourceStating] [bit] NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[AllocatingStatingDate] [datetime] NULL,
	[IncomeSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[HangerType] [tinyint] NULL,
	[Status] [tinyint] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuccessStatingHangerProductItem]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuccessStatingHangerProductItem](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[SiteNo] [varchar](50) NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[IsSucessedFlow] [bit] NULL,
	[SiteId] [char](32) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsIncomeSite] [bit] NULL,
	[Memo] [varchar](100) NULL,
	[IsReturnWorkFlow] [bit] NULL,
	[IsReworkSourceStating] [bit] NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[IncomeSiteDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SucessEmployeeFlowProduction]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SucessEmployeeFlowProduction](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[EmployeeId] [char](32) NULL,
	[EmployeeName] [varchar](20) NULL,
	[CardNo] [varchar](50) NULL,
	[SiteNo] [varchar](50) NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[SiteId] [char](32) NULL,
	[IsFlowChatChange] [bit] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SucessHangerProductItem]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SucessHangerProductItem](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[HPIndex] [int] NULL,
	[SiteId] [char](32) NULL,
	[SiteNo] [varchar](50) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsIncomeSite] [bit] NULL,
	[IsSucessedFlow] [bit] NULL,
	[IsReturnWorkFlow] [bit] NULL,
	[returnWorkSiteNo] [char](10) NULL,
	[Memo] [varchar](100) NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[IncomeSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SucessHangerReworkRecord]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SucessHangerReworkRecord](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[ProcessFlowId] [char](32) NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[SiteId] [char](32) NULL,
	[SiteNo] [varchar](50) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsIncomeSite] [bit] NULL,
	[IsSucessedFlow] [bit] NULL,
	[returnWorkSiteNo] [char](10) NULL,
	[Memo] [varchar](100) NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[IncomeSiteDate] [datetime] NULL,
	[CompareDate] [datetime] NULL,
	[OutSiteDate] [datetime] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL,
	[DefectCode] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SucessProcessOrderHanger]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SucessProcessOrderHanger](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[PO] [varchar](50) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[SizeNum] [int] NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowId] [char](32) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[SiteId] [char](32) NULL,
	[SiteNo] [varchar](50) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsSemiFinishedProducts] [bit] NULL,
	[SFClearDate] [datetime] NULL,
	[Memo] [varchar](200) NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL,
	[IsIncomeSite] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SucessProducts]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SucessProducts](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[ProductionNumber] [int] NULL,
	[ImplementDate] [datetime] NULL,
	[HangingPieceSiteNo] [varchar](100) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[Status] [tinyint] NULL,
	[CustomerPurchaseOrderId] [char](32) NULL,
	[OrderNo] [varchar](200) NULL,
	[StyleNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PO] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[LineName] [varchar](200) NULL,
	[FlowSection] [varchar](200) NULL,
	[Unit] [varchar](200) NULL,
	[TaskNum] [int] NULL,
	[OnlineNum] [int] NULL,
	[TodayHangingPieceSiteNum] [int] NULL,
	[TodayProdOutNum] [int] NULL,
	[TotalProdOutNum] [int] NULL,
	[TodayBindCard] [int] NULL,
	[TodayRework] [int] NULL,
	[TotalHangingPieceSiteNum] [int] NULL,
	[TotalRework] [int] NULL,
	[TotalBindNum] [int] NULL,
	[Memo] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SusLanguage]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SusLanguage](
	[Id] [char](32) NOT NULL,
	[LanguageKey] [varchar](20) NULL,
	[LanguageValue] [varchar](30) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemLogs]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLogs](
	[Id] [char](32) NOT NULL,
	[Name] [varchar](50) NULL,
	[LogInfo] [text] NULL,
	[CreatedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemModuleParameter]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemModuleParameter](
	[Id] [char](32) NOT NULL,
	[SystemModuleParameterId] [char](32) NULL,
	[SysNo] [varchar](20) NULL,
	[ModuleType] [smallint] NULL,
	[ModuleText] [varchar](20) NULL,
	[SecondModuleType] [smallint] NULL,
	[SecondModuleText] [varchar](20) NULL,
	[ParamterKey] [varchar](100) NULL,
	[ParamterValue] [smallint] NULL,
	[ParamterControlType] [varchar](50) NULL,
	[ParamterControlTitle] [varchar](500) NULL,
	[ParamterControlDescribe] [nvarchar](1000) NULL,
	[IsEnabled] [bit] NOT NULL,
	[Memo] [varchar](100) NULL,
	[Memo2] [varchar](100) NULL,
	[Memo3] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemModuleParameterDomain]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemModuleParameterDomain](
	[Id] [char](32) NOT NULL,
	[SYSTEMMODULEPARAMETER_Id] [char](32) NULL,
	[Code] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Enable] [bit] NOT NULL,
	[Memo] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemModuleParameterValue]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemModuleParameterValue](
	[Id] [char](32) NOT NULL,
	[SYSTEMMODULEPARAMETER_Id] [char](32) NULL,
	[ParameterValue] [varchar](50) NOT NULL,
	[ProductLineId] [varchar](32) NULL,
	[ParameterDomainCode] [varchar](50) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestSiteTable]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestSiteTable](
	[Id] [char](32) NOT NULL,
	[StatingNo] [char](20) NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClientMachines]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClientMachines](
	[Id] [char](32) NOT NULL,
	[UserId] [char](32) NOT NULL,
	[ClientMachineId] [char](32) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClientMachinesPipelinings]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClientMachinesPipelinings](
	[Id] [char](32) NOT NULL,
	[USERCLIENTMACHINES_Id] [char](32) NULL,
	[PIPELINING_Id] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginInfo](
	[SessionId] [char](32) NOT NULL,
	[UserId] [char](32) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[EmployeeId] [char](32) NOT NULL,
	[EmployeeName] [varchar](20) NOT NULL,
	[LoginDate] [datetime] NOT NULL,
	[LoginOutDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOperateLogDetail]    Script Date: 2018/9/3 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOperateLogDetail](
	[Id] [char](32) NOT NULL,
	[USEROPERATELOGS_Id] [char](32) NOT NULL,
	[FieldName] [varchar](255) NULL,
	[FieldCode] [varchar](30) NULL,
	[BeforeChange] [varchar](2000) NULL,
	[Changed] [varchar](2000) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOperateLogs]    Script Date: 2018/9/3 21:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOperateLogs](
	[Id] [char](32) NOT NULL,
	[OpFormName] [varchar](50) NULL,
	[OpFormCode] [varchar](50) NULL,
	[OpTableName] [varchar](100) NULL,
	[OpTableCode] [varchar](50) NULL,
	[OpDataCode] [varchar](50) NULL,
	[OpType] [int] NOT NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 2018/9/3 21:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [char](32) NOT NULL,
	[ROLES_Id] [char](32) NULL,
	[USERS_Id] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2018/9/3 21:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [char](32) NOT NULL,
	[EMPLOYEE_Id] [char](32) NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [char](36) NOT NULL,
	[CardNO] [char](10) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WaitProcessOrderHanger]    Script Date: 2018/9/3 21:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaitProcessOrderHanger](
	[Id] [char](32) NOT NULL,
	[GroupNo] [varchar](100) NULL,
	[MainTrackNumber] [smallint] NULL,
	[ProductsId] [char](32) NULL,
	[PO] [varchar](50) NULL,
	[BatchNo] [bigint] NULL,
	[HangerNo] [varchar](200) NULL,
	[ProcessOrderId] [char](32) NULL,
	[ProcessOrderNo] [varchar](200) NULL,
	[PColor] [varchar](200) NULL,
	[PSize] [varchar](200) NULL,
	[FlowChartd] [char](32) NULL,
	[LineName] [varchar](200) NULL,
	[FlowIndex] [smallint] NULL,
	[SizeNum] [int] NULL,
	[FlowNo] [varchar](20) NULL,
	[ProcessFlowId] [char](32) NULL,
	[ProcessFlowCode] [varchar](200) NULL,
	[ProcessFlowName] [varchar](200) NULL,
	[SiteId] [char](32) NULL,
	[SiteNo] [varchar](50) NULL,
	[IsFlowChatChange] [bit] NULL,
	[IsIncomeSite] [bit] NULL,
	[IsSemiFinishedProducts] [bit] NULL,
	[SFClearDate] [datetime] NULL,
	[Memo] [varchar](200) NULL,
	[ClientMachineId] [char](32) NULL,
	[SusLineId] [char](32) NULL,
	[HangerType] [tinyint] NULL,
	[Status] [tinyint] NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workshop]    Script Date: 2018/9/3 21:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workshop](
	[Id] [char](32) NOT NULL,
	[FACTORY_Id] [char](32) NULL,
	[WorCode] [varchar](20) NULL,
	[WorName] [varchar](50) NULL,
	[Memo] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkType]    Script Date: 2018/9/3 21:01:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkType](
	[Id] [char](32) NOT NULL,
	[WTypeCode] [varchar](100) NULL,
	[WTypeName] [varchar](200) NULL,
	[InsertDateTime] [datetime] NULL,
	[InsertUser] [char](32) NULL,
	[UpdateDateTime] [datetime] NULL,
	[UpdateUser] [char](32) NULL,
	[Deleted] [tinyint] NULL,
	[CompanyId] [char](32) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[BasicProcessFlow] ([Id], [ProcessCode], [ProcessStatus], [ProcessName], [SortNo], [StanardSecond], [SAM], [StanardHours], [StandardPrice], [PrcocessRmark], [DefaultFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c8e09f67adb84d5595263b25bff0e08f', N'1                             ', NULL, N'挂片                          ', NULL, 10, N'0.1667              ', NULL, CAST(0.2000 AS Decimal(8, 4)), NULL, NULL, CAST(N'2018-09-01T14:04:17.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[BasicProcessFlow] ([Id], [ProcessCode], [ProcessStatus], [ProcessName], [SortNo], [StanardSecond], [SAM], [StanardHours], [StandardPrice], [PrcocessRmark], [DefaultFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a497537b7ce2414e822deafed83b6c8e', N'2                             ', NULL, N'合肩                          ', NULL, 12, N'0.2                 ', NULL, CAST(0.3000 AS Decimal(8, 4)), NULL, NULL, CAST(N'2018-09-01T14:04:17.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[BasicProcessFlow] ([Id], [ProcessCode], [ProcessStatus], [ProcessName], [SortNo], [StanardSecond], [SAM], [StanardHours], [StandardPrice], [PrcocessRmark], [DefaultFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ae9ecdb904b442f284daca3591def0e6', N'3                             ', NULL, N'上袖                          ', NULL, 12, N'0.2                 ', NULL, CAST(0.3000 AS Decimal(8, 4)), NULL, NULL, CAST(N'2018-09-01T14:04:17.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[BasicProcessFlow] ([Id], [ProcessCode], [ProcessStatus], [ProcessName], [SortNo], [StanardSecond], [SAM], [StanardHours], [StandardPrice], [PrcocessRmark], [DefaultFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'414c396207704e679ca74043e2ae4fd6', N'4                             ', NULL, N'上领                          ', NULL, 15, N'0.25                ', NULL, CAST(0.5000 AS Decimal(8, 4)), NULL, NULL, CAST(N'2018-09-01T14:04:17.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[BasicProcessFlow] ([Id], [ProcessCode], [ProcessStatus], [ProcessName], [SortNo], [StanardSecond], [SAM], [StanardHours], [StandardPrice], [PrcocessRmark], [DefaultFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd0a6bddbd6ba497c9b6d4f43431f6273', N'5                             ', NULL, N'合侧边                        ', NULL, 20, N'0.3333              ', NULL, CAST(0.3000 AS Decimal(8, 4)), NULL, NULL, CAST(N'2018-09-01T14:04:17.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[BasicProcessFlow] ([Id], [ProcessCode], [ProcessStatus], [ProcessName], [SortNo], [StanardSecond], [SAM], [StanardHours], [StandardPrice], [PrcocessRmark], [DefaultFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'3eee87b6f6d14f13a7ab7fb88da745a8', N'6                             ', NULL, N'坎下摆                        ', NULL, 12, N'0.2                 ', NULL, CAST(0.4000 AS Decimal(8, 4)), NULL, NULL, CAST(N'2018-09-01T14:04:17.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[BasicProcessFlow] ([Id], [ProcessCode], [ProcessStatus], [ProcessName], [SortNo], [StanardSecond], [SAM], [StanardHours], [StandardPrice], [PrcocessRmark], [DefaultFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'12daee36505a472882a8bb33859bc509', N'7                             ', NULL, N'QC                            ', NULL, 12, N'0.2                 ', NULL, CAST(0.4000 AS Decimal(8, 4)), NULL, NULL, CAST(N'2018-09-01T14:04:17.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardInfo] ([Id], [CardNo], [CardType], [CardDescription], [IsEnabled], [IsMultiLogin], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd0359dc90fef4c3ba8b923edcb21181d', N'1', 4, NULL, 1, 0, CAST(N'2018-09-01T14:00:02.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardInfo] ([Id], [CardNo], [CardType], [CardDescription], [IsEnabled], [IsMultiLogin], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5b279f387dd34d89b6b282c7d8c5da0a', N'2', 4, NULL, 1, 0, CAST(N'2018-09-01T14:00:16.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardInfo] ([Id], [CardNo], [CardType], [CardDescription], [IsEnabled], [IsMultiLogin], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'be71c5d034064a7fa8cade02f91f05bc', N'3', 4, NULL, 1, 0, CAST(N'2018-09-01T14:00:40.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardInfo] ([Id], [CardNo], [CardType], [CardDescription], [IsEnabled], [IsMultiLogin], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'56913fc9667f4dffa9f81fabc7aa4886', N'4', 4, NULL, 1, 0, CAST(N'2018-09-01T14:00:56.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardInfo] ([Id], [CardNo], [CardType], [CardDescription], [IsEnabled], [IsMultiLogin], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'48d11928ea4e4c4ebe78afb5ff2111fe', N'5', 4, NULL, 1, 0, CAST(N'2018-09-01T14:01:19.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardInfo] ([Id], [CardNo], [CardType], [CardDescription], [IsEnabled], [IsMultiLogin], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e54c8ca5c39f469ca736914c307a488e', N'6', 4, NULL, 1, 0, CAST(N'2018-09-01T14:01:36.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'883373b8ed5c450fa8087ceddd3d38c6', N'd0359dc90fef4c3ba8b923edcb21181d', 1, N'1', CAST(N'2018-09-01T14:12:55.000' AS DateTime), CAST(N'2018-09-03T09:21:28.170' AS DateTime), 0, CAST(N'2018-09-01T14:12:55.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8498cb23cabe4ee1b416b237ce26298d', N'5b279f387dd34d89b6b282c7d8c5da0a', 1, N'2', CAST(N'2018-09-01T14:12:59.000' AS DateTime), NULL, 1, CAST(N'2018-09-01T14:12:59.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c178baf56f8944ef9ee3e826107eef69', N'be71c5d034064a7fa8cade02f91f05bc', 1, N'3', CAST(N'2018-09-01T14:13:03.000' AS DateTime), CAST(N'2018-09-03T09:21:35.553' AS DateTime), 0, CAST(N'2018-09-01T14:13:03.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'fea170f9924f4033ad6c62fad211805b', N'56913fc9667f4dffa9f81fabc7aa4886', 1, N'4', CAST(N'2018-09-01T14:13:07.000' AS DateTime), CAST(N'2018-09-03T09:21:39.600' AS DateTime), 0, CAST(N'2018-09-01T14:13:07.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'766555bc673e413da75923c147a8c612', N'48d11928ea4e4c4ebe78afb5ff2111fe', 1, N'5', CAST(N'2018-09-01T14:13:11.000' AS DateTime), CAST(N'2018-09-03T09:21:42.913' AS DateTime), 0, CAST(N'2018-09-01T14:13:11.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9637ddc234d747d0bf1178642423ca95', N'e54c8ca5c39f469ca736914c307a488e', 1, N'6', CAST(N'2018-09-01T14:13:15.000' AS DateTime), CAST(N'2018-09-03T09:21:45.983' AS DateTime), 0, CAST(N'2018-09-01T14:13:15.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cdffdf441c444717bb06ae197d456be8', N'd0359dc90fef4c3ba8b923edcb21181d', 1, N'1', CAST(N'2018-09-03T09:21:28.000' AS DateTime), NULL, 1, CAST(N'2018-09-03T09:21:28.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'736430bf368f43b3a3b4273d9d123d7e', N'be71c5d034064a7fa8cade02f91f05bc', 1, N'3', CAST(N'2018-09-03T09:21:35.000' AS DateTime), NULL, 1, CAST(N'2018-09-03T09:21:35.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2f05b5d939e248378b21f95faf763c61', N'56913fc9667f4dffa9f81fabc7aa4886', 1, N'4', CAST(N'2018-09-03T09:21:39.000' AS DateTime), NULL, 1, CAST(N'2018-09-03T09:21:39.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7f00b2abeed84d7ab3c9411a89a22b92', N'48d11928ea4e4c4ebe78afb5ff2111fe', 1, N'5', CAST(N'2018-09-03T09:21:42.000' AS DateTime), NULL, 1, CAST(N'2018-09-03T09:21:42.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CardLoginInfo] ([Id], [CARDINFO_Id], [MainTrackNumber], [LoginStatingNo], [LoginDate], [LogOutDate], [IsOnline], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ee5e24487c1140e7a85b1da24ae57b8e', N'e54c8ca5c39f469ca736914c307a488e', 1, N'6', CAST(N'2018-09-03T09:21:45.000' AS DateTime), NULL, 1, CAST(N'2018-09-03T09:21:45.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Customer] ([Id], [CusNo], [CusName], [PurchaseOrderNo], [Address], [LinkMan], [Tel], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f3335bece5e64acc9bbe474a364f19b5', N'MSD', N'麦仕德', NULL, NULL, NULL, NULL, CAST(N'2018-09-01T14:04:44.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrder] ([Id], [CUSTOMER_Id], [STYLE_Id], [CusNo], [CusName], [CusPurOrderNum], [PurchaseOrderNo], [OrderNo], [GeneratorDate], [DeliveryDate], [Mobile], [DeliverAddress], [Address], [LinkMan], [Tel], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'464911a4bf004fa0b4439ce27dcbfe5e', N'f3335bece5e64acc9bbe474a364f19b5', NULL, N'MSD', N'麦仕德', NULL, N'US01', N'MSD01', CAST(N'2018-09-01T00:00:00.000' AS DateTime), CAST(N'2018-09-30T00:00:00.000' AS DateTime), N'', N'', NULL, N'郑', N'', CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorItem] ([Id], [POCOLOR_Id], [CUSTOMERPURCHASEORDER_Id], [MOrderItemNo], [Color], [ColorDescription], [Total], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'fc1a93a3acb7462892ca830b755e20dc', N'2e14ad50aba74756b9a9e47e1c220d33', N'464911a4bf004fa0b4439ce27dcbfe5e', NULL, N'红色                ', NULL, N'300                                               ', CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorItem] ([Id], [POCOLOR_Id], [CUSTOMERPURCHASEORDER_Id], [MOrderItemNo], [Color], [ColorDescription], [Total], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5163d0afc4e0490d88928bebe08ae5b4', N'ed58ca8b95a449ffbd5ec79037e34b03', N'464911a4bf004fa0b4439ce27dcbfe5e', NULL, N'黄色                ', NULL, N'300                                               ', CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorItem] ([Id], [POCOLOR_Id], [CUSTOMERPURCHASEORDER_Id], [MOrderItemNo], [Color], [ColorDescription], [Total], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b38ad3adeeef455db67cce7cd9dba897', N'542bba42f13f40beaf9bb6a85d970a52', N'464911a4bf004fa0b4439ce27dcbfe5e', NULL, N'蓝色                ', NULL, N'300                                               ', CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorItem] ([Id], [POCOLOR_Id], [CUSTOMERPURCHASEORDER_Id], [MOrderItemNo], [Color], [ColorDescription], [Total], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bf7040cd06d2457584ac2e1c6384a3b9', N'f4475aa116ec4cd2bd25f714dbfdb114', N'464911a4bf004fa0b4439ce27dcbfe5e', NULL, N'白色                ', NULL, N'300                                               ', CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cdc9de940fb944b2bed0f8a647ed8475', N'fc1a93a3acb7462892ca830b755e20dc', N'1e4a303a56664a6a9aae2451dba8252c', N'S', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'40b9c79298694306ba3b604c2eaa59d3', N'fc1a93a3acb7462892ca830b755e20dc', N'877ee22e6d7243e6b01479b5cbd2b761', N'M', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ac6c47ec6f814ae88970c80989cd576a', N'fc1a93a3acb7462892ca830b755e20dc', N'786fe8fa602b4dd385eb954bf1af4867', N'L', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bf3be482810d4c3fa94e1d8cf969284c', N'5163d0afc4e0490d88928bebe08ae5b4', N'1e4a303a56664a6a9aae2451dba8252c', N'S', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'90b4608615c14a7785b549ba3b622da4', N'5163d0afc4e0490d88928bebe08ae5b4', N'877ee22e6d7243e6b01479b5cbd2b761', N'M', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bac6ec7da54040f2a906fb649e7d4fab', N'5163d0afc4e0490d88928bebe08ae5b4', N'786fe8fa602b4dd385eb954bf1af4867', N'L', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cb54950830ff469a85e4d38d5b8c95c0', N'b38ad3adeeef455db67cce7cd9dba897', N'1e4a303a56664a6a9aae2451dba8252c', N'S', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9f0a31e972db407aaf322acea738e6ce', N'b38ad3adeeef455db67cce7cd9dba897', N'877ee22e6d7243e6b01479b5cbd2b761', N'M', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c93d8af43aeb4eca91373aafc38b1935', N'b38ad3adeeef455db67cce7cd9dba897', N'786fe8fa602b4dd385eb954bf1af4867', N'L', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'87e41b6bff1f4a90a73b94d9e69842d8', N'bf7040cd06d2457584ac2e1c6384a3b9', N'1e4a303a56664a6a9aae2451dba8252c', N'S', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'30af1013177942bcaf454ba3cbccb896', N'bf7040cd06d2457584ac2e1c6384a3b9', N'877ee22e6d7243e6b01479b5cbd2b761', N'M', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[CustomerPurchaseOrderColorSizeItem] ([Id], [CUSTOMERPURCHASEORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'fa2c524f01ae478ca46b06b6fc22a021', N'bf7040cd06d2457584ac2e1c6384a3b9', N'786fe8fa602b4dd385eb954bf1af4867', N'L', N'100', NULL, CAST(N'2018-09-01T14:07:38.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Employee] ([Id], [DEPARTMENT_Id], [AREA_Id], [ORGANIZATIONS_Id], [WORKTYPE_Id], [SITEGROUP_Id], [Code], [Password], [RealName], [Birthday], [HeadImage], [Sex], [Email], [CardNo], [Phone], [Mobile], [Valid], [EmploymentDate], [Address], [StartingDate], [LeaveDate], [BankCardNo], [Memo], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'dc226049baf743ebb6796ad54395902d', NULL, NULL, NULL, NULL, NULL, N'7557be', NULL, N'系统用户', NULL, NULL, NULL, NULL, N'1003479932', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-07-10T21:58:44.000' AS DateTime), N'                                ', CAST(N'2018-07-10T21:58:45.000' AS DateTime), N'                                ', 0, N'c001                            ')
GO
INSERT [dbo].[Employee] ([Id], [DEPARTMENT_Id], [AREA_Id], [ORGANIZATIONS_Id], [WORKTYPE_Id], [SITEGROUP_Id], [Code], [Password], [RealName], [Birthday], [HeadImage], [Sex], [Email], [CardNo], [Phone], [Mobile], [Valid], [EmploymentDate], [Address], [StartingDate], [LeaveDate], [BankCardNo], [Memo], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b99f7dff53f449809665ee2654225d78', NULL, NULL, NULL, NULL, N'9866238500604d0291a888ae4c91368e', N'MSD001', NULL, N'飞马', NULL, NULL, 1, N'', N'1', N'', N'', 1, NULL, N'', NULL, NULL, N'', NULL, CAST(N'2018-09-01T14:00:02.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Employee] ([Id], [DEPARTMENT_Id], [AREA_Id], [ORGANIZATIONS_Id], [WORKTYPE_Id], [SITEGROUP_Id], [Code], [Password], [RealName], [Birthday], [HeadImage], [Sex], [Email], [CardNo], [Phone], [Mobile], [Valid], [EmploymentDate], [Address], [StartingDate], [LeaveDate], [BankCardNo], [Memo], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5d4797a1aa6849738583af6059b30135', NULL, NULL, NULL, NULL, NULL, N'MSD002', NULL, N'杰克', NULL, NULL, 1, N'', N'2', N'', N'', 1, NULL, N'', NULL, NULL, N'', NULL, CAST(N'2018-09-01T14:00:16.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Employee] ([Id], [DEPARTMENT_Id], [AREA_Id], [ORGANIZATIONS_Id], [WORKTYPE_Id], [SITEGROUP_Id], [Code], [Password], [RealName], [Birthday], [HeadImage], [Sex], [Email], [CardNo], [Phone], [Mobile], [Valid], [EmploymentDate], [Address], [StartingDate], [LeaveDate], [BankCardNo], [Memo], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4758299546a54754ac9d3c05d91cc841', NULL, NULL, NULL, NULL, NULL, N'MSD003', NULL, N'中缝', NULL, NULL, 1, N'', N'3', N'', N'', 1, NULL, N'', NULL, NULL, N'', NULL, CAST(N'2018-09-01T14:00:40.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Employee] ([Id], [DEPARTMENT_Id], [AREA_Id], [ORGANIZATIONS_Id], [WORKTYPE_Id], [SITEGROUP_Id], [Code], [Password], [RealName], [Birthday], [HeadImage], [Sex], [Email], [CardNo], [Phone], [Mobile], [Valid], [EmploymentDate], [Address], [StartingDate], [LeaveDate], [BankCardNo], [Memo], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'23aa77b807f34fd5a9444b428c96e207', NULL, NULL, NULL, NULL, NULL, N'MSD004', NULL, N'重机', NULL, NULL, 1, N'', N'4', N'', N'', 1, NULL, N'', NULL, NULL, N'', NULL, CAST(N'2018-09-01T14:00:56.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Employee] ([Id], [DEPARTMENT_Id], [AREA_Id], [ORGANIZATIONS_Id], [WORKTYPE_Id], [SITEGROUP_Id], [Code], [Password], [RealName], [Birthday], [HeadImage], [Sex], [Email], [CardNo], [Phone], [Mobile], [Valid], [EmploymentDate], [Address], [StartingDate], [LeaveDate], [BankCardNo], [Memo], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a755ca1e91f54847a9a9b27bf554e6e3', NULL, NULL, NULL, NULL, NULL, N'MSD005', NULL, N'森本', NULL, NULL, 1, N'', N'5', N'', N'', 1, NULL, N'', NULL, NULL, N'', NULL, CAST(N'2018-09-01T14:01:19.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Employee] ([Id], [DEPARTMENT_Id], [AREA_Id], [ORGANIZATIONS_Id], [WORKTYPE_Id], [SITEGROUP_Id], [Code], [Password], [RealName], [Birthday], [HeadImage], [Sex], [Email], [CardNo], [Phone], [Mobile], [Valid], [EmploymentDate], [Address], [StartingDate], [LeaveDate], [BankCardNo], [Memo], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b65ad288fc6e491aa3ed45e00e2c3eaa', NULL, NULL, NULL, NULL, NULL, N'MSD006', NULL, N'兄弟', NULL, NULL, 1, N'', N'6', N'', N'', 1, NULL, N'', NULL, NULL, N'', NULL, CAST(N'2018-09-01T14:01:36.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[EmployeeCardRelation] ([Id], [EMPLOYEE_Id], [CARDINFO_Id]) VALUES (N'00ed041d2f5d4d24836931a4ba3baa13', N'b99f7dff53f449809665ee2654225d78', N'd0359dc90fef4c3ba8b923edcb21181d')
GO
INSERT [dbo].[EmployeeCardRelation] ([Id], [EMPLOYEE_Id], [CARDINFO_Id]) VALUES (N'f317067db2594bd8aace697889d8ce93', N'5d4797a1aa6849738583af6059b30135', N'5b279f387dd34d89b6b282c7d8c5da0a')
GO
INSERT [dbo].[EmployeeCardRelation] ([Id], [EMPLOYEE_Id], [CARDINFO_Id]) VALUES (N'1c2c27e7c8cf4d23b792942c8c3d10e0', N'4758299546a54754ac9d3c05d91cc841', N'be71c5d034064a7fa8cade02f91f05bc')
GO
INSERT [dbo].[EmployeeCardRelation] ([Id], [EMPLOYEE_Id], [CARDINFO_Id]) VALUES (N'efe20f9fcd2846fbb5947d9bcf252d91', N'23aa77b807f34fd5a9444b428c96e207', N'56913fc9667f4dffa9f81fabc7aa4886')
GO
INSERT [dbo].[EmployeeCardRelation] ([Id], [EMPLOYEE_Id], [CARDINFO_Id]) VALUES (N'9000624e51294fc286a3de97241ec417', N'a755ca1e91f54847a9a9b27bf554e6e3', N'48d11928ea4e4c4ebe78afb5ff2111fe')
GO
INSERT [dbo].[EmployeeCardRelation] ([Id], [EMPLOYEE_Id], [CARDINFO_Id]) VALUES (N'afa3ef85cee54460ac8201dbe429225a', N'b65ad288fc6e491aa3ed45e00e2c3eaa', N'e54c8ca5c39f469ca736914c307a488e')
GO
INSERT [dbo].[EmployeeFlowProduction] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [EmployeeId], [EmployeeName], [CardNo], [SiteNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [IsFlowChatChange], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b164b254f68b4ab09ad9ab70c9dfb4cb', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'4758299546a54754ac9d3c05d91cc841', N'中缝', N'3', N'3', N'5825880', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', NULL, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', 2, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:23:12.817' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[EmployeeFlowProduction] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [EmployeeId], [EmployeeName], [CardNo], [SiteNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [IsFlowChatChange], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6a6989c5bd66494ea699b3e71124bd94', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'23aa77b807f34fd5a9444b428c96e207', N'重机', N'4', N'4', N'5803209', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', NULL, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', 2, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:48:14.263' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'3c60b5aea9df4c5590dc482a742bf5a4', 1925059, CAST(N'2018-09-01T16:36:06.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'3c81fb4b89064fad913c3c023c884799', 3891865, CAST(N'2018-09-01T17:48:55.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'9f8089c74ea046859216b74402e25dca', 5825412, CAST(N'2018-09-01T18:03:12.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'03ffe8adaa6f4d07b630a875e3ca2ff2', 3654152, CAST(N'2018-09-01T18:03:18.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'983634afb677474cbb3ac7ba15587d3d', 5805528, CAST(N'2018-09-01T18:42:16.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'3905cf1a37364028a86cc4b10eb40a14', 3880203, CAST(N'2018-09-01T18:42:22.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'2b99729545c047d28539790ff331ba7e', 1910827, CAST(N'2018-09-01T19:16:59.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'c836e9487f8c4ffa9c66339874a2479a', 5803209, CAST(N'2018-09-03T09:22:52.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'88863cc7cd894e23b742bc89e5e1c988', 1931390, CAST(N'2018-09-03T09:23:00.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'463e797632dd44c886ec83aef2caf3b4', 5806752, CAST(N'2018-09-01T18:03:24.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'a53496d4914b404589fa13158a0e88dc', 5813545, CAST(N'2018-09-01T18:43:03.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'd62df8a4d12c473c8a6b8678678a04b1', 1929144, CAST(N'2018-09-01T19:19:42.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'6922dc37d14c48c4b1cadda13c7ff12c', 2178816, CAST(N'2018-09-03T09:24:37.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'386f407760e748409fde53f288cde8ad', 5804793, CAST(N'2018-09-03T09:31:23.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'05ed12e1af36499f82dda3449f19de0a', 1924111, CAST(N'2018-09-01T21:27:19.000' AS DateTime))
GO
INSERT [dbo].[Hanger] ([Id], [HangerNo], [RegisterDate]) VALUES (N'231febff95d64ed083bd199bd6fa3f8b', 5825880, CAST(N'2018-09-03T12:11:12.000' AS DateTime))
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd20ad06664294b2cb10dbe9d51cb0983', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5803209', 0, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 2, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', N'bb6b92b9432642d5a99b91619720190d', 4, 20, 5, 4, 2, 0, 1, 0, NULL, N'黄色', N'L', N'重机', N'4', NULL, CAST(N'2018-09-03T16:48:09.803' AS DateTime), CAST(N'2018-09-03T16:48:14.237' AS DateTime), CAST(N'2018-09-03T16:48:14.247' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5ed288b53edb4972bff2a19ad99096c4', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5803209', 0, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 3, N'3dcaab7fb94c49649fece7416e5bc514', N'3', N'3                   ', N'上袖                                    ', N'bb6b92b9432642d5a99b91619720190d', 4, 0, NULL, NULL, 2, 0, 1, 0, NULL, N'黄色', N'L', N'重机', N'4', NULL, CAST(N'2018-09-03T16:48:09.803' AS DateTime), CAST(N'2018-09-03T16:48:14.237' AS DateTime), CAST(N'2018-09-03T16:48:14.250' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bef6a0ac233f460789bf4132fcd72bcb', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5825880', NULL, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 1, NULL, N'1', N'1                   ', N'挂片                                    ', NULL, 1, 100, NULL, NULL, 2, 2, 0, NULL, NULL, N'黄色', N'L', N'飞马', N'1', NULL, CAST(N'2018-09-03T16:18:22.950' AS DateTime), CAST(N'2018-09-03T16:18:29.880' AS DateTime), CAST(N'2018-09-03T16:18:29.880' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2b2632083b2a4852bd319101fc034e02', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5803209', NULL, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 1, NULL, N'1', N'1                   ', N'挂片                                    ', NULL, 1, 100, NULL, NULL, 2, 2, 0, NULL, NULL, N'黄色', N'L', N'飞马', N'1', NULL, CAST(N'2018-09-03T16:18:31.627' AS DateTime), CAST(N'2018-09-03T16:18:34.003' AS DateTime), CAST(N'2018-09-03T16:18:34.003' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6dde0d03dbdd4472a31f0361da372b19', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5805528', NULL, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 1, NULL, N'1', N'1                   ', N'挂片                                    ', NULL, 1, 100, NULL, NULL, 2, 2, 0, NULL, NULL, N'黄色', N'L', N'飞马', N'1', NULL, CAST(N'2018-09-03T16:18:35.817' AS DateTime), CAST(N'2018-09-03T16:19:12.457' AS DateTime), CAST(N'2018-09-03T16:19:12.457' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'eedab46709764ce695d7d8761d49a81c', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5803209', 0, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 2, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', N'137f345ff80542159a87b54c23ca186b', 3, 20, 5, 3, 2, 0, 1, 0, NULL, N'黄色', N'L', N'中缝', N'3', NULL, CAST(N'2018-09-03T16:23:00.357' AS DateTime), CAST(N'2018-09-03T16:23:04.883' AS DateTime), CAST(N'2018-09-03T16:23:04.907' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c6afb62f064c45a19d1d8c92e39eef3f', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5803209', 0, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 3, N'3dcaab7fb94c49649fece7416e5bc514', N'3', N'3                   ', N'上袖                                    ', N'137f345ff80542159a87b54c23ca186b', 3, 0, NULL, NULL, 2, 0, 1, 0, NULL, N'黄色', N'L', N'中缝', N'3', NULL, CAST(N'2018-09-03T16:23:00.357' AS DateTime), CAST(N'2018-09-03T16:23:04.883' AS DateTime), CAST(N'2018-09-03T16:23:04.907' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd4416c64eb5a489281366efca0cd3b18', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5825880', 0, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 2, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', N'137f345ff80542159a87b54c23ca186b', 3, 20, 5, 3, 2, 0, 1, 0, NULL, N'黄色', N'L', N'中缝', N'3', CAST(N'2018-09-03T16:18:46.410' AS DateTime), CAST(N'2018-09-03T16:23:07.880' AS DateTime), CAST(N'2018-09-03T16:23:12.767' AS DateTime), CAST(N'2018-09-03T16:23:12.787' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'dbed9889e69a4558baa77393b633707d', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5825880', 0, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 3, N'3dcaab7fb94c49649fece7416e5bc514', N'3', N'3                   ', N'上袖                                    ', N'137f345ff80542159a87b54c23ca186b', 3, 0, NULL, NULL, 2, 0, 1, 0, NULL, N'黄色', N'L', N'中缝', N'3', CAST(N'2018-09-03T16:18:46.410' AS DateTime), CAST(N'2018-09-03T16:23:07.880' AS DateTime), CAST(N'2018-09-03T16:23:12.767' AS DateTime), CAST(N'2018-09-03T16:23:12.787' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c6a9c88d997c4bf88e32d257ae01e1d9', 1, N'acbf8501354843119780f00cbcfc2455', 2, N'5806752', NULL, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 1, NULL, N'1', N'1                   ', N'挂片                                    ', NULL, 1, 100, NULL, NULL, 2, 2, 0, NULL, NULL, N'黄色', N'L', N'飞马', N'1', NULL, CAST(N'2018-09-03T16:19:14.280' AS DateTime), CAST(N'2018-09-03T16:31:19.443' AS DateTime), CAST(N'2018-09-03T16:31:19.443' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [FlowNo], [ProcessFlowId], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [HPIndex], [SiteId], [SiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [Memo], [ClientMachineId], [SusLineId], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c8e981300bde45759f4259997dd4aeff', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5825880', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', NULL, N'2', N'a72a4e68ab13451f92199bb63a074806', N'2                   ', N'合肩                                    ', 2, NULL, NULL, N'3', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:18:46.413' AS DateTime), CAST(N'2018-09-03T16:23:07.880' AS DateTime), CAST(N'2018-09-03T16:23:12.760' AS DateTime), CAST(N'2018-09-03T16:23:12.770' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerProductItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [FlowNo], [ProcessFlowId], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [HPIndex], [SiteId], [SiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [Memo], [ClientMachineId], [SusLineId], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'31c4aa971ba140c6bfe540f862dc7087', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5803209', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', NULL, N'2', N'a72a4e68ab13451f92199bb63a074806', N'2                   ', N'合肩                                    ', 2, NULL, NULL, N'4', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:19:02.963' AS DateTime), CAST(N'2018-09-03T16:48:09.803' AS DateTime), CAST(N'2018-09-03T16:48:14.237' AS DateTime), CAST(N'2018-09-03T16:48:14.240' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b30f35414a4b4d1680acf7d2bc9cf232', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5825880', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'3', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:18:29.000' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:18:29.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7bd61efcd375452a8c6c783f7366e360', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5825880', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'1', N'挂片', 0, NULL, N'1', NULL, 0, NULL, NULL, NULL, NULL, 1, N'-1', NULL, NULL, NULL, CAST(N'2018-09-03T16:18:29.857' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:18:29.893' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'3559fe42faf746cd9f83030c10412115', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5825880', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'3', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:18:29.867' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:18:29.903' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'919327e787c0452bb129bf91deb11130', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5803209', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'4', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:18:33.000' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:18:33.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c0875edf8eec40589d55bcfbb165670e', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5803209', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'1', N'挂片', 0, NULL, N'1', NULL, 0, NULL, NULL, NULL, NULL, 1, N'-1', NULL, NULL, NULL, CAST(N'2018-09-03T16:18:33.993' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:18:34.007' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'87848890209b48ebb93d690b05141419', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5803209', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'4', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:18:33.993' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:18:34.007' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'0437a7f6393e4dffb457dca19705f502', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5803209', NULL, NULL, NULL, NULL, N'39b76b82c50c4cb8a191d0be5a240ac6', NULL, NULL, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', 2, NULL, NULL, N'4', NULL, NULL, NULL, NULL, NULL, NULL, N'监测点衣架重新分配', NULL, NULL, NULL, CAST(N'2018-09-03T16:18:52.107' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:18:52.107' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f19f03966af4458f897c363e0471cfd9', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5805528', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'1', N'挂片', 0, NULL, N'1', NULL, 0, NULL, NULL, NULL, NULL, 1, N'-1', NULL, NULL, NULL, CAST(N'2018-09-03T16:19:12.453' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:19:12.460' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7af639b9fcf04cb1860ffa312448477f', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5805528', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'3', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:19:12.453' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:19:12.460' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'285187c4c5354e1abbec8ac21131ff71', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5805528', NULL, NULL, NULL, NULL, N'39b76b82c50c4cb8a191d0be5a240ac6', NULL, NULL, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', 2, NULL, NULL, N'4', NULL, NULL, NULL, NULL, NULL, NULL, N'监测点衣架重新分配', NULL, NULL, NULL, CAST(N'2018-09-03T16:19:46.010' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:19:46.013' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'15b206b2cee44ac595534e669c985c50', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5805528', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'3', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:19:12.000' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:19:12.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1989708d951a45d2af2eeaad7d0701ac', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5805528', NULL, NULL, NULL, NULL, N'39b76b82c50c4cb8a191d0be5a240ac6', NULL, NULL, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', 2, NULL, NULL, N'4', NULL, NULL, NULL, NULL, NULL, NULL, N'监测点衣架重新分配', NULL, NULL, NULL, CAST(N'2018-09-03T16:19:57.607' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:19:57.607' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bcbaf82341374a7ea12c58eee26ffe48', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5805528', NULL, NULL, NULL, NULL, N'39b76b82c50c4cb8a191d0be5a240ac6', NULL, NULL, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', 2, NULL, NULL, N'4', NULL, NULL, NULL, NULL, NULL, NULL, N'监测点衣架重新分配', NULL, NULL, NULL, CAST(N'2018-09-03T16:20:15.067' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:20:15.070' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a7560e03443549adbc894eefc37b1000', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 2, N'5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'3', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:31:19.000' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:19.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'076bb2efc5cd429b8d295c1650fec184', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5803209', NULL, N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', NULL, NULL, N'ebd6eb439192413cb887a22a035f59c4', N'4', N'4                   ', N'上领                                    ', 4, NULL, N'4', N'5', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:48:14.243' AS DateTime), NULL, NULL, NULL, 0, 0, CAST(N'2018-09-03T16:48:14.263' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'734af6700faf4a02b82cdedf52434027', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5803209', NULL, N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', NULL, NULL, N'ebd6eb439192413cb887a22a035f59c4', N'4', N'4                   ', N'上领                                    ', 4, NULL, N'3', N'5', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:23:04.893' AS DateTime), NULL, NULL, NULL, 0, 0, CAST(N'2018-09-03T16:23:04.910' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cb05a2a981244f02aa21015373db88af', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5825880', NULL, N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', NULL, NULL, N'ebd6eb439192413cb887a22a035f59c4', N'4', N'4                   ', N'上领                                    ', 4, NULL, N'3', N'5', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:23:12.773' AS DateTime), NULL, NULL, NULL, 0, 0, CAST(N'2018-09-03T16:23:12.823' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bd0b06839a16450abe2726db531de4d1', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 2, N'5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'1', N'挂片', 0, NULL, N'1', NULL, 0, NULL, NULL, NULL, NULL, 1, N'-1', NULL, NULL, NULL, CAST(N'2018-09-03T16:31:19.437' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:19.443' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[HangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6349f6f615f040d3b4d34e28fd145460', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 2, N'5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'3', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:31:19.437' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:19.443' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ID_GENERATOR] ([ID], [FLAG_NO], [BEGIN_VALUE], [CURRENT_VALUE], [END_VALUE], [SORT_VALUE], [MEMO], [CompanyId]) VALUES (N'9fbd0b67aa624086bfbabcbbf6518d75', N'ProcessOrder', 1, 0, 99999, NULL, NULL, N'c001                            ')
GO
INSERT [dbo].[ID_GENERATOR] ([ID], [FLAG_NO], [BEGIN_VALUE], [CURRENT_VALUE], [END_VALUE], [SORT_VALUE], [MEMO], [CompanyId]) VALUES (N'237877b91500499b999d0811658ec78d', N'ProcessFlowVersion', 1, 0, 99999, NULL, NULL, N'c001                            ')
GO
INSERT [dbo].[ID_GENERATOR] ([ID], [FLAG_NO], [BEGIN_VALUE], [CURRENT_VALUE], [END_VALUE], [SORT_VALUE], [MEMO], [CompanyId]) VALUES (N'7868fce4b11a49c9a95e18f2653b7517', N'ProcessFlowChart', 1, 0, 99999, NULL, NULL, N'c001                            ')
GO
INSERT [dbo].[ID_GENERATOR] ([ID], [FLAG_NO], [BEGIN_VALUE], [CURRENT_VALUE], [END_VALUE], [SORT_VALUE], [MEMO], [CompanyId]) VALUES (N'847c1d2170ef43cda52b64b1b451df56', N'Products', 1, 1, 255, NULL, NULL, N'c001                            ')
GO
INSERT [dbo].[MainTrack] ([Id], [GroupNo], [Num], [Status], [StartDateTime], [EmergencyStopDateTime], [StopDateTime], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f5594290699147ffa32c913f052bd379', N'A', 1, 1, CAST(N'2018-09-03T16:47:12.000' AS DateTime), CAST(N'2018-09-03T16:56:55.000' AS DateTime), CAST(N'2018-09-01T14:49:52.000' AS DateTime), NULL, CAST(N'2018-09-01T14:49:38.000' AS DateTime), CAST(N'2018-09-03T16:56:55.000' AS DateTime), N'                                ', N'                                ', 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'48e5cf57e20c47eda36e618d33d21164', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T14:49:49.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b54ddcb8ac9f4c4aa5b6d8445c72a417', N'f5594290699147ffa32c913f052bd379', 2, N'停止主轨!', CAST(N'2018-09-01T14:49:52.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1b2400549b39487aabcf35d068c8206e', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T14:50:02.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2433e282b3ef4813a46c61b08aa0c9fb', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T14:50:10.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'aa0d0fc0699b40adbc8487b76e5eec7b', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T14:53:45.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'eab2d0309f4e4226ab527dc837f6e8a4', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T14:53:47.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'52e3c0300b2b422ba0d3ea4262fda512', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T14:56:34.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7e9d7f5d21034ad4a557642020f77159', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T14:57:11.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ed9d2d0aae864fcd9f2423b08ceb5ba8', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T15:01:31.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1b4307ea0824421e9340882aa932e957', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T15:01:53.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'aade32a8f1ab4fc183e0745bffb55eb1', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T16:13:47.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'637f453a69524046ac31f5758ab7825f', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T16:49:37.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b9fde94b100b4b20bd4542a7d54ce9e6', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T17:51:59.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ddbf95a7162e4f129f9b042be5af263c', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T18:04:35.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9e50bbecbe204160bdaf19f06c1742c2', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T18:44:25.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'57d732ebfae348c3aa7e546f28612ce0', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T21:28:09.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'175ec7c4cb8f4716bba872ce0cb0adc9', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T09:21:48.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c440918a77ed49eeb8286ccb5e05e8c7', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T11:19:07.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8c6d2ab46f5446b3af75b6b97dd8242f', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T11:27:29.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'59304b1b399d4015bfd3d617cffa94b8', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T12:26:25.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'0b5a2ef13e884adf9424df21ba38149d', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T12:28:07.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5f429934aeb249689a58024a82d2e468', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T12:40:06.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e063bc47771943f9ae71ca683d33cc13', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T13:45:07.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a7da0aade1b647f78712282ad5cc44c5', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T13:48:02.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ea2658ca2a7641549db257352a9132da', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T13:58:20.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'afd51378e0e545138e80ad85ffd5a303', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T16:20:40.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8ce0948109094cc1b1a8623bc15d94bf', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T16:27:24.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'0e3966d5314243ca925e558cee41ea11', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T16:16:00.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'38fcb1cde2a34a0290e77ddd105df1e7', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T16:33:21.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'35faca50e9cd449c8c068305ce35b7e5', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T17:48:38.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'03e1ef0a1f3d41cd863424214f1135b1', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T17:49:59.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'047c2d0dd281457c88118be281123611', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T17:56:45.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2f5de12f5450458eaadf5d870d8ecbb3', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T18:02:56.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6f65247bc4334e8bb1713a8aa98aef04', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T18:05:10.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1bc86fb7d9274bdfabf5c3c172b0491d', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T18:19:55.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'719424b63bd04428986bf365f43cddf4', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T18:41:18.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cfee96faf4dd4209af6d6faea98c3465', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T19:16:14.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'78a51657a00f4d039a6aee994bd163c0', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-01T19:22:20.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'69ebb8eab8ab4fd7ad3dbba538f4def2', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-01T21:26:32.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bb2863cbdbc243f78e3219030a0f9b6c', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T09:26:28.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c910cb54db2848ac94e779550abea9bf', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T09:29:32.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a9f55ea56a8a45feb61c5f7db77f0557', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T09:33:31.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'29853aa66e8a4f65aca41c65f822c06d', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T11:13:45.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'65a5ee52d9634c15818f4470a71c545b', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T11:23:39.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a490c6d5b28d49f4b5e7c8b8f13eeeaf', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T11:37:38.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'460d9c5fb67245ff83f66c9b5db82cb2', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T11:39:21.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4547bac4ad4448b5834621a2a5ca5aab', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T11:45:00.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e4d20af75a334ecbb073734f29b993c2', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T11:49:37.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'40668f76f8304a05bf5bdca275e62ad6', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T11:56:48.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1d58146fa12e440a9f970070c50f72ff', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T12:05:02.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'3bba528a953a42e184c559e4e663d9d1', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T12:05:36.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b33c253807834aa38b5f5866b112f828', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T12:13:54.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7303729d77f04a2fbc2c4a560b739970', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T12:38:34.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd010d402fd854f61b4bc1360dc978056', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T14:00:33.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'0f054f4e47674c8c8b33ae69e2b05fd3', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T14:14:49.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd0a999214929472ab69d3f3957cfca10', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T14:39:57.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8e54211a0c934576a1677b200422dab7', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T16:18:13.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a27fbacdd8464a6389a0016cc3122fef', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T16:22:36.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'94b9d4c57d2d4edea122ff5ff02864f4', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T16:32:45.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'05d02a6d3c9b44e4b0620f83baecd43e', N'f5594290699147ffa32c913f052bd379', 1, N'急停主轨!', CAST(N'2018-09-03T16:56:55.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5222fabbdc8d4764806de9067c1b2d52', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T16:30:56.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[MainTrackOperateRecord] ([Id], [MAINTRACK_Id], [MType], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c18f2a20f98049f5981a8f84543e4a0d', N'f5594290699147ffa32c913f052bd379', 0, N'启动主轨!', CAST(N'2018-09-03T16:47:12.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e182ff1816e8454c8c0c1c9788872478', NULL, N'厦门悬挂系统', N'root', N'根目录', 1, 0, CAST(0.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'69f9e95079d142ebbaaf1e5daf115a59', N'e182ff1816e8454c8c0c1c9788872478', N'启动主轨', N'barBtn_RunMainTrack', N'启动主轨', 3, 1, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'164bdafc5cdd41b29265bb8cfe9f06f4', N'e182ff1816e8454c8c0c1c9788872478', N'停止主轨', N'barBtn_StopMainTrack', N'停止主轨', 3, 1, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'42efab31384f476e8fe91836bab7fac2', N'e182ff1816e8454c8c0c1c9788872478', N'急停主轨', N'barBtn_EmergencyStopMainTrack', N'急停主轨', 3, 1, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1e41f5ee1bef461fa67fe8b053bed05c', N'e182ff1816e8454c8c0c1c9788872478', N'生产管理', N'barBtn_RealTimeInformation', N'生产管理', 3, 1, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'65e5267e953947c9bc8e597624ec5644', N'1e41f5ee1bef461fa67fe8b053bed05c', N'生产制单', N'Billing_ProcessOrderIndex', N'生产制单', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6389a8429a1e4c77abeade0bf0fd9fdd', N'1e41f5ee1bef461fa67fe8b053bed05c', N'制单工序', N'Billing_ProcessFlowIndex', N'制单工序', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'156240961dfc40aaaf8014e81c635f7b', N'1e41f5ee1bef461fa67fe8b053bed05c', N'工艺路线图', N'Billing_ProcessFlowChartIndex', N'工艺路线图', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'491d39b1d0d249609745f3ba9f5f1a3a', N'1e41f5ee1bef461fa67fe8b053bed05c', N'产线实时信息', N'Billing_ProductRealtimeInfo', N'产线实时信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'821c15668fe14878b4e21c44b7dba36f', N'1e41f5ee1bef461fa67fe8b053bed05c', N'在制品信息', N'Billing_ProductsingInfo', N'在制品信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'576d19566b8c48078fe05e5131956616', N'1e41f5ee1bef461fa67fe8b053bed05c', N'衣架信息', N'Billing_CoatHanger', N'衣架信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cc1102076eb541338af41222a3954566', N'e182ff1816e8454c8c0c1c9788872478', N'统计报表', N'barBtn_RealTimeInformation', N'统计报表', 3, 1, CAST(5.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'784e66f1c75546939b42aa5dec1e92f3', N'cc1102076eb541338af41222a3954566', N'产量汇总', N' ', N'产量汇总', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9fd9dd41f0774f219b7abe1db0e8117e', N'cc1102076eb541338af41222a3954566', N'员工产量报表', N' ', N'员工产量报表', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'44b487507fc847aea9220ae0bd6d0ef8', N'cc1102076eb541338af41222a3954566', N'生产进度报表', N' ', N'生产进度报表', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bd5940202d9d490db30f0da8fa2b8c5b', N'cc1102076eb541338af41222a3954566', N'工时分析报表', N' ', N'工时分析报表', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'68428f0c136d4717b0560004533c576a', N'cc1102076eb541338af41222a3954566', N'制单工序交叉报表', N' ', N'制单工序交叉报表', 2, 2, CAST(5.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ff17d95cc2564590b056a5224c1dd29b', N'cc1102076eb541338af41222a3954566', N'产出明细报表', N' ', N'产出明细报表', 2, 2, CAST(6.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'47f9e8b3a6c14f618979155609d2693b', N'cc1102076eb541338af41222a3954566', N'返工详情报表', N' ', N'返工详情报表', 2, 2, CAST(7.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b546e76e66984f15879c21f9c8d171d7', N'cc1102076eb541338af41222a3954566', N'返工汇总&疵点分析报表', N' ', N'返工汇总&疵点分析报表', 2, 2, CAST(8.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'83437ba19b6b4df1bba712c60fc79f5c', N'cc1102076eb541338af41222a3954566', N'组别竞赛报表', N' ', N'组别竞赛报表', 2, 2, CAST(9.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'23687241c5524bf18afd38c0b8bc7350', N'e182ff1816e8454c8c0c1c9788872478', N'订单信息', N'barBtn_OrderInfo', N'订单信息', 3, 1, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c0775ae3685c4b89a236ae077e86de94', N'23687241c5524bf18afd38c0b8bc7350', N'客户信息', N'OrderInfo_CustomerInfo', N'客户信息', 2, 1, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c1941a976a69489bba7b6b31abb57468', N'23687241c5524bf18afd38c0b8bc7350', N'客户订单', N'OrderInfo_CustomerOrderInfo', N'客户订单', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8f8f1003245b45a1885b96100327fc7a', N'e182ff1816e8454c8c0c1c9788872478', N'制单信息', N'barBtn_billing', N'制单信息', 3, 1, CAST(5.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'09eb98efa6bf405c8381eb59ef23e133', N'8f8f1003245b45a1885b96100327fc7a', N'生产制单', N'Billing_ProcessOrderIndex', N'生产制单', 2, 3, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd6a8b1dc95ad4edda42ee5715aea8454', N'09eb98efa6bf405c8381eb59ef23e133', N'全屏', N'Billing_ProcessOrderIndex.btnMax', N'生产制单.全屏', 3, 4, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8128ddba7f9b4caab41e01704b650ed4', N'09eb98efa6bf405c8381eb59ef23e133', N'刷新', N'Billing_ProcessOrderIndex.btnRefresh', N'生产制单.刷新', 3, 4, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'36ed8ef68aca4ae1b084c07fe80f5122', N'09eb98efa6bf405c8381eb59ef23e133', N'新增', N'Billing_ProcessOrderIndex.btnAdd', N'生产制单.新增', 3, 4, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'63eca0b8ce284dbe9d76b471e39be402', N'09eb98efa6bf405c8381eb59ef23e133', N'退出', N'Billing_ProcessOrderIndex.btnClose', N'生产制单.退出', 3, 4, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e5ad7b144d5b4d93bc70131210f9c80d', N'09eb98efa6bf405c8381eb59ef23e133', N'删除', N'Billing_ProcessOrderIndex.btnDelete', N'生产制单.删除', 3, 4, CAST(5.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'07dbd9c98a8c4700976259aebd51f1a0', N'09eb98efa6bf405c8381eb59ef23e133', N'导出', N'Billing_ProcessOrderIndex.btnExport', N'生产制单.', 3, 4, CAST(6.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'49bd237f9ea64a128b8c0006b1b293ab', N'8f8f1003245b45a1885b96100327fc7a', N'制单工序', N'Billing_ProcessFlowIndex', N'制单工序', 2, 4, CAST(7.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'13442f28057e46758e66e933061acef1', N'8f8f1003245b45a1885b96100327fc7a', N'工艺路线图', N'Billing_ProcessFlowChartIndex', N'工艺路线图', 2, 3, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4de34b96386249b583746896006e53c9', N'e182ff1816e8454c8c0c1c9788872478', N'产品基础数据', N'barBtn_ProductBaseData', N'产品基础数据', 3, 1, CAST(6.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b5d63a5fb0a742ef9813c5aaf08a9a64', N'4de34b96386249b583746896006e53c9', N'产品部位', N'ProductBaseData_ProductPart', N'产品部位', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'939f281e6d914a71b9dc6467a437d2cf', N'4de34b96386249b583746896006e53c9', N'基本尺码表', N'ProductBaseData_BasicSizeTable', N'基本尺码表', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'18007b461ebc40238379444207a108ff', N'4de34b96386249b583746896006e53c9', N'基本颜色表', N'ProductBaseData_BasicColorTable', N'基本颜色表', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c5b833de5ff5497c98ae808857359e1a', N'4de34b96386249b583746896006e53c9', N'款式工艺表', N'ProductBaseData_Style', N'款式工艺表', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7d7ab9d2bab24350985ebae133ebdb65', N'e182ff1816e8454c8c0c1c9788872478', N'工艺基础数据', N'barBtn_ProcessBaseData', N'工艺基础数据', 3, 1, CAST(7.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e459794194e04ddd85f98e730bcdb631', N'7d7ab9d2bab24350985ebae133ebdb65', N'基本工序段', N'ProcessBaseData_BasicProcessSection', N'基本工序段', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'293d219c1a8c4898ad2db88f21235e80', N'7d7ab9d2bab24350985ebae133ebdb65', N'基本工序库', N'ProcessBaseData_BasicProcessLirbary', N'基本工序库', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6b3318ec89614dc2ae68c83f84807b2f', N'7d7ab9d2bab24350985ebae133ebdb65', N'款式工序库', N'ProcessBaseData_StyleProcessLirbary', N'款式工序库', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e6a8cb6a2b424bfda54f87731610835e', N'7d7ab9d2bab24350985ebae133ebdb65', N'疵点代码', N'ProcessBaseData_DefectCode', N'疵点代码', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'01d00c4aa59a4994a36e4cd4d35cb4f7', N'7d7ab9d2bab24350985ebae133ebdb65', N'缺料代码', N'ProcessBaseData_LackOfMaterialCode', N'缺料代码', 2, 2, CAST(5.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'87eb1bc160564b9a896c628732680d31', N'e182ff1816e8454c8c0c1c9788872478', N'生产线', N'barBtn_ProductionLine', N'生产线', 3, 1, CAST(8.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2f7106c8cbf242eea7e3de51c9caa2ca', N'87eb1bc160564b9a896c628732680d31', N'控制端配置', N'ProductionLine_ControlSet', N'控制端配置', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a2733a5772624a968947180a71e51d57', N'87eb1bc160564b9a896c628732680d31', N'流水线管理', N'ProductionLine_PipelineMsg', N'流水线管理', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'3496fe67dc70421cae8741f38a304b2e', N'87eb1bc160564b9a896c628732680d31', N'桥接配置', N'ProductionLine_BridgingSet', N'桥接配置', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c740a2069ffc41bebaff48caeed13d1e', N'87eb1bc160564b9a896c628732680d31', N'客户机信息', N'ProductionLine_ClientInfo', N'客户机信息', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4c0644ead7664875b4ff376a39b093b1', N'87eb1bc160564b9a896c628732680d31', N'系统参数', N'ProductionLine_SystemMsg', N'系统参数', 2, 2, CAST(5.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b5305553b4e44eafa5e227c5335d761d', N'87eb1bc160564b9a896c628732680d31', N'Tcp测试', N'ProductionLine_TcpTest', N'Tcp测试', 2, 2, CAST(6.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e4b0da7a334649bf89827d207ba02bd8', N'e182ff1816e8454c8c0c1c9788872478', N'衣车管理', N'barBtn_ClothingCarManagement', N'衣车管理', 3, 1, CAST(8.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c424c0cd30c2424d97e2e71a3530d85d', N'e4b0da7a334649bf89827d207ba02bd8', N'衣车类别表', N'ClothingCarManagement_EwingMachineType', N'衣车类别表', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f8a5bff164684dbaa201d73d536ff096', N'e4b0da7a334649bf89827d207ba02bd8', N'故障代码表', N'ClothingCarManagement_FalutCodeTable', N'故障代码表', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'572d6c49915c42858fefb0b1c506ce2d', N'e4b0da7a334649bf89827d207ba02bd8', N'衣车资料', N'ClothingCarManagement_SewingMachineData', N'衣车资料', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c55a3e5ce02842a292b918f27a4d12d9', N'e4b0da7a334649bf89827d207ba02bd8', N'机修人员表', N'ClothingCarManagement_MechanicEmployee', N'机修人员表', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'92325ac532a942aea7661f46d9613d76', N'e182ff1816e8454c8c0c1c9788872478', N'人事管理', N'barBtn_PersonnelManagement', N'人事管理', 3, 1, CAST(9.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7957903936964994982d861e193a00cb', N'92325ac532a942aea7661f46d9613d76', N'生产组别', N'PersonnelManagement_ProductGroup', N'生产组别', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2afe51840eb6412b8c38b1059355d880', N'92325ac532a942aea7661f46d9613d76', N'部门信息', N'PersonnelManagement_DepartmentInfo', N'部门信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'35b0cc9481d14947b7f3fc7cab17b503', N'92325ac532a942aea7661f46d9613d76', N'工种信息', N'PersonnelManagement_ProfessionInfo', N'工种信息', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'351a56364eb24668a657bbbd7645244e', N'92325ac532a942aea7661f46d9613d76', N'职务信息', N'PersonnelManagement_PositionInfo', N'职务信息', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'24c91ce5636c4e6788bc9f466343e0ea', N'92325ac532a942aea7661f46d9613d76', N'员工资料', N'PersonnelManagement_EmployeeInfo', N'员工资料', 2, 2, CAST(5.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd86e7b765e284f559ed9ce7e384ea6f7', N'e182ff1816e8454c8c0c1c9788872478', N'裁床管理', N'barBtn_CuttingRoomManage', N'裁床管理', 3, 1, CAST(5.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b52109414ca7459d9af5f3d6dcb31392', N'92325ac532a942aea7661f46d9613d76', N'管理卡信息', N'PersonnelManagement_MsgCardInfo', N'管理卡信息', 2, 2, CAST(6.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'07cbc4f7d9d94645a07959613cdfa961', N'e182ff1816e8454c8c0c1c9788872478', N'权限管理', N'barBtn_AuthorityManagement', N'权限管理', 3, 1, CAST(10.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'89982e179e6f437b826938b617aa31ed', N'07cbc4f7d9d94645a07959613cdfa961', N'菜单管理', N'AuthorityManagement_ModuleMsg', N'菜单管理', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'21f095d8cab34b40973b349e72359c84', N'07cbc4f7d9d94645a07959613cdfa961', N'角色管理', N'AuthorityManagement_RoleMsg', N'角色管理', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2ec84d4822dd4e63a4296302fd1e4e8c', N'07cbc4f7d9d94645a07959613cdfa961', N'用户管理', N'AuthorityManagement_UserMsg', N'用户管理', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f8f8e3e11b6e434f88fb346ba798aeec', N'07cbc4f7d9d94645a07959613cdfa961', N'用户操作日志', N'AuthorityManagement_UserOperatorLog', N'用户操作日志', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9f98aada061f420dbad806fa848d4b73', N'e182ff1816e8454c8c0c1c9788872478', N'考勤管理', N'barBtn_AttendanceManagement', N'考勤管理', 3, 1, CAST(11.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9c68042b03a54117922eb9344b43b165', N'9f98aada061f420dbad806fa848d4b73', N'用户管理', N'AttendanceManagement_HolidayInfo', N'用户管理', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1f2f48cfd25143cfb2befa2777c2f746', N'9f98aada061f420dbad806fa848d4b73', N'班次信息', N'AttendanceManagement_ClasssesInfo', N'班次信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'dc051d83ff0542e9bb8cf2cc848368db', N'9f98aada061f420dbad806fa848d4b73', N'员工排班表', N'AttendanceManagement_EmployeeScheduling', N'员工排班表', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(N'2018-07-19T22:34:31.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Pipelining] ([Id], [SITEGROUP_Id], [PRODTYPE_Id], [PipeliNo], [PushRodNum], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6366850ece4c43d1ba645a98258e26bf', N'9866238500604d0291a888ae4c91368e', NULL, N'1', NULL, NULL, CAST(N'2018-09-01T12:04:17.000' AS DateTime), CAST(N'2018-09-03T09:20:58.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[PoColor] ([Id], [SNo], [ColorValue], [ColorDescption], [Rmark], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2e14ad50aba74756b9a9e47e1c220d33', N'1', N'红色', NULL, NULL, CAST(N'2018-09-01T14:06:53.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PoColor] ([Id], [SNo], [ColorValue], [ColorDescption], [Rmark], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ed58ca8b95a449ffbd5ec79037e34b03', N'2', N'黄色', NULL, NULL, CAST(N'2018-09-01T14:06:53.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PoColor] ([Id], [SNo], [ColorValue], [ColorDescption], [Rmark], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'542bba42f13f40beaf9bb6a85d970a52', N'3', N'蓝色', NULL, NULL, CAST(N'2018-09-01T14:06:53.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PoColor] ([Id], [SNo], [ColorValue], [ColorDescption], [Rmark], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f4475aa116ec4cd2bd25f714dbfdb114', N'4', N'白色', NULL, NULL, CAST(N'2018-09-01T14:06:53.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlow] ([Id], [PROCESSFLOWVERSION_Id], [BASICPROCESSFLOW_Id], [ProcessNo], [ProcessOrderField], [ProcessName], [ProcessCode], [ProcessStatus], [StanardSecond], [StanardMinute], [StanardHours], [StandardPrice], [DefaultFlowNo], [PrcocessRemark], [ProcessColor], [EffectiveDate], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cd464b01b29b4f779d88bc6e74eecdcc', N'd4c4a4179e3445e9984cb476f13981c2', N'c8e09f67adb84d5595263b25bff0e08f', NULL, NULL, N'挂片                                                                                                                                                                                                    ', N'1                   ', NULL, 10, CAST(0.1667 AS Decimal(8, 4)), N'0.1667', CAST(0.2000 AS Decimal(8, 4)), 1, NULL, NULL, NULL, NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlow] ([Id], [PROCESSFLOWVERSION_Id], [BASICPROCESSFLOW_Id], [ProcessNo], [ProcessOrderField], [ProcessName], [ProcessCode], [ProcessStatus], [StanardSecond], [StanardMinute], [StanardHours], [StandardPrice], [DefaultFlowNo], [PrcocessRemark], [ProcessColor], [EffectiveDate], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a72a4e68ab13451f92199bb63a074806', N'd4c4a4179e3445e9984cb476f13981c2', N'a497537b7ce2414e822deafed83b6c8e', NULL, NULL, N'合肩                                                                                                                                                                                                    ', N'2                   ', NULL, 12, CAST(0.2000 AS Decimal(8, 4)), N'0.2', CAST(0.3000 AS Decimal(8, 4)), 2, NULL, NULL, NULL, NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlow] ([Id], [PROCESSFLOWVERSION_Id], [BASICPROCESSFLOW_Id], [ProcessNo], [ProcessOrderField], [ProcessName], [ProcessCode], [ProcessStatus], [StanardSecond], [StanardMinute], [StanardHours], [StandardPrice], [DefaultFlowNo], [PrcocessRemark], [ProcessColor], [EffectiveDate], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'3dcaab7fb94c49649fece7416e5bc514', N'd4c4a4179e3445e9984cb476f13981c2', N'ae9ecdb904b442f284daca3591def0e6', NULL, NULL, N'上袖                                                                                                                                                                                                    ', N'3                   ', NULL, 12, CAST(0.2000 AS Decimal(8, 4)), N'0.2', CAST(0.3000 AS Decimal(8, 4)), 3, NULL, NULL, NULL, NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlow] ([Id], [PROCESSFLOWVERSION_Id], [BASICPROCESSFLOW_Id], [ProcessNo], [ProcessOrderField], [ProcessName], [ProcessCode], [ProcessStatus], [StanardSecond], [StanardMinute], [StanardHours], [StandardPrice], [DefaultFlowNo], [PrcocessRemark], [ProcessColor], [EffectiveDate], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ebd6eb439192413cb887a22a035f59c4', N'd4c4a4179e3445e9984cb476f13981c2', N'414c396207704e679ca74043e2ae4fd6', NULL, NULL, N'上领                                                                                                                                                                                                    ', N'4                   ', NULL, 15, CAST(0.2500 AS Decimal(8, 4)), N'0.25', CAST(0.5000 AS Decimal(8, 4)), 4, NULL, NULL, NULL, NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlow] ([Id], [PROCESSFLOWVERSION_Id], [BASICPROCESSFLOW_Id], [ProcessNo], [ProcessOrderField], [ProcessName], [ProcessCode], [ProcessStatus], [StanardSecond], [StanardMinute], [StanardHours], [StandardPrice], [DefaultFlowNo], [PrcocessRemark], [ProcessColor], [EffectiveDate], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1d05e96153344bbba8b5cfaffa763155', N'd4c4a4179e3445e9984cb476f13981c2', N'd0a6bddbd6ba497c9b6d4f43431f6273', NULL, NULL, N'合侧边                                                                                                                                                                                                  ', N'5                   ', NULL, 20, CAST(0.3333 AS Decimal(8, 4)), N'0.3333', CAST(0.3000 AS Decimal(8, 4)), 5, NULL, NULL, NULL, NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlow] ([Id], [PROCESSFLOWVERSION_Id], [BASICPROCESSFLOW_Id], [ProcessNo], [ProcessOrderField], [ProcessName], [ProcessCode], [ProcessStatus], [StanardSecond], [StanardMinute], [StanardHours], [StandardPrice], [DefaultFlowNo], [PrcocessRemark], [ProcessColor], [EffectiveDate], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd1afe4b5f9764117909a29c32e13363f', N'd4c4a4179e3445e9984cb476f13981c2', N'3eee87b6f6d14f13a7ab7fb88da745a8', NULL, NULL, N'坎下摆                                                                                                                                                                                                  ', N'6                   ', NULL, 12, CAST(0.2000 AS Decimal(8, 4)), N'0.2', CAST(0.4000 AS Decimal(8, 4)), 6, NULL, NULL, NULL, NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlow] ([Id], [PROCESSFLOWVERSION_Id], [BASICPROCESSFLOW_Id], [ProcessNo], [ProcessOrderField], [ProcessName], [ProcessCode], [ProcessStatus], [StanardSecond], [StanardMinute], [StanardHours], [StandardPrice], [DefaultFlowNo], [PrcocessRemark], [ProcessColor], [EffectiveDate], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'41e0fc941aee422a886f7d9caf9856f2', N'd4c4a4179e3445e9984cb476f13981c2', N'12daee36505a472882a8bb33859bc509', NULL, NULL, N'QC                                                                                                                                                                                                      ', N'7                   ', NULL, 12, CAST(0.2000 AS Decimal(8, 4)), N'0.2', CAST(0.4000 AS Decimal(8, 4)), 7, NULL, NULL, NULL, NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowChart] ([Id], [PROCESSFLOWVERSION_Id], [LinkName], [pFlowChartNum], [ProductPosition], [TargetNum], [OutputProcessFlowId], [BoltProcessFlowId], [Remark], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'39b76b82c50c4cb8a191d0be5a240ac6', N'd4c4a4179e3445e9984cb476f13981c2', N'MSD001_MSD001_180901-1_180901-1                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     ', 1, NULL, NULL, NULL, NULL, N'', NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowChartFlowRelation] ([Id], [PROCESSFLOW_Id], [PROCESSFLOWCHART_Id], [CraftFlowNo], [IsEnabled], [EnabledText], [IsMergeForward], [MergeForwardText], [FlowNo], [FlowCode], [FlowName], [IsProduceFlow], [MergeProcessFlowChartFlowRelationId], [MergeFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4ee93c532a4c4445a5a053d7858d5a29', N'cd464b01b29b4f779d88bc6e74eecdcc', N'39b76b82c50c4cb8a191d0be5a240ac6', N'1', 1, NULL, 0, NULL, N'1', N'1                   ', N'挂片                                    ', NULL, NULL, NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowChartFlowRelation] ([Id], [PROCESSFLOW_Id], [PROCESSFLOWCHART_Id], [CraftFlowNo], [IsEnabled], [EnabledText], [IsMergeForward], [MergeForwardText], [FlowNo], [FlowCode], [FlowName], [IsProduceFlow], [MergeProcessFlowChartFlowRelationId], [MergeFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b1cb4283c98c4296a66e3aa0c11e8752', N'a72a4e68ab13451f92199bb63a074806', N'39b76b82c50c4cb8a191d0be5a240ac6', N'2', 1, NULL, 0, NULL, N'2', N'2                   ', N'合肩                                    ', NULL, NULL, NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowChartFlowRelation] ([Id], [PROCESSFLOW_Id], [PROCESSFLOWCHART_Id], [CraftFlowNo], [IsEnabled], [EnabledText], [IsMergeForward], [MergeForwardText], [FlowNo], [FlowCode], [FlowName], [IsProduceFlow], [MergeProcessFlowChartFlowRelationId], [MergeFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'0145ebe7475b41fc8e8fcc0e06724cbe', N'3dcaab7fb94c49649fece7416e5bc514', N'39b76b82c50c4cb8a191d0be5a240ac6', N'3', 1, NULL, 1, NULL, N'3', N'3                   ', N'上袖                                    ', NULL, N'b1cb4283c98c4296a66e3aa0c11e8752', N'2', CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowChartFlowRelation] ([Id], [PROCESSFLOW_Id], [PROCESSFLOWCHART_Id], [CraftFlowNo], [IsEnabled], [EnabledText], [IsMergeForward], [MergeForwardText], [FlowNo], [FlowCode], [FlowName], [IsProduceFlow], [MergeProcessFlowChartFlowRelationId], [MergeFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7b2b3d4dbfe44bcbb6421e53949ae169', N'ebd6eb439192413cb887a22a035f59c4', N'39b76b82c50c4cb8a191d0be5a240ac6', N'4', 1, NULL, 0, NULL, N'4', N'4                   ', N'上领                                    ', NULL, NULL, NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowChartFlowRelation] ([Id], [PROCESSFLOW_Id], [PROCESSFLOWCHART_Id], [CraftFlowNo], [IsEnabled], [EnabledText], [IsMergeForward], [MergeForwardText], [FlowNo], [FlowCode], [FlowName], [IsProduceFlow], [MergeProcessFlowChartFlowRelationId], [MergeFlowNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7c9ab383bbe2478ea023b03d3bde7508', N'1d05e96153344bbba8b5cfaffa763155', N'39b76b82c50c4cb8a191d0be5a240ac6', N'5', 1, NULL, 0, NULL, N'5', N'5                   ', N'合侧边                                  ', NULL, NULL, NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowChartGrop] ([Id], [SITEGROUP_Id], [PROCESSFLOWCHART_Id], [GroupNo], [GroupName]) VALUES (N'00d6e05641f543bb9850588aeb50a872', N'9866238500604d0291a888ae4c91368e', N'39b76b82c50c4cb8a191d0be5a240ac6', N'9866238500604d0291a888ae4c91368e', N'A')
GO
INSERT [dbo].[ProcessFlowStatingItem] ([Id], [PROCESSFLOWCHARTFLOWRELATION_Id], [STATING_Id], [IsReceivingHanger], [ReceingContent], [SiteGroupNo], [mainTrackNumber], [No], [StatingRoleName], [Memo], [IsReceivingAllSize], [IsReceivingAllColor], [isReceivingAllPONumber], [IsEndStating], [Proportion], [ReceivingColor], [ReceivingSize], [ReceivingPONumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cff72cedd64349abb55e14c36ebb315c', N'b1cb4283c98c4296a66e3aa0c11e8752', N'137f345ff80542159a87b54c23ca186b', 1, NULL, N'A', 1, N'3', N'车缝站', NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(3, 2)), NULL, NULL, NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowStatingItem] ([Id], [PROCESSFLOWCHARTFLOWRELATION_Id], [STATING_Id], [IsReceivingHanger], [ReceingContent], [SiteGroupNo], [mainTrackNumber], [No], [StatingRoleName], [Memo], [IsReceivingAllSize], [IsReceivingAllColor], [isReceivingAllPONumber], [IsEndStating], [Proportion], [ReceivingColor], [ReceivingSize], [ReceivingPONumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8dd2126be0bd41cab20472c7ddc01f62', N'b1cb4283c98c4296a66e3aa0c11e8752', N'bb6b92b9432642d5a99b91619720190d', 1, NULL, N'A', 1, N'4', N'车缝站', NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(3, 2)), NULL, NULL, NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowStatingItem] ([Id], [PROCESSFLOWCHARTFLOWRELATION_Id], [STATING_Id], [IsReceivingHanger], [ReceingContent], [SiteGroupNo], [mainTrackNumber], [No], [StatingRoleName], [Memo], [IsReceivingAllSize], [IsReceivingAllColor], [isReceivingAllPONumber], [IsEndStating], [Proportion], [ReceivingColor], [ReceivingSize], [ReceivingPONumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ee231678e5df4ab2bf92d758fc68303b', N'7b2b3d4dbfe44bcbb6421e53949ae169', N'e29b30ff82c2469ca3de0c112ecfac5e', 1, NULL, N'A', 1, N'5', N'车缝站', NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(3, 2)), NULL, NULL, NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowStatingItem] ([Id], [PROCESSFLOWCHARTFLOWRELATION_Id], [STATING_Id], [IsReceivingHanger], [ReceingContent], [SiteGroupNo], [mainTrackNumber], [No], [StatingRoleName], [Memo], [IsReceivingAllSize], [IsReceivingAllColor], [isReceivingAllPONumber], [IsEndStating], [Proportion], [ReceivingColor], [ReceivingSize], [ReceivingPONumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4a9ac59f3dab4875a25886c4692cd423', N'7c9ab383bbe2478ea023b03d3bde7508', N'03dcedbd691d4562af6433bedc76b02c', 1, NULL, N'A', 1, N'6', N'QC站', NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(3, 2)), NULL, NULL, NULL, CAST(N'2018-09-03T12:04:42.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessFlowVersion] ([Id], [PROCESSORDER_Id], [ProVersionNum], [ProVersionNo], [ProcessVersionName], [EffectiveDate], [TotalStandardPrice], [TotalSAM], [UpdateDateTime], [InsertDate], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd4c4a4179e3445e9984cb476f13981c2', N'd9e3e36cbb6b421db9da6d4beee578be', N'1', N'MSD001_180901-1                                   ', NULL, CAST(N'2018-09-01T14:08:29.000' AS DateTime), N'2.4000                          ', N'1.5500                          ', NULL, NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrder] ([Id], [STYLE_Id], [CUSTOMER_Id], [POrderNo], [POrderNum], [MOrderNo], [POrderType], [POrderTypeDesption], [ProductNoticeOrderNo], [Num], [Status], [StyleCode], [StyleName], [CustomerNO], [CustomerName], [CustomerStyle], [CustOrderNo], [CustPurchaseOrderNo], [DeliveryDate], [GenaterOrderDate], [OrderNo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd9e3e36cbb6b421db9da6d4beee578be', NULL, NULL, N'MSD001                                                                                                                                                                                                  ', 1, NULL, 2, N'合成类型制单                                      ', N'                    ', NULL, 0, N'                    ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorItem] ([Id], [PROCESSORDER_Id], [POCOLOR_Id], [CUSTOMERPURCHASEORDER_Id], [MOrderItemNo], [Color], [ColorDescription], [Total], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5a81daac0ec243c99f6d2ae21c705598', N'd9e3e36cbb6b421db9da6d4beee578be', N'2e14ad50aba74756b9a9e47e1c220d33', N'464911a4bf004fa0b4439ce27dcbfe5e', NULL, N'红色', NULL, 300, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorItem] ([Id], [PROCESSORDER_Id], [POCOLOR_Id], [CUSTOMERPURCHASEORDER_Id], [MOrderItemNo], [Color], [ColorDescription], [Total], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e4136978e8b745d28bf9c1ab47db80f8', N'd9e3e36cbb6b421db9da6d4beee578be', N'ed58ca8b95a449ffbd5ec79037e34b03', N'464911a4bf004fa0b4439ce27dcbfe5e', NULL, N'黄色', NULL, 300, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorItem] ([Id], [PROCESSORDER_Id], [POCOLOR_Id], [CUSTOMERPURCHASEORDER_Id], [MOrderItemNo], [Color], [ColorDescription], [Total], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b28038847c8048e880db5c8c8452e5a5', N'd9e3e36cbb6b421db9da6d4beee578be', N'542bba42f13f40beaf9bb6a85d970a52', N'464911a4bf004fa0b4439ce27dcbfe5e', NULL, N'蓝色', NULL, 300, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorItem] ([Id], [PROCESSORDER_Id], [POCOLOR_Id], [CUSTOMERPURCHASEORDER_Id], [MOrderItemNo], [Color], [ColorDescription], [Total], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'668a03ae95d146eebca11fb61a645fcc', N'd9e3e36cbb6b421db9da6d4beee578be', N'f4475aa116ec4cd2bd25f714dbfdb114', N'464911a4bf004fa0b4439ce27dcbfe5e', NULL, N'白色', NULL, 300, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f286ef76445543298cc5a26c0494bdfc', N'5a81daac0ec243c99f6d2ae21c705598', N'1e4a303a56664a6a9aae2451dba8252c', N'S', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e48bc5466b4943bf8229df708c40de87', N'5a81daac0ec243c99f6d2ae21c705598', N'877ee22e6d7243e6b01479b5cbd2b761', N'M', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'25f794fbb3034eb2bbaef1a907efdbe6', N'5a81daac0ec243c99f6d2ae21c705598', N'786fe8fa602b4dd385eb954bf1af4867', N'L', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'151d35326fa8463abc001c009fca590d', N'e4136978e8b745d28bf9c1ab47db80f8', N'1e4a303a56664a6a9aae2451dba8252c', N'S', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c79ef3736ecb40429dd58d51462d5fd7', N'e4136978e8b745d28bf9c1ab47db80f8', N'877ee22e6d7243e6b01479b5cbd2b761', N'M', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'86529a252d034a2e94cbb6ee51ebd63a', N'e4136978e8b745d28bf9c1ab47db80f8', N'786fe8fa602b4dd385eb954bf1af4867', N'L', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6c0e304864944b75b9b3c7dedcf5fc6a', N'b28038847c8048e880db5c8c8452e5a5', N'1e4a303a56664a6a9aae2451dba8252c', N'S', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8b8dae855bbd4155b44e79bc13b85b56', N'b28038847c8048e880db5c8c8452e5a5', N'877ee22e6d7243e6b01479b5cbd2b761', N'M', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7e0ce5bf59ea499dbb963ab74a261522', N'b28038847c8048e880db5c8c8452e5a5', N'786fe8fa602b4dd385eb954bf1af4867', N'L', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8ffc026a148f42ee9066c786779a020b', N'668a03ae95d146eebca11fb61a645fcc', N'1e4a303a56664a6a9aae2451dba8252c', N'S', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a686d7aa798644bd9a93476e81212279', N'668a03ae95d146eebca11fb61a645fcc', N'877ee22e6d7243e6b01479b5cbd2b761', N'M', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[ProcessOrderColorSizeItem] ([Id], [PROCESSORDERCOLORITEM_Id], [PSIZE_Id], [SizeDesption], [Total], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7e4ee1c254ca446ba73f40317e95b2e5', N'668a03ae95d146eebca11fb61a645fcc', N'786fe8fa602b4dd385eb954bf1af4867', N'L', N'100', NULL, CAST(N'2018-09-01T14:08:21.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Products] ([Id], [PROCESSFLOWCHART_Id], [PROCESSORDER_Id], [GroupNo], [ProductionNumber], [ImplementDate], [HangingPieceSiteNo], [ProcessOrderNo], [Status], [CustomerPurchaseOrderId], [OrderNo], [StyleNo], [PColor], [PO], [PSize], [LineName], [FlowSection], [Unit], [TaskNum], [OnlineNum], [TodayHangingPieceSiteNum], [TodayProdOutNum], [TotalProdOutNum], [TodayBindCard], [TodayRework], [TotalHangingPieceSiteNum], [TotalRework], [TotalBindNum], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'acbf8501354843119780f00cbcfc2455', N'39b76b82c50c4cb8a191d0be5a240ac6', N'd9e3e36cbb6b421db9da6d4beee578be', N'A', 1, NULL, N'1', N'MSD001', 2, NULL, N'US01', N'                    ', N'黄色', N'US01', N'L', N'MSD001_MSD001_180901-1_180901-1', N'', N'1', 12, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:02:10.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PSize] ([Id], [PSNo], [Size], [SizeDesption], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1e4a303a56664a6a9aae2451dba8252c', N'1', N'S', NULL, NULL, CAST(N'2018-09-01T14:06:10.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PSize] ([Id], [PSNo], [Size], [SizeDesption], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'877ee22e6d7243e6b01479b5cbd2b761', N'2', N'M', NULL, NULL, CAST(N'2018-09-01T14:06:10.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PSize] ([Id], [PSNo], [Size], [SizeDesption], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'786fe8fa602b4dd385eb954bf1af4867', N'3', N'L', NULL, NULL, CAST(N'2018-09-01T14:06:10.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PSize] ([Id], [PSNo], [Size], [SizeDesption], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'28712747f9354021a084d21c5a26d08b', N'4', N'XL', NULL, NULL, CAST(N'2018-09-01T14:06:10.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PSize] ([Id], [PSNo], [Size], [SizeDesption], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2817cf489666422bb07012d3d24a85d0', N'5', N'XXL', NULL, NULL, CAST(N'2018-09-01T14:06:10.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[PSize] ([Id], [PSNo], [Size], [SizeDesption], [Memo], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c1496e0d05d84353bc935b04bc015897', N'6', N'XXXL', NULL, NULL, CAST(N'2018-09-01T14:06:10.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Roles] ([Id], [ActionName], [Description], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'3036bd41118e4b71b82fe208e8dd70ff', N'SuperAdmin', N'超级管理员', CAST(N'2018-07-10T21:58:44.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Roles] ([Id], [ActionName], [Description], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a9430646e41e45eca92a27d831af31c6', N'Admin', N'管理员', CAST(N'2018-07-10T21:58:44.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SiteGroup] ([Id], [WORKSHOP_Id], [GroupNO], [GroupName], [FactoryCode], [WorkshopCode], [MainTrackNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9866238500604d0291a888ae4c91368e', NULL, N'A', NULL, N'MSD', N'CF', 1, CAST(N'2018-09-01T12:03:10.000' AS DateTime), CAST(N'2018-09-03T16:01:11.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[SiteGroup] ([Id], [WORKSHOP_Id], [GroupNO], [GroupName], [FactoryCode], [WorkshopCode], [MainTrackNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'0e554b79e19b447d8963b5709d26c6ca', NULL, N'B', N'B', N'MSD', N'CF', 2, CAST(N'2018-09-03T15:46:46.000' AS DateTime), CAST(N'2018-09-03T16:01:11.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[SiteGroup] ([Id], [WORKSHOP_Id], [GroupNO], [GroupName], [FactoryCode], [WorkshopCode], [MainTrackNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1d0cc263419f4644806a32291f25db09', NULL, N'C', NULL, N'MSD ', N'CF', 3, CAST(N'2018-09-03T16:01:11.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[Stating] ([Id], [SUSLANGUAGE_Id], [STATINGROLES_Id], [SITEGROUP_Id], [STATINGDIRECTION_Id], [StatingName], [StatingNo], [Language], [MainTrackNumber], [Capacity], [IsReceivingHanger], [ColorValue], [IsLoadMonitor], [IsChainHoist], [IsPromoteTripCachingFull], [SiteBarCode], [IsEnabled], [Direction], [Memo], [MainboardNumber], [SerialNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'35d48e401bcc404191fb25434de12aff', NULL, N'8c26c9ec5e7d43dd9e566eea3202f4d6', N'9866238500604d0291a888ae4c91368e', N'a5f2bbbdb4ad473a978564d068b538c4', N'挂片站              ', N'1                   ', NULL, 1, 100, 1, N'104                 ', 1, 0, 0, NULL, 1, 1, NULL, N'123', N'-1', CAST(N'2018-09-01T12:04:17.000' AS DateTime), CAST(N'2018-09-03T09:20:58.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[Stating] ([Id], [SUSLANGUAGE_Id], [STATINGROLES_Id], [SITEGROUP_Id], [STATINGDIRECTION_Id], [StatingName], [StatingNo], [Language], [MainTrackNumber], [Capacity], [IsReceivingHanger], [ColorValue], [IsLoadMonitor], [IsChainHoist], [IsPromoteTripCachingFull], [SiteBarCode], [IsEnabled], [Direction], [Memo], [MainboardNumber], [SerialNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'298282219cd1416092a1492510aec927', NULL, N'ec285428a97a4c36b4144983901ae92c', N'9866238500604d0291a888ae4c91368e', N'a5f2bbbdb4ad473a978564d068b538c4', N'车缝站              ', N'2                   ', NULL, 1, 20, 1, N'100                 ', 1, 0, 0, NULL, 1, 1, NULL, N'123', N'-1', CAST(N'2018-09-01T12:04:17.000' AS DateTime), CAST(N'2018-09-01T21:26:28.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[Stating] ([Id], [SUSLANGUAGE_Id], [STATINGROLES_Id], [SITEGROUP_Id], [STATINGDIRECTION_Id], [StatingName], [StatingNo], [Language], [MainTrackNumber], [Capacity], [IsReceivingHanger], [ColorValue], [IsLoadMonitor], [IsChainHoist], [IsPromoteTripCachingFull], [SiteBarCode], [IsEnabled], [Direction], [Memo], [MainboardNumber], [SerialNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'137f345ff80542159a87b54c23ca186b', NULL, N'ec285428a97a4c36b4144983901ae92c', N'9866238500604d0291a888ae4c91368e', N'a5f2bbbdb4ad473a978564d068b538c4', N'车缝站              ', N'3                   ', NULL, 1, 20, 1, N'100                 ', 1, 0, 0, NULL, 1, 1, NULL, N'123', N'-1', CAST(N'2018-09-01T12:04:17.000' AS DateTime), CAST(N'2018-09-01T21:26:28.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[Stating] ([Id], [SUSLANGUAGE_Id], [STATINGROLES_Id], [SITEGROUP_Id], [STATINGDIRECTION_Id], [StatingName], [StatingNo], [Language], [MainTrackNumber], [Capacity], [IsReceivingHanger], [ColorValue], [IsLoadMonitor], [IsChainHoist], [IsPromoteTripCachingFull], [SiteBarCode], [IsEnabled], [Direction], [Memo], [MainboardNumber], [SerialNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bb6b92b9432642d5a99b91619720190d', NULL, N'ec285428a97a4c36b4144983901ae92c', N'9866238500604d0291a888ae4c91368e', N'a5f2bbbdb4ad473a978564d068b538c4', N'车缝站              ', N'4                   ', NULL, 1, 20, 1, N'100                 ', 0, 0, 0, NULL, 1, 1, NULL, N'123', N'-1', CAST(N'2018-09-01T12:04:17.000' AS DateTime), CAST(N'2018-09-01T18:00:13.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[Stating] ([Id], [SUSLANGUAGE_Id], [STATINGROLES_Id], [SITEGROUP_Id], [STATINGDIRECTION_Id], [StatingName], [StatingNo], [Language], [MainTrackNumber], [Capacity], [IsReceivingHanger], [ColorValue], [IsLoadMonitor], [IsChainHoist], [IsPromoteTripCachingFull], [SiteBarCode], [IsEnabled], [Direction], [Memo], [MainboardNumber], [SerialNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e29b30ff82c2469ca3de0c112ecfac5e', NULL, N'ec285428a97a4c36b4144983901ae92c', N'9866238500604d0291a888ae4c91368e', N'a5f2bbbdb4ad473a978564d068b538c4', N'车缝站              ', N'5                   ', NULL, 1, 20, 1, N'100                 ', 0, 0, 0, NULL, 1, 1, NULL, N'123', N'-1', CAST(N'2018-09-01T12:04:17.000' AS DateTime), CAST(N'2018-09-01T18:00:13.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[Stating] ([Id], [SUSLANGUAGE_Id], [STATINGROLES_Id], [SITEGROUP_Id], [STATINGDIRECTION_Id], [StatingName], [StatingNo], [Language], [MainTrackNumber], [Capacity], [IsReceivingHanger], [ColorValue], [IsLoadMonitor], [IsChainHoist], [IsPromoteTripCachingFull], [SiteBarCode], [IsEnabled], [Direction], [Memo], [MainboardNumber], [SerialNumber], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'03dcedbd691d4562af6433bedc76b02c', NULL, N'ced2c9b317b5485a8fb46341ee1c8aa4', N'9866238500604d0291a888ae4c91368e', N'a5f2bbbdb4ad473a978564d068b538c4', N'QC站                ', N'6                   ', NULL, 1, 20, 1, N'106                 ', 1, 0, 0, NULL, 1, 1, NULL, N'123', N'-1', CAST(N'2018-09-01T12:04:17.000' AS DateTime), CAST(N'2018-09-01T18:41:13.000' AS DateTime), N'f57ad5187c3e45dbad37e7cef9b4f90f', N'f57ad5187c3e45dbad37e7cef9b4f90f', 0, N'c001                            ')
GO
INSERT [dbo].[StatingDirection] ([Id], [DirectionKey], [DirectionDesc], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a5f2bbbdb4ad473a978564d068b538c4', N'1', N'出站,入站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingDirection] ([Id], [DirectionKey], [DirectionDesc], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'33448d44089341069c68af0b6506dcf0', N'2', N'入站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingDirection] ([Id], [DirectionKey], [DirectionDesc], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'eb049a1ff00740159581c2a7ea7bbe10', N'2', N'出战', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingHangerProductItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [SiteNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [IsSucessedFlow], [SiteId], [IsFlowChatChange], [Memo], [IsIncomeSite], [IsReturnWorkFlow], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [IncomeSiteDate], [OutSiteDate], [CompareDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'85c782d91dd34105b1916c58095094ac', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'3', N'5825880', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', NULL, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', 2, 1, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, CAST(N'2018-09-03T16:18:46.000' AS DateTime), CAST(N'2018-09-03T16:23:12.000' AS DateTime), CAST(N'2018-09-03T16:23:07.000' AS DateTime), CAST(N'2018-09-03T16:23:12.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingHangerProductItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [SiteNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [IsSucessedFlow], [SiteId], [IsFlowChatChange], [Memo], [IsIncomeSite], [IsReturnWorkFlow], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [IncomeSiteDate], [OutSiteDate], [CompareDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5db25a433deb4911a1702d4549d01fa5', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'4', N'5803209', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', NULL, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', 2, 1, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, CAST(N'2018-09-03T16:19:02.000' AS DateTime), CAST(N'2018-09-03T16:48:14.000' AS DateTime), CAST(N'2018-09-03T16:48:09.000' AS DateTime), CAST(N'2018-09-03T16:48:14.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingRoles] ([Id], [RoleCode], [RoleName], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ec285428a97a4c36b4144983901ae92c', N'100', N'车缝站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingRoles] ([Id], [RoleCode], [RoleName], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cac0b62e68694e16b0b814bb8319498c', N'101', N'多功能站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingRoles] ([Id], [RoleCode], [RoleName], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'30e54a7cbe28482e87c6052ab22a5970', N'102', N'储存站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingRoles] ([Id], [RoleCode], [RoleName], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'48bee37c24db45f3af42172232ec7d12', N'103', N'专用返工站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingRoles] ([Id], [RoleCode], [RoleName], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8c26c9ec5e7d43dd9e566eea3202f4d6', N'104', N'挂片站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingRoles] ([Id], [RoleCode], [RoleName], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ecbf58cdf84b4f83a529aa5e63cd7fd9', N'105', N'超载站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingRoles] ([Id], [RoleCode], [RoleName], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ced2c9b317b5485a8fb46341ee1c8aa4', N'106', N'QC站', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[StatingRoles] ([Id], [RoleCode], [RoleName], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'71f23f0332bf443196a6107afe218b46', N'107', N'桥接专用', CAST(N'2018-07-10T21:58:44.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4c0aaab57c144c0f83815f9fc5cfa8e3', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5806752', 0, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 2, N'a72a4e68ab13451f92199bb63a074806', N'2', N'2                   ', N'合肩                                    ', N'137f345ff80542159a87b54c23ca186b', 3, 20, 5, 3, 2, 0, 1, 0, NULL, N'黄色', N'L', N'中缝', N'3', NULL, CAST(N'2018-09-03T16:24:41.167' AS DateTime), CAST(N'2018-09-03T16:24:46.223' AS DateTime), CAST(N'2018-09-03T16:31:14.990' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'15144a08a4d04722aad77bf6643855cc', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5806752', 0, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 3, N'3dcaab7fb94c49649fece7416e5bc514', N'3', N'3                   ', N'上袖                                    ', N'137f345ff80542159a87b54c23ca186b', 3, 0, NULL, NULL, 2, 0, 1, 0, NULL, N'黄色', N'L', N'中缝', N'3', NULL, CAST(N'2018-09-03T16:24:41.167' AS DateTime), CAST(N'2018-09-03T16:24:46.223' AS DateTime), CAST(N'2018-09-03T16:31:15.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c4d0308f2a324e729fe26b3db64d04c1', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5806752', NULL, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 1, NULL, N'1', N'1                   ', N'挂片                                    ', NULL, 1, 100, NULL, NULL, 2, 2, 0, NULL, NULL, N'黄色', N'L', N'飞马', N'1', NULL, CAST(N'2018-09-03T16:19:14.280' AS DateTime), CAST(N'2018-09-03T16:24:11.733' AS DateTime), CAST(N'2018-09-03T16:31:15.000' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerProductFlowChart] ([Id], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [IsHangerSucess], [PO], [ProcessOrderNo], [ProcessChartId], [FlowIndex], [FlowId], [FlowNo], [FlowCode], [FlowName], [StatingId], [StatingNo], [StatingCapacity], [NextStatingNo], [FlowRealyProductStatingNo], [Status], [FlowType], [IsFlowSucess], [IsReworkSourceStating], [DefectCode], [PColor], [PSize], [EmployeeName], [CardNo], [IncomeSiteDate], [CompareDate], [OutSiteDate], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'390a6432ae834cfd91014db62d493176', 1, N'acbf8501354843119780f00cbcfc2455', 1, N'-5806752', NULL, NULL, N'MSD001', N'39b76b82c50c4cb8a191d0be5a240ac6', 1, NULL, N'1', N'1                   ', N'挂片                                    ', NULL, 1, 100, NULL, NULL, 2, 2, 0, NULL, NULL, N'黄色', N'L', N'飞马', N'1', NULL, CAST(N'2018-09-03T16:19:14.280' AS DateTime), CAST(N'2018-09-03T16:31:15.050' AS DateTime), CAST(N'2018-09-03T16:31:19.427' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2d5da1f86c604a08ad0d969e429dfe38', NULL, 1, N'acbf8501354843119780f00cbcfc2455', NULL, N'5806752', NULL, N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', NULL, NULL, N'ebd6eb439192413cb887a22a035f59c4', N'4', N'4                   ', N'上领                                    ', 4, NULL, N'3', N'5', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:24:46.227' AS DateTime), NULL, NULL, NULL, 0, 0, CAST(N'2018-09-03T16:31:15.023' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ac4cb617b64e494698b0568a3389b61e', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'4', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:24:11.000' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:15.033' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'af7c47d09fd54c65b5d866678785a399', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'1', N'挂片', 0, NULL, N'1', NULL, 0, NULL, NULL, NULL, NULL, 1, N'-1', NULL, NULL, NULL, CAST(N'2018-09-03T16:24:11.727' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:15.037' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a7c8089ba4ba4c06bdb129f59c963a82', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'4', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:24:11.727' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:15.037' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9fb1ca56786b4dabb5a43abf4e1a2d78', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'-5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'1', N'挂片', 0, NULL, N'1', NULL, 0, NULL, NULL, NULL, NULL, 1, N'-1', NULL, NULL, NULL, CAST(N'2018-09-03T16:31:15.043' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:19.433' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'747cb723831b43dc89801241f582cebc', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'-5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'3', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:31:15.043' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:19.433' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SuccessHangerStatingAllocationItem] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [ProcessFlowId], [FlowNo], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [NextSiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSucessedFlow], [IsReturnWorkFlow], [returnWorkSiteNo], [HSAINdex], [Memo], [IsReworkSourceStating], [ClientMachineId], [SusLineId], [AllocatingStatingDate], [IncomeSiteDate], [CompareDate], [OutSiteDate], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'5693661d16484f5982397a8aa562176c', NULL, 1, N'acbf8501354843119780f00cbcfc2455', 1, N'-5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001                                                                                                                                                                                                  ', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'cd464b01b29b4f779d88bc6e74eecdcc', N'2', N'2                   ', N'合肩                                    ', 2, NULL, N'1', N'3', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:31:15.000' AS DateTime), NULL, NULL, NULL, NULL, 0, CAST(N'2018-09-03T16:31:19.433' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[SucessProcessOrderHanger] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [PO], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [FlowNo], [ProcessFlowId], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [IsFlowChatChange], [IsSemiFinishedProducts], [SFClearDate], [Memo], [ClientMachineId], [SusLineId], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId], [IsIncomeSite]) VALUES (N'7c244426c26b47c396d080689e2afb21', NULL, 1, N'acbf8501354843119780f00cbcfc2455', N'US01', 1, N'-5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'1', N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'挂片', 0, NULL, N'1', 0, NULL, NULL, N'半成品衣架', NULL, NULL, CAST(N'2018-09-03T16:31:15.007' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ', NULL)
GO
INSERT [dbo].[SucessProcessOrderHanger] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [PO], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [SizeNum], [FlowNo], [ProcessFlowId], [ProcessFlowCode], [ProcessFlowName], [FlowIndex], [SiteId], [SiteNo], [IsFlowChatChange], [IsSemiFinishedProducts], [SFClearDate], [Memo], [ClientMachineId], [SusLineId], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId], [IsIncomeSite]) VALUES (N'46a8fe9e7bd141eab1c04963a2309276', NULL, 1, N'acbf8501354843119780f00cbcfc2455', N'US01', 1, N'-5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 1, N'1', N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'挂片', 0, NULL, N'1', 0, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:31:19.430' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ', NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'31a1410130aa4ff8a2bc5d3459f299a5', NULL, N'0001', 0, N'用户参数', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'3ad6dce7aa464713a6c2eaea09d1244a', NULL, N'0002', 1, N'客户机参数', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'4ca3c82ec7084fa6b0b7aa1a47830d10', NULL, N'0003', 2, N'吊挂线', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'064223c47e9c4c41bafe39d23bb9db4a', N'4ca3c82ec7084fa6b0b7aa1a47830d10', N'0003', NULL, NULL, 2, N'挂片站', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'910ab85a8c1a4b92a66cf598c8c1bdc7', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'RackUseAfterReg', NULL, N'checkbox', N' A. 衣架须注册后才能使用', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'16483d956bd7413e970008b7d2131ead', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'RackNotLendOtherLine', NULL, N'checkbox', N' B. 注册在本流水线的衣架不外借', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'2c6b3d312c67431ab877d6e49d0b1be4', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'StartingStopOutWhenOverPlan', NULL, N'checkbox', N' C. 挂片站出衣架达到计划数后停止出衣', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'8688808c5c274c239b8c56eaada7a5c3', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'ResetRackWhenStart', NULL, N'checkbox', N' D. 从挂片站打出衣架时，清除半成品信息', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'b498905e5d3c4870bb58434cedc2886d', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'StartAfterBund', NULL, N'checkbox', N' E. 挂片站有扎卡数据才能出站', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'2e832b6a63674b42a81938b9a4f0dcdb', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'ChgProOnlineAfterBund', NULL, N'checkbox', N' F. 挂片站刷扎卡后自动切换当前上线在制品', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'47020a621f5a43c7b4f71fe55526a0c0', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'MultiStCanUseSameBund', NULL, N'checkbox', N' G. 同组内挂片站可共用扎卡', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'afbad1054c2c4a159919d9f37a79d99f', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'MultiLineCanUseSameBund', NULL, N'checkbox', N' H. 不同组挂片站可共用扎卡', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'532046851ebb4dd784b67f95be3959de', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'ChecksumQuantityGreaterThanOrderQuantity', NULL, N'number', N' I. 在制品添加产品上线时，或修改任务数量时，校验数量是否大于制单数量', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'2ed553cf40bf458e9f98c7c32fc025c3', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'ToOriginWay', NULL, N'dropdown', N' J. 空衣架回挂片站方式', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'63183ad3b44841c2a9c95e141ed81997', N'064223c47e9c4c41bafe39d23bb9db4a', N'0003', NULL, NULL, NULL, NULL, N'SingleHangerMaximumNumber', NULL, N'number', N' K. 单个衣架最大挂衣数（即产品单位允许输入的最大值）', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'644d13ab1f15488c9f36f92bd5f151b9', N'4ca3c82ec7084fa6b0b7aa1a47830d10', N'0003', NULL, NULL, 3, N'生产线', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'098c39e192244abe95d101cff11195ac', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'CanLoginFromStation', NULL, N'checkbox', N' A. 允许员工从终端登录', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'e4aba7ba20bb47d182bc3652d3585b9b', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'VertifyWhenLoginStation', NULL, N'checkbox', N' B. 员工从终端登录时要求验证密码', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'1342223592a547ca9702eb375218b299', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'RackQtyContainOnline', NULL, N'checkbox', N' C. 检测站点满站时，包括线上待进衣架', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'd4331af5efd54d94825f3dd86dd70631', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'SeqListCombine', NULL, N'checkbox', N' D. 站点加工连续的非合并工序，一次性加工完成', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'04285703c14c43b3896aa9fc37464cb2', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'AssignAimWhenOut', NULL, N'checkbox', N' E. 产品出站前须分配下一站点', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'90905d74691e45689f8b98afdd1321a3', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'StopOutWhenNextFull', NULL, N'checkbox', N' F. 下道工序不能进站时，暂停出站', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'8cb404c87a85469a915475eb7206a13d', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'AssSameEmpUseSameEmp', NULL, N'checkbox', N' G. 员工做前后不同工序时，产品优先选择同一人生产', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'5d3252caef4849ecb6155cc49c8828a4', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'GenStationCanQC', NULL, N'checkbox', N' H. 允许车缝站返工', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'cedf0d3ba6e84b2ebf2993fb67e03dd2', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'FailReturnToStation', NULL, N'checkbox', N' I. 返工送至原工序站点，未选送至原工序员工', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'63c06f782d2a4b36a9002f98e245ae34', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'VertifyFailCode', NULL, N'checkbox', N' J. 校验返工指令中的疵点代码', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'fca811a1f0804c6a9514ec692cfd4452', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'SeqInfoType', NULL, N'checkbox', N' K. 终端显示工序信息方式', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'a4df5799b57a4fa2b1a74dd6bacf4383', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'RackLow', NULL, N'number', N' L. 同工序站点站内衣数低于此值时依次分配', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'4136658e7703434c922fa7d01f84a5dc', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'OnlineRackOverMins', NULL, N'number', N' M. 线上衣架超时时间(分)', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'420a987eb49a486598a910a583dc113a', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'HangerLoadMax', NULL, N'number', N' N. 主轨推杆负载上限（%）', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'cc0fb8559f4e43a489579f20fa7bf389', N'644d13ab1f15488c9f36f92bd5f151b9', N'0003', NULL, NULL, NULL, NULL, N'HangerUnitMax', NULL, N'number', N' O. 每个衣架最大挂衣数（即产品单位允许输入的最大值）', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'1394272ac63e46a59f18447ec8fbe1bd', N'4ca3c82ec7084fa6b0b7aa1a47830d10', N'0003', NULL, NULL, 4, N'其他', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'ffbb18759c79418aa542930cbc7dc48d', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'CanEditSystemPara', NULL, N'checkbox', N' A. 站点终端允许修改系统参数权限', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'0ed8b86ff498423d8d08af41a702b76d', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'CanNotRunWhenAutoStop', NULL, N'checkbox', N' B. 自动停止期间，禁止手工运行吊挂线', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'708e0bd21e094aefb949cb4e7f5337a3', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'MachMustLogin', NULL, N'checkbox', N' C. 站位须登录衣车后做工', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'40277dcc2c4c4f9a9b467086b27452a0', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'MachLogoutByEmpLogout', NULL, N'checkbox', N' D. 员工登出站点时，同时登出衣车', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'b12805eadd6c4b2d949ee2eb1f44ecf0', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'CanQuickReportFaultMach', NULL, N'checkbox', N' E. 允许衣车快速报修', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'1fcfd93b7464467da177b0a5bb6c3dcd', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'CanLogoutFaultMach', NULL, N'checkbox', N' F. 允许登出已故障报修的衣车', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'b1b36c2b4a5f4d3cbec0e09988699342', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'CanLogoutOtherMach', NULL, N'checkbox', N' G. 终端有衣车登录时，允许刷其他衣车卡替换', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'aa685de8c4914e698be5a47fa908435c', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'EnablePacking', NULL, N'checkbox', N' H. 末道工序启动装箱功能', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'9555742c4e84487a8ee6c91de5cbd830', N'1394272ac63e46a59f18447ec8fbe1bd', N'0003', NULL, NULL, NULL, NULL, N'CallNoticeStNo', NULL, N'number', N' I. 呼叫显示到指定站点屏幕', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'c14ab4bcd0aa47a183e930b69e9b24aa', NULL, N'0004', 3, N'系统', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'3d01a1b4d3ad444a88b623674458333e', N'c14ab4bcd0aa47a183e930b69e9b24aa', N'0004', NULL, NULL, 5, N'考勤', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'aaca7b80e58d464aa28e649895eb4101', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'CanLoginMultiStation', NULL, N'checkbox', N' A. 允许员工多站点登录', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'7d1abc24ff444eee8641d48f11780d43', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'CanLogoutOtherEmp', NULL, N'checkbox', N' B. 终端有员工登录时，允许其他员工刷卡替换', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'f88daf9fe1324713b58d4fece8e485b4', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'EmpLogBySchdule', NULL, N'checkbox', N' C. 员工必须按排班时间登录', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'9587bd41293d4bb7a8f23f70cea996ba', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'OTTimeBlongWork', NULL, N'checkbox', N' D. 加班时间计入工序用时', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'5285af8656bc4979b83499739157371c', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'OTTimeBlongOffstd', NULL, N'checkbox', N' E. 加班时间计入非本位用时', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'e64bac33b32245d19a3556f45c0ac55a', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'AttIn_BeforeMins', NULL, N'number', N' F. 班次开始前有效考勤的提前分钟数', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'd14357d58bc84c7181e07b1fee26c63b', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'AttOut_AfterMins', NULL, N'number', N' G. 班次结束后有效考勤的延迟分钟数', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'e1eaf5fd8fee404b8dc87ea5bab21cc9', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'DayNightShiftHours', NULL, N'number', N' H. 第二天早班开始时间与第一天晚班结束时间的最小间隔(小时)', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'61c1c5444b5d47eb88ccbdc61612e053', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'WorkAnalysisOneDaTwoCards', NULL, N'checkbox', N' I. 考勤分析按一天刷两次卡处理', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'cb292620012447b38c6219ae78245101', N'3d01a1b4d3ad444a88b623674458333e', N'0004', NULL, NULL, NULL, NULL, N'SalaryDay', NULL, N'number', N' J. 月薪结算日', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'b7873c9a136146d9b9fb3e8f84dc7015', N'c14ab4bcd0aa47a183e930b69e9b24aa', N'0004', NULL, NULL, 6, N'考勤-生产', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'38afdb24f561419b907f4d0591e21adf', N'b7873c9a136146d9b9fb3e8f84dc7015', N'0004', NULL, NULL, NULL, NULL, N'CalcRealMinute1', NULL, N'checkbox', N' A. 计算工序用工时间', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'dc69af1da07b4c36a9f2516e438d41e8', N'b7873c9a136146d9b9fb3e8f84dc7015', N'0004', NULL, NULL, NULL, NULL, N'CalcRealMinute2', NULL, N'checkbox', N' B. 计算工序车缝时间', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'230da4228123485fb7c38c52278a9982', N'b7873c9a136146d9b9fb3e8f84dc7015', N'0004', NULL, NULL, NULL, NULL, N'EffType', NULL, N'dropdown', N' C. 显示效率类别', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'd3140fa688f9409da1877abdcdfaee16', N'b7873c9a136146d9b9fb3e8f84dc7015', N'0004', NULL, NULL, NULL, NULL, N'ProcessSiteMode', NULL, N'dropdown', N' D. 工序分配站点方式', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'2d17e385930b4ddd918498a2e1fb3bf9', N'c14ab4bcd0aa47a183e930b69e9b24aa', N'0004', NULL, NULL, 7, N'考勤-其他', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'0c86145388d347f099744fe395dc90dd', N'2d17e385930b4ddd918498a2e1fb3bf9', N'0004', NULL, NULL, NULL, NULL, N'lockScreen', NULL, N'number', N' A. 自动锁屏时间（秒）', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameter] ([Id], [SystemModuleParameterId], [SysNo], [ModuleType], [ModuleText], [SecondModuleType], [SecondModuleText], [ParamterKey], [ParamterValue], [ParamterControlType], [ParamterControlTitle], [ParamterControlDescribe], [IsEnabled], [Memo], [Memo2], [Memo3]) VALUES (N'53a3d619f0ec44d6b552207f63f905b2', N'2d17e385930b4ddd918498a2e1fb3bf9', N'0004', NULL, NULL, NULL, NULL, N'SaveLogDays', NULL, N'number', N' B. 系统日志保留天数', NULL, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'28bc7b8c538a47e4bc978245dabef89c', N'2ed553cf40bf458e9f98c7c32fc025c3', N'OutletStation', N'回起始站点', 1, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'0da081c8f9874a7da7bf483a055c7150', N'2ed553cf40bf458e9f98c7c32fc025c3', N'AverageYield', N'平均分配', 1, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'c1f20b4629dc4e819532e011cb89363c', N'fca811a1f0804c6a9514ec692cfd4452', N'ProcessAndCode', N'工序号+代码', 1, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'79daec885ecc448491c9f94748391490', N'fca811a1f0804c6a9514ec692cfd4452', N'ProcessAndName', N'工序号+名称', 1, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'7453718ee4e047cbbc8dddecc17caa3b', N'fca811a1f0804c6a9514ec692cfd4452', N'ProcessAndCodeAndName', N'工序号+代码+名称', 1, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'354b6ca79dd340b19039f876d9add535', N'230da4228123485fb7c38c52278a9982', N'DmploymentEfficiency', N'用工效率', 1, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'abe0b6d289e9474eb57e311e522741fe', N'230da4228123485fb7c38c52278a9982', N'SewingEfficiency', N'车缝效率', 1, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'91e3f202be2c46e69b600a85c63cf0ef', N'd3140fa688f9409da1877abdcdfaee16', N'StandNumberOfUnderwear', N'站内衣架数 ', 1, NULL)
GO
INSERT [dbo].[SystemModuleParameterDomain] ([Id], [SYSTEMMODULEPARAMETER_Id], [Code], [Name], [Enable], [Memo]) VALUES (N'c446a7763a5046fc8e73427e14c7f0b4', N'd3140fa688f9409da1877abdcdfaee16', N'TotalWorkingHoursStation', N'站内总工时 ', 1, NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'6a58b64fe7094714a21f9f5103842400', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T12:00:59.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'9cc09333ee49487384ae7f7facb28b9d', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T14:09:30.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'23a5fd65362a4e83a5a97d300c483a96', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T14:12:10.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'd2262affa62f4d82bcd28cf88bf30c75', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T14:49:35.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'c9388612448c40299ca94fba40686524', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T14:53:42.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'e1e3d4cfcc3d450bac5a4935b2b3d513', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T21:25:51.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'5f98c7dffc54458da8400ff438133b59', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T12:26:04.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'939dc9b1c0aa48c383c82369b0fe587b', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T13:43:52.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'fddff8f331dc4a6090ecb15d24618859', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T14:56:26.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'c783b45c5a114933a360f0763f19e297', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T14:57:01.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'9ab99bd3dcff418b81934831575af5ea', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T15:01:03.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'ca869d7511854bf59644c0194315d38f', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T16:10:43.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'8a97baf8d6ea46d2bcce3b3d0d5a946f', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T16:17:28.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'b7f0d74e79bd498189a5b5c594421e4f', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T17:48:01.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'aedd7b0c0b914318a327b67dc123af78', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T18:01:37.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'fd9d5dee7a5f44febecd4bfcbee9b3c1', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T18:38:51.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'5a1e03b0637f4d409e70005de573cc6b', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-01T19:15:56.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'd13282f87fec49c7ae8d16d0ba334cd7', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T09:19:25.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'dad0543a42de40b49e353471af65785a', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T11:03:18.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'8405a65895c144399a71dda092e097c2', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T11:44:15.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'0304eab1c5aa446d92af0a68758349fe', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T12:09:19.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'deb0b386c3384e78a2c75ff1d5768c49', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T12:38:15.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'78bba9d2b85d4f999e941b4c900da3cc', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T14:12:17.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'890122e71cba4565994b092568143feb', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T14:37:07.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'03d737d67e284db28299430e182f72ab', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T15:29:28.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'313b9d61e9f94d5087f26123930f3a9c', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T15:53:32.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserLoginInfo] ([SessionId], [UserId], [UserName], [EmployeeId], [EmployeeName], [LoginDate], [LoginOutDate]) VALUES (N'1d4edfee0b194af18a3ad386c126e1af', N'f57ad5187c3e45dbad37e7cef9b4f90f', N'admin', N'dc226049baf743ebb6796ad54395902d', N'系统用户', CAST(N'2018-09-03T16:18:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[UserOperateLogDetail] ([Id], [USEROPERATELOGS_Id], [FieldName], [FieldCode], [BeforeChange], [Changed], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'5fff4ec46f6d4084b53623a4134ca336', N'f962e7df8e654c15857f172b13f29673', N'工厂', N'FactoryCode', N'', N'MSD', CAST(N'2018-09-01T12:03:22.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogDetail] ([Id], [USEROPERATELOGS_Id], [FieldName], [FieldCode], [BeforeChange], [Changed], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'b3d0c023e3054688acff388849cde038', N'f962e7df8e654c15857f172b13f29673', N'车间', N'WorkshopCode', N'', N'CF', CAST(N'2018-09-01T12:03:22.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogDetail] ([Id], [USEROPERATELOGS_Id], [FieldName], [FieldCode], [BeforeChange], [Changed], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'268853ed6e0f4b7f9d1b0ba509b3b507', N'695a0ba1d1fe4612816006088bbedc6d', N'组名称', N'GroupName', N'', N'B', CAST(N'2018-09-03T15:48:06.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'f5c4cf29dbeb4b5c868bef50fc62af3a', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'9866238500604d0291a888ae4c91368e', 1, CAST(N'2018-09-01T12:03:10.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'f962e7df8e654c15857f172b13f29673', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'9866238500604d0291a888ae4c91368e', 3, CAST(N'2018-09-01T12:03:22.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'c8075d3888604e3a877510474caf64f9', N'员工管理', N'EmployeeAdd', NULL, N'Employee', N'b99f7dff53f449809665ee2654225d78', 1, CAST(N'2018-09-01T14:00:02.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'757f81a9eba146acad45295753a4d7b5', N'员工管理', N'EmployeeAdd', NULL, N'Employee', N'5d4797a1aa6849738583af6059b30135', 1, CAST(N'2018-09-01T14:00:16.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'39f510c1ff6d4e09952edf2e65350743', N'员工管理', N'EmployeeAdd', NULL, N'Employee', N'4758299546a54754ac9d3c05d91cc841', 1, CAST(N'2018-09-01T14:00:40.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'ddfae0d5087e4c0c94428eac1cb3c5db', N'员工管理', N'EmployeeAdd', NULL, N'Employee', N'23aa77b807f34fd5a9444b428c96e207', 1, CAST(N'2018-09-01T14:00:56.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'56d5610b78584aec97cc89328b8dd593', N'员工管理', N'EmployeeAdd', NULL, N'Employee', N'a755ca1e91f54847a9a9b27bf554e6e3', 1, CAST(N'2018-09-01T14:01:19.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'5c2b9b5677864f6a803df89bbd5352bf', N'员工管理', N'EmployeeAdd', NULL, N'Employee', N'b65ad288fc6e491aa3ed45e00e2c3eaa', 1, CAST(N'2018-09-01T14:01:36.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'65b6c99323274d39a8016e16ea4e0cf7', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'9866238500604d0291a888ae4c91368e', 3, CAST(N'2018-09-03T15:46:46.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'e3fc57ec26704c0ba7cebbfdb8784654', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'0e554b79e19b447d8963b5709d26c6ca', 1, CAST(N'2018-09-03T15:46:46.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'695a0ba1d1fe4612816006088bbedc6d', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'0e554b79e19b447d8963b5709d26c6ca', 3, CAST(N'2018-09-03T15:48:06.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'790f02290fc94dd489a85152c33a6a38', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'9866238500604d0291a888ae4c91368e', 3, CAST(N'2018-09-03T15:48:06.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'19e4e2ce58194723bd49f300fab5a1af', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'0e554b79e19b447d8963b5709d26c6ca', 3, CAST(N'2018-09-03T16:01:11.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'229cb7c8b283483f8826030accf0ab1d', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'9866238500604d0291a888ae4c91368e', 3, CAST(N'2018-09-03T16:01:11.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserOperateLogs] ([Id], [OpFormName], [OpFormCode], [OpTableName], [OpTableCode], [OpDataCode], [OpType], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser]) VALUES (N'fde05565bc6c41aead937dc104d0a8f3', N'站点组', N'SiteGroupIndex', NULL, N'SiteGroup', N'1d0cc263419f4644806a32291f25db09', 1, CAST(N'2018-09-03T16:01:11.000' AS DateTime), NULL, N'f57ad5187c3e45dbad37e7cef9b4f90f', NULL)
GO
INSERT [dbo].[UserRoles] ([Id], [ROLES_Id], [USERS_Id]) VALUES (N'6cc34fbca67d444c94fc778dfca1c310', N'3036bd41118e4b71b82fe208e8dd70ff', N'f57ad5187c3e45dbad37e7cef9b4f90f')
GO
INSERT [dbo].[Users] ([Id], [EMPLOYEE_Id], [UserName], [Password], [CardNO], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f57ad5187c3e45dbad37e7cef9b4f90f', N'dc226049baf743ebb6796ad54395902d', N'admin', N'21232f297a57a5a743894a0e4a801fc3    ', N'123456789 ', CAST(N'2018-07-10T21:58:44.000' AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
GO
INSERT [dbo].[WaitProcessOrderHanger] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [PO], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [FlowIndex], [SizeNum], [FlowNo], [ProcessFlowId], [ProcessFlowCode], [ProcessFlowName], [SiteId], [SiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSemiFinishedProducts], [SFClearDate], [Memo], [ClientMachineId], [SusLineId], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'080e95cc7e0f436c9f51bb94de332787', NULL, 1, N'acbf8501354843119780f00cbcfc2455', N'US01', 1, N'5825880', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 0, 1, N'1', N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'挂片', NULL, N'1', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:18:29.857' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[WaitProcessOrderHanger] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [PO], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [FlowIndex], [SizeNum], [FlowNo], [ProcessFlowId], [ProcessFlowCode], [ProcessFlowName], [SiteId], [SiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSemiFinishedProducts], [SFClearDate], [Memo], [ClientMachineId], [SusLineId], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'535c878cbfb642da9ac33c9cc8be3e22', NULL, 1, N'acbf8501354843119780f00cbcfc2455', N'US01', 1, N'5803209', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 0, 1, N'1', N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'挂片', NULL, N'1', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:18:33.993' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[WaitProcessOrderHanger] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [PO], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [FlowIndex], [SizeNum], [FlowNo], [ProcessFlowId], [ProcessFlowCode], [ProcessFlowName], [SiteId], [SiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSemiFinishedProducts], [SFClearDate], [Memo], [ClientMachineId], [SusLineId], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bf94323b48254aea9c9da39a23730c2d', NULL, 1, N'acbf8501354843119780f00cbcfc2455', N'US01', 1, N'5805528', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 0, 1, N'1', N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'挂片', NULL, N'1', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:19:12.453' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
INSERT [dbo].[WaitProcessOrderHanger] ([Id], [GroupNo], [MainTrackNumber], [ProductsId], [PO], [BatchNo], [HangerNo], [ProcessOrderId], [ProcessOrderNo], [PColor], [PSize], [FlowChartd], [LineName], [FlowIndex], [SizeNum], [FlowNo], [ProcessFlowId], [ProcessFlowCode], [ProcessFlowName], [SiteId], [SiteNo], [IsFlowChatChange], [IsIncomeSite], [IsSemiFinishedProducts], [SFClearDate], [Memo], [ClientMachineId], [SusLineId], [HangerType], [Status], [InsertDateTime], [UpdateDateTime], [InsertUser], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'12c079778afd408d9ec8b0a8606cb808', NULL, 1, N'acbf8501354843119780f00cbcfc2455', N'US01', 2, N'5806752', N'd9e3e36cbb6b421db9da6d4beee578be', N'MSD001', N'黄色', N'L', N'39b76b82c50c4cb8a191d0be5a240ac6', N'MSD001_MSD001_180901-1_180901-1', 0, 1, N'1', N'cd464b01b29b4f779d88bc6e74eecdcc', N'1', N'挂片', NULL, N'1', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2018-09-03T16:31:19.437' AS DateTime), NULL, N'                                ', NULL, 0, N'c001                            ')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_APPLICATIONPROFILE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ApplicationProfile] ADD  CONSTRAINT [PK_APPLICATIONPROFILE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_AREA]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Area] ADD  CONSTRAINT [PK_AREA] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_BASICPROCESSFLOW]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[BasicProcessFlow] ADD  CONSTRAINT [PK_BASICPROCESSFLOW] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_BRIDGESET]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[BridgeSet] ADD  CONSTRAINT [PK_BRIDGESET] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CARDINFO]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[CardInfo] ADD  CONSTRAINT [PK_CARDINFO] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CARDLOGININFO]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[CardLoginInfo] ADD  CONSTRAINT [PK_CARDLOGININFO] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CHANGECARDRESUME]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ChangeCardResume] ADD  CONSTRAINT [PK_CHANGECARDRESUME] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CITY]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [PK_CITY] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CLASSESEMPLOYEE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ClassesEmployee] ADD  CONSTRAINT [PK_CLASSESEMPLOYEE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CLASSESINFO]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ClassesInfo] ADD  CONSTRAINT [PK_CLASSESINFO] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CLIENTMACHINES]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ClientMachines] ADD  CONSTRAINT [PK_CLIENTMACHINES] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_COMPANY]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [PK_COMPANY] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CUSTOMER]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [PK_CUSTOMER] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CUSTOMERPURCHASEORDER]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[CustomerPurchaseOrder] ADD  CONSTRAINT [PK_CUSTOMERPURCHASEORDER] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CUSTOMERPURCHASEORDERCOLORI]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[CustomerPurchaseOrderColorItem] ADD  CONSTRAINT [PK_CUSTOMERPURCHASEORDERCOLORI] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CUSTOMERPURCHASEORDERCOLORS]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[CustomerPurchaseOrderColorSizeItem] ADD  CONSTRAINT [PK_CUSTOMERPURCHASEORDERCOLORS] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_DEFECTCODETABLE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[DefectCodeTable] ADD  CONSTRAINT [PK_DEFECTCODETABLE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_DEPARTMENT]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [PK_DEPARTMENT] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_EMPLOYEE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [PK_EMPLOYEE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_EMPLOYEECARDRELATION]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[EmployeeCardRelation] ADD  CONSTRAINT [PK_EMPLOYEECARDRELATION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_EMPLOYEEFLOWPRODUCTION]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[EmployeeFlowProduction] ADD  CONSTRAINT [PK_EMPLOYEEFLOWPRODUCTION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_EMPLOYEEGRADE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[EmployeeGrade] ADD  CONSTRAINT [PK_EMPLOYEEGRADE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_EMPLOYEEPOSITIONS]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[EmployeePositions] ADD  CONSTRAINT [PK_EMPLOYEEPOSITIONS] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_EMPLOYEESCHEDULING]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[EmployeeScheduling] ADD  CONSTRAINT [PK_EMPLOYEESCHEDULING] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_FACTORY]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Factory] ADD  CONSTRAINT [PK_FACTORY] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_FLOWACTION]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[FlowAction] ADD  CONSTRAINT [PK_FLOWACTION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_FLOWSTATINGCOLOR]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[FlowStatingColor] ADD  CONSTRAINT [PK_FLOWSTATINGCOLOR] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_FLOWSTATINGRESUME]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[FlowStatingResume] ADD  CONSTRAINT [PK_FLOWSTATINGRESUME] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_FLOWSTATINGSIZE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[FlowStatingSize] ADD  CONSTRAINT [PK_FLOWSTATINGSIZE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGER]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Hanger] ADD  CONSTRAINT [PK_HANGER] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGERPRODUCTFLOWCHART]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HangerProductFlowChart] ADD  CONSTRAINT [PK_HANGERPRODUCTFLOWCHART] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGERPRODUCTITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HangerProductItem] ADD  CONSTRAINT [PK_HANGERPRODUCTITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGERREWORKRECORD]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HangerReworkRecord] ADD  CONSTRAINT [PK_HANGERREWORKRECORD] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGERREWORKREQUEST]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HangerReworkRequest] ADD  CONSTRAINT [PK_HANGERREWORKREQUEST] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGERREWORKREQUESTITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HangerReworkRequestItem] ADD  CONSTRAINT [PK_HANGERREWORKREQUESTITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGERREWORKREQUESTQUEUE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HangerReworkRequestQueue] ADD  CONSTRAINT [PK_HANGERREWORKREQUESTQUEUE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGERREWORKREQUESTQUEUEITE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HangerReworkRequestQueueItem] ADD  CONSTRAINT [PK_HANGERREWORKREQUESTQUEUEITE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HANGERSTATINGALLOCATIONITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HangerStatingAllocationItem] ADD  CONSTRAINT [PK_HANGERSTATINGALLOCATIONITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_HOLIDAYINFO]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[HolidayInfo] ADD  CONSTRAINT [PK_HOLIDAYINFO] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_ID_GENERATOR]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ID_GENERATOR] ADD  CONSTRAINT [PK_ID_GENERATOR] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_LACKMATERIALSTABLE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[LackMaterialsTable] ADD  CONSTRAINT [PK_LACKMATERIALSTABLE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_LEDHOURSPLANTABLEITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[LEDHoursPlanTableItem] ADD  CONSTRAINT [PK_LEDHOURSPLANTABLEITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_LEDSCREENCONFIG]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[LEDScreenConfig] ADD  CONSTRAINT [PK_LEDSCREENCONFIG] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_LEDSCREENPAGE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[LEDScreenPage] ADD  CONSTRAINT [PK_LEDSCREENPAGE] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_MAINTRACK]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[MainTrack] ADD  CONSTRAINT [PK_MAINTRACK] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_MAINTRACKOPERATERECORD]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[MainTrackOperateRecord] ADD  CONSTRAINT [PK_MAINTRACKOPERATERECORD] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_MODULES]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Modules] ADD  CONSTRAINT [PK_MODULES] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_ORDERPRODUCTITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[OrderProductItem] ADD  CONSTRAINT [PK_ORDERPRODUCTITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_ORGANIZATIONS]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Organizations] ADD  CONSTRAINT [PK_ORGANIZATIONS] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PIPELINING]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Pipelining] ADD  CONSTRAINT [PK_PIPELINING] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_POCOLOR]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[PoColor] ADD  CONSTRAINT [PK_POCOLOR] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_POSITION]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Position] ADD  CONSTRAINT [PK_POSITION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSCRAFTACTION]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessCraftAction] ADD  CONSTRAINT [PK_PROCESSCRAFTACTION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSFLOW]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessFlow] ADD  CONSTRAINT [PK_PROCESSFLOW] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSFLOWCHART]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessFlowChart] ADD  CONSTRAINT [PK_PROCESSFLOWCHART] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSFLOWCHARTFLOWRELATIO]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessFlowChartFlowRelation] ADD  CONSTRAINT [PK_PROCESSFLOWCHARTFLOWRELATIO] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSFLOWCHARTGROP]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessFlowChartGrop] ADD  CONSTRAINT [PK_PROCESSFLOWCHARTGROP] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSFLOWSECTION]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessFlowSection] ADD  CONSTRAINT [PK_PROCESSFLOWSECTION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSFLOWSTATINGITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessFlowStatingItem] ADD  CONSTRAINT [PK_PROCESSFLOWSTATINGITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSFLOWVERSION]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessFlowVersion] ADD  CONSTRAINT [PK_PROCESSFLOWVERSION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSORDER]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessOrder] ADD  CONSTRAINT [PK_PROCESSORDER] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSORDERCOLORITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessOrderColorItem] ADD  CONSTRAINT [PK_PROCESSORDERCOLORITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROCESSORDERCOLORSIZEITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProcessOrderColorSizeItem] ADD  CONSTRAINT [PK_PROCESSORDERCOLORSIZEITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PRODTYPE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProdType] ADD  CONSTRAINT [PK_PRODTYPE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PRODUCTGROUP]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProductGroup] ADD  CONSTRAINT [PK_PRODUCTGROUP] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PRODUCTORDER]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProductOrder] ADD  CONSTRAINT [PK_PRODUCTORDER] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PRODUCTS]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [PK_PRODUCTS] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PRODUCTSHANGPIECERESUME]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[ProductsHangPieceResume] ADD  CONSTRAINT [PK_PRODUCTSHANGPIECERESUME] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROVINCE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Province] ADD  CONSTRAINT [PK_PROVINCE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PSIZE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[PSize] ADD  CONSTRAINT [PK_PSIZE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_ROLES]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [PK_ROLES] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_ROLESMODULES]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[RolesModules] ADD  CONSTRAINT [PK_ROLESMODULES] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SITEGROUP]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SiteGroup] ADD  CONSTRAINT [PK_SITEGROUP] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_STATING]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Stating] ADD  CONSTRAINT [PK_STATING] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_STATINGDIRECTION]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[StatingDirection] ADD  CONSTRAINT [PK_STATINGDIRECTION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_STATINGHANGERPRODUCTITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[StatingHangerProductItem] ADD  CONSTRAINT [PK_STATINGHANGERPRODUCTITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_STATINGROLES]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[StatingRoles] ADD  CONSTRAINT [PK_STATINGROLES] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_STYLE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Style] ADD  CONSTRAINT [PK_STYLE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_STYLEPROCESSFLOWITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[StyleProcessFlowItem] ADD  CONSTRAINT [PK_STYLEPROCESSFLOWITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_STYLEPROCESSFLOWSECTIONITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[StyleProcessFlowSectionItem] ADD  CONSTRAINT [PK_STYLEPROCESSFLOWSECTIONITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUCCESSHANGERPRODUCTFLOWCHA]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SuccessHangerProductFlowChart] ADD  CONSTRAINT [PK_SUCCESSHANGERPRODUCTFLOWCHA] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUCCESSHANGERSTATINGALLOCAT]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SuccessHangerStatingAllocationItem] ADD  CONSTRAINT [PK_SUCCESSHANGERSTATINGALLOCAT] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUCCESSSTATINGHANGERPRODUCT]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SuccessStatingHangerProductItem] ADD  CONSTRAINT [PK_SUCCESSSTATINGHANGERPRODUCT] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUCESSEMPLOYEEFLOWPRODUCTIO]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SucessEmployeeFlowProduction] ADD  CONSTRAINT [PK_SUCESSEMPLOYEEFLOWPRODUCTIO] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUCESSHANGERPRODUCTITEM]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SucessHangerProductItem] ADD  CONSTRAINT [PK_SUCESSHANGERPRODUCTITEM] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUCESSHANGERREWORKRECORD]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SucessHangerReworkRecord] ADD  CONSTRAINT [PK_SUCESSHANGERREWORKRECORD] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUCESSPROCESSORDERHANGER]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SucessProcessOrderHanger] ADD  CONSTRAINT [PK_SUCESSPROCESSORDERHANGER] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUCESSPRODUCTS]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SucessProducts] ADD  CONSTRAINT [PK_SUCESSPRODUCTS] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SUSLANGUAGE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SusLanguage] ADD  CONSTRAINT [PK_SUSLANGUAGE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SYSTEMLOGS]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SystemLogs] ADD  CONSTRAINT [PK_SYSTEMLOGS] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SYSTEMMODULEPARAMETER]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SystemModuleParameter] ADD  CONSTRAINT [PK_SYSTEMMODULEPARAMETER] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SYSTEMMODULEPARAMETERDOMAIN]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SystemModuleParameterDomain] ADD  CONSTRAINT [PK_SYSTEMMODULEPARAMETERDOMAIN] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SYSTEMMODULEPARAMETERVALUE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[SystemModuleParameterValue] ADD  CONSTRAINT [PK_SYSTEMMODULEPARAMETERVALUE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_TESTSITETABLE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[TestSiteTable] ADD  CONSTRAINT [PK_TESTSITETABLE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_USERCLIENTMACHINES]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[UserClientMachines] ADD  CONSTRAINT [PK_USERCLIENTMACHINES] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_USERCLIENTMACHINESPIPELININ]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[UserClientMachinesPipelinings] ADD  CONSTRAINT [PK_USERCLIENTMACHINESPIPELININ] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_USERLOGININFO]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [PK_USERLOGININFO] PRIMARY KEY NONCLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_USEROPERATELOGDETAIL]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[UserOperateLogDetail] ADD  CONSTRAINT [PK_USEROPERATELOGDETAIL] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_USEROPERATELOGS]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[UserOperateLogs] ADD  CONSTRAINT [PK_USEROPERATELOGS] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_USERROLES]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [PK_USERROLES] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_USERS]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [PK_USERS] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_WAITPROCESSORDERHANGER]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[WaitProcessOrderHanger] ADD  CONSTRAINT [PK_WAITPROCESSORDERHANGER] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_WORKSHOP]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[Workshop] ADD  CONSTRAINT [PK_WORKSHOP] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_WORKTYPE]    Script Date: 2018/9/3 21:01:32 ******/
ALTER TABLE [dbo].[WorkType] ADD  CONSTRAINT [PK_WORKTYPE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HangerProductFlowChart] ADD  DEFAULT ((0)) FOR [IsReworkSourceStating]
GO
ALTER TABLE [dbo].[HangerProductItem] ADD  DEFAULT ((0)) FOR [IsReturnWorkFlow]
GO
ALTER TABLE [dbo].[HangerReworkRequest] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[HangerReworkRequestQueue] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[HangerStatingAllocationItem] ADD  DEFAULT ((0)) FOR [IsReturnWorkFlow]
GO
ALTER TABLE [dbo].[HangerStatingAllocationItem] ADD  DEFAULT ((0)) FOR [IsReworkSourceStating]
GO
ALTER TABLE [dbo].[Stating] ADD  DEFAULT ((0)) FOR [IsReceivingHanger]
GO
ALTER TABLE [dbo].[StatingHangerProductItem] ADD  DEFAULT ((0)) FOR [IsReturnWorkFlow]
GO
ALTER TABLE [dbo].[StatingHangerProductItem] ADD  DEFAULT ((0)) FOR [IsReworkSourceStating]
GO
ALTER TABLE [dbo].[SuccessHangerProductFlowChart] ADD  DEFAULT ((0)) FOR [IsReworkSourceStating]
GO
ALTER TABLE [dbo].[SuccessHangerStatingAllocationItem] ADD  DEFAULT ((0)) FOR [IsReturnWorkFlow]
GO
ALTER TABLE [dbo].[SuccessHangerStatingAllocationItem] ADD  DEFAULT ((0)) FOR [IsReworkSourceStating]
GO
ALTER TABLE [dbo].[SuccessStatingHangerProductItem] ADD  DEFAULT ((0)) FOR [IsReturnWorkFlow]
GO
ALTER TABLE [dbo].[SuccessStatingHangerProductItem] ADD  DEFAULT ((0)) FOR [IsReworkSourceStating]
GO
ALTER TABLE [dbo].[SucessHangerProductItem] ADD  DEFAULT ((0)) FOR [IsReturnWorkFlow]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_RELATIONS_City] FOREIGN KEY([CITY_Id])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Area] CHECK CONSTRAINT [FK_Area_RELATIONS_City]
GO
ALTER TABLE [dbo].[CardLoginInfo]  WITH CHECK ADD  CONSTRAINT [FK_CardLoginInfo_RELATIONS_CardInfo] FOREIGN KEY([CARDINFO_Id])
REFERENCES [dbo].[CardInfo] ([Id])
GO
ALTER TABLE [dbo].[CardLoginInfo] CHECK CONSTRAINT [FK_CardLoginInfo_RELATIONS_CardInfo]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_RELATIONS_Province] FOREIGN KEY([PROVINCE_Id])
REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_RELATIONS_Province]
GO
ALTER TABLE [dbo].[ClassesEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ClassesEmployee_RELATIONS_ClassesInfo] FOREIGN KEY([CLASSESINFO_Id])
REFERENCES [dbo].[ClassesInfo] ([Id])
GO
ALTER TABLE [dbo].[ClassesEmployee] CHECK CONSTRAINT [FK_ClassesEmployee_RELATIONS_ClassesInfo]
GO
ALTER TABLE [dbo].[ClassesEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ClassesEmployee_RELATIONS_Employee] FOREIGN KEY([EMPLOYEE_Id])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[ClassesEmployee] CHECK CONSTRAINT [FK_ClassesEmployee_RELATIONS_Employee]
GO
ALTER TABLE [dbo].[CustomerPurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPurchaseOrder_RELATIONS_Customer] FOREIGN KEY([CUSTOMER_Id])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[CustomerPurchaseOrder] CHECK CONSTRAINT [FK_CustomerPurchaseOrder_RELATIONS_Customer]
GO
ALTER TABLE [dbo].[CustomerPurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPurchaseOrder_RELATIONS_Style] FOREIGN KEY([STYLE_Id])
REFERENCES [dbo].[Style] ([Id])
GO
ALTER TABLE [dbo].[CustomerPurchaseOrder] CHECK CONSTRAINT [FK_CustomerPurchaseOrder_RELATIONS_Style]
GO
ALTER TABLE [dbo].[CustomerPurchaseOrderColorItem]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPurchaseOrderColorItem_RELATIONS_CustomerPurchaseOrder] FOREIGN KEY([CUSTOMERPURCHASEORDER_Id])
REFERENCES [dbo].[CustomerPurchaseOrder] ([Id])
GO
ALTER TABLE [dbo].[CustomerPurchaseOrderColorItem] CHECK CONSTRAINT [FK_CustomerPurchaseOrderColorItem_RELATIONS_CustomerPurchaseOrder]
GO
ALTER TABLE [dbo].[CustomerPurchaseOrderColorItem]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPurchaseOrderColorItem_RELATIONS_PoColor] FOREIGN KEY([POCOLOR_Id])
REFERENCES [dbo].[PoColor] ([Id])
GO
ALTER TABLE [dbo].[CustomerPurchaseOrderColorItem] CHECK CONSTRAINT [FK_CustomerPurchaseOrderColorItem_RELATIONS_PoColor]
GO
ALTER TABLE [dbo].[CustomerPurchaseOrderColorSizeItem]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPurchaseOrderColorSizeItem_RELATIONS_CustomerPurchaseOrderColorItem] FOREIGN KEY([CUSTOMERPURCHASEORDERCOLORITEM_Id])
REFERENCES [dbo].[CustomerPurchaseOrderColorItem] ([Id])
GO
ALTER TABLE [dbo].[CustomerPurchaseOrderColorSizeItem] CHECK CONSTRAINT [FK_CustomerPurchaseOrderColorSizeItem_RELATIONS_CustomerPurchaseOrderColorItem]
GO
ALTER TABLE [dbo].[CustomerPurchaseOrderColorSizeItem]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPurchaseOrderColorSizeItem_RELATIONS_PSize] FOREIGN KEY([PSIZE_Id])
REFERENCES [dbo].[PSize] ([Id])
GO
ALTER TABLE [dbo].[CustomerPurchaseOrderColorSizeItem] CHECK CONSTRAINT [FK_CustomerPurchaseOrderColorSizeItem_RELATIONS_PSize]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_RELATIONS_Area] FOREIGN KEY([AREA_Id])
REFERENCES [dbo].[Area] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_RELATIONS_Area]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_RELATIONS_Department] FOREIGN KEY([DEPARTMENT_Id])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_RELATIONS_Department]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_RELATIONS_Organizations] FOREIGN KEY([ORGANIZATIONS_Id])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_RELATIONS_Organizations]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_RELATIONS_SiteGroup] FOREIGN KEY([SITEGROUP_Id])
REFERENCES [dbo].[SiteGroup] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_RELATIONS_SiteGroup]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_RELATIONS_WorkType] FOREIGN KEY([WORKTYPE_Id])
REFERENCES [dbo].[WorkType] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_RELATIONS_WorkType]
GO
ALTER TABLE [dbo].[EmployeeCardRelation]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeCardRelation_RELATIONS_CardInfo] FOREIGN KEY([CARDINFO_Id])
REFERENCES [dbo].[CardInfo] ([Id])
GO
ALTER TABLE [dbo].[EmployeeCardRelation] CHECK CONSTRAINT [FK_EmployeeCardRelation_RELATIONS_CardInfo]
GO
ALTER TABLE [dbo].[EmployeeCardRelation]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeCardRelation_RELATIONS_Employee] FOREIGN KEY([EMPLOYEE_Id])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeCardRelation] CHECK CONSTRAINT [FK_EmployeeCardRelation_RELATIONS_Employee]
GO
ALTER TABLE [dbo].[EmployeePositions]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePositions_RELATIONS_Employee] FOREIGN KEY([EMPLOYEE_Id])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeePositions] CHECK CONSTRAINT [FK_EmployeePositions_RELATIONS_Employee]
GO
ALTER TABLE [dbo].[EmployeePositions]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePositions_RELATIONS_Position] FOREIGN KEY([POSITION_Id])
REFERENCES [dbo].[Position] ([Id])
GO
ALTER TABLE [dbo].[EmployeePositions] CHECK CONSTRAINT [FK_EmployeePositions_RELATIONS_Position]
GO
ALTER TABLE [dbo].[FlowStatingColor]  WITH CHECK ADD  CONSTRAINT [FK_FlowStatingColor_RELATIONS_ProcessFlowStatingItem] FOREIGN KEY([PROCESSFLOWSTATINGITEM_Id])
REFERENCES [dbo].[ProcessFlowStatingItem] ([Id])
GO
ALTER TABLE [dbo].[FlowStatingColor] CHECK CONSTRAINT [FK_FlowStatingColor_RELATIONS_ProcessFlowStatingItem]
GO
ALTER TABLE [dbo].[FlowStatingResume]  WITH CHECK ADD  CONSTRAINT [FK_FlowStatingResume_RELATIONS_ProcessFlowChartFlowRelation] FOREIGN KEY([PROCESSFLOWCHARTFLOWRELATION_Id])
REFERENCES [dbo].[ProcessFlowChartFlowRelation] ([Id])
GO
ALTER TABLE [dbo].[FlowStatingResume] CHECK CONSTRAINT [FK_FlowStatingResume_RELATIONS_ProcessFlowChartFlowRelation]
GO
ALTER TABLE [dbo].[FlowStatingResume]  WITH CHECK ADD  CONSTRAINT [FK_FlowStatingResume_RELATIONS_Stating] FOREIGN KEY([STATING_Id])
REFERENCES [dbo].[Stating] ([Id])
GO
ALTER TABLE [dbo].[FlowStatingResume] CHECK CONSTRAINT [FK_FlowStatingResume_RELATIONS_Stating]
GO
ALTER TABLE [dbo].[FlowStatingSize]  WITH CHECK ADD  CONSTRAINT [FK_FlowStatingSize_RELATIONS_ProcessFlowStatingItem] FOREIGN KEY([PROCESSFLOWSTATINGITEM_Id])
REFERENCES [dbo].[ProcessFlowStatingItem] ([Id])
GO
ALTER TABLE [dbo].[FlowStatingSize] CHECK CONSTRAINT [FK_FlowStatingSize_RELATIONS_ProcessFlowStatingItem]
GO
ALTER TABLE [dbo].[HangerReworkRequestItem]  WITH CHECK ADD  CONSTRAINT [FK_HangerReworkRequestItem_RELATIONS_HangerReworkRequest] FOREIGN KEY([HANGERREWORKREQUEST_Id])
REFERENCES [dbo].[HangerReworkRequest] ([Id])
GO
ALTER TABLE [dbo].[HangerReworkRequestItem] CHECK CONSTRAINT [FK_HangerReworkRequestItem_RELATIONS_HangerReworkRequest]
GO
ALTER TABLE [dbo].[HangerReworkRequestQueueItem]  WITH CHECK ADD  CONSTRAINT [FK_HangerReworkRequestQueueItem_RELATIONS_HangerReworkRequestQueue] FOREIGN KEY([HANGERREWORKREQUESTQUEUE_Id])
REFERENCES [dbo].[HangerReworkRequestQueue] ([Id])
GO
ALTER TABLE [dbo].[HangerReworkRequestQueueItem] CHECK CONSTRAINT [FK_HangerReworkRequestQueueItem_RELATIONS_HangerReworkRequestQueue]
GO
ALTER TABLE [dbo].[LEDScreenPage]  WITH CHECK ADD  CONSTRAINT [FK_LEDScreenPage_RELATIONS_LEDScreenConfig] FOREIGN KEY([LEDSCREENCONFIG_Id])
REFERENCES [dbo].[LEDScreenConfig] ([Id])
GO
ALTER TABLE [dbo].[LEDScreenPage] CHECK CONSTRAINT [FK_LEDScreenPage_RELATIONS_LEDScreenConfig]
GO
ALTER TABLE [dbo].[MainTrackOperateRecord]  WITH CHECK ADD  CONSTRAINT [FK_MainTrackOperateRecord_RELATIONS_MainTrack] FOREIGN KEY([MAINTRACK_Id])
REFERENCES [dbo].[MainTrack] ([Id])
GO
ALTER TABLE [dbo].[MainTrackOperateRecord] CHECK CONSTRAINT [FK_MainTrackOperateRecord_RELATIONS_MainTrack]
GO
ALTER TABLE [dbo].[Modules]  WITH CHECK ADD  CONSTRAINT [FK_Modules_RELATIONS_Modules] FOREIGN KEY([MODULES_Id])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[Modules] CHECK CONSTRAINT [FK_Modules_RELATIONS_Modules]
GO
ALTER TABLE [dbo].[OrderProductItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderProductItem_RELATIONS_ProductOrder] FOREIGN KEY([PRODUCTORDER_Id])
REFERENCES [dbo].[ProductOrder] ([Id])
GO
ALTER TABLE [dbo].[OrderProductItem] CHECK CONSTRAINT [FK_OrderProductItem_RELATIONS_ProductOrder]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_RELATIONS_Organizations] FOREIGN KEY([ORGANIZATIONS_Id])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Organizations_RELATIONS_Organizations]
GO
ALTER TABLE [dbo].[Pipelining]  WITH CHECK ADD  CONSTRAINT [FK_Pipelining_RELATIONS_ProdType] FOREIGN KEY([PRODTYPE_Id])
REFERENCES [dbo].[ProdType] ([Id])
GO
ALTER TABLE [dbo].[Pipelining] CHECK CONSTRAINT [FK_Pipelining_RELATIONS_ProdType]
GO
ALTER TABLE [dbo].[Pipelining]  WITH CHECK ADD  CONSTRAINT [FK_Pipelining_RELATIONS_SiteGroup] FOREIGN KEY([SITEGROUP_Id])
REFERENCES [dbo].[SiteGroup] ([Id])
GO
ALTER TABLE [dbo].[Pipelining] CHECK CONSTRAINT [FK_Pipelining_RELATIONS_SiteGroup]
GO
ALTER TABLE [dbo].[ProcessFlow]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlow_RELATIONS_BasicProcessFlow] FOREIGN KEY([BASICPROCESSFLOW_Id])
REFERENCES [dbo].[BasicProcessFlow] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlow] CHECK CONSTRAINT [FK_ProcessFlow_RELATIONS_BasicProcessFlow]
GO
ALTER TABLE [dbo].[ProcessFlow]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlow_RELATIONS_ProcessFlowVersion] FOREIGN KEY([PROCESSFLOWVERSION_Id])
REFERENCES [dbo].[ProcessFlowVersion] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlow] CHECK CONSTRAINT [FK_ProcessFlow_RELATIONS_ProcessFlowVersion]
GO
ALTER TABLE [dbo].[ProcessFlowChart]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlowChart_RELATIONS_ProcessFlowVersion] FOREIGN KEY([PROCESSFLOWVERSION_Id])
REFERENCES [dbo].[ProcessFlowVersion] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlowChart] CHECK CONSTRAINT [FK_ProcessFlowChart_RELATIONS_ProcessFlowVersion]
GO
ALTER TABLE [dbo].[ProcessFlowChartFlowRelation]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlowChartFlowRelation_RELATIONS_ProcessFlow] FOREIGN KEY([PROCESSFLOW_Id])
REFERENCES [dbo].[ProcessFlow] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlowChartFlowRelation] CHECK CONSTRAINT [FK_ProcessFlowChartFlowRelation_RELATIONS_ProcessFlow]
GO
ALTER TABLE [dbo].[ProcessFlowChartFlowRelation]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlowChartFlowRelation_RELATIONS_ProcessFlowChart] FOREIGN KEY([PROCESSFLOWCHART_Id])
REFERENCES [dbo].[ProcessFlowChart] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlowChartFlowRelation] CHECK CONSTRAINT [FK_ProcessFlowChartFlowRelation_RELATIONS_ProcessFlowChart]
GO
ALTER TABLE [dbo].[ProcessFlowChartGrop]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlowChartGrop_RELATIONS_ProcessFlowChart] FOREIGN KEY([PROCESSFLOWCHART_Id])
REFERENCES [dbo].[ProcessFlowChart] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlowChartGrop] CHECK CONSTRAINT [FK_ProcessFlowChartGrop_RELATIONS_ProcessFlowChart]
GO
ALTER TABLE [dbo].[ProcessFlowChartGrop]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlowChartGrop_RELATIONS_SiteGroup] FOREIGN KEY([SITEGROUP_Id])
REFERENCES [dbo].[SiteGroup] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlowChartGrop] CHECK CONSTRAINT [FK_ProcessFlowChartGrop_RELATIONS_SiteGroup]
GO
ALTER TABLE [dbo].[ProcessFlowStatingItem]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlowStatingItem_RELATIONS_ProcessFlowChartFlowRelation] FOREIGN KEY([PROCESSFLOWCHARTFLOWRELATION_Id])
REFERENCES [dbo].[ProcessFlowChartFlowRelation] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlowStatingItem] CHECK CONSTRAINT [FK_ProcessFlowStatingItem_RELATIONS_ProcessFlowChartFlowRelation]
GO
ALTER TABLE [dbo].[ProcessFlowStatingItem]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlowStatingItem_RELATIONS_Stating] FOREIGN KEY([STATING_Id])
REFERENCES [dbo].[Stating] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlowStatingItem] CHECK CONSTRAINT [FK_ProcessFlowStatingItem_RELATIONS_Stating]
GO
ALTER TABLE [dbo].[ProcessFlowVersion]  WITH CHECK ADD  CONSTRAINT [FK_ProcessFlowVersion_RELATIONS_ProcessOrder] FOREIGN KEY([PROCESSORDER_Id])
REFERENCES [dbo].[ProcessOrder] ([Id])
GO
ALTER TABLE [dbo].[ProcessFlowVersion] CHECK CONSTRAINT [FK_ProcessFlowVersion_RELATIONS_ProcessOrder]
GO
ALTER TABLE [dbo].[ProcessOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProcessOrder_RELATIONS_Customer] FOREIGN KEY([CUSTOMER_Id])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[ProcessOrder] CHECK CONSTRAINT [FK_ProcessOrder_RELATIONS_Customer]
GO
ALTER TABLE [dbo].[ProcessOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProcessOrder_RELATIONS_Style] FOREIGN KEY([STYLE_Id])
REFERENCES [dbo].[Style] ([Id])
GO
ALTER TABLE [dbo].[ProcessOrder] CHECK CONSTRAINT [FK_ProcessOrder_RELATIONS_Style]
GO
ALTER TABLE [dbo].[ProcessOrderColorItem]  WITH CHECK ADD  CONSTRAINT [FK_ProcessOrderColorItem_RELATIONS_CustomerPurchaseOrder] FOREIGN KEY([CUSTOMERPURCHASEORDER_Id])
REFERENCES [dbo].[CustomerPurchaseOrder] ([Id])
GO
ALTER TABLE [dbo].[ProcessOrderColorItem] CHECK CONSTRAINT [FK_ProcessOrderColorItem_RELATIONS_CustomerPurchaseOrder]
GO
ALTER TABLE [dbo].[ProcessOrderColorItem]  WITH CHECK ADD  CONSTRAINT [FK_ProcessOrderColorItem_RELATIONS_PoColor] FOREIGN KEY([POCOLOR_Id])
REFERENCES [dbo].[PoColor] ([Id])
GO
ALTER TABLE [dbo].[ProcessOrderColorItem] CHECK CONSTRAINT [FK_ProcessOrderColorItem_RELATIONS_PoColor]
GO
ALTER TABLE [dbo].[ProcessOrderColorItem]  WITH CHECK ADD  CONSTRAINT [FK_ProcessOrderColorItem_RELATIONS_ProcessOrder] FOREIGN KEY([PROCESSORDER_Id])
REFERENCES [dbo].[ProcessOrder] ([Id])
GO
ALTER TABLE [dbo].[ProcessOrderColorItem] CHECK CONSTRAINT [FK_ProcessOrderColorItem_RELATIONS_ProcessOrder]
GO
ALTER TABLE [dbo].[ProcessOrderColorSizeItem]  WITH CHECK ADD  CONSTRAINT [FK_ProcessOrderColorSizeItem_RELATIONS_ProcessOrderColorItem] FOREIGN KEY([PROCESSORDERCOLORITEM_Id])
REFERENCES [dbo].[ProcessOrderColorItem] ([Id])
GO
ALTER TABLE [dbo].[ProcessOrderColorSizeItem] CHECK CONSTRAINT [FK_ProcessOrderColorSizeItem_RELATIONS_ProcessOrderColorItem]
GO
ALTER TABLE [dbo].[ProcessOrderColorSizeItem]  WITH CHECK ADD  CONSTRAINT [FK_ProcessOrderColorSizeItem_RELATIONS_PSize] FOREIGN KEY([PSIZE_Id])
REFERENCES [dbo].[PSize] ([Id])
GO
ALTER TABLE [dbo].[ProcessOrderColorSizeItem] CHECK CONSTRAINT [FK_ProcessOrderColorSizeItem_RELATIONS_PSize]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_RELATIONS_ProcessFlowChart] FOREIGN KEY([PROCESSFLOWCHART_Id])
REFERENCES [dbo].[ProcessFlowChart] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_RELATIONS_ProcessFlowChart]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_RELATIONS_ProcessOrder] FOREIGN KEY([PROCESSORDER_Id])
REFERENCES [dbo].[ProcessOrder] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_RELATIONS_ProcessOrder]
GO
ALTER TABLE [dbo].[ProductsHangPieceResume]  WITH CHECK ADD  CONSTRAINT [FK_ProductsHangPieceResume_RELATIONS_Products] FOREIGN KEY([PRODUCTS_Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductsHangPieceResume] CHECK CONSTRAINT [FK_ProductsHangPieceResume_RELATIONS_Products]
GO
ALTER TABLE [dbo].[RolesModules]  WITH CHECK ADD  CONSTRAINT [FK_RolesModules_RELATIONS_Modules] FOREIGN KEY([MODULES_Id])
REFERENCES [dbo].[Modules] ([Id])
GO
ALTER TABLE [dbo].[RolesModules] CHECK CONSTRAINT [FK_RolesModules_RELATIONS_Modules]
GO
ALTER TABLE [dbo].[RolesModules]  WITH CHECK ADD  CONSTRAINT [FK_RolesModules_RELATIONS_Roles] FOREIGN KEY([ROLES_Id])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[RolesModules] CHECK CONSTRAINT [FK_RolesModules_RELATIONS_Roles]
GO
ALTER TABLE [dbo].[SiteGroup]  WITH CHECK ADD  CONSTRAINT [FK_SiteGroup_RELATIONS_Workshop] FOREIGN KEY([WORKSHOP_Id])
REFERENCES [dbo].[Workshop] ([Id])
GO
ALTER TABLE [dbo].[SiteGroup] CHECK CONSTRAINT [FK_SiteGroup_RELATIONS_Workshop]
GO
ALTER TABLE [dbo].[Stating]  WITH CHECK ADD  CONSTRAINT [FK_Stating_RELATIONS_SiteGroup] FOREIGN KEY([SITEGROUP_Id])
REFERENCES [dbo].[SiteGroup] ([Id])
GO
ALTER TABLE [dbo].[Stating] CHECK CONSTRAINT [FK_Stating_RELATIONS_SiteGroup]
GO
ALTER TABLE [dbo].[Stating]  WITH CHECK ADD  CONSTRAINT [FK_Stating_RELATIONS_StatingDirection] FOREIGN KEY([STATINGDIRECTION_Id])
REFERENCES [dbo].[StatingDirection] ([Id])
GO
ALTER TABLE [dbo].[Stating] CHECK CONSTRAINT [FK_Stating_RELATIONS_StatingDirection]
GO
ALTER TABLE [dbo].[Stating]  WITH CHECK ADD  CONSTRAINT [FK_Stating_RELATIONS_StatingRoles] FOREIGN KEY([STATINGROLES_Id])
REFERENCES [dbo].[StatingRoles] ([Id])
GO
ALTER TABLE [dbo].[Stating] CHECK CONSTRAINT [FK_Stating_RELATIONS_StatingRoles]
GO
ALTER TABLE [dbo].[Stating]  WITH CHECK ADD  CONSTRAINT [FK_Stating_RELATIONS_SusLanguage] FOREIGN KEY([SUSLANGUAGE_Id])
REFERENCES [dbo].[SusLanguage] ([Id])
GO
ALTER TABLE [dbo].[Stating] CHECK CONSTRAINT [FK_Stating_RELATIONS_SusLanguage]
GO
ALTER TABLE [dbo].[StyleProcessFlowItem]  WITH CHECK ADD  CONSTRAINT [FK_StyleProcessFlowItem_RELATIONS_BasicProcessFlow] FOREIGN KEY([BASICPROCESSFLOW_Id])
REFERENCES [dbo].[BasicProcessFlow] ([Id])
GO
ALTER TABLE [dbo].[StyleProcessFlowItem] CHECK CONSTRAINT [FK_StyleProcessFlowItem_RELATIONS_BasicProcessFlow]
GO
ALTER TABLE [dbo].[StyleProcessFlowItem]  WITH CHECK ADD  CONSTRAINT [FK_StyleProcessFlowItem_RELATIONS_Style] FOREIGN KEY([STYLE_Id])
REFERENCES [dbo].[Style] ([Id])
GO
ALTER TABLE [dbo].[StyleProcessFlowItem] CHECK CONSTRAINT [FK_StyleProcessFlowItem_RELATIONS_Style]
GO
ALTER TABLE [dbo].[StyleProcessFlowSectionItem]  WITH CHECK ADD  CONSTRAINT [FK_StyleProcessFlowSectionItem_RELATIONS_BasicProcessFlow] FOREIGN KEY([BASICPROCESSFLOW_Id])
REFERENCES [dbo].[BasicProcessFlow] ([Id])
GO
ALTER TABLE [dbo].[StyleProcessFlowSectionItem] CHECK CONSTRAINT [FK_StyleProcessFlowSectionItem_RELATIONS_BasicProcessFlow]
GO
ALTER TABLE [dbo].[StyleProcessFlowSectionItem]  WITH CHECK ADD  CONSTRAINT [FK_StyleProcessFlowSectionItem_RELATIONS_ProcessFlowSection] FOREIGN KEY([PROCESSFLOWSECTION_Id])
REFERENCES [dbo].[ProcessFlowSection] ([Id])
GO
ALTER TABLE [dbo].[StyleProcessFlowSectionItem] CHECK CONSTRAINT [FK_StyleProcessFlowSectionItem_RELATIONS_ProcessFlowSection]
GO
ALTER TABLE [dbo].[StyleProcessFlowSectionItem]  WITH CHECK ADD  CONSTRAINT [FK_StyleProcessFlowSectionItem_RELATIONS_Style] FOREIGN KEY([STYLE_Id])
REFERENCES [dbo].[Style] ([Id])
GO
ALTER TABLE [dbo].[StyleProcessFlowSectionItem] CHECK CONSTRAINT [FK_StyleProcessFlowSectionItem_RELATIONS_Style]
GO
ALTER TABLE [dbo].[SystemModuleParameterDomain]  WITH CHECK ADD  CONSTRAINT [FK_SystemModuleParameterDomain_RELATIONS_SystemModuleParameter] FOREIGN KEY([SYSTEMMODULEPARAMETER_Id])
REFERENCES [dbo].[SystemModuleParameter] ([Id])
GO
ALTER TABLE [dbo].[SystemModuleParameterDomain] CHECK CONSTRAINT [FK_SystemModuleParameterDomain_RELATIONS_SystemModuleParameter]
GO
ALTER TABLE [dbo].[SystemModuleParameterValue]  WITH CHECK ADD  CONSTRAINT [FK_SystemModuleParameterValue_RELATIONS_SystemModuleParameter] FOREIGN KEY([SYSTEMMODULEPARAMETER_Id])
REFERENCES [dbo].[SystemModuleParameter] ([Id])
GO
ALTER TABLE [dbo].[SystemModuleParameterValue] CHECK CONSTRAINT [FK_SystemModuleParameterValue_RELATIONS_SystemModuleParameter]
GO
ALTER TABLE [dbo].[UserClientMachinesPipelinings]  WITH CHECK ADD  CONSTRAINT [FK_UserClientMachinesPipelinings_RELATIONS_Pipelining] FOREIGN KEY([PIPELINING_Id])
REFERENCES [dbo].[Pipelining] ([Id])
GO
ALTER TABLE [dbo].[UserClientMachinesPipelinings] CHECK CONSTRAINT [FK_UserClientMachinesPipelinings_RELATIONS_Pipelining]
GO
ALTER TABLE [dbo].[UserClientMachinesPipelinings]  WITH CHECK ADD  CONSTRAINT [FK_UserClientMachinesPipelinings_RELATIONS_UserClientMachines] FOREIGN KEY([USERCLIENTMACHINES_Id])
REFERENCES [dbo].[UserClientMachines] ([Id])
GO
ALTER TABLE [dbo].[UserClientMachinesPipelinings] CHECK CONSTRAINT [FK_UserClientMachinesPipelinings_RELATIONS_UserClientMachines]
GO
ALTER TABLE [dbo].[UserOperateLogDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserOperateLogDetail_RELATIONS_UserOperateLogs] FOREIGN KEY([USEROPERATELOGS_Id])
REFERENCES [dbo].[UserOperateLogs] ([Id])
GO
ALTER TABLE [dbo].[UserOperateLogDetail] CHECK CONSTRAINT [FK_UserOperateLogDetail_RELATIONS_UserOperateLogs]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_RELATIONS_Roles] FOREIGN KEY([ROLES_Id])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_RELATIONS_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_RELATIONS_Users] FOREIGN KEY([USERS_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_RELATIONS_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_RELATIONS_Employee] FOREIGN KEY([EMPLOYEE_Id])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_RELATIONS_Employee]
GO
ALTER TABLE [dbo].[Workshop]  WITH CHECK ADD  CONSTRAINT [FK_Workshop_RELATIONS_Factory] FOREIGN KEY([FACTORY_Id])
REFERENCES [dbo].[Factory] ([Id])
GO
ALTER TABLE [dbo].[Workshop] CHECK CONSTRAINT [FK_Workshop_RELATIONS_Factory]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统初始化信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplicationProfile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area', @level2type=N'COLUMN',@level2name=N'AreaName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area', @level2type=N'COLUMN',@level2name=N'Addess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Area'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序状态
   0:待走流程
   1:流程已经完成
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SAM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'SAM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准工时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'StanardHours'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准工价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'StandardPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'PrcocessRmark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'DefaultFlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基本工序库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BasicProcessFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顺序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet', @level2type=N'COLUMN',@level2name=N'BIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A线主轨' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet', @level2type=N'COLUMN',@level2name=N'AMainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet', @level2type=N'COLUMN',@level2name=N'ASiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'方向' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet', @level2type=N'COLUMN',@level2name=N'Direction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'方向描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet', @level2type=N'COLUMN',@level2name=N'DirectionTxt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'B线主轨' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet', @level2type=N'COLUMN',@level2name=N'BMainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'B线主轨站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet', @level2type=N'COLUMN',@level2name=N'BSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'桥接配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BridgeSet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号(换算十六进制占无字节)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'CardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:衣架卡
   2:衣车卡
   3:机修卡
   4:员工卡' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'CardType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡描述(1:衣架卡
   2:衣车卡
   3:机修卡
   4:员工卡)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'CardDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'IsEnabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否能多点登录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'IsMultiLogin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡片信息(
   1:衣架卡
   2:衣车卡
   3:机修卡
   4:员工卡)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'CARDINFO_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录主轨' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录站点(十进制，换算成16进制占1字节)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'LoginStatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'LoginDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下线时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'LogOutDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否在线' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'IsOnline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡登录信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CardLoginInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume', @level2type=N'COLUMN',@level2name=N'CardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换卡原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume', @level2type=N'COLUMN',@level2name=N'ChangeCardReason'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换卡履历' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangeCardResume'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'PROVINCE_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'CLASSESINFO_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出勤日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'AttendanceDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'EffectDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段1上班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'Time1GoToWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段1下班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'Time1GoOffWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段2上班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'Time2GoToWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段2下班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'Time2GoOffWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段3上班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'Time3GoToWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段3下班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'Time3GoOffWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次员工' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesEmployee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次类型(0:正常班次;1.加班班次;2.假日班次)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'CType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段1上班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Time1GoToWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段1下班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Time1GoOffWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段2上班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Time2GoToWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段2下班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Time2GoOffWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段3上班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Time3GoToWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时段3下班时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Time3GoOffWorkDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用(0:启用;1:禁用)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'IsEnabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'班次信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClassesInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientMachines', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientMachines', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientMachines', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientMachines', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientMachines', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'CompanyCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'CompanyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'CusNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'CusName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PO编号(外贸订单号)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'PurchaseOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'LinkMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'CusNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'CusName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'CusPurOrderNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PO编号(外贸订单号)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'PurchaseOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'OrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下单日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'GeneratorDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交货日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'DeliveryDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发货地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'DeliverAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'LinkMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户外贸单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'MOrderItemNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'Color'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'ColorDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'Total'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户外贸订单明细颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'SizeDesption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'Total'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户外贸订单明细颜色尺码明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerPurchaseOrderColorSizeItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable', @level2type=N'COLUMN',@level2name=N'DefectNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'疵点代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable', @level2type=N'COLUMN',@level2name=N'DefectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'疵点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable', @level2type=N'COLUMN',@level2name=N'DefectName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'疵点代码表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DefectCodeTable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'DepNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'DepName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'DEPARTMENT_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'AREA_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'WORKTYPE_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号(唯一)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'RealName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'CardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Valid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录用日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'EmploymentDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'住址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入职时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'StartingDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'离职日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'LeaveDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'银行卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'BankCardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeCardRelation', @level2type=N'COLUMN',@level2name=N'CARDINFO_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工卡关系' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeCardRelation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'EmployeeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序生产的员工姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'EmployeeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'CardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:正常衣架;1:返工衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'HangerType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未完成;1.已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工工序产量明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeFlowProduction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeGrade', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeGrade', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeGrade', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeGrade', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeGrade', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeGrade', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeGrade', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeGrade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeePositions', @level2type=N'COLUMN',@level2name=N'POSITION_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工职务' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeePositions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeScheduling', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeScheduling', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeScheduling', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeScheduling', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeScheduling', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeScheduling', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeScheduling', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工排班信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EmployeeScheduling'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工厂编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'FacCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工厂名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'FacName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工厂' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Factory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowAction', @level2type=N'COLUMN',@level2name=N'ActionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowAction', @level2type=N'COLUMN',@level2name=N'ActionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowAction', @level2type=N'COLUMN',@level2name=N'IsEnabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'PROCESSFLOWSTATINGITEM_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'StatingName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'ColorValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'ColorDesption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺工序制作站点指定的颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingResume', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingResume', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingResume', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingResume', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingResume', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingResume', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingResume', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序制作站变更履历' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingResume'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'PROCESSFLOWSTATINGITEM_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'StatingName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'ColorValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'ColorDesption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'Total'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺工序制作站点指定的尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FlowStatingSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Hanger', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Hanger', @level2type=N'COLUMN',@level2name=N'RegisterDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Hanger'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨号(0-255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架是否生产完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'IsHangerSucess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PO号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'PO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'ProcessChartId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序制作站点Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'StatingId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序制作站点(来自工艺路线图中的站点)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序制作站点容量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'StatingCapacity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序下一站点(计算出的下一道工序制作站点)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'NextStatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序实际生产站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowRealyProductStatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产状态(0:待生产:1:生产中:2:生产完成)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:正常工序;1:返工工序:2:其他' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否生产完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'IsFlowSucess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工发起站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'IsReworkSourceStating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'疵点代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'DefectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序生产的员工姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'EmployeeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'CardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架生产工艺图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductFlowChart'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'HPIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'IsSucessedFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'IsReturnWorkFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工来源站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'returnWorkSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架生产明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerProductItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'IsSucessedFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工来源站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'returnWorkSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工疵点代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord', @level2type=N'COLUMN',@level2name=N'DefectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架返工记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRecord'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态【0:收到的下位机的返工请求;1;正在返工;2:返工完成;-1:返工出错】' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架返工请求' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequest'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'FlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工工序疵点代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'DefectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架返工请求明细队列明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态【0:收到的下位机的返工请求;1;正在返工;2:返工完成;-1:返工出错】' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架返工请求队列' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'FlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工工序疵点代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'DefectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架返工请求明细队列明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerReworkRequestQueueItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下一站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'NextSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsSucessedFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsReturnWorkFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工来源站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'returnWorkSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分配索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'HSAINdex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工发起站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsReworkSourceStating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点分配时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'AllocatingStatingDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:正常衣架;1:返工衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'HangerType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未完成;1.已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架站点分配明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HangerStatingAllocationItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HolidayInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HolidayInfo', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HolidayInfo', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HolidayInfo', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HolidayInfo', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HolidayInfo', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HolidayInfo', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'假日信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HolidayInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'@generateColumnConfig=sequence' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标示符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR', @level2type=N'COLUMN',@level2name=N'FLAG_NO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR', @level2type=N'COLUMN',@level2name=N'BEGIN_VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目前值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR', @level2type=N'COLUMN',@level2name=N'CURRENT_VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR', @level2type=N'COLUMN',@level2name=N'END_VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR', @level2type=N'COLUMN',@level2name=N'SORT_VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR', @level2type=N'COLUMN',@level2name=N'MEMO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'@needUpdate=false
   @generateColumnConfig=fix:data=1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'号码段的分类
   @needMeta=false' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ID_GENERATOR'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缺料代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LackMaterialsTable', @level2type=N'COLUMN',@level2name=N'LackMaterialsCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缺料名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LackMaterialsTable', @level2type=N'COLUMN',@level2name=N'LackMaterialsName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LackMaterialsTable', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LackMaterialsTable', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LackMaterialsTable', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LackMaterialsTable', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LackMaterialsTable', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缺料代码表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LackMaterialsTable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDHoursPlanTableItem', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDHoursPlanTableItem', @level2type=N'COLUMN',@level2name=N'BeginDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDHoursPlanTableItem', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDHoursPlanTableItem', @level2type=N'COLUMN',@level2name=N'PlanNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDHoursPlanTableItem', @level2type=N'COLUMN',@level2name=N'InsertDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDHoursPlanTableItem', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LED小时计划表明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDHoursPlanTableItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'屏号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'ScreenNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制器类型(EQ2011)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'ControllerTypeTxt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制器类型Key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'ControllerKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通信方式(0:串口;1:网络)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'CommunicationWay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通信方式描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'CommunicationWayTxt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示屏宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'SWidth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示屏高度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'SHeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'ColorType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色类型描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'ColorTypeTxt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'IPAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'端口号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'Port'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用(0：否:1:是)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig', @level2type=N'COLUMN',@level2name=N'Enable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LED屏配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenConfig'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'LEDSCREENCONFIG_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'PageNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'InfoType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息类别描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'InfoTypeTxt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自定义信息内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'CusContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时长(秒)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'Times'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'刷新周期(秒)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'RefreshCycle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage', @level2type=N'COLUMN',@level2name=N'InsertTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LED显示屏页面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LEDScreenPage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(0--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运行状态(0:运行中;1:已停止)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启动时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'StartDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'急停时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'EmergencyStopDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停止时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'StopDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型(0:启动;1：急停;2：停止)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord', @level2type=N'COLUMN',@level2name=N'MType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨操作记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MainTrackOperateRecord'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'ActionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作Key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'ActionKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:菜单
   2:按钮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'ModulesType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单 模块表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'SequenceNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'ProductNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'ProductName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'Color'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'Rule'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'ProductNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'ProductUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'每箱数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'BoxNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加工单产品明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderProductItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations', @level2type=N'COLUMN',@level2name=N'ActionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Organizations'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水线号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'PipeliNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推杆数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'PushRodNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水线' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pipelining'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'SNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'ColorValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'ColorDescption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'Rmark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PoColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职务编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'PosCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'PosName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职务' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Position'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'ActionNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'ActionDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否生效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'IsEnabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序段序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'ProSectionNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序段代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'ProSectionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序段名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'ProSectionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'Remark2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记：
   1：已删除；0正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图制作动作' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessCraftAction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessOrderField'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序状态
   0:待走流程
   1:流程已经完成
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准工时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'StanardHours'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准工价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'StandardPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'DefaultFlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'PrcocessRemark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'ProcessColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'EffectiveDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记:0正常，1:删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'LinkName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'pFlowChartNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品部位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'ProductPosition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标产量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'TargetNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产出工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'OutputProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂片开始生产工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'BoltProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图:
   产品在吊挂生产线的生产工序排序及员工任务分配的数据集合' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChart'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顺序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'CraftFlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'IsEnabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效文本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'EnabledText'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否往前合并' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'IsMergeForward'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'往前合并文本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'MergeForwardText'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'FlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'FlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是产出工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'IsProduceFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合并工序制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'MergeProcessFlowChartFlowRelationId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合并工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'MergeFlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图制单工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartFlowRelation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartGrop', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartGrop', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartGrop', @level2type=N'COLUMN',@level2name=N'GroupName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图使用组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowChartGrop'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序段序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'ProSectionNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序段名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'ProSectionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序段代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'ProSectionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowSection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否接收衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'IsReceivingHanger'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收内容(默认是衣架)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'ReceingContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'SiteGroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'mainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'No'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点角色(备用查询字段)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'StatingRoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否全部接收尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'IsReceivingAllSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否全部接收颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'IsReceivingAllColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否全部接收PO号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'isReceivingAllPONumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是收尾站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'IsEndStating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'Proportion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'ReceivingColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'ReceivingSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收PO号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'ReceivingPONumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序制作站点明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowStatingItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'ProVersionNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'ProVersionNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'ProcessVersionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'EffectiveDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总工价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'TotalStandardPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总SAM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'TotalSAM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'InsertDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessFlowVersion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'POrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'POrderNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'MOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单类型(1.散客;2.包装;3.无客户制单)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'POrderType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单类型描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'POrderTypeDesption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生成通知单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'ProductNoticeOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'Num'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单状态
   1:已审核
   2:制作完成
   3:生产中
   4：完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'StyleCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款式描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'StyleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'CustomerNO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'CustomerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户款号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'CustomerStyle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'CustOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户PO号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'CustPurchaseOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交货日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'DeliveryDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下单日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'GenaterOrderDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'OrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记:0正常，1:删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'MOrderItemNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'Color'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'ColorDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码合计' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'Total'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记：0正常，1删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单明细颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'SizeDesption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'Total'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单明细颜色尺码明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessOrderColorSizeItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProdType', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProdType', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProdType', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProdType', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProdType', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProdType', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产线类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProdType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductGroup', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductGroup', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductGroup', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductGroup', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductGroup', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductGroup', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductGroup', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductGroup'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'OrderName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'包装类型
   1：允许布匹混装
   2:允许产品混装
   3：允许超装
   4：允许工单混装' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'OrderPackgeType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加工单类型
   1:货单
   2：样品单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'OrderType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'VersionNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'ProcessDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'ProcessPerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'SystemOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'CustomerOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加工单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'CustomerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'SucessDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产加工单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排产号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ProductionNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上线日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ImplementDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂片站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'HangingPieceSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态:0:未分配;1:已分配;3.上线;4.已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户外贸单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'CustomerPurchaseOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'OrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'StyleNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PO号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'PO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'FlowSection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'Unit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TaskNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在线数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'OnlineNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日挂片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TodayHangingPieceSiteNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日产出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TodayProdOutNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计产出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TotalProdOutNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日绑卡' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TodayBindCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日返工' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TodayRework'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计挂片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TotalHangingPieceSiteNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计返工' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TotalRework'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计绑卡' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TotalBindNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Products'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'PRODUCTS_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂片站编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'HangPieceNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂片站名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'HangName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品挂片履历' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductsHangPieceResume'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Province', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Province', @level2type=N'COLUMN',@level2name=N'ProvinceCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Province', @level2type=N'COLUMN',@level2name=N'ProvinceName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'PSNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'Size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'SizeDesption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'ActionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限的角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Roles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色模块' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolesModules'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'GroupNO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'GroupName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工厂' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'FactoryCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'WorkshopCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记：0正常，1删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点组:
   为客户企业管理中的行政划分单位，与吊挂生产线可以是一对一关系，也可以是一对多，多对多的关系，视实际情况安装' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SiteGroup'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'StatingName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点语言' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'Language'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'容量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'Capacity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否接收衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'IsReceivingHanger'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'ColorValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'负载监(:0正常，1:不可用)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'IsLoadMonitor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'链式提升' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'IsChainHoist'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提升行程缓存满' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'IsPromoteTripCachingFull'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点条码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'SiteBarCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'IsEnabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'方向' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'Direction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主板版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'MainboardNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SN号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'SerialNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记:0正常，1:删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点:
   即一个包括进站、出站功能，有终端显示操作面板的集合，一般安装一名生产员工及一台衣车' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Stating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'方向Key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection', @level2type=N'COLUMN',@level2name=N'DirectionKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'方向描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection', @level2type=N'COLUMN',@level2name=N'DirectionDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点方向' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingDirection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsSucessedFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsReturnWorkFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工发起站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsReworkSourceStating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点衣架生产明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingHangerProductItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingRoles', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingRoles', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingRoles', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingRoles', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingRoles', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingRoles', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StatingRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'StyleNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款式名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'StyleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'Rmark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记：
   1：已删除；0正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款式工艺' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Style'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'ProVersionNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'ProcessVersionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'ProcessCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序状态
   0:待走流程
   1:流程已经完成
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'ProcessStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SAM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'SAM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准工时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'StanardHours'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准工价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'StandardPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款式工序明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'ProcessCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序状态
   0:待走流程
   1:流程已经完成
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'ProcessStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'ProcessName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SAM' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'SAM'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准工时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'StanardHours'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准工价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'StandardPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'PrcocessRmark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序段工序明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StyleProcessFlowSectionItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主轨号(0-255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架是否生产完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'IsHangerSucess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PO号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'PO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'ProcessChartId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序制作站点Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'StatingId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序制作站点(来自工艺路线图中的站点)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序制作站点容量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'StatingCapacity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序下一站点(计算出的下一道工序制作站点)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'NextStatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序实际生产站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowRealyProductStatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产状态(0:待生产:1:生产中:2:生产完成)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:正常工序;1:返工工序:2:其他' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'FlowType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否生产完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'IsFlowSucess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工发起站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'IsReworkSourceStating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'疵点代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'DefectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序生产的员工姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'EmployeeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'CardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成的衣架生产工艺图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerProductFlowChart'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下一站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'NextSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsSucessedFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsReturnWorkFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工来源站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'returnWorkSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分配索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'HSAINdex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工发起站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IsReworkSourceStating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点分配时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'AllocatingStatingDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:正常衣架;1:返工衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'HangerType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未完成;1.已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成衣架站点分配明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessHangerStatingAllocationItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsSucessedFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsReturnWorkFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工发起站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsReworkSourceStating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机器Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成的站点衣架生产明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SuccessStatingHangerProductItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'EmployeeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序生产的员工姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'EmployeeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'CardNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成员工工序产量明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessEmployeeFlowProduction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'HPIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsSucessedFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是返工工序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'IsReturnWorkFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工来源站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'returnWorkSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机器Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成的衣架生产明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerProductItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序是否完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'IsSucessedFlow'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工来源站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'returnWorkSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机器Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进站时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'IncomeSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'CompareDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出战时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'OutSiteDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返工疵点代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord', @level2type=N'COLUMN',@level2name=N'DefectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成的衣架返工记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessHangerReworkRecord'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PO号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'PO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是半成品清除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'IsSemiFinishedProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'半成品清除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SFClearDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产完成制单衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProcessOrderHanger'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排产号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'ProductionNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上线日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'ImplementDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂片站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'HangingPieceSiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态:0:未分配;1:已分配;3.上线;4.已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户外贸单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'CustomerPurchaseOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'OrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'款号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'StyleNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PO号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'PO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'FlowSection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'Unit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TaskNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在线数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'OnlineNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日挂片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TodayHangingPieceSiteNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日产出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TodayProdOutNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计产出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TotalProdOutNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日绑卡' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TodayBindCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'今日返工' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TodayRework'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计挂片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TotalHangingPieceSiteNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计返工' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TotalRework'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计绑卡' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'TotalBindNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产完成制品' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SucessProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'语言简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage', @level2type=N'COLUMN',@level2name=N'LanguageKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'语言' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage', @level2type=N'COLUMN',@level2name=N'LanguageValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'语言' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SusLanguage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级模块参数Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'SystemModuleParameterId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'SysNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数类型(0:用户参数;1:客户机参数:2：吊挂线;3:系统)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'ModuleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数Key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'ModuleText'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级模块参数类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'SecondModuleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级模块参数描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'SecondModuleText'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数Value' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'ParamterKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'ParamterValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数控件类型(0：Checkbox;,1:Text;2:其他)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'ParamterControlType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数控件标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'ParamterControlTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数控件描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'ParamterControlDescribe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'IsEnabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'Memo2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter', @level2type=N'COLUMN',@level2name=N'Memo3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameterDomain', @level2type=N'COLUMN',@level2name=N'SYSTEMMODULEPARAMETER_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameterValue', @level2type=N'COLUMN',@level2name=N'SYSTEMMODULEPARAMETER_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameterValue', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameterValue', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameterValue', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameterValue', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SystemModuleParameterValue', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestSiteTable', @level2type=N'COLUMN',@level2name=N'StatingNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestSiteTable', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestSiteTable', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestSiteTable', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestSiteTable', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestSiteTable', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestSiteTable', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'测试站点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TestSiteTable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机器Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserClientMachines', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserClientMachinesPipelinings', @level2type=N'COLUMN',@level2name=N'PIPELINING_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户客户机流水线' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserClientMachinesPipelinings'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogDetail', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogDetail', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogDetail', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogDetail', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: insert
   2: delete
   3: select
   4: update' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogs', @level2type=N'COLUMN',@level2name=N'OpType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogs', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogs', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogs', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogs', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户操作日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserOperateLogs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生产组编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'GroupNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一个组对应一个主轨号(范围1--255)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'MainTrackNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProductsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PO号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'PO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'BatchNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'衣架号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'HangerNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessOrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessOrderNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'PColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'尺码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'PSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺路线图Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'FlowChartd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工艺图名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'LineName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'FlowIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SizeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'FlowNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单工序Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessFlowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessFlowCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工序名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ProcessFlowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SiteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求出站站号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SiteNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路线图是否改变' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'IsFlowChatChange'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否进站' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'IsIncomeSite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是半成品清除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'IsSemiFinishedProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'半成品清除时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SFClearDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户机Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'ClientMachineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'吊挂线Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'SusLineId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:正常衣架;1:返工衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'HangerType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未完成;1.已完成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'待生产制单衣架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WaitProcessOrderHanger'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车间编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'WorCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车间名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'WorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'车间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Workshop'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkType', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkType', @level2type=N'COLUMN',@level2name=N'InsertDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插入用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkType', @level2type=N'COLUMN',@level2name=N'InsertUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkType', @level2type=N'COLUMN',@level2name=N'UpdateDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkType', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkType', @level2type=N'COLUMN',@level2name=N'Deleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkType', @level2type=N'COLUMN',@level2name=N'CompanyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工种信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkType'
GO
