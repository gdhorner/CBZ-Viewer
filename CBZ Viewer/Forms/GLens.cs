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
using Google.Cloud.Vision.V1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Image = Google.Cloud.Vision.V1.Image;

namespace CBZ_Viewer
{
    public partial class GLens : Form
    {
        private const string creds = "C:\\Users\\Gavin\\AppData\\Roaming\\gcloud\\application_default_credentials.json";

        public GLens()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", creds);

            
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;

            Size = Screen.FromControl(this).Bounds.Size;
            CenterToScreen();

           // SetStyle(ControlStyles.SupportsTransparentBackColor, true);
           // this.BackColor = Color.Transparent;
           // this.TransparencyKey = Color.Transparent;

        }

       // protected override void OnPaintBackground(PaintEventArgs e) { /* Ignore */ }


       /* private void GLens_Analyze()
        {
            var client = ImageAnnotatorClient.Create();
            var image = Image.FromUri("gs://cloud-vision-codelab/otter_crossing.jpg");
            var response = client.DetectText(image);
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                {
                    Console.WriteLine(annotation.Description);
                }
            }
        }*/

        int startX, startY, endX, endY;
        bool mouseDown;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseDown = true;
            startX = e.X;
            startY = e.Y;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            endX = e.X;
            endY = e.Y;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black);
            blackPen.DashStyle = DashStyle.Dash;
            Rectangle rect = new(startX, startY, endX - startX, endY - startY);
            e.Graphics.DrawRectangle(blackPen, rect);
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
