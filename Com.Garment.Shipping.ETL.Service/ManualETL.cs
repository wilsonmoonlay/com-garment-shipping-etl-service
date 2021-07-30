using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Com.Garment.Shipping.ETL.Service.Services;
using Com.Garment.Shipping.ETL.Service.Models;
using System.Collections.Generic;

namespace Com.Garment.Shipping.ETL.Service
{
    public class ManualETL
    {
        private readonly IGShippingExportService _gShippingExportService;
        private readonly IGShippingLocalService _gShippingLocalService;
        public ManualETL(
            IGShippingExportService gShippingExportService,
            IGShippingLocalService gshippingLocalService)
        {
            _gShippingExportService = gShippingExportService;
            _gShippingLocalService = gshippingLocalService;
        }

        [FunctionName("manual-etl")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
            try
            {
                //Get and Insert to Export
                var result = await _gShippingExportService.Get();

                var listData = new List<GShippingExportModel>();
                foreach (var exportData in result)
                {
                    listData.Add(new GShippingExportModel(
                        exportData.IdPackingLists, exportData.InvoiceNo, exportData.TruckingDate, exportData.BuyerAgentCode, exportData.BuyerAgentName, exportData.Destination, exportData.SectionCode, exportData.PackingListId, exportData.IdShippingInvoice, exportData.GarmentShippingInvoiceId, exportData.BuyerBrandName, exportData.ComodityCode, exportData.ComodityName, exportData.UnitCode, exportData.Quantity, exportData.UomUnit, exportData.CMTPrice, exportData.Amount));
                }

                await _gShippingExportService.Save(listData);

                //Get and Insert to Local
                var resultLocal = await _gShippingLocalService.Get();

                var listDataLocal = new List<GShippingLocalModel>();
                foreach (var localData in resultLocal)
                {
                    listDataLocal.Add(new GShippingLocalModel(
                        localData.Id, localData.NoteNo, localData.Date, localData.BuyerCode, localData.BuyerName, localData.LocalSalesNoteId, localData.Quantity, localData.UomUnit, localData.Price, localData.Amount
                        ));
                }

                await _gShippingLocalService.Save(resultLocal);

                return new OkObjectResult("Success");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
