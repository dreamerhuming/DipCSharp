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
using System.Drawing.Imaging;

namespace DipCSharp
{
    public partial class PaletteForm : Form
    {
        imageClass pImage;
        MainForm mainFF;    //主窗体指针，用于刷新事件

        System.Windows.Forms.PictureBox[] palettePanel = new System.Windows.Forms.PictureBox[256];
        Rectangle[] entryRect = new Rectangle[256];
        Point[] leftUpperPos = new Point[256];
        ColorPalette tempPalette;
        ColorPalette imPalette;
        public PaletteForm()
        {
            InitializeComponent();
            
        }
        public imageClass getIndex
        {
            get { return pImage; }
            set { pImage = value; }
        }

        public MainForm mainF
        {
            get { return mainFF; }
            set { mainFF = value; }
        }

        //建立委托
        public delegate void mainRefreshPal(object sender, EventArgs e);
        public event mainRefreshPal onMainRefresh2;

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            pImage.setPalette(imPalette);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pImage.setPalette(imPalette);
            mainFF.panel.Refresh();
            this.Refresh();
        }
        private void PaletteForm_Load(object sender, EventArgs e)
        {
            if (pImage == null) { return; }
            imPalette = pImage.getPalette();

            int i, j, pos;

            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 16; j++)
                {
                    leftUpperPos[i * 16 + j].X = 10 + (25 + 2) * i;
                    leftUpperPos[i * 16 + j].Y = 10 + (25 + 2) * j;
                }
            }
        }
        
        private void PaletteForm_MouseClick(object sender, MouseEventArgs e)
        {
            int i, j, mouseX, mouseY;
            tempPalette = pImage.getPalette();
            if (MousePosition.X > 10 & MousePosition.Y > 10)
            {
                mouseX = (e.X - 10) / 27;
                mouseY = (e.Y - 10) / 27;
                ColorDialog paletteDialog = new ColorDialog();
                paletteDialog.FullOpen=true;
                Color newColor = new Color();
                DialogResult result = paletteDialog.ShowDialog(this);
                if (result == DialogResult.OK) 
                {
                    try
                    {
                        newColor = paletteDialog.Color;
                        tempPalette.Entries[16 * mouseY + mouseX] = newColor;
                    }
                    catch (System.Exception ex)
                    {

                    }
                    pImage.setPalette(tempPalette);
                    this.Refresh();
                }
                
            }
        }

        private void PaletteForm_Paint(object sender, PaintEventArgs e)
        {
            int i, j, posPalette;
            Graphics g = e.Graphics;
            tempPalette = pImage.getPalette();
            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 16; j++)
                {
                    posPalette = i * 16 + j;
                    leftUpperPos[posPalette].X = 10 + (25 + 2) * i;
                    leftUpperPos[posPalette].Y = 10 + (25 + 2) * j;
                    try
                    {
                        g.FillRectangle(new SolidBrush(tempPalette.Entries[posPalette]), leftUpperPos[16 * j + i].X, leftUpperPos[16 * j + i].Y, 25, 25);
                    }
                    catch (System.Exception ex)
                    {
                        g.FillRectangle(new SolidBrush(tempPalette.Entries[posPalette-1]), leftUpperPos[16 * j + i].X, leftUpperPos[16 * j + i].Y, 25, 25);
                    }
                }
            }
            Pen penRectMargin=new Pen(Color.Red);
            for (i = 0; i < 256; i++)
            {
                g.DrawRectangle(penRectMargin, leftUpperPos[i].X, leftUpperPos[i].Y, 25, 25);
            }

        }
        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                pImage.setPalette(tempPalette);
                //onMainRefresh2(this, new EventArgs()); 
                mainFF.panel.Refresh();

            }
            if (checkBox1.Checked == false)
            {
                pImage.setPalette(imPalette);
                //onMainRefresh2(this, new EventArgs()); 
                mainFF.panel.Refresh();
            }
        }
        
        public void applyButton_Click(object sender, EventArgs e)
        {
            
            pImage.setPalette(tempPalette);
            mainFF.panel.Refresh();
            //onMainRefresh2(this, new EventArgs());
        }

        private void PaletteForm_MouseMove(object sender, MouseEventArgs e)
        {
            int mouseX, mouseY;
            tempPalette = pImage.getPalette();
            try
            {
                if (MousePosition.X > 10 & MousePosition.Y > 10)
                {
                    mouseX = (e.X - 10) / 27;
                    mouseY = (e.Y - 10) / 27;
                    label1.Text = "No." + (16 * mouseY + mouseX) + "(" + tempPalette.Entries[16 * mouseY + mouseX].R + "," + tempPalette.Entries[16 * mouseY + mouseX].G + "," + tempPalette.Entries[16 * mouseY + mouseX].B + ")";
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }

        
        


        
        
    }
}

