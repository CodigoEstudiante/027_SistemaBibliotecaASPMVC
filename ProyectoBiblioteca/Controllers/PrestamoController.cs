using ProyectoBiblioteca.Logica;
using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoBiblioteca.Controllers
{
    public class PrestamoController : Controller
    {
        // GET: Prestamo
        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Consultar()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GuardarPrestamos(Prestamo objeto)
        {
            bool _respuesta = false;
            string _mensaje = string.Empty;

            _respuesta = PrestamoLogica.Instancia.Existe(objeto);

            if (_respuesta)
            {
                _respuesta = false;
                _mensaje = "El lector ya tiene un prestamo pendiente con el mismo libro";
            }
            else {
                _respuesta = PrestamoLogica.Instancia.Registrar(objeto);
                _mensaje = _respuesta ? "Registro completo" : "No se pudo registrar";
            }

            
            return Json(new { resultado = _respuesta, mensaje = _mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEstados()
        {
            List<EstadoPrestamo> oLista = new List<EstadoPrestamo>();
            oLista = PrestamoLogica.Instancia.ListarEstados();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Listar(int idestadoprestamo, int idpersona)
        {
            List<Prestamo> oLista = new List<Prestamo>();
            oLista = PrestamoLogica.Instancia.Listar(idestadoprestamo, idpersona);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Devolver(string estadorecibido,int idprestamo)
        {
            bool respuesta = false;
            respuesta = PrestamoLogica.Instancia.Devolver(estadorecibido,idprestamo);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}