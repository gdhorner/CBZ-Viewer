using CBZ_Viewer.Properties;

namespace CBZ_Viewer
{
    partial class Viewer
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
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlPages = new System.Windows.Forms.Panel();
            this.pbPageImage = new System.Windows.Forms.PictureBox();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.pnlPages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPageImage)).BeginInit();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlRight.Location = new System.Drawing.Point(950, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(169, 623);
            this.pnlRight.TabIndex = 6;
            this.pnlRight.Click += new System.EventHandler(this.pnlRight_Click);
            // 
            // pnlPages
            // 
            this.pnlPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPages.AutoScroll = true;
            this.pnlPages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.pnlPages.Controls.Add(this.pbPageImage);
            this.pnlPages.Location = new System.Drawing.Point(246, 0);
            this.pnlPages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlPages.Name = "pnlPages";
            this.pnlPages.Size = new System.Drawing.Size(630, 623);
            this.pnlPages.TabIndex = 4;
            // 
            // pbPageImage
            // 
            this.pbPageImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.pbPageImage.Location = new System.Drawing.Point(0, 0);
            this.pbPageImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pbPageImage.Name = "pbPageImage";
            this.pbPageImage.Size = new System.Drawing.Size(630, 623);
            this.pbPageImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPageImage.TabIndex = 0;
            this.pbPageImage.TabStop = false;
            this.pbPageImage.Click += new System.EventHandler(this.pbPageImage_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlLeft.Controls.Add(this.lblPageCount);
            this.pnlLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlLeft.Location = new System.Drawing.Point(1, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(169, 623);
            this.pnlLeft.TabIndex = 5;
            this.pnlLeft.Click += new System.EventHandler(this.pnlLeft_Click);
            // 
            // lblPageCount
            // 
            this.lblPageCount.AutoSize = true;
            this.lblPageCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPageCount.ForeColor = System.Drawing.Color.White;
            this.lblPageCount.Location = new System.Drawing.Point(10, 47);
            this.lblPageCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(35, 15);
            this.lblPageCount.TabIndex = 1;
            this.lblPageCount.Text = "0 / 0";
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(2)))), ((int)(((byte)(2)))));
            this.ClientSize = new System.Drawing.Size(1120, 623);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlPages);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Viewer";
            this.Text = "ComicViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Viewer_FormClosing);
            this.Load += new System.EventHandler(this.Viewer_Load);
            this.pnlPages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPageImage)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlRight;
        private Panel pnlPages;
        private PictureBox pbPageImage;
        private Panel pnlLeft;
        private Label lblPageCount;
    }
}