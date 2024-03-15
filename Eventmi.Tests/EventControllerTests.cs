using RestSharp;
using System.Net;

namespace Eventmi.Tests
{
    [TestFixture]
    public class EventControllerTests
    {
        private RestClient _client;
        private const string _baseURL = @"https://localhost:7236";
        
        [SetUp]
        public void Setup()
        {
            _client = new RestClient(_baseURL);
        }

        [Test]
        public async Task GetAllEvents_ShouldReturnsSuccessStatusCode()
        {
            // Arrange: Prepare the HTTP request to the All action
            var request = new RestRequest("/Event/All");

            // Act: Execute HTTP request
            var response =await _client.ExecuteAsync(request, Method.Get);

            // Assert: Verify that the response status code is OK (200)
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Add_GetRequest_ShouldReturn_AddView()
        {
            // Arrange
            var request = new RestRequest("Event/Add");

            // Act
            var response = await _client.ExecuteAsync(request, Method.Get);

            //Assert
            // Check status code
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));    
        }
    }
}