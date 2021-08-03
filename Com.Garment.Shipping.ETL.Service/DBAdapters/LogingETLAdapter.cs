using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Helpers;
using Com.Garment.Shipping.ETL.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public class LogingETLAdapter : ILogingETLAdapter
    {
        private readonly ISqlDataContext<LogingETLModel> context;
        public LogingETLAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<LogingETLModel>>();
        }
        public async Task<IEnumerable<LogingETLModel>> Get(int size, int page, string keyword)
        {
            try{    
                var query = $"SELECT * From [GDashboardLogETL] ORDER BY UpdatedAt OFFSET({(page - 1) * size}) ROWS FETCH NEXT({size}) ROWS ONLY";
                var result = await context.QueryAsync(query);
                return result.ToList();
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task Save(LogingETLModel model)
        {
            try{
                var query = $"INSERT INTO [GDashboardLogETL] ([DataArea] ,[UpdatedAt] ,[UpdatedBy] ,[Status]) VALUES (@DataArea, @UpdatedAt, @UpdatedBy, @Status)";
                var result = await context.ExecuteAsync(query, model);
            }catch(Exception ex){
                throw ex;
            }
        }
    }


    public interface ILogingETLAdapter
    {        
        Task<IEnumerable<LogingETLModel>> Get(int size, int page, string keyword);
        Task Save(LogingETLModel model);
    }
}