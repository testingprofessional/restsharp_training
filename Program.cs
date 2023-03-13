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
        static void Main(string[] args)
        {
            string url = "https://jsonplaceholder.typicode.com/posts";
            var client = new RestClient(url);
            var request = new RestRequest();
            //var parameters = new {
            //    id = new [] {"2", "3"}
            //};

            //request.AddObject(parameters);

            request.AddParameter("id", "2");
            request.AddParameter("id", "3");
            var response = client.Get(request);
            var jsonplaceholder = response.Content.ToString();
            Console.WriteLine(response.GetType());
            Console.WriteLine(jsonplaceholder.GetType());

            var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
            Console.WriteLine("Object: " + obj[0].title);

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