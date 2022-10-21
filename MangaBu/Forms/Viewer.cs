using IronOcr;
using MangaBu.Functions;
using MangaBu.Models;

namespace MangaBu
{
    public partial class Viewer : Form
    {
        private int currentPage = 0;

        private ComicBook comicBook;

        string[]? images;

        int ZoomSize = 1;

        public Viewer(ComicBook comicBook)
        {
            InitializeComponent();

            this.comicBook = comicBook;

            this.Text = comicBook.ComicName + " " + comicBook.IssueNumber;
        }

        private async void Viewer_Load(object sender, EventArgs e)
        {
            images = await ComicFunctions.ReadComic(comicBook);

            comicBook.Pages = images.Length;

            pbPageImage.Size = pnlPages.Size;

            currentPage = comicBook.CurrentPage - 1;

            lblPageCount.Text = $"{comicBook.Pages}";
            //tbPageInput.Text = $"{currentPage + 1}";

            this.Focus();

            pbPageImage.Image = await GlobalFunctions.CompressImage(images[currentPage]);

        }

        private void Viewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Directory.Delete(MangaBu.ComicExtractLocation + "\\" + comicBook.SeriesId, true);
            // if (MainScreen.UserData.Settings.SaveLastPage)
            // {
            //     comicIssue.CurrentPage = currentPage + 1;
            // }
        }

        private void pnlLeft_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        private void pnlRight_Click(object sender, EventArgs e)
        {
            PreviousPage();
        }

        async void NextPage()
        {
            if (currentPage < comicBook.Pages - 1)
            {
                currentPage++;
                pbPageImage.Image.Dispose();

                if (File.Exists(images[currentPage]))
                {
                    pbPageImage.Image = await GlobalFunctions.CompressImage(images[currentPage]);
                }
                else
                {
                    comicBook.Completed = true;
                }
            }
        }

        async void PreviousPage()
        {
            if (currentPage > 0)
            {
                currentPage--;
                pbPageImage.Image.Dispose();

                if (File.Exists(images[currentPage]))
                {
                    pbPageImage.Image = await GlobalFunctions.CompressImage(images[currentPage]);
                }

            }
        }

        private void ZoomIn()
        {
            if (ZoomSize < 15)
            {
                //Zoom ratio by which the images will be zoomed by default
                int zoomRatio = 10;
                //Set the zoomed width and height
                int widthZoom = pnlPages.Width * zoomRatio / 100;
                int heightZoom = pnlPages.Height * zoomRatio / 100;

                //Add the width and height to the picture box dimensions
                pbPageImage.Width += widthZoom;
                pbPageImage.Height += heightZoom;

                ZoomSize++;
            }
        }

        private static void Magnify()
        {
            var mg = new Magnify()
            {
                Size = new Size(150, 150),
                AutoClose = true,
                HideCursor = true,
                ZoomFactor = 2,
            };
            mg.Show();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape) { Dispose(); }
            else if (e.KeyCode == Keys.Left) { NextPage(); }
            else if (e.KeyCode == Keys.Right) { PreviousPage(); }
            else if (e.Control && e.KeyCode == Keys.M) { Magnify(); }
            else if (e.Control && e.KeyCode == Keys.L) { scanning = true; }
        }

        // The following three methods will draw a rectangle and allow 
        // the user to use the mouse to resize the rectangle.  If the 
        // rectangle intersects a control's client rectangle, the 
        // control's color will change.

        bool isDrag, scanning = false;
        Rectangle theRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
        Point startPoint;

        private void pbPageImage_MouseDown(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {

            // Set the isDrag variable to true and get the starting point 
            // by using the PointToScreen method to convert form 
            // coordinates to screen coordinates.
            if (e.Button == MouseButtons.Left && scanning)
            {
                isDrag = true;
            }

            Control control = (Control)sender;

            // Calculate the startPoint by using the PointToScreen 
            // method.
            startPoint = control.PointToScreen(new Point(e.X, e.Y));
        }

        private void pbPageImage_MouseMove(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {

            // If the mouse is being dragged, 
            // undraw and redraw the rectangle as the mouse moves.
            if (isDrag && scanning)

            // Hide the previous rectangle by calling the 
            // DrawReversibleFrame method with the same parameters.
            {
                ControlPaint.DrawReversibleFrame(theRectangle,
                    this.BackColor, FrameStyle.Dashed);

                // Calculate the endpoint and dimensions for the new 
                // rectangle, again using the PointToScreen method.
                Point endPoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));

                int width = endPoint.X - startPoint.X;
                int height = endPoint.Y - startPoint.Y;
                theRectangle = new Rectangle(startPoint.X,
                    startPoint.Y, width, height);

                // Draw the new rectangle by calling DrawReversibleFrame
                // again.  
                ControlPaint.DrawReversibleFrame(theRectangle,
                    this.BackColor, FrameStyle.Dashed);
            }
        }

        private void pbPageImage_MouseUp(object sender,
               System.Windows.Forms.MouseEventArgs e)
        {
            // If the MouseUp event occurs, the user is not dragging.
            if (scanning)
            {
                isDrag = false;

                // Draw the rectangle to be evaluated. Set a dashed frame style 
                // using the FrameStyle enumeration.
                ControlPaint.DrawReversibleFrame(theRectangle,
                    this.BackColor, FrameStyle.Dashed);

                // Find out which controls intersect the rectangle and 
                // change their color. The method uses the RectangleToScreen  
                // method to convert the Control's client coordinates 
                // to screen coordinates.
                Rectangle controlRectangle;
                for (int i = 0; i < Controls.Count; i++)
                {
                    controlRectangle = Controls[i].RectangleToScreen
                        (Controls[i].ClientRectangle);
                    if (controlRectangle.IntersectsWith(theRectangle))
                    {
                        Controls[i].BackColor = Color.BurlyWood;
                    }
                }

                Scan();
                // Reset the rectangle.
                //theRectangle = new Rectangle(0, 0, 0, 0);
            }
        }

        private void Scan()
        {
            Bitmap? scrBmp = new Bitmap(theRectangle.Width, theRectangle.Height);
            Graphics? scrGrp = Graphics.FromImage(scrBmp);

            scrGrp.CopyFromScreen(theRectangle.X, theRectangle.Y, 0, 0, scrBmp.Size);
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.JapaneseBest;
            var Result = Ocr.Read(scrBmp);
            if (string.IsNullOrWhiteSpace(Result.Text)) { return; }
            Clipboard.SetText(Result.Text);
        }
    }

}
