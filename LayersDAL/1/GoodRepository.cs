using LayersDAL.Entities;
using LayersDAL.EF;
using LayersDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LayersDAL.Repositories
{
    class GoodRepository : IRepository<Good>
    {
        private StoreContext db;

        public GoodRepository(StoreContext context)
        {
            this.db = context;
        }

        public IEnumerable<Good> GetAll()
        {
            return db.Goods;
        }

        public Good Get(int id)
        {
            return db.Goods.Find(id);
        }

        public void Create(Good item)
        {
            db.Goods.Add(item);
        }

        public void Update(Good item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Good> Find(Func<Good, Boolean> predicate)
        {
            return db.Goods.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Good item = db.Goods.Find(id);
            if (item != null)
                db.Goods.Remove(item);
        }
    }
}
