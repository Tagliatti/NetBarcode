using System;
using NetBarcode.Types;
using Xunit;

namespace NetBarcode.Test
{
    public class Code11Test
    {
        [Fact]
        public void TestNumeric()
        {
            var code11 = new Code11("123456");
            Assert.Equal("10110010110101101001011011001010101101101101101010011010110101101011001", code11.GetEncoding());
        }
        
        [Fact]
        public void TestNumericAndMinus()
        {
            var code11 = new Code11("123-456");
            Assert.Equal("101100101101011010010110110010101011010101101101101101010011010110010101011001", code11.GetEncoding());
        }
        
        [Fact]
        public void TestInvalid()
        {
            var code11 = new Code11("123A45");
            Assert.Throws<Exception>(() => code11.GetEncoding());
        }
    }
}