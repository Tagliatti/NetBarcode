using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

using Xunit;

namespace NetBarcode.Tests
{
    public class BarcodeTest
    {
        [Fact]
        public void GenerateBarcode_Label_Customized()
        {
            var barcode = new Barcode(data: "40992012319999999999", 
                type: BarcodeType.Code128C, 
                showLabel: true,
                width: 440,
                height: 200,
                labelFont: new Font("Verdana", 18, FontStyle.Bold),
                label: "40-99-201231-99-9999-9999");
            var file = @"c:\temp\barcode128c.png";
            barcode.SaveImageFile(file, ImageFormat.Png, 96);
        }
    }
}
