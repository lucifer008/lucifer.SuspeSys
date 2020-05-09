using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils.Authorization
{
    public class ComputeInfo
    {
        /// <summary>
        /// cpu 序列号
        /// </summary>
        public string CpuId { get; private set; }

        /// <summary>
        /// 硬盘信息
        /// </summary>
        public string DiskId { get; private set; }

        /// <summary>
        /// Mac
        /// </summary>
        public List<string> MacAddress { get; private set; }

        /// <summary>
        /// 就是加名称
        /// </summary>
        public string ComputeName { get; private set; }


        public ComputeInfo()
        {
            this.MacAddress = new List<string>();

            this.getCpuId();
            this.getDiskId();
            this.getMacAddress();
            this.getComputerName();
        }

        private void getCpuId()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    this.CpuId = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
            }
            catch
            {
                this.CpuId = "syspesys-cpuid-unknown";
            }
            finally
            {

            }
        }

        private void getDiskId()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    this.DiskId = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
            }
            catch
            {
                this.DiskId = "syspesys-diskid-unknown";
            }
            finally
            {
            }

        }

        private void getMacAddress()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        this.MacAddress.Add(mo["MacAddress"].ToString().Replace(":","-"));
                        //break;
                    }
                }
                moc = null;
                mc = null;
            }
            catch
            {
                this.MacAddress.Add("syspesys-maxaddress-unknown");
            }
            finally
            {
            }

        }

        private void getComputerName()
        {
            try
            {
                this.ComputeName = System.Environment.MachineName;

            }
            catch
            {
                this.ComputeName = "syspesys-computername-unknown";
            }
            finally
            {
            }
        }
    }
}
