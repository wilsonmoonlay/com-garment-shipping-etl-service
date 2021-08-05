using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Helpers;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Garment.Shipping.ETL.Service.Test.Services
{
    public class ManualETLTest
    {
        [Fact]
        public async Task Function_Manual_ETL()
        {           
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var logger = new Mock<ILogger>();

            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Query["name"]).Returns("Test");
            request.Setup(x => x.Body).Returns(ms);
            request.Setup(x => x.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"));
            request.Setup(x => x.Headers["Authorization"]).Returns(new Microsoft.Extensions.Primitives.StringValues("1000"));

            var tokenPayload = new Mock<TokenPayloadExtractorService>();
            
            var mockJwt = new Mock<JwtSecurityTokenHandler>();
            

            var mockShippingExport = new Mock<IGShippingExportService>();
            var mockShippingLocal = new Mock<IGShippingLocalService>();

            var logModel = new LogingETLModel(1, "Area", new DateTime(), "Test", true);

            var mockLogging = new Mock<ILogingETLService>();
            mockLogging.Setup(x => x.Update(new LogingETLModel(1, "Area", new DateTime(), "Test", true)));


            ManualETL service = new ManualETL(mockShippingExport.Object,mockShippingLocal.Object, mockLogging.Object);

            var result = await service.Run(request.Object, logger.Object);
        }
    }
}
