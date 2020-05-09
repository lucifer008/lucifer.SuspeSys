USE [SyspeSysV2.0]
GO

/****** Object:  View [dbo].[v_Hanger_Product_Item]    Script Date: 2019/1/21 17:11:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_Hanger_Product_Item]
AS
SELECT   Res_Main.InsertDateTime, Res_Main.EmployeeName, Res_Main.ProcessOrderNo, Res_Main.PColor, Res_Main.PSize, 
                Res_Main.ProcessFlowCode, Res_Main.ProcessFlowName, Res_Main.SiteNo, Res_Main.PurchaseOrderNo AS PO, 
                Res_Main.FlowSection, Res_Main.FlowIndex, Res_Main.ProcessFlowId, Res_Main.StanardHours, 
                Res_Main.StandardPrice, Res_Main.GroupNo, Res_Main.StyleNo, Res_Out.YieldCount, Res_Rework.ReworkCount, 
                Res_Out.RealyWorkMin, Res_Out.UnitCunt, Res_Main.WorName
FROM      (SELECT DISTINCT 
                                 CONVERT(varchar(10), T1.InsertDateTime, 120) AS InsertDateTime, T1.EmployeeName, T1.ProcessOrderNo, 
                                 T1.PColor, T1.PSize, T1.FlowCode AS ProcessFlowCode, T1.FlowName AS ProcessFlowName, 
                                 T1.StatingNo AS SiteNo, T4.PurchaseOrderNo, T5.FlowSection, T1.FlowIndex, T1.FlowId AS ProcessFlowId, 
                                 T8.StanardMinute AS StanardHours, T8.StandardPrice, T5.GroupNo, T5.StyleNo, WP.WorName, 
                                 T1.InsertDateTime AS Expr1
                 FROM      dbo.HangerProductFlowChart AS T1 LEFT OUTER JOIN
                                 dbo.ProcessOrder AS T2 ON T1.ProcessOrderNo = T2.POrderNo LEFT OUTER JOIN
                                 dbo.ProcessOrderColorItem AS T3 ON T3.PROCESSORDER_Id = T2.Id LEFT OUTER JOIN
                                 dbo.CustomerPurchaseOrder AS T4 ON T4.Id = T3.CUSTOMERPURCHASEORDER_Id LEFT OUTER JOIN
                                 dbo.Products AS T5 ON T5.Id = T1.ProductsId LEFT OUTER JOIN
                                 dbo.ProcessFlowChart AS T6 ON T6.Id = T1.ProcessChartId LEFT OUTER JOIN
                                 dbo.ProcessFlow AS T8 ON T8.Id = T1.FlowId LEFT OUTER JOIN
                                 dbo.SiteGroup AS SG ON SG.GroupNO = T5.GroupNo LEFT OUTER JOIN
                                 dbo.Workshop AS WP ON SG.WORKSHOP_Id = WP.Id
                 WHERE   (T1.Status = 2)) AS Res_Main LEFT OUTER JOIN
                    (SELECT   InsertDateTime, EmployeeName, ProcessOrderNo, PColor, PSize, ProcessFlowCode, ProcessFlowName, 
                                     SiteNo, FlowSection, ProcessFlowId, GroupNo, COUNT(*) AS YieldCount, SUM(RealyWorkMin) 
                                     AS RealyWorkMin, COUNT(*) * SUM(Unit) AS UnitCunt
                     FROM      (SELECT   CONVERT(varchar(10), T1.InsertDateTime, 120) AS InsertDateTime, T1.EmployeeName, 
                                                      T1.ProcessOrderNo, T1.PColor, T1.PSize, T1.FlowCode AS ProcessFlowCode, 
                                                      T1.FlowName AS ProcessFlowName, T1.StatingNo AS SiteNo, T5.FlowSection, T1.FlowIndex, 
                                                      T1.FlowId AS ProcessFlowId, T8.StanardHours, T8.StandardPrice, T5.GroupNo, 
                                                      DATEDIFF(second, T1.CompareDate, T1.OutSiteDate) AS RealyWorkMin, CAST(T5.Unit AS int) 
                                                      AS Unit
                                      FROM      dbo.HangerProductFlowChart AS T1 LEFT OUTER JOIN
                                                      dbo.Products AS T5 ON T5.Id = T1.ProductsId LEFT OUTER JOIN
                                                      dbo.ProcessFlowChart AS T6 ON T6.Id = T1.ProcessChartId LEFT OUTER JOIN
                                                      dbo.ProcessFlow AS T8 ON T8.Id = T1.FlowId
                                      WHERE   (T1.Status = 2) AND (T1.FlowType IN (0, 2)) AND (T1.HangerNo > 0)) AS T_OUt
                     GROUP BY InsertDateTime, EmployeeName, ProcessOrderNo, PColor, PSize, ProcessFlowCode, ProcessFlowName, 
                                     SiteNo, FlowSection, ProcessFlowId, GroupNo) AS Res_Out ON 
                Res_Main.InsertDateTime = Res_Out.InsertDateTime AND Res_Main.EmployeeName = Res_Out.EmployeeName AND 
                Res_Main.ProcessOrderNo = Res_Out.ProcessOrderNo AND Res_Main.PColor = Res_Out.PColor AND 
                Res_Main.PSize = Res_Out.PSize AND Res_Main.ProcessFlowCode = Res_Out.ProcessFlowCode AND 
                Res_Main.ProcessFlowName = Res_Out.ProcessFlowName AND Res_Main.SiteNo = Res_Out.SiteNo AND 
                Res_Main.FlowSection = Res_Out.FlowSection AND Res_Main.ProcessFlowId = Res_Out.ProcessFlowId AND 
                Res_Main.GroupNo = Res_Out.GroupNo LEFT OUTER JOIN
                    (SELECT   InsertDateTime, EmployeeName, ProcessOrderNo, PColor, PSize, ProcessFlowCode, ProcessFlowName, 
                                     SiteNo, FlowSection, ProcessFlowId, GroupNo, COUNT(*) AS ReworkCount
                     FROM      (SELECT   CONVERT(varchar(10), T1.InsertDateTime, 120) AS InsertDateTime, T1.EmployeeName, 
                                                      T1.ProcessOrderNo, T1.PColor, T1.PSize, T1.FlowCode AS ProcessFlowCode, 
                                                      T1.FlowName AS ProcessFlowName, T1.StatingNo AS SiteNo, T5.FlowSection, T1.FlowIndex, 
                                                      T1.FlowId AS ProcessFlowId, T8.StanardHours, T8.StandardPrice, T5.GroupNo
                                      FROM      dbo.HangerProductFlowChart AS T1 LEFT OUTER JOIN
                                                      dbo.Products AS T5 ON T5.Id = T1.ProductsId LEFT OUTER JOIN
                                                      dbo.ProcessFlowChart AS T6 ON T6.Id = T1.ProcessChartId LEFT OUTER JOIN
                                                      dbo.ProcessFlow AS T8 ON T8.Id = T1.FlowId
                                      WHERE   (T1.Status = 2) AND (T1.FlowType = 1) AND (T1.HangerNo > 0)) AS T_Rework
                     GROUP BY InsertDateTime, EmployeeName, ProcessOrderNo, PColor, PSize, ProcessFlowCode, ProcessFlowName, 
                                     SiteNo, FlowSection, ProcessFlowId, GroupNo) AS Res_Rework ON 
                Res_Main.InsertDateTime = Res_Rework.InsertDateTime AND 
                Res_Main.EmployeeName = Res_Rework.EmployeeName AND 
                Res_Main.ProcessOrderNo = Res_Rework.ProcessOrderNo AND Res_Main.PColor = Res_Rework.PColor AND 
                Res_Main.PSize = Res_Rework.PSize AND Res_Main.ProcessFlowCode = Res_Rework.ProcessFlowCode AND 
                Res_Main.ProcessFlowName = Res_Rework.ProcessFlowName AND Res_Main.SiteNo = Res_Rework.SiteNo AND 
                Res_Main.FlowSection = Res_Rework.FlowSection AND Res_Main.ProcessFlowId = Res_Rework.ProcessFlowId AND 
                Res_Main.GroupNo = Res_Rework.GroupNo

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[6] 4[32] 2[36] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Res_Out"
            Begin Extent = 
               Top = 6
               Left = 271
               Bottom = 146
               Right = 466
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Res_Rework"
            Begin Extent = 
               Top = 6
               Left = 504
               Bottom = 146
               Right = 699
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Res_Main"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 233
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 23
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3240
         Alias = 3585
         Table = 4545
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         O' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Hanger_Product_Item'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'r = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Hanger_Product_Item'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Hanger_Product_Item'
GO


