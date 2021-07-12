using System;
using NetBarcode.Types;
using Xunit;

namespace NetBarcode.Test
{
    public class CodabarTest
    {
        [Fact]
        public void TestStringWithStartAndStopCharacters()
        {
            var codabar = new Codabar("A12345B");
            Assert.Equal("10110010010101011001010100101101100101010101101001011010100101001001011", codabar.GetEncoding());
        }
        
        [Fact]
        public void TestStringWithoutStartCharacters()
        {
            var codabar = new Codabar("12345B");
            Assert.Throws<Exception>(() => codabar.GetEncoding());
        }
        
        [Fact]
        public void TestStringWithoutStopCharacters()
        {
            var codabar = new Codabar("A12345");
            Assert.Throws<Exception>(() => codabar.GetEncoding());
        }
        
        [Fact]
        public void TestEmpty()
        {
            var codabar = new Codabar("");
            Assert.Throws<Exception>(() => codabar.GetEncoding());
        }
        
        [Fact]
        public void TestInvalid()
        {
            var codabar = new Codabar("A1234OOPS56A");
            Assert.Throws<Exception>(() => codabar.GetEncoding());
        }
    }
}