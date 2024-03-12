using NUnit.Framework.Internal;
using RestSharp;
using RestSharp.Authenticators;
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
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Post);
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
    }
}