using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Winner.Models;

namespace Winner.IRepository
{
    public interface IProductClassService
    {
        Task<IEnumerable<ProductClass>> GetList(int? pid, int? level, int? page, int? pagesize);
        Task<ProductClass> Get(int id);
        Task<int> Post(ProductClass productclass);
        Task<int> Put(int id, ProductClass productclass);
        Task<int> Delete(int id);
    }
}
