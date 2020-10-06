﻿using Microsoft.AspNetCore.Http;
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

        public async void CompressFile(IFormFile file)
        {
            DestinyFileName = $"Compressed_{file.Name}.huff";
            OriginFileName = file.FileName;
            using var saver = new FileStream($"{FilePath}/{OriginFileName}", FileMode.OpenOrCreate);
            await file.CopyToAsync(saver);

            using var reader = new BinaryReader(saver);
            int bufferSize = 2000000;
            var buffer = new byte[bufferSize];
            Dictionary<byte, HuffmanNode<T>> BytesDictionary = new Dictionary<byte, HuffmanNode<T>>();

            while (saver.Position != saver.Length - 1)
            {
                buffer = reader.ReadBytes(bufferSize);
                T value = new T();
                HuffmanNode<T> Node;
                foreach (var byteData in buffer)
                {
                    if (!BytesDictionary.ContainsKey(byteData))
                    {
                        value.SetByte(byteData);
                        Node = new HuffmanNode<T>(value);
                        BytesDictionary.Add(byteData, Node);
                    }
                    BytesDictionary[byteData].Value.AddFrecuency();
                    value = new T();
                }
            }

            var differentBytesCount = 0.00;
            foreach (var Node in BytesDictionary.Values)
            {
                differentBytesCount += Node.Value.Probability;
            }

            PriorityQueue<HuffmanNode<T>> priorityQueue = new PriorityQueue<HuffmanNode<T>>();

            foreach (var Node in BytesDictionary.Values)
            {
                Node.Value.SetProbability(differentBytesCount);
                priorityQueue.AddValue(Node, Node.Value.Probability);
            }

            T NewNodeValue = new T();
            while (priorityQueue.DataNumber != 1)
            {
                var Node1 = priorityQueue.GetFirst();
                var Node2 = priorityQueue.GetFirst();
                NewNodeValue.Probability = Node1.Value.Probability + Node2.Value.Probability;
                var NewNode = new HuffmanNode<T>(NewNodeValue);
                if (Node1.Value.Probability < Node2.Value.Probability)
                {
                    NewNode.Leftson = Node2;
                    NewNode.Rightson = Node1;
                }
                priorityQueue.AddValue(NewNode, NewNode.Value.Probability);
            }

            // Aquí debemos agregar el código para huffman y ya luego ir leyendo el texto y transformarlo hacia el nuevo código.
        }


        public void CompressString(string text)
        {
            throw new NotImplementedException();
        }

        public void DecompressFile(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public string DecompressText(string text)
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
