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
    class RoleRepository : IRepository<Role>
    {
        private StoreContext db;

        public RoleRepository(StoreContext context)
        {
            this.db = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        public void Update(Role item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public IEnumerable<Role> Find(Func<Role, Boolean> predicate)
        {
            return db.Roles.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Role item = db.Roles.Find(id);
            if (item != null)
                db.Roles.Remove(item);
        }
    }
}
