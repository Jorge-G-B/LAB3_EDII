using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Helpers_;
using CustomGenerics.Structures;
using Microsoft.AspNetCore.Http;

namespace API.Models_
{
    public class HuffmanCom 
    {
        public string OriginalName { get; set; }
        public string CompressedName { get; set; }
        public string CompressedFilePath { get; set; }
        public double CompressionRatio { get; set; }
        public double CompressionFactor { get; set; }
        public double ReductionPercentage { get; set; }

        public HuffmanCom() { }

        public void SetAttributes(string path, string prevName, string newName)
        {
            var CountBytesO = System.IO.File.ReadAllBytes($"{path}/{prevName}");
            int BNO = CountBytesO.Count();
            var CountBytesC = System.IO.File.ReadAllBytes($"{path}/{newName}");
            int BNC = CountBytesC.Count();

            OriginalName = prevName;
            CompressedName = newName;
            GetRatio(BNC, BNO);
            GetFactor(BNC, BNO);
            RPercentage();

            LoadHistList(path);
            Storage.Instance.HistoryList.Add(this);
            var file = new FileStream($"{path}/CompressionHist", FileMode.OpenOrCreate);
            var BytesToWrite = JsonSerializer.Serialize(Storage.Instance.HistoryList, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            using var writer = new BinaryWriter(file);
            writer.Write(BytesToWrite);
            writer.Close();
            file.Close();
        }

        public static void LoadHistList(string path)
        {
            using var content = new MemoryStream();
            var file = new FileStream($"{path}/CompressionHist", FileMode.OpenOrCreate);
            if (file.Length != 0)
            {
                file.CopyToAsync(content);
                var text = Encoding.ASCII.GetString(content.ToArray());
                var list = JsonSerializer.Deserialize<List<HuffmanCom>>(text, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                foreach (var item in list)
                {
                    Storage.Instance.HistoryList.Add(item);
                }
            }
            file.Close();
        }

        public void GetRatio(int bNC, int bNO)
        {
            CompressionRatio = bNC / bNO;
        }

        public void GetFactor(int NumBC, int NumBO)
        {
            CompressionFactor = NumBO / NumBC;
        }

        public void RPercentage()
        {
            ReductionPercentage = CompressionRatio * 100;
        }
    }
}
