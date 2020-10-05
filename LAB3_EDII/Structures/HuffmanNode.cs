using System;
using System.Collections.Generic;
using System.Text;

namespace CustomGenerics.Structures
{
    class HuffmanNode<T> where T : IProbability, new()
    {
        #region Variables
        public byte ID;
        public double Chance;
        public HuffmanNode<T> Rightson;
        public HuffmanNode<T> Leftson;
        #endregion
        public HuffmanNode(byte newid, double newchance)
        {
            ID = newid;
            Chance = newchance;
        }
    }
}
