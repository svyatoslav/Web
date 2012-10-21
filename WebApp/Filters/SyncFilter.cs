using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;

namespace WebApp.Filters
{
    public class SyncFilterAttribute : ActionFilterAttribute 
    {
        public static Hashtable requestHistory;
        public static readonly string marker = "syncMarker";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpContextBase ctx = filterContext.HttpContext;
            
            ensureHistory(ctx);
            
            int lastTicket = GetLastTicket(ctx);
            
            int thisTicket = GetCurrentTicket(ctx, lastTicket);
            
            if (thisTicket > lastTicket || (thisTicket == lastTicket && thisTicket == 0))
            {
                UpdateLastRefreshTicket(ctx, thisTicket);
            }
            else
            {
                clearPath(ctx, thisTicket);
                
                ctx.Response.Redirect("/Home/Error");
            }

            
            base.OnActionExecuting(filterContext);

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            int ticket = (int)ctx.Items[SyncFilterAttribute.marker];

            ctx.Response.Write(String.Format("<input type='hidden' name='{0}' id='{0}' value='{1}' form='mform'/>", SyncFilterAttribute.marker, ticket));


            base.OnActionExecuted(filterContext);
        }

        private static void clearPath(HttpContextBase ctx, int ticket)
        {
            requestHistory.Remove(ctx.Request.Path);
            ctx.Session[marker] = requestHistory;
        }

        private void ensureHistory(HttpContextBase ctx)
        {
            if (ctx.Session[marker] != null)
            {
                requestHistory = (Hashtable)ctx.Session[marker];
            }
            else
            {
                requestHistory = new Hashtable();
            }
        }
        // Возвращает маркер последнего выполненного запроса с данным URL
        private int GetLastTicket(HttpContextBase ctx)
        {
            // Извлекаем и возвращаем последний маркер
            if (!requestHistory.ContainsKey(ctx.Request.Path))
                return 0;
            else
                return (int)requestHistory[ctx.Request.Path];
        }
        // Возвращает маркер, ассоциированный со страницей
        private int GetCurrentTicket(HttpContextBase ctx, int lastTicket)
        {
            int ticket;
            //HttpCookie o = ctx.Request.Cookies.Get(SyncFilterAttribute.marker);
            object o = ctx.Request[SyncFilterAttribute.marker];
            if (o == null)
                ticket = lastTicket;
            else
                ticket = Convert.ToInt32(o);
            ctx.Items[marker] = ticket + 1;
            return ticket;
        }
        // Сохраняет маркер последнего выполненного запроса с данным URL
        private void UpdateLastRefreshTicket(HttpContextBase ctx, int ticket)
        {
            requestHistory[ctx.Request.Path] = ticket;
            ctx.Session[marker] = requestHistory;

        }
    }
}