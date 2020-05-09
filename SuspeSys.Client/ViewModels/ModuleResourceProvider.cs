namespace DevExpress.DevAV.Services {
    public interface IModuleResourceProvider {
        string GetCaption(ModuleType moduleType);
        object GetModuleImage(ModuleType moduleType);
    }
    public class ModuleResourceProvider : IModuleResourceProvider {
        public string GetCaption(ModuleType moduleType) {
            switch(moduleType) {
                case ModuleType.Unknown:
                    return null;
                //case ModuleType.EmployeesPeek:
                //case ModuleType.EmployeesFilterPane:
                //    return "Employees";
                //case ModuleType.CustomersPeek:
                //case ModuleType.CustomersFilterPane:
                //    return "Customers";
                //case ModuleType.ProductsPeek:
                //case ModuleType.ProductsFilterPane:
                //    return "Products";
                //case ModuleType.Orders:
                //case ModuleType.OrdersFilterPane:
                //    return "Sales";
                //case ModuleType.Quotes:
                //case ModuleType.QuotesFilterPane:
                //    return "Opportunities";
                case ModuleType.ProduceData:
                    return "生产资料";
                case ModuleType.Suspe:
                    return "吊挂管理";
                default:
                    return moduleType.ToString();
            }
        }
        public virtual object GetModuleImage(ModuleType moduleType) {
            switch(moduleType) {
                case ModuleType.Employees:
                case ModuleType.EmployeesFilterPane:
                    return SuspeSys.Client.Properties.Resources.icon_nav_employees_32;
                case ModuleType.Customers:
                case ModuleType.CustomersFilterPane:
                    return SuspeSys.Client.Properties.Resources.icon_nav_customers_32;
                case ModuleType.Products:
                case ModuleType.ProductsFilterPane:
                    return SuspeSys.Client.Properties.Resources.icon_nav_products_32;
                case ModuleType.Orders:
                case ModuleType.OrdersFilterPane:
                    return SuspeSys.Client.Properties.Resources.icon_nav_sales_32;
                case ModuleType.Quotes:
                case ModuleType.QuotesFilterPane:
                    return SuspeSys.Client.Properties.Resources.icon_nav_opportunities_32;
                case ModuleType.Unknown:
                default:
                    return null;
            }
        }
    }
}
