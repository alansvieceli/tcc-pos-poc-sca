using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Shared.Services
{
    public interface IGenericService<T>
    {
        void SetUrl(string url);

        Task<IEnumerable<T>> FindAllAsync();

        Task<T> FindByIdAsync(int? id);

        Task<bool> InsertAsync(T obj);        

        Task<bool> UpdateAsync(int? id, T obj);

        Task<bool> DeleteAsync(int? id);
    }
}
