using RestSharp;
using System.Threading.Tasks;

namespace TransPerfect.Interfaces
{
    public abstract class ApiClient
    {
        protected readonly IRestClient _restClient;

        public ApiClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        protected Task<IRestResponse> GetResponseContentAsync(IRestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
