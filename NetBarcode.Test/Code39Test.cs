using System;
using NetBarcode.Types;
using Xunit;

namespace NetBarcode.Test
{
    public class Code39Test
    {
        [Fact]
        public void TestNormal()
        {
            var code39 = new Code39("AB12");
            Assert.Equal("10010110110101101010010110101101001011011010010101101011001010110100101101101", code39.GetEncoding());
        }
        
        [Fact]
        public void TestInvalid()
        {
            var code39 = new Code39("AB!12");
            Assert.Throws<Exception>(() => code39.GetEncoding());
        }
    }
}