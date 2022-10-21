using MangaBu.Models;
using System.IO.Compression;

namespace MangaBu.Functions
{
    internal class ComicFunctions
    {
        public static Task<string[]> ReadComic(ComicBook comic)
        {
            string dir = Directory.CreateDirectory(MangaBu.ComicExtractLocation + "\\" + comic.SeriesId + "\\" + comic.IssueId).FullName;

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
