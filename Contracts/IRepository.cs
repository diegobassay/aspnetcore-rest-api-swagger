using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore.api.swagger.Contracts
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> List();

        void Save(T entity);

        void Delete(T entity);

        void Update(T entity);

        T FindById(int Id);
    }
}
