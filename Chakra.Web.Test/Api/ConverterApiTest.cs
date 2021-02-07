using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

using static Chakra.Web.Test.Api.ApiTestHelper;

namespace Chakra.Web.Test.Api
{
  public class ConverterApiTest : IClassFixture<WebApplicationFactory<Startup>>
  {
    private HttpClient _client;
    
    public ConverterApiTest(WebApplicationFactory<Startup> fixture)
    {
      _client = fixture.CreateClient();
    }

    [Fact]
    public async Task ShouldCompileASnippet()
    {
      var request = new
      {
        csharp = @"new {
	        user = new {name = ""one"", id = 302},
	        friends =  new [] {
			        new {name = ""one"", id = 301},
              new {name = ""two"", id = 303},
              new {name = ""three"", id = 304},
		        },
          salary = 3232.30m,
          active = true
        }"
      };

      var expected = new
      {
        json="{\"user\":{\"name\":\"one\",\"id\":302},\"friends\":[{\"name\":\"one\",\"id\":301},{\"name\":\"two\",\"id\":303},{\"name\":\"three\",\"id\":304}],\"salary\":3232.30,\"active\":true}"
      };
      
      var response = await SendRequest("/convert", request, _client.PutAsync);
      var responseString = await response.Content.ReadAsStringAsync();
      var actual = JsonConvert.DeserializeAnonymousType(responseString, expected);

      Assert.Equal(expected, actual);
    }
  }
}

