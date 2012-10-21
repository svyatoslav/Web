using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Models;
using System.Web.Mvc;

namespace WebApp.Helper
{
    public static class PagingHelper
    {
        public static Paging GetPagingModel(int page, int totalItems, int itemsPerPage, string baseUrl)
        {
            int totalPages = (totalItems - 1)/ itemsPerPage + 1;
           

            Paging paging = new Paging()
            {
                BaseUrl = baseUrl + "?page=",
                Current = page,
                Total = totalPages,
                ShowFirst = page != 1,
                FirstUrl = 1,
                ShowLast = page != totalPages,
                LastUrl = totalPages,
                ShowNext = page + 1 < totalPages,
                NextUrl = (page + 1),
                ShowPrev = page - 1 > 1,
                PrevUrl = (page - 1)
            };

            return paging;
        }
    }
}