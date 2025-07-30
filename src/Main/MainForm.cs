// Copyright (c) 2025 Ming Hu. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace DipCSharp
{
  public partial class MainForm : Form
  {
    #region Global Variables
    //--------------------                         
    HistogramForm histForm = new HistogramForm();
    GrayTransForm gTransForm = new GrayTransForm();
    PaletteForm palForm = new PaletteForm();
    ConvolutionForm conForm = new ConvolutionForm();
    BinaryForm biForm = new BinaryForm();
    GradianForm graForm = new GradianForm();
    RotateForm rotForm = new RotateForm();
    ZoomForm zoForm = new ZoomForm();
    ColorChannels ccForm = new ColorChannels();
    imageClass myImage = new imageClass();
    string imageName;
    bool isZoomAll;
    bool isZoom;
    bool needZoomOut;
    Point startPoint;
    private Color RubberColor = new Color();
    Rectangle theRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
    double mx0, my0, mox, moy;
    double picMX0, picMY0;
    double cmX, cmY;

    bool isMove;
    Point offset;
    Point moveP;
    //-------------------------ToolStrip check status
    bool checkFlagTSB1 = false;
    bool checkFlagTSB3 = false;
    bool checkFlagTSB4 = false;
    bool isLeftClicked = false;
    #endregion

    #region MainForm and events
    public MainForm()
    {
      InitializeComponent();
      isZoomAll = false;
      needZoomOut = true;
      try { imageName = Application.StartupPath + "\\lena.bmp"; }
      catch (System.Exception ex) { }
      ;
      myImage.readImage(imageName);
      this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panel_MouseWheel);
    }

    private void panel_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
    {

    }

    private void panel_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        isLeftClicked = true;
      //--------------Shift + MouseDown: Zoom in
      if (e.Button == System.Windows.Forms.MouseButtons.Left && ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) || checkFlagTSB3 == true)
      {
        picMX0 = e.X;
        picMY0 = e.Y;
        panel.Cursor = Cursors.Cross;
        isZoom = true;
      }
      //--------------Middle + MouseDown: Move image
      if (e.Button == System.Windows.Forms.MouseButtons.Middle && checkFlagTSB3 == false || checkFlagTSB4 == true)
      {
        picMY0 = e.X;
        picMY0 = e.Y;
        panel.Cursor = Cursors.SizeAll;
        moveP = e.Location;
        isMove = true;
      }

      Control sControl = (Control)sender as Control;
      startPoint = sControl.PointToScreen(new Point(e.X, e.Y));
    }

    private void panel_MouseUp(object sender, MouseEventArgs e)
    {
      //------------MouseRight: Zoom out
      if (e.Button == System.Windows.Forms.MouseButtons.Right)
      {
        myImage.ZoomInOut(1, myImage.MapToImageX(e.X), myImage.MapToImageY(e.Y));
        panel.Refresh();
      }
      //------------MouseLeft: Zoom in
      else if (e.Button == System.Windows.Forms.MouseButtons.Left && isZoom || checkFlagTSB3 == true)
      {
        ControlPaint.DrawReversibleFrame(theRectangle, RubberColor, FrameStyle.Thick);
        theRectangle = new Rectangle(0, 0, 0, 0);

        mx0 = picMX0;
        my0 = picMY0;
        mox = e.X;
        moy = e.Y;

        double xx0 = IIF(mx0 > mox, mox, mx0);
        double yy0 = IIF(my0 > moy, moy, my0);
        double xx1 = IIF(mx0 > mox, mx0, mox);
        double yy1 = IIF(my0 > moy, my0, moy);

        if (Math.Abs(xx1 - xx0) > 5.0 || Math.Abs(yy1 - yy0) > 5.0)
        {
          myImage.xWinMin = myImage.MapToImageX(xx0);
          myImage.yWinMin = myImage.MapToImageY(yy0);
          myImage.xWinMax = myImage.MapToImageX(xx1);
          myImage.yWinMax = myImage.MapToImageY(yy1);
          panel.Refresh();
        }
        else
        {
          myImage.ZoomInOut(0, myImage.MapToImageX(e.X), myImage.MapToImageY(e.Y));
          panel.Refresh();
        }
        if (checkFlagTSB3 == false)
          panel.Cursor = Cursors.Default;
      }
      else if (e.Button == MouseButtons.Middle && isMove)
      {
        if (checkFlagTSB4 == false)
          panel.Cursor = Cursors.Default;
      }
      isZoom = false;
      isMove = false;
      isLeftClicked = false;
    }

    private void panel_MouseMove(object sender, MouseEventArgs e)
    {
    }

    private void panel_Paint(object sender, PaintEventArgs e)
    {
      myImage.getXView = panel.Width;// -panel.Margin.Right;
      myImage.getYView = panel.Height;// -panel.Margin.Bottom;
      e.Graphics.Clear(Color.White);
      if (isZoomAll)
        myImage.zoomExtent(e.Graphics);
      else
      {
        myImage.zoomImage(e.Graphics, IIF(needZoomOut, 0, 1));
        needZoomOut = true;
      }
      this.statusStrip.Refresh();

      string imageType = "";
      if (myImage.MyImageType == 0)
      {
        imageType = "8Bits";
      }
      if (myImage.MyImageType == 1)
      {
        imageType = "24Bits";
      }
      gTransForm.onMainRefresh += new GrayTransForm.mainRefresh(dataRefresh);
      palForm.onMainRefresh2 += new PaletteForm.mainRefreshPal(dataRefresh2);
      this.toolStripStatusLabel2.Text = myImage.MWidth.ToString() + "(w) x" + myImage.MHeight.ToString() + "(H) x" + " " + imageType;
    }

    private int IIF(bool a, int b, int c)   //VB中的一种判断语句
    {
      if (a == true)
        return b;
      else
        return c;
    }
    private double IIF(bool a, double b, double c)
    {
      if (a == true)
        return b;
      else
        return c;
    }
    private void MainForm_Load(object sender, EventArgs e)
    {
      isZoomAll = false;
      isZoom = false;
      RubberColor = Color.Cyan;
      needZoomOut = true;
      isMove = false;
      offset = new Point((int)myImage.xWinMin, (int)myImage.yWinMin);
    }
    private void MainForm_Resize(object sender, EventArgs e)
    {
      panel.Refresh();
    }
    private void panel_DoubleClick(object sender, EventArgs e)
    {
      isZoomAll = true;
      if (myImage.isAvailable() == true)
        panel.Refresh();
      isZoomAll = false;
    }
    #endregion

    #region Functions to operate files
    private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      openImage();
      try
      {
        gTransForm.onMainRefresh += new GrayTransForm.mainRefresh(dataRefresh);
        palForm.onMainRefresh2 += new PaletteForm.mainRefreshPal(dataRefresh2);
      }
      catch (System.Exception ex) { }
    }

    private void openImage()
    {
      if (myImage == null)
      {
        myImage = new imageClass();
        myImage.readImage(Application.StartupPath + "\\lena.bmp");
      }
      else
      {
        openFileDialog1.Filter = "Bitmap Image|*.bmp";
        openFileDialog1.FilterIndex = 0;
        openFileDialog1.Title = "Choose a bit map";
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
          imageName = openFileDialog1.FileName;
          myImage.readImage(imageName);
        }
        if (openFileDialog1.FileName == "") return;
      }
      panel.Refresh();
      if (histForm != null)
      {
        myImage.HistCalculation();
        histForm.getIndex = this.myImage;
        histForm.Refresh();
      }
      if (gTransForm != null)
      {
        myImage.HistCalculation();
        gTransForm.getIndex = this.myImage;
        gTransForm.Refresh();
      }

    }

    private void openLenaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage = null;
      }
      openImage();
      try
      {
        gTransForm.onMainRefresh += new GrayTransForm.mainRefresh(dataRefresh);
        palForm.onMainRefresh2 += new PaletteForm.mainRefreshPal(dataRefresh2);
      }
      catch (System.Exception ex)
      {

      }
    }

    private void reOpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.readImage(imageName);
      panel.Refresh();
      if (histForm != null || histForm.IsDisposed == false)
      {
        myImage.HistCalculation();
        histForm.getIndex = this.myImage;
        histForm.Refresh();
      }

      if (gTransForm != null || gTransForm.IsDisposed == false)
      {
        myImage.HistCalculation();
        gTransForm.getIndex = myImage;
        gTransForm.Refresh();
      }
      try
      {
        gTransForm.onMainRefresh += new GrayTransForm.mainRefresh(dataRefresh);
        palForm.onMainRefresh2 += new PaletteForm.mainRefreshPal(dataRefresh2);

      }
      catch { }
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      saveFileDialog1.Filter = "Bitmap Image|*.bmp";
      saveFileDialog1.FilterIndex = 0;
      saveFileDialog1.Title = "Save as bit map";
      saveFileDialog1.FileName = "bit map";
      string sTrack = Application.StartupPath;
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        if (myImage.BMap != null)
        {
          Bitmap biMap;
          biMap = myImage.BMap;
          sTrack = saveFileDialog1.FileName;
          biMap.Save(sTrack);
        }
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }
    #endregion

    #region Image Processing Part I
    //---negative image
    private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.negative();
      panel.Refresh();
      if (histForm != null || histForm.IsDisposed == false)
      {
        myImage.HistCalculation();
        histForm.getIndex = myImage;
        histForm.Refresh();
      }
      if (gTransForm != null || gTransForm.IsDisposed == false)
      {
        myImage.HistCalculation();
        gTransForm.getIndex = myImage;
        gTransForm.Refresh();
      }

    }
    //---grey histogram
    private void histgramToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.HistCalculation();
      histForm.getIndex = this.myImage;   //processing multi buttons
      if (histForm == null || histForm.IsDisposed)
      {
        histForm = new HistogramForm();
        histForm.getIndex = this.myImage;
        histForm.Show(this);
      }
      else
      {
        //histForm.WindowState = FormWindowState.Normal;
        histForm.Visible = false;
        histForm.Show(this);
      }
      //Make sure histForm is always above MainForm
    }
    //---histogram equalization
    private void histgramEqualizationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.HistCalculation();
      myImage.HistEqualize(ref myImage);
      panel.Refresh();

      myImage.HistCalculation();
      histForm.getIndex = myImage;
      if (histForm != null & histForm.Visible == true) { histForm.Refresh(); }
      gTransForm.getIndex = myImage;
      if (gTransForm != null & gTransForm.Visible == true)
      {
        gTransForm.Refresh();
      }
    }
    //---gray transform
    private void grayTransformToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        myImage.HistCalculation();
        gTransForm.getIndex = myImage;
        if (gTransForm.IsDisposed || gTransForm == null)
        {
          gTransForm = new GrayTransForm();
          gTransForm.getIndex = myImage;
          gTransForm.Show(this);
        }
        else
        {
          //gTransForm.WindowState = FormWindowState.Normal;
          gTransForm.Visible = false;
          gTransForm.Show(this);
        }
      }
      else
      {
        MessageBox.Show("Please Open 8bits Gray Image!");
      }
    }
    #endregion

    #region Palette Processing
    private void editPaletteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        try
        {
          palForm = new PaletteForm();
          palForm.getIndex = myImage;
          palForm.Show(this);

          palForm.mainF = this;
        }
        catch (System.Exception ex) { }
      }
      else
      {
        MessageBox.Show("No Palette!!!");
        return;
      }

    }

    private void redPaletteToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage.redPalette();
        panel.Refresh();
      }
    }

    private void greenPaletteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage.greenPalette();
        panel.Refresh();
      }
    }

    private void bluePaletteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage.bluePalette();
        panel.Refresh();
      }
    }

    private void yellowPaletteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage.yellowPalette();
        panel.Refresh();
      }
    }

    private void cyanPaletteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage.cyanPalette();
        panel.Refresh();
      }
    }

    private void purplePaletteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage.purplePalette();
        panel.Refresh();
      }
    }

    private void specifiedColorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage.specifiedPalette();
        panel.Refresh();
      }
    }

    private void paletteNegativeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage != null)
      {
        myImage.negativePalette();
        panel.Refresh();
      }
    }

    private void indexColorPaletteToGrayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        myImage.IndexColorPalToGray();
        this.Refresh();
      }
      else MessageBox.Show("Not an index image！");
    }

    private void indexImageToGrayImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        myImage.IndexImageToGrayImage();
        this.Refresh();
      }
      else MessageBox.Show("Not an index image！");
    }

    private void colorToGrayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 1)
      {
        myImage.ColorToGray();
        this.Refresh();
      }
      else
        MessageBox.Show("Not a colorful image！");
    }

    private void colorImageChangeChannelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 1)
      {
        myImage.ColorToGray();
        this.Refresh();
      }
      else
        MessageBox.Show("Not a colorful image！");
    }

    private void 彩色通道分解ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 1)
      {
        if (ccForm.IsDisposed || ccForm == null)
        {
          ccForm = new ColorChannels();
          ccForm.GetIndex = myImage;
          ccForm.Show(this);
        }
        else
        {
          ccForm.GetIndex = myImage;
          ccForm.Visible = false;
          ccForm.Show(this);
        }
      }
      else
      {
        MessageBox.Show("Please open a color image！");
      }
    }
    #endregion

    #region Image Procession Part II: Filtering
    //---SaltPepperNoise
    private void addSaltPepperNoiseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.addSaltPepperNoise();
      panel.Refresh();
      histForm.Refresh();
      gTransForm.Refresh();

    }
    //---Middle Value Filter
    private void mToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.MediumFilter();
      panel.Refresh();
      histForm.Refresh();
      gTransForm.Refresh();
    }
    //---Mean Value Filter
    private void AverageFilterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.AverageFilter();
      panel.Refresh();
      histForm.Refresh();
      gTransForm.Refresh();
    }
    //---Edge-saved Middle Value Filter
    private void EdgeMediumFilterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.EdgeMediumFilter();
      panel.Refresh();
      histForm.Refresh();
      gTransForm.Refresh();
    }
    //---Edge-saved Mean Value Filter
    private void EdgeAverageFilterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.EdgeAverageFilter();
      panel.Refresh();
      histForm.Refresh();
      gTransForm.Refresh();
    }
    //---Convolution Filter
    private void ConvolutionFilterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        if (conForm.IsDisposed || conForm == null)
        {
          conForm = new ConvolutionForm();
          conForm.GetIndex = myImage;
          conForm.Show(this);
          conForm.MainFF = this;
        }
        else
        {
          conForm.GetIndex = myImage;
          conForm.Visible = false;
          conForm.Show(this);
          conForm.MainFF = this;
        }
      }
      else
      {
        MessageBox.Show("Sorry, image type not supported！");
      }
    }
    //---gradian filter
    private void gradianToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        if (graForm.IsDisposed || graForm == null)
        {
          graForm = new GradianForm();
          graForm.GetIndex = myImage;
          graForm.Show(this);
          graForm.MainFF = this;
        }
        else
        {
          graForm.GetIndex = myImage;
          graForm.Visible = false;
          graForm.Show(this);
          graForm.MainFF = this;
        }
      }
      else
      {
        MessageBox.Show("Sorry, image type not supported！");
      }
    }
    //---Threshold Procession
    private void binarizationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        if (biForm.IsDisposed || biForm == null)
        {
          biForm = new BinaryForm();
          biForm.GetIndex = myImage;
          biForm.Show(this);
          biForm.MainFF = this;
        }
        else
        {
          biForm.GetIndex = myImage;
          biForm.Visible = false;
          biForm.Show(this);
          biForm.MainFF = this;
        }
      }
      else
      {
        MessageBox.Show("Sorry, image type not supported！");
      }
    }
    #endregion

    #region Geometry Transform
    //---X Mirror
    private void mirrorXaxisToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.MirrorX();
      panel.Refresh();
    }
    //---Y Mirror
    private void mirrorYaxisToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.MirrorY();
      panel.Refresh();
    }
    //---O Mirror
    private void mirrorOriginToolStripMenuItem_Click(object sender, EventArgs e)
    {
      myImage.MirrorOrigin();
      panel.Refresh();
    }
    //---Rotate
    private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        if (rotForm.IsDisposed || rotForm == null)
        {
          rotForm = new RotateForm();
          rotForm.GetIndex = myImage;
          rotForm.Show(this);
          rotForm.MainFF = this;
          rotForm.TspBar = this.toolStripProgressBar1;
        }
        else
        {
          rotForm.GetIndex = myImage;
          rotForm.Visible = false;
          rotForm.Show(this);
          rotForm.MainFF = this;
          rotForm.TspBar = this.toolStripProgressBar1;
        }
      }
      else
      {
        MessageBox.Show("Sorry, image type not supported！");
      }
    }
    //---Zoom
    private void zoomInOutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (myImage.MyImageType == 0)
      {
        if (zoForm.IsDisposed || zoForm == null)
        {
          zoForm = new ZoomForm();
          zoForm.GetIndex = myImage;
          zoForm.Show(this);
          zoForm.MainFF = this;
          zoForm.TspBar = this.toolStripProgressBar1;
        }
        else
        {
          zoForm.GetIndex = myImage;
          zoForm.Visible = false;
          zoForm.Show(this);
          zoForm.MainFF = this;
          zoForm.TspBar = this.toolStripProgressBar1;
        }
      }
      else
      {
        MessageBox.Show("Sorry, image type not supported！");
      }
    }
    #endregion

    #region Interface Attribute
    public ToolStripProgressBar TspBar
    {
      get { return toolStripProgressBar1; }
      set { toolStripProgressBar1 = value; }
    }

    public HistogramForm HistForm
    {
      get { return histForm; }
      set { histForm = value; }
    }

    public GrayTransForm GTFrom
    {
      get { return gTransForm; }
      set { gTransForm = value; }
    }
    public imageClass MyImage
    {
      get { return myImage; }
      set { myImage = value; }
    }

    private void dataRefresh(object sender, EventArgs e)
    {
      panel.Refresh();
    }
    private void dataRefresh2(object sender, EventArgs e)
    {
      panel.Refresh();
    }
    #endregion

    #region Tool Strip
    //---Refresh
    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      myImage.readImage(imageName);
      panel.Refresh();
      if (histForm != null || histForm.IsDisposed == false)
      {
        myImage.HistCalculation();
        histForm.getIndex = this.myImage;
        histForm.Refresh();
      }

      if (gTransForm != null || gTransForm.IsDisposed == false)
      {
        myImage.HistCalculation();
        gTransForm.getIndex = myImage;
        gTransForm.Refresh();
      }
      try
      {
        gTransForm.onMainRefresh += new GrayTransForm.mainRefresh(dataRefresh);
        palForm.onMainRefresh2 += new PaletteForm.mainRefreshPal(dataRefresh2);
      }
      catch { }
    }
    //---default
    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      if (checkFlagTSB1 == false)
      {
        toolStripButton1.CheckState = CheckState.Indeterminate;
        toolStripButton3.CheckState = CheckState.Unchecked;
        toolStripButton4.CheckState = CheckState.Unchecked;
        checkFlagTSB1 = true;
        checkFlagTSB3 = false;
        checkFlagTSB4 = false;
      }
      else
      {
        toolStripButton1.CheckState = CheckState.Unchecked;
        checkFlagTSB1 = true;
        panel.Cursor = Cursors.Default;
      }

    }
    //---Zoom in
    private void toolStripButton3_Click(object sender, EventArgs e)
    {
      if (checkFlagTSB3 == false)
      {
        toolStripButton3.CheckState = CheckState.Indeterminate;
        toolStripButton1.CheckState = CheckState.Unchecked;
        toolStripButton4.CheckState = CheckState.Unchecked;
        checkFlagTSB3 = true;
        checkFlagTSB1 = false;
        checkFlagTSB4 = false;
      }
      else
      {
        toolStripButton3.CheckState = CheckState.Unchecked;
        checkFlagTSB3 = false;
        panel.Cursor = Cursors.Default;
      }
    }
    //---Move
    private void toolStripButton4_Click(object sender, EventArgs e)
    {
      if (checkFlagTSB4 == false)
      {
        toolStripButton4.CheckState = CheckState.Indeterminate;
        toolStripButton1.CheckState = CheckState.Unchecked;
        toolStripButton3.CheckState = CheckState.Unchecked;
        checkFlagTSB1 = false;
        checkFlagTSB3 = false;
        checkFlagTSB4 = true;
      }

      else
      {
        toolStripButton4.CheckState = CheckState.Unchecked;
        checkFlagTSB4 = false;
        panel.Cursor = Cursors.Default;
      }
    }
    #endregion
  }
}