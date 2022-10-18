using CBZ_Viewer.Functions;
using CBZ_Viewer.Models;

namespace CBZ_Viewer
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
            Directory.Delete(CBZViewer.ComicExtractLocation + "\\" + comicBook.SeriesId, true);
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
            var f = new Magnify()
            {
                Size = new Size(150, 150),
                AutoClose = true,
                HideCursor = true,
                ZoomFactor = 2,
                NearestNeighborInterpolation = false
            };
            f.Show();
        }
        private static void GLens()
        {
            var f = new GLens()
            {
                
            };
            f.Show();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape) { Dispose(); }
            else if (e.KeyCode == Keys.Left) { NextPage(); }
            else if (e.KeyCode == Keys.Right) { PreviousPage(); }
            else if (e.Control && e.KeyCode == Keys.M) { Magnify(); }
            else if (e.Control && e.KeyCode == Keys.L) { GLens(); }
        }

    }
}
