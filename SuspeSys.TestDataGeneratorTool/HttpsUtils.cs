using log4net;
using Newtonsoft.Json;
using SuspeSys.Utils.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.TestDataGeneratorTool
{
    public class HttpsUtils
    {
        static readonly string appId = "20190514000297454";
        static readonly string sign = "8HjZAp0M4STtTaljnXuK";
        static readonly string baiduFanYiApi = "http://api.fanyi.baidu.com/api/trans/vip/translate?";
        private static readonly ILog log = LogManager.GetLogger(typeof(HttpsUtils));
        public static string Get(string url, string requestBody = null)
        {

            Uri address = new Uri(url);

            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            request.Method = "Get";
            //string requestBody = // create booking request content

            if (null != requestBody)
            {
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(requestBody);
                request.ContentLength = byteData.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(byteData, 0, byteData.Length);
                }
            }
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string location = response.Headers["Location"];
                    if (location != null)
                    {
                        Console.WriteLine("Created new booking at: " + location);
                    }
                    //获取响应内容
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        var result = reader.ReadToEnd();
                        return result;
                    }
                }
            }
            return null;
        }

        public static string Translate(string content, string lanKey)
        {
            try
            {
                //long salt = DateTime.Now.Ticks;
                //var p = string.Format("{0}{1}{2}{3}", appId, content, salt, sign);
                ////  UTF8Encoding.UTF8.GetString(content);
                //var pData = System.Text.ASCIIEncoding.Default.GetBytes(p);
                //var pU8 = System.Text.UTF8Encoding.Default.GetBytes(p);
                //var resultSign = MD5.Encrypt(p);
                //var reqParams = string.Format("{0}q={1}&from=zh&to={2}&appid={3}&salt={4}&sign={5}", baiduFanYiApi, content, lanKey, appId, salt, resultSign);
                //return Get(reqParams);
                if (string.IsNullOrEmpty(content)) return "";
                var rT = HttpsUtils.Translate<BaiDuResult>(content, lanKey);
                if (rT == null) {
                    log.ErrorFormat($"接口翻译异常!content=>{content} lanKey=>{lanKey}");
                    return null;
                }
                return rT.trans_result.Count > 0 ? rT.trans_result[0].dst : "";
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return "";
            }
        }
        public static T Translate<T>(string content, string lanKey)
        {
            // Thread.Sleep(3000);
            long salt = DateTime.Now.Ticks;
            var p = string.Format("{0}{1}{2}{3}", appId, content, salt, sign);
            //  UTF8Encoding.UTF8.GetString(content);
            var pData = System.Text.ASCIIEncoding.Default.GetBytes(p);
            var pU8 = System.Text.UTF8Encoding.Default.GetBytes(p);
            var resultSign = MD5.Encrypt(p);
            var reqParams = string.Format("{0}q={1}&from=zh&to={2}&appid={3}&salt={4}&sign={5}", baiduFanYiApi, content, lanKey, appId, salt, resultSign);
            var requestResult = Get(reqParams);
            return JsonConvert.DeserializeObject<T>(requestResult);

        }

    }
    class BaiDuResult
    {
        public string from { set; get; }
        public string to { set; get; }
        public List<trans_result> trans_result { set; get; }
    }
    class trans_result
    {
        public string src { set; get; }
        public string dst { set; get; }
    }
}
