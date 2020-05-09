/*
执行本脚本之后，需要重新配置角色可访问页面数据。
*/
USE [SyspeSysV2.0]
GO
delete from Modules
delete from RolesModules
/****** Object:  Table [dbo].[Modules]    Script Date: 07/19/2018 22:40:24 ******/
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e182ff1816e8454c8c0c1c9788872478', NULL, N'厦门悬挂系统', N'root', N'根目录', 1, 0, CAST(0.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'69f9e95079d142ebbaaf1e5daf115a59', N'e182ff1816e8454c8c0c1c9788872478', N'启动主轨', N'barBtn_RunMainTrack', N'启动主轨', 3, 1, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'164bdafc5cdd41b29265bb8cfe9f06f4', N'e182ff1816e8454c8c0c1c9788872478', N'停止主轨', N'barBtn_StopMainTrack', N'停止主轨', 3, 1, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'42efab31384f476e8fe91836bab7fac2', N'e182ff1816e8454c8c0c1c9788872478', N'急停主轨', N'barBtn_EmergencyStopMainTrack', N'急停主轨', 3, 1, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1e41f5ee1bef461fa67fe8b053bed05c', N'e182ff1816e8454c8c0c1c9788872478', N'生产管理', N'barBtn_RealTimeInformation', N'生产管理', 3, 1, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'65e5267e953947c9bc8e597624ec5644', N'1e41f5ee1bef461fa67fe8b053bed05c', N'生产制单', N'Billing_ProcessOrderIndex', N'生产制单', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6389a8429a1e4c77abeade0bf0fd9fdd', N'1e41f5ee1bef461fa67fe8b053bed05c', N'制单工序', N'Billing_ProcessFlowIndex', N'制单工序', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'156240961dfc40aaaf8014e81c635f7b', N'1e41f5ee1bef461fa67fe8b053bed05c', N'工艺路线图', N'Billing_ProcessFlowChartIndex', N'工艺路线图', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'491d39b1d0d249609745f3ba9f5f1a3a', N'1e41f5ee1bef461fa67fe8b053bed05c', N'产线实时信息', N'Billing_ProductRealtimeInfo', N'产线实时信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'821c15668fe14878b4e21c44b7dba36f', N'1e41f5ee1bef461fa67fe8b053bed05c', N'在制品信息', N'Billing_ProductsingInfo', N'在制品信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'576d19566b8c48078fe05e5131956616', N'1e41f5ee1bef461fa67fe8b053bed05c', N'衣架信息', N'Billing_CoatHanger', N'衣架信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'cc1102076eb541338af41222a3954566', N'e182ff1816e8454c8c0c1c9788872478', N'统计报表', N'barBtn_RealTimeInformation', N'统计报表', 3, 1, CAST(5.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'784e66f1c75546939b42aa5dec1e92f3', N'cc1102076eb541338af41222a3954566', N'产量汇总', N' ', N'产量汇总', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9fd9dd41f0774f219b7abe1db0e8117e', N'cc1102076eb541338af41222a3954566', N'员工产量报表', N' ', N'员工产量报表', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'44b487507fc847aea9220ae0bd6d0ef8', N'cc1102076eb541338af41222a3954566', N'生产进度报表', N' ', N'生产进度报表', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'bd5940202d9d490db30f0da8fa2b8c5b', N'cc1102076eb541338af41222a3954566', N'工时分析报表', N' ', N'工时分析报表', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'68428f0c136d4717b0560004533c576a', N'cc1102076eb541338af41222a3954566', N'制单工序交叉报表', N' ', N'制单工序交叉报表', 2, 2, CAST(5.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'ff17d95cc2564590b056a5224c1dd29b', N'cc1102076eb541338af41222a3954566', N'产出明细报表', N' ', N'产出明细报表', 2, 2, CAST(6.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'47f9e8b3a6c14f618979155609d2693b', N'cc1102076eb541338af41222a3954566', N'返工详情报表', N' ', N'返工详情报表', 2, 2, CAST(7.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b546e76e66984f15879c21f9c8d171d7', N'cc1102076eb541338af41222a3954566', N'返工汇总&疵点分析报表', N' ', N'返工汇总&疵点分析报表', 2, 2, CAST(8.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'83437ba19b6b4df1bba712c60fc79f5c', N'cc1102076eb541338af41222a3954566', N'组别竞赛报表', N' ', N'组别竞赛报表', 2, 2, CAST(9.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'23687241c5524bf18afd38c0b8bc7350', N'e182ff1816e8454c8c0c1c9788872478', N'订单信息', N'barBtn_OrderInfo', N'订单信息', 3, 1, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c0775ae3685c4b89a236ae077e86de94', N'23687241c5524bf18afd38c0b8bc7350', N'客户信息', N'OrderInfo_CustomerInfo', N'客户信息', 2, 1, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c1941a976a69489bba7b6b31abb57468', N'23687241c5524bf18afd38c0b8bc7350', N'客户订单', N'OrderInfo_CustomerOrderInfo', N'客户订单', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8f8f1003245b45a1885b96100327fc7a', N'e182ff1816e8454c8c0c1c9788872478', N'制单信息', N'barBtn_billing', N'制单信息', 3, 1, CAST(5.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'09eb98efa6bf405c8381eb59ef23e133', N'8f8f1003245b45a1885b96100327fc7a', N'生产制单', N'Billing_ProcessOrderIndex', N'生产制单', 2, 3, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd6a8b1dc95ad4edda42ee5715aea8454', N'09eb98efa6bf405c8381eb59ef23e133', N'全屏', N'Billing_ProcessOrderIndex.btnMax', N'生产制单.全屏', 3, 4, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'8128ddba7f9b4caab41e01704b650ed4', N'09eb98efa6bf405c8381eb59ef23e133', N'刷新', N'Billing_ProcessOrderIndex.btnRefresh', N'生产制单.刷新', 3, 4, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'36ed8ef68aca4ae1b084c07fe80f5122', N'09eb98efa6bf405c8381eb59ef23e133', N'新增', N'Billing_ProcessOrderIndex.btnAdd', N'生产制单.新增', 3, 4, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'63eca0b8ce284dbe9d76b471e39be402', N'09eb98efa6bf405c8381eb59ef23e133', N'退出', N'Billing_ProcessOrderIndex.btnClose', N'生产制单.退出', 3, 4, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e5ad7b144d5b4d93bc70131210f9c80d', N'09eb98efa6bf405c8381eb59ef23e133', N'删除', N'Billing_ProcessOrderIndex.btnDelete', N'生产制单.删除', 3, 4, CAST(5.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'07dbd9c98a8c4700976259aebd51f1a0', N'09eb98efa6bf405c8381eb59ef23e133', N'导出', N'Billing_ProcessOrderIndex.btnExport', N'生产制单.', 3, 4, CAST(6.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'49bd237f9ea64a128b8c0006b1b293ab', N'8f8f1003245b45a1885b96100327fc7a', N'制单工序', N'Billing_ProcessFlowIndex', N'制单工序', 2, 4, CAST(7.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'13442f28057e46758e66e933061acef1', N'8f8f1003245b45a1885b96100327fc7a', N'工艺路线图', N'Billing_ProcessFlowChartIndex', N'工艺路线图', 2, 3, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4de34b96386249b583746896006e53c9', N'e182ff1816e8454c8c0c1c9788872478', N'产品基础数据', N'barBtn_ProductBaseData', N'产品基础数据', 3, 1, CAST(6.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b5d63a5fb0a742ef9813c5aaf08a9a64', N'4de34b96386249b583746896006e53c9', N'产品部位', N'ProductBaseData_ProductPart', N'产品部位', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'939f281e6d914a71b9dc6467a437d2cf', N'4de34b96386249b583746896006e53c9', N'基本尺码表', N'ProductBaseData_BasicSizeTable', N'基本尺码表', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'18007b461ebc40238379444207a108ff', N'4de34b96386249b583746896006e53c9', N'基本颜色表', N'ProductBaseData_BasicColorTable', N'基本颜色表', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c5b833de5ff5497c98ae808857359e1a', N'4de34b96386249b583746896006e53c9', N'款式工艺表', N'ProductBaseData_Style', N'款式工艺表', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7d7ab9d2bab24350985ebae133ebdb65', N'e182ff1816e8454c8c0c1c9788872478', N'工艺基础数据', N'barBtn_ProcessBaseData', N'工艺基础数据', 3, 1, CAST(7.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e459794194e04ddd85f98e730bcdb631', N'7d7ab9d2bab24350985ebae133ebdb65', N'基本工序段', N'ProcessBaseData_BasicProcessSection', N'基本工序段', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'293d219c1a8c4898ad2db88f21235e80', N'7d7ab9d2bab24350985ebae133ebdb65', N'基本工序库', N'ProcessBaseData_BasicProcessLirbary', N'基本工序库', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'6b3318ec89614dc2ae68c83f84807b2f', N'7d7ab9d2bab24350985ebae133ebdb65', N'款式工序库', N'ProcessBaseData_StyleProcessLirbary', N'款式工序库', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e6a8cb6a2b424bfda54f87731610835e', N'7d7ab9d2bab24350985ebae133ebdb65', N'疵点代码', N'ProcessBaseData_DefectCode', N'疵点代码', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'01d00c4aa59a4994a36e4cd4d35cb4f7', N'7d7ab9d2bab24350985ebae133ebdb65', N'缺料代码', N'ProcessBaseData_LackOfMaterialCode', N'缺料代码', 2, 2, CAST(5.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'87eb1bc160564b9a896c628732680d31', N'e182ff1816e8454c8c0c1c9788872478', N'生产线', N'barBtn_ProductionLine', N'生产线', 3, 1, CAST(8.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2f7106c8cbf242eea7e3de51c9caa2ca', N'87eb1bc160564b9a896c628732680d31', N'控制端配置', N'ProductionLine_ControlSet', N'控制端配置', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'a2733a5772624a968947180a71e51d57', N'87eb1bc160564b9a896c628732680d31', N'流水线管理', N'ProductionLine_PipelineMsg', N'流水线管理', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'3496fe67dc70421cae8741f38a304b2e', N'87eb1bc160564b9a896c628732680d31', N'桥接配置', N'ProductionLine_BridgingSet', N'桥接配置', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c740a2069ffc41bebaff48caeed13d1e', N'87eb1bc160564b9a896c628732680d31', N'客户机信息', N'ProductionLine_ClientInfo', N'客户机信息', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'4c0644ead7664875b4ff376a39b093b1', N'87eb1bc160564b9a896c628732680d31', N'系统参数', N'ProductionLine_SystemMsg', N'系统参数', 2, 2, CAST(5.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b5305553b4e44eafa5e227c5335d761d', N'87eb1bc160564b9a896c628732680d31', N'Tcp测试', N'ProductionLine_TcpTest', N'Tcp测试', 2, 2, CAST(6.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'e4b0da7a334649bf89827d207ba02bd8', N'e182ff1816e8454c8c0c1c9788872478', N'衣车管理', N'barBtn_ClothingCarManagement', N'衣车管理', 3, 1, CAST(8.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c424c0cd30c2424d97e2e71a3530d85d', N'e4b0da7a334649bf89827d207ba02bd8', N'衣车类别表', N'ClothingCarManagement_EwingMachineType', N'衣车类别表', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f8a5bff164684dbaa201d73d536ff096', N'e4b0da7a334649bf89827d207ba02bd8', N'故障代码表', N'ClothingCarManagement_FalutCodeTable', N'故障代码表', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'572d6c49915c42858fefb0b1c506ce2d', N'e4b0da7a334649bf89827d207ba02bd8', N'衣车资料', N'ClothingCarManagement_SewingMachineData', N'衣车资料', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'c55a3e5ce02842a292b918f27a4d12d9', N'e4b0da7a334649bf89827d207ba02bd8', N'机修人员表', N'ClothingCarManagement_MechanicEmployee', N'机修人员表', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'92325ac532a942aea7661f46d9613d76', N'e182ff1816e8454c8c0c1c9788872478', N'人事管理', N'barBtn_PersonnelManagement', N'人事管理', 3, 1, CAST(9.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'7957903936964994982d861e193a00cb', N'92325ac532a942aea7661f46d9613d76', N'生产组别', N'PersonnelManagement_ProductGroup', N'生产组别', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2afe51840eb6412b8c38b1059355d880', N'92325ac532a942aea7661f46d9613d76', N'部门信息', N'PersonnelManagement_DepartmentInfo', N'部门信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'35b0cc9481d14947b7f3fc7cab17b503', N'92325ac532a942aea7661f46d9613d76', N'工种信息', N'PersonnelManagement_ProfessionInfo', N'工种信息', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'351a56364eb24668a657bbbd7645244e', N'92325ac532a942aea7661f46d9613d76', N'职务信息', N'PersonnelManagement_PositionInfo', N'职务信息', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'24c91ce5636c4e6788bc9f466343e0ea', N'92325ac532a942aea7661f46d9613d76', N'员工资料', N'PersonnelManagement_EmployeeInfo', N'员工资料', 2, 2, CAST(5.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'd86e7b765e284f559ed9ce7e384ea6f7', N'e182ff1816e8454c8c0c1c9788872478', N'裁床管理', N'barBtn_CuttingRoomManage', N'裁床管理', 3, 1, CAST(5.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'b52109414ca7459d9af5f3d6dcb31392', N'92325ac532a942aea7661f46d9613d76', N'管理卡信息', N'PersonnelManagement_MsgCardInfo', N'管理卡信息', 2, 2, CAST(6.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'07cbc4f7d9d94645a07959613cdfa961', N'e182ff1816e8454c8c0c1c9788872478', N'权限管理', N'barBtn_AuthorityManagement', N'权限管理', 3, 1, CAST(10.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'89982e179e6f437b826938b617aa31ed', N'07cbc4f7d9d94645a07959613cdfa961', N'菜单管理', N'AuthorityManagement_ModuleMsg', N'菜单管理', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'21f095d8cab34b40973b349e72359c84', N'07cbc4f7d9d94645a07959613cdfa961', N'角色管理', N'AuthorityManagement_RoleMsg', N'角色管理', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'2ec84d4822dd4e63a4296302fd1e4e8c', N'07cbc4f7d9d94645a07959613cdfa961', N'用户管理', N'AuthorityManagement_UserMsg', N'用户管理', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'f8f8e3e11b6e434f88fb346ba798aeec', N'07cbc4f7d9d94645a07959613cdfa961', N'用户操作日志', N'AuthorityManagement_UserOperatorLog', N'用户操作日志', 2, 2, CAST(4.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9f98aada061f420dbad806fa848d4b73', N'e182ff1816e8454c8c0c1c9788872478', N'考勤管理', N'barBtn_AttendanceManagement', N'考勤管理', 3, 1, CAST(11.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'9c68042b03a54117922eb9344b43b165', N'9f98aada061f420dbad806fa848d4b73', N'用户管理', N'AttendanceManagement_HolidayInfo', N'用户管理', 2, 2, CAST(1.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'1f2f48cfd25143cfb2befa2777c2f746', N'9f98aada061f420dbad806fa848d4b73', N'班次信息', N'AttendanceManagement_ClasssesInfo', N'班次信息', 2, 2, CAST(2.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
INSERT [dbo].[Modules] ([Id], [MODULES_Id], [ActionName], [ActionKey], [Description], [ModulesType], [ModuleLevel], [OrderField], [InsertDateTime], [InsertUser], [UpdateDateTime], [UpdateUser], [Deleted], [CompanyId]) VALUES (N'dc051d83ff0542e9bb8cf2cc848368db', N'9f98aada061f420dbad806fa848d4b73', N'员工排班表', N'AttendanceManagement_EmployeeScheduling', N'员工排班表', 2, 2, CAST(3.00 AS Decimal(5, 2)), CAST(0x0000A92201740774 AS DateTime), N'                                ', NULL, NULL, 0, N'c001                            ')
