using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Web.Test
{
    public class WeatherForecastControllerTest: IClassFixture<WebApplicationFactory<Web.Startup>>
    {
        private HttpClient _client { get; }

        public WeatherForecastControllerTest(WebApplicationFactory<Web.Startup> fixture)
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
