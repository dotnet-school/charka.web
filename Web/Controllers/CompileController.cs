using Chakra;
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
            try
            {
                return CompileResponse.ForSuccess(Executor.ExecuteSnippet(request.Snippet));
            }
            catch (DynamicCompilationException e)
            {
                return CompileResponse.ForError(e);
            }
        }
    }
}
