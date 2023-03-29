using RestSharp;
using System;
using NUnit.Framework;
using System.Text.Json;
using System.Net;
using Newtonsoft.Json;

namespace restSharpTesting
{
    class Program
    {
      [Test]
        static void Main(string[] args)
        {
            string url = "https://jsonplaceholder.typicode.com/posts";
            var client = new RestClient(url);
            var request = new RestRequest();
            //var parameters = new {
            //    id = new [] {"2", "3"}
            //};

            //request.AddObject(parameters);

            //request.AddParameter("id", "2");
            //request.AddParameter("id", "3");
            request.AddQueryParameter("id", "2");
            request.AddQueryParameter("id", "3");
            var response = client.Get(request);
            var jsonplaceholder = response.Content.ToString();
            Console.WriteLine("response.GetType(): " + response.GetType());
            Console.WriteLine("jsonplaceholder.GetType(): " + jsonplaceholder.GetType());

            var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
            Console.WriteLine("Object[0].title: " + obj[0].title);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.IsTrue(jsonplaceholder.Contains("qui est esse"));
                Assert.AreEqual((String)obj[0].title, "qui est esse");
                Assert.AreEqual((String)obj[1].title, "ea molestias quasi exercitationem repellat qui ipsa sit aut");
                Console.WriteLine("Response: " + response);
                Console.WriteLine("jsonplaceholder: " + jsonplaceholder);
            }
        }
    }
}

/*
response.GetType(): RestSharp.RestResponse
jsonplaceholder.GetType(): System.String
Object[0].title: qui est esse
Response: RestSharp.RestResponse
jsonplaceholder: [
  {
    "userId": 1,
    "id": 2,
    "title": "qui est esse",
    "body": "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
  },
  {
    "userId": 1,
    "id": 3,
    "title": "ea molestias quasi exercitationem repellat qui ipsa sit aut",
    "body": "et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut"
  }
]
*/