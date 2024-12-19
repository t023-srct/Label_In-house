using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Label_In_house
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
                var MyData = qr.CreateQrCode(txt1.Text, QRCoder.QRCodeGenerator.ECCLevel.H);
                var code = new QRCoder.QRCode(MyData);

                // Set the generated QR code image to the PictureBox
                pictureBox1.Image = code.GetGraphic(50); // Define the size of the QR code
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                // Clear the appData1 dataset
                this.appData1.Clear();

                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the QR code to the MemoryStream
                    var image = code.GetGraphic(50);
                    image.Save(ms, ImageFormat.Png);

                    // Convert MemoryStream to byte[]
                    byte[] qrCodeBytes = ms.ToArray();

                    // Add data to appData1.Qrcode once
                    this.appData1.Qrcode.AddQrcodeRow(txt1.Text, qrCodeBytes );
                }
            }
            catch (Exception ex)                    
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
