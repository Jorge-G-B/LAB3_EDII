using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace CustomGenerics.Structures
{
    class Huffman<T> : ICompressor where T : IProbability, new()
    {
        #region Variables
        public HuffmanNode<T> Root;
        public int NextId = 1;
        FileStream DestinyFile;
        FileStream OriginFile;
        string FilePath;
        string DestinyFileName;
        string OriginFileName;
        #endregion

        public Huffman(string filePath)
        {
            FilePath = filePath;
        }

        public async void Compress(IFormFile file)
        {
            DestinyFileName = $"Compressed_{file.Name}.huff";
            OriginFileName = file.FileName;
            using var saver = new FileStream($"{FilePath}/{OriginFileName}", FileMode.OpenOrCreate);
            await file.CopyToAsync(saver);

            using var reader = new BinaryReader(saver);
            int bufferSize = 2000000;
            var buffer = new byte[bufferSize];
            Dictionary<byte, T> BytesDictionary = new Dictionary<byte, T>();

            while (saver.Position != saver.Length - 1)
            {
                buffer = reader.ReadBytes(bufferSize);
                T value = new T();
                foreach (var byteData in buffer)
                {
                    if (!BytesDictionary.ContainsKey(byteData))
                    {
                        value.SetByte(byteData);
                        BytesDictionary.Add(byteData, value);
                    }
                    BytesDictionary[byteData].AddFrecuency();
                    value = new T();
                }
            }

            var differentBytes = 0.00;
            foreach (var byteValue in BytesDictionary.Values)
            {
                differentBytes += byteValue.Probability;
            }

            PriorityQueue<T> priorityQueue = new PriorityQueue<T>();

            foreach (var byteValue in BytesDictionary.Values)
            {
                byteValue.SetProbability(differentBytes);
                priorityQueue.AddValue(byteValue, byteValue.Probability);
            }

            T NewNodeValue = new T();
            while (priorityQueue.DataNumber != 1)
            {
                // Pensar como no perder los hijos que tiene en huffman al insertarlo en la cola de prioridad
                // Una solución puede ser hacer tanto el dicionario como la cola de prioridad de nodos de huffman
            }
        }

        public void Decompress(IFormFile file)
        {
            throw new NotImplementedException();
        }

        //public PQNode<T> Insert(PQNode<T> Node1, PQNode<T> Node2)
        //{
        //    HuffmanNode<T> Right = new HuffmanNode<T>(Node1.Key, Node1.Priority);
        //    HuffmanNode<T> Left = new HuffmanNode<T>(Node2.Key, Node2.Priority);
        //    byte Id = Convert.ToByte("N" + Convert.ToString(NextId));
        //    HuffmanNode<T> Father = new HuffmanNode<T>(Id, (Node1.Priority + Node2.Priority));
        //    Father.Rightson = Right;
        //    Father.Leftson = Left;
        //}
    }
}
