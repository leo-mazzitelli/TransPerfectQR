using RestSharp;
using System;
using System.Threading.Tasks;
using TransPerfect.Interfaces;

namespace TransPerfect.Services
{
    public class ApiClientGoQR : ApiClient
    {
        public ApiClientGoQR() : base(new RestClient("https://api.qrserver.com/v1/"))
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

        public string ReadQR(byte[] imageBytes)
        {
            var request = new RestRequest("read-qr-code", Method.POST);

            request.AddHeader("Content-Type", "multipart/form-data; boundary=----WebKitFormBoundaryBW8IWFKUxGM9ABOs");

            request.AddFileBytes("file", imageBytes, "prueba.png");

            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(_restClient, request) as RestResponse;
            }).Wait();

            if (response.IsSuccessful)
            {
                return response.Content;
            }

            throw new Exception($"Error reading QR end with code {response.StatusCode}: {response.Content} {response.ErrorMessage}");
        }
    }
}
