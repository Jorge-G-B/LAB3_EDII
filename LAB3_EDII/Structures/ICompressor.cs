using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CustomGenerics.Structures
{
    interface ICompressor
    {
        Task<string> CompressFile(IFormFile file);
        Task<string> DecompressFile(IFormFile file);
        string CompressText(string text);
        string DecompressText(string text);
    }
}
