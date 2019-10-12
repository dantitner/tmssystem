using LayersDAL.EF;
using LayersDAL.Entities;
using LayersDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayersDAL.Repositories
{
    class EFUnitOfWork : IUnitOfWork
    {
        private StoreContext db;
        private GoodRepository goodRepository;
        private OrderRepository orderRepository;
        private GoodOrderRepository goodOrderRepository;
        private UserRepository userRepository;
        private RoleRepository roleRepository;


        public EFUnitOfWork(string connectionString)
        {
            db = new StoreContext(connectionString);
        }
        public IRepository<Good> Goods
        {
            get
            {
                if (goodRepository == null)
                    goodRepository = new GoodRepository(db);
                return goodRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public IRepository<GoodOrder> GoodOrders
        {
            get
            {
                if (goodOrderRepository == null)
                    goodOrderRepository = new GoodOrderRepository(db);
                return goodOrderRepository;
            }
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

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}