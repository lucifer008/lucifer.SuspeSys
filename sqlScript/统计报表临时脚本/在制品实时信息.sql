SELECT 
            Res_Main.GroupNO,Res_Main.StatingNo,Res_Main.Capacity,Res_Main.MainTrackNumber,
            Res_FlowChart.IsReceivingHanger,Res_FlowChart.TotalStanardHours,
            Res_LoginStatInfo.Code,Res_LoginStatInfo.RealName
            ,(
				select COUNT(*) from HangerProductFlowChart
				WHERE (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				AND CONVERT(varchar(10), 
				DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				AND MainTrackNumber=Res_Main.MainTrackNumber 
				And EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=0
				AND ProcessChartId in('{0}')
            ) TodayOutAll,
            (select COUNT(*) from HangerProductFlowChart
				WHERE (InsertDateTime BETWEEN CONVERT(varchar(10), GETDATE(), 120) 
				AND CONVERT(varchar(10), 
				DATEADD(day, 1, GETDATE()), 120)) AND StatingNo=Res_Main.StatingNo 
				AND MainTrackNumber=Res_Main.MainTrackNumber And 
				EmployeeName=Res_LoginStatInfo.RealName And Status=2 And FlowType=1
				AND ProcessChartId in('{0}')
            )TodayReworkAll,
            (
				select SUM(ISNULL(ReHours,0)) ReHours from (
				select DATEDIFF (second , CompareDate , OutSiteDate )ReHours  
				from HangerProductFlowChart where Status=2 And FlowType=0 and ProcessChartId='{0}'
				AND StatingNo=Res_Main.StatingNo 
				union all 
				select DATEDIFF ( second  , CompareDate , OutSiteDate )ReHours 
				from SuccessHangerProductFlowChart where Status=2 And FlowType=0
				AND  ProcessChartId='{0}'
				AND StatingNo=Res_Main.StatingNo 
				)Res_ReailHours
            ) ReailHours,Res_FlowChart.TotalStanardHours
 FROM(
select T1.GroupNO,T2.StatingNo,T1.MainTrackNumber,T2.Capacity 
 from SiteGroup T1
INNER JOIN Stating T2 ON T2.SITEGROUP_Id=T1.Id
)Res_Main
INNER JOIN(
    select T.SiteGroupNo,T.IsReceivingHanger,T.No,SUM(CONVERT(int,ISNULL(T3.StanardHours,0))) TotalStanardHours
    from ProcessFlowStatingItem T 
    INNER JOIN ProcessFlowChartFlowRelation T2 ON T2.ID=T.PROCESSFLOWCHARTFLOWRELATION_Id
    INNER JOIN ProcessFlow T3 ON T3.Id=t2.PROCESSFLOW_Id
    WHERE T2.PROCESSFLOWCHART_Id='{0}'
    Group by T.SiteGroupNo,T.IsReceivingHanger,T.No
) Res_FlowChart ON Res_FlowChart.SiteGroupNo=Res_Main.GroupNO 
and Res_FlowChart.No=Res_Main.StatingNo

LEFT JOIN(
	select TC.MainTrackNumber,TC.LoginStatingNo,EM.Code,Em.RealName from [dbo].[CardLoginInfo] TC
	LEFT JOIN EmployeeCardRelation EC ON EC.CARDINFO_Id=TC.CARDINFO_Id
	LEFT JOIN Employee EM ON EM.Id=EC.EMPLOYEE_Id
	WHERE IsOnline=1
) Res_LoginStatInfo ON Res_LoginStatInfo.LoginStatingNo=Res_Main.StatingNo 
AND Res_LoginStatInfo.MainTrackNumber=Res_Main.MainTrackNumber

WHERE 1=1 


select * from SuccessHangerProductFlowChart
select * from Products
select * from ProcessFlowStatingItem
select * from ProcessFlowChartFlowRelation
select * from ProcessFlow