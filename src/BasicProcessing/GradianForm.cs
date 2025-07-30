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
  public partial class GradianForm : Form
  {
    imageClass pImage = new imageClass();
    MainForm mainFF;
    public GradianForm()
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
    byte[] tempImageB;

    private void button1_Click(object sender, EventArgs e)
    {
      long i, j, pos;
      tempImageB = new byte[pImage.MBData];
      for (i = 0; i < pImage.MHeight; i++)
      {
        for (j = 0; j < pImage.MWidth; j++)
        {
          pos = i * pImage.MBWidth + j;
          tempImageB[pos] = pImage.ImageB[pos];
        }
      }
      double tempGra = 0;
      byte Gra = 0;
      List<double> tempList = new List<double>();
      for (i = 0; i < pImage.MHeight - 1; i++)
      {
        for (j = 0; j < pImage.MWidth - 1; j++)
        {
          pos = i * pImage.MBWidth + j;
          //不同的梯度
          //i表示X轴方向，j表示Y轴方向
          if (radioButton1.Checked == true)
          {
            tempGra = Math.Abs(tempImageB[pos + pImage.MBWidth] - tempImageB[pos]) + Math.Abs(tempImageB[pos + 1] - tempImageB[pos]);
          }
          if (radioButton2.Checked == true)
          {
            tempGra = Math.Max(Math.Abs(tempImageB[pos + pImage.MBWidth] - tempImageB[pos]), Math.Abs(tempImageB[pos + 1] - tempImageB[pos]));
          }
          if (radioButton3.Checked == true)
          {
            tempGra = Math.Sqrt(Math.Pow(tempImageB[pos + pImage.MBWidth] - tempImageB[pos], 2) +
                Math.Pow(tempImageB[pos + 1] - tempImageB[pos], 2));
          }
          if (radioButton4.Checked == true)
          {
            tempGra = Math.Abs(tempImageB[pos + pImage.MBWidth + 1] - tempImageB[pos]) +
                Math.Abs(tempImageB[pos + 1] - tempImageB[pos + pImage.MBWidth]);
          }
          if (radioButton5.Checked == true)
          {
            tempGra = Math.Max(Math.Abs(tempImageB[pos + pImage.MBWidth + 1] - tempImageB[pos]),
                Math.Abs(tempImageB[pos + 1] - tempImageB[pos + pImage.MBWidth]));
          }
          if (radioButton6.Checked == true)
          {
            tempGra = Math.Sqrt(Math.Pow(tempImageB[pos + pImage.MBWidth + 1] - tempImageB[pos], 2) +
                Math.Pow(tempImageB[pos + 1] - tempImageB[pos + pImage.MBWidth], 2));
          }
          tempList.Add(tempGra);
          //超限处理方式1：
          if (checkBox1.Checked == false)
          {
            if (tempGra > 255)
              tempGra = 255;
            if (tempGra < 0)
              tempGra = 0;
            Gra = (byte)tempGra;
            pImage.ImageB[pos] = Gra;
          }
        }
      }
      //超限处理方法2：灰度超限线性约束
      double minGra = double.MaxValue, maxGra = 0;
      int a, index = 0;
      if (checkBox1.Checked == true)
      {
        for (a = 0; a < tempList.Count; a++)
        {
          if (minGra > tempList[a])
            minGra = tempList[a];
          if (maxGra < tempList[a])
            maxGra = tempList[a];
        }
        for (i = 0; i < pImage.MHeight - 1; i++)
        {
          for (j = 0; j < pImage.MWidth - 1; j++)
          {
            pos = i * pImage.MBWidth + j;
            pImage.ImageB[pos] = (byte)(((tempList[index] - minGra) / (maxGra - minGra)) * 255);
            index++;
          }
        }
      }
      pImage.putBitMapData();
      mainFF.Refresh();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (tempImageB != null)
      {
        long i, j, pos;
        for (i = 0; i < pImage.MHeight; i++)
        {
          for (j = 0; j < pImage.MWidth; j++)
          {
            pos = i * pImage.MBWidth + j;
            pImage.ImageB[pos] = tempImageB[pos];
          }
        }
        pImage.putBitMapData();
        mainFF.Refresh();
      }
    }

  }
}