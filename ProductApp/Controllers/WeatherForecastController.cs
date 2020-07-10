using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SuperMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        public IActionResult Import()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Choose json or xml format that is more suitable for your project
                // Alternatively API token can be supplied in Authorization Header instead of being part of URL
                // e.g. client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Engage", "7JK_GPoJc_IDML7XFsuwtvnMaTV1m9XQDQQ4D15CX9i1VgLMcoXqZ94nlwvHoj76EE30PK0lIs6jE1Mik2SyFWfRZRdIWIHGe6DB9i2av1sxhkD6xQOMtjWpyKvuMrxSlOX-z-b2008");
                var queryString = string.Format("?format={0}&apiToken={1}", "json", "7JK_GPoJc_IDML7XFsuwtvnMaTV1m9XQDQQ4D15CX9i1VgLMcoXqZ94nlwvHoj76EE30PK0lIs6jE1Mik2SyFWfRZRdIWIHGe6DB9i2av1sxhkD6xQOMtjWpyKvuMrxSlOX-z-b2008");
                client.BaseAddress = new Uri("https://api.ubiquity.co.nz/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var r = client.PostAsync()

                var response = client.PostAsync("database/contacts/import/file" + queryString, new StringContent("{ Data to be posted }"), "application/json").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    // Successful: parse json/xml from responseBody and proceed
                }
                else
                {
                    // Failed: logic that handles unsuccessful response
                }
            }
            return Ok();
        }

    }
}
