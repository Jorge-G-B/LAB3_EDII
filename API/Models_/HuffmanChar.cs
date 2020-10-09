using CustomGenerics.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models_
{
    public class HuffmanChar : IProbability
    {
        int Frequency;
        double Probability;
        byte Value;

        public HuffmanChar() { }

        public void AddFrecuency()
        {
            Frequency++;
        }

        public void CalculateProbability(double totalBytes)
        {
            Probability = Convert.ToDouble(Frequency) / totalBytes;
        }

        public int GetFrequency()
        {
            return Frequency;
        }

        public double GetProbability()
        {
            return Probability;
        }

        public byte GetValue()
        {
            return Value;
        }

        public void SetByte(byte value)
        {
            Value = value;
        }

        public void SetProbability(double number)
        {
            Probability = number;
        }
    }
}
