using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using Xunit;

using static Chakra.Web.Test.Api.ApiTestHelper;

namespace Chakra.Web.Test.Api
{
  public class CompileApiTest : IClassFixture<WebApplicationFactory<Chakra.Web.Startup>>
  {
    private HttpClient _client;
    
    public CompileApiTest(WebApplicationFactory<Chakra.Web.Startup> fixture)
    {
      _client = fixture.CreateClient();
    }

    [Fact]
    public async Task ShouldCompileASnippet()
    {
      var request = new Dictionary<string, object>()
      {
              ["snippet"] = new [] {
                      "Console.WriteLine($\"Hello\");",
                      "Console.WriteLine($\"World!\");"
              }
      };

      var expected = new Dictionary<string, object>() {
              ["consoleOutput"] = new JArray("Hello", "World!"),
              ["error"] = null,
              ["errorLineNumber"] = 0L
      };

      await ExpectResponse(request, expected);
    }

    private async Task ExpectResponse(Dictionary<string, object> request,  Dictionary<string, object> expected)
    {
      var response = await SendRequest("/compile", request, _client.PutAsync);
      var actual = await BodyOf(response);
      
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.Equal(expected, actual);
    }

  }
}