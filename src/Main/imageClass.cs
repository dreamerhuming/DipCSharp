// Copyright (c) 2025 Ming Hu. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DipCSharp
{
  public class imageClass
  {
    #region 全局变量
    private Bitmap bMap;            //位图对象
    private string myImageName;     //图像路径
    private int myImageType;        //图像类型，0为8位图像，1为24位彩色图像，其他类型的图像暂不支持
    private long mWidth, mHeight;   //像素宽度和高度
                                    //----------灰度图像------------
    private byte[] imageB;          //存储灰度图像数据，一维数组存储。对于8位图像，一个像素即是一个字节。
                                    //每行存储的字节数必须是4的整倍数，需要时添加适当字节数，确保与文件中记录的数据量完全一致
    private long mBWidth;           //存储灰度图像数据宽度
    private long mBSize;            //灰度图像实际像素大小
    private long mBData;            //灰度图像存储数据大小
                                    //----------彩色图像------------
    private byte[] imageC;          //存储彩色图像数据，也是一个一维数组存储。对于24位图像，一个像素
                                    //即是三个字节。每行存储的字节数是像素个数的三倍。每行存储的字节数还需要确保是
                                    //4的整倍数，需要时添加适当字节。
    private long mCWidth;           //彩色图像每行像素存储的字节数，mCWidth = (mWidth*3+3)\4*4
    private long mCSize;            //彩色图像实际像素大小
    private long mCData;            //彩色图像存储数据大小
    private long[] CFirstPosition;  //彩色图像首字节位置数组
                                    //----------描述图像范围--------
    private double xWmin, yWmin, xWmax, yWmax;
    private double xWmin0, yWmin0, xWmax0, yWmax0;

    //灰度直方图全局变量
    private double entropyH = 0, sigma = 0, grayMin = 0, grayMax = 0, grayAverage = 0;
    private double[] GrayPosibility = new double[256];  //灰度图像概率

    private double[] RedPosibility = new double[256];           //彩色图像概率
    private double[] GreenPosibility = new double[256];
    private double[] BluePosibility = new double[256];

    private double[] GrayNum = new double[256];
    private double[] RedNum = new double[256];
    private double[] GreenNum = new double[256];
    private double[] BlueNum = new double[256];

    private double xView, yView;
    private double mLastScale;
    private bool myImageIsOpen = false;
    #endregion

    #region 灰度图像数据处理以及图像显示
    public bool readImage(string imageName)
    {
      // if (!System.IO.Directory.Exists(imageName)) //文件存在与否
      //  return false;
      if (bMap != null)   //图像文件是否为空
        bMap.Dispose();
      try
      {
        bMap = new Bitmap(imageName);
        getBitMapData();
        //对参量初始化
        xWmin = 0;
        yWmin = 0;
        xWmax = bMap.Width - 1;
        yWmax = bMap.Height - 1;

        myImageName = imageName;
        myImageIsOpen = true;
        return true;
      }
      catch (System.Exception ex)
      {
        return false;
      }

    }

    private bool getBitMapData()
    {
      //从BitMap对象里获取图像数据
      //在实际处理程序中一般不采用拷贝备份的做法，可以通过获得的数据指针，直接操作就可以了。
      //过程是：锁定内存，获取数据的起始地址，根据图像类型操作数据，结束锁定
      if (bMap == null) return false;
      Rectangle rec = new Rectangle(0, 0, bMap.Width, bMap.Height);   //原点坐标，宽度，高度，图像矩形
      BitmapData bmpData = bMap.LockBits(rec, ImageLockMode.ReadOnly, bMap.PixelFormat);  //锁定图像数据
      IntPtr ptr = bmpData.Scan0; //起始地址

      if (bMap.PixelFormat == PixelFormat.Format8bppIndexed)
      {
        mWidth = bMap.Width;
        mHeight = bMap.Height;
        mBWidth = ((mWidth + 3) / 4) * 4;
        mBSize = mWidth * mHeight; //像素大小
        mBData = mBWidth * mHeight;    //数据量大小

        imageB = new byte[mBData];    //保存图像数据的数组
                                      //拷贝数据到ImageB
        System.Runtime.InteropServices.Marshal.Copy(ptr, imageB, 0, (Int32)mBData); //为什么VB中LONG可以隐式转成INT32？
                                                                                    //不如直接定义为int
        myImageType = 0;
      }

      else if (bMap.PixelFormat == PixelFormat.Format24bppRgb)
      {
        mWidth = bMap.Width;
        mHeight = bMap.Height;
        mCWidth = ((3 * mWidth + 3) / 4) * 4;
        mCSize = mWidth * mHeight;    //像素大小
        mCData = mCWidth * mHeight;  //数据量大小

        imageC = new byte[mCData];   //保存图像数据的数组
        System.Runtime.InteropServices.Marshal.Copy(ptr, imageC, 0, (Int32)mCData); //拷贝数据
        myImageType = 1;

        CFirstPosition = new long[mWidth];
        for (int i = 0; i < mHeight; i++)
        {
          CFirstPosition[i] = i * mCWidth;
        }
      }
      else
        MessageBox.Show("暂不支持该图像类型！");
      bMap.UnlockBits(bmpData);
      return true;
    }

    public bool putBitMapData()
    {
      if (bMap == null)
        return false;
      Rectangle rec = new Rectangle(0, 0, bMap.Width, bMap.Height);
      BitmapData bmpData = bMap.LockBits(rec, ImageLockMode.WriteOnly, bMap.PixelFormat);

      IntPtr ptr = bmpData.Scan0;

      if (bMap.PixelFormat == PixelFormat.Format8bppIndexed)
      {
        mBData = mBWidth * mHeight;
        System.Runtime.InteropServices.Marshal.Copy(imageB, 0, ptr, (Int32)mBData);
      }
      else if (bMap.PixelFormat == PixelFormat.Format24bppRgb)
      {
        mCData = mCWidth * mHeight;
        System.Runtime.InteropServices.Marshal.Copy(imageC, 0, ptr, (Int32)mCData);
      }
      bMap.UnlockBits(bmpData);
      return true;
    }

    public bool zoomImage(Graphics e, int flag = 0)
    {
      double wX, wY;
      double vX, vY;
      double s;

      if (bMap == null) return false;
      if (myImageIsOpen == false) return false;
      wX = xWmax - xWmin; //需要显示的宽度
      wY = yWmax - yWmin; //需要显示的高度
      vX = xView; //画纸的宽度
      vY = yView; //画纸的高度
      if (wX == 0 & wY == 0) return false;

      if (flag == 0 || mLastScale == 0.0000000001)    //flag != 0,按原比例画图
      {
        s = wX / vX;
        if (wY / vY > s) s = wY / vY;   //比例关系，取较大者，保证图像能够完全显示，而且相对最大地显示
        mLastScale = s;
      }
      else
      {
        s = mLastScale;
      }

      xWmin = xWmin + (wX - vX * s) / 2;  //确保图像中间显示？
      xWmax = xWmin + vX * s;
      yWmin = yWmin + (wY - vY * s) / 2;
      yWmax = yWmin + vY * s;

      PointF[] targetPoint = new PointF[3]; //横纵坐标点值为单精度浮点float，Point则为整型int
      targetPoint[0].X = 0;
      targetPoint[0].Y = 0;
      targetPoint[1].X = (float)xView - 1;
      targetPoint[1].Y = 0;
      targetPoint[2].X = 0;
      targetPoint[2].Y = (float)yView - 1;

      int xLeft = (int)xWmin;
      int xWidth = (int)xWmax - (int)xWmin + 1;
      int yUpper = (int)yWmin;
      int yHeight = (int)yWmax - (int)yWmin + 1;

      Rectangle scrRec = new Rectangle(xLeft, yUpper, xWidth, yHeight);
      GraphicsUnit units = GraphicsUnit.Pixel;    //枚举类型

      try
      {
        e.DrawImage(bMap, targetPoint, scrRec, units);
      }
      catch (System.Exception ex) { }

      xWmin0 = xWmin;
      yWmin0 = yWmin;
      xWmax0 = xWmax;
      yWmax0 = yWmax;

      return true;
    }

    public bool zoomExtent(Graphics e)  //将图像放至最大
    {
      if (!myImageIsOpen)
        return false;
      else
      {
        xWmin = 0;
        yWmin = 0;
        xWmax = bMap.Width - 1;
        yWmax = bMap.Height - 1;
        zoomImage(e, 0);
        return true;
      }
    }

    public void ZoomInOut(int flag = 0, double MouseX = -100, double MouseY = -100)
    {
      double winWidth, winHeight;
      double winXc, winYc;
      if (myImageIsOpen == false)
        return;
      winXc = (xWmax + xWmin) / 2.0;
      winYc = (yWmax + yWmin) / 2.0;

      if (MouseX < -10)
      {
        winXc = 0;
        winYc = 0;
      }
      else
      {
        winXc = winXc - MouseX;
        winYc = winYc - MouseY;
      }

      winWidth = xWmax - xWmin;
      winHeight = yWmax - yWmin;
      //中心缩放
      if (flag == 0)
      {
        xWmin = MouseX + winXc * 0.8 - winWidth * 0.4;
        xWmax = MouseX + winXc * 0.8 + winWidth * 0.4;
        yWmin = MouseY + winYc * 0.8 - winHeight * 0.4;
        yWmax = MouseY + winYc * 0.8 + winHeight * 0.4;
      }
      else if (flag == 1)
      {
        xWmin = MouseX + winXc * 1.25 - winWidth * 0.625;
        xWmax = MouseX + winXc * 1.25 + winWidth * 0.625;
        yWmin = MouseY + winYc * 1.25 - winHeight * 0.625;
        yWmax = MouseY + winYc * 1.25 + winHeight * 0.625;
      }
    }

    public void MoveImage(Graphics e, int dx, int dy)
    {
      if (myImageIsOpen == false)
        return;
      double wX, wY;
      double vX, vY;
      wX = xWmax - xWmin; //需要显示的宽度
      wY = yWmax - yWmin; //需要显示的高度
      vX = xView;         //画纸的宽度
      vY = yView;         //画纸的高度
      if (wX == 0 & wY == 0) return;

      xWmin = xWmin + (wX - vX) / 2;  //确保图像中间显示？
      xWmax = xWmin + vX;
      yWmin = yWmin + (wY - vY) / 2;
      yWmax = yWmin + vY;

      PointF[] targetPoint = new PointF[3];
      targetPoint[0].X = 0 + dx;
      targetPoint[0].Y = 0 + dy;
      targetPoint[1].X = (float)xView - 1 + dx;
      targetPoint[1].Y = 0 + dy;
      targetPoint[2].X = 0 + dx;
      targetPoint[2].Y = (float)yView - 1 + dy;

      int xLeft = (int)xWmin;
      int xWidth = (int)xWmax - (int)xWmin + 1;
      int yUpper = (int)yWmin;
      int yHeight = (int)yWmax - (int)yWmin + 1;

      Rectangle scrRec = new Rectangle(xLeft, yUpper, xWidth, yHeight);
      GraphicsUnit units = GraphicsUnit.Pixel;
      try
      {
        e.DrawImage(bMap, targetPoint, scrRec, units);
      }
      catch (System.Exception ex) { }

    }
    public double MapToImageX(double x)
    {
      return (x / xView * (xWmax0 - xWmin0) + xWmin0);
    }
    public double MapToImageY(double y)
    {
      return (y / yView * (yWmax0 - yWmin0) + yWmin0);
    }
    #endregion

    #region 灰度直方图
    public void HistCalculation()
    {
      int i, j;
      if (myImageType == 0)   //8位灰度图像计算
      {
        byte a;
        for (i = 0; i < 256; i++)
        {
          GrayNum[i] = 0;
        }
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            a = imageB[i * mBWidth + j];
            GrayNum[a] = GrayNum[a] + 1;
          }
        }
        for (i = 0; i < 256; i++)   //计算概率
        {
          GrayPosibility[i] = GrayNum[i] / mBSize;
        }
        for (i = 0; i < 256; i++)   //获取最小灰度
        {
          if (GrayPosibility[i] != 0)
          {
            grayMin = i; break;
          }
        }
        for (i = 255; i > -1; i--)  //获取最大灰度
        {
          if (GrayPosibility[i] != 0)
          {
            grayMax = i; break;
          }
        }
        entropyH = 0;
        for (i = 0; i < 255; i++)   //计算熵
        {
          if (GrayPosibility[i] != 0) //概率不可以为0否则log函数计算就会出错
          {
            entropyH = -GrayPosibility[i] * Math.Log(GrayPosibility[i], 2) + entropyH;
          }
        }
        //计算平均值
        double s = 0;
        for (i = 0; i < 256; i++)
        {
          s = s + GrayNum[i] * i;
        }
        grayAverage = s / mBSize;
        //计算均方根
        sigma = 0;
        for (i = 0; i < mBSize; i++)
        {
          sigma = sigma + Math.Pow(grayAverage - imageB[i], 2);
        }
        sigma = Math.Sqrt(sigma / mBSize);
      }
      else if (myImageType == 1)  //24位彩色图像计算
      {
        double pos;
        byte a, b, c;
        for (i = 0; i < 256; i++)
        {
          RedNum[i] = 0; GreenNum[i] = 0; BlueNum[i] = 0;
        }
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            pos = i * mCWidth + 3 * j;  //一个像素的三个通道的排列顺序为B G R
            a = imageC[(int)pos];
            b = imageC[(int)pos + 1];
            c = imageC[(int)pos + 2];
            BlueNum[a] = BlueNum[a] + 1;
            GreenNum[b] = GreenNum[b] + 1;
            RedNum[c] = RedNum[c] + 1;
          }
        }
        for (i = 0; i < 256; i++)   //获取各个通道的概率
        {
          RedPosibility[i] = RedNum[i] / mCSize;
          GreenPosibility[i] = GreenNum[i] / mCSize;
          BluePosibility[i] = BlueNum[i] / mCSize;
        }
        //计算最小值
        for (i = 0; i < 256; i++)
        {
          if (RedPosibility[i] != 0 || GreenPosibility[i] != 0 || BluePosibility[i] != 0)
          {
            grayMin = i;
            break;
          }
        }
        //计算最大值
        for (i = 255; i > -1; i++)
        {
          if (RedPosibility[i] != 0 || GreenPosibility[i] != 0 || BluePosibility[i] != 0)
          {
            grayMax = i;
            break;
          }
        }
        //计算平均值
        grayAverage = 0;
        for (i = 0; i < 256; i++)
        {
          grayAverage = grayAverage + (0.299 * RedNum[i] + 0.587 * GreenNum[i] + 0.114 * BlueNum[i]) * i;
        }
        grayAverage = grayAverage / mCSize;
        //计算sigma
        sigma = 0;
        for (i = 0; i < 256; i++)
        {
          sigma = sigma + Math.Pow(0.299 * RedNum[i] + 0.587 * GreenNum[i] + 0.114 * BlueNum[i] - grayAverage, 2);
        }
        sigma = Math.Sqrt(sigma / mCSize);
        //计算熵
        entropyH = 0;
        double[] P = new double[256];
        for (i = 0; i < 256; i++)
        {
          P[i] = 0.299 * RedPosibility[i] + 0.587 * GreenPosibility[i] + 0.114 * BluePosibility[i];
        }
        for (i = 0; i < 256; i++)
        {
          if (P[i] != 0)
          {
            entropyH = entropyH - P[i] * Math.Log(P[i], 2);
          }
        }
      }
    }
    //绘制直方图
    public void HistDraw(Graphics e)
    {
      int i;
      Point pC = new Point();
      Point pX = new Point();
      Point pY = new Point();
      pC.X = 20; pC.Y = 249;
      pX.X = 536; pX.Y = 249;
      pY.X = 20; pY.Y = 20;

      PointF p1 = new PointF();
      PointF p2 = new PointF();

      Pen penAxis = new Pen(Color.Black, 1);
      penAxis.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      penAxis.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
      e.DrawLine(penAxis, pC, pX);
      e.DrawLine(penAxis, pC, pY);

      //绘制坐标信息
      PointF p3 = new PointF();
      PointF p4 = new PointF();
      PointF p5 = new PointF();
      PointF p6 = new PointF();
      Pen penShortLine = new Pen(Color.Black);
      p3.X = 530; p3.Y = 249; p4.X = 530; p4.Y = 239;
      e.DrawLine(penShortLine, p3, p4);
      p5.X = 20; p5.Y = 29; p6.X = 30; p6.Y = 29;
      e.DrawLine(penShortLine, p5, p6);

      Font drawFont1 = new Font("Arial", 10);
      SolidBrush drawBrush = new SolidBrush(Color.Black);
      e.DrawString("O", drawFont1, drawBrush, new PointF(8, 249));
      e.DrawString("i", drawFont1, drawBrush, new PointF(530, 253));
      e.DrawString("Vi", drawFont1, drawBrush, new PointF(5, 5));

      if (myImageType == 0)
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
          p1.X = 20 + 2 * i;
          p1.Y = 29 + 220 * (float)(1 - GrayPosibility[i] / maxP);
          p2.X = 22 + 2 * i;
          p2.Y = 29 + 220 * (float)(1 - GrayPosibility[i + 1] / maxP);
          e.DrawLine(penPolyline, p1, p2);
        }
        //封闭
        p1.X = 530;
        p1.Y = 249;
        e.DrawLine(penPolyline, p2, p1);
      }
      if (myImageType == 1)
      {
        double maxRed = RedPosibility[0];
        double maxGreen = GreenPosibility[0];
        double maxBlue = BluePosibility[0];

        for (i = 0; i < 256; i++)
        {
          if (maxRed < RedPosibility[i]) { maxRed = RedPosibility[i]; }
          if (maxGreen < GreenPosibility[i]) { maxGreen = GreenPosibility[i]; }
          if (maxBlue < BluePosibility[i]) { maxBlue = BluePosibility[i]; }
        }
        Pen penRedPolyline = new Pen(Color.Red);
        Pen penGreenPolyline = new Pen(Color.Green);
        Pen penBluePolyline = new Pen(Color.Blue);

        for (i = 0; i < 255; i++)
        {
          //R通道
          p1.X = 20 + 2 * i;
          p1.Y = 29 + 220 * (float)(1 - RedPosibility[i] / maxRed);
          p2.X = 22 + 2 * i;
          p2.Y = 29 + 220 * (float)(1 - RedPosibility[i + 1] / maxRed);
          e.DrawLine(penRedPolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 530; p1.Y = 249;
            e.DrawLine(penRedPolyline, p2, p1);
          }
          //G通道
          p1.X = 20 + 2 * i;
          p1.Y = 29 + 220 * (float)(1 - GreenPosibility[i] / maxGreen);
          p2.X = 22 + 2 * i;
          p2.Y = 29 + 220 * (float)(1 - GreenPosibility[i + 1] / maxGreen);
          e.DrawLine(penGreenPolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 530; p1.Y = 249;
            e.DrawLine(penGreenPolyline, p2, p1);
          }
          //B通道
          p1.X = 20 + 2 * i;
          p1.Y = 29 + 220 * (float)(1 - BluePosibility[i] / maxBlue);
          p2.X = 22 + 2 * i;
          p2.Y = 29 + 220 * (float)(1 - BluePosibility[i + 1] / maxBlue);
          e.DrawLine(penBluePolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 530; p1.Y = 249;
            e.DrawLine(penBluePolyline, p2, p1);
          }
        }
      }
    }

    //直方图均衡化
    public void HistEqualize(ref imageClass mImage)
    {
      int i, j;
      if (mImage == null) { return; }
      //灰度图像均衡化
      if (mImage.myImageType == 0)
      {
        byte[] pixelNum = new byte[256];
        double[] Nsk = new double[256];
        Nsk[0] = mImage.GrayNum[0];
        for (i = 1; i < 256; i++)
        {
          Nsk[i] = Nsk[i - 1] + mImage.GrayNum[i];
        }

        for (i = 0; i < 256; i++)
        {
          pixelNum[i] = (byte)(255.0 * Nsk[i] / mImage.mBSize + 0.5); //加0.5,四舍五入
        }
        for (i = 0; i < mImage.mHeight; i++)
        {
          for (j = 0; j < mImage.mWidth; j++)
          {
            mImage.imageB[i * mImage.mBWidth + j] = pixelNum[mImage.imageB[i * mImage.mBWidth + j]];
          }
        }
        mImage.putBitMapData();
      }
      //彩色图像均衡化
      if (mImage.myImageType == 1)
      {
        byte[] pixelNumR = new byte[256];
        byte[] pixelNumG = new byte[256];
        byte[] pixelNumB = new byte[256];
        double[] NskR = new double[256];
        double[] NskG = new double[256];
        double[] NskB = new double[256];

        NskR[0] = mImage.RedNum[0];
        NskG[0] = mImage.GreenNum[0];
        NskB[0] = mImage.BlueNum[0];
        for (i = 1; i < 256; i++)
        {
          NskR[i] = NskR[i - 1] + mImage.RedNum[i];
          NskG[i] = NskG[i - 1] + mImage.GreenNum[i];
          NskB[i] = NskB[i - 1] + mImage.BlueNum[i];
        }
        for (i = 0; i < 256; i++)
        {
          pixelNumR[i] = (byte)(255.0 * NskR[i] / mImage.mCSize + 0.5);
          pixelNumG[i] = (byte)(255.0 * NskG[i] / mImage.mCSize + 0.5);
          pixelNumB[i] = (byte)(255.0 * NskB[i] / mImage.mCSize + 0.5);
        }
        double pos;
        for (i = 0; i < mImage.mHeight; i++)
        {
          for (j = 0; j < mImage.mWidth; j++)
          {
            pos = i * mImage.mCWidth + 3 * j;   //mCWidth的宽度已经乘了3，表示字节而不是像素
            mImage.imageC[(long)pos] = pixelNumB[mImage.imageC[(long)pos]];
            mImage.imageC[(long)pos + 1] = pixelNumG[mImage.imageC[(long)pos + 1]];
            mImage.imageC[(long)pos + 2] = pixelNumR[mImage.imageC[(long)pos + 2]];
          }
        }
        mImage.putBitMapData();
      }
    }

    /// <summary>
    /// 灰度变换
    /// </summary>
    /// <returns></returns>
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
      if (myImageType == 0)
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
      if (myImageType == 1)
      {
        double maxRed = RedPosibility[0];
        double maxGreen = GreenPosibility[0];
        double maxBlue = BluePosibility[0];

        for (i = 0; i < 256; i++)
        {
          if (maxRed < RedPosibility[i]) { maxRed = RedPosibility[i]; }
          if (maxGreen < GreenPosibility[i]) { maxGreen = GreenPosibility[i]; }
          if (maxBlue < BluePosibility[i]) { maxBlue = BluePosibility[i]; }
        }
        Pen penRedPolyline = new Pen(Color.Red);
        Pen penGreenPolyline = new Pen(Color.Green);
        Pen penBluePolyline = new Pen(Color.Blue);

        for (i = 0; i < 255; i++)
        {
          //R通道
          p1.X = 20 + i;
          p1.Y = 40 + 210 * (float)(1 - RedPosibility[i] / maxRed);
          p2.X = 20 + i + 1;
          p2.Y = 40 + 210 * (float)(1 - RedPosibility[i + 1] / maxRed);
          e.DrawLine(penRedPolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 275; p1.Y = 250;
            e.DrawLine(penRedPolyline, p2, p1);
          }
          //G通道
          p1.X = 20 + i;
          p1.Y = 40 + 210 * (float)(1 - GreenPosibility[i] / maxGreen);
          p2.X = 20 + i + 1;
          p2.Y = 40 + 210 * (float)(1 - GreenPosibility[i + 1] / maxGreen);
          e.DrawLine(penGreenPolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 275; p1.Y = 250;
            e.DrawLine(penGreenPolyline, p2, p1);
          }
          //B通道
          p1.X = 20 + i;
          p1.Y = 40 + 210 * (float)(1 - BluePosibility[i] / maxBlue);
          p2.X = 21 + i;
          p2.Y = 40 + 210 * (float)(1 - BluePosibility[i + 1] / maxBlue);
          e.DrawLine(penBluePolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 275; p1.Y = 250;
            e.DrawLine(penBluePolyline, p2, p1);
          }
        }
      }
    }

    int a, b, ga, gb;
    public int getA
    {
      get { return a; }
      set { a = value; }
    }
    public int getB
    {
      get { return b; }
      set { b = value; }
    }
    public int getGa
    {
      get { return ga; }
      set { ga = value; }
    }
    public int getGb
    {
      get { return gb; }
      set { gb = value; }
    }
    public byte[] getByte
    {
      get { return imageB; }
      set { imageB = value; }

    }
    public byte[] grayTransform(byte[] grayByte, decimal a, decimal b, decimal ga, decimal gb) //被舍入误差害死了！！！想了好久为什么会出现问题。注：原来a,b,c,d用的是整型。。。
    {
      int i, j, pos;
      grayByte = new byte[mBSize];
      for (i = 0; i < mBData; i++)
      {
        grayByte[i] = imageB[i];
      }
      if (myImageType == 0)
      {
        if (a < b)
        {
          for (i = 0; i < mHeight; i++)
          {
            for (j = 0; j < mWidth; j++)
            {
              pos = i * (int)mBWidth + j;
              if (grayByte[pos] < a)
              {
                grayByte[pos] = (byte)(grayByte[pos] * (ga / a));
                continue;
              }
              if (grayByte[pos] >= a & grayByte[pos] < b)
              {
                grayByte[pos] = (byte)(ga + ((gb - ga) / (b - a)) * (grayByte[pos] - a));
                continue;
              }
              if (grayByte[pos] >= b)
              {
                grayByte[pos] = (byte)(gb + ((255 - gb) / (255 - b)) * (grayByte[pos] - b));
                continue;
              }
            }
          }
        }
      }
      return grayByte;
    }

    public void afterHistDraw(Graphics e, double[] GrayP)
    {
      int i;
      GrayP = new double[256];
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
      if (myImageType == 0)
      {
        //概率最大值
        double maxP = GrayP[0];
        for (i = 0; i < 256; i++)
        {
          if (maxP < GrayP[i]) { maxP = GrayP[i]; }
        }
        //画折线
        Pen penPolyline = new Pen(Color.Gray, 1);
        for (i = 0; i < 255; i++)
        {
          p1.Y = 300 - i;
          p1.X = 250 - 210 * (float)(GrayP[i] / maxP);
          p2.Y = 300 - i - 1;
          p2.X = 250 - 210 * (float)(GrayP[i + 1] / maxP);
          e.DrawLine(penPolyline, p1, p2);
        }
        //封闭
        p1.X = 250;
        p1.Y = 45;
        e.DrawLine(penPolyline, p2, p1);
      }
      if (myImageType == 1)
      {
        double maxRed = RedPosibility[0];
        double maxGreen = GreenPosibility[0];
        double maxBlue = BluePosibility[0];

        for (i = 0; i < 256; i++)
        {
          if (maxRed < RedPosibility[i]) { maxRed = RedPosibility[i]; }
          if (maxGreen < GreenPosibility[i]) { maxGreen = GreenPosibility[i]; }
          if (maxBlue < BluePosibility[i]) { maxBlue = BluePosibility[i]; }
        }
        Pen penRedPolyline = new Pen(Color.Red);
        Pen penGreenPolyline = new Pen(Color.Green);
        Pen penBluePolyline = new Pen(Color.Blue);

        for (i = 0; i < 255; i++)
        {
          //R通道
          p1.X = 20 + i;
          p1.Y = 40 + 210 * (float)(1 - RedPosibility[i] / maxRed);
          p2.X = 20 + i + 1;
          p2.Y = 40 + 210 * (float)(1 - RedPosibility[i + 1] / maxRed);
          e.DrawLine(penRedPolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 250; p1.Y = 45;
            e.DrawLine(penRedPolyline, p2, p1);
          }
          //G通道
          p1.X = 20 + i;
          p1.Y = 40 + 210 * (float)(1 - GreenPosibility[i] / maxGreen);
          p2.X = 20 + i + 1;
          p2.Y = 40 + 210 * (float)(1 - GreenPosibility[i + 1] / maxGreen);
          e.DrawLine(penGreenPolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 250; p1.Y = 45;
            e.DrawLine(penGreenPolyline, p2, p1);
          }
          //B通道
          p1.X = 20 + i;
          p1.Y = 40 + 210 * (float)(1 - BluePosibility[i] / maxBlue);
          p2.X = 21 + i;
          p2.Y = 40 + 210 * (float)(1 - BluePosibility[i + 1] / maxBlue);
          e.DrawLine(penBluePolyline, p1, p2);
          if (i == 254)
          {
            p1.X = 250; p1.Y = 45;
            e.DrawLine(penBluePolyline, p2, p1);
          }
        }
      }

    }
    #endregion

    #region 调色板
    public ColorPalette getPalette()
    {
      ColorPalette cp;
      cp = bMap.Palette;
      return cp;
    }
    /// <summary>
    /// 调色板编辑
    /// </summary>
    /// <param name="newPalette"></param>
    public void setPalette(ColorPalette cp)
    {
      if (cp != null)
      {
        bMap.Palette = cp;
      }
    }

    public void redPalette()
    {
      try
      {
        ColorPalette mPalette;
        mPalette = bMap.Palette;
        for (int i = 0; i < mPalette.Entries.Length; i++)
        {
          mPalette.Entries[i] = Color.FromArgb(255, i, 0, 0);
        }
        bMap.Palette = mPalette;
      }
      catch (System.Exception ex)
      {

      }
    }
    public void greenPalette()
    {
      try
      {
        ColorPalette mPalette = bMap.Palette;
        for (int i = 0; i < mPalette.Entries.Length; i++)
        {
          mPalette.Entries[i] = Color.FromArgb(255, 0, i, 0);
        }
        bMap.Palette = mPalette;
      }
      catch (System.Exception ex)
      {
        {

        }
      }
    }
    public void bluePalette()
    {
      try
      {
        ColorPalette mPalette = bMap.Palette;
        for (int i = 0; i < mPalette.Entries.Length; i++)
        {
          mPalette.Entries[i] = Color.FromArgb(255, 0, 0, i);
        }
        bMap.Palette = mPalette;
      }
      catch (System.Exception ex)
      {

      }
    }
    public void yellowPalette()
    {
      try
      {
        ColorPalette mPalette = bMap.Palette;
        for (int i = 0; i < mPalette.Entries.Length; i++)
        {
          mPalette.Entries[i] = Color.FromArgb(255, i, i, 0);
        }
        bMap.Palette = mPalette;
      }
      catch (System.Exception ex)
      {

      }
    }
    public void cyanPalette()
    {
      try
      {
        ColorPalette mPalette = bMap.Palette;
        for (int i = 0; i < mPalette.Entries.Length; i++)
        {
          mPalette.Entries[i] = Color.FromArgb(255, 0, i, i); //cyan [255,0,255,255]
        }
        bMap.Palette = mPalette;
      }
      catch (System.Exception ex)
      {

      }
    }
    public void purplePalette()
    {
      try
      {
        ColorPalette mPalette = bMap.Palette;
        for (int i = 0; i < mPalette.Entries.Length; i++)
        {
          mPalette.Entries[i] = Color.FromArgb(255, i * 128 / 255, 0, i * 128 / 255); //purple [255,128,0,128]
        }
        bMap.Palette = mPalette;
      }
      catch (System.Exception ex)
      {

      }
    }
    public void specifiedPalette()
    {
      try
      {
        ColorPalette mPalette = bMap.Palette;
        ColorDialog cd = new ColorDialog();
        cd.AnyColor = true;
        cd.SolidColorOnly = true;
        Color co = new Color();
        var result = cd.ShowDialog();
        if (result == DialogResult.OK)
        {
          try
          {
            co = cd.Color;
            for (int i = 0; i < mPalette.Entries.Length; i++)
            {
              mPalette.Entries[i] = Color.FromArgb(255, i * co.R / 255, i * co.G / 255, i * co.B / 255); //purple [255,128,0,128]
            }
          }
          catch (System.Exception ex) { }
        }

        bMap.Palette = mPalette;
      }
      catch (System.Exception ex)
      {

      }
    }
    public void negativePalette()
    {
      try
      {
        ColorPalette mPalette = bMap.Palette;
        Color co = new Color();
        for (int i = 0; i < mPalette.Entries.Length; i++)
        {
          co = mPalette.Entries[i];
          mPalette.Entries[i] = Color.FromArgb(255, 255 - co.R, 255 - co.G, 255 - co.B);
        }
        bMap.Palette = mPalette;

      }
      catch (System.Exception ex) { }
    }
    public void IndexColorPalToGray()
    {
      ColorPalette mPalette;
      mPalette = bMap.Palette;
      byte temp;
      //---Gray = 0.30R + 0.59G + 0.11B
      for (int i = 0; i < mPalette.Entries.Length; i++)
      {
        temp = (byte)(bMap.Palette.Entries[i].R * 0.3 + bMap.Palette.Entries[i].G * 0.59 + bMap.Palette.Entries[i].B * 0.11);
        mPalette.Entries[i] = Color.FromArgb(255, temp, temp, temp);
      }
      bMap.Palette = mPalette;
    }
    public void IndexImageToGrayImage()
    {
      //---原始数据是调色板的索引，并且数据的值就是调色板通道从0到255的值
      //---将原始数据对调色板的索引转换为灰度，这个灰度就是调色板对应的灰度
      long i, j, pos;
      ColorPalette mPalette;
      mPalette = bMap.Palette;
      byte[] temp = new byte[256];
      //---Gray = 0.30R + 0.59G + 0.11B
      for (i = 0; i < mPalette.Entries.Length; i++)
      {
        temp[i] = (byte)(bMap.Palette.Entries[i].R * 0.3 + bMap.Palette.Entries[i].G * 0.59 + bMap.Palette.Entries[i].B * 0.11);

        mPalette.Entries[i] = Color.FromArgb(255, (int)i, (int)i, (int)i);
      }
      bMap.Palette = mPalette;
      for (i = 0; i < mHeight; i++)
      {
        for (j = 0; j < mWidth; j++)
        {
          pos = i * mBWidth + j;
          imageB[pos] = temp[imageB[pos]];
        }
      }
      putBitMapData();
    }
    //---彩色转灰度
    public void ColorToGray()
    {
      long i, j, pos;
      byte tempGray;
      //-------Gray = 0.3R + 0.59G + 0.11B
      for (i = 0; i < mHeight; i++)
      {
        for (j = 0; j < mWidth; j++)
        {
          pos = i * mCWidth + 3 * j;
          tempGray = (byte)(imageC[pos] * 0.3 + imageC[pos + 1] * 0.59 + imageC[pos + 2] * 0.11);
          imageC[pos] = imageC[pos + 1] = imageC[pos + 2] = tempGray;
        }
      }
      putBitMapData();
      myImageType = 0;
    }

    public void ColorImageChangeChannel()
    {

    }

    #endregion

    #region 图像变换基本处理
    public void negative()  //负片
    {
      long pos;
      int i, j;
      if (myImageType == 0)
      {
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            pos = i * mBWidth + j;
            imageB[pos] = (byte)(255 - imageB[pos]);    //花了好多时间。。。
          }
        }
        putBitMapData();
      }
      else if (myImageType == 1)
      {
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth; j++)        //彩色图像一个宽度单位有三个字节
          {
            pos = CFirstPosition[i] + j * 3;
            imageC[pos] = (byte)(255 - imageC[pos]);
            imageC[pos + 1] = (byte)(255 - imageC[pos + 1]);
            imageC[pos + 2] = (byte)(255 - imageC[pos + 2]);
          }
        }
        putBitMapData();
      }
    }

    public void MirrorX()   //垂直镜像
    {
      double i, j, pos0, pos1;
      byte[] temp = new byte[3];
      if (myImageType == 0)
      {
        for (i = 0; i < mHeight / 2; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            pos0 = mBWidth * i + j;
            pos1 = mBWidth * (mHeight - i - 1) + j;
            temp[0] = imageB[(int)pos0];
            imageB[(int)pos0] = imageB[(int)pos1];
            imageB[(int)pos1] = temp[0];
          }
        }
        putBitMapData();
      }
      else if (myImageType == 1)
      {
        for (i = 0; i < mHeight / 2; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            pos0 = mCWidth * i + 3 * j;
            pos1 = mCWidth * (mHeight - i - 1) + 3 * j;
            temp[0] = imageC[(int)pos0];
            temp[1] = imageC[(int)pos0 + 1];
            temp[2] = imageC[(int)pos0 + 2];
            imageC[(int)pos0] = imageC[(int)pos1];
            imageC[(int)pos0 + 1] = imageC[(int)pos1 + 1];
            imageC[(int)pos0 + 2] = imageC[(int)pos1 + 2];
            imageC[(int)pos1] = temp[0];
            imageC[(int)pos1 + 1] = temp[1];
            imageC[(int)pos1 + 2] = temp[2];
          }
        }
        putBitMapData();
      }
    }

    public void MirrorY()   //水平镜像
    {
      double i, j, pos0, pos1;
      //double medium;
      byte[] temp = new byte[3];
      if (myImageType == 0)
      {
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth / 2; j++)
          {
            pos0 = i * mBWidth + j;
            pos1 = i * mBWidth + mWidth - j - 1;
            temp[0] = imageB[(int)pos0];
            imageB[(int)pos0] = imageB[(int)pos1];
            imageB[(int)pos1] = temp[0];
          }
        }
        putBitMapData();

      }
      else if (myImageType == 1)
      {
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth / 2; j++)
          {
            pos0 = i * mCWidth + 3 * j;
            pos1 = i * mCWidth + 3 * mWidth - 3 * j - 3;
            temp[0] = imageC[(int)pos0];
            temp[1] = imageC[(int)pos0 + 1];
            temp[2] = imageC[(int)pos0 + 2];
            imageC[(int)pos0] = imageC[(int)pos1];
            imageC[(int)pos0 + 1] = imageC[(int)pos1 + 1];
            imageC[(int)pos0 + 2] = imageC[(int)pos1 + 2];
            imageC[(int)pos1] = temp[0];
            imageC[(int)pos1 + 1] = temp[1];
            imageC[(int)pos1 + 2] = temp[2];
          }
        }
        putBitMapData();

      }
    }

    public void MirrorOrigin()  //中心镜像
    {
      double i, j, pos0, pos1, pos2, pos3;
      byte[] temp = new byte[3];
      if (myImageType == 0)
      {
        for (i = 0; i < mHeight / 2; i++)
        {
          for (j = 0; j < mWidth / 2; j++)
          {
            pos0 = i * mBWidth + j;
            pos1 = mBWidth * (mHeight - i - 1) + mWidth - j - 1;
            temp[0] = imageB[(int)pos0];
            imageB[(int)pos0] = imageB[(int)pos1];
            imageB[(int)pos1] = temp[0];

            pos2 = i * mBWidth + mWidth - j - 1;
            pos3 = mBWidth * (mHeight - i - 1) + j;
            temp[1] = imageB[(int)pos2];
            imageB[(int)pos2] = imageB[(int)pos3];
            imageB[(int)pos3] = temp[1];
          }
        }
        putBitMapData();
      }
      else if (myImageType == 1)
      {
        for (i = 0; i < mHeight / 2; i++)
        {
          for (j = 0; j < mWidth / 2; j++)
          {
            pos0 = i * mCWidth + 3 * j;
            pos1 = mCWidth * (mHeight - i - 1) + 3 * (mWidth - j - 1);
            temp[0] = imageC[(int)pos0];
            temp[1] = imageC[(int)(pos0 + 1)];
            temp[2] = imageC[(int)(pos0 + 2)];
            imageC[(int)pos0] = imageC[(int)pos1];
            imageC[(int)pos0 + 1] = imageC[(int)pos1 + 1];
            imageC[(int)pos0 + 2] = imageC[(int)pos1 + 2];
            imageC[(int)pos1] = temp[0];
            imageC[(int)pos1 + 1] = temp[1];
            imageC[(int)pos1 + 2] = temp[2];

            pos2 = i * mCWidth + 3 * (mWidth - j - 1);
            pos3 = mCWidth * (mHeight - i - 1) + 3 * j;
            temp[0] = imageC[(int)pos2];
            temp[1] = imageC[(int)(pos2 + 1)];
            temp[2] = imageC[(int)(pos2 + 2)];
            imageC[(int)pos2] = imageC[(int)pos3];
            imageC[(int)pos2 + 1] = imageC[(int)pos3 + 1];
            imageC[(int)pos2 + 2] = imageC[(int)pos3 + 2];
            imageC[(int)pos3] = temp[0];
            imageC[(int)pos3 + 1] = temp[1];
            imageC[(int)pos3 + 2] = temp[2];
          }
          putBitMapData();
        }
      }
    }
    #endregion

    #region 滤波处理
    public void addSaltPepperNoise()    //加椒盐
    {
      int i, j, k, pos;
      Random ran = new Random();
      if (myImageType == 0)
      {
        int pixelNum = (int)(mBSize * 0.01);
        for (k = 0; k < pixelNum; k++)
        {
          i = ran.Next(2, (int)mHeight - 2);  //ran.Next(a,b) >=a and  <b
          j = ran.Next(2, (int)mWidth - 2);
          pos = i * (int)mBWidth + j;
          imageB[pos] = 0;
        }
        for (k = 0; k < pixelNum; k++)
        {
          i = ran.Next(2, (int)mHeight - 2);  //ran.Next(a,b) >=a and  <b
          j = ran.Next(2, (int)mWidth - 2);
          pos = i * (int)mBWidth + j;
          imageB[pos] = 255;
        }
        putBitMapData();
      }
    }

    public void MediumFilter()      //中值滤波 1X5滤波器
    {
      long i, j, pos;
      long a, b;
      byte[] pixelMat = new byte[5]; //内部采用1X5，边缘不考虑
      byte tempPixel = 0;
      if (myImageType == 0)
      {
        byte[] tempImageB = new byte[mBData];   //备份数据
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            pos = i * mBWidth + j;
            tempImageB[pos] = imageB[pos];
          }
        }
        for (i = 0; i < mHeight; i++)
        {
          for (j = 2; j < mWidth - 2; j++)
          {
            pos = i * mBWidth + j;
            pixelMat[0] = tempImageB[pos - 2];    //滤波器数组赋值
            pixelMat[1] = tempImageB[pos - 1];
            pixelMat[2] = tempImageB[pos];
            pixelMat[3] = tempImageB[pos + 1];
            pixelMat[4] = tempImageB[pos + 2];
            for (a = 0; a < 3; a++)     //排序求中值，只需运行一半
            {
              for (b = a + 1; b < 5; b++)
              {
                if (pixelMat[a] > pixelMat[b])
                {
                  tempPixel = pixelMat[a];
                  pixelMat[a] = pixelMat[b];
                  pixelMat[b] = tempPixel;
                }
              }
            }
            imageB[pos] = pixelMat[2];
          }
        }
        putBitMapData();
      }
    }

    public void AverageFilter() //均值滤波
    {
      long i, j, pos, a;
      byte[] pixelMat = new byte[5]; //内部采用1X5，边缘不考虑
      double tempPixel = 0;
      if (myImageType == 0)
      {
        byte[] tempImageB = new byte[mBData];   //备份数据
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            pos = i * mBWidth + j;
            tempImageB[pos] = imageB[pos];
          }
        }
        for (i = 0; i < mHeight; i++)
        {
          for (j = 2; j < mWidth - 2; j++)
          {
            pos = i * mBWidth + j;
            pixelMat[0] = tempImageB[pos - 2];    //滤波器数组赋值
            pixelMat[1] = tempImageB[pos - 1];
            pixelMat[2] = tempImageB[pos];
            pixelMat[3] = tempImageB[pos + 1];
            pixelMat[4] = tempImageB[pos + 2];
            tempPixel = pixelMat[0];
            for (a = 1; a < 5; a++)
            {
              tempPixel = tempPixel + pixelMat[a];
            }
            imageB[pos] = (byte)(tempPixel / 5);
          }
        }
        putBitMapData();
      }
    }
    //K近旁中值滤波器 KNNMF，其中K=5，边界不考虑。由于加加椒盐时，两个椒盐可能挨得很近，因此K=3时并不能将所有椒盐滤去，经过尝试，k=5时效果更好
    public void EdgeMediumFilter()
    {
      long i, j;
      int a, b = 0;
      long pos = 0;
      if (myImageType == 0)
      {
        byte[] tempImageB = new byte[mBData];   //一定要备份源数据
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            tempImageB[i * mBWidth + j] = imageB[i * mBWidth + j];
          }
        }
        List<byte> tempList = new List<byte>();
        List<byte> recordList = new List<byte>();
        byte min;
        for (i = 1; i < mHeight - 1; i++)
        {
          for (j = 1; j < mWidth - 1; j++)
          {
            pos = i * mBWidth + j;
            tempList.Clear();   //初始化
            recordList.Clear();
            min = 255;
            recordList.Add(tempImageB[pos]);    //像素本身需要加进来
            tempList.Add(tempImageB[pos - mBWidth - 1]);
            tempList.Add(tempImageB[pos - mBWidth]);
            tempList.Add(tempImageB[pos - mBWidth + 1]);
            tempList.Add(tempImageB[pos - 1]);
            tempList.Add(tempImageB[pos + 1]);
            tempList.Add(tempImageB[pos + mBWidth - 1]);
            tempList.Add(tempImageB[pos + mBWidth]);
            tempList.Add(tempImageB[pos + mBWidth + 1]);
            for (a = 0; a < tempList.Count; a++)
            {
              if (min > Math.Abs(tempList[a] - tempImageB[pos]))
              {
                min = (byte)Math.Abs(tempList[a] - tempImageB[pos]);
                b = a;
              }
            }
            recordList.Add(tempList[b]);
            tempList.Remove(tempList[b]);
            min = 255;
            for (a = 0; a < tempList.Count; a++)
            {
              if (min > Math.Abs(tempList[a] - tempImageB[pos]))
              {
                min = (byte)Math.Abs(tempList[a] - tempImageB[pos]);
                b = a;
              }
            }
            recordList.Add(tempList[b]);
            tempList.Remove(tempList[b]);
            min = 255;
            for (a = 0; a < tempList.Count; a++)
            {
              if (min > Math.Abs(tempList[a] - tempImageB[pos]))
              {
                min = (byte)Math.Abs(tempList[a] - tempImageB[pos]);
                b = a;
              }
            }
            recordList.Add(tempList[b]);
            tempList.Remove(tempList[b]);
            min = 255;
            for (a = 0; a < tempList.Count; a++)
            {
              if (min > Math.Abs(tempList[a] - tempImageB[pos]))
              {
                min = (byte)Math.Abs(tempList[a] - tempImageB[pos]);
                b = a;
              }
            }
            recordList.Add(tempList[b]);
            recordList.Sort();
            imageB[pos] = recordList[2];
          }
        }
        putBitMapData();
      }
    }

    public void EdgeAverageFilter()
    {
      long i, j;
      int a, b = 0;
      long pos = 0;
      if (myImageType == 0)
      {
        byte[] tempImageB = new byte[mBData];   //一定要备份源数据
        for (i = 0; i < mHeight; i++)
        {
          for (j = 0; j < mWidth; j++)
          {
            tempImageB[i * mBWidth + j] = imageB[i * mBWidth + j];
          }
        }
        List<byte> tempList = new List<byte>();
        List<byte> recordList = new List<byte>();
        byte min;
        for (i = 1; i < mHeight - 1; i++)
        {
          for (j = 1; j < mWidth - 1; j++)
          {
            pos = i * mBWidth + j;
            tempList.Clear();   //初始化
            recordList.Clear();
            min = 255;
            recordList.Add(tempImageB[pos]);    //像素本身需要加进来
            tempList.Add(tempImageB[pos - mBWidth - 1]);
            tempList.Add(tempImageB[pos - mBWidth]);
            tempList.Add(tempImageB[pos - mBWidth + 1]);
            tempList.Add(tempImageB[pos - 1]);
            tempList.Add(tempImageB[pos + 1]);
            tempList.Add(tempImageB[pos + mBWidth - 1]);
            tempList.Add(tempImageB[pos + mBWidth]);
            tempList.Add(tempImageB[pos + mBWidth + 1]);
            for (a = 0; a < tempList.Count; a++)
            {
              if (min > Math.Abs(tempList[a] - tempImageB[pos]))
              {
                min = (byte)Math.Abs(tempList[a] - tempImageB[pos]);
                b = a;
              }
            }
            recordList.Add(tempList[b]);
            tempList.Remove(tempList[b]);
            min = 255;
            for (a = 0; a < tempList.Count; a++)
            {
              if (min > Math.Abs(tempList[a] - tempImageB[pos]))
              {
                min = (byte)Math.Abs(tempList[a] - tempImageB[pos]);
                b = a;
              }
            }
            recordList.Add(tempList[b]);
            imageB[pos] = (byte)((recordList[0] + recordList[1] + recordList[2]) / 3);
          }
        }
        putBitMapData();
      }
    }


    public bool isAvailable()
    {
      if (bMap == null || myImageIsOpen == false)
        return false;
      else
        return true;
    }
    #endregion

    #region 属性接口处理
    public double EntropyH() { return entropyH; }
    public double Sigma() { return sigma; }
    public double GrayMin() { return grayMin; }
    public double GrayMax() { return grayMax; }
    public double GrayAverage() { return grayAverage; }

    public double getXView  //获取属性，不是函数
    {
      get { return xView; }
      set { xView = value; }
    }

    public double getYView
    {
      get { return yView; }
      set { yView = value; }
    }


    public int MyImageType          //获取图像类型
    {
      get { return myImageType; }
      set { myImageType = value; }
    }
    public long MWidth
    {
      get { return mWidth; }
    }
    public long MHeight
    {
      get { return mHeight; }
    }
    public byte[] ImageB
    {
      get { return imageB; }
      private set { imageB = value; }
    }
    public long MBWidth
    {
      get { return mBWidth; }
    }
    public long MBData
    {
      get { return mBData; }
    }
    public Bitmap BMap
    {
      get { return bMap; }
      set //图像数据也应该跟着一起变化
      {
        bMap = value;
        getBitMapData();
        //对参量初始化
        xWmin = 0;
        yWmin = 0;
        xWmax = bMap.Width - 1;
        yWmax = bMap.Height - 1;

      }
    }

    public byte GetGray(int i, int j)
    {
      try
      {
        long pos = i * mBWidth + j;
        return imageB[pos];
      }
      catch (System.Exception ex) { return 0; }
    }
    public double xWinMin
    {
      get { return xWmin; }
      set { xWmin = value; }
    }
    public double xWinMax
    {
      get { return xWmax; }
      set { xWmax = value; }
    }
    public double yWinMin
    {
      get { return yWmin; }
      set { yWmin = value; }
    }
    public double yWinMax
    {
      get { return yWmax; }
      set { yWmax = value; }
    }
    public long MCWidth
    {
      get { return mCWidth; }
      set { mCWidth = value; }
    }
    public byte[] ImageC
    {
      get { return imageC; }
      set { imageC = value; }
    }
    #endregion
  }
}