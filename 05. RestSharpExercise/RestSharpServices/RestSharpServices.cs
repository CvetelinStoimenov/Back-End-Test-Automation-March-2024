using System.Net;
using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpServices.Models;




namespace RestSharpServices
{
    public class GitHubApiClient
    {
        private RestClient client;

        public GitHubApiClient(string baseUrl, string username, string token)
        {
            throw new NotImplementedException();
        }

        public List<Issue>  GetAllIssues(string repo)
        {
            throw new NotImplementedException();
        }

        public Issue  GetIssueByNumber(string repo, int issueNumber)
        {
            throw new NotImplementedException();

        }

        public Issue  CreateIssue(string repo, string title, string body)
        {
            throw new NotImplementedException();
        }

        public List<Label>  GetAllLabelsForIssue(string repo, int issueNumber)
        {
            throw new NotImplementedException();

        }

        public List<Comment>  GetAllCommentsForIssue(string repo, int issueNumber)
        {
            throw new NotImplementedException();
        }

        public Comment CreateCommentOnGitHubIssue(string repo, int issueNumber, string body)
        {
            throw new NotImplementedException();
        }

        public Comment  GetCommentById (string repo, int commentId)
        {
            throw new NotImplementedException();
        }

        public Comment  EditCommentOnGitHubIssue( string repo, int commentId, string newBody)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCommentOnGitHubIssue(string repo, int commentId)
        {
            throw new NotImplementedException();
        }

    }
}
