using Chakra;
using Chakra.Web.Requests;
using Chakra.Web.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Chakra.Web.Controllers
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
