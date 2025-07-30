// Copyright (c) 2025 Ming Hu. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace DipCSharp
{
    partial class ColorChannels
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
            this.pictureRGB = new System.Windows.Forms.PictureBox();
            this.pictureR = new System.Windows.Forms.PictureBox();
            this.pictureG = new System.Windows.Forms.PictureBox();
            this.pictureB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRGB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureB)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureRGB
            // 
            this.pictureRGB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureRGB.Location = new System.Drawing.Point(6, 9);
            this.pictureRGB.Name = "pictureRGB";
            this.pictureRGB.Size = new System.Drawing.Size(343, 228);
            this.pictureRGB.TabIndex = 0;
            this.pictureRGB.TabStop = false;
            this.pictureRGB.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureRGB_Paint);
            // 
            // pictureR
            // 
            this.pictureR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureR.Location = new System.Drawing.Point(355, 9);
            this.pictureR.Name = "pictureR";
            this.pictureR.Size = new System.Drawing.Size(343, 228);
            this.pictureR.TabIndex = 1;
            this.pictureR.TabStop = false;
            this.pictureR.Click += new System.EventHandler(this.pictureR_Click);
            this.pictureR.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureR_Paint);
            // 
            // pictureG
            // 
            this.pictureG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureG.Location = new System.Drawing.Point(6, 243);
            this.pictureG.Name = "pictureG";
            this.pictureG.Size = new System.Drawing.Size(343, 228);
            this.pictureG.TabIndex = 2;
            this.pictureG.TabStop = false;
            this.pictureG.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureG_Paint);
            // 
            // pictureB
            // 
            this.pictureB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureB.Location = new System.Drawing.Point(355, 243);
            this.pictureB.Name = "pictureB";
            this.pictureB.Size = new System.Drawing.Size(343, 228);
            this.pictureB.TabIndex = 3;
            this.pictureB.TabStop = false;
            this.pictureB.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureB_Paint);
            // 
            // ColorChannels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 480);
            this.Controls.Add(this.pictureB);
            this.Controls.Add(this.pictureG);
            this.Controls.Add(this.pictureR);
            this.Controls.Add(this.pictureRGB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorChannels";
            this.Text = "ColorChannels";
            this.Load += new System.EventHandler(this.ColorChannels_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureRGB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureRGB;
        private System.Windows.Forms.PictureBox pictureR;
        private System.Windows.Forms.PictureBox pictureG;
        private System.Windows.Forms.PictureBox pictureB;
    }
}
