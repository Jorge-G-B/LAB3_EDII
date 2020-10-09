using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models_;
using CustomGenerics.Structures;


namespace API.Helpers_
{
    public class Storage
    {
        private static Storage _instance = null;

        public static Storage Instance
        {
            get
            {
                if (_instance == null) _instance = new Storage();
                return _instance;
            }
        }
        public Huffman<HuffmanChar> HuffmanTree;
        public List<HuffmanCom> HistoryList = new List<HuffmanCom>();
    }
}
