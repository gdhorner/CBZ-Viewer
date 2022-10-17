using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBZ_Viewer.Models;

namespace CBZ_Viewer
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
