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
    public partial class ColorChannels : Form
    {
        imageClass pImage = new imageClass();
        byte[] tempImageC;
        long i, j, pos;
        public ColorChannels()
        {
            InitializeComponent();
        }
        public imageClass GetIndex
        {
            get { return pImage; }
            set { pImage = value; }
        }

        private void pictureRGB_Paint(object sender, PaintEventArgs e)
        {
            pImage.getXView = pictureRGB.Width;
            pImage.getYView = pictureRGB.Height;
            for (i = 0; i < pImage.MHeight; i++)
            {
                for (j = 0; j < pImage.MWidth; j++)
                {
                    pos = i * pImage.MCWidth + 3 * j;
                    pImage.ImageC[pos] = tempImageC[pos];
                    pImage.ImageC[pos + 1] = tempImageC[pos + 1];
                    pImage.ImageC[pos + 2] = tempImageC[pos + 2];
                }
            }
            pImage.ImageC = tempImageC;
            pImage.putBitMapData();
            e.Graphics.Clear(Color.White);
            pImage.zoomImage(e.Graphics);
            
        }

        private void ColorChannels_Load(object sender, EventArgs e)
        {
            tempImageC = new byte[pImage.MCWidth * pImage.MHeight];
            for (i = 0; i < pImage.MHeight; i++)
            {
                for (j = 0; j < pImage.MWidth; j++)
                {
                    pos = i * pImage.MCWidth + 3 * j;
                    tempImageC[pos] = pImage.ImageC[pos];
                    tempImageC[pos+1] = pImage.ImageC[pos+1];
                    tempImageC[pos+2] = pImage.ImageC[pos+2];
                }
            }
        }

        private void pictureR_Paint(object sender, PaintEventArgs e)
        {
            for (i = 0; i < pImage.MHeight; i++)
            {
                for (j = 0; j < pImage.MWidth; j++)
                {
                    pos = i * pImage.MCWidth + 3 * j;
                    pImage.ImageC[pos] = pImage.ImageC[pos + 1] = 0;
                    pImage.ImageC[pos + 2] = tempImageC[pos + 2];
                }
            }
            pImage.putBitMapData();
            pImage.getXView = pictureRGB.Width;
            pImage.getYView = pictureRGB.Height;
            e.Graphics.Clear(Color.White);
            pImage.zoomImage(e.Graphics);
        }

        private void pictureR_Click(object sender, EventArgs e)
        {
        }

        private void pictureG_Paint(object sender, PaintEventArgs e)
        {
            for (i = 0; i < pImage.MHeight; i++)
            {
                for (j = 0; j < pImage.MWidth; j++)
                {
                    pos = i * pImage.MCWidth + 3 * j;
                    pImage.ImageC[pos] = pImage.ImageC[pos + 2] = 0;
                    pImage.ImageC[pos + 1] = tempImageC[pos + 1];
                }
            }
            pImage.putBitMapData();
            pImage.getXView = pictureRGB.Width;
            pImage.getYView = pictureRGB.Height;
            e.Graphics.Clear(Color.White);
            pImage.zoomImage(e.Graphics);
        }

        private void pictureB_Paint(object sender, PaintEventArgs e)
        {
            for (i = 0; i < pImage.MHeight; i++)
            {
                for (j = 0; j < pImage.MWidth; j++)
                {
                    pos = i * pImage.MCWidth + 3 * j;
                    pImage.ImageC[pos+1] = pImage.ImageC[pos + 2] = 0;
                    pImage.ImageC[pos] = tempImageC[pos];
                }
            }
            pImage.putBitMapData();
            pImage.getXView = pictureRGB.Width;
            pImage.getYView = pictureRGB.Height;
            e.Graphics.Clear(Color.White);
            pImage.zoomImage(e.Graphics);
        }

    }
}

