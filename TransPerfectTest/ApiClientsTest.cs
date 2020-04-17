using NUnit.Framework;
using System.IO;
using TransPerfect.Services;

namespace TransPerfectTest
{
    public class ApliClientsTest
    {
        private ApiClientGoQR _apiClientGoQR;

        [SetUp]
        public void Setup()
        {
            _apiClientGoQR = new ApiClientGoQR();
        }

        [Test]
        public void ApiClientGoQR_CreateQR_Correctly()
        {
            byte[] imageBytes = _apiClientGoQR.GenerateQR("Leonardo Mazzitelli");
            Assert.AreEqual(448, imageBytes.Length);
        }

        [Test]
        public void ApiClientGoQR_ReadQR_Correctly()
        {
            string data = _apiClientGoQR.ReadQR(Path.GetFullPath("LMazzitelli.png"));
            Assert.AreNotEqual(null, data);
        }
    }
}