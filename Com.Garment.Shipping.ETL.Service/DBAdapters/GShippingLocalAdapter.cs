using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Services;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public class GShippingLocalAdapter : IGShippingLocalAdapter
    {
        public Task GetData(IEnumerable<GShippingLocal> models)
        {
            throw new System.NotImplementedException();
        }

        public Task LoadData(IEnumerable<GShippingLocal> models)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IGShippingLocalAdapter : IBaseAdapter<GShippingLocal>
    {
        
    }
}