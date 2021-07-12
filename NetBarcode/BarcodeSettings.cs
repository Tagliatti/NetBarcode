using System.Drawing;

namespace NetBarcode
{
    public class BarcodeSettings
    {
        public Color LineColor { get; set; } = Color.Black;
        public Color BackgroundColor { get; set; } = Color.White;
        public int BarWidth { get; set; } = 2;
        public int BarcodeHeight { get; set; } = 150;
        public bool ShowLabel { get; set; } = true;
        public Font LabelFont { get; set; } = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
        public LabelPosition LabelPosition { get; set; } = LabelPosition.BottomCenter;
        public BarcodeType BarcodeType { get; set; } = BarcodeType.Code128;
        public string Text { get; set; }
        public RotateFlipType Rotate { get; set; } = RotateFlipType.RotateNoneFlipNone;
    }
}