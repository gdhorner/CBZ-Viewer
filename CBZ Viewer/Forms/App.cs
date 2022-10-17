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
            OpenFileDialog openFile = new()
            {
                InitialDirectory = "c:\\",
                Filter = "CBZ Files (*.cbz)|*.cbz",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(Path.GetFileName(Path.GetDirectoryName(openFile.FileName)))) { return; }

                ComicBook comic = new()
                {
                    Location = openFile.FileName,
                    ComicName = Path.GetFileName(Path.GetDirectoryName(openFile.FileName))

                };

                OpenReader(comic);
            }
        }

        public static void OpenReader(ComicBook comicBook)
        {
            Viewer viewer = new Viewer(comicBook);
            viewer.Show();
        }

        private void App_Load(object sender, EventArgs e)
        {

        }
    }
}