using RestSharpServices;
using System.Net;
using System.Reflection.Emit;
using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework.Internal;
using RestSharpServices.Models;
using System;

namespace TestGitHubApi
{
    public class TestGitHubApi
    {
        private GitHubApiClient client;
        

        [SetUp]
        public void Setup()
        {            
            client = new GitHubApiClient("https://api.github.com/repos/testnakov/", "your_username", "your_password");
        }


        [Test, Order (1)]
        public void Test_GetAllIssuesFromARepo()
        {
            
        }

        [Test, Order (2)]
        public void Test_GetIssueByValidNumber()
        {
            
        }
        
        [Test, Order (3)]
        public void Test_GetAllLabelsForIssue()
        {
            
        }

        [Test, Order (4)]
        public void Test_GetAllCommentsForIssue()
        {
           
        }

        [Test, Order(5)]
        public void Test_CreateGitHubIssue()
        {
            
        }

        [Test, Order (6)]
        public void Test_CreateCommentOnGitHubIssue()
        {
           

         }

        [Test, Order (7)]
        public void Test_GetCommentById()
        {
            
        }


        [Test, Order (8)]
        public void Test_EditCommentOnGitHubIssue()
        {
           
        }

        [Test, Order (9)]
        public void Test_DeleteCommentOnGitHubIssue()
        {
           
        }


    }
}

