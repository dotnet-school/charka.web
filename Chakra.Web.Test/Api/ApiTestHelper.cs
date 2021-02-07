using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chakra.Web.Test.Api
{
  public static class ApiTestHelper
  {
    public static async Task<HttpResponseMessage> SendRequest(string url, object request, Func<string, HttpContent, Task<HttpResponseMessage>> @async)
    {
      return await @async(url, new StringContent(await JsonConvert.SerializeObjectAsync(request), Encoding.UTF8, "application/json"));
    }

    
    public static async Task<IDictionary<string, object>> BodyOf(HttpResponseMessage response)
    {
      return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
    }
  }
}