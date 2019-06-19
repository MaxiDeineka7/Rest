using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestTests.PostPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTests
{
    public class Posts
    {
        string autoPostInfo = @"C:\Users\Maxim\Desktop\SimpleAPi (2)\SimpleAPi\Data\PostInfo.json";
        string autoPostRate = @"C:\Users\Maxim\Desktop\SimpleAPi (2)\SimpleAPi\Data\PostRate.json";
        string dataPostInfo = @"C:\Users\Maxim\Desktop\SimpleAPi (2)\DataForSerealization\PostInfo.json";
        string dataPostRate = @"C:\Users\Maxim\Desktop\SimpleAPi (2)\DataForSerealization\PostRate.json";


        string id;
        double rate;
        string title;
        string text;


        [SetUp]
        public void PreCoudition()
        {
            using (var read = new StreamReader(dataPostInfo))
            {
                var jsonData = read.ReadToEnd();
                var myData = JsonConvert.DeserializeObject<PostInfo[]>(jsonData);
                id = myData[0].Id;
                rate = myData[0].Rate;
                title = myData[0].Text;
                text = myData[0].Title;
            }

            if (File.Exists(autoPostInfo))
            {
                File.Delete(autoPostInfo);
            }
            File.Copy(dataPostInfo, autoPostInfo);
        }
        [Test]
        public void GetAll()
        {
            RestApiHelper<PostInfo> resultAPI = new RestApiHelper<PostInfo>();
            var restClient = resultAPI.FullURL("/api/posts");
            var restRequest = resultAPI.CreateGetRequest();
            var response = resultAPI.GetResponse(restClient, restRequest);

            var content = resultAPI.GetContent<PostInfo>(response);

            Console.WriteLine($"{content.Id},{content.Rate},{content.Text}, {content.Title}");
            
        }

        [Test]
        public void CreatePost()
        {
            BasePost jsonString = new BasePost{
                                                Title= title,
                                                Text= text
                                              };
            RestApiHelper<PostInfo> resultAPI = new RestApiHelper<PostInfo>();
            var restClient = resultAPI.FullURL("/api/posts");
            var restRequest = resultAPI.CreatePostRequest(jsonString);
            var response = resultAPI.GetResponse(restClient, restRequest);
            var content = resultAPI.GetContent<PostInfo>(response);
            
            Console.WriteLine($"{content.Id},{content.Rate},{content.Text}, {content.Title}");
                         
            //Assert.AreEqual(element.Title, "WoRLDD");
        }

        [Test]
        public void CreatePostRate()
        {
            BasePostRate jsonString = new BasePostRate
                                                        {
                                                            Rate = rate,
                                                            PostId = id,
                                                        };
            RestApiHelper<BasePostRate> resultAPI = new RestApiHelper<BasePostRate>();
            var restClient = resultAPI.FullURL("/api/posts/rate");
            var restRequest = resultAPI.CreatePostRequest(jsonString);
            var response = resultAPI.GetResponse(restClient, restRequest);
            var content = resultAPI.GetContent<PostInfo>(response);

            Console.WriteLine($"До поста с ID:{content.Id}, добавлен Rate: {content.Rate}");
        }

        [Test]
        public void CreatePutExample()
        {
            var jsonString = new PostInfo{  Id = id,
                                            Rate = rate,
                                            Title = "Hello world",
                                            Text = "Helloooo  world, ello wld He world"
            };
            RestApiHelper<PostInfo> resultAPI = new RestApiHelper<PostInfo>();
            var restClient = resultAPI.FullURL("/api/posts");
            var restRequest = resultAPI.CreatePutRequest(jsonString);
            var response = resultAPI.GetResponse(restClient, restRequest);
            var content = resultAPI.GetContent<PostInfo>(response);

            Console.WriteLine($"{content.Id},{content.Rate},{content.Text},{content.Title}\n");
        }

       
        [Test]
        public void GetById()
        {
            RestApiHelper<PostInfo> resultAPI = new RestApiHelper<PostInfo>();
            var restClient = resultAPI.FullURL("/api/posts");
            var restRequest = resultAPI.CreateGetRequest(id);
            var response = resultAPI.GetResponse(restClient, restRequest);
            var content = resultAPI.GetContent<PostInfo>(response);

            Console.WriteLine($" ID: {content.Id}\n Rate: {content.Rate}\n Title:{content.Title}\n Text: {content.Text}");          
        }

        [Test]
        public void DeleteById()
        {
            RestApiHelper<PostInfo> resultAPI = new RestApiHelper<PostInfo>();
            var restClient = resultAPI.FullURL("/api/posts");
            var restRequest = resultAPI.CreateDeleteRequest(id);
            var response = resultAPI.GetResponse(restClient, restRequest);
            //PostInfo content = resultAPI.GetContent<PostInfo>(response);

           // Console.WriteLine("Post was delete by ID");
        }
    }
}
