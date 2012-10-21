using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;

namespace DomainModel.Interfaces
{
    public interface IUnitOfWork
    {
        void RegisterNew(User user);
        void RegisterDirty(User user);
        void RegisterClean(User user);
        void RegisterDeleted(int userID);
        void Commit();
        void Rollback();
    }
}
