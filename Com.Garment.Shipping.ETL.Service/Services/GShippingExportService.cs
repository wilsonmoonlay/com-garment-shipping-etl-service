using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Models;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public class GShippingExportService : IGShippingExportService
    {
        public Task<IEnumerable<GShippingExportModel>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task Save(IEnumerable<GShippingExportModel> data)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IGShippingExportService : IBaseService<GShippingExportModel> 
    {
        
    }
}