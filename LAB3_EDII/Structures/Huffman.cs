using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace CustomGenerics.Structures
{
    class Huffman<T> : ICompressor
    {
        #region Variables
        public HuffmanNode<T> Root;
        public int NextId = 1;
        #endregion

        public void Compress()
        {
            throw new NotImplementedException();
        }

        public void Decompress()
        {
            throw new NotImplementedException();
        }
        public PQNode<T> Insert(PQNode<T> Node1, PQNode<T> Node2)
        {
            HuffmanNode<T> Right = new HuffmanNode<T>(Node1.Key, Node1.Priority);
            HuffmanNode<T> Left = new HuffmanNode<T>(Node2.Key, Node2.Priority);
            byte Id = Convert.ToByte("N" + Convert.ToString(NextId));
            HuffmanNode<T> Father = new HuffmanNode<T>(Id, (Node1.Priority + Node2.Priority));
            Father.Rightson = Right;
            Father.Leftson = Left;
        }

    }
}
