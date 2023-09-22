using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_DOAN.Models
{
    public class RANDOMID
    {
        public RANDOMID() { }
        public string GenerateRandomString(int n)
        {
            Random res = new Random();

            // String that contain both alphabets and numbers
            string str = "abcdefghijklmnopqrstuvwxyz0123456789";

            // Initializing the empty string
            string randomstring = "";

            for (int i = 0; i < n; i++)
            {

                // Selecting a index randomly
                int x = res.Next(str.Length);

                // Appending the character at the 
                // index to the random alphanumeric string.
                randomstring = randomstring + str[x];
            }
            return randomstring;
        }
    }
}