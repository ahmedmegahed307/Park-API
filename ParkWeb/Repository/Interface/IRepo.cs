using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Repository.Interface
{
    public interface  IRepo<T> where T:class
    {

        Task<T> GetAsyc(string url, int id);
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<bool> CreateAsync(string url, T objToCreate);
        Task<bool> UpdateAsyc(string url, T objToUpdate);
        Task<bool> DeleteAsyc(string url, int id);
    }
}
