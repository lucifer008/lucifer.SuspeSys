using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SusNet.Common.Utils;
using System.Text;

namespace SuspeSys.Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            //十六进制转十进制
            var v1 = System.Convert.ToString(01, 10);
            Console.WriteLine(v1);

            //十六进制转二进制
            var v2 = System.Convert.ToString(01, 2);
            Console.WriteLine(v2);
        }
        [TestMethod]
        public void Test2()
        {
            // string hexValues = "48 65 6C 6C 6F 20 57 6F 72 6C 64 21";
            string hexValues = "00010";
            string[] hexValuesSplit = hexValues.Split(' ');
            foreach (String hex in hexValuesSplit)
            {
                // Convert the number expressed in base-16 to an integer.
                int value = Convert.ToInt32(hex, 16);
                // Get the character corresponding to the integral value.
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                Console.WriteLine("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",
                                    hex, value, stringValue, charValue);
            }
        }
        [TestMethod]
        public void TestGuid()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32);
            Console.WriteLine(guid);
            var hexString = "01 00 03 FF 00 06 00 00 00 00 00 12";
            Console.WriteLine("----->" + HexHelper.StringToHexByte(hexString).Length);
            var t = 255.ToString("X2");
            Console.WriteLine(t);
            Console.WriteLine("HEX--->" + HexHelper.BytesToHexString(HexHelper.StringToHexByte(hexString)));
        }
        [TestMethod]
        public void TestUnicode()
        {
             var data = "1,red,S,2";
           // var data = "蓝";
            var strUnicode = StringToUnicode(data);
            var bs = UnicodeUtils.GetBytesByUnicode(data); ;
            var hexStr = HexHelper.BytesToHexString(bs);
            var hexss = BitConverter.ToString(bs);
            Array.Reverse(bs);
            var hexStr2 = HexHelper.BytesToHexString(bs);
        }
        /// <summary>
        /// 字符串转Unicode码
        /// </summary>
        /// <returns>The to unicode.</returns>
        /// <param name="value">Value.</param>
        private string StringToUnicode(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                // 取两个字符，每个字符都是右对齐。
                stringBuilder.AppendFormat("u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }
        [TestMethod]
        public void Test222()
        {
            int x = 439041118;  // 十六进制为 1A2B3C5E

            string s = null;

            byte[] b = BitConverter.GetBytes(x);

            s = BitConverter.ToString(b); // 小端模式
            Console.WriteLine(s); // 小端输出 为 5E-3C-2B-1A

            Array.Reverse(b); // 反转

            s = BitConverter.ToString(b); // 大端模式
            Console.WriteLine(s); // 大端输出 为 1A-2B-3C-5E

            Console.ReadKey();
        }
        [TestMethod]
        public void Test33() {
            DateTime time = DateTime.Now;
            string s = time.ToString("t\\M");
            //Console.WriteLine(s);
            var ss = string.Format("{0:P2}", 0.01);
            Console.WriteLine(ss);
        }
    }
}
