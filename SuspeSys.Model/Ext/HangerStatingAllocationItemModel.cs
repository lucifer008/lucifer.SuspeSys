﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    public class HangerStatingAllocationItemModel: HangerStatingAllocationItem
    {
        public virtual string ProcessFlowChartFlowRelationId { set; get; }
        public virtual string MergeProcessFlowChartFlowRelationId { set; get; }
    }
}