using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using NetBarcode.Types;

namespace NetBarcode
{
    public enum BarcodeType
    {
        Codabar,
        Code11,
        Code128,
        Code128A,
        Code128B,
        Code128C,
        Code39,
        Code39E,
        Code93,
        EAN13,
        EAN8
    }

    public enum LabelPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public class Barcode : IBarcode
    {
        private BarcodeSettings _barcodeSettings;

        public Barcode()
        {
            _barcodeSettings = new BarcodeSettings();
        }

        /// <summary>
        /// Set configs
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public Barcode Configure(Action<BarcodeSettings> settings)
        {
            settings(_barcodeSettings);

            return this;
        }

        /// <summary>
        /// Set configs
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public Barcode Configure(BarcodeSettings settings)
        {
            _barcodeSettings = settings;

            return this;
        }

        /// <summary>
        ///     Saves the image to a file.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="path">The file path for the image.</param>
        /// <param name="imageFormat">The image format. Defaults to Png.</param>
        public void SaveImageFile(string data, string path, ImageFormat imageFormat = null)
        {
            var encodedData = GetEncoding(data);
            var image = GenerateImage(encodedData, _barcodeSettings.Text ?? data);

            image.Save(path, imageFormat ?? ImageFormat.Png);
        }

        /// <summary>
        ///     Gets the image as a Base64 encoded string.
        /// <param name="data">The data to encode as a barcode.</param>
        ///     <param name="imageFormat">The image format. Defaults to Png.</param>
        /// </summary>
        public string GetBase64Image(string data, ImageFormat imageFormat = null)
        {
            var encodedData = GetEncoding(data);
            var image = GenerateImage(encodedData, _barcodeSettings.Text ?? data);

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, imageFormat ?? ImageFormat.Png);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        ///     Gets the image as a byte array.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="imageFormat">The image format. Defaults to PNG.</param>
        /// <returns></returns>
        public byte[] GetByteArray(string data, ImageFormat imageFormat = null)
        {
            var encodedData = GetEncoding(data);
            var image = GenerateImage(encodedData, _barcodeSettings.Text ?? data);

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, imageFormat ?? ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        ///     Gets the image.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <returns>
        ///     Image class
        /// </returns>
        public Bitmap GetImage(string data)
        {
            var encodedData = GetEncoding(data);
            return GenerateImage(encodedData, _barcodeSettings.Text ?? data);
        }

        private string GetEncoding(string data)
        {
            IBarcodeBase barcode;

            switch (_barcodeSettings.BarcodeType)
            {
                case BarcodeType.Code128:
                    barcode = new Code128(data);
                    break;
                case BarcodeType.Code128A:
                    barcode = new Code128(data, Code128.Code128Type.A);
                    break;
                case BarcodeType.Code128B:
                    barcode = new Code128(data, Code128.Code128Type.B);
                    break;
                case BarcodeType.Code128C:
                    barcode = new Code128(data, Code128.Code128Type.C);
                    break;
                case BarcodeType.Code11:
                    barcode = new Code11(data);
                    break;
                case BarcodeType.Code39:
                    barcode = new Code39(data);
                    break;
                case BarcodeType.Code39E:
                    barcode = new Code39(data, true);
                    break;
                case BarcodeType.Code93:
                    barcode = new Code93(data);
                    break;
                case BarcodeType.EAN8:
                    barcode = new Ean8(data);
                    break;
                case BarcodeType.EAN13:
                    barcode = new Ean13(data);
                    break;
                case BarcodeType.Codabar:
                    barcode = new Codabar(data);
                    break;
                default:
                    barcode = new Code128(data);
                    break;
            }

            return barcode.GetEncoding();
        }

        private Bitmap GenerateImage(string encodedData, string data)
        {
            var width = _barcodeSettings.BarWidth * encodedData.Length;
            var topLabelAdjustment = 0;

            // Shift drawing down if top label.
            if (_barcodeSettings.ShowLabel && (_barcodeSettings.LabelPosition &
                                               (LabelPosition.TopCenter | LabelPosition.TopLeft |
                                                LabelPosition.TopRight)) > 0)
                topLabelAdjustment = _barcodeSettings.LabelFont.Height;

            var bitmap = new Bitmap(width, _barcodeSettings.BarcodeHeight + (_barcodeSettings.ShowLabel ? _barcodeSettings.LabelFont.Height : 0));
            var shiftAdjustment = 0;

            if (_barcodeSettings.BarWidth <= 0)
                throw new Exception(
                    "EGENERATE_IMAGE-2: Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel)");

            //draw image
            var pos = 0;
            var halfBarWidth = (int) (_barcodeSettings.BarWidth * 0.5);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                //clears the image and colors the entire background
                graphics.Clear(_barcodeSettings.BackgroundColor);

                //lines are fBarWidth wide so draw the appropriate color line vertically
                using (var backpen = new Pen(_barcodeSettings.BackgroundColor, _barcodeSettings.BarWidth))
                {
                    using (var pen = new Pen(_barcodeSettings.LineColor, _barcodeSettings.BarWidth))
                    {
                        while (pos < encodedData.Length)
                        {
                            if (encodedData[pos] == '1')
                            {
                                graphics.DrawLine(pen,
                                    new Point(pos * _barcodeSettings.BarWidth + shiftAdjustment + halfBarWidth, topLabelAdjustment),
                                    new Point(pos * _barcodeSettings.BarWidth + shiftAdjustment + halfBarWidth,
                                        _barcodeSettings.BarcodeHeight + topLabelAdjustment));
                            }
                            pos++;
                        }
                    }
                }
            }

            if (_barcodeSettings.ShowLabel)
            {
                bitmap = InsertLabel(bitmap, data);
            }
            
            bitmap.RotateFlip(_barcodeSettings.Rotate);

            return bitmap;
        }

