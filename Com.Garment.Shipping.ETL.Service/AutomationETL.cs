using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Com.Garment.Shipping.ETL.Service
{

    public class AutomationETL
    {
        private readonly IGShippingExportService _gShippingExportService;
        private readonly IGShippingLocalService _gShippingLocalService;
        private readonly ILogingETLService _loggingETLService;
        public AutomationETL(
            IGShippingExportService gShippingExportService,
            IGShippingLocalService gshippingLocalService,
            ILogingETLService logingETLService)
        {
            _gShippingExportService = gShippingExportService;
            _gShippingLocalService = gshippingLocalService;
            _loggingETLService = logingETLService;
        }

        [FunctionName("automation-etl")]
        public async Task<IActionResult> Run([TimerTrigger("0 20 14 1/1 * ? *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                //Get and Insert to Export
                var result = await _gShippingExportService.Get();

                await _gShippingExportService.ClearData(result);

                await _gShippingExportService.Save(result);

                //Get and Insert to Local
                var resultLocal = await _gShippingLocalService.Get();

                await _gShippingLocalService.ClearData(resultLocal);

                var listDataLocal = new List<GShippingLocalModel>();

                await _gShippingLocalService.Save(resultLocal);

                //var loggingExportData = new LogingETLModel(
                //    data.Id,
                //    data.DataArea,
                //    DateTime.Now,
                //    tokenPayload.GetUsername(),
                //    true
                //);

                //await _loggingETLService.Update(loggingExportData);

                return new OkObjectResult(new { message = "success" });
            }
            catch (Exception Ex)
            {
                //var loggingExportData = new LogingETLModel(
                //    data.Id,
                //    data.DataArea,
                //    DateTime.Now,
                //    tokenPayload.GetUsername(),
                //    false
                //);

                //await _loggingETLService.Update(loggingExportData);

                return new BadRequestObjectResult(new { message = "Bad Request", info = Ex.Message });
            }
        }
    }
}
