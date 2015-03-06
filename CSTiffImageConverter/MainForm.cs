/****************************** Module Header ******************************\ 
Module Name:    MainForm.cs 
Project:        CSTiffImageConverter
Copyright (c) Microsoft Corporation. 

This sample demonstrates how to convert JPEG images into TIFF images and vice 
versa. This sample also allows to create single multipage TIFF images from 
selected JPEG images.

TIFF (originally standing for Tagged Image File Format) is a flexible, 
adaptable file format for handling images and data within a single file, 
by including the header tags (size, definition, image-data arrangement, 
applied image compression) defining the image's geometry. For example, a 
TIFF file can be a container holding compressed (lossy) JPEG and (lossless) 
PackBits compressed images. A TIFF file also can include a vector-based 
clipping path (outlines, croppings, image frames). 

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;


namespace CSTiffImageConverter
{
    public partial class MainForm : Form
    {
        private OutputFormat selectedFormat;
        private String[] selectedFiles;

        public MainForm()
        {
            InitializeComponent();

            populateLstOutputFormat();
        }

        private void populateLstOutputFormat()
        {
            var dataSource = new List<OutputFormat>();
            dataSource.Add(new OutputFormat() { Name = "BMP (*.BMP)", Value = "bmp", ImageFormat = ImageFormat.Bmp });
            dataSource.Add(new OutputFormat() { Name = "CompuServe GIF (*.GIF)", Value = "gif", ImageFormat = ImageFormat.Gif });
            dataSource.Add(new OutputFormat() { Name = "JPEG (*.JPG, *.JPEG, *.JPE)", Value = "jpg", ImageFormat = ImageFormat.Jpeg });
            dataSource.Add(new OutputFormat() { Name = "TIFF (*.TIF, *.TIFF)", Value = "tiff", ImageFormat = ImageFormat.Tiff });
            

            this.cbOutputFormat.DataSource = dataSource;
            this.cbOutputFormat.DisplayMember = "Name";
            this.cbOutputFormat.ValueMember = "Value";
        }

        private void btnImagePicker_Click(object sender, EventArgs e)
        {
            dlgOpenFileDialog.Multiselect = (selectedFormat.Value.Equals("tiff") && chkIsMultipage.Checked) ? true : false;
            dlgOpenFileDialog.Filter = "Image files (.jpg, .jpeg, .gif, .bmp, .tif, .tiff)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff";

            if (dlgOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFiles = dlgOpenFileDialog.FileNames;
            }
        }

        private void cbOutputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            int selectedIndex = cmb.SelectedIndex;
         
            selectedFormat = (OutputFormat)cmb.SelectedItem;

            chkIsMultipage.Enabled = (selectedFormat.Value.Equals("tiff")) ? true : false;
            
        }

        private void btnConvertImage_Click(object sender, EventArgs e)
        {
            if (selectedFiles != null && selectedFiles[0].Length > 0)
            {
                try
                {
                    ImageConverter ic = new ImageConverter(selectedFormat.ImageFormat);

                    ic.ConvertImage(selectedFiles, chkIsMultipage.Checked);
                    MessageBox.Show("Image conversion completed.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error");
                }
            }
        }

    }
}
