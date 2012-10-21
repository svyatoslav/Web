using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using DomainModel.Models;

namespace DomainModel.Interfaces
{
    public interface IUserMapper
    {
        void Create(User user);
        User Retrieve(int userID);
        void Update(User user);
        void Delete(int userId);
        List<User> GetAllUsers();

       // void Save();
    }
}
