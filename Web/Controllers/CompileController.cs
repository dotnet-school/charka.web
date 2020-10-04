using System;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompileController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "hello";
        }
        
        [HttpPut]
        public string CompileCode(CompileRequest request)
        {
            return string.Join(", ", request.Snippet ?? Array.Empty<string>());
        }
    }
}