        private Bitmap InsertLabel(Bitmap image, string data)
        {
            try
            {
                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.DrawImage(image, 0, 0);

                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Near
                    };

                    var labelY = 0;

                    switch (_barcodeSettings.LabelPosition)
                    {
                        case LabelPosition.BottomCenter:
                            labelY = image.Height - _barcodeSettings.LabelFont.Height;
                            stringFormat.Alignment = StringAlignment.Center;
                            break;
                        case LabelPosition.BottomLeft:
                            labelY = image.Height - _barcodeSettings.LabelFont.Height;
                            stringFormat.Alignment = StringAlignment.Near;
                            break;
                        case LabelPosition.BottomRight:
                            labelY = image.Height - _barcodeSettings.LabelFont.Height;
                            stringFormat.Alignment = StringAlignment.Far;
                            break;
                        case LabelPosition.TopCenter:
                            labelY = 0;
                            stringFormat.Alignment = StringAlignment.Center;
                            break;
                        case LabelPosition.TopLeft:
                            labelY = 0;
                            stringFormat.Alignment = StringAlignment.Near;
                            break;
                        case LabelPosition.TopRight:
                            labelY = 0;
                            stringFormat.Alignment = StringAlignment.Far;
                            break;
                    }

                    //color a background color box at the bottom of the barcode to hold the string of data
                    graphics.FillRectangle(new SolidBrush(_barcodeSettings.BackgroundColor),
                        new RectangleF(0, labelY, image.Width, _barcodeSettings.LabelFont.Height));

                    //draw datastring under the barcode image
                    graphics.DrawString(data, _barcodeSettings.LabelFont, new SolidBrush(_barcodeSettings.LineColor),
                        new RectangleF(0, labelY, image.Width, _barcodeSettings.LabelFont.Height), stringFormat);

                    graphics.Save();
                }

                return image;
            }
            catch (Exception ex)
            {
                throw new Exception("ELABEL_GENERIC-1: " + ex.Message);
            }
        }
    }
}