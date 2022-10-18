using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Vision.V1;
using Image = Google.Cloud.Vision.V1.Image;

namespace CBZ_Viewer
{
    public partial class GLens : Form
    {
        private const string creds = "C:\\Users\\Gavin\\AppData\\Roaming\\gcloud\\application_default_credentials.json";

        public GLens()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", creds);
            InitializeComponent();
        }

        private void OnShown(object sender, EventArgs e)
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
        }
    }
}
