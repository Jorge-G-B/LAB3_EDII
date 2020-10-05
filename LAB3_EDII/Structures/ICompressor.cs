using System;
using System.Collections.Generic;
using System.Text;

namespace CustomGenerics.Structures
{
    interface ICompressor
    {
        void Compress(string filePath);
        void Decompress(string filePath);
    }
}
