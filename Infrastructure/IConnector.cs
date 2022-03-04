using System.Collections.Generic;

namespace alfa_back.Infrastructure
{
    public interface IConnector<T>
    {
        IEnumerable<T> GetElements();
        T GetElementById(string id);
        bool InsertElement(T record);

        bool RemoveElement(string id);

        bool Update(string id, T element);
    }
}