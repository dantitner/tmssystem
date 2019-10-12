using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayersDAL.Entities;
using LayersDAL.Interfaces;
using LayersDAL.EF;
using System.Data.Entity;
namespace LayersDAL.Repositories
{
    public class SessionRepository : IRepository<Session>
    {
        private TimeManagmentSytemContex db;

        public SessionRepository(TimeManagmentSytemContex contex)
        {
            this.db = contex;
        }


        public void Create(Session item)
        {
            db.Sessions.Add(item);
        }

        public void Delete(int id)
        {
            Session item = db.Sessions.Find(id);
            if (item != null)
                db.Sessions.Remove(item);
        }

        public IEnumerable<Session> Find(Func<Session, bool> predicate)
        {
            return db.Sessions.Where(predicate).ToList();
        }

        public Session Get(int id)
        {
            return db.Sessions.Find(id);
        }

        public IEnumerable<Session> GetAll()
        {
            return db.Sessions;
        }

        public void Update(Session item)
        {
            var toChange = db.Sessions.Find(item.Id);
            toChange.Commentary = item.Commentary;
            if(toChange.EndTime == null)
            toChange.EndTime = DateTime.Now;
            db.Entry(toChange).State = EntityState.Modified;
        }

    }
}
