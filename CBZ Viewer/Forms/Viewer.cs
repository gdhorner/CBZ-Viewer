using CBZ_Viewer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Directory.Delete(App.ComicExtractLocation + "\\" + comicBook.SeriesId, true);
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
            if(currentPage < comicBook.Pages - 1)
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

        private void Magnify()
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape) {Dispose();}
            else if (e.Control && e.KeyCode == Keys.M) Magnify();
        }

    }
}
