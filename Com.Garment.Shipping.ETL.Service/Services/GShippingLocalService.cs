using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Models;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public class GShippingLocalService : IGShippingLocalService
    {
        public Task<IEnumerable<GShippingLocalModel>> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task Save(IEnumerable<GShippingLocalModel> data)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IGShippingLocalService  : IBaseService<GShippingLocalModel>
    {
        
    }
}