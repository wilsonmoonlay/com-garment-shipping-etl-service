using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service
{
    public class ETLService : IETLService
    {
        private readonly ILogingETLService _logingETLService;
        private readonly ILogingDWHService _logingDWHService;
        private readonly IGShippingExportService _gShippingExportService;
        private readonly IGShippingLocalService _gShippingLocalService;
        
        public ETLService(IServiceProvider service)
        {
            _logingETLService = service.GetService<ILogingETLService>();
            _logingDWHService = service.GetService<ILogingDWHService>();
            _gShippingExportService = service.GetService<IGShippingExportService>();
            _gShippingLocalService = service.GetService<IGShippingLocalService>();
        }
        // public async Task TriggerPowerBI(){

        // }

        public async Task StartETL(){
            try{
                var result = await _gShippingExportService.Get();
                await _gShippingExportService.ClearData(result);
                await _gShippingExportService.Save(result);
                
                var resultLocal = await _gShippingLocalService.Get();
                await _gShippingLocalService.Save(resultLocal);
                await _gShippingLocalService.Save(resultLocal);
            }catch(Exception ex){
                throw ex;
            }
        }
        
        public async Task CreateDWHLog(LogingDWHModel data){       
            try{                                
                var logingDWHModel = new LogingDWHModel(
                    0,
                    data.UpdatedAt,
                    data.UpdatedBy,
                    data.Status
                );

                await _logingDWHService.Save(logingDWHModel);
            }catch(Exception ex){
                throw ex;
            }  
        }

        // public async Task CrateETLLog(LogingETLModel data){         
        //     try{                
        //         var logingEtlModel = new LogingETLModel(
        //             0,
        //             data.DataArea,
        //             data.UpdatedAt,
        //             data.UpdatedBy,
        //             data.Status
        //         );
        //         // logingEtlModel.Add(new LogingETLModel(
        //         //     0,
        //         //     data.DataArea,
        //         //     data.UpdatedAt,
        //         //     data.UpdatedBy,
        //         //     data.Status
        //         // ));                
                
        //         await _logingETLService.Save(logingEtlModel);
        //     }catch(Exception ex){
        //         throw ex;
        //     }            
        // }
        

        public async Task CrateETLLog(string token, string area, bool status){            
            // var tokenPayload = new TokenPayloadExtractorService(token);

            var data = new LogingETLModel(
                0,
                area,
                DateTime.Now,
                // tokenPayload.getUsername(),
                "tes",
                status
            );
             
            await _logingETLService.Save(data);
        }
    }
    public interface IETLService{
        Task StartETL();
        Task CreateDWHLog(LogingDWHModel data);
        Task CrateETLLog(string token, string area, bool status);
    }
}