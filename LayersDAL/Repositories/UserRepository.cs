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
    public class UserRepository : IRepository<User>
    {
        private TimeManagmentSytemContex db;

        public UserRepository(TimeManagmentSytemContex contex)
        {
            this.db = contex;
        }


        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User item = db.Users.Find(id);
            if (item != null)
                db.Users.Remove(item);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            User user = db.Users.Find(item.Id);
            user.Login = item.Login;
            user.Name = item.Name;
            user.Password = item.Password;
            user.Email = item.Email;
            db.Entry(user).State = EntityState.Modified;
        }

    }
}
