using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SGA.Dao
{
    public class ConexionRepositorio
    {
        public static SqlConnection ConexionSQL()
        {
            try
            {
                string conexionString = ConfigurationManager.ConnectionStrings["SQLConexion"].ConnectionString;
                SqlConnection conexion = new SqlConnection(conexionString);
                return conexion;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al conectar SqlConnection ", ex);
            }
        }

    }
}
