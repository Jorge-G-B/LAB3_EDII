using System;
using System.Collections.Generic;
using System.Text;

namespace CustomGenerics.Structures
{
    interface IProbability
    {
        double Probability { get; set; }
        int Frequency { get; set; }
        byte Value { get; set; }
        void AddFrecuency();
        void SetByte(byte value);
        void SetProbability(double totalBytes);
    }
}
