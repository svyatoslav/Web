using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApp.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        protected IActionFilter preFilter;
        protected IActionFilter postFilter;

        public LogFilter() { }

        public LogFilter(IActionFilter preFilter, IActionFilter postFilter) 
        {
            this.preFilter = preFilter;
            this.postFilter = postFilter;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (preFilter != null)
            {
                preFilter.OnActionExecuting(filterContext);
            }
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt", "logFilter: Action Before" +  Environment.NewLine);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt", "logFilter: Action After" + Environment.NewLine);
            if (postFilter != null)
            {
                postFilter.OnActionExecuted(filterContext);
            }
        }
    }
}