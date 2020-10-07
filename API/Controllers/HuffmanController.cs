using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Helpers_;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/<HuffmanController>
        [HttpGet]
        [Route("api/compressions")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
                return Ok();
            }
            catch
            {

                return StatusCode(500); 
            }
        }

        // POST api/<HuffmanController>
        [HttpPost]
        [Route("api/compress/{name}")]
        public IActionResult PostHuffman(string name)
        {
            try
            {


                return Ok();
            }
            catch
            {
                return StatusCode(500);
                
            }
        }
    }
}
