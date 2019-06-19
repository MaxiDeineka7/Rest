using NUnit.Framework;
using RestTests.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTests
{
    public class Users
    {
        [Test]
        public void CreateUser()
        {
            AddApiUser jsonString  = new AddApiUser {
                                                        name = "Gena Bukin",
                                                        email = "masdfsd@gmail.com",
                                                        password = "qwerty4141"
            };
            RestApiHelper<UserData> resultAPI = new RestApiHelper<UserData>();
            var client = resultAPI.FullURL("/Users");
            var request = resultAPI.CreatePostRequest(jsonString);
            var response = resultAPI.GetResponse(client, request);

            var content = resultAPI.GetContent<UserData>(response);

            //Assert.AreEqual(content.email, "masdfsd@gmail.com");
            //Console.WriteLine($"User was create \n Name: {content.name}\n Email: {content.email}");
        }

        //public string GetToken()
        //{          
        //        LoginModel jsonString = new LoginModel
        //        {
        //            email = "masdfsd@gmail.com",
        //            password = "qwerty4141"
        //        };
        //        RestApiHelper<ApiUser> resultApi = new RestApiHelper<ApiUser>();
        //        var client = resultApi.FullURL("/Users/Login");
        //        var request = resultApi.CreatePostRequest(jsonString);
        //        var response = resultApi.GetResponse(client, request);

        //        var content = resultApi.GetContent<ApiUser>(response);    
            
        //    return content.token;
        //}

        [Test]
        public void Login()
        {
            LoginModel jsonString = new LoginModel {
                                                        email = "masdfsd@gmail.com",
                                                        password = "qwerty4141"
                                                   };
            RestApiHelper<ApiUser> resultApi = new RestApiHelper<ApiUser>();
            var client = resultApi.FullURL("/Users/Login");
            var request = resultApi.CreatePostRequest(jsonString);
            var response = resultApi.GetResponse(client,request);
            
            var content = resultApi.GetContent<ApiUser>(response);
            //Console.WriteLine($"{content.token}");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetTestAuth()
        {
            RestApiHelper<LoginModel> resultAPI = new RestApiHelper<LoginModel>();
            var client = resultAPI.FullURL("/Users/Login");
            var request = resultAPI.CreateGetRequest();
            var loginedUser = RestApiHelper<LoginModel>.LoginUser(new LoginModel { email = "deniizz@gmail.com", password = "qwerty123" });
            request.AddHeader("Autorization",$"Bearer {loginedUser.token}");
            var response = resultAPI.GetResponse(client,request);
            Console.WriteLine($"User was login {loginedUser.token}");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK)," We have problems :( ");
        }
    }
}
