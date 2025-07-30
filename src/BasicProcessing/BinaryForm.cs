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
    public partial class BinaryForm : Form
    {
        imageClass pImage;
        MainForm mainFF;
        public BinaryForm()
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
        byte threshold;
        byte[] tempImageB;
        private void button1_Click(object sender, EventArgs e)
        {
            threshold = (byte)numericUpDown1.Value;
            long i, j, pos;
            tempImageB = new byte[pImage.MBData];
            for (i = 0; i < pImage.MHeight; i++)
            {
                for (j = 0; j < pImage.MWidth; j++)
                {
                    pos = i * pImage.MBWidth + j;
                    tempImageB[pos] = pImage.ImageB[pos];
                    if (pImage.ImageB[pos] >= threshold)
                        pImage.ImageB[pos] = 255;
                    else if (pImage.ImageB[pos] < threshold)
                        pImage.ImageB[pos] = 0;
                }
            }
            pImage.putBitMapData();
            mainFF.Refresh();
            try 
            {
                mainFF.HistForm.Refresh();
                mainFF.GTFrom.Refresh();
            }
            catch { }
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
