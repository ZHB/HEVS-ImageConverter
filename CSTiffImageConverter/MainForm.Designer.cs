namespace ImageTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnImagePicker = new System.Windows.Forms.Button();
            this.chkIsMultipage = new System.Windows.Forms.CheckBox();
            this.dlgOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblOutputOptions = new System.Windows.Forms.Label();
            this.cbOutputFormat = new System.Windows.Forms.ComboBox();
            this.btnConvertImage = new System.Windows.Forms.Button();
            this.chkResize = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btnImagePicker);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 77);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image to convert";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(6, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(319, 19);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Click on \"Select image\" button to browse the image(s)  to convert";
            // 
            // btnImagePicker
            // 
            this.btnImagePicker.Location = new System.Drawing.Point(70, 45);
            this.btnImagePicker.Name = "btnImagePicker";
            this.btnImagePicker.Size = new System.Drawing.Size(179, 23);
            this.btnImagePicker.TabIndex = 2;
            this.btnImagePicker.Text = "Select image";
            this.btnImagePicker.UseVisualStyleBackColor = true;
            this.btnImagePicker.Click += new System.EventHandler(this.btnImagePicker_Click);
            // 
            // chkIsMultipage
            // 
            this.chkIsMultipage.AutoSize = true;
            this.chkIsMultipage.Checked = true;
            this.chkIsMultipage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsMultipage.Enabled = false;
            this.chkIsMultipage.Location = new System.Drawing.Point(70, 69);
            this.chkIsMultipage.Name = "chkIsMultipage";
            this.chkIsMultipage.Size = new System.Drawing.Size(216, 17);
            this.chkIsMultipage.TabIndex = 1;
            this.chkIsMultipage.Text = "Check to create multipage tiff (single) file";
            this.chkIsMultipage.UseVisualStyleBackColor = true;
            // 
            // dlgOpenFileDialog
            // 
            this.dlgOpenFileDialog.Filter = "Image files (.jpg, .jpeg, .tif)|*.jpg;*.jpeg;*.tif;*.tiff";
            this.dlgOpenFileDialog.Multiselect = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkResize);
            this.groupBox3.Controls.Add(this.lblOutputOptions);
            this.groupBox3.Controls.Add(this.cbOutputFormat);
            this.groupBox3.Controls.Add(this.chkIsMultipage);
            this.groupBox3.Location = new System.Drawing.Point(12, 95);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(331, 115);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output options";
            // 
            // lblOutputOptions
            // 
            this.lblOutputOptions.AutoSize = true;
            this.lblOutputOptions.Location = new System.Drawing.Point(6, 16);
            this.lblOutputOptions.Name = "lblOutputOptions";
            this.lblOutputOptions.Size = new System.Drawing.Size(175, 13);
            this.lblOutputOptions.TabIndex = 3;
            this.lblOutputOptions.Text = "Selecte disired image output format ";
            // 
            // cbOutputFormat
            // 
            this.cbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputFormat.FormattingEnabled = true;
            this.cbOutputFormat.Location = new System.Drawing.Point(70, 42);
            this.cbOutputFormat.Name = "cbOutputFormat";
            this.cbOutputFormat.Size = new System.Drawing.Size(179, 21);
            this.cbOutputFormat.TabIndex = 0;
            this.cbOutputFormat.SelectedIndexChanged += new System.EventHandler(this.cbOutputFormat_SelectedIndexChanged);
            // 
            // btnConvertImage
            // 
            this.btnConvertImage.Location = new System.Drawing.Point(82, 225);
            this.btnConvertImage.Name = "btnConvertImage";
            this.btnConvertImage.Size = new System.Drawing.Size(179, 23);
            this.btnConvertImage.TabIndex = 3;
            this.btnConvertImage.Text = "Convert";
            this.btnConvertImage.UseVisualStyleBackColor = true;
            this.btnConvertImage.Click += new System.EventHandler(this.btnConvertImage_Click);
            // 
            // chkResize
            // 
            this.chkResize.AutoSize = true;
            this.chkResize.Checked = true;
            this.chkResize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResize.Location = new System.Drawing.Point(70, 92);
            this.chkResize.Name = "chkResize";
            this.chkResize.Size = new System.Drawing.Size(139, 17);
            this.chkResize.TabIndex = 5;
            this.chkResize.Text = "Resize to 1024 * 768 px";
            this.chkResize.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 261);
            this.Controls.Add(this.btnConvertImage);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MultiFormatImageConverter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chkIsMultipage;
        private System.Windows.Forms.Button btnImagePicker;
        private System.Windows.Forms.OpenFileDialog dlgOpenFileDialog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbOutputFormat;
        private System.Windows.Forms.Button btnConvertImage;
        private System.Windows.Forms.Label lblOutputOptions;
        private System.Windows.Forms.CheckBox chkResize;

    }
}

