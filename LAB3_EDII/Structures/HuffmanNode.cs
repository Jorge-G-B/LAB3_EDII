using System;
using System.Collections.Generic;
using System.Text;

namespace CustomGenerics.Structures
{
    class HuffmanNode<T> where T : IProbability, new()
    {
        #region Variables
        public T Value;
        public string Code;
        public HuffmanNode<T> Father;
        public HuffmanNode<T> Rightson;
        public HuffmanNode<T> Leftson;
        #endregion

        public HuffmanNode(T value)
        {
            Value = value;
        }
    }
}
