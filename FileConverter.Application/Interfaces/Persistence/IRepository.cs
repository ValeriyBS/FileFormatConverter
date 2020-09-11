using System.Collections.Generic;
using System.Linq;

namespace FileConverter.Application.Interfaces.Persistence
{
    public interface IRepository<out T>
    {
        IQueryable<T> GetAllRows();

        T GetRow(int id);
    }
}
