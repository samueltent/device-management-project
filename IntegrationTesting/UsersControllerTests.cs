using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DeviceManagementAPI.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IntegrationTesting
{
    public class UsersControllerTests : IntegrationTest
    { 
        private string baseUrl = "https://localhost:44395/api/Users";
    
        [Fact]
        public async Task GetAll_ResponseCodeOk()
        {
            var response = await _client.GetAsync(baseUrl);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsOkAndCorrectUser()
        {
            string testId = "62268aed32729971782c0c37";

            var response = await _client.GetAsync($"{baseUrl}/{testId}");

            string jsonBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<User>(jsonBody);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(testId, result.Id); 
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundWithWrongId()
        {
            string testId = "62268aed32729971782c0c55";

            var response = await _client.GetAsync($"{baseUrl}/{testId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_StatusCreated()
        {
            var byteContent = CreateByteContent();

            var response = await _client.PostAsync($"{baseUrl}", byteContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Put_StatusOkUpdateSuccesful()
        {
            string testId = "62268aff6c2e740bf8a897d8";
            var byteContent = CreateByteContent(testId);

            var response = await _client.PutAsync($"{baseUrl}/{testId}", byteContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Put_FailedNotFoundWrongId()
        {
            string testId = "62268aff6c2e740bf8a897c5";
            var byteContent = CreateByteContent(testId);

            var response = await _client.PutAsync($"{baseUrl}/{testId}", byteContent);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_StatusOkDeleteSuccesful()
        {
            string testId = "62269885e725aa1c72dc03e7";

            var response = await _client.DeleteAsync($"{baseUrl}/{testId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_FailedNotFoundWrongId()
        {
            string testId = "62268b3a4f3cae17a9eeab22";

            var response = await _client.DeleteAsync($"{baseUrl}/{testId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        public static ByteArrayContent CreateByteContent(string testId=null)
        {
            var testUser = new User()
            {
                Name = "Test",
                Role = "Tester",
                Location = "Testland"
            };

            if(testId != null)
            {
                testUser.Id = testId;
            }

            var testContent = JsonConvert.SerializeObject(testUser);
            var buffer = System.Text.Encoding.UTF8.GetBytes(testContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

    }
}
