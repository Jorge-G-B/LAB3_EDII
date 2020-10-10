using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Helpers_;
using API.Models_;
using CustomGenerics.Structures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.ComponentModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuffmanController : ControllerBase
    {
        private IWebHostEnvironment Environment;

        public HuffmanController(IWebHostEnvironment env)
        {
            Environment = env;
        }

        [HttpGet]
        public IEnumerable<string> TryGet()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<HuffmanController>
        [HttpGet]
        [Route("compressions")]
        public List<HuffmanCom> GetListCompress()
        {
            HuffmanCom.LoadHistList(Environment.ContentRootPath);
            return Storage.Instance.HistoryList;
        }

        // GET api/<HuffmanController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HuffmanController>
        [HttpPost]
        [Route("decompress")]
        public async Task<IActionResult> PostDecompress([FromForm] IFormFile file)
        {
            try
            {
                Storage.Instance.HuffmanTree = new Huffman<HuffmanChar>($"{Environment.ContentRootPath}");
                HuffmanCom.LoadHistList(Environment.ContentRootPath);
                var name = "";
                foreach (var item in Storage.Instance.HistoryList)
                {
                    if (item.CompressedName == file.FileName)
                    {
                        name = item.OriginalName;
                    }
                }
                await Storage.Instance.HuffmanTree.DecompressFile(file, name);
                return PhysicalFile($"{Environment.ContentRootPath}/{name}", MediaTypeNames.Text.Plain, ".txt"); 
            }
            catch
            {
                return StatusCode(500); 
            }
        }

        // POST api/<HuffmanController>
        [HttpPost]
        [Route("compress/{name}")]
        public async Task<IActionResult> PostHuffmanAsync([FromForm] IFormFile file, string name)
        {
            try
            {
                Storage.Instance.HuffmanTree = new Huffman<HuffmanChar>($"{Environment.ContentRootPath}");
                int i = 1;
                var originalname = name;
                while (System.IO.File.Exists($"{Environment.ContentRootPath}/{name}"))
                {
                    name = originalname + "(" + i.ToString() + ")";
                    i++;
                }
                await Storage.Instance.HuffmanTree.CompressFile(Environment.ContentRootPath, file, name);
                var HuffmanInfo = new HuffmanCom();
                HuffmanInfo.SetAttributes(Environment.ContentRootPath, file.FileName, name);
                Storage.Instance.HistoryList.Add(HuffmanInfo);
                
                return PhysicalFile($"{Environment.ContentRootPath}/{name}", MediaTypeNames.Text.Plain, $"{name}.huff");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
