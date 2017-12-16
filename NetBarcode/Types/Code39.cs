using System;
using System.Collections;

namespace NetBarcode.Types
{
    /// <summary>
    ///  Code 39 encoding
    ///  Written by: Brad Barnhill
    ///  Refactored: Filipe Tagliatti
    /// </summary>
    internal class Code39 : Base, IBarcode
    {
        private readonly Hashtable _codes = new Hashtable();
        private readonly Hashtable _codesExtended = new Hashtable();
        private readonly bool _extended = false;
        private readonly bool _enableChecksum = false;
        private readonly string _data;

        /// <summary>
        /// Encodes with Code39.
        /// </summary>
        /// <param name="data">Data to encode.</param>
        public Code39(string data)
        {
            _data = data;
        }

        /// <summary>
        /// Encodes with Code39.
        /// </summary>
        /// <param name="data">_data to encode.</param>
        /// <param name="extended">Allow Extended Code 39 (Full Ascii mode).</param>
        public Code39(string data, bool extended)
        {
            _data = data;
            _extended = extended;
        }

        /// <summary>
        /// Encodes with Code39.
        /// </summary>
        /// <param name="data">_data to encode.</param>
        /// <param name="extended">Allow Extended Code 39 (Full Ascii mode).</param>
        /// <param name="enableChecksum">Whether to calculate the Mod 43 checksum and encode it into the barcode</param>
        public Code39(string data, bool extended, bool enableChecksum)
        {
            _data = data;
            _extended = extended;
            _enableChecksum = enableChecksum;
        }

        /// <summary>
        /// Encode the raw data using the Code 39 algorithm.
        /// </summary>
        public string GetEncoding()
        {
            Initialize();

            var strNoAstr = _data.Replace("*", "");
            var formattedData = "*" + strNoAstr + (_enableChecksum ? GetChecksumChar(strNoAstr).ToString() : String.Empty) + "*";

            if (_extended)
            {
                InitializeExtended();
                InsertExtendedCharsIfNeeded(ref formattedData);
            }

            var encodedData = "";
            
            foreach (char c in formattedData)
            {
                try
                {
                    encodedData += _codes[c].ToString();
                    encodedData += "0";//whitespace
                }
                catch
                {
                    if (_extended)
                    {
                        throw new Exception("EC39-1: Invalid data.");
                    }
                    
                    throw new Exception("EC39-1: Invalid data. (Try using Extended Code39)");
                }
            }

            encodedData = encodedData.Substring(0, encodedData.Length-1);

            return encodedData;
        }
        
        private void Initialize()
        {
            _codes.Clear();
            _codes.Add('0', "101001101101");
            _codes.Add('1', "110100101011");
            _codes.Add('2', "101100101011");
            _codes.Add('3', "110110010101");
            _codes.Add('4', "101001101011");
            _codes.Add('5', "110100110101");
            _codes.Add('6', "101100110101");
            _codes.Add('7', "101001011011");
            _codes.Add('8', "110100101101");
            _codes.Add('9', "101100101101");
            _codes.Add('A', "110101001011");
            _codes.Add('B', "101101001011");
            _codes.Add('C', "110110100101");
            _codes.Add('D', "101011001011");
            _codes.Add('E', "110101100101");
            _codes.Add('F', "101101100101");
            _codes.Add('G', "101010011011");
            _codes.Add('H', "110101001101");
            _codes.Add('I', "101101001101");
            _codes.Add('J', "101011001101");
            _codes.Add('K', "110101010011");
            _codes.Add('L', "101101010011");
            _codes.Add('M', "110110101001");
            _codes.Add('N', "101011010011");
            _codes.Add('O', "110101101001");
            _codes.Add('P', "101101101001");
            _codes.Add('Q', "101010110011");
            _codes.Add('R', "110101011001");
            _codes.Add('S', "101101011001");
            _codes.Add('T', "101011011001");
            _codes.Add('U', "110010101011");
            _codes.Add('V', "100110101011");
            _codes.Add('W', "110011010101");
            _codes.Add('X', "100101101011");
            _codes.Add('Y', "110010110101");
            _codes.Add('Z', "100110110101");
            _codes.Add('-', "100101011011");
            _codes.Add('.', "110010101101");
            _codes.Add(' ', "100110101101");
            _codes.Add('$', "100100100101");
            _codes.Add('/', "100100101001");
            _codes.Add('+', "100101001001");
            _codes.Add('%', "101001001001");
            _codes.Add('*', "100101101101");
        }
        
