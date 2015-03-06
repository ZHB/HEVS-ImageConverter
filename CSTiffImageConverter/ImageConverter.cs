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


namespace CSTiffImageConverter
{
    public class ImageConverter
    {
        private ImageFormat outputFormat;

        public ImageConverter(ImageFormat outputFormat)
        {
            this.outputFormat = outputFormat;
        }

        /// <summary>
        /// Resize an image to given width and height
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
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            //a holder for the result
            Bitmap bitmap = new Bitmap(width, height);

            //set the resolutions the same to avoid cropping due to resolution differences
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);


            //use a graphics object to draw the resized image into the bitmap
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
            }

            //return the resulting bitmap
            return bitmap;
        }

        private string[] saveImage(Image image, string[] fileNames)
        {
            FrameDimension frameDimensions = new FrameDimension(image.FrameDimensionsList[0]);

            // Gets the number of pages from the tiff image (if multipage)
            int frameNum = image.GetFrameCount(frameDimensions);
            string[] imagePaths = new string[frameNum];

            for (int frame = 0; frame < frameNum; frame++)
            {
                // Selects one frame at a time and save as jpeg.
                image.SelectActiveFrame(frameDimensions, frame);
                using (Bitmap bmp = new Bitmap(image))
                {
                    imagePaths[frame] = String.Format("{0}\\{1}{2}." + outputFormat.ToString().ToLowerInvariant(),
                        Path.GetDirectoryName(fileNames[0]),
                        Path.GetFileNameWithoutExtension(fileNames[0]),
                        frame);
                    bmp.Save(imagePaths[frame], outputFormat);
                }
            }

            return imagePaths;
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
        public string[] ConvertImage(string[] fileNames, bool isMultipage)
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
                                tiffPaths[i] = String.Format("{0}\\{1}.tif",
                                    Path.GetDirectoryName(fileNames[i]),
                                    Path.GetFileNameWithoutExtension(fileNames[i]));

                                // Initialize the first frame of multipage tiff.
                                tiffImg = Image.FromFile(fileNames[i]);
                                encoderParams.Param[0] = new EncoderParameter(
                                    Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                                tiffImg.Save(tiffPaths[i], tiffCodecInfo, encoderParams);
                            }
                            else
                            {
                                // Add additional frames.
                                encoderParams.Param[0] = new EncoderParameter(
                                    Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);
                                using (Image frame = Image.FromFile(fileNames[i]))
                                {
                                    tiffImg.SaveAdd(frame, encoderParams);
                                }
                            }

                            if (i == fileNames.Length - 1)
                            {
                                // When it is the last frame, flush the resources and closing.
                                encoderParams.Param[0] = new EncoderParameter(
                                    Encoder.SaveFlag, (long)EncoderValue.Flush);
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
                     

                    return null;
                }
                else
                {
                    return saveImage(imageFile, fileNames);

                    /*
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
                            bmp.Save(imagePaths[frame], outputFormat);
                        }
                    }
                    

                    return imagePaths;
                     */
                }
            }
        }
    }
}