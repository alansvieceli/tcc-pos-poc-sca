using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCA.Shared.Services
{
    public interface IGenericService<T>
    {
        void SetUrl(string url);

        void SetToken(string token);

        Task<IEnumerable<T>> FindAllAsync(string recurso = "");

        Task<T> FindByIdAsync(int id, string recurso = "");

        Task<T> CompleteFindByIdAsync(int id);

        Task<int> InsertAsync(T obj, string recurso = "");        

        Task<bool> UpdateAsync(int id, T obj, string recurso = "");

        Task<bool> DeleteAsync(int id, string recurso = "");
    }
}
