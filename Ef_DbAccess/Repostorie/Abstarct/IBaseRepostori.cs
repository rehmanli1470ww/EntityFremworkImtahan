using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_DbAccess.Repostorie.Abstarct
{
    public interface IBaseRepostori<T>
    {
        public IEnumerable<T> GetAll();
        public void Add(T item);
        public void Remove(T item);
        public void Update(T item);
        public void Save();
        public IQueryable<T> GetQuery();
        public void RemoveId(int Id);
    }
}
