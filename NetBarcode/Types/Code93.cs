using System;
using System.Data;

namespace NetBarcode.Types
{
    /// <summary>
    ///  Code 93 encoding
    ///  Written by: Brad Barnhill
    /// </summary>
    internal class Code93 : Base, IBarcode
    {
        private readonly DataTable _codes = new DataTable("C93_Code");
        private readonly string _data;

        /// <summary>
        /// Encodes with Code93.
        /// </summary>
        /// <param name="data">Data to encode.</param>
        public Code93(string data)
        {
            _data = data;
        }

        /// <summary>
        /// Encode the raw data using the Code 93 algorithm.
        /// </summary>
        public string GetEncoding()
        {
            Initialize();

            var formattedData = AddCheckDigits(_data);

            var encodedData = _codes.Select("Character = '*'")[0]["Encoding"].ToString();
            
            foreach (char c in formattedData)
            {
                try
                {
                    encodedData += _codes.Select("Character = '" + c.ToString() + "'")[0]["Encoding"].ToString();
                }
                catch
                {
                    throw new Exception("EC93-1: Invalid data.");
                }
            }

            encodedData += _codes.Select("Character = '*'")[0]["Encoding"].ToString();

            //termination bar
            encodedData += "1";

            return encodedData;
        }
        
        private void Initialize()
        {
            _codes.Rows.Clear();
            _codes.Columns.Clear();
            _codes.Columns.Add("Value");
            _codes.Columns.Add("Character");
            _codes.Columns.Add("Encoding");
            _codes.Rows.Add(new object[] { "0", "0", "100010100" });
            _codes.Rows.Add(new object[] { "1", "1", "101001000" });
            _codes.Rows.Add(new object[] { "2", "2", "101000100" });
            _codes.Rows.Add(new object[] { "3", "3", "101000010" });
            _codes.Rows.Add(new object[] { "4",  "4", "100101000" });
            _codes.Rows.Add(new object[] { "5",  "5", "100100100" });
            _codes.Rows.Add(new object[] { "6",  "6", "100100010" });
            _codes.Rows.Add(new object[] { "7",  "7", "101010000" });
            _codes.Rows.Add(new object[] { "8",  "8", "100010010" });
            _codes.Rows.Add(new object[] { "9",  "9", "100001010" });
            _codes.Rows.Add(new object[] { "10", "A", "110101000" });
            _codes.Rows.Add(new object[] { "11", "B", "110100100" });
            _codes.Rows.Add(new object[] { "12", "C", "110100010" });
            _codes.Rows.Add(new object[] { "13", "D", "110010100" });
            _codes.Rows.Add(new object[] { "14", "E", "110010010" });
            _codes.Rows.Add(new object[] { "15", "F", "110001010" });
            _codes.Rows.Add(new object[] { "16", "G", "101101000" });
            _codes.Rows.Add(new object[] { "17", "H", "101100100" });
            _codes.Rows.Add(new object[] { "18", "I", "101100010" });
            _codes.Rows.Add(new object[] { "19", "J", "100110100" });
            _codes.Rows.Add(new object[] { "20", "K", "100011010" });
            _codes.Rows.Add(new object[] { "21", "L", "101011000" });
            _codes.Rows.Add(new object[] { "22", "M", "101001100" });
            _codes.Rows.Add(new object[] { "23", "N", "101000110" });
            _codes.Rows.Add(new object[] { "24", "O", "100101100" });
            _codes.Rows.Add(new object[] { "25", "P", "100010110" });
            _codes.Rows.Add(new object[] { "26", "Q", "110110100" });
            _codes.Rows.Add(new object[] { "27", "R", "110110010" });
            _codes.Rows.Add(new object[] { "28", "S", "110101100" });
            _codes.Rows.Add(new object[] { "29", "T", "110100110" });
            _codes.Rows.Add(new object[] { "30", "U", "110010110" });
            _codes.Rows.Add(new object[] { "31", "V", "110011010" });
            _codes.Rows.Add(new object[] { "32", "W", "101101100" });
            _codes.Rows.Add(new object[] { "33", "X", "101100110" });
            _codes.Rows.Add(new object[] { "34", "Y", "100110110" });
            _codes.Rows.Add(new object[] { "35", "Z", "100111010" });
            _codes.Rows.Add(new object[] { "36", "-", "100101110" });
            _codes.Rows.Add(new object[] { "37", ".", "111010100" });
            _codes.Rows.Add(new object[] { "38", " ", "111010010" });
            _codes.Rows.Add(new object[] { "39", "$", "111001010" });
            _codes.Rows.Add(new object[] { "40", "/", "101101110" });
            _codes.Rows.Add(new object[] { "41", "+", "101110110" });
            _codes.Rows.Add(new object[] { "42", "%", "110101110" });
            _codes.Rows.Add(new object[] { "43", "(", "100100110" });//dont know what character actually goes here
            _codes.Rows.Add(new object[] { "44", ")", "111011010" });//dont know what character actually goes here
            _codes.Rows.Add(new object[] { "45", "#", "111010110" });//dont know what character actually goes here
            _codes.Rows.Add(new object[] { "46", "@", "100110010" });//dont know what character actually goes here
            _codes.Rows.Add(new object[] { "-",  "*", "101011110" });
        }
        
        private string AddCheckDigits(string data)
        {
            //populate the C weights
            var aryCWeights = new int[data.Length];
            var curweight = 1;
            
            for (var i = data.Length - 1; i >= 0; i--)
            {
                if (curweight > 20)
                    curweight = 1;
                aryCWeights[i] = curweight;
                curweight++;
            }

            //populate the K weights
            var aryKWeights = new int[data.Length + 1];
            curweight = 1;
            
            for (var i = data.Length; i >= 0; i--)
            {
                if (curweight > 15)
                    curweight = 1;
                aryKWeights[i] = curweight;
                curweight++;
            }

            //calculate C checksum
            var sum = 0;
            
            for (var i = 0; i < data.Length; i++)
            {
                sum += aryCWeights[i] * int.Parse(_codes.Select("Character = '" + data[i].ToString() + "'")[0]["Value"].ToString());
            }
            
            var checksumValue = sum % 47;

            data += _codes.Select("Value = '" + checksumValue.ToString() + "'")[0]["Character"].ToString();

            //calculate K checksum
            sum = 0;
            for (var i = 0; i < data.Length; i++)
            {
                sum += aryKWeights[i] * int.Parse(_codes.Select("Character = '" + data[i].ToString() + "'")[0]["Value"].ToString());
            }
            
            checksumValue = sum % 47;

            data += _codes.Select("Value = '" + checksumValue.ToString() + "'")[0]["Character"].ToString();

            return data;
        }
    }
}
