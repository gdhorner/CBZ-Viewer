using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronOcr;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MangaBu
{
    public partial class ScanText : Form
    {
        private const string creds = "C:\\Users\\Gavin\\AppData\\Roaming\\gcloud\\application_default_credentials.json";
        private int startX, startY, endX, endY, scanHeight, scanWidth;
        private bool mouseDown;
        private Bitmap? scrBmp;
        private Graphics? scrGrp;

        public ScanText()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", creds);

            
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;

            Size = Screen.FromControl(this).Bounds.Size;
            CenterToScreen();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.TransparencyKey = Color.Transparent;

        }

         protected override void OnPaintBackground(PaintEventArgs e) { /* Ignore */ }

        private void GLens_Analyze(Image image)
        {
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.JapaneseBest;
            var Result = Ocr.Read(image);
            Console.WriteLine(Result.Text);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseDown = true;
            startX = e.X;
            startY = e.Y;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (mouseDown)
            {
                endX = e.X;
                endY = e.Y;
                scanHeight = Math.Abs(endY - startY);
                scanWidth = Math.Abs(endX - startX);
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseDown = false;

            if (scrBmp == null)
            {
                scrBmp = new Bitmap(scanWidth, scanHeight);
                scrGrp = Graphics.FromImage(scrBmp);
            }

            scrGrp.CopyFromScreen(startX, startY, 0, 0, scrBmp.Size);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen blackPen = new(Color.Black);
            blackPen.DashStyle = DashStyle.Dash;
            Rectangle rect = new(startX, startY, scanWidth, scanHeight);
            e.Graphics.DrawRectangle(blackPen, rect);

            if (scrBmp != null)
            {
                var pos = Cursor.Position;
                var cr = RectangleToScreen(rect);
                var dY = cr.Top - Top;
                var dX = cr.Left - Left;

                e.Graphics.TranslateTransform(Width / 2, Height / 2);
                e.Graphics.TranslateTransform(-pos.X - dX, -pos.Y - dY);

                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

                if (scrBmp != null) e.Graphics.DrawImage(scrBmp, 0, 0);

                scrBmp.Save("image.png");
                GLens_Analyze(scrBmp);
            }
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape || e.Control && e.KeyCode == Keys.L)
            {
                Dispose();
            }
        }
    }

}
