using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;
using DomainModel.Extensions;
using System.Configuration;
using DomainModel.Interfaces;
using System.Data.Linq;

namespace DomainModel
{
    public class UserMapper : IUserMapper
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["userEntities"].ConnectionString;
        private databaseDataContext context;

        public UserMapper(databaseDataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            this.context = context;
        }
        public List<User> GetAllUsers()
        {
            return context.UsersDatas.Select(row => row.ConvertToUser()).ToList();
        }

        public void Create(User user)
        {
            UsersData data = new UsersData()
            {
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Phone = user.Phone
            };
            context.UsersDatas.InsertOnSubmit(data);
            context.SubmitChanges();
        }

        public User Retrieve(int userId)
        {
            return context.UsersDatas.First(x => x.UserID == userId).ConvertToUser();
        }

        public void Update(User user)
        {
            UsersData data = context.UsersDatas.First(x => x.UserID == user.UserID);
            data.Address = user.Address;
            data.Phone = user.Phone;
            data.Name = user.Name;
            data.Surname = user.Surname;
            context.SubmitChanges();
        }

        public void Delete(int userId)
        {
            var userToDelete = context.UsersDatas.First(x => x.UserID == userId);
            context.UsersDatas.DeleteOnSubmit(userToDelete);
            context.SubmitChanges();
        }

        public int GetCount()
        {
            return context.UsersDatas.Count();
        }

        public List<User> GetRange(int skip, int items)
        {
            return context.UsersDatas.Select(row => row.ConvertToUser()).Skip(skip).Take(items).ToList();
        }
    }
}
