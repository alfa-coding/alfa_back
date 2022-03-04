using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alfa_back.Infrastructure
{
    public interface IConnector<T>
    {
        IQueryable<T> GetElements();
        Task<T> GetElementById(string id);
        Task InsertElementAsync(T record);

        Task DeleteElementByIdAsync(string id);

        Task UpdateElementAsync(string id, T element);
    }
}