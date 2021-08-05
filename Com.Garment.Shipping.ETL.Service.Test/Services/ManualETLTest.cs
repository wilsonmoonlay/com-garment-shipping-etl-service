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
            
            
            var serviceProvider = new Mock<IServiceProvider>();

            var mockShippingExport = new Mock<IGShippingExportService>();
            var mockShippingLocal = new Mock<IGShippingLocalService>();


            ManualETL service = new ManualETL(mockShippingExport.Object,mockShippingLocal.Object);

            var response = await service.Run(request.Object, logger.Object);
        }
    }
}
