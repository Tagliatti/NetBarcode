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
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALQAAABaCAYAAAARg3zAAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABR0lEQVR4nO3S0QqCQBQA0RX67+rLzYeCilJ7qGA4B2RRlvWyzGGMcVqe4/Kcb+s8z8dpmh7ex9W778/u963tfz5v6/y1/d+c99M5f3nu1j29W1/971/3sGfuPfMeBoQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KRcALf9h2i8y01OAAAAAElFTkSuQmCC";
            Assert.Equal(expected, base64);
        }

        [Fact]
        public void GetImage_WithLabel()
        {
            var barcode = new Barcode("Hello", true);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALQAAABaCAYAAAARg3zAAAAACXBIWXMAAA7EAAAOxAGVKw4bAAADAUlEQVR4nO3YsUsycRzH8W+UhPgYza0GTdEkOIpT9Bc0iXsRTo72uDY9Q4urq5OTKfpMEQ9IPIH/gPtDkIhTg48/0bBT705Cgw/vFxzn3f08f55v5HTPzH6Ol9vxUpqtR6PR7c7Ozqdtm1q132t+nN947/mCzu83fpPzXXee2zxv0HVatV72et91HcLMO8x89wwQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQQtCQ4oL+M15+eda2ZNsC9pvPOL/xYV83zPhNznfdeXqf92O8/HbbV1dXlsvlLJlMfjpPPp932//Gy9/pOux8w76fMJ/zpq/DV+YdON+90WhUnx7Iz6+X7De//V6rzhs0Luj8IcZvZL7rztP7vEKhYP1+38rlcubk5MTOz88Xznt2dmYPDw/3buzd3d39mvMN+358P+dNXwdbs4d158stB6QQ9BY9Pz9bqVSy19fXyfb19bXVajWLRCJWr9cXxl9eXlqj0bCDgwNrt9t2fHxs8EfQWzQYDKzX69n7+/tku9VqWbVatWKxaC8vLwvjm82muwWxm5sb63a7BB0CQW9ROp1299D29PT0sS+bzbr7QDs6Olr6nHg8bru7u4ZwCPqbVSoVe3x8tP39fcPXEfQ3Oj09nXxDv729WSaTWXo8lUpNHicSCUMwgt4S92PQ3Vo4nU7HotGoXVxcTO6r3Y/CWCw2+aZ2+2drF/lwOPw4jmAEvSUu0Bn3r8XM4eHhx2N3vzy/9h5HMIKGFIKGFIKGFIKGFIKGFIKGFIKGFIKGFIKGFIKGFIKGFIKGFIKGFIKGlP+e0pwFTB7DJQAAAABJRU5ErkJggg==";
            Assert.Equal(expected, base64);
        }

        [Fact]
        public void Code39()
        {
            var barcode = new Barcode("HELLO", Type.Code39);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            // _output.WriteLine(base64);
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALQAAABaCAYAAAARg3zAAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABOklEQVR4nO3SQQqDMBQA0Qjeu/Xkqe3KTUGLmw7vQRBD4BuZdYzx3NdjX9uc8/38WJZlO+4f36/u/3ru2/fcPefq/f/tflfn3DH/7H+8u7d1QIigSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETYqgSRE0KYImRdCkCJoUQZMiaFIETcoLN1W6aHI69BgAAAAASUVORK5CYII=";
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
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOgAAAB0CAYAAACPDX5AAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABpUlEQVR4nO3TywqDMAAAQQX/u/XL7ePkRWjaHLYwA0EMkZjAbsuy3J/j9hz7cRyv59u6rvt5/vw+Ov/tuqv/mb3P6Pn/7Xyj+8zY/9N7vFr36/cz72n2uUfmtwXIEiiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwh6v+O7QaiyIlAAAAABJRU5ErkJggg==";
            Assert.Equal(expected, base64);
        }
    }
}