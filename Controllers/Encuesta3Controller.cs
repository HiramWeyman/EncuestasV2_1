using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Models;
using EncuestasV2.Filters;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace EncuestasV2.Controllers
{
    public class Encuesta3Controller : Controller
    {

        // GET: Encuesta2
        public ActionResult Index(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 3
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 8; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index4?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index4(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 4
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar4()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar4(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 6; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index5?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index5(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 5
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar5()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar5(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 10; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index6?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index6(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 6
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar6()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar6(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 5; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index7?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index7(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 7
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar7()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar7(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 5; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index8?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index8(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 8
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar8()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar8(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 6; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index9?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index9(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 9
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar9()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar9(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 6; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index10?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index10(string user)
        {
            if (user != null)
            {
                
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 10
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar10()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar10(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 14; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index11?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index11(string user)
        {
            //if (user != null)
            if(2<3)
            {
                user = "kiko@hotmail.com";
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 11
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar11()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar11(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 5; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }

                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index12?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

    }
}