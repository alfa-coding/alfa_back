using System.Collections.Generic;

namespace alfa_back.Infrastructure.Concrete
{
     public interface IConnector<T>
    {
        IEnumerable<T> GetElements(string table);
        T GetElementById(string id, string table);
        bool InsertElement(T record, string table);

        bool RemoveElement(string id, string table);

        bool Update(string id, T element, string table);
    }
}