using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NetBarcode.Types
{
    /// <summary>
    ///  Code 128 encoding
    ///  Written by: Brad Barnhill
    ///  Refactored: Filipe Tagliatti
    /// </summary>
    internal class Code128 : Base, IBarcode
    {
        public enum Code128Type { Auto, A, B, C };
        private readonly DataTable _codes = new DataTable("C128");
        private readonly List<string> _formattedData = new List<string>();
        private readonly List<string> _encodedData = new List<string>();
        private readonly Code128Type _code128Type = Code128Type.Auto;
        private readonly string _data;

        /// <summary>
        /// Encodes data.
        /// </summary>
        /// <param name="data">Data to encode.</param>
        public Code128(string data)
        {
            _data = data;
        }

        /// <summary>
        /// Encodes data.
        /// </summary>
        /// <param name="data">Data to encode.</param>
        /// <param name="code128Type">Type of encoding to lock to. (Code 128A, Code 128B, Code 128C)</param>
        public Code128(string data, Code128Type code128Type)
        {
            _code128Type = code128Type;
            _data = data;
        }

        public string GetEncoding()
        {
            //initialize datastructure to hold encoding information
            Initialize();

            return Encode();            
        }
        
        private void Initialize()
        {
            //set the table to case sensitive since there are upper and lower case values
            _codes.CaseSensitive = true;

            //set up columns
            _codes.Columns.Add("Value", typeof(string));
            _codes.Columns.Add("A", typeof(string));
            _codes.Columns.Add("B", typeof(string));
            _codes.Columns.Add("C", typeof(string));
            _codes.Columns.Add("Encoding", typeof(string));

            //populate data
            _codes.Rows.Add(new object[] { "0", " ", " ", "00", "11011001100" });
            _codes.Rows.Add(new object[] { "1", "!", "!", "01", "11001101100" });
            _codes.Rows.Add(new object[] { "2", "\"", "\"", "02", "11001100110" });
            _codes.Rows.Add(new object[] { "3", "#", "#", "03", "10010011000" });
            _codes.Rows.Add(new object[] { "4", "$", "$", "04", "10010001100" });
            _codes.Rows.Add(new object[] { "5", "%", "%", "05", "10001001100" });
            _codes.Rows.Add(new object[] { "6", "&", "&", "06", "10011001000" });
            _codes.Rows.Add(new object[] { "7", "'", "'", "07", "10011000100" });
            _codes.Rows.Add(new object[] { "8", "(", "(", "08", "10001100100" });
            _codes.Rows.Add(new object[] { "9", ")", ")", "09", "11001001000" });
            _codes.Rows.Add(new object[] { "10", "*", "*", "10", "11001000100" });
            _codes.Rows.Add(new object[] { "11", "+", "+", "11", "11000100100" });
            _codes.Rows.Add(new object[] { "12", ",", ",", "12", "10110011100" });
            _codes.Rows.Add(new object[] { "13", "-", "-", "13", "10011011100" });
            _codes.Rows.Add(new object[] { "14", ".", ".", "14", "10011001110" });
            _codes.Rows.Add(new object[] { "15", "/", "/", "15", "10111001100" });
            _codes.Rows.Add(new object[] { "16", "0", "0", "16", "10011101100" });
            _codes.Rows.Add(new object[] { "17", "1", "1", "17", "10011100110" });
            _codes.Rows.Add(new object[] { "18", "2", "2", "18", "11001110010" });
            _codes.Rows.Add(new object[] { "19", "3", "3", "19", "11001011100" });
            _codes.Rows.Add(new object[] { "20", "4", "4", "20", "11001001110" });
            _codes.Rows.Add(new object[] { "21", "5", "5", "21", "11011100100" });
            _codes.Rows.Add(new object[] { "22", "6", "6", "22", "11001110100" });
            _codes.Rows.Add(new object[] { "23", "7", "7", "23", "11101101110" });
            _codes.Rows.Add(new object[] { "24", "8", "8", "24", "11101001100" });
            _codes.Rows.Add(new object[] { "25", "9", "9", "25", "11100101100" });
            _codes.Rows.Add(new object[] { "26", ":", ":", "26", "11100100110" });
            _codes.Rows.Add(new object[] { "27", ";", ";", "27", "11101100100" });
            _codes.Rows.Add(new object[] { "28", "<", "<", "28", "11100110100" });
            _codes.Rows.Add(new object[] { "29", "=", "=", "29", "11100110010" });
            _codes.Rows.Add(new object[] { "30", ">", ">", "30", "11011011000" });
            _codes.Rows.Add(new object[] { "31", "?", "?", "31", "11011000110" });
            _codes.Rows.Add(new object[] { "32", "@", "@", "32", "11000110110" });
            _codes.Rows.Add(new object[] { "33", "A", "A", "33", "10100011000" });
            _codes.Rows.Add(new object[] { "34", "B", "B", "34", "10001011000" });
            _codes.Rows.Add(new object[] { "35", "C", "C", "35", "10001000110" });
            _codes.Rows.Add(new object[] { "36", "D", "D", "36", "10110001000" });
            _codes.Rows.Add(new object[] { "37", "E", "E", "37", "10001101000" });
            _codes.Rows.Add(new object[] { "38", "F", "F", "38", "10001100010" });
            _codes.Rows.Add(new object[] { "39", "G", "G", "39", "11010001000" });
            _codes.Rows.Add(new object[] { "40", "H", "H", "40", "11000101000" });
            _codes.Rows.Add(new object[] { "41", "I", "I", "41", "11000100010" });
            _codes.Rows.Add(new object[] { "42", "J", "J", "42", "10110111000" });
            _codes.Rows.Add(new object[] { "43", "K", "K", "43", "10110001110" });
            _codes.Rows.Add(new object[] { "44", "L", "L", "44", "10001101110" });
            _codes.Rows.Add(new object[] { "45", "M", "M", "45", "10111011000" });
            _codes.Rows.Add(new object[] { "46", "N", "N", "46", "10111000110" });
            _codes.Rows.Add(new object[] { "47", "O", "O", "47", "10001110110" });
            _codes.Rows.Add(new object[] { "48", "P", "P", "48", "11101110110" });
            _codes.Rows.Add(new object[] { "49", "Q", "Q", "49", "11010001110" });
            _codes.Rows.Add(new object[] { "50", "R", "R", "50", "11000101110" });
            _codes.Rows.Add(new object[] { "51", "S", "S", "51", "11011101000" });
            _codes.Rows.Add(new object[] { "52", "T", "T", "52", "11011100010" });
            _codes.Rows.Add(new object[] { "53", "U", "U", "53", "11011101110" });
            _codes.Rows.Add(new object[] { "54", "V", "V", "54", "11101011000" });
            _codes.Rows.Add(new object[] { "55", "W", "W", "55", "11101000110" });
            _codes.Rows.Add(new object[] { "56", "X", "X", "56", "11100010110" });
            _codes.Rows.Add(new object[] { "57", "Y", "Y", "57", "11101101000" });
            _codes.Rows.Add(new object[] { "58", "Z", "Z", "58", "11101100010" });
            _codes.Rows.Add(new object[] { "59", "[", "[", "59", "11100011010" });
            _codes.Rows.Add(new object[] { "60",@"\",@"\", "60", "11101111010" });
            _codes.Rows.Add(new object[] { "61", "]", "]", "61", "11001000010" });
            _codes.Rows.Add(new object[] { "62", "^", "^", "62", "11110001010" });
            _codes.Rows.Add(new object[] { "63", "_", "_", "63", "10100110000" });
            _codes.Rows.Add(new object[] { "64", "\0", "`", "64", "10100001100" });
            _codes.Rows.Add(new object[] { "65", Convert.ToChar(1).ToString(), "a", "65", "10010110000" });
            _codes.Rows.Add(new object[] { "66", Convert.ToChar(2).ToString(), "b", "66", "10010000110" });
            _codes.Rows.Add(new object[] { "67", Convert.ToChar(3).ToString(), "c", "67", "10000101100" });
            _codes.Rows.Add(new object[] { "68", Convert.ToChar(4).ToString(), "d", "68", "10000100110" });
            _codes.Rows.Add(new object[] { "69", Convert.ToChar(5).ToString(), "e", "69", "10110010000" });
            _codes.Rows.Add(new object[] { "70", Convert.ToChar(6).ToString(), "f", "70", "10110000100" });
            _codes.Rows.Add(new object[] { "71", Convert.ToChar(7).ToString(), "g", "71", "10011010000" });
            _codes.Rows.Add(new object[] { "72", Convert.ToChar(8).ToString(), "h", "72", "10011000010" });
            _codes.Rows.Add(new object[] { "73", Convert.ToChar(9).ToString(), "i", "73", "10000110100" });
            _codes.Rows.Add(new object[] { "74", Convert.ToChar(10).ToString(), "j", "74", "10000110010" });
            _codes.Rows.Add(new object[] { "75", Convert.ToChar(11).ToString(), "k", "75", "11000010010" });
            _codes.Rows.Add(new object[] { "76", Convert.ToChar(12).ToString(), "l", "76", "11001010000" });
            _codes.Rows.Add(new object[] { "77", Convert.ToChar(13).ToString(), "m", "77", "11110111010" });
            _codes.Rows.Add(new object[] { "78", Convert.ToChar(14).ToString(), "n", "78", "11000010100" });
            _codes.Rows.Add(new object[] { "79", Convert.ToChar(15).ToString(), "o", "79", "10001111010" });
            _codes.Rows.Add(new object[] { "80", Convert.ToChar(16).ToString(), "p", "80", "10100111100" });
            _codes.Rows.Add(new object[] { "81", Convert.ToChar(17).ToString(), "q", "81", "10010111100" });
            _codes.Rows.Add(new object[] { "82", Convert.ToChar(18).ToString(), "r", "82", "10010011110" });
            _codes.Rows.Add(new object[] { "83", Convert.ToChar(19).ToString(), "s", "83", "10111100100" });
            _codes.Rows.Add(new object[] { "84", Convert.ToChar(20).ToString(), "t", "84", "10011110100" });
            _codes.Rows.Add(new object[] { "85", Convert.ToChar(21).ToString(), "u", "85", "10011110010" });
            _codes.Rows.Add(new object[] { "86", Convert.ToChar(22).ToString(), "v", "86", "11110100100" });
            _codes.Rows.Add(new object[] { "87", Convert.ToChar(23).ToString(), "w", "87", "11110010100" });
            _codes.Rows.Add(new object[] { "88", Convert.ToChar(24).ToString(), "x", "88", "11110010010" });
            _codes.Rows.Add(new object[] { "89", Convert.ToChar(25).ToString(), "y", "89", "11011011110" });
            _codes.Rows.Add(new object[] { "90", Convert.ToChar(26).ToString(), "z", "90", "11011110110" });
            _codes.Rows.Add(new object[] { "91", Convert.ToChar(27).ToString(), "{", "91", "11110110110" });
            _codes.Rows.Add(new object[] { "92", Convert.ToChar(28).ToString(), "|", "92", "10101111000" });
            _codes.Rows.Add(new object[] { "93", Convert.ToChar(29).ToString(), "}", "93", "10100011110" });
            _codes.Rows.Add(new object[] { "94", Convert.ToChar(30).ToString(), "~", "94", "10001011110" });

            _codes.Rows.Add(new object[] { "95", Convert.ToChar(31).ToString(), Convert.ToChar(127).ToString(), "95", "10111101000" });
            _codes.Rows.Add(new object[] { "96", Convert.ToChar(202).ToString()/*FNC3*/, Convert.ToChar(202).ToString()/*FNC3*/, "96", "10111100010" });
            _codes.Rows.Add(new object[] { "97", Convert.ToChar(201).ToString()/*FNC2*/, Convert.ToChar(201).ToString()/*FNC2*/, "97", "11110101000" });
            _codes.Rows.Add(new object[] { "98", "SHIFT", "SHIFT", "98", "11110100010" });
            _codes.Rows.Add(new object[] { "99", "CODE_C", "CODE_C", "99", "10111011110" });
            _codes.Rows.Add(new object[] { "100", "CODE_B", Convert.ToChar(203).ToString()/*FNC4*/, "CODE_B", "10111101110" });
            _codes.Rows.Add(new object[] { "101", Convert.ToChar(203).ToString()/*FNC4*/, "CODE_A", "CODE_A", "11101011110" });
            _codes.Rows.Add(new object[] { "102", Convert.ToChar(200).ToString()/*FNC1*/, Convert.ToChar(200).ToString()/*FNC1*/, Convert.ToChar(200).ToString()/*FNC1*/, "11110101110" });
            _codes.Rows.Add(new object[] { "103", "START_A", "START_A", "START_A", "11010000100" });
            _codes.Rows.Add(new object[] { "104", "START_B", "START_B", "START_B", "11010010000" });
            _codes.Rows.Add(new object[] { "105", "START_C", "START_C", "START_C", "11010011100" });
            _codes.Rows.Add(new object[] { "", "STOP", "STOP", "STOP", "11000111010" });
        }
        
        private List<DataRow> FindStartorCodeCharacter(string s, ref int col)
        {
            var rows = new List<DataRow>();
            DataRow startCharacter = null;

            //if two chars are numbers (or FNC1) then START_C or CODE_C
            if (s.Length > 1 && (char.IsNumber(s[0]) || s[0] == Convert.ToChar(200)) && (char.IsNumber(s[1]) || s[1] == Convert.ToChar(200)))
            {
                if (startCharacter == null)
                {
                    startCharacter = _codes.Select("A = 'START_C'")[0];
                    rows.Add(startCharacter);
                }
                else
                {
                    rows.Add(_codes.Select("A = 'CODE_C'")[0]);
                }

                col = 1;
            }
            else
            {
                var aFound = false;
                var bFound = false;
                foreach (DataRow row in _codes.Rows)
                {
                    try
                    {
                        if (!aFound && s == row["A"].ToString())
                        {
                            aFound = true;
                            col = 2;

                            if (startCharacter == null)
                            {
                                startCharacter = _codes.Select("A = 'START_A'")[0];
                                rows.Add(startCharacter);
                            }
                            else
                            {
                                rows.Add(_codes.Select("B = 'CODE_A'")[0]);//first column is FNC4 so use B
                            }
                        }
                        else if (!bFound && s == row["B"].ToString())
                        {
                            bFound = true;
                            col = 1;

                            if (startCharacter == null)
                            {
                                startCharacter = _codes.Select("A = 'START_B'")[0];
                                rows.Add(startCharacter);
                            }
                            else
                                rows.Add(_codes.Select("A = 'CODE_B'")[0]);
                        }
                        else if (aFound && bFound)
                            break;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("EC128-1: " + ex.Message);
                    }
                }                

                if (rows.Count <= 0)
                    throw new Exception("EC128-2: Could not determine start character.");
            }

            return rows;
        }
        
        private string CalculateCheckDigit()
        {
            var currentStartChar = _formattedData[0];
            uint checkSum = 0;

            for (uint i = 0; i < _formattedData.Count; i++)
            {
                //replace apostrophes with double apostrophes for escape chars
                var s = _formattedData[(int)i].Replace("'", "''");

                //try to find value in the A column
                var rows = _codes.Select("A = '" + s + "'");

                //try to find value in the B column
                if (rows.Length <= 0)
                {
                    rows = _codes.Select("B = '" + s + "'");
                }

                //try to find value in the C column
                if (rows.Length <= 0)
                {
                    rows = _codes.Select("C = '" + s + "'");
                }

                var value = uint.Parse(rows[0]["Value"].ToString());
                var addition = value * ((i == 0) ? 1 : i);
                checkSum +=  addition;
            }

            var remainder = (checkSum % 103);
            var retRows = _codes.Select("Value = '" + remainder + "'");
            
            return retRows[0]["Encoding"].ToString();
        }
        
        private void BreakUpDataForEncoding()
        {
            var temp = "";
            var tempRawData = _data;
            
            //breaking the raw data up for code A and code B will mess up the encoding
            if (_code128Type == Code128Type.A || _code128Type == Code128Type.B)
            {
                foreach (var c in _data)
                {
                    _formattedData.Add(c.ToString());
                }
                
                return;
            }
            
            if (_code128Type == Code128Type.C)
            {
                if (!CheckNumericOnly(_data))
                {
                    throw new Exception("EC128-6: Only numeric values can be encoded with C128-C.");
                }

                //CODE C: adds a 0 to the front of the Data if the length is not divisible by 2
                if (_data.Length % 2 > 0)
                {
                    tempRawData = "0" + _data;
                }
            }
           
            foreach (var c in tempRawData)
            {
                if (char.IsNumber(c))
                {
                    if (temp == "")
                    {
                        temp += c;
                    }
                    else
                    {
                        temp += c;
                        _formattedData.Add(temp);
                        temp = "";
                    }
                }
                else
                {
                    if (temp != "")
                    {
                        _formattedData.Add(temp);
                        temp = "";
                    }
                    _formattedData.Add(c.ToString());
                }
            }

            //if something is still in temp go ahead and push it onto the queue
            if (temp != "")
            {
                _formattedData.Add(temp);
                temp = "";
            }
        }
        
        private void InsertStartandCodeCharacters()
        {
            DataRow currentCodeSet = null;
            var currentCodeString = "";

            if (_code128Type != Code128Type.Auto)
            {
                switch (_code128Type)
                {
                    case Code128Type.A: _formattedData.Insert(0, "START_A");
                        break;
                    case Code128Type.B: _formattedData.Insert(0, "START_B");
                        break;
                    case Code128Type.C: _formattedData.Insert(0, "START_C");
                        break;
                    default: 
                        throw new Exception("EC128-4: Unknown start type in fixed type encoding.");
                }
            }
            else
            {
                try
                {
                    for (var i = 0; i < (_formattedData.Count); i++)
                    {
                        var col = 0;
                        var tempStartChars = FindStartorCodeCharacter(_formattedData[i], ref col);

                        //check all the start characters and see if we need to stay with the same codeset or if a change of sets is required
                        var sameCodeSet = false;
                        foreach (var row in tempStartChars)
                        {
                            if (row["A"].ToString().EndsWith(currentCodeString) || row["B"].ToString().EndsWith(currentCodeString) || row["C"].ToString().EndsWith(currentCodeString))
                            {
                                sameCodeSet = true;
                                break;
                            }
                        }

                        if (currentCodeString == "" || !sameCodeSet)
                        {
                            currentCodeSet = tempStartChars[0];
                            var error = true;
                        
                            while (error)
                            {
                                try
                                {
                                    currentCodeString = currentCodeSet[col].ToString().Split(new char[] { '_' })[1];
                                    error = false;
                                }
                                catch 
                                { 
                                    error = true;

                                    if (col++ > currentCodeSet.ItemArray.Length)
                                        throw new Exception("No start character found in CurrentCodeSet.");
                                }
                            }
                            
                            _formattedData.Insert(i++, currentCodeSet[col].ToString());
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("EC128-3: Could not insert start and code characters.\n Message: " + ex.Message);
                }
            }
        }
        
        private string Encode()
        {
            //break up data for encoding
            BreakUpDataForEncoding();

            //insert the start characters
            InsertStartandCodeCharacters();

            var digit = CalculateCheckDigit();

            var encodedData = "";
            foreach (var s in _formattedData)
            {
                //handle exception with apostrophes in select statements
                var s1 = s.Replace("'", "''");
                DataRow[] eRow;

                //select encoding only for type selected
                switch (_code128Type)
                {
                    case Code128Type.A: eRow = _codes.Select("A = '" + s1 + "'");
                        break;
                    case Code128Type.B: eRow = _codes.Select("B = '" + s1 + "'");
                        break;
                    case Code128Type.C: eRow = _codes.Select("C = '" + s1 + "'");
                        break;
                    case Code128Type.Auto: eRow = _codes.Select("A = '" + s1 + "'");
                        if (eRow.Length <= 0)
                        {
                            eRow = _codes.Select("B = '" + s1 + "'");

                            if (eRow.Length <= 0)
                            {
                                eRow = _codes.Select("C = '" + s1 + "'");
                            }
                        }
                        break;
                    default: eRow = null;
                        break;
                }              

                if (eRow == null || eRow.Length <= 0)
                    throw new Exception("EC128-5: Could not find encoding of a value( " + s1 + " ) in C128 type " + _code128Type);

                encodedData += eRow[0]["Encoding"].ToString();
                _encodedData.Add(eRow[0]["Encoding"].ToString());
            }

            //add the check digit
            encodedData += CalculateCheckDigit();
            _encodedData.Add(CalculateCheckDigit());

            //add the stop character
            encodedData += _codes.Select("A = 'STOP'")[0]["Encoding"].ToString();
            _encodedData.Add(_codes.Select("A = 'STOP'")[0]["Encoding"].ToString());

            //add the termination bars
            encodedData += "11";
            _encodedData.Add("11");

            return encodedData;
        }
    }
}
