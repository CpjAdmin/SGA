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
    public class QuorumRepositorio: IQuorumRepositorio
    {
        private IDbConnection bd;

        //*** VALIDADO - Verificar Cédula
        public int IngresarSE(string cedula, string usuario, string terminal) {

            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "C";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cedula", cedula);
                parametros.Add("@usuario", usuario);
                parametros.Add("@terminal", terminal);
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_quorum", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");
                    return res;
                }

            }
            catch (Exception ex)
            {
                return -1;
                throw new Exception("sp_sga_crud_quorum", ex);
            }
        }
        //*** VALIDADO - Obtener la Lista de Quorum ( Ingreso y Salida )
        public List<Quorum> ListaQuorum(string cod_proceso) {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<Quorum>("sp_sga_crud_quorum", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_quorum", ex);
            }
        }
        //*** VALIDADO - Eliminar Registro Quorum 
        public Resultado EliminarD(string cedula)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Delegado eliminado correctamente." };
            try
            {
                //Parametros
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cedula", cedula);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_quorum", parametros, commandType: CommandType.StoredProcedure);
                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al eliminar delegado";
                    };
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al eliminar delegado: " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_quorum", ex);
            }
        }
        //*** VALIDADO - Abrir Quorum 
        public Resultado AbrirQuorum(string usuario, string terminal)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Quorum abierto con éxito." };
            try
            {
                //Parametros
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "Q";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@usuario", usuario);
                parametros.Add("@terminal", terminal);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_quorum", parametros, commandType: CommandType.StoredProcedure);
                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al abrir el Quorum.";
                    };
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al abrir el Quorum: " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_quorum", ex);
            }
        }
        //*** VALIDADO - Cerrar Quorum 
        public Resultado CerrarQuroum(string usuario, string terminal)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Quorum cerrado con éxito." };
            try
            {
                //Parametros
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "X";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@usuario", usuario);
                parametros.Add("@terminal", terminal);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_quorum", parametros, commandType: CommandType.StoredProcedure);
                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al cerrar el quorum.";
                    };
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al cerrar el quorum: " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_quorum", ex);
            }
        }
        //*** VALIDADO - Validar Quorum 
        public int ValidaQuorum()
        {
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "S";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_quorum", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");
                    return res;
                }

            }
            catch (Exception ex)
            {
                return -1;
                throw new Exception("sp_sga_crud_quorum", ex);
            }
        }

        public int UltimoQuorumC()
        {
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "U";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_quorum", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");
                    return res;
                }

            }
            catch (Exception ex)
            {
                return -1;
                throw new Exception("sp_sga_crud_quorum", ex);
            }
        }
        public Quorum ValidarCedulaQuorum(string cedula, string usuario, string terminal)
        {
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "V";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cedula", cedula);
                parametros.Add("@usuario", usuario);
                parametros.Add("@terminal", terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //***Retorna los datos de la persona a la que pertenece la cédula leída, si no la encuentra, devuelve NULL
                    var registro = new Quorum();
                    registro = bd.QueryFirstOrDefault<Quorum>("sp_sga_crud_quorum", parametros, commandType: CommandType.StoredProcedure);
                    //***Return

                    return registro;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_quorum", ex);
            }
        }



    }
}




