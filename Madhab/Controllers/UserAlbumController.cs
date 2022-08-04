using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Madhab.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller/api]")]
    public class UserAlbumController : ControllerBase
    {
        private Data collection = new();
        private List<string> endPoinst = new() { "posts", "users", "albums" };
        [HttpGet("/collections")]
        public async Task<IActionResult> Posts()
        {
            foreach (var item in endPoinst)
            {
                await collectionAsync(item);
            }
            return Ok(collection);
        }

        private async Task collectionAsync(string type)
        {
            string URL = "https://jsonplaceholder.typicode.com/" + type;
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(URL);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (type == "users")
            {
                collection.users = JsonConvert.DeserializeObject<List<user>>(responseBody).Take(30).ToList();

            }
            else if (type == "posts")
            {
                collection.posts = JsonConvert.DeserializeObject<List<post>>(responseBody).Take(30).ToList();
            }
            else
            {
                collection.albums = JsonConvert.DeserializeObject<List<album>>(responseBody).Take(30).ToList();
            }
        }

        private class Data
        {
            public List<post> posts { get; set; }
            public List<user> users { get; set; }
            public List<album> albums { get; set; }
        }
        private class post
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string body { get; set; }
        }
        private class album
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
        }
        private class user
        {
            public int id { get; set; }
            public string name { get; set; }
            public string username { get; set; }

            public string email { get; set; }
            public address address { get; set; }

            public company company { get; set; }
            public string phone { get; set; }
            public string website { get; set; }

        }
        private class address
        {
            public string street { get; set; }
            public string suite { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }

            public geo geo { get; set; }

        }
        private class geo
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }
        private class company
        {
            public string name { get; set; }
            public string catchPhrase { get; set; }
            public string bs { get; set; }
        }

    }
}