using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TransPerfect.Interfaces;

namespace TransPerfect.Services
{
    public class ApiClientGoQR : ApiClient
    {
        private const string API_URL = "http://api.qrserver.com/v1/";

        public ApiClientGoQR() : base(new RestClient(API_URL))
        {
        }

        public byte[] GenerateQR(string data)
        {
            var request = new RestRequest("create-qr-code", Method.GET);

            request.AddParameter("data", data);

            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(_restClient, request) as RestResponse;
            }).Wait();

            if (response.IsSuccessful)
            {
                return response.RawBytes;
            }

            throw new Exception($"Error generating QR end with code {response.StatusCode}: {response.ErrorMessage}");
        }

        public string ReadQR(string imagePath)
        {
            HttpContent bytesContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(bytesContent, "file", "file");
                var response = client.PostAsync(API_URL + "read-qr-code/", formData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return JsonConvert.SerializeObject(response.Content.ReadAsStringAsync().Result, Formatting.None);
            }
        }
    }
}
