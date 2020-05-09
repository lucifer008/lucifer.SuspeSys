using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
    public class ImageHelp
    {
        //参数是图片的路径  
        public static byte[] GetPictureData(string imagePath)
        {
            FileStream fs = new FileStream(imagePath, FileMode.Open);
            byte[] byteData = new byte[fs.Length];
            fs.Read(byteData, 0, byteData.Length);
            fs.Close();
            return byteData;
        }

        public static System.Drawing.Image ByteToImage(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

        public static string ImageToString(string imagePath)
        {
            byte[] bytes = GetPictureData(imagePath);
            return Convert.ToBase64String(bytes);
        }

        public static Image StringToImage(string imageString)
        {
            byte[] bytes = Convert.FromBase64String(imageString);// System.Text.Encoding.Default.GetBytes(imageString);

            return ByteToImage(bytes);
        }
    }
}
