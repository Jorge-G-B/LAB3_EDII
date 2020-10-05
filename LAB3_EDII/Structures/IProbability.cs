using System;
using System.Collections.Generic;
using System.Text;

namespace CustomGenerics.Structures
{
    interface IProbability
    {
        double Probability { get; set; }
        void AddFrecuency();
        void SetProbability(int totalBytes);
    }
}
