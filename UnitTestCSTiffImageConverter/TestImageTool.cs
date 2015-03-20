using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageTool;
using System.Drawing.Imaging;


namespace UnitTestImageTool
{
    [TestClass]
    public class TestImageTool
    {
        //Picture path for UnitTests
        String startupPath = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\pictures\\";

        [TestMethod]
        public void TestResizeOneImage()
        {
            // Test image source
            String[] paths = new String[1];
            paths[0] = startupPath + "testimg.jpg";

            // output size
            ImageFormat outputFormat = ImageFormat.Bmp;

            // convert / resize one image
            String[] resizedFilePath = BmpImageConverter.ConvertImage(paths, outputFormat, false, 1024, 768);

            // get resized image width and height
            Image resizedImageFile = Image.FromFile(resizedFilePath[0]);

            // resized image must be 1024 * 768 pixel
            Assert.AreEqual(resizedImageFile.Size.Width, 1024, "Resized image width not equal to 1024px");
            Assert.AreEqual(resizedImageFile.Size.Height, 768, "Resized image height not equal to 768px");
            Assert.AreEqual(resizedImageFile.RawFormat, outputFormat, "Resized image output format not equal to " + outputFormat);
        }

        [TestMethod]
        public void TestResizeManyImage()
        {

            // array containing 
            String[] paths = new String[2];
            paths[0] = startupPath + "testimg.jpg";
            paths[1] = startupPath + "testimg.jpg";

            // output format
            ImageFormat outputFormat = ImageFormat.Tiff;

            String[] resizedFilePath = BmpImageConverter.ConvertImage(paths, outputFormat, true, 1024, 768);

            
            
            Console.WriteLine("Length : {0}", resizedFilePath.Length);

            /*
            
            // get resized image width and height for last frame
            Image resizedImageFile = Image.FromFile(resizedFilePath[0]);

            

            // resized image must be 1024 * 768 pixel
            Assert.AreEqual(resizedImageFile.Size.Width, BmpImageConverter.width, "Resized image width not equal to " + BmpImageConverter.width + "px");
            Assert.AreEqual(resizedImageFile.Size.Height, BmpImageConverter.height, "Resized image height not equal to " + BmpImageConverter.height + "px");
            Assert.AreEqual(resizedImageFile.RawFormat, outputFormat, "Resized image output format not equal to " + outputFormat);
            */
        }

        [TestMethod]
        public void TestResizeOneImageCroped()
        {
            // array containing 
            String[] paths = new String[1];
            paths[0] = startupPath + "testimg.jpg";

            // output format
            BmpImageConverter.cropImage = true;

            ImageFormat outputFormat = ImageFormat.Gif;

            String[] resizedFilePath = BmpImageConverter.ConvertImage(paths, outputFormat, false, 1024, 768);

            // get resized image width and height for last frame
            Image resizedImageFile = Image.FromFile(resizedFilePath[0]);

            // resized image must be 1024 * 768 pixel
            Assert.AreEqual(resizedImageFile.Size.Width, 1024, "Resized image width not equal to 1024px");
            Assert.AreEqual(resizedImageFile.Size.Height, 768, "Resized image height not equal to 768px");
            Assert.AreEqual(resizedImageFile.RawFormat, outputFormat, "Resized image output format not equal to " + outputFormat);
        }

        [TestMethod]
        public void TestNoResize()
        {
            // array containing 
            String[] paths = new String[1];
            paths[0] = startupPath + "testimg.jpg";


            ImageFormat outputFormat = ImageFormat.Jpeg;

            String[] resizedFilePath = BmpImageConverter.ConvertImage(paths, outputFormat, true);

            // get resized image width and height for last frame
            Image resizedImageFile = Image.FromFile(resizedFilePath[0]);

            // resized image must be 1024 * 768 pixel
            Assert.AreEqual(resizedImageFile.Size.Width, resizedImageFile.Size.Width, "Resized image width not equal to " + resizedImageFile.Size.Width + "px");
            Assert.AreEqual(resizedImageFile.Size.Height, resizedImageFile.Size.Height, "Resized image height not equal to " + resizedImageFile.Size.Height + "px");
            Assert.AreEqual(resizedImageFile.RawFormat, outputFormat, "Resized image output format not equal to " + outputFormat);
        }
    }
}
