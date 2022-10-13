using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Dapper;
using SGA.Dao.Repositorio;
using SGA.Dominio;


namespace SGA.Dao.Dapper
{
    public class DispositivoRepositorio
    {
        private IDbConnection bd;

        #region Metodos Dispositivos
            //*** Insertar Usuario
            public Resultado InsertarD(Dispositivo objDispositivo)
            {
                Resultado resultado = new Resultado() { Error = 0, Mensaje = "Dispositivo guardado correctamente." };
                try
                {
                    bd = ConexionRepositorio.ConexionSQL();
                    string cod_proceso = "C";
                    //Parametros
                    var parametros = new DynamicParameters();
                    parametros.Add("@nombre", objDispositivo.Nombre);
                    parametros.Add("@descripcion", objDispositivo.Descripcion);
                    parametros.Add("@telefono", objDispositivo.Telefono);
                    parametros.Add("@i_estado", objDispositivo.I_estado);
                    parametros.Add("@usuario_ejecuta", objDispositivo.User_creacion);
                    parametros.Add("@terminal", objDispositivo.Terminal_creacion);
                    parametros.Add("@cod_proceso", cod_proceso);

                    using (bd)
                    {
                        //Retorna la lista de candidatos
                        var resp = bd.Execute("sp_sga_crud_dispositivos", parametros, commandType: CommandType.StoredProcedure);

                        if (resp <= 0)
                        {
                            resultado.Error = -1;
                            resultado.Mensaje = "Error al insertar dispositivo";
                        }
                    }

                    return resultado;
                }
                catch (Exception ex)
                {
                    resultado.Error = -1;
                    resultado.Mensaje = "Error al insertar usuario: " + ex.Message;
                    return resultado;
                    throw new Exception("sp_sga_crud_dispositivos", ex);
                }
            }

        public Resultado ActualizarD(Dispositivo objDispositivo)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Dispositivo actualizado correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "U";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id", objDispositivo.Id_dispositivo);
                parametros.Add("@nombre", objDispositivo.Nombre);
                parametros.Add("@descripcion", objDispositivo.Descripcion);
                parametros.Add("@telefono", objDispositivo.Telefono);
                parametros.Add("@i_estado", objDispositivo.I_estado);
                parametros.Add("@usuario_ejecuta", objDispositivo.User_creacion);
                parametros.Add("@terminal", objDispositivo.Terminal_creacion);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_dispositivos", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar dispositivo";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar usuario: " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_dispositivos", ex);
            }
        }

        public Resultado EliminarD(int id)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Dispositivo actualizado correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id", id);
                parametros.Add("@nombre", "ELIMINAR");
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_dispositivos", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar dispositivo";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar usuario: " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_dispositivos", ex);
            }
        }


        //*** Listar Dispositivos
        public List<Dispositivo> ListaDispositivos()
        {
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "R";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de dispositivos
                    return bd.Query<Dispositivo>("sp_sga_crud_dispositivos", parametros, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("sp_sga_crud_dispositivos", ex);
            }
        }

        //*** Listar Dispositivos Activos
        public List<Dispositivo> ListaDispositivosActivos()
        {
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "S";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de dispositivos Activos
                    return bd.Query<Dispositivo>("sp_sga_crud_dispositivos", parametros, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("sp_sga_crud_dispositivos", ex);
            }
        }


        public string ObtenerTelefono(int id) 
        {
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "L";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id", id);
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@resultado", dbType: DbType.String, direction: ParameterDirection.Output,size:50);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_dispositivos", parametros, commandType: CommandType.StoredProcedure);
                    string res = parametros.Get<string>("@resultado");
                    return res;
                }
            }
            catch (Exception ex)
            {
                return "";
                throw new Exception("sp_sga_crud_dispositivos", ex);
            }
        }

        #endregion
    }
}
