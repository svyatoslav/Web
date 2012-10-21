using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Interface;
using DomainModel;
using DomainModel.Models;

namespace WebApp.Helper
{
    public class ValueListIterator : IValueListIterator
    {
        private readonly UserMapper provider;
        private readonly int itemsPerPage;

        public ValueListIterator(UserMapper provider, int itemsPerPage)
        {
            this.provider = provider;
            this.itemsPerPage = itemsPerPage;
        }

        public int Size
        {
            get
            {
                return provider.GetCount();
            }
        }

        public List<User> GetCurrentElement(int page)
        {
            return provider.GetRange((page - 1) * itemsPerPage, itemsPerPage);
        }
    }
}