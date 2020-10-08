using CustomGenerics.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Structures
{
    public class Huffman<T> : ICompressor where T : IProbability, new()
    {
        #region Variables
        HuffmanNode<T> Root;
        public int NextId = 1;
        FileStream DestinyFile;
        string FilePath;
        string DestinyFileName;
        string OriginFileName;
        Dictionary<byte, HuffmanNode<T>> BytesDictionary;
        #endregion

        public Huffman(string filePath)
        {
            FilePath = filePath;
        }

        public async Task<string> CompressFile(IFormFile file)
        {
            OriginFileName = file.FileName;
            using var saver = new FileStream($"{FilePath}/{OriginFileName}", FileMode.OpenOrCreate);
            await file.CopyToAsync(saver);

            using var reader = new BinaryReader(saver);
            int bufferSize = 2000000;
            var buffer = new byte[bufferSize];
            BytesDictionary = new Dictionary<byte, HuffmanNode<T>>();
            saver.Position = saver.Seek(0, SeekOrigin.Begin);

            while (saver.Position != saver.Length)
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

            double BytesCount = 0.00;
            foreach (var Node in BytesDictionary.Values)
            {
                BytesCount += Node.Value.GetFrequency();
            }

            PriorityQueue<HuffmanNode<T>> priorityQueue = new PriorityQueue<HuffmanNode<T>>();

            foreach (var Node in BytesDictionary.Values)
            {
                Node.Value.CalculateProbability(BytesCount);
                priorityQueue.AddValue(Node, Node.Value.GetProbability());
            }

            T NewNodeValue = new T();
            while (priorityQueue.DataNumber != 1)
            {
                var Node1 = priorityQueue.GetFirst();
                var Node2 = priorityQueue.GetFirst();
                NewNodeValue.SetProbability(Node1.Value.GetProbability() + Node2.Value.GetProbability());
                var NewNode = new HuffmanNode<T>(NewNodeValue);
                Node1.Father = NewNode;
                Node2.Father = NewNode;
                if (Node1.Value.GetProbability() < Node2.Value.GetProbability())
                {
                    NewNode.Leftson = Node2;
                    NewNode.Rightson = Node1;
                }
                else
                {
                    NewNode.Rightson = Node2;
                    NewNode.Leftson = Node1;
                }
                priorityQueue.AddValue(NewNode, NewNode.Value.GetProbability());
                Root = NewNode;
            }


                
            SetCode(Root, "");
            //Ir leyendo el texto y transformarlo hacia el nuevo código.
            saver.Position = saver.Seek(0, SeekOrigin.Begin);
            string FinalText = "";
            string Metadata = "";
            Metadata += Char.ConvertFromUtf32(BytesDictionary.Values.Count); // Convertir este número a su representación en ascii
            int maxValue = BytesDictionary.Values.Max(x => x.Value.GetFrequency());
            var intBytes = ByteGenerator.ConvertToBytes(maxValue.ToString());
            Metadata += Char.ConvertFromUtf32(intBytes.Length);
            byte[] numInBytes = new byte[intBytes.Length]; //Revisar de llenar los demás espacios con 00000 en cada valor
            foreach (var byteObject in BytesDictionary.Values)
            {
                Metadata += Encoding.ASCII.GetString(new byte[] { byteObject.Value.GetValue() }) + 
                Encoding.ASCII.GetString(new byte[] { Convert.ToByte(byteObject.Value.GetFrequency().ToString()) });
            }
            //Aqui aún hace falta ir poniendo el byte y luego su frecuencia en binario.

            while (saver.Position != saver.Length - 1)
            {
                buffer = reader.ReadBytes(bufferSize);
                foreach (var byteData in buffer)
                {
                    FinalText += BytesDictionary[byteData].Code;
                }
            }

            while (FinalText.Length % 8 == 0)
            {
                FinalText += "0";
            }

            var stringBytes = Enumerable.Range(0, FinalText.Length / 8).Select(x => FinalText.Substring(x * 8, 8));
            FinalText = "";
            byte[] bytes = new byte[1];
            foreach (var byteData in stringBytes)
            {
                bytes[0] = Convert.ToByte(byteData, 2);
                FinalText += Encoding.UTF8.GetString(bytes);
            }

            string savingText = Metadata + FinalText;
            DestinyFileName = $"Compressed_{file.Name}.huff";
            var newFile = new FileStream($"{FilePath}/{DestinyFileName}", FileMode.OpenOrCreate);
            var writer = new BinaryWriter(newFile);
            writer.Write(savingText);
            newFile.Close();
            writer.Close();
            return $"{FilePath}/{DestinyFileName}";
        }

        public Task<string> DecompressFile(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public void CompressText(string text)
        {
            throw new NotImplementedException();
        }

        public string DecompressText(string text)
        {
            throw new NotImplementedException();
        }

        private void SetCode(HuffmanNode<T> node, string prevCode)
        {
            if (node.Father != null)
            {
                if (node.Father.Leftson == node)
                {
                    node.Code = $"{prevCode}0";
                }
                else
                {
                    node.Code = $"{prevCode}1";
                }

                if (BytesDictionary.ContainsKey(node.Value.GetValue()))
                {
                    BytesDictionary[node.Value.GetValue()].Code = node.Code;
                }
            }
            else
            {
                node.Code = "";
            }

            if (node.Leftson != null)
            {
                SetCode(node.Leftson, node.Code);
            }

            if (node.Rightson != null)
            {
                SetCode(node.Rightson, node.Code);
            }
        }
        
    }
}
