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
    public class Comments
    {
        [Test]
        public void GetCommentByID()
        {
            RestApiHelper<CommentInfo> resultAPI = new RestApiHelper<CommentInfo>();
            var client = resultAPI.FullURL("/api/comments");
            var request = resultAPI.CreateGetRequest("/121");
            var response = resultAPI.GetResponse(client, request);
            var content = resultAPI.GetContent<CommentInfo>(response);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            //Console.WriteLine($"ID: {content.Id}\n CommentedBy: {content.CommentedBy}\n PostID: {content.PostId}\n Text: {content.Text}");
        }

        [Test]
        public void CreateComment()
        {
           //Login добавить
            CommentBase jsonString = new CommentBase {
                                                        PostId = "121",
                                                        Text = "Good post"
                                                     };
            RestApiHelper<CommentBase> resultAPI = new RestApiHelper<CommentBase>();
            var client = resultAPI.FullURL("/api/comments");
            var request = resultAPI.CreatePostRequest(jsonString);
            //request.AddHeader("Authorization", $"Bearer {}");
            var response = resultAPI.GetResponse(client,request);
            var content = resultAPI.GetContent<CommentBase>(response);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    
            //Assert.AreEqual("121", result.PostId);
        }
    }
}
