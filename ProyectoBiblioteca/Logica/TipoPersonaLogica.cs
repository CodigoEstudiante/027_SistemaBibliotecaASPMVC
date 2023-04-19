using ProyectoBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoBiblioteca.Logica
{
    public class TipoPersonaLogica
    {
        private static TipoPersonaLogica instancia = null;

        public TipoPersonaLogica()
        {

        }

        public static TipoPersonaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new TipoPersonaLogica();
                }

                return instancia;
            }
        }

        public List<TipoPersona> Listar()
        {
            List<TipoPersona> Lista = new List<TipoPersona>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdTipoPersona,Descripcion from TIPO_PERSONA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new TipoPersona()
                            {
                                IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<TipoPersona>();
                }
            }
            return Lista;
        }
    }
}