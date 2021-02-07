using System;
using Chakra.Web.Requests;
using Chakra.Web.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Chakra.Web.Controllers
{
  [ApiController]
  [Route("convert")]
  public class JsonController
  {
    [HttpPut]
    public ConverterResponse CompileCode(ConverterRequest request)
    {
      string snippet = @"
       var csharp = __$chshar-snippet$__;
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(csharp));  
      ".Replace("__$chshar-snippet$__", request.Csharp);

      try
      {
        var result = Executor.ExecuteSnippet(snippet.Split(Environment.NewLine));
        return ConverterResponse.ForSuccess(result);
      }
      catch (DynamicCompilationException e)
      {
        return ConverterResponse.ForError(e);
      }
    }
  }
}