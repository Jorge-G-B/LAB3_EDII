using System;
using System.Collections.Generic;
using System.Text;

namespace CustomGenerics.Utilities
{
    class ByteGenerator
    {
        static Encoding e = Encoding.GetEncoding("iso-8859-1");
        public static byte[] ConvertToBytes(string text)
        {
            return e.GetBytes(text);
        }

        public static string ConvertToString(byte[] bytes)
        {
            return e.GetString(bytes);

        }
    }
}
