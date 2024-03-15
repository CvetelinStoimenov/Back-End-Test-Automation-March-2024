using RestSharp;
using System.Net;

namespace APITestsNUnitRestSharp
{
    public class Tests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            var options = new RestClientOptions("https://api.github.com")
            { 
                MaxTimeout = 3000            
            };
            
            client = new RestClient(options);
        }

        [Test]
        public void Test_GitHubApiRequest()
        {
            //var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Get);
            var response = client.Get(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}