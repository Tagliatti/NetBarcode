using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using Xunit.Abstractions;

namespace NetBarcode.Tests
{
    public class BarcodeTests
    {
        private readonly ITestOutputHelper _output;
        public BarcodeTests(ITestOutputHelper output)
            => _output = output;

        [Fact]
        public void GetImage()
        {
            var barcode = new Barcode("Hello");
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            _output.WriteLine(base64);
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALQAAABaCAYAAAARg3zAAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABYUlEQVR4nO3UAQqCQBQAUYPuf2UTKgjJ1iILhvdAFmVZPzJ4nq7m5Trd13lxWjze3/ZNW8/XHve92r8+b3T+q/1HzvvunL88d/SdttZn7/vXd9gz9555z7ebacc6DZ6vjc4Z7ft0/1Hzvjvnr8/d+95P12/Pe8jc9z80JAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJkXQpAiaFEGTImhSBE2KoEkRNCmCJuUCdO3FKq0nLL8AAAAASUVORK5CYII=";
            Assert.Equal(expected, base64);
        }

        [Fact]
        public void GetImage_WithLabel()
        {
            var barcode = new Barcode("Hello", true);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            _output.WriteLine(base64);
            var expected = "data:image/png;base64,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";
            Assert.Equal(expected, base64);
        }

        [Fact]
        public void Code39()
        {
            var barcode = new Barcode("HELLO", Type.Code39);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            _output.WriteLine(base64);
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALQAAABaCAYAAAARg3zAAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABSUlEQVR4nO3UAQeDQBiA4aL//5dbrJExdnM2ez0PyZ34rrzalrv9uNb9cK6X9XDdv65H9z997tV5Zs8Zff9/e7/ROTPmv/sdZ/e2nZvL5b48rX99//Z5qvO+NWd03tRzPf7QkCBoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImpQbz6sNJPg2jg0AAAAASUVORK5CYII=";
            Assert.Equal(expected, base64);
        }

        [Fact]
        public void Code39E()
        {
            var barcode = new Barcode("HELLO!", Type.Code39E);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            _output.WriteLine(base64);
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOgAAAB0CAYAAACPDX5AAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABwElEQVR4nO3VAYrCMBQA0Qi9/5Xdgi4UQdisUUd5D6Q0VH9SGNzGxXn/nM676/047Y7rx/vZ9f8+d28/q+fMnv/Tzjc7Z8X8v77He889+v2V72n1uWfWt+viOFzHzf27r6/ez7fOe9Wc2Xljcv3Z53r095bu5/cfFAgSKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiE/QARW1d2MRzOAwAAAABJRU5ErkJggg==";
            Assert.Equal(expected, base64);
        }
    }
}