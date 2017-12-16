using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using NetBarcode.Types;

namespace NetBarcode
{
    public enum Type
    {
        Code11,
        Code128,
        Code39,
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
    };

    public enum AlignmentPosition
    {
        Center,
        Left,
        Right
    };

    public class Barcode
    {
        private readonly string _data;
        private readonly Type _type;
        private string _encodedData;
        private readonly Color _foregroundColor = Color.Black;
        private readonly Color _backgroundColor = Color.White;
        private int _width = 300;
        private int _height = 150;
        private readonly bool _autoSize = true;
        private readonly bool _showLabel = false;
        private readonly Font _labelFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        private readonly LabelPosition _labelPosition = LabelPosition.BottomCenter;
        private readonly AlignmentPosition _alignmentPosition = AlignmentPosition.Center;
        
        public Barcode(string data)
        {
            _data = data;
            _type = Type.Code128;
            
            InitializeType();
        }

        public Barcode(string data, Type type)
        {
            _data = data;
            _type = type;
            
            InitializeType();
        }

        public Barcode(string data, bool showLabel)
        {
            _data = data;
            _showLabel = showLabel;
            
            InitializeType();
        }

        public Barcode(string data, Type type, bool showLabel)
        {
            _data = data;
            _type = type;
            _showLabel = showLabel;
            
            InitializeType();
        }

        public Barcode(string data, int width, int height)
        {
            _autoSize = false;
            _data = data;
            _width = width;
            _height = height;
            
            InitializeType();
        }

        public Barcode(string data, bool showLabel, int width, int height)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            
            InitializeType();
        }

        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            
            InitializeType();
        }

        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition, AlignmentPosition alignmentPosition)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _alignmentPosition = alignmentPosition;
            
            InitializeType();
        }

        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition, AlignmentPosition alignmentPosition, Color backgroundColor, Color foregroundColor)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _alignmentPosition = alignmentPosition;
            _backgroundColor = backgroundColor;
            _foregroundColor = foregroundColor;
            
            InitializeType();
        }

        private void InitializeType()
        {
            IBarcode barcode;

            switch (_type)
            {
                case Type.Code128:
                    barcode = new Code128(_data);
                    break;
                case Type.Code11:
                    barcode = new Code11(_data);
                    break;
                case Type.Code39:
                    barcode = new Code39(_data);
                    break;
                case Type.Code93:
                    barcode = new Code93(_data);
                    break;
                case Type.EAN8:
                    barcode = new EAN8(_data);
                    break;
                case Type.EAN13:
                    barcode = new EAN13(_data);
                    break;
                default:
                    barcode = new Code128(_data);
                    break;
            }

            _encodedData = barcode.GetEncoding();
        }

        public void SaveImageFile(string path, ImageFormat imageFormat = null)
        {
            var image = GenerateImage();

            image.Save(path, imageFormat ?? ImageFormat.Jpeg);
        }

        public string GetBase64Image()
        {
            var image = GenerateImage();
            
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        private Image GenerateImage()
        {
            const int barWidth = 2;
            const int aspectRatio = 2;

            if (_autoSize)
            {
                _width = barWidth * _encodedData.Length;
    
                _height = _width / aspectRatio;
            }

            var topLabelAdjustment = 0;

            if (_showLabel)
            {
                // Shift drawing down if top label.
                if ((_labelPosition & (LabelPosition.TopCenter | LabelPosition.TopLeft | LabelPosition.TopRight)) > 0)
                    topLabelAdjustment = _labelFont.Height;

                _height -= _labelFont.Height;
            }

            var bitmap = new Bitmap(_width, _height);
            var iBarWidth = _width / _encodedData.Length;
            var shiftAdjustment = 0;
            var iBarWidthModifier = 1;

            switch (_alignmentPosition)
            {
                case AlignmentPosition.Center:
                    shiftAdjustment = (_width % _encodedData.Length) / 2;
                    break;
                case AlignmentPosition.Left:
                    shiftAdjustment = 0;
                    break;
                case AlignmentPosition.Right:
                    shiftAdjustment = (_width % _encodedData.Length);
                    break;
                default:
                    shiftAdjustment = (_width % _encodedData.Length) / 2;
                    break;
            }

            if (iBarWidth <= 0)
                throw new Exception(
                    "EGENERATE_IMAGE-2: Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel)");

            //draw image
            var pos = 0;
            var halfBarWidth = (int) (iBarWidth * 0.5);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                //clears the image and colors the entire background
                graphics.Clear(_backgroundColor);

                //lines are fBarWidth wide so draw the appropriate color line vertically
                using (var backpen = new Pen(_backgroundColor, iBarWidth / iBarWidthModifier))
                {
                    using (var pen = new Pen(_foregroundColor, iBarWidth / iBarWidthModifier))
                    {
                        while (pos < _encodedData.Length)
                        {
                            if (_encodedData[pos] == '1')
                            {
                                graphics.DrawLine(pen,
                                    new Point(pos * iBarWidth + shiftAdjustment + halfBarWidth, topLabelAdjustment),
                                    new Point(pos * iBarWidth + shiftAdjustment + halfBarWidth,
                                        _height + topLabelAdjustment));
                            }
                            pos++;
                        }
                    }
                }
            }

            var image = (Image) bitmap;

            if (_showLabel)
            {
                image = InsertLabel(image);
            }
            return image;
        }

        private Image InsertLabel(Image image)
        {
            try
            {
                using (var g = Graphics.FromImage(image))
                {
                    g.DrawImage(image, 0, 0);

                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Near
                    };

                    var labelY = 0;

                    switch (_labelPosition)
                    {
                        case LabelPosition.BottomCenter:
                            labelY = image.Height - (_labelFont.Height);
                            stringFormat.Alignment = StringAlignment.Center;
                            break;
                        case LabelPosition.BottomLeft:
                            labelY = image.Height - (_labelFont.Height);
                            stringFormat.Alignment = StringAlignment.Near;
                            break;
                        case LabelPosition.BottomRight:
                            labelY = image.Height - (_labelFont.Height);
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
                    g.FillRectangle(new SolidBrush(_backgroundColor),
                        new RectangleF(0, labelY, image.Width, _labelFont.Height));

                    //draw datastring under the barcode image
                    g.DrawString(_data, _labelFont, new SolidBrush(_foregroundColor),
                        new RectangleF(0, labelY, image.Width, _labelFont.Height), stringFormat);

                    g.Save();
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