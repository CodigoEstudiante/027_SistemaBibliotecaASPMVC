using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoBiblioteca.Logica
{
    public class LibroLogica
    {

        private static LibroLogica instancia = null;

        public LibroLogica()
        {

        }

        public static LibroLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new LibroLogica();
                }

                return instancia;
            }
        }

        public List<Libro> Listar()
        {

            List<Libro> rptListaLibro = new List<Libro>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("select l.IdLibro,l.Titulo,l.RutaPortada,l.NombrePortada,");
                sb.AppendLine("a.IdAutor,a.Descripcion[DescripcionAutor],");
                sb.AppendLine("c.IdCategoria,c.Descripcion[DescripcionCategoria],");
                sb.AppendLine("e.IdEditorial,e.Descripcion[DescripcionEditorial],");
                sb.AppendLine("l.Ubicacion,l.Ejemplares,l.Estado");
                sb.AppendLine("from LIBRO l");
                sb.AppendLine("inner join AUTOR a on a.IdAutor = l.IdAutor");
                sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = l.IdCategoria");
                sb.AppendLine("inner join EDITORIAL e on e.IdEditorial = l.IdEditorial");

                SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaLibro.Add(new Libro()
                        {
                            IdLibro = Convert.ToInt32(dr["IdLibro"].ToString()),
                            Titulo = dr["Titulo"].ToString(),
                            RutaPortada = dr["RutaPortada"].ToString(),
                            NombrePortada = dr["NombrePortada"].ToString(),
                            oAutor = new Autor() { IdAutor = Convert.ToInt32(dr["IdAutor"].ToString()), Descripcion = dr["DescripcionAutor"].ToString() },
                            oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"].ToString()), Descripcion = dr["DescripcionCategoria"].ToString() },
                            oEditorial = new Editorial() { IdEditorial = Convert.ToInt32(dr["IdEditorial"].ToString()), Descripcion = dr["DescripcionEditorial"].ToString() },
                            Ubicacion = dr["Ubicacion"].ToString(),
                            Ejemplares = Convert.ToInt32(dr["Ejemplares"].ToString()),
                            base64 = Utilidades.convertirBase64(Path.Combine(dr["RutaPortada"].ToString(), dr["NombrePortada"].ToString())),
                            extension = Path.GetExtension(dr["NombrePortada"].ToString()).Replace(".",""),
                            Estado = Convert.ToBoolean(dr["Estado"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaLibro;

                }
                catch (Exception ex)
                {
                    rptListaLibro = null;
                    return rptListaLibro;
                }
            }
        }


        public int Registrar(Libro objeto)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarLibro", oConexion);
                    cmd.Parameters.AddWithValue("Titulo", objeto.Titulo);
                    cmd.Parameters.AddWithValue("RutaPortada", objeto.RutaPortada);
                    cmd.Parameters.AddWithValue("NombrePortada", objeto.NombrePortada);
                    cmd.Parameters.AddWithValue("IdAutor", objeto.oAutor.IdAutor);
                    cmd.Parameters.AddWithValue("IdCategoria", objeto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdEditorial", objeto.oEditorial.IdEditorial);
                    cmd.Parameters.AddWithValue("Ubicacion", objeto.Ubicacion);
                    cmd.Parameters.AddWithValue("Ejemplares", objeto.Ejemplares);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }


        public bool Modificar(Libro objeto)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_modificarLibro", oConexion);
                    cmd.Parameters.AddWithValue("IdLibro", objeto.IdLibro);
                    cmd.Parameters.AddWithValue("Titulo", objeto.Titulo);
                    cmd.Parameters.AddWithValue("IdAutor", objeto.oAutor.IdAutor);
                    cmd.Parameters.AddWithValue("IdCategoria", objeto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdEditorial", objeto.oEditorial.IdEditorial);
                    cmd.Parameters.AddWithValue("Ubicacion", objeto.Ubicacion);
                    cmd.Parameters.AddWithValue("Ejemplares", objeto.Ejemplares);
                    cmd.Parameters.AddWithValue("Estado", objeto.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizarRutaImagen(Libro objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizarRutaImagen", oConexion);
                    cmd.Parameters.AddWithValue("IdLibro", objeto.IdLibro);
                    cmd.Parameters.AddWithValue("NombrePortada", objeto.NombrePortada);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from LIBRO where IdLibro = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }


    }
}