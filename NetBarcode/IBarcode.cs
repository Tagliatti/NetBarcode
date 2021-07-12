using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace NetBarcode
{
    public interface IBarcode
    {
        void SaveImageFile(string data, string path, ImageFormat imageFormat = null);
        string GetBase64Image(string data, ImageFormat imageFormat = null);
        byte[] GetByteArray(string data, ImageFormat imageFormat = null);
        Bitmap GetImage(string data);
        Barcode Configure(Action<BarcodeSettings> barcodeSettings);
        Barcode Configure(BarcodeSettings barcodeSettings);
    }
}