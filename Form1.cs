using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace qrcodeimg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            for (int i = 1; i <= 811;)
            {
               

                string strPos0 = "FQ-8-"+i.ToString("0000");
                string strPos1 = "FQ-8-" + (i+1).ToString("0000");
                string strPos2 = "FQ-8-" + (i+2).ToString("0000");

                //生成二维码
                Image im0 = CreateQRCode("http://dktr.foxlink.com.tw:8083/QR?position=" + strPos0, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 6, 4, 534, 0, Color.Black);
                Image im1 = CreateQRCode("http://dktr.foxlink.com.tw:8083/QR?position=" + strPos1, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 6, 4, 534, 0, Color.Black);
                Image im2 = CreateQRCode("http://dktr.foxlink.com.tw:8083/QR?position=" + strPos2, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 6, 4, 534, 0, Color.Black);

                // 二维码添加文字
                //Image bt0 = InsertWords2(im0,  strPos0);
                //Image bt1 = InsertWords2(im1, strPos1);
                //Image bt2 = InsertWords2(im2, strPos2);

                //将生成的二维码图像粘贴至相框上面
                Image imgBack = System.Drawing.Image.FromFile(@"C:\Users\Administrator\source\repos\qrcodeimg\w.png");     //相框图片 
                FontFamily fontFamily = new FontFamily("楷体");
                Font font1 = new Font(fontFamily, 100f, FontStyle.Regular, GraphicsUnit.Pixel);
                

                Graphics g = Graphics.FromImage(imgBack);

                g.DrawString(strPos0, font1, Brushes.Black, 1833, 422);//602
                g.DrawString(strPos1, font1, Brushes.Black, 1854, 1552);//1732
                g.DrawString(strPos2, font1, Brushes.Black, 1855, 2658);//2838

                g.DrawImage(im0, 1283, 335, im0.Width, im0.Height);
                g.DrawImage(im1, 1304, 1465, im1.Width, im1.Height);
                g.DrawImage(im2, 1305, 2571, im2.Width, im2.Height);
                

                imgBack.Save( i+"-"+(i+2)+ ".jpg");
                g.Dispose();
                i = i + 3;
            }
            button1.Enabled = true;
            // im.Save(Application.StartupPath+"FD001" + ".jpg");


            ////需要生成的文字
            //string enCodeString = "dktr.foxlink.com.tw:8083/QR?position=FD10001";

            //QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            ////二维码背景颜色
            //qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;
            ////二维码编码方式
            //qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            ////每个小方格的宽度
            //qrCodeEncoder.QRCodeScale = 4;
            ////版本号越大生成的二维码越大最大为40
            //qrCodeEncoder.QRCodeVersion = 8;
            ////纠错率级别
            //qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //Bitmap bt = InsertWords(qrCodeEncoder.Encode(enCodeString, Encoding.UTF8), "FD00001");
            //bt.Save(Application.StartupPath + "FD001" + ".jpg");
        }

        /// <summary>
        /// 生成二维码图片，并返回文件的保存路径
        /// </summary>
        /// <param name="nr">要生成二维码的字符串</param>
        /// <returns></returns>
        private string CreateQR(string nr)
        {
            Bitmap bt;
            if (!string.IsNullOrEmpty(nr))
            {
                string filename = Guid.NewGuid().ToString().ToUpper();
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                bt = qrCodeEncoder.Encode(nr, Encoding.UTF8);
                string imgPath = Application.StartupPath + filename + ".jpg";
                try
                {
                    bt.Save(imgPath);
                    return imgPath;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 二维码下面加上文字
        /// </summary>
        /// <param name="qrImg">QR图片</param>
        /// <param name="content">文字内容</param>
        /// <param name="n"></param>
        /// <returns></returns>
        public Bitmap InsertWords(Bitmap qrImg, string content = "")
        {
            Bitmap backgroudImg = new Bitmap(qrImg.Width, qrImg.Height);
            backgroudImg.MakeTransparent();
            Graphics g2 = Graphics.FromImage(backgroudImg);
            g2.Clear(Color.Transparent);
            //画二维码到新的面板上
            g2.DrawImage(qrImg, 0, 0);

            if (!string.IsNullOrEmpty(content))
            {
                FontFamily fontFamily = new FontFamily("楷体");
                Font font1 = new Font(fontFamily, 20f, FontStyle.Bold, GraphicsUnit.Pixel);

                //文字长度 
                int strWidth = (int)g2.MeasureString(content, font1).Width;
                //总长度减去文字长度的一半  （居中显示）
                int wordStartX = (qrImg.Width - strWidth) / 2;
                int wordStartY = qrImg.Height - 18;

                g2.DrawString(content, font1, Brushes.Red, wordStartX, wordStartY);
            }

            g2.Dispose();
            return backgroudImg;
        }

        public Image InsertWords2(Image qrImg, string content = "")
        {
            Image backgroudImg = new Bitmap(qrImg.Width, qrImg.Height);
           // backgroudImg.MakeTransparent();
            Graphics g2 = Graphics.FromImage(backgroudImg);
            g2.Clear(Color.Transparent);
            //画二维码到新的面板上
            g2.DrawImage(qrImg, 0, 0);

            if (!string.IsNullOrEmpty(content))
            {
                FontFamily fontFamily = new FontFamily("楷体");
                Font font1 = new Font(fontFamily, 20f, FontStyle.Bold, GraphicsUnit.Pixel);

                //文字长度 
                int strWidth = (int)g2.MeasureString(content, font1).Width;
                //总长度减去文字长度的一半  （居中显示）
                int wordStartX = (qrImg.Width - strWidth) / 2;
                int wordStartY = qrImg.Height - 18;

                g2.DrawString(content, font1, Brushes.Red, wordStartX, wordStartY);
            }

            g2.Dispose();
            return backgroudImg;
        }

        /// <summary>
        /// 二维码管理
        /// </summary>

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="Content">内容文本</param>
        /// <param name="QRCodeEncodeMode">二维码编码方式</param>
        /// <param name="QRCodeErrorCorrect">纠错码等级</param>L ：7%；M：15%；Q：25%；H：30%
        /// <param name="QRCodeVersion">二维码版本号 0-40</param>
        /// <param name="QRCodeScale">每个小方格的预设宽度（像素），正整数</param>
        /// <param name="size">图片尺寸（像素），0表示不设置</param>
        /// <param name="border">图片白边（像素），当size大于0时有效</param>
        /// <param name="codeEyeColor"></param>
        /// <returns></returns>
        public Image CreateQRCode(string Content, QRCodeEncoder.ENCODE_MODE QRCodeEncodeMode, QRCodeEncoder.ERROR_CORRECTION QRCodeErrorCorrect, int QRCodeVersion, int QRCodeScale, int size, int border, Color codeEyeColor)
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncodeMode;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeErrorCorrect;
                qrCodeEncoder.QRCodeScale = QRCodeScale;
                qrCodeEncoder.QRCodeVersion = QRCodeVersion;
            Image image = qrCodeEncoder.Encode(Content);

                #region 根据设定的目标图片尺寸调整二维码QRCodeScale设置，并添加边框
                if (size > 0)
                {
                    //当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                    #region 当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                    while (image.Width < size)
                    {
                        qrCodeEncoder.QRCodeScale++;
                        Image imageNew = qrCodeEncoder.Encode(Content);
                        if (imageNew.Width < size)
                        {
                            image = new Bitmap(imageNew);
                            imageNew.Dispose();
                            imageNew = null;
                        }
                        else
                        {
                            qrCodeEncoder.QRCodeScale--; //新尺寸未采用，恢复最终使用的尺寸
                            imageNew.Dispose();
                            imageNew = null;
                            break;
                        }
                    }
                    #endregion

                    //当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                    #region 当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                    while (image.Width > size && qrCodeEncoder.QRCodeScale > 1)
                    {
                        qrCodeEncoder.QRCodeScale--;
                        Image imageNew = qrCodeEncoder.Encode(Content);
                        image = new Bitmap(imageNew);
                        imageNew.Dispose();
                        imageNew = null;
                        if (image.Width < size)
                        {
                            break;
                        }
                    }
                    #endregion

                    //根据参数设置二维码图片白边的最小宽度（按需缩小）
                    #region 根据参数设置二维码图片白边的最小宽度
                    if (image.Width <= size && border > 0)
                    {
                        while (image.Width <= size && size - image.Width < border * 2 && qrCodeEncoder.QRCodeScale > 1)
                        {
                            qrCodeEncoder.QRCodeScale--;
                            Image imageNew = qrCodeEncoder.Encode(Content);
                            image = new Bitmap(imageNew);
                            imageNew.Dispose();
                            imageNew = null;
                        }
                    }
                    #endregion

                    //已经确认二维码图像，为图像染色修饰
                    if (true)
                    {
                        //定位点方块边长
                        int beSize = qrCodeEncoder.QRCodeScale * 3;

                        int bep1_l = qrCodeEncoder.QRCodeScale * 2;
                        int bep1_t = qrCodeEncoder.QRCodeScale * 2;

                        int bep2_l = image.Width - qrCodeEncoder.QRCodeScale * 5 - 1;
                        int bep2_t = qrCodeEncoder.QRCodeScale * 2;

                        int bep3_l = qrCodeEncoder.QRCodeScale * 2;
                        int bep3_t = image.Height - qrCodeEncoder.QRCodeScale * 5 - 1;

                        int bep4_l = image.Width - qrCodeEncoder.QRCodeScale * 7 - 1;
                        int bep4_t = image.Height - qrCodeEncoder.QRCodeScale * 7 - 1;

                        Graphics graphic0 = Graphics.FromImage(image);

                        // Create solid brush.
                        SolidBrush blueBrush = new SolidBrush(codeEyeColor);

                        graphic0.FillRectangle(blueBrush, bep1_l, bep1_t, beSize, beSize);
                        graphic0.FillRectangle(blueBrush, bep2_l, bep2_t, beSize, beSize);
                        graphic0.FillRectangle(blueBrush, bep3_l, bep3_t, beSize, beSize);
                        graphic0.FillRectangle(blueBrush, bep4_l, bep4_t, qrCodeEncoder.QRCodeScale, qrCodeEncoder.QRCodeScale);
                    }

                    //当目标图片尺寸大于二维码尺寸时，将二维码绘制在目标尺寸白色画布的中心位置
                    #region 如果目标尺寸大于生成的图片尺寸，将二维码绘制在目标尺寸白色画布的中心位置
                    if (image.Width < size)
                    {
                        //新建空白绘图
                        Bitmap panel = new Bitmap(size, size);
                        Graphics graphic0 = Graphics.FromImage(panel);
                    graphic0.Clear(Color.White);
                    int p_left = 0;
                        int p_top = 0;
                        if (image.Width <= size) //如果原图比目标形状宽
                        {
                            p_left = (size - image.Width) / 2;
                        }
                        if (image.Height <= size)
                        {
                            p_top = (size - image.Height) / 2;
                        }

                        //将生成的二维码图像粘贴至绘图的中心位置
                        graphic0.DrawImage(image, p_left, p_top, image.Width, image.Height);
                        image = new Bitmap(panel);
                        panel.Dispose();
                        panel = null;
                        graphic0.Dispose();
                        graphic0 = null;
                    }
                    #endregion
                }
                #endregion
                return image;
            }

        
        
        

    }
}
