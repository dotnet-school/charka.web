using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Chakra.Web.Requests;
using Chakra.Web.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Chakra.Web.Controllers
{
  [ApiController]
  [Route("convert")]
  [EnableCors("MyPolicy")]
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
        var responseObject = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(result);
        return ConverterResponse.ForSuccess(responseObject);
      }
      catch (DynamicCompilationException e)
      {
        return ConverterResponse.ForError(e);
      }
    }
  }
}