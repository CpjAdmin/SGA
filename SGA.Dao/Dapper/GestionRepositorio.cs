using Dapper;
using SGA.Dao.Repositorio;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Dapper
{
    public class GestionRepositorio: IGestionRepositorio
    {
        private IDbConnection bd;

        #region Metodos

        //*** @cod_proceso = 'C' - VALIDADO - Insertar Registro - Delegados
        public Resultado Ingresar(Gestion objGestion)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Delegado agregado con éxito, Paleta: " + objGestion.Num_paleta.ToString() };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "C";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id", objGestion.Num_paleta);
                parametros.Add("@usuario", objGestion.Usuario);
                parametros.Add("@terminal", objGestion.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_uso_palabra", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar delegado, Paleta: " + objGestion.Num_paleta.ToString();
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar delegado, Paleta: " + objGestion.Num_paleta.ToString() + " - " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_uso_palabra", ex);
            }
        }

        //*** @cod_proceso = 'A' - VALIDADO - Insertar Registro - Candidato
        public Resultado IngresarC(Gestion objGestion)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Candidato agregado con exito. Posición: " + objGestion.Num_posicion.ToString() + " | Cédula: " + objGestion.Cedula};
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "A";

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id", objGestion.Num_posicion);
                parametros.Add("@cedula", objGestion.Cedula);
                parametros.Add("@i_estado", "F");
                parametros.Add("@tiempo", objGestion.Tiempo);
                parametros.Add("@usuario", objGestion.Usuario);
                parametros.Add("@terminal", objGestion.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_uso_palabra", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al agregar candidato, Posición: " + objGestion.Num_posicion.ToString() + " | Cédula: " + objGestion.Cedula;
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al agregar candidato, Posición: " + objGestion.Num_posicion.ToString() + " | Cédula: " + objGestion.Cedula + " - " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_uso_palabra", ex);
            }
        }

        //*** @cod_proceso = 'U' - VALIDADO - Actualizar Registro - Delegados
        public Resultado Actualizar(Gestion objGestion)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Delegado actualizado con éxito, Paleta: " + objGestion.Num_paleta.ToString() };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "U";

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cedula", objGestion.Cedula);
                parametros.Add("@i_estado", "F");
                parametros.Add("@tiempo", objGestion.Tiempo);
                parametros.Add("@usuario", objGestion.Usuario);
                parametros.Add("@terminal", objGestion.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_uso_palabra", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al actualizar delegado, paleta: " + objGestion.Num_paleta.ToString();
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al actualizar delegado, paleta: " + objGestion.Num_paleta.ToString() + " - " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_uso_palabra", ex);
            }
        }

        //*** @cod_proceso = 'D' - VALIDADO - Eliminar Registro - General
        public Resultado Eliminar(Gestion objGestion)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Delegado eliminado con éxito, Paleta: " + objGestion.Num_paleta.ToString() };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id", objGestion.Num_paleta);
                parametros.Add("@cedula", objGestion.Cedula);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_uso_palabra", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al eliminar delegado, Paleta: " + objGestion.Num_paleta.ToString();
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al eliminar delegado, Paleta: " + objGestion.Num_paleta.ToString() + " - " + ex.Message;
                return resultado;
            }
        }

        //*** @cod_proceso = 'I' - ***** DESCONOCIDO - VALIDAR DONDE SE USA *****
        public Gestion BuscarId(Gestion objGestion)
        {
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "I";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@num_paleta", objGestion.Num_paleta);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos

                    return bd.QuerySingle<Gestion>("sp_sga_crud_uso_palabra", parametros, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_uso_palabra", ex);
            }
        }

        //*** VALIDADO - Lista de Uso de la Palabra - Delegados
        public List<Gestion> ListaGestion()
        {
            try
            {
                // Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", "R");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    // Retorna la lista 
                    return bd.Query<Gestion>("sp_sga_crud_uso_palabra", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_uso_palabra", ex);
            }

        }
        //*** VALIDADO - Lista de Uso de la Palabra - Candidatos
        public List<Gestion> ListaGestionC(int id_papeleta)
        {
            try
            {
                // Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", "T");
                parametros.Add("@papeleta", id_papeleta);

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    // Retorna la lista 
                    return bd.Query<Gestion>("sp_sga_crud_uso_palabra", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_uso_palabra", ex);
            }

        }

        #endregion
    }
}
