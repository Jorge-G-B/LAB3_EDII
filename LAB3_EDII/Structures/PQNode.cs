using System;
using System.Collections.Generic;
using System.Text;

namespace CustomGenerics.Structures
{
    internal class PQNode<T>
    {
        public PQNode<T> Father;
        public PQNode<T> RightSon;
        public PQNode<T> LeftSon;
        public T Key;
        public double Priority;
    }
}
