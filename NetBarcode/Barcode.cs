using NetBarcode.Types;
using System;
using System.IO;
using System.Linq;
using SixLabors.Fonts;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Pbm;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tga;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;

namespace NetBarcode
{
    public enum Type
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
        EAN8,
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

    public enum AlignmentPosition
    {
        Center,
        Left,
        Right
    }

    public enum ImageFormat
    {
        Bmp,
        Gif,
        Jpeg,
        Pbm,
        Png,
        Tga,
        Tiff,
        Webp,
    }

    public class Barcode
    {
        private readonly string _data;
        private readonly Type _type = Type.Code128;
        private string _encodedData;
        private readonly Color _foregroundColor = Color.Black;
        private readonly Color _backgroundColor = Color.White;
        private int _width = 300;
        private int _height = 150;
        private readonly bool _autoSize = true;
        private readonly bool _showLabel = false;

        private readonly Font _labelFont =
            SystemFonts.CreateFont(SystemFonts.Collection.Families.First().Name, 10, FontStyle.Bold);

        private readonly FontCollection _fontCollection = new FontCollection();
        private readonly LabelPosition _labelPosition = LabelPosition.BottomCenter;
        private readonly AlignmentPosition _alignmentPosition = AlignmentPosition.Center;

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode"/> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        public Barcode(string data)
        {
            _data = data;
            _type = Type.Code128;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        public Barcode(string data, Type type)
        {
            _data = data;
            _type = type;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        public Barcode(string data, bool showLabel)
        {
            _data = data;
            _showLabel = showLabel;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, Font labelFont)
        {
            _data = data;
            _showLabel = showLabel;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        public Barcode(string data, Type type, bool showLabel)
        {
            _data = data;
            _type = type;
            _showLabel = showLabel;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, Type type, bool showLabel, Font labelFont)
        {
            _data = data;
            _type = type;
            _showLabel = showLabel;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        public Barcode(string data, int width, int height)
        {
            _autoSize = false;
            _data = data;
            _width = width;
            _height = height;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        public Barcode(string data, bool showLabel, int width, int height)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, int width, int height, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        public Barcode(string data, Type type, bool showLabel, int width, int height)
        {
            _autoSize = false;
            _data = data;
            _type = type;
            _showLabel = showLabel;
            _width = width;
            _height = height;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, Type type, bool showLabel, int width, int height, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _type = type;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition)
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _alignmentPosition = alignmentPosition;
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        /// <param name="backgroundColor">Color of the background. Defaults to white.</param>
        /// <param name="foregroundColor">Color of the foreground. Defaults to black.</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition, Color backgroundColor, Color foregroundColor)
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        /// <param name="backgroundColor">Color of the background. Defaults to white.</param>
        /// <param name="foregroundColor">Color of the foreground. Defaults to black.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition, Color backgroundColor, Color foregroundColor, Font labelFont)
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
            _labelFont = labelFont;

            InitializeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode" /> class.
        /// </summary>
        /// <param name="data">The data to encode as a barcode.</param>
        /// <param name="type">The type of barcode. Defaults to Code128</param>
        /// <param name="showLabel">if set to <c>true</c> show the data as a label. Defaults to false.</param>
        /// <param name="width">The width in pixels. Defaults to 300.</param>
        /// <param name="height">The height in pixels. Defaults to 150.</param>
        /// <param name="labelPosition">The label position. Defaults to bottom-center.</param>
        /// <param name="alignmentPosition">The alignment position. Defaults to center.</param>
        /// <param name="backgroundColor">Color of the background. Defaults to white.</param>
        /// <param name="foregroundColor">Color of the foreground. Defaults to black.</param>
        /// <param name="labelFont">The label font. Defaults to Font("Microsoft Sans Serif", 10, FontStyle.Bold)</param>
        public Barcode(string data, Type type, bool showLabel, int width, int height, LabelPosition labelPosition,
            AlignmentPosition alignmentPosition, Color backgroundColor, Color foregroundColor, Font labelFont)
        {
            _autoSize = false;
            _data = data;
            _type = type;
            _showLabel = showLabel;
            _width = width;
            _height = height;
            _labelPosition = labelPosition;
            _alignmentPosition = alignmentPosition;
            _backgroundColor = backgroundColor;
            _foregroundColor = foregroundColor;
            _labelFont = labelFont;

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
                case Type.Code128A:
                    barcode = new Code128(_data, Code128.Code128Type.A);
                    break;
                case Type.Code128B:
                    barcode = new Code128(_data, Code128.Code128Type.B);
                    break;
                case Type.Code128C:
                    barcode = new Code128(_data, Code128.Code128Type.C);
                    break;
                case Type.Code11:
                    barcode = new Code11(_data);
                    break;
                case Type.Code39:
                    barcode = new Code39(_data);
                    break;
                case Type.Code39E:
                    barcode = new Code39(_data, true);
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
                case Type.Codabar:
                    barcode = new Codabar(_data);
                    break;
                default:
                    barcode = new Code128(_data);
                    break;
            }

            _encodedData = barcode.GetEncoding();
        }

        /// <summary>
        /// Saves the image to a file.
        /// </summary>
        /// <param name="path">The file path for the image.</param>
        /// <param name="imageFormat">The image format. Defaults to Jpeg.</param>
        public void SaveImageFile(string path, ImageFormat imageFormat = ImageFormat.Jpeg)
        {
            using (var image = GenerateImage())
                image.Save(path, getImageEncoder(imageFormat));
        }

        /// <summary>
        /// Gets the image in PNG format as a Base64 encoded string.
        /// </summary>
        public string GetBase64Image()
        {
            using (var image = GenerateImage())
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, getImageEncoder(ImageFormat.Png));
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Gets the image in PNG format as a byte array.
        /// </summary>
        public byte[] GetByteArray()
        {
            using (var image = GenerateImage())
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, getImageEncoder(ImageFormat.Png));
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Gets the image as a byte array.
        /// </summary>
        /// <param name="imageFormat">The image format. Defaults to PNG.</param>
        /// <returns></returns>
        public byte[] GetByteArray(ImageFormat imageFormat)
        {
            using (var image = GenerateImage())
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, getImageEncoder(imageFormat));
                return memoryStream.ToArray();
            }
        }

        private IImageEncoder getImageEncoder(ImageFormat imageFormat)
        {
            if (imageFormat == ImageFormat.Bmp)
            {
                return new BmpEncoder();
            }

            if (imageFormat == ImageFormat.Gif)
            {
                return new GifEncoder();
            }

            if (imageFormat == ImageFormat.Jpeg)
            {
                return new JpegEncoder();
            }

            if (imageFormat == ImageFormat.Pbm)
            {
                return new PbmEncoder();
            }

            if (imageFormat == ImageFormat.Png)
            {
                return new PngEncoder();
            }

            if (imageFormat == ImageFormat.Tga)
            {
                return new TgaEncoder();
            }

            if (imageFormat == ImageFormat.Tiff)
            {
                return new TiffEncoder();
            }

            if (imageFormat == ImageFormat.Webp)
            {
                return new WebpEncoder();
            }

            throw new ArgumentException();
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>
        /// Image class
        /// </returns>
        public Image GetImage()
        {
            return GenerateImage();
        }

        private Image GenerateImage()
        {
            const int barWidth = 2;
            const int aspectRatio = 2;

            if (_width == 0 || _autoSize)
            {
                _width = barWidth * _encodedData.Length;
            }

            if (_autoSize)
            {
                _height = _width / aspectRatio;
            }

            var topLabelAdjustment = 0F;
            
            var textOptions = new TextOptions(_labelFont)
            {
                Dpi = 200,
            };
            var labelSize = TextMeasurer.Measure(_data, textOptions);

            if (_showLabel)
            {
                topLabelAdjustment = labelSize.Height;
            }

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
            var halfBarWidth = (int)(iBarWidth * 0.5);

            var image = new Image<Rgba32>(_width, _height);
            image.Mutate(imageContext =>
            {
                //clears the image and colors the entire background
                imageContext.BackgroundColor(_backgroundColor);

                //lines are fBarWidth wide so draw the appropriate color line vertically
                var pen = new Pen(_foregroundColor, iBarWidth / iBarWidthModifier);
                var drawingOptions = new DrawingOptions
                {
                    GraphicsOptions = new GraphicsOptions
                    {
                        Antialias = true,
                        AlphaCompositionMode = PixelAlphaCompositionMode.Src,
                    }
                };

                while (pos < _encodedData.Length)
                {
                    if (_encodedData[pos] == '1')
                    {
                        imageContext.DrawLines(drawingOptions, pen,
                            new PointF(pos * iBarWidth + shiftAdjustment + halfBarWidth, 0),
                            new PointF(pos * iBarWidth + shiftAdjustment + halfBarWidth, _height - topLabelAdjustment)
                        );
                    }

                    pos++;
                }
            });

            if (_showLabel)
            {
                var labelY = 0;
                var labelX = 0;
                
                switch (_labelPosition)
                {
                    case LabelPosition.TopCenter:
                    case LabelPosition.BottomCenter:
                        labelY = image.Height - ((int)labelSize.Height);
                        labelX = _width / 2;
                        textOptions.HorizontalAlignment = HorizontalAlignment.Center;
                        break;
                    case LabelPosition.TopLeft:
                    case LabelPosition.BottomLeft:
                        labelY = image.Height - ((int)labelSize.Height);
                        labelX = 0;
                        textOptions.HorizontalAlignment = HorizontalAlignment.Left;
                        break;
                    case LabelPosition.TopRight:
                    case LabelPosition.BottomRight:
                        labelY = image.Height - ((int)labelSize.Height);
                        labelX = _width - (int) labelSize.Width;
                        textOptions.HorizontalAlignment = HorizontalAlignment.Left;
                        break;
                }

                textOptions.Origin = new Point(labelX, labelY);

                image.Mutate(x=> x.DrawText(textOptions, _data, _foregroundColor));
            }

            return image;
        }
    }
}