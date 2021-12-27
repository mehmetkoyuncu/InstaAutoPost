using InstaAutoPost.UI.Core.Common.CharacterConverter;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using SixLabors.ImageSharp.Processing;
using System.Drawing.Drawing2D;
using InstaAutoPost.UI.Core.Common.Constants;

namespace InstaAutoPost.UI.Core.Utilities
{
    public class ImageUtility
    {
        public string Download(string imageURL, string fileName, ImageFormat imageFormat, string contentRootPath, bool isCreateFolder = false, string rooth = "", bool isContent = false,string content="")
        {
            string imageSrc;
            try

            {
                string orijinalFileName = fileName;
                string sImageFormat = "." + ((imageFormat.ToString()).ToLower());
                fileName = CharacterConvertGenerator.TurkishToEnglish(fileName);
                fileName = CharacterConvertGenerator.RemovePunctuation(fileName);
                fileName = fileName.Length > 5 ? fileName.Substring(0, 5).Trim() : fileName.Trim();
                fileName = fileName + Guid.NewGuid().ToString();
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(imageURL);
                Bitmap bitmap = new Bitmap(stream);
                System.IO.FileStream fs = null;
                System.IO.FileStream fullFs = null;
                if (bitmap != null)
                {

                    if (isCreateFolder == false)
                    {
                        fs = System.IO.File.Open(Path.Combine(contentRootPath + @"/wwwroot/images", fileName + sImageFormat), FileMode.Create);
                    }
                    else
                    {
                        fs = System.IO.File.Open(Path.Combine(contentRootPath + @"/wwwroot/" + rooth, fileName + sImageFormat), FileMode.Create);
                    }

                    bitmap.Save(fs, imageFormat);

                    fs.Close();
                    if (fs != null && isContent == true)
                    {

                        //Şablon Arkaplan
                        Bitmap backImage = new Bitmap(1080, 1080);
                        //Resim boyutlandır
                       
                        //using (Graphics graphics = Graphics.FromImage(backImage))
                        //{
                            ApplyTemplate(DesignConstants.Wanted, backImage, contentRootPath,bitmap,orijinalFileName,content);

                        //graphics.DrawImage(imageL, 250, 230);
                        //Image design = Image.FromFile(contentRootPath + @"/wwwroot/img/image-design/6.png");
                        //Bitmap backDesign = new Bitmap(design, new Size(500, 500));
                        //graphics.DrawImage(backDesign, 0, 0);
                       
                        //}
                        fullFs = System.IO.File.Open(Path.Combine(contentRootPath + @"/wwwroot/images", fileName + "_full" + sImageFormat), FileMode.Create);
                        var fonts = new List<FontFamily>();
                        foreach (FontFamily font_family in fonts)
                        {
                            fonts.Add(font_family);
                        }
                        backImage.Save(fullFs, imageFormat);
                        fullFs.Close();

                    }
                }

                stream.Flush();
                stream.Close();
                client.Dispose();
                if (fs != null && isContent == true)
                    imageSrc = fileName + "_full" + sImageFormat;
                else
                    imageSrc = fileName + "_full" + sImageFormat;

            }
            catch (Exception ex)
            {
                imageSrc = null;
            }

            return imageSrc;
        }
        private static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        public static Image CropImage(Image imgToResize, Size destinationSize)
        {
            var originalWidth = imgToResize.Width;
            var originalHeight = imgToResize.Height;

            var hRatio = (float)originalHeight / destinationSize.Height;
            var wRatio = (float)originalWidth / destinationSize.Width;

            var ratio = Math.Min(hRatio, wRatio);

            var hScale = Convert.ToInt32(destinationSize.Height * ratio);
            var wScale = Convert.ToInt32(destinationSize.Width * ratio);

            var startX = (originalWidth - wScale) / 2;
            var startY = (originalHeight - hScale) / 2;

            var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

            var bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);

            var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }

