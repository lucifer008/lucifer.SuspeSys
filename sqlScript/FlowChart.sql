  select T.SiteGroupNo,T2.CraftFlowNo,t.mainTrackNumber,T.IsReceivingHanger,T.No
    from ProcessFlowStatingItem T 
    INNER JOIN ProcessFlowChartFlowRelation T2 ON T2.ID=T.PROCESSFLOWCHARTFLOWRELATION_Id
    INNER JOIN ProcessFlow T3 ON T3.Id=t2.PROCESSFLOW_Id
    WHERE T2.PROCESSFLOWCHART_Id='1ace70bef1d345858e3097b07899f8b8'
  Order by T2.CraftFlowNo