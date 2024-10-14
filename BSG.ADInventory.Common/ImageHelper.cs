using BSG.ADInventory.Common.Classes;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace BSG.ADInventory.Common
{
   
   public static class ImageHelper
        {
            private static void EnsureDirectoryExists(string path)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }


            /// <summary>
            /// Resize the image to the specified width and height.
            /// </summary>
            /// <param name="image">The image to resize.</param>
            /// <param name="width">The width to resize to.</param>
            /// <param name="height">The height to resize to.</param>
            /// <returns>The resized image.</returns>
            private static Bitmap ResizeImage(Image image, int width, int height)
            {
                var destRect = new Rectangle(0, 0, width, height);
                var destImage = new Bitmap(width, height);

                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (var graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                return destImage;
            }

            public static PictureSaveResult SavePicture(HttpContextBase context, HttpPostedFileBase file, List<PictureSize> sizes, string savePath)
            {
                var result = new PictureSaveResult();

                // Get the upload path using the provided HttpContext
                var uploadPath = Path.Combine(context.Server.MapPath(savePath));

                // Check if the directory exists and create it if it does not
                EnsureDirectoryExists(uploadPath);

                // Save the original image
                string fileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                string fileExtention = Path.GetExtension(file.FileName);

                var image = System.Drawing.Image.FromStream(file.InputStream);

                foreach (var item in sizes)
                {
                    Bitmap newImage = ResizeImage(image, item.Width, item.Height);
                    newImage.Save(uploadPath + item.Prefix + fileName + fileExtention);
                    switch (item.Prefix)
                    {
                        case "":
                            result.ImageUrl = savePath + item.Prefix + fileName + fileExtention;
                            break;
                        case "Normal_":
                            result.ImageUrlNormalSize = savePath + item.Prefix + fileName + fileExtention;
                            break;
                        case "Thumbnail_":
                            result.ImageUrlThumbnailSize = savePath + item.Prefix + fileName + fileExtention;
                            break;
                        case "Finger_":
                            result.ImageUrlFingerSize = savePath + item.Prefix + fileName + fileExtention;
                            break;
                        default:
                            break;
                    }

                }
                return result;
            }

            public static string SavePicture(HttpContextBase context, HttpPostedFileBase file, PictureSize size, string savePath)
            {
                // Get the upload path using the provided HttpContext
                var uploadPath = Path.Combine(context.Server.MapPath(savePath));

                // Check if the directory exists and create it if it does not
                EnsureDirectoryExists(uploadPath);

                // Save the original image
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff"); //Guid.NewGuid().ToString().Replace("-", string.Empty);
                string fileExtention = Path.GetExtension(file.FileName);

                var image = System.Drawing.Image.FromStream(file.InputStream);

                image.Save(uploadPath + size.Prefix + fileName + fileExtention);

                //Bitmap newImage = ResizeImage(image, size.Width, size.Height);
                //newImage.Save(uploadPath + size.Prefix + fileName + fileExtention);

                var result = savePath + size.Prefix + fileName + fileExtention;

                return result;
            }


            public static PictureSaveResult SaveWebPImage(HttpContextBase context, HttpPostedFileBase file, List<PictureSize> sizes, string savePath)
            {
                var memoryStream = new MemoryStream();
                file.InputStream.CopyTo(memoryStream);
                var webPImage = memoryStream.ToArray();
                var result = new PictureSaveResult();

                // Get the upload path using the provided HttpContext
                var uploadPath = Path.Combine(context.Server.MapPath(savePath));

                // Check if the directory exists and create it if it does not
                EnsureDirectoryExists(uploadPath);

                // Save the original image
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff"); //Guid.NewGuid().ToString().Replace("-", string.Empty);
                string fileExtention = ".webp";// Path.GetExtension(webPImage.FileName);




                using (var image = new MagickImage(webPImage))
                {
                    foreach (var size in sizes)
                    {


                        // Clone the image to resize
                        using (var resizedImage = image.Clone())
                        {
                            // Resize the image
                            resizedImage.Resize((uint)size.Width, (uint)size.Height);

                            // Set output file name and path                       
                            var outputPath = Path.Combine(savePath, fileName + fileExtention);

                            // Save the resized image
                            resizedImage.Write(uploadPath + size.Prefix + fileName + fileExtention);
                            switch (size.Prefix)
                            {
                                case "":
                                    result.ImageUrl = savePath + size.Prefix + fileName + fileExtention;
                                    break;
                                case "Normal_":
                                    result.ImageUrlNormalSize = savePath + size.Prefix + fileName + fileExtention;
                                    break;
                                case "Thumbnail_":
                                    result.ImageUrlThumbnailSize = savePath + size.Prefix + fileName + fileExtention;
                                    break;
                                case "Finger_":
                                    result.ImageUrlFingerSize = savePath + size.Prefix + fileName + fileExtention;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                return result;
            }


            public static string SaveWebPImage(HttpContextBase context, HttpPostedFileBase file, PictureSize size, string savePath)
            {

                var memoryStream = new MemoryStream();
                file.InputStream.CopyTo(memoryStream);
                var webPImage = memoryStream.ToArray();

                // Get the upload path using the provided HttpContext
                var uploadPath = Path.Combine(context.Server.MapPath(savePath));

                // Check if the directory exists and create it if it does not
                EnsureDirectoryExists(uploadPath);

                // Save the original image
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff"); //Guid.NewGuid().ToString().Replace("-", string.Empty);
                string fileExtention = ".webp";// Path.GetExtension(file.FileName);

                using (var image = new MagickImage(webPImage))
                {
                    // Clone the image to resize
                    using (var resizedImage = image.Clone())
                    {
                        // Resize the image
                        resizedImage.Resize((uint)size.Width, (uint)size.Height);

                        // Set output file name and path                       
                        var outputPath = Path.Combine(savePath, fileName + fileExtention);

                        // Save the resized image
                        resizedImage.Write(uploadPath + size.Prefix + fileName + fileExtention);
                    }

                }

                var result = savePath + size.Prefix + fileName + fileExtention;

                return result;
            }
        }

   
}
