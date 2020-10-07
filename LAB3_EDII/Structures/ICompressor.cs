using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CustomGenerics.Structures
{
    interface ICompressor
    {
        string CompressFile(IFormFile file);
        void CompressString(string text);
        void DecompressFile(IFormFile file);
        string DecompressText(string text);
    }
}
