using Newtonsoft.Json;
using RestSharp;
using RestTests.Construction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTests
{
    public class RestApiHelper<T>
    {
        public RestClient restClient;
        public RestRequest restRequest;

        public string baseURL = "http://localhost:5000";

        public static ApiUser LoginUser(LoginModel user)
        {
            RestApiHelper<PostInfo> restApi = new RestApiHelper<PostInfo>();
            var client = restApi.FullURL("/Users/Login");
            var request = restApi.CreatePostRequest(user);
            var response = restApi.GetResponse(client, request);
            return JsonConvert.DeserializeObject<ApiUser>(response.Content);
        }

        public RestClient FullURL(string plusURL)
        {
            var url = baseURL + plusURL;
            var restClient = new RestClient(url);
            return restClient;
        }
 
        public RestRequest CreateGetRequest()
        {
            restRequest = new RestRequest(Method.POST);
            return restRequest;
        }
        public RestRequest CreateGetRequest(string ID)
        {
            restRequest = new RestRequest(($"/{ID}"), Method.GET);
            return restRequest;
        }


        public RestRequest CreatePostRequest(object jsonString)
        {
            restRequest = new RestRequest(Method.POST);
            restRequest.AddJsonBody(jsonString);
            return restRequest;
        }

        public RestRequest CreatePutRequest(object jsonString)
        {
            restRequest = new RestRequest(Method.PUT);
            restRequest.AddJsonBody(jsonString);
            return restRequest;
        }

        public RestRequest CreateDeleteRequest(string ID)
        {
            restRequest = new RestRequest(($"/{ID}"), Method.DELETE);
            return restRequest;
        }

        public IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }

        public T GetContent<T>(IRestResponse response)
        { 
            var content = response.Content;
            var deserializeObj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);

            return deserializeObj;
        }
    }

}
