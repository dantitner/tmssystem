using LayersDAL.EF;
using LayersDAL.Entities;
using LayersDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDAL.Repositories
{
    class GoodOrderRepository : IRepository<GoodOrder>
    {
        private StoreContext db;

        public GoodOrderRepository(StoreContext context)
        {
            this.db = context;
        }

        public IEnumerable<GoodOrder> GetAll()
        {
            return db.GoodOrders;
        }

        public GoodOrder Get(int id)
        {
            return db.GoodOrders.Find(id);
        }

        public void Create(GoodOrder item)
        {
            db.GoodOrders.Add(item);
        }

        public void Update(GoodOrder item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<GoodOrder> Find(Func<GoodOrder, Boolean> predicate)
        {
            return db.GoodOrders.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Good item = db.Goods.Find(id);
            if (item != null)
                db.Goods.Remove(item);
        }
    }
}
