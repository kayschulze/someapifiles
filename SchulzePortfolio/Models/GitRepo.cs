using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SchulzePortfolio.Models
{
    public class GitRepo
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }

        public static List<GitRepo> GetGitRepos()
        {
            var client = new RestClient("");
            var request = new RestRequest("Accounts/EnvironmentVariable.AccountSid/Messages.json", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator("EnvironmentVariable.AccountSid", "EnvironmentVariable.AuthToken");
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var gitRepoList = JsonConvert.DeserializeObject<List<GitRepo>>(jsonResponse["messages"].ToString());
            return gitRepoList;
        }

        public void Send(List<GitRepo> gitrepos)
        {
            var client = new RestClient("");
            var request = new RestRequest("Accounts/" + EnvironmentVariable.AccountSid + "Messages", Method.POST);

            foreach (var gitrepo in gitrepos)
            {
                request.AddParameter("To", To);
                request.AddParameter("From", From);
                request.AddParameter("Body", Body);
                client.Authenticator = new HttpBasicAuthenticator("EnvironmentVariable.AccountSid", "EnvironmentVariable.AuthToken");
                client.ExecuteAsync(request, response =>
                {
                    Console.WriteLine(response.Content);
                });
            }
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
