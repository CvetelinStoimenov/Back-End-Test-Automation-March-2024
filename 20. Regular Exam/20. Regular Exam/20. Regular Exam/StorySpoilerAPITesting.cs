using _20._Regular_Exam.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Text.Json;


namespace _20._Regular_Exam
{
    public class StorySpoilerAPITesting
    {
        private RestClient client;
        private static string storyId;

        [OneTimeSetUp]
        public void Setup()
        {
            string accessToken = GetAccessToken("BaiGencho", "123456");

            var restOptions = new RestClientOptions("https://d5wfqm7y6yb3q.cloudfront.net")
            {
                Authenticator = new JwtAuthenticator(accessToken),
            };

            this.client = new RestClient(restOptions);
        }

        private string GetAccessToken(string username, string password)
        {
            var authClient = new RestClient("https://d5wfqm7y6yb3q.cloudfront.net");

            var authRequest = new RestRequest("/api/User/Authentication", Method.Post);
            authRequest.AddJsonBody(
            new AuthenticationRequest
            {
                UserName = username,
                Password = password
            });

            var response = authClient.Execute(authRequest);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<AuthenticationResponse>(response.Content);
                var accessToken = content.AccessToken;
                return accessToken;
            }
            else
            {
                throw new InvalidOperationException("Authentication failed");
            }
        }

        [Test, Order(1)]
        public void CreateNewSpoiler_WithCorrectData_ShouldSucceed()
        {
            // Arrange
            var requestData = new StoryDTO()
            {
                Title = "TestTitle",
                Description = "TestDescription",
            };

            var request = new RestRequest("/api/Story/Create");
            request.AddJsonBody(requestData);

            // Act
            var response = this.client.Execute(request, Method.Post);
            var responseData = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);
            storyId = responseData.StoryId;

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(responseData.Message, Is.EqualTo("Successfully created!"));
            Assert.That(storyId, Is.Not.Null.Or.Empty);
        }

        [Order(2)]
        [Test]
        public void EditSpoiler_WithNewTitle_ShouldSucceed()
        {
            // Arrange
            var requestData = new StoryDTO()
            {
                Title = "editedTestTitle",
                Description = "TestDescription with edits",
            };

            var request = new RestRequest($"/api/Story/Edit/{storyId}", Method.Put);
            request.AddJsonBody(requestData);

            // Act
            var response = this.client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var content = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            Assert.That(content.Message, Is.EqualTo("Successfully edited"));
        }

        [Order(3)]
        [Test]
        public void DeleteSpoiler_WithNewTitle_ShouldSucceed()
        {
            // Arrange
            var request = new RestRequest($"/api/Story/Delete/{storyId}", Method.Delete);

            // Act
            var response = this.client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var content = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            Assert.That(content.Message, Is.EqualTo("Deleted successfully!"));
        }

        [Order(4)]
        [Test]
        public void CreateSpoiler_WithIncorrectData_ShouldFail()
        {
            // Arrange
            var request = new RestRequest("/api/Story/Create", Method.Post);
            request.AddJsonBody(new { });

            // Act
            var response = this.client.Execute(request);

            // Assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Order(5)]
        [Test]
        public void EditSpoiler_WithNonExisitingId_ShouldFail()
        {
            // Arrange
            var requestData = new StoryDTO()
            {
                Title = "editedTestTitle",
                Description = "TestDescription with edits",
            };

            var request = new RestRequest($"/api/Story/Edit/XXXXXXXXXX", Method.Put);
            request.AddJsonBody(requestData);

            // Act
            var response = this.client.Execute(request);

            // Assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            var content = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            Assert.That(content.Message, Is.EqualTo("No spoilers..."));
        }

        [Order(6)]
        [Test]
        public void DeleteSpoiler_WithNonExistingId_ShouldFail()
        {
            // Arrange
            var request = new RestRequest("/api/Story/Delete/XASDAXAS", Method.Delete);

            // Act
            var response = this.client.Execute(request);

            // Assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            var content = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            Assert.That(content.Message, Is.EqualTo("Unable to delete this story spoiler!"));
        }
    }
}