using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayersDAL.Entities;
using System.Data.Entity;

namespace LayersDAL.EF
{
    public class TimeManagmentSytemContex : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Company> Сompanies { get; set; }

        static TimeManagmentSytemContex()
        {
            Database.SetInitializer<TimeManagmentSytemContex>(new TMSDbInitializer());
        }
        public TimeManagmentSytemContex(string connectionString) : base(connectionString)
        { }

    }

    public class TMSDbInitializer : DropCreateDatabaseAlways<TimeManagmentSytemContex>
    {
        protected override void Seed(TimeManagmentSytemContex context)
        {
            User u1 = new User { Id = 1, Name = "Name1", Email = "1email@mail.com", Login = "user1", Password = "1"};
            User u2 = new User { Id = 2, Name = "Name2", Email = "2email@mail.com", Login = "user2", Password = "1" };
            User u3 = new User { Id = 3, Name = "Name3", Email = "3email@mail.com", Login = "user3", Password = "1" };
            User u4 = new User { Id = 4, Name = "Name4", Email = "4email@mail.com", Login = "user4", Password = "1" };
            User u5 = new User { Id = 5, Name = "Name5", Email = "5email@mail.com", Login = "user5", Password = "1" };
            User u6 = new User { Id = 6, Name = "Name6", Email = "6email@mail.com", Login = "user6", Password = "1" };

            context.Users.Add(u1);
            context.Users.Add(u4);
            context.Users.Add(u3);
            context.Users.Add(u2);
            context.Users.Add(u5);
            context.Users.Add(u6);
            Company c1 = new Company
            {
                Id = 1,
                Name = "Company Name 1",
                Creator = u1,
                EnteringPassword = "1",
                Users = new List<User>() { u4, u3 }
            };

            Company c2 = new Company
            {
                Id =2,
                Name = "Company Name 2",
                Creator = u2,
                EnteringPassword = "2",
                Users = new List<User>() { u5, u6 }
            };

            context.Сompanies.Add(c1);
            context.Сompanies.Add(c2);

            Session s1 = new Session { Id = 1, CompanyId = 1, UserId = 3,
                StartTime = DateTime.Now.AddDays(-1).AddHours(-2) };
            Session s2 = new Session
            {
                Id = 2,
                CompanyId = 1,
                UserId = 3,
                StartTime = DateTime.Now.AddDays(-2).AddHours(-2),
                EndTime = DateTime.Now.AddDays(-2).AddHours(1)
            };
            Session s3 = new Session
            {
                Id = 3,
                CompanyId = 1,
                UserId = 3,
                StartTime = DateTime.Now.AddDays(-3).AddHours(-2),
                EndTime = DateTime.Now.AddDays(-3).AddHours(1)
            };
            Session s4 = new Session
            {
                Id = 4,
                CompanyId = 1,
                UserId = 3,
                StartTime = DateTime.Now.AddDays(-4).AddHours(-2),
                EndTime = DateTime.Now.AddDays(-4).AddHours(1)
            };
            Session s5 = new Session
            {
                Id = 5,
                CompanyId = 1,
                UserId = 3,
                StartTime = DateTime.Now.AddDays(-5).AddHours(-2),
                EndTime = DateTime.Now.AddDays(-5).AddHours(1)
            };
            Session s6 = new Session
            {
                Id = 6,
                CompanyId = 1,
                UserId = 3,
                StartTime = DateTime.Now.AddDays(-6).AddHours(-2),
                EndTime = DateTime.Now.AddDays(-6).AddHours(1)
            };
            Session s7 = new Session
            {
                Id = 7,
                CompanyId = 1,
                UserId = 4,
                StartTime = DateTime.Now.AddDays(-6).AddHours(-2),
                EndTime = DateTime.Now.AddDays(-6).AddHours(1)
            };
            Session s8 = new Session
            {
                Id = 8,
                CompanyId = 1,
                UserId = 4,
                StartTime = DateTime.Now.AddDays(-6).AddHours(-2),
                EndTime = DateTime.Now.AddDays(-6).AddHours(1)
            };

            context.Sessions.Add(s1);
            context.Sessions.Add(s2);
            context.Sessions.Add(s3);
            context.Sessions.Add(s4);
            context.Sessions.Add(s5);
            context.Sessions.Add(s6);
            context.Sessions.Add(s7);
            context.Sessions.Add(s8);

            context.SaveChanges();
        }
    }
}
