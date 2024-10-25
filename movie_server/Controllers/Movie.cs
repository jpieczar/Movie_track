using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using movie_server.Models;

namespace movie_server.Controllers
{
    [ApiController]
    [Route("movie")]
    public class MovieController : ControllerBase
    {
        private static readonly string key = "";
        private static readonly string URL = "https://www.omdbapi.com/";

        private static async Task<string> FetchFilm(IQueryCollection paramlist)
        {
            string request = $"{URL}?apikey={key}";
            string jsonResponse = "";

            foreach (var param in paramlist)
            {
                request += $"&{param.Key}={param.Value}";
            }
            Console.WriteLine(request);
            Console.WriteLine("++++++++++++++++");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    DBConnect.queryInsert(request, (int)response.StatusCode, paramlist);
                    jsonResponse = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("Unable to fetch upstream data");
                }
            }
            return (jsonResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovie([FromQuery] AllowedParams allowedParams)
        {
            var hashParams = new HashSet<string> {"i", "t", "type", "y", "plot", "r", "callback"};

            foreach (var param in HttpContext.Request.Query)
            {
                if ((!hashParams.Contains(param.Key)) || (string.IsNullOrEmpty(param.Value)))
                {
                    return BadRequest("Make sure the correct parameter(s) are given.");
                }
            }
            if ((HttpContext.Request.Query.ContainsKey("i") == true) && (HttpContext.Request.Query.ContainsKey("t") == true))
            {
                return BadRequest("You must choose between either i or t.");
            }

            string result = await FetchFilm(HttpContext.Request.Query);
            JObject movieData = JObject.Parse(result);
            Console.WriteLine(movieData);
            
            return Ok(result);
        }
    }
}
