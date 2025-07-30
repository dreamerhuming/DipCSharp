using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DipCSharp
{
    public partial class RotateForm : Form
    {
        imageClass pImage = new imageClass();
        MainForm mainFF;
        ToolStripProgressBar tspBar;
        byte[] tempImageB;  //存储窗体加载时原图像的灰度数据
        public RotateForm()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }
        public imageClass GetIndex
        {
            get { return pImage; }
            set { pImage = value; }
        }
        public MainForm MainFF
        {
            get { return mainFF; }
            set { mainFF = value; }
        }
        public ToolStripProgressBar TspBar
        {
            get { return tspBar; }
            set { tspBar = value; }
        }
        
        double sumA = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            long i, j, pos;
            double cosA, sinA;
            sumA = sumA + (double)numericUpDown1.Value;
            double x0 = (long)(pImage.MWidth - 1) / 2;
            double y0 = (long)(pImage.MHeight - 1) / 2;
            double Pi = Math.PI;
            cosA = Math.Cos(sumA * Pi / 180);
            sinA = Math.Sin(sumA * Pi / 180);
            tspBar.Maximum = (int)(pImage.MBData);
            tspBar.Minimum = 0;
            for (i = 0; i < pImage.MHeight; i++)
            {
                for (j = 0; j < pImage.MWidth; j++)
                {
                    pos = i * pImage.MBWidth + j;
                    tspBar.Value = (int)pos;
                    if (radioButton1.Checked == true)
                    {
                        pImage.ImageB[pos] = nearestEle(cosA, sinA, i, j, x0, y0);
                    }
                    if (radioButton2.Checked == true)
                    {
                        pImage.ImageB[pos] = bilinearInterpolation(cosA, sinA, i, j, x0, y0);
                    }
                }
            }
            pImage.putBitMapData();
            tspBar.Value = 0;
            mainFF.Refresh();
        }

        /// <summary>
        /// 双线性内插法
        /// </summary>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <returns></returns>
        private byte bilinearInterpolation(double cosA, double sinA, long i, long j, double x0, double y0)
        {
            double x1, y1;  //相对坐标位置
            double x2, y2;  //旋转之后坐标位置
            double inter1, inter2, interResult;
            double[,] tempGray = new double[2,2];
            int xn, yn;
            x1 = j - x0;
            y1 = i - y0;

            //坐标转换                  __           __
            //                          | cosA  -sinA |                
            //(x2,y2) = (x0,y0)+(x1,y1) |             |
            //                          |_sinA  cosA _|

            
            double[,] rMat = new double[2, 2];  //旋转矩阵
            rMat[0, 0] = cosA;
            rMat[0, 1] = -sinA;
            rMat[1, 0] = sinA;
            rMat[1, 1] = cosA;
            x2 = x1 * rMat[0, 0] + y1 * rMat[1, 0] + x0;    //y轴朝下，其实还是逆时针寻找像素
            y2 = x1 * rMat[0, 1] + y1 * rMat[1, 1] + y0;
            if (Math.Abs(x2 - x0) > pImage.MWidth / 2 || Math.Abs(y2 - y0) > pImage.MHeight / 2)
            {
                return 0;
            }
            else
            {
                xn = (int)x2;
                yn = (int)y2;
                try
                {
                    //内插顺序：
                    //      ↓  ↓
                    //      ↓  ↓
                    //      ------>
                    tempGray[0, 0] = tempImageB[yn * pImage.MBWidth + xn];
                    tempGray[0, 1] = tempImageB[yn * pImage.MBWidth + xn+1];
                    tempGray[1, 0] = tempImageB[(yn + 1) * pImage.MBWidth + xn];
                    tempGray[1, 1] = tempImageB[(yn + 1) * pImage.MBWidth + xn + 1];

                    inter1 = tempGray[0, 0] + ((y2 - yn) / 1) * (tempGray[1, 0] - tempGray[0, 0]);
                    inter2 = tempGray[1, 0] + ((y2 - yn) / 1) * (tempGray[1, 1] - tempGray[0, 1]);
                    interResult = inter1 + ((x2 - xn) / 1) * (inter2 - inter1);
                    return (byte)interResult;
                }
                catch (System.Exception ex)
                {
                    return 120;
                }
                
            }
        }
        /// <summary>
        /// 最邻近元法
        /// </summary>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <returns></returns>
        private byte nearestEle(double cosA,double sinA,long i,long j,double x0,double y0)
        {
            long pos = 0;
            double minDis;
            double x1, y1;  //相对坐标位置
            double x2, y2;  //旋转之后坐标位置
            int xn, yn;
            x1 = j - x0;
            y1 = i - y0;

            //坐标转换                  __           __
            //                          | cosA  -sinA |                
            //(x2,y2) = (x0,y0)+(x1,y1) |             |
            //                          |_sinA  cosA _|
            
            double[,] rMat = new double[2, 2];  //旋转矩阵
            rMat[0, 0] = cosA;
            rMat[0, 1] = -sinA;
            rMat[1, 0] = sinA;
            rMat[1, 1] = cosA;
            x2 = x1 * rMat[0, 0] + y1 * rMat[1, 0] + x0;
            y2 = x1 * rMat[0, 1] + y1 * rMat[1, 1] + y0;
            if (Math.Abs(x2 - x0) > pImage.MWidth / 2 || Math.Abs(y2 - y0) > pImage.MHeight / 2)
            {
                return 0;
            }
            else
            {
                xn = (int)x2;
                yn = (int)y2;
                minDis = double.MaxValue;
                if (minDis > Math.Sqrt(Math.Pow(x2 - xn, 2) + Math.Pow(y2 - yn, 2)))
                {
                    minDis = Math.Sqrt(Math.Pow(x2 - xn, 2) + Math.Pow(y2 - yn, 2));
                    pos = yn * pImage.MBWidth + xn;
                }
                if (minDis > Math.Sqrt(Math.Pow(x2 - xn - 1, 2) + Math.Pow(y2 - yn, 2)))
                {
                    minDis = Math.Sqrt(Math.Pow(x2 - xn - 1, 2) + Math.Pow(y2 - yn, 2));
                    pos = yn * pImage.MBWidth + xn + 1;
                }
                if (minDis > Math.Sqrt(Math.Pow(x2 - xn, 2) + Math.Pow(y2 - yn - 1, 2))) 
                {
                    minDis = Math.Sqrt(Math.Pow(x2 - xn, 2) + Math.Pow(y2 - yn - 1, 2));
                    pos = (yn + 1) * pImage.MBWidth + xn;
                }
                if (minDis > Math.Sqrt(Math.Pow(x2 - xn - 1, 2) + Math.Pow(y2 - yn - 1, 2)))
                {
                    minDis = Math.Sqrt(Math.Pow(x2 - xn - 1, 2) + Math.Pow(y2 - yn - 1, 2));
                    pos = (yn + 1) * pImage.MBWidth + xn + 1;
                }
                try
                {
                    return tempImageB[pos];
                }
                catch { return 120; }
            }
        }

        private void RotateForm_Load(object sender, EventArgs e)
        {
            long i, j, pos;
            //对tempImageB初始化
            tempImageB = new byte[pImage.MBData];
            for (i = 0; i < pImage.MHeight; i++)
            {
                for (j = 0; j < pImage.MWidth; j++)
                {
                    pos = i * pImage.MBWidth + j;
                    tempImageB[pos] = pImage.ImageB[pos];
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


    }
}
