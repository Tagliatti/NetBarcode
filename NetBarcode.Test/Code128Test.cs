using System;
using Xunit;
using NetBarcode.Types;

namespace NetBarcode.Test
{
    public class Code128Test
    {
        [Fact]
        public void TestCode128A()
        {
            var code128 = new Code128("ABC", Code128.Code128Type.A);
            Assert.Equal("11010000100101000110001000101100010001000110110110011001100011101011", code128.GetEncoding());
        }
        
        [Fact]
        public void TestCode128B()
        {
            var code128 = new Code128("a@B=1", Code128.Code128Type.B);
            Assert.Equal("110100100001001011000011000110110100010110001110011001010011100110110111001001100011101011", code128.GetEncoding());
        }
        
        [Fact]
        public void TestCode128C()
        {
            var code128 = new Code128("123456", Code128.Code128Type.C);
            Assert.Equal("11010011100101100111001000101100011100010110100011011101100011101011", code128.GetEncoding());
        }

        [Fact]
        public void TestCode128Auto()
        {
            var code128 = new Code128("12345Hehsan123456A", Code128.Code128Type.Auto);
            Assert.Equal("11010010000100111001101100111001011001011100110010011101101110010011000101000101100100001001100001010111100100100101100001100001010010011100110110011100101100101110011001001110110111001001100111010010100011000100101111001100011101011", code128.GetEncoding());
        }
        
        [Fact]
        public void TestCode128AInvalid()
        {
            var code128 = new Code128("Abc", Code128.Code128Type.A);
            Assert.Throws<Exception>(() => code128.GetEncoding());
        }
        
        [Fact]
        public void TestCode128BInvalid()
        {
            var code128 = new Code128("Abc\t123", Code128.Code128Type.B);
            Assert.Throws<Exception>(() => code128.GetEncoding());
        }
        
        [Fact]
        public void TestCode128CInvalid()
        {
            var code128 = new Code128("1234ab56", Code128.Code128Type.C);
            Assert.Throws<Exception>(() => code128.GetEncoding());
        }
    }
}