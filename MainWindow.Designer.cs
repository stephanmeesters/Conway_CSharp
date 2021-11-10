using System.Drawing;
using System.Windows.Forms;
using System;

namespace Conway
{
    partial class MainWindow
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

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            Brush blackBrush = (Brush)Brushes.Black;
            Brush whiteBrush = (Brush)Brushes.White;

            for (int i = 0; i < gridWidth_; i++)
            {
                for (int j = 0; j < gridHeight_; j++)
                {
                    g.FillRectangle(graphicsGrid_[i][j] ? whiteBrush : blackBrush,
                        i*pixelSize_, j* pixelSize_, pixelSize_, pixelSize_);
                }
            }
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.conway.randomInitialization(sparseness_);

            // Update the graphics
            this.Refresh();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MainWindow";
            this.Text = "Conway\'s Game of Life (Press any key to reset)";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

