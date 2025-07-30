// Copyright (c) 2025 Ming Hu. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace DipCSharp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLenaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histgramEqualizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayTransformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.addSaltPepperNoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AverageFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EdgeMediumFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EdgeAverageFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConvolutionFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.binarizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geoTransformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorXaxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorYaxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorOriginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteNegativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specifiedColorPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specifiedColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.redPaletteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.greedPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bluePaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cyanPaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purplePaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.indexImageToGrayImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexColorPaletteToGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.彩色通道分解ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.panel = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip
            // 
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.processingToolStripMenuItem,
            this.geoTransformationToolStripMenuItem,
            this.colorPaletteToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1434, 43);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.openLenaToolStripMenuItem,
            this.reOpenToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(82, 35);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(249, 44);
            this.openImageToolStripMenuItem.Text = "打开图像";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // openLenaToolStripMenuItem
            // 
            this.openLenaToolStripMenuItem.Name = "openLenaToolStripMenuItem";
            this.openLenaToolStripMenuItem.Size = new System.Drawing.Size(249, 44);
            this.openLenaToolStripMenuItem.Text = "打开Lena";
            this.openLenaToolStripMenuItem.Click += new System.EventHandler(this.openLenaToolStripMenuItem_Click);
            // 
            // reOpenToolStripMenuItem
            // 
            this.reOpenToolStripMenuItem.Name = "reOpenToolStripMenuItem";
            this.reOpenToolStripMenuItem.Size = new System.Drawing.Size(249, 44);
            this.reOpenToolStripMenuItem.Text = "重新打开";
            this.reOpenToolStripMenuItem.Click += new System.EventHandler(this.reOpenToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(249, 44);
            this.saveAsToolStripMenuItem.Text = "另存为";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(246, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(249, 44);
            this.exitToolStripMenuItem.Text = "退出";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // processingToolStripMenuItem
            // 
            this.processingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.negativeToolStripMenuItem,
            this.histgramToolStripMenuItem,
            this.histgramEqualizationToolStripMenuItem,
            this.grayTransformToolStripMenuItem,
            this.toolStripSeparator4,
            this.addSaltPepperNoiseToolStripMenuItem,
            this.mToolStripMenuItem,
            this.AverageFilterToolStripMenuItem,
            this.EdgeMediumFilterToolStripMenuItem,
            this.EdgeAverageFilterToolStripMenuItem,
            this.toolStripSeparator5,
            this.ConvolutionFilterToolStripMenuItem,
            this.gradianToolStripMenuItem,
            this.binarizationToolStripMenuItem});
            this.processingToolStripMenuItem.Name = "processingToolStripMenuItem";
            this.processingToolStripMenuItem.Size = new System.Drawing.Size(130, 35);
            this.processingToolStripMenuItem.Text = "图像处理";
            // 
            // negativeToolStripMenuItem
            // 
            this.negativeToolStripMenuItem.Name = "negativeToolStripMenuItem";
            this.negativeToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.negativeToolStripMenuItem.Text = "负片";
            this.negativeToolStripMenuItem.Click += new System.EventHandler(this.negativeToolStripMenuItem_Click);
            // 
            // histgramToolStripMenuItem
            // 
            this.histgramToolStripMenuItem.Name = "histgramToolStripMenuItem";
            this.histgramToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.histgramToolStripMenuItem.Text = "灰度直方图";
            this.histgramToolStripMenuItem.Click += new System.EventHandler(this.histgramToolStripMenuItem_Click);
            // 
            // histgramEqualizationToolStripMenuItem
            // 
            this.histgramEqualizationToolStripMenuItem.Name = "histgramEqualizationToolStripMenuItem";
            this.histgramEqualizationToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.histgramEqualizationToolStripMenuItem.Text = "直方图均衡化";
            this.histgramEqualizationToolStripMenuItem.Click += new System.EventHandler(this.histgramEqualizationToolStripMenuItem_Click);
            // 
            // grayTransformToolStripMenuItem
            // 
            this.grayTransformToolStripMenuItem.Name = "grayTransformToolStripMenuItem";
            this.grayTransformToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.grayTransformToolStripMenuItem.Text = "灰度变换";
            this.grayTransformToolStripMenuItem.Click += new System.EventHandler(this.grayTransformToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(359, 6);
            // 
            // addSaltPepperNoiseToolStripMenuItem
            // 
            this.addSaltPepperNoiseToolStripMenuItem.Name = "addSaltPepperNoiseToolStripMenuItem";
            this.addSaltPepperNoiseToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.addSaltPepperNoiseToolStripMenuItem.Text = "添加椒盐噪声";
            this.addSaltPepperNoiseToolStripMenuItem.Click += new System.EventHandler(this.addSaltPepperNoiseToolStripMenuItem_Click);
            // 
            // mToolStripMenuItem
            // 
            this.mToolStripMenuItem.Name = "mToolStripMenuItem";
            this.mToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.mToolStripMenuItem.Text = "中值滤波 (1X5)";
            this.mToolStripMenuItem.Click += new System.EventHandler(this.mToolStripMenuItem_Click);
            // 
            // AverageFilterToolStripMenuItem
            // 
            this.AverageFilterToolStripMenuItem.Name = "AverageFilterToolStripMenuItem";
            this.AverageFilterToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.AverageFilterToolStripMenuItem.Text = "均值滤波 (1X5)";
            this.AverageFilterToolStripMenuItem.Click += new System.EventHandler(this.AverageFilterToolStripMenuItem_Click);
            // 
            // EdgeMediumFilterToolStripMenuItem
            // 
            this.EdgeMediumFilterToolStripMenuItem.Name = "EdgeMediumFilterToolStripMenuItem";
            this.EdgeMediumFilterToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.EdgeMediumFilterToolStripMenuItem.Text = "中值滤波 (保持边界)";
            this.EdgeMediumFilterToolStripMenuItem.Click += new System.EventHandler(this.EdgeMediumFilterToolStripMenuItem_Click);
            // 
            // EdgeAverageFilterToolStripMenuItem
            // 
            this.EdgeAverageFilterToolStripMenuItem.Name = "EdgeAverageFilterToolStripMenuItem";
            this.EdgeAverageFilterToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.EdgeAverageFilterToolStripMenuItem.Text = "均值滤波 (保持边界)";
            this.EdgeAverageFilterToolStripMenuItem.Click += new System.EventHandler(this.EdgeAverageFilterToolStripMenuItem_Click);
            // 
            // ConvolutionFilterToolStripMenuItem
            // 
            this.ConvolutionFilterToolStripMenuItem.Name = "ConvolutionFilterToolStripMenuItem";
            this.ConvolutionFilterToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.ConvolutionFilterToolStripMenuItem.Text = "卷积过滤";
            this.ConvolutionFilterToolStripMenuItem.Click += new System.EventHandler(this.ConvolutionFilterToolStripMenuItem_Click);
            // 
            // gradianToolStripMenuItem
            // 
            this.gradianToolStripMenuItem.Name = "gradianToolStripMenuItem";
            this.gradianToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.gradianToolStripMenuItem.Text = "梯度滤波";
            this.gradianToolStripMenuItem.Click += new System.EventHandler(this.gradianToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(359, 6);
            // 
            // binarizationToolStripMenuItem
            // 
            this.binarizationToolStripMenuItem.Name = "binarizationToolStripMenuItem";
            this.binarizationToolStripMenuItem.Size = new System.Drawing.Size(362, 44);
            this.binarizationToolStripMenuItem.Text = "阈值处理";
            this.binarizationToolStripMenuItem.Click += new System.EventHandler(this.binarizationToolStripMenuItem_Click);
            // 
            // geoTransformationToolStripMenuItem
            // 
            this.geoTransformationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mirrorXaxisToolStripMenuItem,
            this.mirrorYaxisToolStripMenuItem,
            this.mirrorOriginToolStripMenuItem,
            this.rotateToolStripMenuItem,
            this.zoomInOutToolStripMenuItem});
            this.geoTransformationToolStripMenuItem.Name = "geoTransformationToolStripMenuItem";
            this.geoTransformationToolStripMenuItem.Size = new System.Drawing.Size(130, 35);
            this.geoTransformationToolStripMenuItem.Text = "几何变换";
            // 
            // mirrorXaxisToolStripMenuItem
            // 
            this.mirrorXaxisToolStripMenuItem.Name = "mirrorXaxisToolStripMenuItem";
            this.mirrorXaxisToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.mirrorXaxisToolStripMenuItem.Text = "X轴镜像";
            this.mirrorXaxisToolStripMenuItem.Click += new System.EventHandler(this.mirrorXaxisToolStripMenuItem_Click);
            // 
            // mirrorYaxisToolStripMenuItem
            // 
            this.mirrorYaxisToolStripMenuItem.Name = "mirrorYaxisToolStripMenuItem";
            this.mirrorYaxisToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.mirrorYaxisToolStripMenuItem.Text = "Y轴镜像";
            this.mirrorYaxisToolStripMenuItem.Click += new System.EventHandler(this.mirrorYaxisToolStripMenuItem_Click);
            // 
            // mirrorOriginToolStripMenuItem
            // 
            this.mirrorOriginToolStripMenuItem.Name = "mirrorOriginToolStripMenuItem";
            this.mirrorOriginToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.mirrorOriginToolStripMenuItem.Text = "中心镜像";
            this.mirrorOriginToolStripMenuItem.Click += new System.EventHandler(this.mirrorOriginToolStripMenuItem_Click);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.rotateToolStripMenuItem.Text = "图像旋转";
            this.rotateToolStripMenuItem.Click += new System.EventHandler(this.rotateToolStripMenuItem_Click);
            // 
            // zoomInOutToolStripMenuItem
            // 
            this.zoomInOutToolStripMenuItem.Name = "zoomInOutToolStripMenuItem";
            this.zoomInOutToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.zoomInOutToolStripMenuItem.Text = "图像缩放";
            this.zoomInOutToolStripMenuItem.Click += new System.EventHandler(this.zoomInOutToolStripMenuItem_Click);
            // 
            // colorPaletteToolStripMenuItem
            // 
            this.colorPaletteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPaletteToolStripMenuItem,
            this.paletteNegativeToolStripMenuItem,
            this.specifiedColorPaletteToolStripMenuItem,
            this.toolStripSeparator2,
            this.indexImageToGrayImageToolStripMenuItem,
            this.indexColorPaletteToGrayToolStripMenuItem,
            this.colorToGrayToolStripMenuItem,
            this.彩色通道分解ToolStripMenuItem});
            this.colorPaletteToolStripMenuItem.Name = "colorPaletteToolStripMenuItem";
            this.colorPaletteToolStripMenuItem.Size = new System.Drawing.Size(178, 35);
            this.colorPaletteToolStripMenuItem.Text = "彩色与调色板";
            // 
            // editPaletteToolStripMenuItem
            // 
            this.editPaletteToolStripMenuItem.Name = "editPaletteToolStripMenuItem";
            this.editPaletteToolStripMenuItem.Size = new System.Drawing.Size(459, 44);
            this.editPaletteToolStripMenuItem.Text = "编辑调色板";
            this.editPaletteToolStripMenuItem.Click += new System.EventHandler(this.editPaletteToolStripMenuItem_Click);
            // 
            // paletteNegativeToolStripMenuItem
            // 
            this.paletteNegativeToolStripMenuItem.Name = "paletteNegativeToolStripMenuItem";
            this.paletteNegativeToolStripMenuItem.Size = new System.Drawing.Size(459, 44);
            this.paletteNegativeToolStripMenuItem.Text = "调色板负片";
            this.paletteNegativeToolStripMenuItem.Click += new System.EventHandler(this.paletteNegativeToolStripMenuItem_Click);
            // 
            // specifiedColorPaletteToolStripMenuItem
            // 
            this.specifiedColorPaletteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.specifiedColorToolStripMenuItem,
            this.toolStripSeparator3,
            this.redPaletteToolStripMenuItem1,
            this.greedPaletteToolStripMenuItem,
            this.bluePaletteToolStripMenuItem,
            this.yellowPaletteToolStripMenuItem,
            this.cyanPaletteToolStripMenuItem,
            this.purplePaletteToolStripMenuItem});
            this.specifiedColorPaletteToolStripMenuItem.Name = "specifiedColorPaletteToolStripMenuItem";
            this.specifiedColorPaletteToolStripMenuItem.Size = new System.Drawing.Size(459, 44);
            this.specifiedColorPaletteToolStripMenuItem.Text = "指定颜色的调色板";
            // 
            // specifiedColorToolStripMenuItem
            // 
            this.specifiedColorToolStripMenuItem.Name = "specifiedColorToolStripMenuItem";
            this.specifiedColorToolStripMenuItem.Size = new System.Drawing.Size(267, 44);
            this.specifiedColorToolStripMenuItem.Text = "自定义颜色";
            this.specifiedColorToolStripMenuItem.Click += new System.EventHandler(this.specifiedColorToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(264, 6);
            // 
            // redPaletteToolStripMenuItem1
            // 
            this.redPaletteToolStripMenuItem1.Name = "redPaletteToolStripMenuItem1";
            this.redPaletteToolStripMenuItem1.Size = new System.Drawing.Size(267, 44);
            this.redPaletteToolStripMenuItem1.Text = "红色";
            this.redPaletteToolStripMenuItem1.Click += new System.EventHandler(this.redPaletteToolStripMenuItem1_Click);
            // 
            // greedPaletteToolStripMenuItem
            // 
            this.greedPaletteToolStripMenuItem.Name = "greedPaletteToolStripMenuItem";
            this.greedPaletteToolStripMenuItem.Size = new System.Drawing.Size(267, 44);
            this.greedPaletteToolStripMenuItem.Text = "绿色";
            this.greedPaletteToolStripMenuItem.Click += new System.EventHandler(this.greenPaletteToolStripMenuItem_Click);
            // 
            // bluePaletteToolStripMenuItem
            // 
            this.bluePaletteToolStripMenuItem.Name = "bluePaletteToolStripMenuItem";
            this.bluePaletteToolStripMenuItem.Size = new System.Drawing.Size(267, 44);
            this.bluePaletteToolStripMenuItem.Text = "蓝色";
            this.bluePaletteToolStripMenuItem.Click += new System.EventHandler(this.bluePaletteToolStripMenuItem_Click);
            // 
            // yellowPaletteToolStripMenuItem
            // 
            this.yellowPaletteToolStripMenuItem.Name = "yellowPaletteToolStripMenuItem";
            this.yellowPaletteToolStripMenuItem.Size = new System.Drawing.Size(267, 44);
            this.yellowPaletteToolStripMenuItem.Text = "黄色";
            this.yellowPaletteToolStripMenuItem.Click += new System.EventHandler(this.yellowPaletteToolStripMenuItem_Click);
            // 
            // cyanPaletteToolStripMenuItem
            // 
            this.cyanPaletteToolStripMenuItem.Name = "cyanPaletteToolStripMenuItem";
            this.cyanPaletteToolStripMenuItem.Size = new System.Drawing.Size(267, 44);
            this.cyanPaletteToolStripMenuItem.Text = "青色";
            this.cyanPaletteToolStripMenuItem.Click += new System.EventHandler(this.cyanPaletteToolStripMenuItem_Click);
            // 
            // purplePaletteToolStripMenuItem
            // 
            this.purplePaletteToolStripMenuItem.Name = "purplePaletteToolStripMenuItem";
            this.purplePaletteToolStripMenuItem.Size = new System.Drawing.Size(267, 44);
            this.purplePaletteToolStripMenuItem.Text = "紫色";
            this.purplePaletteToolStripMenuItem.Click += new System.EventHandler(this.purplePaletteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(456, 6);
            // 
            // indexImageToGrayImageToolStripMenuItem
            // 
            this.indexImageToGrayImageToolStripMenuItem.Name = "indexImageToGrayImageToolStripMenuItem";
            this.indexImageToGrayImageToolStripMenuItem.Size = new System.Drawing.Size(459, 44);
            this.indexImageToGrayImageToolStripMenuItem.Text = "索引彩色图像转灰度图像";
            this.indexImageToGrayImageToolStripMenuItem.Click += new System.EventHandler(this.indexImageToGrayImageToolStripMenuItem_Click);
            // 
            // indexColorPaletteToGrayToolStripMenuItem
            // 
            this.indexColorPaletteToGrayToolStripMenuItem.Name = "indexColorPaletteToGrayToolStripMenuItem";
            this.indexColorPaletteToGrayToolStripMenuItem.Size = new System.Drawing.Size(459, 44);
            this.indexColorPaletteToGrayToolStripMenuItem.Text = "索引彩色调色板转灰度调色板";
            this.indexColorPaletteToGrayToolStripMenuItem.Click += new System.EventHandler(this.indexColorPaletteToGrayToolStripMenuItem_Click);
            // 
            // colorToGrayToolStripMenuItem
            // 
            this.colorToGrayToolStripMenuItem.Name = "colorToGrayToolStripMenuItem";
            this.colorToGrayToolStripMenuItem.Size = new System.Drawing.Size(459, 44);
            this.colorToGrayToolStripMenuItem.Text = "彩色图像转灰度图像";
            this.colorToGrayToolStripMenuItem.Click += new System.EventHandler(this.colorToGrayToolStripMenuItem_Click);
            // 
            // 彩色通道分解ToolStripMenuItem
            // 
            this.彩色通道分解ToolStripMenuItem.Name = "彩色通道分解ToolStripMenuItem";
            this.彩色通道分解ToolStripMenuItem.Size = new System.Drawing.Size(459, 44);
            this.彩色通道分解ToolStripMenuItem.Text = "彩色图像通道分解与转换";
            this.彩色通道分解ToolStripMenuItem.Click += new System.EventHandler(this.彩色通道分解ToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2});
            this.statusStrip.Location = new System.Drawing.Point(0, 1041);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip.Size = new System.Drawing.Size(1434, 41);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(135, 31);
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 29);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(182, 31);
            this.toolStripStatusLabel2.Text = "图像大小及属性";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripSeparator6,
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 43);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1434, 42);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(46, 36);
            this.toolStripButton2.Text = "Panel Refresh";
            this.toolStripButton2.ToolTipText = "重读图像";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::DipCSharp.Properties.Resources.鼠标;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(46, 36);
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::DipCSharp.Properties.Resources.十字;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(46, 36);
            this.toolStripButton3.Text = "拉框放大 (Shift+左键)";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(46, 36);
            this.toolStripButton4.Text = "图像平移 (中键)";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.Control;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 85);
            this.panel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1434, 997);
            this.panel.TabIndex = 1;
            this.panel.TabStop = false;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.panel.DoubleClick += new System.EventHandler(this.panel_DoubleClick);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseWheel);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 1082);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.Text = "Digital Image Processing";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem processingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geoTransformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorXaxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorYaxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorOriginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histgramEqualizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paletteNegativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specifiedColorPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specifiedColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redPaletteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem greedPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bluePaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yellowPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cyanPaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purplePaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem grayTransformToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        public System.Windows.Forms.PictureBox panel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem addSaltPepperNoiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AverageFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EdgeMediumFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EdgeAverageFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConvolutionFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem binarizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gradianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLenaToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem indexColorPaletteToGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexImageToGrayImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 彩色通道分解ToolStripMenuItem;
    }
}


