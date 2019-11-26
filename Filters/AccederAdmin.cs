using EncuestasV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace EncuestasV2.Filters
{
    public class AccederAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var usuario = HttpContext.Current.Session["Usuario"];
            string tipo = "A";
            string usuarioAd = "";
            using (var db = new csstdura_encuestaEntities()) {

                //numeroVeces = db.encuesta_usuarios.Where(p => p.usua_n_usuario == usuario.ToString()
                //                   && p.usua_tipo == "A").Count();
                usuarioAd = db.Database.SqlQuery<string>("Select usua_n_usuario from encuesta_usuarios where usua_n_usuario=@usuario and usua_tipo=@tipo", new SqlParameter("@usuario", usuario), new SqlParameter("@tipo", tipo))
                         .FirstOrDefault();
            }
               
            if (usuarioAd != null)
            {

                filterContext.Result = new RedirectResult("~/Usuarios/Admin");

            }
            base.OnActionExecuted(filterContext);
        }
    }
}