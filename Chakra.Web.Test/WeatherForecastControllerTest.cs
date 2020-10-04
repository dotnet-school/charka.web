using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Chakra.Web.Test
{
    public class WeatherForecastControllerTest: IClassFixture<WebApplicationFactory<Chakra.Web.Startup>>
    {
        private HttpClient _client { get; }

        public WeatherForecastControllerTest(WebApplicationFactory<Chakra.Web.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }
       
        [Fact]
        public async Task Get_Should_Retrieve_Forecast()
        {
            var response = await _client.GetAsync("/weatherforecast");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var forecast = JsonConvert.DeserializeObject<WeatherForecast[]>(await response.Content.ReadAsStringAsync());
            Assert.Equal(5, forecast.Length);

        }
    }
}
