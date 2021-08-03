using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public interface IBaseAdapter<TModel>
    {
        Task<IEnumerable<TModel>> GetData();
        Task LoadData(IEnumerable<TModel> models);
        Task Truncate(IEnumerable<TModel> models);
    }
}