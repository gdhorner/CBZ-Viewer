using System.IO.Compression;
using System.IO;
using static CBZ_Viewer.ComicFunctions;
using CBZ_Viewer.Models;

namespace CBZ_Viewer
{
    public partial class App : Form
    {

        public static string ComicExtractLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CBZ Viewer\comics";

        public App()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "CBZ Files (*.cbz)|*.cbz";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ComicBook comic = new ComicBook()
                {
                    Location = openFileDialog1.FileName,
                    ComicName = Path.GetFileName(Path.GetDirectoryName(openFileDialog1.FileName))
                    
                };

                OpenReader(comic);
            }
        }

        public void OpenReader(ComicBook comicBook)
        {
            Viewer viewer = new Viewer(comicBook);
            viewer.Show();
        }
    }
}