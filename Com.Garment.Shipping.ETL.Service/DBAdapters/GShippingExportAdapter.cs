using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public class GShippingExportAdapter : IGShippingExportAdapter
    {
        public Task GetData(IEnumerable<GShippingExportModel> models)
        {
            throw new System.NotImplementedException();
        }

        public Task LoadData(IEnumerable<GShippingExportModel> models)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IGShippingExportAdapter : IBaseAdapter<GShippingExportModel>
    {
        
    }
}