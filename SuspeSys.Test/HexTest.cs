using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SusNet.Common.Utils;
using System.Collections.Generic;
using System.Numerics;
using Sus.Net.Common.Utils;

namespace SuspeSys.Test
{
    [TestClass]
    public class HexTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // var beginHex = "60 00";//24576
            // var endHex = "67 FF";//26623
            var dicProductNumber = new Dictionary<int, List<int>>();
            var i = 1;
            var pNumber = 0;
            var addressList = new List<int>();
            for (var index = 24576; index <= 26623; index++)
            {
                var hexStr = HexHelper.TenToHexString2Len(index);
                Console.WriteLine(index + "--->" + hexStr);
                Console.WriteLine("");
                addressList.Add(index);
                if (i % 8 == 0)
                {
                    int[] aa = new int[8];
                    addressList.CopyTo(aa);
                    dicProductNumber.Add(pNumber, new List<int>(aa));
                    addressList = new List<int>();
                    pNumber++;
                }

                i++;
            }

            Console.WriteLine(dicProductNumber);
            foreach (var k in dicProductNumber.Keys)
            {
                Console.WriteLine("<-------------" + k);
                foreach (var v in dicProductNumber[k])
                {
                    Console.WriteLine(k + "--->" + v);
                }
                Console.WriteLine("------------->");
            }
        }
        [TestMethod]
        public void TestMethod2()
        {
            var data = "本衣架:12,red,S,分配工序:2";
            //var unioceCodeBytes = UnicodeUtils.GetBytesByUnicode(data);
            //var hexData = HexHelper.BytesToHexString(unioceCodeBytes);
            var dd = CharacterToCoding(data);
            var begin = 0x0160;
            var end = 0x017F;
            //var decHex = HexHelper.TenToHexString(begin);
            for (var index=begin;index<=end;index++) {
                var hex = HexHelper.TenToHexString4Len(index);
                Console.WriteLine(index+"---->"+hex);
            }
            //var decT = HexHelper.HexToTen(t.ToString());
        }
        /// <summary>
        ///汉字转十六进制UNICODE编码字符串
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public string CharacterToCoding(string character)
        {
            string coding = "";

            for (int i = 0; i < character.Length; i++)
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(character.Substring(i, 1));

                //取出二进制编码内容  
                string lowCode = System.Convert.ToString(bytes[0], 16);

                //取出低字节编码内容（两位16进制）  
                if (lowCode.Length == 1)
                {
                    lowCode = "0" + lowCode;
                }

                string hightCode = System.Convert.ToString(bytes[1], 16);

                //取出高字节编码内容（两位16进制）  
                if (hightCode.Length == 1)
                {
                    hightCode = "0" + hightCode;
                }

                coding += (hightCode + lowCode);

            }

            return coding;
        }

        [TestMethod]
        public void Test33() {
            //var s = "0037";
            //var s1 = s.Substring(0,2);
            //var s2 = s.Substring(2, 2);
            //var hh = HexHelper.TenToHexString2Len(10000);
            //var bb = HexHelper.StringToHexByte(hh);
            //var deciHex = HexHelper.DecimalToHexString4Len((decimal)33.445);
            //var decimalVal = HexHelper.ParseHexString(deciHex);

            decimal d = decimal.Parse(334.56+"");

           var dStr= new BigInteger(d).ToString("X");

            String source = dStr;//HexHelper.DecimalToHexString4Len(d); //"4B414D000000011613C3";

            //String highPart = source.Remove(source.Length - 16);
            //String lowPart = source.Substring(source.Length - 16);

            //Decimal result =
            //  ulong.Parse(highPart, System.Globalization.NumberStyles.HexNumber);

            //result = result * ulong.MaxValue + ulong.Parse(lowPart, System.Globalization.NumberStyles.HexNumber);
            uint num = uint.Parse(source, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);
            Console.WriteLine("float convert = {0}", f);
        }
        [TestMethod]
        public void Test77() {
            var txt = AssicUtils.EncodeByStr("000");
        }
    }
}
