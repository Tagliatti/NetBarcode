using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System.Collections;
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

        [Fact]
        public void CodeCode128()
        {
            var barcode = new Barcode("HELLO!", Type.Code128A, false, 200, 50);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            _output.WriteLine(base64);
            var expected = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAA90lEQVR4nO3TS4rCQAAAUQPeO8zJ1SwEadKlWQwSeW+Ttn9Roa63h8vJLcvytz0fP2XdxttzXH/Ove4d18eze59n73m39zk/O3t077h/HM/ufV2bvW/v/5nNjd9zb++ZXS+/YZ2Mj66vk/lP7vnG3iNnZ2vv7v90bnbfqf1KIPAvBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQC4Q52RIKpZseR8gAAAABJRU5ErkJggg==";
            Assert.Equal(expected, base64);
        }


        [Theory, ClassData(typeof(DataAlphanumeric))]
        public void TestAlphanumericAll(Type _type, string _img64)
        {
            var barcode = new Barcode("HELLO!", _type, false, 200, 50);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            _output.WriteLine(base64);
            var expected = $"data:image/png;base64,{_img64}";
            Assert.Equal(expected, base64);
        }


        [Theory, ClassData(typeof(DataNumeric))]
        public void TestNumericAll(Type _type, string data, string _img64)
        {
            var barcode = new Barcode(data, _type, false, 200, 50);
            var image = barcode.GetImage();

            Assert.NotNull(image);

            var base64 = image.ToBase64String(PngFormat.Instance);
            _output.WriteLine(base64);
            var expected = $"data:image/png;base64,{_img64}";
            Assert.Equal(expected, base64);
        }



    }
    public class DataAlphanumeric : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
    {
        new object[] { Type.Code128,  "iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAA90lEQVR4nO3TS4rCQAAAUQPeO8zJ1SwEadKlWQwSeW+Ttn9Roa63h8vJLcvytz0fP2XdxttzXH/Ove4d18eze59n73m39zk/O3t077h/HM/ufV2bvW/v/5nNjd9zb++ZXS+/YZ2Mj66vk/lP7vnG3iNnZ2vv7v90bnbfqf1KIPAvBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQC4Q52RIKpZseR8gAAAABJRU5ErkJggg==" },
        new object[] { Type.Code128A, "iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAA90lEQVR4nO3TS4rCQAAAUQPeO8zJ1SwEadKlWQwSeW+Ttn9Roa63h8vJLcvytz0fP2XdxttzXH/Ove4d18eze59n73m39zk/O3t077h/HM/ufV2bvW/v/5nNjd9zb++ZXS+/YZ2Mj66vk/lP7vnG3iNnZ2vv7v90bnbfqf1KIPAvBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQC4Q52RIKpZseR8gAAAABJRU5ErkJggg==" },
        new object[] { Type.Code128B, "iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAA90lEQVR4nO3TwYqDMAAA0Qr9b9kv360HYQnNVA+lWN67GNIkVXHuvw+3i1uW5We7Ph5l3cbbdZ/fx8/W/p8b9z7bP/ufI2v3+dnes2vH9eN4du74bsbz6p5e3cds7ZXdb99hPTA+MrdO5o+c/Ym1Z/bOfpt9yGff46vzLulbAoG3EAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIhD92RIKpQ/2KsAAAAABJRU5ErkJggg==" },
        new object[] { Type.Code39E, "iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAA6klEQVR4nO3T0QqCQBQAUYX+W/ryyodAwqYsjIRzXtrW1ivGnC43wwGM43i+Peq0XM+f8/e1de0t7/Pumfve4/y95j078875tWesd/jqvW753Zb/ZziA03Ac08p6+uD6t2fW9vec98n5PebvMfvvHSkQ+DmBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUC4AhIyzxirHSbCAAAAAElFTkSuQmCC" }
    };

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }

    public class DataNumeric : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new()
        {
        new object[] { Type.EAN8,"1234567", "iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABAklEQVR4nO3TQW6DMBQA0SDlYOnJcrT2Zq3XEYxjFimL9ySEMA7+ijT33+H2z7Zt+xq3x7h+xjjfr897+2bvV9ePzj+67815dM7KeSvfrTnPzDf7v2bvZ/Mc/f7snJ9wv13DY1zPeN5bn71fXX9O1mf76pyV81a+W3Oeme/dfWfneXeu2fkfc5VA4JIEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBAJBIBAEAkEgEAQCQSAQBALhD3XrkiU5+0kOAAAAAElFTkSuQmCC" },
        new object[] { Type.EAN13,"1234567890123",   "iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABEUlEQVR4nO3TXWqDQBhA0RGysHZlWVq7s9a+lZC5jsS8nQNBouPnD97bz2482Lbtc9987L/v/fDX4//Zumf7Z3Nm29XrrZw3u4+j5x0Tr76X1TnvuM648D2vPMd48bs4O+/K9/9/7W0897f4Hv/P7L8f7D9adzS3zpvdR80tNf/MvKM577jOle955T5f/S7OzlvZPzs+XT8LBBgCgSQQCAKBIBAIAoEgEAgCgSAQCAKBIBAIAoEgEAgCgSAQCAKBIBAIAoEgEAgCgSAQCAKBIBAIAoEgEAgCgSAQCAKBIBAIAoEgEAgCgSAQCAKBIBAIAoEgEAgCgSAQCAKBIBAIAoEgEAgCgSAQCAKBIBAIAoHwCxgmx5hQC0m8AAAAAElFTkSuQmCC" },
        new object[] { Type.Code93, "1234567890123", "iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABB0lEQVR4nO3T22qDQAAAUYX8t/TLm/RNRMdtE1MC57yseFl1dW7fD9NF5nn+eky/rMf18e2xn3F93dE82/11ztGx38w5un30zmfP9ez7jKzXu9d5/X2333rv3iNrV//PdJHbdK3lYKxz9s5bTvY/c4+ROUe39+Ydea6ad+R9Rtbrv9f57N571/9lDV/q6kDgowkEgkAgCASCQCAIBIJAIAgEgkAgCASCQCAIBIJAIAgEgkAgCASCQCAIBIJAIAgEgkAgCASCQCAIBIJAIAgEgkAgCASCQCAIBIJAIAgEgkAgCASCQCAIBIJAIAgEgkAgCASCQCAIBIJAIAgEgkAgCASCQCAIBMIdnnfukdjMDtoAAAAASUVORK5CYII=" },
        new object[] { Type.Code39, "1234567890123", "iVBORw0KGgoAAAANSUhEUgAAAMgAAAAyCAYAAAAZUZThAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABEElEQVR4nO3T0WqDQBQA0Qj5b+mXtwk0kIZ11qWhUDjnRU1y1TXO9fPm8m3bto/b4f68f9/ej0f7R5+9zp+ZPTMz2j+aG20fv52t4Wg9o7nZes7MrDzz2bOu6x09s9k9jtbyH9+Dlff60cT18tM+2N/f9P1s9uzMyty71jCbm61n9d5+ez8r97b6n61c++w5/+o9WD7nayDAE4FAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQBAIBIFAEAgEgUAQCASBQPgCRxJWCWQeKtkAAAAASUVORK5CYII=" },
    };

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}