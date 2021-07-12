using System;
using NetBarcode.Types;
using Xunit;

namespace NetBarcode.Test
{
    public class Ean8Test
    {
        [Fact]
        public void TestSuccess()
        {
            var ean8 = new Ean8("96385074");
            Assert.Equal("1010001011010111101111010110111010101001110111001010001001011100101", ean8.GetEncoding());
        }
        
        [Fact]
        public void TestAutoIncludeChecksumIfMissing()
        {
            var ean8 = new Ean8("9638507");
            Assert.Equal("1010001011010111101111010110111010101001110111001010001001011100101", ean8.GetEncoding());
        }
        
        [Fact]
        public void TestInvalid()
        {
            var ean8 = new Ean8("12345");
            Assert.Throws<Exception>(() => ean8.GetEncoding());
        }
    }
}