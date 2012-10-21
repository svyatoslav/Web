using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces;
using DomainModel.Models;

namespace DomainModel
{
    public class UnitOfWork : IUnitOfWork
    {
        private List<User> newObjects;
        private List<User> dirtyObjects;
        private List<User> cleanObjects;
        private List<int> removedObjects;

        private IUserMapper repository;

        public UnitOfWork(IUserMapper repository)
        {
            newObjects = new List<User>();
            cleanObjects = new List<User>();
            dirtyObjects = new List<User>();
            removedObjects = new List<int>();
            this.repository = repository;
        }

        public void RegisterNew(User user)
        {
            if (user != null && !newObjects.Contains(user))
            {
                newObjects.Add(user);
            }
        }

        public void RegisterDirty(User user)
        {
            if (user != null)
            {
                bool isInNew = false;
                foreach (User u in newObjects)
                {
                    if (u.UserID == user.UserID)
                    {
                        newObjects.Remove(u);
                        newObjects.Add(user);
                        isInNew = true;
                        break;
                    }
                }
                if (!isInNew)
                    dirtyObjects.Add(user);
            }
        }

        public void RegisterClean(User user)
        {
            cleanObjects.Add(user);
        }

        public void RegisterDeleted(int userID)
        {
            removedObjects.Add(userID);
        }

        public void Commit()
        {
            foreach (User user in newObjects)
            {
                repository.Create(user);
            }
            foreach (int userId in removedObjects)
            {
                repository.Delete(userId);
            }
            foreach (User user in dirtyObjects)
            {
                repository.Update(user);
            }
            Clear();
        }

        public void Rollback()
        {
            Clear();
        }

        private void Clear()
        {
            newObjects.Clear();
            cleanObjects.Clear();
            dirtyObjects.Clear();
            removedObjects.Clear();
        }

    }
}
