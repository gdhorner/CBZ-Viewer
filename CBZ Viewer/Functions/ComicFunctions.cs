using CBZ_Viewer.Models;
using System.IO.Compression;

namespace CBZ_Viewer.Functions
{
    internal class ComicFunctions
    {
        public static Task<string[]> ReadComic(ComicBook comic)
        {
            string dir = Directory.CreateDirectory(App.ComicExtractLocation + "\\" + comic.SeriesId + "\\" + comic.IssueId).FullName;

            using (ZipArchive archive = ZipFile.OpenRead(comic.Location))
            {
                archive.ExtractToDirectory(dir);

                var result = Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories).ToArray();

                Array.Sort(result);

                return Task.FromResult(result);
            }
        }
    }
}
