using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CustomGenerics.Structures
{
    interface ICompressor
    {
        void CompressFile(IFormFile file, string name);
        void DecompressFile(IFormFile file);
        void CompressText(string text);
        string DecompressText(string text);
    }
}
