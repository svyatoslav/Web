using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;

namespace WebApp.Interface
{
    interface IValueListIterator
    {
        int Size {get;}

        List<User> GetCurrentElement(int page);
    }
}
