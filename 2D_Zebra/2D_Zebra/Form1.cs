using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.google.zxing; //引用动态链接库
using com.google.zxing.common;

namespace _2D_Zebra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = GetBitmap(textBox1.Text,pictureBox1.Width,pictureBox1.Height);//生成内容
        }

        public static Bitmap GetBitmap(string sContent, int iWidth, int iHeight)//生成二维码
        {
            string _message = sContent;
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(_message, BarcodeFormat.QR_CODE, iWidth, iHeight);
            int width = byteMatrix.Width;
            int height = byteMatrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, byteMatrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000")
                        : ColorTranslator.FromHtml("0xFFFFFFFF"));
                }
            }
            return bmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image myImg = Image.FromFile(@"D:\image\test.jpg");
            textBox2.Text = QrcodeToText((Bitmap)myImg);//这里需要强制转换类型
        }

        public static string QrcodeToText(Bitmap myImage)
        {
            LuminanceSource source = new RGBLuminanceSource(myImage, myImage.Width, myImage.Height); //将图片转为灰度图像。
            com.google.zxing.BinaryBitmap bitmap =
                new com.google.zxing.BinaryBitmap(new com.google.zxing.common.HybridBinarizer(source));
            //二进制像素存储
            try
            {
                Result result = new MultiFormatReader().decode(bitmap);//解码并生成结果
                return result.Text;
            }
            catch (Exception ex)//异常处理部分
            {
                return string.Format("识别二维码出错！\r\n\r\n提示：{0}", ex.Message);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save(@"D:\image\test.jpg");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBox2.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
