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
  public partial class ConvolutionForm : Form
  {
    TextBox[] mTextBox = new TextBox[81];
    Point[] UpperLeft = new Point[81];
    MainForm mainFF;
    public ConvolutionForm()
    {
      InitializeComponent();
    }
    imageClass pImage = new imageClass();
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
    /// <summary>
    /// 加载窗体
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ConvolutionForm_Load(object sender, EventArgs e)
    {
      int i, j, pos;
      textBox1.Text = "9";
      for (i = 0; i < 9; i++)
      {
        for (j = 0; j < 9; j++)
        {
          pos = i * 9 + j;
          UpperLeft[pos].X = 13 + j * (25 + 4);
          UpperLeft[pos].Y = 15 + i * (25 + 4);
          mTextBox[pos] = new TextBox();
          mTextBox[pos].Location = UpperLeft[pos];
          mTextBox[pos].Text = "";
          mTextBox[pos].TextAlign = HorizontalAlignment.Center;
          mTextBox[pos].Width = 25;
          mTextBox[pos].Height = 25;
          mTextBox[pos].Enabled = false;
          this.groupBox2.Controls.Add(mTextBox[pos]);
        }
      }
      for (i = 3; i < 6; i++)
      {
        for (j = 3; j < 6; j++)
        {
          pos = i * 9 + j;
          mTextBox[pos].Enabled = true;
          mTextBox[pos].Text = "1";
        }
      }
      radioButton1.Checked = false;
      radioButton2.Checked = false;
      radioButton3.Checked = false;
      radioButton4.Checked = false;
      radioButton5.Checked = false;
      radioButton6.Checked = false;
      radioButton7.Checked = true;
      checkBox1.Checked = false;
    }


    /// <summary>
    /// RadioButton处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      int i, j, pos;
      if (radioButton1.Checked == true)
      {
        for (i = 0; i < 9; i++)
        {
          for (j = 0; j < 9; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Text = "";
            mTextBox[pos].Enabled = false;
          }
        }
        for (i = 3; i < 6; i++)
        {
          for (j = 3; j < 6; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Enabled = true;
          }
        }
        mTextBox[30].Text = "-1";
        mTextBox[31].Text = "-1";
        mTextBox[32].Text = "-1";
        mTextBox[39].Text = "0";
        mTextBox[40].Text = "0";
        mTextBox[41].Text = "0";
        mTextBox[48].Text = "1";
        mTextBox[49].Text = "1";
        mTextBox[50].Text = "1";
        textBox1.Text = "1";
        numericUpDown1.Value = 3;
      }
      this.Refresh();

    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      int i, j, pos;
      if (radioButton2.Checked == true)
      {
        for (i = 0; i < 9; i++)
        {
          for (j = 0; j < 9; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Text = "";
            mTextBox[pos].Enabled = false;
          }
        }
        for (i = 3; i < 6; i++)
        {
          for (j = 3; j < 6; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Enabled = true;
          }
        }
        mTextBox[30].Text = "-1";
        mTextBox[31].Text = "0";
        mTextBox[32].Text = "1";
        mTextBox[39].Text = "-1";
        mTextBox[40].Text = "0";
        mTextBox[41].Text = "1";
        mTextBox[48].Text = "-1";
        mTextBox[49].Text = "0";
        mTextBox[50].Text = "1";
        textBox1.Text = "1";
        numericUpDown1.Value = 3;
      }
      this.Refresh();
    }

    private void radioButton3_CheckedChanged(object sender, EventArgs e)
    {
      int i, j, pos;
      if (radioButton3.Checked == true)
      {
        for (i = 0; i < 9; i++)
        {
          for (j = 0; j < 9; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Text = "";
            mTextBox[pos].Enabled = false;
          }
        }
        for (i = 3; i < 6; i++)
        {
          for (j = 3; j < 6; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Enabled = true;
          }
        }
        mTextBox[30].Text = "-1";
        mTextBox[31].Text = "-2";
        mTextBox[32].Text = "-1";
        mTextBox[39].Text = "0";
        mTextBox[40].Text = "0";
        mTextBox[41].Text = "0";
        mTextBox[48].Text = "1";
        mTextBox[49].Text = "2";
        mTextBox[50].Text = "1";
        textBox1.Text = "1";
        numericUpDown1.Value = 3;
      }
      this.Refresh();
    }

    private void radioButton4_CheckedChanged(object sender, EventArgs e)
    {
      int i, j, pos;
      if (radioButton4.Checked == true)
      {
        for (i = 0; i < 9; i++)
        {
          for (j = 0; j < 9; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Text = "";
            mTextBox[pos].Enabled = false;
          }
        }
        for (i = 3; i < 6; i++)
        {
          for (j = 3; j < 6; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Enabled = true;
          }
        }
        mTextBox[30].Text = "-1";
        mTextBox[31].Text = "0";
        mTextBox[32].Text = "1";
        mTextBox[39].Text = "-2";
        mTextBox[40].Text = "0";
        mTextBox[41].Text = "2";
        mTextBox[48].Text = "-1";
        mTextBox[49].Text = "0";
        mTextBox[50].Text = "1";
        textBox1.Text = "1";
        numericUpDown1.Value = 3;
      }
      this.Refresh();
    }

    private void radioButton5_CheckedChanged(object sender, EventArgs e)
    {
      int i, j, pos;
      if (radioButton5.Checked == true)
      {
        for (i = 0; i < 9; i++)
        {
          for (j = 0; j < 9; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Text = "";
            mTextBox[pos].Enabled = false;
          }
        }
        for (i = 3; i < 6; i++)
        {
          for (j = 3; j < 6; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Enabled = true;
          }
        }
        mTextBox[30].Text = "0";
        mTextBox[31].Text = "-1";
        mTextBox[32].Text = "0";
        mTextBox[39].Text = "-1";
        mTextBox[40].Text = "4";
        mTextBox[41].Text = "-1";
        mTextBox[48].Text = "0";
        mTextBox[49].Text = "-1";
        mTextBox[50].Text = "0";
        textBox1.Text = "1";
        numericUpDown1.Value = 3;
      }
      this.Refresh();
    }

    private void radioButton6_CheckedChanged(object sender, EventArgs e)
    {
      int i, j, pos;
      if (radioButton6.Checked == true)
      {
        for (i = 0; i < 9; i++)
        {
          for (j = 0; j < 9; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Text = "";
            mTextBox[pos].Enabled = false;
          }
        }
        for (i = 3; i < 6; i++)
        {
          for (j = 3; j < 6; j++)
          {
            pos = i * 9 + j;
            mTextBox[pos].Enabled = true;
          }
        }
        mTextBox[30].Text = "0";
        mTextBox[31].Text = "-1";
        mTextBox[32].Text = "0";
        mTextBox[39].Text = "-1";
        mTextBox[40].Text = "5";
        mTextBox[41].Text = "-1";
        mTextBox[48].Text = "0";
        mTextBox[49].Text = "-1";
        mTextBox[50].Text = "0";
        textBox1.Text = "1";
        numericUpDown1.Value = 3;
      }
      this.Refresh();
    }

    /// <summary>
    /// NumericUpDown处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
      int i, j, pos;
      int order = (Convert.ToInt32(numericUpDown1.Value) / 2) * 2 + 1;
      numericUpDown1.Value = (decimal)order;

      for (i = 0; i < 9; i++)
      {
        for (j = 0; j < 9; j++)
        {
          pos = i * 9 + j;
          mTextBox[pos].Text = "";
          mTextBox[pos].Enabled = false;
        }
      }
      int startPos = 4 - order / 2;
      for (i = startPos; i < startPos + order; i++)
      {
        for (j = startPos; j < startPos + order; j++)
        {
          pos = i * 9 + j;
          mTextBox[pos].Enabled = true;
          mTextBox[pos].Text = "1";
        }
      }
      textBox1.Text = (order * order).ToString();
      radioButton7.Checked = true;
      this.Refresh();
    }

    /// <summary>
    /// 卷积处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    byte[] tempImageB;
    private void Convolution_Click(object sender, EventArgs e)
    {
      int order = Convert.ToInt32(numericUpDown1.Value);
      int denominator = getDenominator();
      long pos, conPos;
      int i, j;
      int a, b;
      tempImageB = new byte[pImage.MBData];
      //备份原始数据
      for (i = 0; i < pImage.MHeight; i++)
      {
        for (j = 0; j < pImage.MWidth; j++)
        {
          pos = i * pImage.MBWidth + j;
          tempImageB[pos] = pImage.ImageB[pos];
        }
      }

      //卷积处理，不考虑边界问题
      int conSum = 0;
      byte conResult;
      double tempResult = 0;
      byte[,] tempArr;
      mainFF.TspBar.Maximum = (int)(pImage.MWidth * pImage.MHeight);
      mainFF.TspBar.Minimum = 0;
      List<double> conSumList = new List<double>();
      for (i = order / 2; i < pImage.MHeight - order / 2; i++)
      {
        for (j = order / 2; j < pImage.MWidth - order / 2; j++)
        {
          pos = i * pImage.MBWidth + j;
          mainFF.TspBar.Value = (int)pos;
          switch (order)
          {
            case 1:
              {
                conSum = int.Parse(mTextBox[40].Text) * tempImageB[pos];
                break;
              }
            case 3:
              {
                conSum = 0;
                tempArr = new byte[3, 3];
                for (a = 0; a < 3; a++)
                {
                  for (b = 0; b < 3; b++)
                  {
                    pos = (i + a - 1) * pImage.MBWidth + j + b - 1;
                    conPos = (a + 3) * 9 + b + 3;
                    tempArr[a, b] = tempImageB[pos];
                    conSum = conSum + tempArr[a, b] * int.Parse(mTextBox[conPos].Text);
                  }
                }
                break;
              }
            case 5:
              {
                conSum = 0;
                tempArr = new byte[5, 5];
                for (a = 0; a < 5; a++)
                {
                  for (b = 0; b < 5; b++)
                  {
                    pos = (i + a - 2) * pImage.MBWidth + j + b - 2;
                    conPos = (a + 2) * 9 + b + 2;
                    tempArr[a, b] = tempImageB[pos];
                    conSum = conSum + tempArr[a, b] * int.Parse(mTextBox[conPos].Text);
                  }
                }
                break;
              }
            case 7:
              {
                conSum = 0;
                tempArr = new byte[7, 7];
                for (a = 0; a < 7; a++)
                {
                  for (b = 0; b < 7; b++)
                  {
                    pos = (i + a - 3) * pImage.MBWidth + j + b - 3;
                    conPos = (a + 1) * 9 + b + 1;
                    tempArr[a, b] = tempImageB[pos];
                    conSum = conSum + tempArr[a, b] * int.Parse(mTextBox[conPos].Text);
                  }
                }
                break;
              }
            case 9:
              {
                conSum = 0;
                tempArr = new byte[9, 9];
                for (a = 0; a < 9; a++)
                {
                  for (b = 0; b < 9; b++)
                  {
                    pos = (i + a - 4) * pImage.MBWidth + j + b - 4;
                    conPos = a * 9 + b;
                    tempArr[a, b] = tempImageB[pos];
                    conSum = conSum + tempArr[a, b] * int.Parse(mTextBox[conPos].Text);
                  }
                }
                break;
              }
            default:
              break;
          }
          conSumList.Add((double)conSum);
          try { tempResult = conSum / (int.Parse(textBox1.Text)); }
          catch { }
          //灰度超限处理方式1：
          if (checkBox1.Checked == false)
          {
            if (tempResult < 0)
              tempResult = 0;
            else if (tempResult > 255)
              tempResult = 255;
            conResult = (byte)tempResult;
            pos = i * pImage.MBWidth + j;
            pImage.ImageB[pos] = conResult;
          }
        }
      }
      //灰度超限处理方式2，线性插值：
      double Gmin = double.MaxValue, Gmax = 0;
      int tempIndex = 0;
      byte g = 0;
      if (checkBox1.Checked == true)
      {
        for (i = 0; i < conSumList.Count; i++)
        {
          if (Gmin > conSumList[i])
            Gmin = conSumList[i];
          if (Gmax < conSumList[i])
            Gmax = conSumList[i];
        }
        for (i = order / 2; i < pImage.MHeight - order / 2; i++)
        {
          for (j = order / 2; j < pImage.MWidth - order / 2; j++)
          {
            pos = (i + order / 2) * pImage.MBWidth + j + order / 2;
            g = (byte)(((conSumList[tempIndex] - Gmin) / (Gmax - Gmin)) * 255);
            pImage.ImageB[pos] = g;
            tempIndex++;
          }
        }
      }
      mainFF.TspBar.Value = 0;
      pImage.putBitMapData();
      mainFF.Refresh();
      try
      {
        mainFF.GTFrom.Refresh();
        mainFF.HistForm.Refresh();
      }
      catch (System.Exception ex) { }
    }


    /// <summary>
    /// 获取分母
    /// </summary>
    /// <returns></returns>
    private int getDenominator()
    {
      int denominator, sum = 0;
      int i, j, pos;
      for (i = 0; i < 9; i++)
      {
        for (j = 0; j < 9; j++)
        {
          pos = i * 9 + j;
          if (mTextBox[pos].Enabled == true)
          {
            sum = sum + int.Parse(mTextBox[pos].Text);
          }
        }
      }
      if (sum == 0)
        sum = 1;
      denominator = sum;
      textBox1.Text = denominator.ToString();
      this.Refresh();
      return denominator;
    }

    private void cancle_Click(object sender, EventArgs e)
    {
      long pos;
      if (tempImageB != null)
      {
        for (long i = 0; i < pImage.MHeight; i++)
        {
          for (long j = 0; j < pImage.MWidth; j++)
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