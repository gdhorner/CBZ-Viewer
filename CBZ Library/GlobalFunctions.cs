using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Xceed.Wpf.Toolkit;

namespace CBZ_Library
{
    public static class GlobalFunctions
    {
        public static async Task<Bitmap> CompressImage(string ImageFilePath)
        {
            try
            {
                if (ImageFilePath != "")
                {
                    using (Image img = Image.FromFile(ImageFilePath))
                    {
                        Bitmap bmp = new Bitmap(img.Width, img.Height);
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            watch.Stop();
                            Console.WriteLine(watch.ElapsedMilliseconds + "ms: " + ImageFilePath);

                            g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        }

                        return await Task.FromResult(bmp);
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show($"There was an error loading an image...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }
    }
}
