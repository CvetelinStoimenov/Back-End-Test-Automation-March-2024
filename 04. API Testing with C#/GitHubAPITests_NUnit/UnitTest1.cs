using NUnit.Framework.Internal;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Text.Json;

namespace GitHubAPITests_NUnit
{
    public class Tests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            var options = new RestClientOptions("https://api.github.com")
            { 
                Authenticator = new HttpBasicAuthenticator("CvetelinStoimenov", "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")
            };

            this.client = new RestClient(options);
        }

        [Test]
        public void Test_GetAllIssuesFromARepo()
        {
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Get);
            var response = client.Execute(request);
            var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

            Assert.That(issues.Count > 1);

            foreach (var issue in issues)
            {
                Assert.That(issue.id, Is.GreaterThan(0));
                Assert.That(issue.number, Is.GreaterThan(0));
                Assert.That(issue.title, Is.Not.Empty);
            }
        }

        private Issue CreateIssue(string title, string body)
        {
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues");
            request.AddBody(new { body, title});
            var response = client.Execute(request, Method.Post);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);
            return issue;
        }

        [Test]
        public void Test_CreateGitHubIssue()
        {
            string title = "This is a Demo Issue";
            string body = "QA Back-End Automation Course February 2024, issue by Cvetelin Stoimenov";
            var issue = CreateIssue(title, body);

            Assert.That(issue.id, Is.GreaterThan(0));
            Assert.That(issue.number, Is.GreaterThan(0));
            Assert.That(issue.title, Is.Not.Empty);
            Assert.That(issue.body, Is.EqualTo(body));
        }

        [Test]
        public void Test_UpdateGitHubIssue()
        {
            var request = new RestRequest("repos/testnakov/test-nakov-repo/issues/6250");
            request.AddJsonBody(new
            {
                title = "Changing the name of the issue that I created"
            });

            var response = client.Execute(request, Method.Patch);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(issue.id, Is.GreaterThan(0), "Issue ID should be greater than 0.");
            Assert.That(issue.number, Is.GreaterThan(0), "Issue number should be greater than 0.");
            Assert.That(issue.title, Is.EqualTo("Changing the name of the issue that I created"));
            Assert.That(response.Content, Is.Not.Empty, "The response content should not be empty.");
        }


        [TestCase("BG", "1000", "Sofia")]
        [TestCase("BG", "5000", "Veliko Turnovo")]
        [TestCase("CA", "M5S", "Toronto")]
        [TestCase("GB", "B1", "Birmingham")]
        [TestCase("DE", "01067", "Dresden")]
        public void TestHippopotamus(string countryCode, string zipCode, string expectedPlace)
        {
            // Arrange
            var restClient = new RestClient("https://api.zippopotam.us");
            var httpRequest = new RestRequest(countryCode + "/" +  zipCode);

            // Act
            var httpResponse = restClient.Execute(httpRequest);
            var location = JsonSerializer.Deserialize<Location>(httpResponse.Content);

            // Assert   
            StringAssert.Contains(expectedPlace, location.Place[0].PlaceName);
        }
    }
}