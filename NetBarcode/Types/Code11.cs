using System;

namespace NetBarcode.Types
{
    /// <summary>
    ///  Code 11 encoding
    ///  Written by: Brad Barnhill
    ///  Refactored: Filipe Tagliatti
    /// </summary>
    internal class Code11 : Base, IBarcode
    {
        private readonly string _data;

        private readonly string[] _codes =
        {
            "101011", "1101011", "1001011", "1100101", "1011011", "1101101", "1001101", "1010011", "1101001", "110101",
            "101101", "1011001"
        };

        public Code11(string data)
        {
            _data = data;
        }
        
        /// <summary>
        /// Encode the raw data using the Code 11 algorithm.
        /// </summary>
        public string GetEncoding()
        {
            if (!CheckNumericOnly(_data.Replace("-", "")))
            {
                throw new Exception("EC11-1: Numeric data and '-' Only");
            }

            //calculate the checksums
            var weight = 1;
            var cTotal = 0;
            var dataToEncodeWithChecksums = _data;

            //figure the C checksum
            for (var i = _data.Length - 1; i >= 0; i--)
            {
                //C checksum weights go 1-10
                if (weight == 10)
                {
                    weight = 1;
                }

                if (_data[i] != '-')
                {
                    cTotal += int.Parse(_data[i].ToString()) * weight++;
                }
                else
                {
                    cTotal += 10 * weight++;
                }
            }
            
            var checksumC = cTotal % 11;

            dataToEncodeWithChecksums += checksumC.ToString();

            //K checksums are recommended on any message length greater than or equal to 10
            if (_data.Length >= 10)
            {
                weight = 1;
                var kTotal = 0;

                //calculate K checksum
                for (var i = dataToEncodeWithChecksums.Length - 1; i >= 0; i--)
                {
                    //K checksum weights go 1-9
                    if (weight == 9)
                    {
                        weight = 1;
                    }

                    if (dataToEncodeWithChecksums[i] != '-')
                    {
                        kTotal += int.Parse(dataToEncodeWithChecksums[i].ToString()) * weight++;
                    }
                    else
                    {
                        kTotal += 10 * weight++;
                    }
                }
                
                var checksumK = kTotal % 11;
                dataToEncodeWithChecksums += checksumK.ToString();
            }

            //encode data
            const string space = "0";
            var result = _codes[11] + space; //start-stop char + interchar space

            foreach (char c in dataToEncodeWithChecksums)
            {
                var index = (c == '-' ? 10 : int.Parse(c.ToString()));
                result += _codes[index];

                //inter-character space
                result += space;
            }

            //stop bars
            result += _codes[11];

            return result;
        }
    }
}
