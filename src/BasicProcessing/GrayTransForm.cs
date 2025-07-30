// Copyright (c) 2025 Ming Hu. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

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
    public partial class GrayTransForm : Form
    {
        imageClass pImage;
        imageClass tempImage = new imageClass();
        double[] GrayNum = new double[256];
        byte[] grayByte;
        double[] GrayPosibility = new double[256];  //灰度图像概率

        //定义委托
        public delegate void mainRefresh(object sender, EventArgs e);
        public event mainRefresh onMainRefresh;

        private void getImageByte()
        {
            grayByte = new byte[pImage.MBWidth * pImage.MHeight];
            grayByte = pImage.getByte;
        }


        public GrayTransForm()
        {
            InitializeComponent();
            numericUpDown1.Value = 50; numericUpDown1.Enabled = false;
            numericUpDown2.Value = 240; numericUpDown2.Enabled = false;
            numericUpDown3.Value = 0; numericUpDown3.Enabled = false;
            numericUpDown4.Value = 255; numericUpDown4.Enabled = false;
            radioButton1.Checked = true;
        }
        public imageClass getIndex
        {
            get { return pImage; }
            set { pImage = value; }
        }

        private void afterHistPanel_Paint(object sender, PaintEventArgs e)
        {
            grayByte = new byte[pImage.MBWidth * pImage.MHeight];
            Graphics g = e.Graphics;
            Point[] pointArray = new Point[3];
            pointArray[0] = new Point(250, 300);
            pointArray[1] = new Point(250, 20);
            pointArray[2] = new Point(20, 300);
            Pen linePen = new Pen(Color.Black);
            linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            linePen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(linePen, pointArray[0], pointArray[1]);
            g.DrawLine(linePen, pointArray[0], pointArray[2]);

            getImageByte();
            if (radioButton1.Checked == true)
            {

                HistCalculation();
                afterHistDraw(e.Graphics);  //C#数组传值传不进？
            }
            if (radioButton3.Checked == true)
            {
                grayByte = pImage.grayTransform(grayByte, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value);
                HistCalculation();
                afterHistDraw(e.Graphics);
            }


        }

        private void transFormPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Point[] pointArray = new Point[3];
            pointArray[0] = new Point(20, 300);
            pointArray[1] = new Point(20, 20);
            pointArray[2] = new Point(300, 300);
            Pen linePen = new Pen(Color.Black);
            linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            linePen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(linePen, pointArray[0], pointArray[1]);
            g.DrawLine(linePen, pointArray[0], pointArray[2]);

            Pen linePen2 = new Pen(Color.Black);
            if (radioButton1.Checked == true)
            {
                g.DrawLine(linePen2, new Point(20, 300), new Point(275, 45));
            }

            if (radioButton3.Checked == true)
            {
                Point pointA = new Point((int)numericUpDown1.Value + 20, 300 - (int)numericUpDown3.Value);
                Point pointB = new Point((int)numericUpDown2.Value + 20, 300 - (int)numericUpDown4.Value);
                Point pointA1 = new Point((int)numericUpDown1.Value + 20, 300);
                Point pointA2 = new Point(20, 300 - (int)numericUpDown3.Value);
                Point pointB1 = new Point((int)numericUpDown2.Value + 20, 300);
                Point pointB2 = new Point(20, 300 - (int)numericUpDown4.Value);
                Pen dotPen = new Pen(Color.Black);
                dotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                g.DrawLine(dotPen, pointA, pointA1); g.DrawLine(dotPen, pointA, pointA2);
                g.DrawLine(dotPen, pointB, pointB1); g.DrawLine(dotPen, pointB, pointB2);
                g.DrawLine(linePen2, new Point(20, 300), pointA);
                g.DrawLine(linePen2, pointA, pointB);
                g.DrawLine(linePen2, pointB, new Point(275, 45));
            }
            //绘制灰度变换函数


        }

        private void preHistPanel_Paint(object sender, PaintEventArgs e)
        {
            //pImage.preHistDraw(e.Graphics);
            Graphics g = e.Graphics;
            Point[] pointArray = new Point[3];
            pointArray[0] = new Point(20, 250);
            pointArray[1] = new Point(20, 20);
            pointArray[2] = new Point(300, 250);
            Pen linePen = new Pen(Color.Black);
            linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            linePen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(linePen, pointArray[0], pointArray[1]);
            g.DrawLine(linePen, pointArray[0], pointArray[2]);

            getImageByte();

            HistCalculation();
            /*pImage.*/
            preHistDraw(e.Graphics);

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void buttonApply_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pImage.MBWidth * pImage.MHeight; i++)
            {
                pImage.ImageB[i] = grayByte[i];
            }
            pImage.putBitMapData();

            onMainRefresh(this, new EventArgs());        //发送事件

            afterHistPanel.Refresh();

            preHistPanel.Refresh();

            transFormPanel.Refresh();

        }



        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton3.Checked == true)
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                grayByte = pImage.grayTransform(grayByte, numericUpDown1.Value, numericUpDown2.Value, numericUpDown3.Value, numericUpDown4.Value);
                HistCalculation();


            }
            if (radioButton3.Checked == false)
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
            }
            transFormPanel.Refresh();
            afterHistPanel.Refresh();
        }

        private void HistCalculation()
        {
            int i, j;
            if (pImage.MyImageType == 0)   //8位灰度图像计算
            {
                byte a;
                for (i = 0; i < 256; i++)
                {
                    GrayNum[i] = 0;
                }
                for (i = 0; i < pImage.MHeight; i++)
                {
                    for (j = 0; j < pImage.MWidth; j++)
                    {
                        a = grayByte[i * pImage.MBWidth + j];
                        GrayNum[a] = GrayNum[a] + 1;
                    }
                }
                for (i = 0; i < 256; i++)   //计算概率
                {
                    GrayPosibility[i] = GrayNum[i] / (pImage.MWidth * pImage.MHeight);
                }
            }

        }
        public void afterHistDraw(Graphics e)
        {
            int i;
            //绘制坐标信息
            Point[] pN = new Point[4];
            pN[0] = new Point(40, 300);
            pN[1] = new Point(40, 295);
            pN[2] = new Point(250, 45);
            pN[3] = new Point(245, 45);
            Pen penShortLine = new Pen(Color.Red);
            e.DrawLine(penShortLine, pN[0], pN[1]);
            e.DrawLine(penShortLine, pN[2], pN[3]);

            Font drawFont1 = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            e.DrawString("O", drawFont1, drawBrush, new Point(250, 300));
            e.DrawString("Si", drawFont1, drawBrush, new PointF(250, 20));
            e.DrawString("Psi", drawFont1, drawBrush, new PointF(20, 300));


            PointF p1 = new PointF();
            PointF p2 = new PointF();
            if (pImage.MyImageType == 0)
            {
                //概率最大值
                double maxP = GrayPosibility[0];
                for (i = 0; i < 256; i++)
                {
                    if (maxP < GrayPosibility[i]) { maxP = GrayPosibility[i]; }
                }
                //画折线
                Pen penPolyline = new Pen(Color.Gray, 1);
                for (i = 0; i < 255; i++)
                {
                    p1.X = 250 - 210 * (float)(GrayPosibility[i] / maxP);
                    p1.Y = 300 - i;
                    p2.X = 250 - 210 * (float)(GrayPosibility[i + 1] / maxP);
                    p2.Y = 300 - i - 1;
                    e.DrawLine(penPolyline, p1, p2);
                }
                //封闭
                p1.X = 250;
                p1.Y = 45;
                e.DrawLine(penPolyline, p2, p1);
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            afterHistPanel.Refresh();
            preHistPanel.Refresh();
            transFormPanel.Refresh();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            afterHistPanel.Refresh();
            preHistPanel.Refresh();
            transFormPanel.Refresh();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            afterHistPanel.Refresh();
            preHistPanel.Refresh();
            transFormPanel.Refresh();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            afterHistPanel.Refresh();
            preHistPanel.Refresh();
            transFormPanel.Refresh();
        }

        public void preHistDraw(Graphics e)
        {
            int i;
            //绘制坐标信息
            Point[] pN = new Point[4];
            pN[0] = new Point(275, 250);
            pN[1] = new Point(275, 245);
            pN[2] = new Point(20, 40);
            pN[3] = new Point(25, 40);
            Pen penShortLine = new Pen(Color.Red);
            e.DrawLine(penShortLine, pN[0], pN[1]);
            e.DrawLine(penShortLine, pN[2], pN[3]);

            Font drawFont1 = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            e.DrawString("O", drawFont1, drawBrush, new Point(8, 252));
            e.DrawString("Ri", drawFont1, drawBrush, new PointF(300, 252));
            e.DrawString("Pri", drawFont1, drawBrush, new PointF(5, 5));

            PointF p1 = new PointF();
            PointF p2 = new PointF();
            if (pImage.MyImageType == 0)
            {
                //概率最大值
                double maxP = GrayPosibility[0];
                for (i = 0; i < 256; i++)
                {
                    if (maxP < GrayPosibility[i]) { maxP = GrayPosibility[i]; }
                }
                //画折线
                Pen penPolyline = new Pen(Color.Gray, 1);
                for (i = 0; i < 255; i++)
                {
                    p1.X = 20 + i;
                    p1.Y = 40 + 210 * (float)(1 - GrayPosibility[i] / maxP);
                    p2.X = 20 + i + 1;
                    p2.Y = 40 + 210 * (float)(1 - GrayPosibility[i + 1] / maxP);
                    e.DrawLine(penPolyline, p1, p2);
                }
                //封闭
                p1.X = 275;
                p1.Y = 250;
                e.DrawLine(penPolyline, p2, p1);
            }

        }
    }
}

