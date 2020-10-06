using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace CustomGenerics.Structures
{
    interface ICompressor
    {
        void Compress(IFormFile file);
        void Decompress(IFormFile file);
    }
}
