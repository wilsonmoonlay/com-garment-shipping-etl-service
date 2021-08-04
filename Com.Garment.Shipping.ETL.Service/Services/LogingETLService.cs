using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public class LogingETLService : ILogingETLService
    {
        ILogingETLAdapter _logingETLAdapter;
        public LogingETLService(IServiceProvider service)
        {            
            _logingETLAdapter = service.GetService<ILogingETLAdapter>();
        }
        public async Task<IEnumerable<LogingETLModel>> Get(int size, int page, string keyword)
        {
            try{
                var result = await _logingETLAdapter.Get(size, page, keyword);
                return result;
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task Save(LogingETLModel data)
        {
            try{
                await _logingETLAdapter.Save(data);
            }catch(Exception ex){
                throw ex;
            }
        }
    }

    public interface ILogingETLService
    {
        Task<IEnumerable<LogingETLModel>> Get(int size, int page, string keyword);
        Task Save(LogingETLModel data);        
    }
}