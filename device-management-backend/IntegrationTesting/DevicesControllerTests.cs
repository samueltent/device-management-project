using DeviceManagementAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting
{
    public class DevicesControllerTests : IntegrationTest
    {
        private string baseUrl = "https://localhost:44395/api/Devices";

        [Fact]
        public async Task GetAll_ResponseCodeOk()
        {
            var response = await _client.GetAsync(baseUrl);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsOkAndCorrectUser()
        {
            string testId = "621f9f325115dad7f98acdc6";

            var response = await _client.GetAsync($"{baseUrl}/{testId}");

            string jsonBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<User>(jsonBody);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(testId, result.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundWithWrongId()
        {
            string testId = "621f9f325115dad7f98acdc2";

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
            string testId = "6226964e2075c8016f18a429";
            var byteContent = CreateByteContent(testId);

            var response = await _client.PutAsync($"{baseUrl}/{testId}", byteContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Put_FailedNotFoundWrongId()
        {
            string testId = "6226963e2075c8016f18a427";
            var byteContent = CreateByteContent(testId);

            var response = await _client.PutAsync($"{baseUrl}/{testId}", byteContent);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_StatusOkDeleteSuccesful()
        {
            string testId = "62269885e725aa1c72dc03e8";

            var response = await _client.DeleteAsync($"{baseUrl}/{testId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_FailedNotFoundWrongId()
        {
            string testId = "6226964f2075c8016f18a425";

            var response = await _client.DeleteAsync($"{baseUrl}/{testId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        public static ByteArrayContent CreateByteContent(string testId = null)
        {
            var testDevice = new Device()
            {
                Name = "Test",
                Manufacturer = "Test",
                Type = "Test",
                Os = "Test",
                OsVersion = 1,
                Processor = "Test",
                RamAmount = 1
            };

            if (testId != null)
            {
                testDevice.Id = testId;
            }

            var testContent = JsonConvert.SerializeObject(testDevice);
            var buffer = System.Text.Encoding.UTF8.GetBytes(testContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

    }
}
