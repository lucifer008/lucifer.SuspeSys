using log4net;
using SuspeSys.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils.Authorization
{
    /// <summary>
    /// 授权检测
    /// </summary>
    public class AuthorizationDetection
    {
        private static readonly ILog log = LogManager.GetLogger("AuthorizationDetection");
        private AuthorizationDetection() { }

        private static AuthorizationDetection _AuthorizationDetection = new AuthorizationDetection();

        public static AuthorizationDetection Instance
        {
            get
            {
                return _AuthorizationDetection;
            }
        }

        /// <summary>
        /// 公司Id
        /// </summary>
        private string CompanyId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CompanyId"];
            }
        }

        public bool Authorization()
        {
            try
            {
                bool result = this.AuthorizationProcess();
                return result;
            }
            catch (BusinessException ex)
            {
                log.Info(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Grant GetGrantInfo(string content)
        {
            Grant grant;
            try
            {
                string plainText = SuspeSys.Utils.Security.RSACrypto.RSADecrypt(content, ReadOnlyData.privateKey);
                grant = Newtonsoft.Json.JsonConvert.DeserializeObject<Grant>(plainText);

            }
            catch (BusinessException ex)
            {

                throw new BusinessException("授权文件不正确,请联系管理员。");
            }

            return grant;
        }

        private bool AuthorizationProcess()
        {
            string path = System.Environment.CurrentDirectory + "\\Grant.Grant";

            if (!System.IO.File.Exists(path))
            {
                throw new BusinessException("授权文件不存在");
            }

            string content = System.IO.File.ReadAllText(path);

            if (string.IsNullOrEmpty(content))
                throw new BusinessException("授权数据不存在");

            Grant grant;
            try
            {
                string plainText = SuspeSys.Utils.Security.RSACrypto.RSADecrypt(content, ReadOnlyData.privateKey);
                grant = Newtonsoft.Json.JsonConvert.DeserializeObject<Grant>(plainText);

            }
            catch (BusinessException ex)
            {

                throw new BusinessException("授权文件不正确");
            }

            DateTime dtCurrent = DateTime.Now;
            if (dtCurrent < grant.Begin || dtCurrent > grant.End)
                throw new BusinessException("授权文件不在有效期内");


            //获取硬件信息
            ComputeInfo computeInfo = new ComputeInfo();
            if (computeInfo.MacAddress.Contains("syspesys-maxaddress-unknown"))
                throw new BusinessException("未获取到Mac信息");

            if (!computeInfo.MacAddress.Contains(grant.Mac))
            {
                string mac = string.Join("\r\n", computeInfo.MacAddress);
                log.Info(mac);
                throw new BusinessException("Mac地址与授权文件不一致，请联系服务商");
            }

            return true;
        }

        public bool Authorization(string content)
        {
            //string path = System.Environment.CurrentDirectory + "\\Grant.Grant";

            //if (!System.IO.File.Exists(path))
            //{
            //    throw new BusinessException("授权文件不存在");
            //}

            //string content = System.IO.File.ReadAllText(path);

            if (string.IsNullOrEmpty(content))
                throw new BusinessException("授权数据不存在");

            Grant grant;
            try
            {
                string plainText = SuspeSys.Utils.Security.RSACrypto.RSADecrypt(content, ReadOnlyData.privateKey);
                grant = Newtonsoft.Json.JsonConvert.DeserializeObject<Grant>(plainText);

            }
            catch (BusinessException ex)
            {

                throw new BusinessException("授权文件不正确");
            }

            DateTime dtCurrent = DateTime.Now;
            if (dtCurrent < grant.Begin || dtCurrent > grant.End)
                throw new BusinessException("授权文件不在有效期内");


            //获取硬件信息
            ComputeInfo computeInfo = new ComputeInfo();
            if (computeInfo.MacAddress.Contains("syspesys-maxaddress-unknown"))
                throw new BusinessException("未获取到Mac信息");

            if (!computeInfo.MacAddress.Contains(grant.Mac))
            {
                string mac = string.Join("\r\n", computeInfo.MacAddress);
                log.Info(mac);
                throw new BusinessException("Mac地址与授权文件不一致，请联系服务商");
            }

            return true;
        }
    }
}
