using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Filters;
using EncuestasV2.Models;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace EncuestasV2.Controllers
{
    [AccederAdmin]
    public class AdminController : Controller
    {
        List<SelectListItem> listaEmpresa;
        List<SelectListItem> listaSexo;
        List<SelectListItem> listaEdad;
        List<SelectListItem> listaEdoCivil;
        List<SelectListItem> listaOpciones;
        List<SelectListItem> listaProceso;
        List<SelectListItem> listaPuesto;
        List<SelectListItem> listaContrata;
        List<SelectListItem> listaPersonal;
        List<SelectListItem> listaJornada;
        List<SelectListItem> listaRotacion;
        List<SelectListItem> listaTiempo;
        List<SelectListItem> listaExpLab;

        //Catalogos

        private void llenarEmpresa()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaEmpresa = (from emp in db.encuesta_empresa
                                select new SelectListItem
                                {
                                    Value = emp.emp_id.ToString(),
                                    Text = emp.emp_descrip,
                                    Selected = false

                                }).ToList();
                listaEmpresa.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }
        private void llenarSexo()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaSexo = (from sexo in db.encuesta_sexo
                             select new SelectListItem
                             {
                                 Value = sexo.sexo_id.ToString(),
                                 Text = sexo.sexo_desc,
                                 Selected = false

                             }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarEdad()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaEdad = (from edad in db.encuesta_edades
                             select new SelectListItem
                             {
                                 Value = edad.edad_id.ToString(),
                                 Text = edad.edad_desc,
                                 Selected = false

                             }).ToList();
                listaEdad.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarEdoCivil()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaEdoCivil = (from edo in db.encuesta_edocivil
                                 select new SelectListItem
                                 {
                                     Value = edo.edocivil_id.ToString(),
                                     Text = edo.edocivil_desc,
                                     Selected = false

                                 }).ToList();
                listaEdoCivil.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarOpciones()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaOpciones = (from op in db.encuaesta_opciones
                                 select new SelectListItem
                                 {
                                     Value = op.opcion_id.ToString(),
                                     Text = op.opcion_desc,
                                     Selected = false

                                 }).ToList();
                listaOpciones.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarProcesoEdu()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaProceso = (from proc in db.encuesta_procesoedu
                                select new SelectListItem
                                {
                                    Value = proc.procesoedu_id.ToString(),
                                    Text = proc.procesoedu_desc,
                                    Selected = false

                                }).ToList();
                listaProceso.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarTipoPuesto()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaPuesto = (from puesto in db.encuesta_tipopuesto
                               select new SelectListItem
                               {
                                   Value = puesto.tipopuesto_id.ToString(),
                                   Text = puesto.tipopuesto_desc,
                                   Selected = false

                               }).ToList();
                listaPuesto.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarTipoContratacion()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaContrata = (from contra in db.encuesta_tipocontrata
                                 select new SelectListItem
                                 {
                                     Value = contra.tipocont_id.ToString(),
                                     Text = contra.tipocont_desc,
                                     Selected = false

                                 }).ToList();
                listaContrata.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarTipoPersonal()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaPersonal = (from personal in db.encuesta_tipopersonal
                                 select new SelectListItem
                                 {
                                     Value = personal.tipoperson_id.ToString(),
                                     Text = personal.tipoperson_desc,
                                     Selected = false

                                 }).ToList();
                listaPersonal.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarTipoJornada()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaJornada = (from jornada in db.encuesta_tipojornada
                                select new SelectListItem
                                {
                                    Value = jornada.tipojornada_id.ToString(),
                                    Text = jornada.tipojornada_desc,
                                    Selected = false

                                }).ToList();
                listaJornada.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarRotacionTurno()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaRotacion = (from rotacion in db.encuaesta_rotacion
                                 select new SelectListItem
                                 {
                                     Value = rotacion.rotacionturno_id.ToString(),
                                     Text = rotacion.rotacionturno_desc,
                                     Selected = false

                                 }).ToList();
                listaRotacion.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }
        private void llenarTiempoEmp()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaTiempo = (from tiempo in db.encuesta_tiempopuesto
                               select new SelectListItem
                               {
                                   Value = tiempo.tiempopue_id.ToString(),
                                   Text = tiempo.tiempopue_desc,
                                   Selected = false

                               }).ToList();
                listaTiempo.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarExpLab()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaExpLab = (from exp in db.encuesta_explab
                               select new SelectListItem
                               {
                                   Value = exp.explab_id.ToString(),
                                   Text = exp.explab_desc,
                                   Selected = false

                               }).ToList();
                listaExpLab.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        public void listarCombos()
        {

            llenarEmpresa();
            llenarSexo();
            llenarEdad();
            llenarEdoCivil();
            llenarOpciones();
            llenarProcesoEdu();
            llenarTipoPuesto();
            llenarTipoContratacion();
            llenarTipoPersonal();
            llenarTipoJornada();
            llenarRotacionTurno();
            llenarTiempoEmp();
            llenarExpLab();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Empleados(encuesta_usuariosCLS empleados_)
        {
            int id_empresa = empleados_.usua_empresa;
            int id_genero = empleados_.usua_genero;
            int id_edo_civ = empleados_.usua_edo_civil;
            int id_puesto = empleados_.usua_tipo_puesto;
            int id_contrata = empleados_.usua_tipo_contratacion;
            int id_personal = empleados_.usua_tipo_personal;
            int id_jornada = empleados_.usua_tipo_jornada;
            int id_tiempo = empleados_.usua_tiempo_puesto;
            int id_expLab = empleados_.usua_exp_laboral;

            listarCombos();

            ViewBag.listaEmpresa = listaEmpresa;
            ViewBag.listaSexo = listaSexo;
            ViewBag.listaEdad = listaEdad;
            ViewBag.listaEdoCivil = listaEdoCivil;
            ViewBag.listaOpciones = listaOpciones;
            ViewBag.listaProceso = listaProceso;
            ViewBag.listaPuesto = listaPuesto;
            ViewBag.listaContrata = listaContrata;
            ViewBag.listaPersonal = listaPersonal;
            ViewBag.listaJornada = listaJornada;
            ViewBag.listaRotacion = listaRotacion;
            ViewBag.listaTiempo = listaTiempo;
            ViewBag.listaExpLab = listaExpLab;

            List<encuesta_usuariosCLS> listaEmpleado = null;
            List<encuesta_usuariosCLS> listaRpta = null;
            using (var db = new csstdura_encuestaEntities())
            {
                listaEmpleado = (from empleado in db.encuesta_usuarios
                                 join empresa in db.encuesta_empresa
                                 on empleado.usua_empresa equals empresa.emp_id
                                 join genero in db.encuesta_sexo
                                 on empleado.usua_genero equals genero.sexo_id
                                 join edad_emp in db.encuesta_edades
                                 on empleado.usua_edad equals edad_emp.edad_id
                                 join edo in db.encuesta_edocivil
                                 on empleado.usua_edo_civil equals edo.edocivil_id
                                 join op in db.encuaesta_opciones
                                 on empleado.usua_sin_forma equals op.opcion_id
                                 join primaria in db.encuesta_procesoedu
                                 on empleado.usua_primaria equals primaria.procesoedu_id
                                 join secundaria in db.encuesta_procesoedu
                                 on empleado.usua_secundaria equals secundaria.procesoedu_id
                                 join prepa in db.encuesta_procesoedu
                                 on empleado.usua_preparatoria equals prepa.procesoedu_id
                                 join tecnico in db.encuesta_procesoedu
                                 on empleado.usua_tecnico equals tecnico.procesoedu_id
                                 join lic in db.encuesta_procesoedu
                                 on empleado.usua_licenciatura equals lic.procesoedu_id
                                 join maestria in db.encuesta_procesoedu
                                 on empleado.usua_maestria equals maestria.procesoedu_id
                                 join doc in db.encuesta_procesoedu
                                 on empleado.usua_doctorado equals doc.procesoedu_id
                                 join tipopuesto in db.encuesta_tipopuesto
                                 on empleado.usua_tipo_puesto equals tipopuesto.tipopuesto_id
                                 join tipocont in db.encuesta_tipocontrata
                                 on empleado.usua_tipo_contratacion equals tipocont.tipocont_id
                                 join tipopersonal in db.encuesta_tipopersonal
                                 on empleado.usua_tipo_personal equals tipopersonal.tipoperson_id
                                 join tipojornada in db.encuesta_tipojornada
                                 on empleado.usua_tipo_jornada equals tipojornada.tipojornada_id
                                 join rota in db.encuaesta_rotacion
                                 on empleado.usua_rotacion_turno equals rota.rotacionturno_id
                                 join tiempo in db.encuesta_tiempopuesto
                                 on empleado.usua_tiempo_puesto equals tiempo.tiempopue_id
                                 join exp in db.encuesta_explab
                                 on empleado.usua_exp_laboral equals exp.explab_id
                                 select new encuesta_usuariosCLS
                                 {
                                     usua_id = empleado.usua_id,
                                     usua_nombre = empleado.usua_nombre,
                                     usua_estatus = empleado.usua_estatus,
                                     usua_n_usuario = empleado.usua_n_usuario,
                                     usua_p_usuario = empleado.usua_p_usuario,
                                     usua_empresa = (int)empleado.usua_empresa,
                                     usua_genero = (int)empleado.usua_genero,
                                     usua_edad = (int)empleado.usua_edad,
                                     usua_edo_civil = (int)empleado.usua_edo_civil,
                                     empleado_empresa = empresa.emp_descrip,
                                     empleado_genero = genero.sexo_desc,
                                     empleado_edad = edad_emp.edad_desc,
                                     empleado_edocivil = edo.edocivil_desc,
                                     empleado_sinformacion = op.opcion_desc,
                                     empleado_primaria = primaria.procesoedu_desc,
                                     empleado_secundaria = secundaria.procesoedu_desc,
                                     empleado_preparatoria = prepa.procesoedu_desc,
                                     empleado_tecnico = tecnico.procesoedu_desc,
                                     empleado_licenciatura = lic.procesoedu_desc,
                                     empleado_maestria = maestria.procesoedu_desc,
                                     empleado_doctorado = doc.procesoedu_desc,
                                     empleado_tipopuesto = tipopuesto.tipopuesto_desc,
                                     empleado_tipocontata = tipocont.tipocont_desc,
                                     empleado_tipopersonal = tipopersonal.tipoperson_desc,
                                     empleado_tipojornada = tipojornada.tipojornada_desc,
                                     empleado_rotacion = rota.rotacionturno_desc,
                                     empleado_tiempopuesto = tiempo.tiempopue_desc,
                                     empleado_explab = exp.explab_desc

                                 }).ToList();
                if (empleados_.usua_id == 0 && empleados_.usua_empresa == 0 && empleados_.usua_genero == 0 && empleados_.usua_edad == 0 && empleados_.usua_edo_civil == 0
                    && empleados_.usua_tipo_puesto == 0 && empleados_.usua_tipo_contratacion == 0 && empleados_.usua_tipo_personal == 0
                    && empleados_.usua_tipo_jornada == 0 && empleados_.usua_tiempo_puesto == 0 && empleados_.usua_exp_laboral == 0)
                {

                    listaRpta = listaEmpleado;
                }
                else
                {
                    if (empleados_.usua_empresa != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_empresa.Equals(empleados_.usua_empresa)).ToList();
                    }

                    if (empleados_.usua_genero != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_genero.ToString().Contains(empleados_.usua_genero.ToString())).ToList();
                    }

                    if (empleados_.usua_edad != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_edad.ToString().Contains(empleados_.usua_edad.ToString())).ToList();
                    }

                    if (empleados_.usua_edo_civil != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_edo_civil.ToString().Contains(empleados_.usua_edo_civil.ToString())).ToList();
                    }

                    if (empleados_.usua_tipo_puesto != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tipo_puesto.ToString().Contains(empleados_.usua_tipo_puesto.ToString())).ToList();
                    }

                    if (empleados_.usua_tipo_contratacion != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tipo_contratacion.ToString().Contains(empleados_.usua_tipo_contratacion.ToString())).ToList();
                    }

                    if (empleados_.usua_tipo_personal != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tipo_personal.ToString().Contains(empleados_.usua_tipo_personal.ToString())).ToList();
                    }

                    if (empleados_.usua_tipo_jornada != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tipo_jornada.ToString().Contains(empleados_.usua_tipo_jornada.ToString())).ToList();
                    }

                    if (empleados_.usua_tiempo_puesto != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tiempo_puesto.ToString().Contains(empleados_.usua_tiempo_puesto.ToString())).ToList();
                    }

                    if (empleados_.usua_exp_laboral != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_exp_laboral.ToString().Contains(empleados_.usua_exp_laboral.ToString())).ToList();
                    }

                    listaRpta = listaEmpleado;

                }


            }
            return View(listaRpta);
        }
        [AccederAdmin]
        public ActionResult CatalogoEmpresa()
        {
            //var test = Session["Usuario"].ToString();
            if (Session["Usuario"] != null)
            {
                ViewBag.user = Session["Usuario"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }

        }
        [HttpPost]
        public ActionResult InsertCatalogoEmp(encuesta_empresaCLS Oencuesta_empresaCLS)
        {
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        encuesta_empresa empresa = new encuesta_empresa();
                        empresa.emp_descrip = Oencuesta_empresaCLS.emp_descrip;
                        empresa.emp_estatus = "A";
                        empresa.emp_u_alta = Oencuesta_empresaCLS.emp_u_alta;
                        empresa.emp_f_alta = DateTime.Now;
                        empresa.emp_u_cancela = "";
                        empresa.emp_f_cancela = null;
                        empresa.emp_no_trabajadores = Oencuesta_empresaCLS.emp_no_trabajadores;
                        empresa.emp_direccion = Oencuesta_empresaCLS.emp_direccion;
                        empresa.emp_telefono = Oencuesta_empresaCLS.emp_telefono;
                        empresa.emp_person_contac = Oencuesta_empresaCLS.emp_person_contac;
                        empresa.emp_correo = Oencuesta_empresaCLS.emp_correo;
                        empresa.emp_cp = Oencuesta_empresaCLS.emp_cp;
                        db.encuesta_empresa.Add(empresa);
                        res = db.SaveChanges();
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }
        public ActionResult ListarEmpresa(encuesta_empresaCLS oEmpresa)
        {

            List<encuesta_empresaCLS> listaEmpresa = null;
            string nombre_empresa = oEmpresa.emp_descrip;
            using (var db = new csstdura_encuestaEntities())
            {
                if (oEmpresa.emp_descrip == null)
                {
                    listaEmpresa = (from empresa in db.encuesta_empresa
                                    select new encuesta_empresaCLS
                                    {
                                        emp_id = empresa.emp_id,
                                        emp_descrip = empresa.emp_descrip,
                                        emp_estatus = empresa.emp_estatus,
                                        emp_u_alta = empresa.emp_u_alta,
                                        emp_f_alta = (DateTime)empresa.emp_f_alta,
                                        emp_u_cancela = empresa.emp_u_cancela,
                                        emp_f_cancela = (DateTime)empresa.emp_f_cancela,
                                        emp_no_trabajadores = empresa.emp_no_trabajadores,
                                        emp_direccion = empresa.emp_direccion,
                                        emp_telefono = empresa.emp_telefono,
                                        emp_person_contac = empresa.emp_person_contac,
                                        emp_correo = empresa.emp_correo,
                                        emp_cp = empresa.emp_cp
                                    }).ToList();
                }
                else
                {

                    listaEmpresa = (from empresa in db.encuesta_empresa
                                    where empresa.emp_descrip.Contains(nombre_empresa)
                                    select new encuesta_empresaCLS
                                    {
                                        emp_id = empresa.emp_id,
                                        emp_descrip = empresa.emp_descrip,
                                        emp_estatus = empresa.emp_estatus,
                                        emp_u_alta = empresa.emp_u_alta,
                                        emp_f_alta = (DateTime)empresa.emp_f_alta,
                                        emp_u_cancela = empresa.emp_u_cancela,
                                        emp_f_cancela = (DateTime)empresa.emp_f_cancela,
                                        emp_no_trabajadores = empresa.emp_no_trabajadores,
                                        emp_direccion = empresa.emp_direccion,
                                        emp_telefono = empresa.emp_telefono,
                                        emp_person_contac = empresa.emp_person_contac,
                                        emp_correo = empresa.emp_correo,
                                        emp_cp = empresa.emp_cp
                                    }).ToList();

                }

            }
            return View(listaEmpresa);
        }
        public ActionResult EditarEmpresa(int id)
        {
            encuesta_empresaCLS Oencuesta_empresaCLS = new encuesta_empresaCLS();

            using (var db = new csstdura_encuestaEntities())
            {
                encuesta_empresa Oencuesta_empresa = db.encuesta_empresa.Where(p => p.emp_id.Equals(id)).First();
                Oencuesta_empresaCLS.emp_id = Oencuesta_empresa.emp_id;
                Oencuesta_empresaCLS.emp_descrip = Oencuesta_empresa.emp_descrip;
                Oencuesta_empresaCLS.emp_estatus = Oencuesta_empresa.emp_estatus;
                Oencuesta_empresaCLS.emp_no_trabajadores = Oencuesta_empresa.emp_no_trabajadores;
                Oencuesta_empresaCLS.emp_direccion = Oencuesta_empresa.emp_direccion;
                Oencuesta_empresaCLS.emp_telefono = Oencuesta_empresa.emp_telefono;
                Oencuesta_empresaCLS.emp_person_contac = Oencuesta_empresa.emp_person_contac;
                Oencuesta_empresaCLS.emp_correo = Oencuesta_empresa.emp_correo;
                Oencuesta_empresaCLS.emp_cp = Oencuesta_empresa.emp_cp;
            }
            return View(Oencuesta_empresaCLS);

        }
        [HttpPost]
        public ActionResult EditarEmpresa(encuesta_empresaCLS Oencuesta_EmpresaCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(Oencuesta_EmpresaCLS);
            }
            int id_empresa = Oencuesta_EmpresaCLS.emp_id;
            using (var db = new csstdura_encuestaEntities())
            {
                encuesta_empresa Oencuesta_empresa = db.encuesta_empresa.Where(p => p.emp_id.Equals(id_empresa)).First();
                Oencuesta_empresa.emp_descrip = Oencuesta_EmpresaCLS.emp_descrip;
                Oencuesta_empresa.emp_estatus = Oencuesta_EmpresaCLS.emp_estatus;
                Oencuesta_empresa.emp_no_trabajadores = Oencuesta_EmpresaCLS.emp_no_trabajadores;
                Oencuesta_empresa.emp_direccion = Oencuesta_EmpresaCLS.emp_direccion;
                Oencuesta_empresa.emp_telefono = Oencuesta_EmpresaCLS.emp_telefono;
                Oencuesta_empresa.emp_person_contac = Oencuesta_EmpresaCLS.emp_person_contac;
                Oencuesta_empresa.emp_correo = Oencuesta_EmpresaCLS.emp_correo;
                Oencuesta_empresa.emp_cp = Oencuesta_EmpresaCLS.emp_cp;
                db.SaveChanges();

            }
            return RedirectToAction("ListarEmpresa");
        }

        public ActionResult EliminarEmpresa(int id)
        {
            encuesta_empresaCLS Oencuesta_empresaCLS = new encuesta_empresaCLS();

            using (var db = new csstdura_encuestaEntities())
            {
                encuesta_empresa Oencuesta_empresa = db.encuesta_empresa.Where(p => p.emp_id.Equals(id)).First();
                Oencuesta_empresaCLS.emp_id = Oencuesta_empresa.emp_id;
                Oencuesta_empresaCLS.emp_descrip = Oencuesta_empresa.emp_descrip;
                Oencuesta_empresaCLS.emp_estatus = Oencuesta_empresa.emp_estatus;
                Oencuesta_empresaCLS.emp_no_trabajadores = Oencuesta_empresa.emp_no_trabajadores;
                Oencuesta_empresaCLS.emp_direccion = Oencuesta_empresa.emp_direccion;
                Oencuesta_empresaCLS.emp_telefono = Oencuesta_empresa.emp_telefono;
                Oencuesta_empresaCLS.emp_person_contac = Oencuesta_empresa.emp_person_contac;
                Oencuesta_empresaCLS.emp_correo = Oencuesta_empresa.emp_correo;
                Oencuesta_empresaCLS.emp_cp = Oencuesta_empresa.emp_cp;
            }
            return View(Oencuesta_empresaCLS);

        }

        [HttpPost]
        public ActionResult EliminarEmpresa(encuesta_empresaCLS Oencuesta_EmpresaCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(Oencuesta_EmpresaCLS);
            }
            int id_empresa = Oencuesta_EmpresaCLS.emp_id;
            using (var db = new csstdura_encuestaEntities())
            {
                encuesta_empresa Oencuesta_empresa = db.encuesta_empresa.Where(p => p.emp_id.Equals(id_empresa)).First();
                Oencuesta_empresa.emp_estatus = "B";
                Oencuesta_empresa.emp_f_cancela = DateTime.Now;

                db.SaveChanges();

            }
            return RedirectToAction("ListarEmpresa");
        }

        public ActionResult EditarUsuarios(int id)
        {
            listarCombos();
            ViewBag.listaEmpresa = listaEmpresa;
            ViewBag.listaSexo = listaSexo;
            ViewBag.listaEdad = listaEdad;
            ViewBag.listaEdoCivil = listaEdoCivil;
            ViewBag.listaOpciones = listaOpciones;
            ViewBag.listaProceso = listaProceso;
            ViewBag.listaPuesto = listaPuesto;
            ViewBag.listaContrata = listaContrata;
            ViewBag.listaPersonal = listaPersonal;
            ViewBag.listaJornada = listaJornada;
            ViewBag.listaRotacion = listaRotacion;
            ViewBag.listaTiempo = listaTiempo;
            ViewBag.listaExpLab = listaExpLab;

            encuesta_usuariosCLS Oencuesta_usuarioCLS = new encuesta_usuariosCLS();
            //List<encuesta_usuariosCLS> oUsuarios = null;
            using (var db = new csstdura_encuestaEntities())
            {

                encuesta_usuarios oUsuarios = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id)).First();



                Oencuesta_usuarioCLS.usua_id = oUsuarios.usua_id;
                Oencuesta_usuarioCLS.usua_nombre = oUsuarios.usua_nombre;
                Oencuesta_usuarioCLS.usua_empresa = (int)oUsuarios.usua_empresa;
                //Oencuesta_usuarioCLS.usua_tipo = oUsuarios.usua_tipo;
                Oencuesta_usuarioCLS.usua_n_usuario = oUsuarios.usua_n_usuario;
                Oencuesta_usuarioCLS.usua_genero = (int)oUsuarios.usua_genero;
                Oencuesta_usuarioCLS.usua_edad = (int)oUsuarios.usua_edad;
                Oencuesta_usuarioCLS.usua_edo_civil = (int)oUsuarios.usua_edo_civil;
                Oencuesta_usuarioCLS.usua_sin_forma = (int)oUsuarios.usua_sin_forma;
                Oencuesta_usuarioCLS.usua_primaria = (int)oUsuarios.usua_primaria;
                Oencuesta_usuarioCLS.usua_secundaria = (int)oUsuarios.usua_secundaria;
                Oencuesta_usuarioCLS.usua_preparatoria = (int)oUsuarios.usua_preparatoria;
                Oencuesta_usuarioCLS.usua_tecnico = (int)oUsuarios.usua_tecnico;
                Oencuesta_usuarioCLS.usua_licenciatura = (int)oUsuarios.usua_licenciatura;
                Oencuesta_usuarioCLS.usua_maestria = (int)oUsuarios.usua_maestria;
                Oencuesta_usuarioCLS.usua_doctorado = (int)oUsuarios.usua_doctorado;
                Oencuesta_usuarioCLS.usua_tipo_puesto = (int)oUsuarios.usua_tipo_puesto;
                Oencuesta_usuarioCLS.usua_tipo_contratacion = (int)oUsuarios.usua_tipo_contratacion;
                Oencuesta_usuarioCLS.usua_tipo_personal = (int)oUsuarios.usua_tipo_personal;
                Oencuesta_usuarioCLS.usua_tipo_jornada = (int)oUsuarios.usua_tipo_jornada;
                Oencuesta_usuarioCLS.usua_rotacion_turno = (int)oUsuarios.usua_rotacion_turno;
                Oencuesta_usuarioCLS.usua_tiempo_puesto = (int)oUsuarios.usua_tiempo_puesto;
                Oencuesta_usuarioCLS.usua_exp_laboral = (int)oUsuarios.usua_exp_laboral;
            }
            return View(Oencuesta_usuarioCLS);

        }

        [HttpPost]
        public ActionResult EditarUsuarios(encuesta_usuariosCLS Oencuesta_usuariosCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(Oencuesta_usuariosCLS);
            }
            int id = Oencuesta_usuariosCLS.usua_id;
            using (var db = new csstdura_encuestaEntities())
            {
                //encuesta_usuarios Oencuesta_usuario = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id_usuario)).FirstOrDefault();
                encuesta_usuarios Oencuesta_usuario = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id)).First();

                Oencuesta_usuario.usua_nombre = Oencuesta_usuariosCLS.usua_nombre;
                Oencuesta_usuario.usua_empresa = Oencuesta_usuariosCLS.usua_empresa;
                Oencuesta_usuario.usua_n_usuario = Oencuesta_usuariosCLS.usua_n_usuario;

                //Cifrando el password
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(Oencuesta_usuariosCLS.usua_p_usuario);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string contraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                Oencuesta_usuario.usua_p_usuario = contraCifrada;

                Oencuesta_usuario.usua_genero = Oencuesta_usuariosCLS.usua_genero;
                Oencuesta_usuario.usua_edad = Oencuesta_usuariosCLS.usua_edad;
                Oencuesta_usuario.usua_edo_civil = Oencuesta_usuariosCLS.usua_edo_civil;
                Oencuesta_usuario.usua_sin_forma = Oencuesta_usuariosCLS.usua_sin_forma;
                Oencuesta_usuario.usua_primaria = Oencuesta_usuariosCLS.usua_primaria;
                Oencuesta_usuario.usua_secundaria = Oencuesta_usuariosCLS.usua_secundaria;
                Oencuesta_usuario.usua_preparatoria = Oencuesta_usuariosCLS.usua_preparatoria;
                Oencuesta_usuario.usua_tecnico = Oencuesta_usuariosCLS.usua_tecnico;
                Oencuesta_usuario.usua_licenciatura = Oencuesta_usuariosCLS.usua_licenciatura;
                Oencuesta_usuario.usua_maestria = Oencuesta_usuariosCLS.usua_maestria;
                Oencuesta_usuario.usua_doctorado = Oencuesta_usuariosCLS.usua_doctorado;
                Oencuesta_usuario.usua_tipo_puesto = Oencuesta_usuariosCLS.usua_tipo_puesto;
                Oencuesta_usuario.usua_tipo_contratacion = Oencuesta_usuariosCLS.usua_tipo_contratacion;
                Oencuesta_usuario.usua_tipo_personal = Oencuesta_usuariosCLS.usua_tipo_personal;
                Oencuesta_usuario.usua_tipo_jornada = Oencuesta_usuariosCLS.usua_tipo_jornada;
                Oencuesta_usuario.usua_rotacion_turno = Oencuesta_usuariosCLS.usua_rotacion_turno;
                Oencuesta_usuario.usua_tiempo_puesto = Oencuesta_usuariosCLS.usua_tiempo_puesto;
                Oencuesta_usuario.usua_exp_laboral = Oencuesta_usuariosCLS.usua_exp_laboral;
                db.SaveChanges();

            }
            return RedirectToAction("Empleados");
        }

        public ActionResult EliminarUsuarios(int id)
        {
            listarCombos();
            ViewBag.listaEmpresa = listaEmpresa;
            ViewBag.listaSexo = listaSexo;
            ViewBag.listaEdad = listaEdad;
            ViewBag.listaEdoCivil = listaEdoCivil;
            ViewBag.listaOpciones = listaOpciones;
            ViewBag.listaProceso = listaProceso;
            ViewBag.listaPuesto = listaPuesto;
            ViewBag.listaContrata = listaContrata;
            ViewBag.listaPersonal = listaPersonal;
            ViewBag.listaJornada = listaJornada;
            ViewBag.listaRotacion = listaRotacion;
            ViewBag.listaTiempo = listaTiempo;
            ViewBag.listaExpLab = listaExpLab;

            encuesta_usuariosCLS Oencuesta_usuarioCLS = new encuesta_usuariosCLS();
         
            using (var db = new csstdura_encuestaEntities())
            {


                encuesta_usuarios oUsuarios = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id)).First();



                Oencuesta_usuarioCLS.usua_id = oUsuarios.usua_id;
                Oencuesta_usuarioCLS.usua_nombre = oUsuarios.usua_nombre;
                Oencuesta_usuarioCLS.usua_empresa = (int)oUsuarios.usua_empresa;
                //Oencuesta_usuarioCLS.usua_tipo = oUsuarios.usua_tipo;
                Oencuesta_usuarioCLS.usua_n_usuario = oUsuarios.usua_n_usuario;
                Oencuesta_usuarioCLS.usua_genero = (int)oUsuarios.usua_genero;
                Oencuesta_usuarioCLS.usua_edad = (int)oUsuarios.usua_edad;
                Oencuesta_usuarioCLS.usua_edo_civil = (int)oUsuarios.usua_edo_civil;
                Oencuesta_usuarioCLS.usua_sin_forma = (int)oUsuarios.usua_sin_forma;
                Oencuesta_usuarioCLS.usua_primaria = (int)oUsuarios.usua_primaria;
                Oencuesta_usuarioCLS.usua_secundaria = (int)oUsuarios.usua_secundaria;
                Oencuesta_usuarioCLS.usua_preparatoria = (int)oUsuarios.usua_preparatoria;
                Oencuesta_usuarioCLS.usua_tecnico = (int)oUsuarios.usua_tecnico;
                Oencuesta_usuarioCLS.usua_licenciatura = (int)oUsuarios.usua_licenciatura;
                Oencuesta_usuarioCLS.usua_maestria = (int)oUsuarios.usua_maestria;
                Oencuesta_usuarioCLS.usua_doctorado = (int)oUsuarios.usua_doctorado;
                Oencuesta_usuarioCLS.usua_tipo_puesto = (int)oUsuarios.usua_tipo_puesto;
                Oencuesta_usuarioCLS.usua_tipo_contratacion = (int)oUsuarios.usua_tipo_contratacion;
                Oencuesta_usuarioCLS.usua_tipo_personal = (int)oUsuarios.usua_tipo_personal;
                Oencuesta_usuarioCLS.usua_tipo_jornada = (int)oUsuarios.usua_tipo_jornada;
                Oencuesta_usuarioCLS.usua_rotacion_turno = (int)oUsuarios.usua_rotacion_turno;
                Oencuesta_usuarioCLS.usua_tiempo_puesto = (int)oUsuarios.usua_tiempo_puesto;
                Oencuesta_usuarioCLS.usua_exp_laboral = (int)oUsuarios.usua_exp_laboral;
            }
            return View(Oencuesta_usuarioCLS);

        }
    }
}