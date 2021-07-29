using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Services;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public class GShippingExportAdapter : IGShippingExportAdapter
    {
        public Task GetData(IEnumerable<GShippingExport> models)
        {
            throw new System.NotImplementedException();
        }

        public Task LoadData(IEnumerable<GShippingExport> models)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IGShippingExportAdapter : IBaseAdapter<GShippingExport>
    {
        
    }
}