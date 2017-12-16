using System;

namespace NetBarcode.Types
{
    /// <summary>
    ///  EAN-8 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class EAN8 : Base, IBarcode
    {
        private readonly string[] _codesA = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private readonly string[] _codesC = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private readonly string _data;

        public EAN8(string data)
        {
            _data = data + CheckDigit(data);
        }
        
        /// <summary>
        /// Encode the raw data using the EAN-8 algorithm.
        /// </summary>
        public string GetEncoding()
        {
            //check length
            if (_data.Length != 8 && _data.Length != 7)
            {
                throw new Exception("EEAN8-1: Invalid data length. (7 or 8 numbers only)");
            }

            //check numeric only
            if (!CheckNumericOnly(_data))
            {
                throw new Exception("EEAN8-2: Numeric only.");
            }

            //encode the data
            var encodedData = "101";

            //first half (Encoded using left hand / odd parity)
            for (var i = 0; i < _data.Length / 2; i++)
            {
                encodedData += _codesA[int.Parse(_data[i].ToString())];
            }

            //center guard bars
            encodedData += "01010";

            //second half (Encoded using right hand / even parity)
            for (var i = _data.Length / 2; i < _data.Length; i++)
            {
                encodedData += _codesC[int.Parse(_data[i].ToString())];
            }

            encodedData += "101";

            return encodedData;
        }

        private string CheckDigit(string data)
        {
            //calculate the checksum digit if necessary
            if (data.Length == 7)
            {
                //calculate the checksum digit
                var even = 0;
                var odd = 0;

                //odd
                for (var i = 0; i <= 6; i += 2)
                {
                    odd += int.Parse(data.Substring(i, 1)) * 3;
                }

                //even
                for (var i = 1; i <= 5; i += 2)
                {
                    even += int.Parse(data.Substring(i, 1));
                }

                var total = even + odd;
                var checksum = total % 10;
                checksum = 10 - checksum;

                if (checksum == 10)
                {
                    checksum = 0;
                }

                //add the checksum to the end of the 
                return checksum.ToString();
            }

            return "";
        }
    }
}
