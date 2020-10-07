using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models_
{
    public class HuffmanCom 
    {
        public string originalName { get; set; }
        public string compressedFilePath { get; set; }
        public double compressionRatio { get; set; }
        public double compressionFactor { get; set; }
        public double reductionPercentage { get; set; }

    }
}
