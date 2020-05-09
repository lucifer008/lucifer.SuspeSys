namespace SuspeSys.SusRedis.SusRedis.SusConst
{
    public class SusRedisConst
    {
        /// <summary>
        /// 在线制品
        /// </summary>
        public const string ON_LINE_PRODUCTS = "ON_LINE_PRODUCTS";

        /// <summary>
        /// 制品
        /// </summary>
        public const string PRODUCTS = "PRODUCTS";
        /// <summary>
        /// 在线制品工艺图
        /// </summary>
        public const string ON_LINE_PRODUCTS_FLOW_CHART = "ON_LINE_PRODUCTS_FLOW_CHART";
        /// <summary>
        /// 等待生产的衣架
        /// </summary>
        public const string WAIT_PROCESS_ORDER_HANGER = "WAIT_PROCESS_ORDER_HANGER";

        /// <summary>
        /// 衣架工艺图
        /// </summary>
        public const string HANGER_PROCESS_FLOW_CHART = "HANGER_PROCESS_FLOW_CHART";

        /// <summary>
        /// 衣架分配明细
        /// </summary>
        public const string HANGER_ALLOCATION_ITME = "HANGER_ALLOCATION_ITME";


        /// <summary>
        /// 衣架分配明细动作记录到数据库
        /// </summary>
        public const string HANGER_ALLOCATION_ITME_DB_RECORD_ACTION = "HANGER_ALLOCATION_ITME_ACTION";

        /// <summary>
        /// 衣架挂片请求队列
        /// </summary>
        public const string HANGER_HNGING_PIECE_REQUEST_ACTION = "HANGER_HNGING_PIECE_REQUEST_ACTION";


        /// <summary>
        ///更新衣架站点工序生产信息
        /// </summary>
        public const string UPDATE_HANGER_PROCESS_FLOW_CHART_ACTION = "UPDATE_HANGER_PROCESS_FLOW_CHART_ACTION";

        /// <summary>
        /// 记录衣架生产明细
        /// </summary>
        public const string RECORD_STATING_HANGER_PROCESS_ITEM_ACTION = "RECORD_STATING_HANGER_PROCESS_ITEM_ACTION";

        /// <summary>
        /// 更新待生产记录
        /// </summary>
        public const string UPDATE_WAIT_PROCESS_ORDER_HANGER_ACTION = "UPDATE_WAIT_PROCESS_ORDER_HANGER_ACTION";

        public const string UPDATE_HANGER_PRODUCT_ITEM_ACTION = "UPDATE_HANGER_PRODUCT_ITEM_ACTION";

        /// <summary>
        /// 进站消息
        /// </summary>
        public const string HANGER_IN_SITE_ACTION = "HANGER_IN_SITE_ACTION";
        /// <summary>
        /// 出站消息
        /// </summary>
        public const string HANGER_OUT_SITE_ACTION = "HANGER_OUT_SITE_ACTION";
        /// <summary>
        /// 【衣架履历入库Action】
        /// </summary>
        public const string HANGER_RESUME_ACTION = "HANGER_RESUME_ACTION";
        /// <summary>
        /// 【衣架分配入库Action】
        /// </summary>
        public const string HANGER_AOLLOCATION_ACTION = "HANGER_AOLLOCATION_ACTION";

        /// <summary>
        /// 挂片站出站触发
        /// </summary>
        public const string HANGER_OUT_HANGING_STATION_ACTION = "HANGER_OUT_HANGING_STATION_ACTION";

        /// <summary>
        /// 站状态:满站与否
        /// </summary>
        public const string MAINTRACK_STATING_STATUS = "MAINTRACK_STATING_STATUS";
        /// <summary>
        /// 分配数
        /// </summary>
        public const string MAINTRACK_STATING_ALLOCATION_NUM = "MAINTRACK_STATING_ALLOCATION_NUM";
        /// <summary>
        /// 在线数
        /// </summary>
        public const string MAINTRACK_STATING_ONLINE_NUM = "MAINTRACK_STATING_ONLINE_NUM";
        
        /// <summary>
        /// 站内数
        /// </summary>
        public const string MAINTRACK_STATING_IN_NUM = "MAINTRACK_STATING_IN_NUM";

        /// <summary>
        /// 站内衣架集合
        /// </summary>
        public const string MAINTRACK_STATING_IN_HANGER= "MAINTRACK_STATING_IN_HANGER";


        /// <summary>
        /// 疵点代码
        /// </summary>
        public const string DEFECT_CODE_TABLE = "DEFECT_CODE_TABLE";

        /// <summary>
        /// 站点
        /// </summary>
        public const string STATING_TABLE = "STATING_TABLE";

        /// <summary>
        /// 用户角色
        /// </summary>
        public const string USER_ROLE_TABLE = "USER_ROLE_TABLE";

        /// <summary>
        /// 衣架生产明细
        /// </summary>
        public const string HANGER_PRODUCT_ITEM = "HANGER_PRODUCT_ITEM";

        /// <summary>
        /// 衣架生产明细扩展
        /// </summary>
        public const string HANGER_PRODUCT_ITEM_EXT = "HANGER_PRODUCT_ITEM_EXT";

        /// <summary>
        /// 站点生产明细
        /// </summary>
        public const string STATING_HANGER_PRODUCT_ITEM = "STATING_HANGER_PRODUCT_ITEM";

        /// <summary>
        /// 主轨监测点消息
        /// </summary>
        public const string MAINTRACK_STATING_MONITOR_ACTION = "MAINTRACK_STATING_MONITOR_ACTION";

        /// <summary>
        /// 衣架返工缓存队列
        /// </summary>
        public const string HANGER_REWORK_REQUEST_QUEUE = "HANGER_REWORK_REQUEST_QUEUE";
        /// <summary>
        /// 衣架返工工序及疵点缓存
        /// </summary>
        public const string HANGER_REWORK_REQUEST_FLOW_OR_DEFECT_QUEUE = "HANGER_REWORK_REQUEST_FLOW_OR_DEFECT_QUEUE";
        /// <summary>
        /// 衣架返工缓存请求消息
        /// </summary>
        public const string HANGER_REWORK_REQUEST_QUEUE_ACTION = "HANGER_REWORK_REQUEST_QUEUE_ACTION";
        /// <summary>
        /// 衣架返工工序及疵点缓存消息
        /// </summary>
        public const string HANGER_REWORK_REQUEST_FLOW_OR_DEFECT_QUEUE_ACTION = "HANGER_REWORK_REQUEST_FLOW_OR_DEFECT_QUEUE_ACTION";

        /// <summary>
        ///站内数,分配数及缓存修正Action
        /// </summary>
        public const string HANGER_STATION_IN_OR_ALLOCATION_ACTION = "HANGER_STATION_IN_OR_ALLOCATION_ACTION";
        /// <summary>
        ///站内数,分配数缓存
        /// </summary>
        public const string HANGER_STATION_IN_OR_ALLOCATION = "HANGER_STATION_IN_OR_ALLOCATION";

        /// <summary>
        /// 站点信息消息
        /// </summary>
        public const string STATING_EDIT_ACTION = "STATING";

        //员工相关
       // public const string STATING_EMPLOYEE = "STATING_EMPLOYEE";
      //  public const string STATING_EMPLOYEE_ACTION = "STATING_EMPLOYEE_ACTION";
        public const string CARD_INFO = "CARD_INFO";
      //  public const string EMPLOYEE_CARD_RELATION = "EMPLOYEE_CARD_RELATION"; //EmployeeCardRelation
        //public const string EMPLOYEE_CARD_RELATION_ACTION = "EMPLOYEE_CARD_RELATION_ACTION";
        public const string PRODUCT_SUCCESS_COPY_DATA_ACTION = "PRODUCT_SUCCESS_COPY_DATA_ACTION";

        // HangerProductItem

        /// <summary>
        /// 流水线站点关系
        /// </summary>
        public const string PIPELINING_STATING_QUEUE = "PIPELINING_STATING_QUEUE";

        /// <summary>
        /// 系统参数相关
        /// </summary>
        public const string SYSTEM_PARAMETER_QUEUE = "SYSTEM_PARAMETER_QUEUE";

        /// <summary>
        /// 站点登录信息
        /// </summary>
        public const string STATING_LOGIN_INFO = "STATING_LOGIN_INFO";

        /// <summary>
        /// 工艺路线图分配比例缓存
        /// </summary>
        public const string STATING_ALLOCATION = "STATING_ALLOCATION";

        /// <summary>
        /// 工艺路线图分配比例日志
        /// </summary>
        public const string STATING_ALLOCATION_SAVE_CHANGE_ACTION = "STATING_ALLOCATION_SAVE_CHANGE_ACTION";

        /// <summary>
        /// 衣架生产履历
        /// </summary>
        public const string HANGER_PRODUCTS_CHART_RESUME = "HANGER_PRODUCTS_CHART_RESUME";

        /// <summary>
        /// 衣架生产履历Action
        /// </summary>
        public const string HANGER_PRODUCTS_CHART_RESUME_ACTION = "HANGER_PRODUCTS_CHART_RESUME_ACTION";
        /// <summary>
        /// 衣架生产履历 db Action
        /// </summary>
        public const string HANGER_PRODUCTS_CHART_RESUME_DB_ACTION = "HANGER_PRODUCTS_CHART_RESUME_DB_ACTION";
        public const string PUBLIC_TEST = "PUBLIC_TEST";

        /// <summary>
        /// 当前衣架生产的工序
        /// </summary>
        public const string CURRENT_HANGER_PRODUCTS_FLOW = "CURRENT_HANGER_PRODUCTS_FLOW";

        /// <summary>
        /// 衣架工序状态维护Action
        /// </summary>
        public const string CURRENT_HANGER_PRODUCTS_FLOW_ACTION="CURRENT_HANGER_PRODUCTS_FLOW_ACTION";

        /// <summary>
        /// 衣架挂片主轨
        /// </summary>

        public const string HANGER_HANGING_PIECE_MAINTRACK_NUMBER = "HANGER_HANGING_PIECE_MAINTRACK_NUMBER";

        /// <summary>
        /// 桥接设置
        /// </summary>

        public const string BRIDGE_SET = "BRIDGE_SET";

        /// <summary>
        /// 桥接站衣架出战履历
        /// </summary>
        public const string BRIDGE_STATING_HANGER_OUT_SITE_RESUME = "BRIDGE_STATING_HANGER_OUT_SITE_RESUME";
        /// <summary>
        /// 返工需要经过桥接站，缓存下一站列表
        /// </summary>
        public const string BRIDGE_STATING_NEXT_STATING_ITEM = "BRIDGE_STATING_NEXT_STATING_ITEM";

        /// <summary>
        /// 桥接站衣架进站缓存
        /// </summary>
        public const string BRIDGE_STATING_HANGER_IN_COME_ITEM = "BRIDGE_STATING_HANGER_IN_COME_ITEM";

        /// <summary>
        /// 缺料代码表Key
        /// </summary>
        public const string LACK_MATERIALS_TABLE_KEY = "LackMaterialsTable";

        /// <summary>
        /// F2指定缓存
        /// </summary>
        public const string F2_Hanger_Assign_Cache_List = "F2_Hanger_Assign_Cache_List";

        /// <summary>
        /// F2指定缓存
        /// </summary>
        public const string Current_F2_Hanger_Assign_Cache = "Current_F2_Hanger_Assign_Cache";

        /// <summary>
        /// F2指定衣架上传缓存
        /// </summary>
        public const string F2_Hanger_Assign_Upload_Cache = "F2_Hanger_Assign_Upload_Cache";

        /// <summary>
        /// 衣架出战记录
        /// </summary>
        public const string Hanger_Out_Site_Record = "Hanger_Out_Site_Record";

        /// <summary>
        /// 故障代码一级菜单序号和地址对应关系
        /// </summary>
        public const string Fault_Code_AND_First_Address_Mapping = "Fault_Code_AND_First_Address_Mapping";
        /// <summary>
        /// 故障代码二级菜单序号和地址对应关系
        /// </summary>
        public const string Fault_Code_AND_Second_Address_Mapping = "Fault_Code_AND_Second_Address_Mapping";
    }
}
