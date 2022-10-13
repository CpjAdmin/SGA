using Dapper;
using SGA.Dao.Repositorio;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;

namespace SGA.Dao.Dapper
{
    public class BitacoraProcRepositorio:IBitacoraProcRepositorio
    {
        public List<Proceso> ListadoProcesos() {

            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", 2);


                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista de candidatos
                    return bd.Query<Proceso>("sp_sga_auditoria_procesos", parametros, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_auditoria_procesos", ex);
            }
        }
        public void RegistrarEvento(int idproceso, string login_name, string pagina, string descripcion, string terminal,string i_estado)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_proceso", idproceso);
                parametros.Add("@login", login_name);
                parametros.Add("@pagina", pagina);
                parametros.Add("@i_estado", i_estado);
                parametros.Add("@descripcion", descripcion);
                parametros.Add("@terminal_id", terminal);
                parametros.Add("@cod_proceso",1);
                

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista de candidatos
                    var resultado = bd.Execute("sp_sga_auditoria_procesos", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                // Cuando ocurre erro al guardar bitácora de Auditoría, se guarda en el log
                WriteToFile(ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message + " - " + idproceso.ToString() + " - " + login_name + " - " + pagina + " - " + descripcion + " - " + terminal);
                // Manejo de Exception
                throw new Exception("sp_sga_auditoria_procesos", ex);
            }
        }
        public void WriteToFile(string Message)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    //*** Crear el archivo
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
                else
                {
                    //*** Escribir en el archivo
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de Exception
                throw new Exception("Logs - Proceso WriteToFile", ex);
            }
        }
    }
}
