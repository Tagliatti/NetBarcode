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
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALQAAABaCAYAAAARg3zAAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABc0lEQVR4nO3SUYrCMBRA0RRcmLMylzazM+enBSlaMx9O4XIOSDE8ktDeyxjjuv5+tuf9fv9eluXr8f9YvVrfe5w7mt/v927/o/lP3vev9/zPfd+9p1fPZ+ed9R5m7j1z3y3o27rf9hy79TGxfjR3m5yb2f9o/pP3PTr37H1nz535ztdxzns4mpu+72VAiKBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUn4BDBmgA5skUyYAAAAASUVORK5CYII=";
            Assert.Equal(expected, base64);
        }

        [Fact]
        public void GetImage_WithLabel()
        {
            var barcode = new Barcode("Hello", true);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALQAAABaCAYAAAARg3zAAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAEPElEQVR4nO3bO0gjWxyA8b8QFZ8Iaida2Kj4ABtFFHIrG0sRRBHUXra0kRUE34X4bLQSra28haAXBLm1rSAWNqIgiPiG7D2zZozJZHKSm8fy5/tByGTm7JlD8rGYTBIQkeDn7Z/wfSgUOsnLy/sr8rF8irc/WuQ4v/HR8yWa3298Jteb7DqzOW+i5ynevdf5cvU82KzbZr3hoH9+zhe+l6j9YrHfb9xPy3E28/uNz+R6/c6b63ltz2vzOgclN8+D3zjr9QYEUISgoQpBQxWChioEDVUIGqoQNFQhaKhC0FCFoKEKQUMVgoYqBA1VCBqqEDRUIWioQtBQhaChCkFDFYKGKgQNVQgaqhA0VCFoqELQUIWgoQpBQxWChioEDVUIGqoQNFQhaKhC0FCFoKEKQUMVgoYqBA1VCBqqEDRUIWioQtBQhaChCkFDFYKGKgQNVQgaqhA0VCFoqELQUIWgoQpBQxWChioEDVUIGqqYoP/977YSdS8ejyXBfvEZ5zfe9rw24zO53mTXmc15bc9r8zrn6nmwWXfC9QZCodDfnwd+RN577Be//dHizZtoXKL5LcZnZL3JrjMH89qe1/d1ztXzYLvuROfnTw6oQtBQhaChCkFDFYKOMjc3J29vb872+Pi41NTUuMdOT0/l+PjY2a6vr5fh4eGk59/d3ZXLy0tne2BgQBobG+Xi4kL29/edfbW1tTI6OipIDUFHmZ2dlcfHR2e7t7c3Jujp6Wn3WKpBHx0dOdvNzc1u0OF5e3p6CPp/IGioQtBQhaChCkFDFYKGKgQNVQg6DTY3N52P3W5vb2OOVVVVydTUlExMTCQ979ramszMzHjOW15eLiMjI84YfCFoHyaYkpIS9/HNzY3nuMXFRc/ojLu7O5mfn08p6IWFhbjzPjw8yPr6uhN8RUWF4DeC9mEueNh4enpyt80Vv6KiInl/f3euBBrPz88pnT9y3v7+fiktLXXPEb6aacYQ9BeCTrO+vj4pKyuTl5eXtM67vLwsdXV1zvbBwYEbNL4jaB8bGxvS1NTkPt7b25Pt7W3Bn4ugfbS3t0tnZ6f7+OzsLOG/MW8QCwsL5ePjQ5B9BJ1mk5OTMfsKCgoE2UHQaTY0NCT5+fnf9o2NjQmyg6DTbGtry3lTiNwg6Ax5fX2VhoYGZ9sEfn5+Lsg8gs6QUCgkV1dXzjafE2cPQaeZeVNo3gSm+1OO7u5uCQR+v1zmKiG8EXQamO9rhC9Rm4/tvI6norq6Wu7v753t6+vrmOMmcP73/46go5jf9IUvVZsvAEUyP2ANBoPOdmtrq7vfXGxZWVmJ++WkyO9xtLW1OZfFDROsUVlZ6c7b0tLijt3Z2ZHV1dW4X04aHByU4uJiwReCjnJ4eBj3mPlRrNcPY7u6upybjaWlpZh9HR0dcnJyErPf/JlhbrBH0FCFoKEKQUMVgoYqBA1VfgF9iClNy+z/ywAAAABJRU5ErkJggg==";
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
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALQAAABaCAYAAAARg3zAAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABYElEQVR4nO3SQWrDMBQAURlysPZkPVp6M3eTgGkxRK6gdHgPsvBH8KUwtzHG2+P3ue/7fTxs2/Z+nB+/Z+dXz53dZ/We2ff/t/fN7lmx/9X/8ZVzM/Nn0B/jp+/z4/fs/Oq5s/us3jP7/lV7z/b99Z4V+39zr8v3vA0IETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNiqBJETQpgiZF0KQImhRBkyJoUgRNyhe/wtr4MLilZAAAAABJRU5ErkJggg==";
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
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOgAAAB0CAYAAACPDX5AAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAB00lEQVR4nO3TQWrDMBQAURlysPZkPVp7M3eTgmkwRLGgU3gPsrCw8iXD3MYYb/ff177vn+Nu27b34/rxeXb91ffOzrN6zuz9/9v9ZuesmP/sdzx77+r+ld9p9b1n1n8C/RiPfq8fn2fXX33v7Dyr58zef9Xcs3l/PWfF/CvnWrH/2XOu/r8r+x/WbwPIEiiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohAkUwgQKYQKFMIFCmEAhTKAQJlAIEyiECRTCBAphAoUwgUKYQCFMoBAmUAgTKIQJFMIECmEChTCBQphAIUygECZQCBMohH0DvzgX405Hw8QAAAAASUVORK5CYII=";
            Assert.Equal(expected, base64);
        }
    }
}