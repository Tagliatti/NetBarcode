using System;
using NetBarcode.Types;
using Xunit;

namespace NetBarcode.Test
{
    public class Ean13Test
    {
        [Fact]
        public void TestSuccess()
        {
            var ean13 = new Ean13("5901234123457");
            Assert.Equal("10100010110100111011001100100110111101001110101010110011011011001000010101110010011101000100101", ean13.GetEncoding());
        }
        
        [Fact]
        public void TestAutoIncludeChecksumIfMissing()
        {
            var ean13 = new Ean13("590123412345");
            Assert.Equal("10100010110100111011001100100110111101001110101010110011011011001000010101110010011101000100101", ean13.GetEncoding());
        }
        
        [Fact]
        public void TestInvalid()
        {
            Assert.Throws<Exception>(() => new Ean13("12345"));
        }
    }
}