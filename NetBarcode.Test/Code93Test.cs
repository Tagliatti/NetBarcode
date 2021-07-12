using NetBarcode.Types;
using Xunit;

namespace NetBarcode.Test
{
    public class Code93Test
    {
        [Fact]
        public void TestSuccess()
        {
            var code93 = new Code93("ABC123");
            Assert.Equal("1010111101101010001101001001101000101010010001010001001010000101011011001000010101010111101", code93.GetEncoding());
        }
    }
}