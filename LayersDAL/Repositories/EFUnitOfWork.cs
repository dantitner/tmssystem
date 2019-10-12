using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayersDAL.EF;
using LayersDAL.Entities;
using LayersDAL.Interfaces;

namespace LayersDAL.Repositories
{
    public class EFUnitOfWork : IDisposable,IUnitOfWork
    {
        private TimeManagmentSytemContex db;
        private UserRepository userRepository;
        private SessionRepository sessionRepository;
        private CompanyRepository companyRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new TimeManagmentSytemContex(connectionString);
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Session> Sessions
        {
            get
            {
                if (sessionRepository == null)
                    sessionRepository = new SessionRepository(db);
                return sessionRepository;
            }
        }

        public IRepository<Company> Companies
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(db);
                return companyRepository;
            }
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
