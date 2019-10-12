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
    public class CompanyRepository : IRepository<Company>
    {
        private TimeManagmentSytemContex db;

        public CompanyRepository(TimeManagmentSytemContex contex)
        {
            this.db = contex;
        }


        public void Create(Company item)
        {
            db.Сompanies.Add(item);
        }

        public void Delete(int id)
        {
            Company item = db.Сompanies.Find(id);
            if (item != null)
                db.Сompanies.Remove(item);
        }

        public IEnumerable<Company> Find(Func<Company, bool> predicate)
        {
            return db.Сompanies.Where(predicate).ToList();
        }

        public Company Get(int id)
        {
            return db.Сompanies.Find(id);
        }

        public IEnumerable<Company> GetAll()
        {
            return db.Сompanies;
        }


        public void Update(Company item)
        {
            //нашел компанию поменял данные
            var toChange = db.Сompanies.Find(item.Id);
            toChange.Name = item.Name;
            toChange.EnteringPassword = item.EnteringPassword;

            bool wereDeleted = false; //флаг если были удалены пользователи

            //удаление пользователей
            for (int i = 0; i < toChange.Users.Count; i++)
            {
               
                if (item.Users.Find(n => n.Id == toChange.Users[i].Id) == null)
                {
                    toChange.Users.Remove(toChange.Users[i]);
                    wereDeleted = true;
                }
            }
            //добавление пользователей
            if (!wereDeleted)
            {
                for (int i = 0; i < item.Users.Count; i++)
                {
                    if (toChange.Users.Find(n => n.Id == item.Users[i].Id) == null)
                    {
                        var user = db.Users.Find(item.Users[i].Id);
                        if(user != null)
                        toChange.Users.Add(user);
                    }
                        
                }

            }
            db.Entry(toChange).State = EntityState.Modified;
        }
    }
}