            return bitmap;

        }

        public static Image ApplyTemplate(string template, Image backImage,string contentRootPath,Bitmap bitmap,string title="",string content="")
        {
            Image image = null;
            Image croppedImage = null;
            Image design = Image.FromFile(contentRootPath + @"/wwwroot/img/image-design/"+ template);
            Image logo= Image.FromFile(contentRootPath + @"/wwwroot/img/logo/logo.png");
            Image facebookLogo = Image.FromFile(contentRootPath + @"/wwwroot/img/logo/facebook.png");
            Image twitterLogo = Image.FromFile(contentRootPath + @"/wwwroot/img/logo/twitter.png");
            Image instagramLogo = Image.FromFile(contentRootPath + @"/wwwroot/img/logo/instagram.png");
            Bitmap templateDesign = new Bitmap(design, new Size(1080, 1080));
            StringFormat verticalFormat = new StringFormat();
            verticalFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;
            StringFormat alignCenterFormat = new StringFormat();
            alignCenterFormat.LineAlignment = StringAlignment.Center;
            using (Graphics graphics = Graphics.FromImage(backImage))
            {
                switch (template)
                {

                    case DesignConstants.PurpleRainbow:
                        image = ResizeImage(bitmap as Image, new Size(1000, 750));
                        croppedImage = CropImage(image, new Size(1000, 750));
                        graphics.DrawImage(templateDesign, 0, 0);
                        graphics.DrawImage(croppedImage, 40, 40);
                        graphics.DrawImage(logo, 880, 50,150,150);
                        using (Font arialFont = new Font("Arial", 25))
                        {
                            int maxWidth = 1000;
                            int maxHeight = 250;
                            graphics.FillRectangle(Brushes.SlateGray, 50, 720, 400, 60);
                            graphics.DrawImage(instagramLogo, 57, 730, 40, 40);
                            graphics.DrawImage(twitterLogo, 100, 730, 40, 40);
                            graphics.DrawImage(facebookLogo, 143, 730, 40, 40);
                            graphics.DrawString("@bertarafgundem", arialFont, Brushes.White, new PointF(185, 730));
                            graphics.DrawString(title, new Font("Arial",30), Brushes.White, new Rectangle(30, 800,maxWidth,maxHeight),centerFormat);
                        }
                        break;
                    case DesignConstants.Wanted:
                        image = ResizeImage(bitmap as Image, new Size(924, 762));
                        croppedImage = CropImage(image, new Size(924, 762));
                        graphics.DrawImage(templateDesign, 0, 0);
                        graphics.DrawImage(croppedImage, 78, 100);
                        graphics.DrawImage(logo, 840, 120, 150, 150);
                        using (Font arialFont = new Font("Arial", 25))
                        {
                            int maxWidth = 955;
                            int maxHeight = 200;
                            graphics.FillRectangle(Brushes.SlateGray, 100,790, 410, 60);
                            graphics.DrawImage(instagramLogo, 107, 800, 40, 40);
                            graphics.DrawImage(twitterLogo, 150, 800, 40, 40);
                            graphics.DrawImage(facebookLogo, 193, 800, 40, 40);
                            graphics.DrawString("@bertarafgundem", arialFont, Brushes.White, new PointF(235, 800));
                            graphics.DrawString(title, new Font("Arial", 25), Brushes.White, new Rectangle(60, 880, maxWidth, maxHeight), centerFormat);
                        }
                        break;
                    case DesignConstants.Default:
                          image = ResizeImage(bitmap as Image, new Size(1000, 750));
                        croppedImage = CropImage(image, new Size(1000, 750));
                        graphics.DrawImage(templateDesign, 0, 0);
                        graphics.DrawImage(croppedImage, 40, 40);
                        graphics.DrawImage(logo, 880, 50,150,150);
                        using (Font arialFont = new Font("Arial", 25))
                        {
                            int maxWidth = 1000;
                            int maxHeight = 250;
                            graphics.FillRectangle(Brushes.SlateGray, 50, 720, 400, 60);
                            graphics.DrawImage(instagramLogo, 57, 730, 40, 40);
                            graphics.DrawImage(twitterLogo, 100, 730, 40, 40);
                            graphics.DrawImage(facebookLogo, 143, 730, 40, 40);
                            graphics.DrawString("@bertarafgundem", arialFont, Brushes.White, new PointF(185, 730));
                            graphics.DrawString(title, new Font("Arial",30), Brushes.White, new Rectangle(30, 800,maxWidth,maxHeight),centerFormat);
                        }
                        break;
                     
                    case DesignConstants.Design8:
                        Rectangle blackBackgroundImageSize = new Rectangle(0, 0, 1080, 1080);
                        graphics.FillRectangle(Brushes.Black, blackBackgroundImageSize);
                        image = ResizeImage(bitmap as Image, new Size(850, 700));
                        croppedImage = CropImage(image, new Size(850, 700));
                        graphics.DrawImage(croppedImage, 115, 115);
                        graphics.DrawImage(templateDesign, 0, 0);

                        using (Font arialFont = new Font("Arial", 20))
                        {
                            int maxWidth = 870;
                            int maxHeight = 100;
                            int maxHeigtTitle = 250;
                            graphics.DrawString("WWW.GOOGLE.COM", arialFont, Brushes.Black, new PointF(9, 350), verticalFormat);
                            graphics.DrawString("Logo Buraya", arialFont, Brushes.White, new PointF(930, 30));
                            graphics.DrawString(content, new Font("Arial", 15), Brushes.White, new Rectangle(135, 887, maxWidth, maxHeight), alignCenterFormat);
                            graphics.DrawString(title, new Font("Arial", 35), Brushes.Black, new Rectangle(80, 30, maxWidth, maxHeigtTitle), alignCenterFormat);
                        }
                        break;
                }
            }
            return backImage;
        }

    }
}
