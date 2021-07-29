using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public interface IBaseAdapter<TModel>
    {
        Task GetData(IEnumerable<TModel> models);
        Task LoadData(IEnumerable<TModel> models);
    }
}