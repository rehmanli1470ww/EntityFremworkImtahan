using Ef_DbAccess.Context;
using Ef_DbAccess.Repostorie.Abstarct;
using Ef_Model.Entities.Abstarct;
using Ef_Model.Entities.Concret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_DbAccess.Repostorie.Concret
{
    public class BaseRepostori<T> : IBaseRepostori<T> where T : class
    {

        private TurboAzDbContext _context;
        public BaseRepostori()
        {
            _context = new TurboAzDbContext();
        }
        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Remove(T item)
        {
            _context.Remove(item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T item)
        {
            _context.Update(item);
        }

        public T GetAdmin() 
        {
            return _context.Set<T>().FirstOrDefault();
        }

        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>();
        }

        public void RemoveId(int Id)
        {
            
          
        }
    }
}
