using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoBiblioteca.Models
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string RutaPortada { get; set; }
        public string NombrePortada { get; set; }
        
        public Autor oAutor { get; set; }
        
        public Categoria oCategoria { get; set; }
        
        public Editorial oEditorial { get; set; }
        public string Ubicacion { get; set; }
        public int Ejemplares { get; set; }
        public bool Estado { get; set; }

        public string base64 { get; set; }
        public string extension { get; set; }
    }
}