using System.Linq;

namespace NetBarcode.Types
{
    internal abstract class Base
    {
        protected bool CheckNumericOnly(string data)
        {
            //This function takes a string of data and breaks it into parts and trys to do Int64.TryParse
            //This will verify that only numeric data is contained in the string passed in.  The complexity below
            //was done to ensure that the minimum number of interations and checks could be performed.

            //early check to see if the whole number can be parsed to improve efficency of this method

            if (data != null)
            {
                if (long.TryParse(data, out _))
                    return true;
            }
            else
            {
                return false;
            }

            //9223372036854775808 is the largest number a 64bit number(signed) can hold so ... make sure its less than that by one place
            const int stringLengths = 18;

            var temp = data;
            var strings = new string[(data.Length / stringLengths) + ((data.Length % stringLengths == 0) ? 0 : 1)];

            var i = 0;
            
            while (i < strings.Length)
            {
                if (temp.Length >= stringLengths)
                {
                    strings[i++] = temp.Substring(0, stringLengths);
                    temp = temp.Substring(stringLengths);
                }
                else
                {
                    strings[i++] = temp.Substring(0);
                }
            }

            return strings.All(s => long.TryParse(s, out _));
        }
    }
}