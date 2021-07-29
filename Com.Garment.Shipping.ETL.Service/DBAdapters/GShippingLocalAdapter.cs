using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public class GShippingLocalAdapter : IGShippingLocalAdapter
    {
        public Task GetData(IEnumerable<GShippingLocalModel> models)
        {
            throw new System.NotImplementedException();
        }

        public Task LoadData(IEnumerable<GShippingLocalModel> models)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IGShippingLocalAdapter : IBaseAdapter<GShippingLocalModel>
    {
        
    }
}