using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomGenerics.Structures;

namespace API.Models_
{
    public class HuffmanCom 
    {
        public string OriginalName { get; set; }
        public string CompressedFilePath { get; set; }
        public double CompressionRatio { get; set; }
        public double CompressionFactor { get; set; }
        public double ReductionPercentage { get; set; }

        public HuffmanCom()
        {

        }

        public void GetRatio(int bNC, int bNO)
        {
            CompressionRatio = bNC / bNO;
        }

        public void GetFactor(int NumBC, int NumBO)
        {
            CompressionFactor = NumBO / NumBC;
        }

        public void RPercentage()
        {
            ReductionPercentage = CompressionRatio * 100;
        }
    }
}
