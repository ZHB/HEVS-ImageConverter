/****************************** Module Header ******************************\ 
Module Name:    TiffImageConverter.cs 
Project:        CSTiffImageConverter
Copyright (c) Microsoft Corporation. 

The class defines the helper methods for converting TIFF from or to JPEG 
file(s)

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
\***************************************************************************/

using System;
using System.IO;
using System.Linq;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace ImageTool
{
    public static class BmpImageConverter
    {
        public static Boolean cropImage { get; set; }

        /// <summary>
        /// Resize an image
        /// </summary>
        /// <param name="image">
        /// Image to resize
        /// </param>
        /// <param name="cropImage">
        /// Crop image or not 
        /// </param>
        /// <returns>
        /// Resized image
        /// </returns>
        public static Image ResizeImage(Image image, Boolean cropImage = false, int width = 0, int height = 0)
        {
            width = (width <= 0) ? image.Size.Width : width;
            height = (height <= 0) ? image.Size.Height : height;

            return ResizeImage(image, new Size(width, height), cropImage);
        }

        /// <summary>
        /// Resize an image to given width and height
        /// </summary>
        /// <param name="image">
        /// Image to resize
        /// </param>
        /// <param name="destinationSize">
        /// Desired output width and height
        /// </param>
        /// <param name="cropImage">
        /// Crop image or not
        /// </param>
        /// <returns>
        /// Resized image
        /// </returns>
        public static Image ResizeImage(Image image, Size destinationSize, Boolean cropImage = false)
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            //how many units are there to make the original length
            double hRatio = (double)originalHeight / destinationSize.Height;
            double wRatio = (double)originalWidth / destinationSize.Width;

            //get the shorter side
            //double ratio = Math.Min(hRatio, wRatio);
            double ratio = cropImage ? Math.Min(hRatio, wRatio) : Math.Max(hRatio, wRatio);

            int hScale = Convert.ToInt32(destinationSize.Height * ratio);
            int wScale = Convert.ToInt32(destinationSize.Width * ratio);

            //start cropping from the center
            int startX = (originalWidth - wScale) / 2;
            int startY = (originalHeight - hScale) / 2;

            //crop the image from the specified location and size
            Rectangle sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

            //the future size of the image
            Image bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);

            //fill-in the whole bitmap
            Rectangle destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            //generate the new image
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }

            return bitmap;
        }

        /// <summary>
        /// Converts image(s) in desired format
        /// </summary>
        /// <param name="fileNames">
        /// String array having full name to image(s).
        /// </param>
        /// <param name="outputFormat">
        /// Desired image(s) ouput format
        /// </param>
        /// <param name="isMultipage">
        /// true to create single multipage tiff file otherwise, false.
        /// </param>
        /// <returns>
        /// String array having full name to images.
        /// </returns>
        public static string[] ConvertImage(string[] fileNames, ImageFormat outputFormat, Boolean isMultipage, int outputWidth = 0, int outputHeight = 0)
        {
            using (Image imageFile = Image.FromFile(fileNames[0]))
            {
             
                // is output TIFF image and Multipage ?
                if(isMultipage && outputFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                {
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    ImageCodecInfo tiffCodecInfo = ImageCodecInfo.GetImageEncoders()
                        .First(ie => ie.MimeType == "image/tiff");

                    string[] tiffPaths = null; 

                    tiffPaths = new string[1];
                    Image tiffImg = null;
                    try
                    {
                        for (int i = 0; i < fileNames.Length; i++)
                        {
                            if (i == 0)
                            {
                                tiffPaths[i] = String.Format("{0}\\{1}.tiff",
                                    Path.GetDirectoryName(fileNames[i]),
                                    Path.GetFileNameWithoutExtension(fileNames[i]));

                                // Initialize the first frame of multipage tiff.
                                tiffImg = Image.FromFile(fileNames[i]);
                                encoderParams.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.MultiFrame);

                                // resize image
                                tiffImg = BmpImageConverter.ResizeImage(tiffImg, cropImage, outputWidth, outputHeight);

                                tiffImg.Save(tiffPaths[i], tiffCodecInfo, encoderParams);
                            }
                            else
                            {
                                // Add additional frames.
                                encoderParams.Param[0] = new EncoderParameter(
                                    Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);
                                using (Image frame = Image.FromFile(fileNames[i]))
                                {
                                    // resize image
                                    Image frame2 = BmpImageConverter.ResizeImage(frame, cropImage, outputWidth, outputHeight);

                                    tiffImg.SaveAdd(frame2, encoderParams);
                                }
                            }

                            if (i == fileNames.Length - 1)
                            {
                                // When it is the last frame, flush the resources and closing.
                                encoderParams.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.Flush);
                                tiffImg.SaveAdd(encoderParams);
                            }
                        }
                    }
                    finally
                    {
                        if (tiffImg != null)
                        {
                            tiffImg.Dispose();
                            tiffImg = null;
                        }
                    }

                    return tiffPaths;
                }
                else
                {
                    
                    FrameDimension frameDimensions = new FrameDimension(imageFile.FrameDimensionsList[0]);

                    // Gets the number of pages from the tiff image (if multipage)
                    int frameNum = imageFile.GetFrameCount(frameDimensions);
                    string[] imagePaths = new string[frameNum];


                    for (int frame = 0; frame < frameNum; frame++)
                    {
                        // Selects one frame at a time and save as jpeg.
                        imageFile.SelectActiveFrame(frameDimensions, frame);
                        using (Bitmap bmp = new Bitmap(imageFile))
                        {
                            imagePaths[frame] = String.Format("{0}\\{1}{2}." + outputFormat.ToString().ToLowerInvariant(),
                                Path.GetDirectoryName(fileNames[0]),
                                Path.GetFileNameWithoutExtension(fileNames[0]),
                                frame);

                            
                            // resize image
                            Image img = BmpImageConverter.ResizeImage(bmp, cropImage, outputWidth, outputHeight);

                            // save image
                            img.Save(imagePaths[frame], outputFormat);
                        }
                    }
                    

                    return imagePaths;   
                }
            }
        }
    }
}