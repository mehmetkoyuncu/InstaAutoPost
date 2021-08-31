using InstaAutoPost.UI.Core.Common.CharacterConverter;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;

namespace InstaAutoPost.UI.Core.Utilities
{
    public class ImageUtility
    {
        public string Download(string imageURL, string fileName, ImageFormat imageFormat,string contentRootPath)
        {
            string imageSrc;
            try
            {
                string sImageFormat = "." + ((imageFormat.ToString()).ToLower());
                CharacterConvertGenerator generator = new CharacterConvertGenerator();
                fileName = generator.TurkishToEnglish(fileName);
                fileName = generator.RemovePunctuation(fileName);
                fileName = fileName.Substring(0, 5);
                fileName = fileName + Guid.NewGuid().ToString();
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(imageURL);
                Bitmap bitmap; bitmap = new Bitmap(stream);

                if (bitmap != null)
                {

                    System.IO.FileStream fs = System.IO.File.Open(Path.Combine(contentRootPath + @"/wwwroot/images", fileName + sImageFormat), FileMode.Create);
                    bitmap.Save(fs, imageFormat);
                    fs.Close();
                }

                stream.Flush();
                stream.Close();
                client.Dispose();
                imageSrc = fileName + sImageFormat;


            }
            catch (Exception ex)
            {
                imageSrc = "Fotoğraf Yüklenemedi..";
            }

            return imageSrc;
        }
    }
}