        private void InitializeExtended()
        {
            _codesExtended.Clear();
            _codesExtended.Add(Convert.ToChar(0).ToString(), "%U");
            _codesExtended.Add(Convert.ToChar(1).ToString(), "$A");
            _codesExtended.Add(Convert.ToChar(2).ToString(), "$B");
            _codesExtended.Add(Convert.ToChar(3).ToString(), "$C");
            _codesExtended.Add(Convert.ToChar(4).ToString(), "$D");
            _codesExtended.Add(Convert.ToChar(5).ToString(), "$E");
            _codesExtended.Add(Convert.ToChar(6).ToString(), "$F");
            _codesExtended.Add(Convert.ToChar(7).ToString(), "$G");
            _codesExtended.Add(Convert.ToChar(8).ToString(), "$H");
            _codesExtended.Add(Convert.ToChar(9).ToString(), "$I");
            _codesExtended.Add(Convert.ToChar(10).ToString(), "$J");
            _codesExtended.Add(Convert.ToChar(11).ToString(), "$K");
            _codesExtended.Add(Convert.ToChar(12).ToString(), "$L");
            _codesExtended.Add(Convert.ToChar(13).ToString(), "$M");
            _codesExtended.Add(Convert.ToChar(14).ToString(), "$N");
            _codesExtended.Add(Convert.ToChar(15).ToString(), "$O");
            _codesExtended.Add(Convert.ToChar(16).ToString(), "$P");
            _codesExtended.Add(Convert.ToChar(17).ToString(), "$Q");
            _codesExtended.Add(Convert.ToChar(18).ToString(), "$R");
            _codesExtended.Add(Convert.ToChar(19).ToString(), "$S");
            _codesExtended.Add(Convert.ToChar(20).ToString(), "$T");
            _codesExtended.Add(Convert.ToChar(21).ToString(), "$U");
            _codesExtended.Add(Convert.ToChar(22).ToString(), "$V");
            _codesExtended.Add(Convert.ToChar(23).ToString(), "$W");
            _codesExtended.Add(Convert.ToChar(24).ToString(), "$X");
            _codesExtended.Add(Convert.ToChar(25).ToString(), "$Y");
            _codesExtended.Add(Convert.ToChar(26).ToString(), "$Z");
            _codesExtended.Add(Convert.ToChar(27).ToString(), "%A");
            _codesExtended.Add(Convert.ToChar(28).ToString(), "%B");
            _codesExtended.Add(Convert.ToChar(29).ToString(), "%C");
            _codesExtended.Add(Convert.ToChar(30).ToString(), "%D");
            _codesExtended.Add(Convert.ToChar(31).ToString(), "%E");
            _codesExtended.Add("!", "/A");
            _codesExtended.Add("\"", "/B");
            _codesExtended.Add("#", "/C");
            _codesExtended.Add("$", "/D");
            _codesExtended.Add("%", "/E");
            _codesExtended.Add("&", "/F");
            _codesExtended.Add("'", "/G");
            _codesExtended.Add("(", "/H");
            _codesExtended.Add(")", "/I");
            _codesExtended.Add("*", "/J");
            _codesExtended.Add("+", "/K");
            _codesExtended.Add(",", "/L");
            _codesExtended.Add("/", "/O");
            _codesExtended.Add(":", "/Z");
            _codesExtended.Add(";", "%F");
            _codesExtended.Add("<", "%G");
            _codesExtended.Add("=", "%H");
            _codesExtended.Add(">", "%I");
            _codesExtended.Add("?", "%J");
            _codesExtended.Add("[", "%K");
            _codesExtended.Add("\\", "%L");
            _codesExtended.Add("]", "%M");
            _codesExtended.Add("^", "%N");
            _codesExtended.Add("_", "%O");
            _codesExtended.Add("{", "%P");
            _codesExtended.Add("|", "%Q");
            _codesExtended.Add("}", "%R");
            _codesExtended.Add("~", "%S");
            _codesExtended.Add("`", "%W");
            _codesExtended.Add("@", "%V");
            _codesExtended.Add("a", "+A");
            _codesExtended.Add("b", "+B");
            _codesExtended.Add("c", "+C");
            _codesExtended.Add("d", "+D");
            _codesExtended.Add("e", "+E");
            _codesExtended.Add("f", "+F");
            _codesExtended.Add("g", "+G");
            _codesExtended.Add("h", "+H");
            _codesExtended.Add("i", "+I");
            _codesExtended.Add("j", "+J");
            _codesExtended.Add("k", "+K");
            _codesExtended.Add("l", "+L");
            _codesExtended.Add("m", "+M");
            _codesExtended.Add("n", "+N");
            _codesExtended.Add("o", "+O");
            _codesExtended.Add("p", "+P");
            _codesExtended.Add("q", "+Q");
            _codesExtended.Add("r", "+R");
            _codesExtended.Add("s", "+S");
            _codesExtended.Add("t", "+T");
            _codesExtended.Add("u", "+U");
            _codesExtended.Add("v", "+V");
            _codesExtended.Add("w", "+W");
            _codesExtended.Add("x", "+X");
            _codesExtended.Add("y", "+Y");
            _codesExtended.Add("z", "+Z");
            _codesExtended.Add(Convert.ToChar(127).ToString(), "%T"); //also %X, %Y, %Z 
        }
        
        private void InsertExtendedCharsIfNeeded(ref string formattedData)
        {
            var output = "";
            
            foreach (char c in formattedData)
            {
                try
                {
                    string s = _codes[c].ToString();
                    output += c;
                }
                catch 
                { 
                    //insert extended substitution
                    object oTrans = _codesExtended[c.ToString()];
                    output += oTrans.ToString();
                }
            }

            formattedData = output;
        }
        
        private char GetChecksumChar(string strNoAstr) 
        {
            //checksum
            const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";
            InsertExtendedCharsIfNeeded(ref strNoAstr);
            var sum = 0;

            //Calculate the checksum
            for (var i = 0; i < strNoAstr.Length; ++i)
            {
                sum = sum + charset.IndexOf(strNoAstr[i].ToString(), StringComparison.Ordinal);
            }

            //return the checksum char
            return charset[sum % 43];
        }
    }
}
