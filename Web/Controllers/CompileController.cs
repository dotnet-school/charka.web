using System;
using Microsoft.AspNetCore.Mvc;
using Web.Responses;

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
        public CompileResponse CompileCode(CompileRequest request)
        {
            return CompileResponse.ForSuccess(Chakra.Executor.ExecuteSnippet(request.Snippet));
        }
    }
}
