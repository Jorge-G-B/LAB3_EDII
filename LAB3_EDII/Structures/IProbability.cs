using System;
using System.Collections.Generic;
using System.Text;

namespace CustomGenerics.Structures
{
    public interface IProbability
    {
        double GetProbability();
        int GetFrequency();
        byte GetValue();
        void AddFrecuency();
        void SetByte(byte value);
        void CalculateProbability(double totalBytes);
        void SetProbability(double number);
    }
}
