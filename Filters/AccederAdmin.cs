using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasV2.Filters
{
    public class AccederAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var usuario = HttpContext.Current.Session["Usuario"];
            if (usuario == null)
            {

                filterContext.Result = new RedirectResult("~/Usuarios/Admin");

            }
            base.OnActionExecuted(filterContext);
        }
    }
}