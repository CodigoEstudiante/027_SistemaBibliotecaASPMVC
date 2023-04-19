using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoBiblioteca.Models
{
    public class Editorial
    {
        public int IdEditorial { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}