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
  public partial class HistogramForm : Form
  {
    private imageClass pImage;
    int HistStyle;

    public HistogramForm()
    {
      InitializeComponent();
      HistStyle = 0;
    }

    public imageClass getIndex  //imageClass必须是一个public 类
    {
      get
      {
        return pImage;
      }
      set
      {
        pImage = value;
      }
    }
    private void HistPanel_Click(object sender, EventArgs e)
    {
      if (HistStyle == 0)
        HistStyle = 1;
      else
        HistStyle = 0;
      HistPanel.Refresh();
    }


    private void HistPanel_Paint(object sender, PaintEventArgs e)
    {
      if (pImage != null)
      {
        this.Text = "灰度直方图";
        pImage.HistDraw(e.Graphics);
        toolStripStatusLabel1.Text = "熵=" + pImage.EntropyH().ToString("0.00") + "  " + "标准差=" + pImage.Sigma().ToString("0.00") + "  "
            + "最小值=" + pImage.GrayMin().ToString("0") + "  " + "最大值=" + pImage.GrayMax().ToString("0") + "  " + "平均值=" + pImage.GrayAverage().ToString("0.00");

      }

    }

    private void HistogramForm_Resize(object sender, EventArgs e)
    {
      HistPanel.Refresh();
    }






  }
}