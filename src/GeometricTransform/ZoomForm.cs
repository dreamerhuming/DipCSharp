// Copyright (c) 2025 Ming Hu. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DipCSharp
{
  public partial class ZoomForm : Form
  {
    imageClass pImage = new imageClass();
    MainForm mainFF;
    ToolStripProgressBar tspBar = new ToolStripProgressBar();
    Bitmap newBitmap;
    byte[] tempImageB;
    public ZoomForm()
    {
      InitializeComponent();
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

    private void ZoomForm_Load(object sender, EventArgs e)
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
      numericUpDown1.Value = 2.0M;
      numericUpDown1.DecimalPlaces = 1;
      trackBar1.Value = 100;
    }

    private void trackBar1_ValueChanged(object sender, EventArgs e)
    {
      numericUpDown1.Value = decimal.Parse((((double)trackBar1.Value / 100) + 1).ToString("0.0"));
      this.Refresh();
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
      try
      {
        trackBar1.Value = (int)((numericUpDown1.Value - 1) * 100);
        this.Refresh();
      }
      catch (System.Exception ex) { }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      double zoomFactor;
      long i, j, pos, oriPos;
      zoomFactor = (double)this.numericUpDown1.Value;
      long imHeight = (long)(pImage.BMap.Height / zoomFactor);
      long imWidth = (long)(pImage.BMap.Width / zoomFactor);
      long imBWidth = ((imWidth + 3) / 4) * 4;
      long imBData = imHeight * imBWidth;
      tempImageB = new byte[imBData];
      Rectangle rec = new Rectangle(0, 0, (int)imWidth, (int)imHeight);
      for (i = 0; i < imHeight; i++)
      {
        for (j = 0; j < imWidth; j++)
        {
          pos = i * imBWidth + j;
          oriPos = (long)(i * zoomFactor) * pImage.MBWidth + (long)(zoomFactor * j);
          try
          {
            tempImageB[pos] = pImage.ImageB[oriPos];
          }
          catch (System.Exception ex) { }
        }
      }

      //------由这里可以知道，灰度图像也是有调色板的，只不过调色板的256个通道是灰度---------
      try
      {
        newBitmap = new Bitmap((int)imBWidth, (int)imHeight, PixelFormat.Format8bppIndexed);
        newBitmap.Palette = pImage.BMap.Palette;
        BitmapData bmpData = newBitmap.LockBits(rec, ImageLockMode.WriteOnly, newBitmap.PixelFormat);
        IntPtr ptr = bmpData.Scan0;
        System.Runtime.InteropServices.Marshal.Copy(tempImageB, 0, ptr, (int)imBData);
        newBitmap.UnlockBits(bmpData);
        pImage.BMap = newBitmap;
      }
      catch (System.Exception ex) { }
      mainFF.Refresh();

    }

    private void button2_Click(object sender, EventArgs e)
    {
      double zoomFactor;
      long i, j, pos, oriPos;
      zoomFactor = (double)this.numericUpDown1.Value;
      long imHeight = (long)(pImage.BMap.Height * zoomFactor);
      long imWidth = (long)(pImage.BMap.Width * zoomFactor);
      long imBWidth = ((imWidth + 3) / 4) * 4;
      long imBData = imBWidth * imHeight;

      tempImageB = new byte[imBData];
      Rectangle rec = new Rectangle(0, 0, (int)imWidth, (int)imHeight);
      tspBar.Maximum = (int)imBData;
      tspBar.Minimum = 0;
      for (i = 0; i < imHeight; i++)
      {
        for (j = 0; j < imWidth; j++)
        {
          pos = i * imBWidth + j;
          tspBar.Value = (int)pos;
          oriPos = (long)(i / zoomFactor) * pImage.MBWidth + (long)(j / zoomFactor);
          try
          {
            tempImageB[pos] = pImage.ImageB[oriPos];
          }
          catch (System.Exception ex) { }
        }
      }

      try
      {
        newBitmap = new Bitmap((int)imWidth, (int)imHeight, PixelFormat.Format8bppIndexed);
        newBitmap.Palette = pImage.BMap.Palette;
        BitmapData bmpData = newBitmap.LockBits(rec, ImageLockMode.WriteOnly, newBitmap.PixelFormat);
        IntPtr ptr = bmpData.Scan0;
        System.Runtime.InteropServices.Marshal.Copy(tempImageB, 0, ptr, (int)imBData);
        newBitmap.UnlockBits(bmpData);
        pImage.BMap = newBitmap;
      }
      catch (System.Exception ex) { }
      tspBar.Value = 0;
      mainFF.Refresh();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.Close();
      this.Dispose();
    }

  }
}