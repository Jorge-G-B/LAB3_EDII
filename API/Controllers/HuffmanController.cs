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
        public List<HuffmanCom> GetHuffmanComs()
        {
            return new List<HuffmanCom>();
        }

        [HttpGet]
        public IEnumerable<string> TryGet()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<HuffmanController>
        [HttpGet]
        [Route("api/compressions")]
        public IActionResult GetLCompress([FromForm] IFormFile file)
        {
            try
            {
                var text = "";
                //var CompressionsL = JsonSerializer.Serialize<List<HuffmanCom>>(text);
                return Ok();
            }
            catch 
            {

                
                return StatusCode(500); 
            }
        }

        // GET api/<HuffmanController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HuffmanController>
        [HttpPost]
        [Route("api/decompress")]
        public IActionResult PostDecompress([FromForm] IFormFile file)
        {
            try
            {
                Storage.Instance.HuffmanTree = new Huffman<HuffmanChar>($"{Environment.ContentRootPath}");
                Storage.Instance.HuffmanTree.DecompressFile(file);
                return PhysicalFile($"{Environment.ContentRootPath}/{file.Name}Decompressed", ".txt"); 
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
                using var saver = new FileStream($"{Environment.ContentRootPath}/{file.Name}", FileMode.OpenOrCreate);
                await file.CopyToAsync(saver);
                var CountBytesO = System.IO.File.ReadAllBytes($"{Environment.ContentRootPath}/{file.Name}");
                int BNO = CountBytesO.Count();
                
                Storage.Instance.HuffmanTree.CompressFile(file, name);
                
                using var saver2 = new FileStream($"{Environment.ContentRootPath}/{file.Name}", FileMode.OpenOrCreate);
                await file.CopyToAsync(saver);
                var CountBytesC = System.IO.File.ReadAllBytes($"{Environment.ContentRootPath}/{name}");
                int BNC = CountBytesO.Count();

                var HuffmanInfo = new HuffmanCom();
                HuffmanInfo.GetRatio(BNC, BNO);
                HuffmanInfo.GetFactor(BNC, BNO);
                HuffmanInfo.RPercentage();
                
                return PhysicalFile($"{Environment.ContentRootPath}/{name}", MediaTypeNames.Text.Plain, $"{name}.huff");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
