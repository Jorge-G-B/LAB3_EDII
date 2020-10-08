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

        internal static double GetRatio(int bNC, int bNO)
        {
            int ratio = bNC / bNO;
            return ratio;
        }
        internal static double GetFactor(int NumBC, int NumBO)
        {
            int factor = NumBO / NumBC;
            return factor;
        }
        internal static double RPercentage(double ratio)
        {
            double percentage = ratio * 100;
            return percentage;

        }
    }
}
