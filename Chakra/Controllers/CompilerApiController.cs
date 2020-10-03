using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chakra.Controllers
{
    [ApiController]
    [Route("api/compiler")]
    public class CompilerApiController : ControllerBase
    {
        private readonly ILogger<CompilerApiController> _logger;

        public CompilerApiController(ILogger<CompilerApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "hello";
        }
        
        [Route("snippet")]
        [HttpPost]
        public string[] Get(CompileSnippetRequest request)
        { 
            _logger.Log(LogLevel.Information, "Received request to compile.");
            return request.Snippet;
        }
    }
}
