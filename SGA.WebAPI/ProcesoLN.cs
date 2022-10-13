using SGA.Dao;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SGA.Dao.Dapper;


namespace SGA.WebAPI
{
    public class ProcesoLN
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string numbers = "0123456789";


        #region "Logica de Negocio Proceso"
        private static ProcesoLN ObjProceso = null;
        private ProcesoLN() { }
        public static ProcesoLN getInstancia()
        {
            if (ObjProceso == null)
            {
                ObjProceso = new ProcesoLN();
            }
            return ObjProceso;
        }
        #endregion

        //Auditoria
        public void AuditarProceso(int idproceso, string login_name,string pagina, string descripcionP, string i_estado, HttpContext contexto)
        {
            try
            {
                string terminal_id;

                BitacoraProcRepositorio btn = new BitacoraProcRepositorio();

                //Resolver Error:  System.Net.Sockets.SocketException: No such host is known
                try
                {
                    terminal_id = GetIPAddress(contexto);
                }
                catch (Exception)
                {
                    terminal_id = GetIPAddress(contexto);

                    if (terminal_id == "")
                    {
                        terminal_id = "Desconocido";
                    }
                }

                //Auditoría
                btn.RegistrarEvento(idproceso, login_name, pagina, descripcionP, terminal_id, i_estado);

            }
            catch (Exception)
            {
                throw;
            }

        }



        public string GetIPAddress(HttpContext context)
        {
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public string GetPin()
        {
            string carater = new string(Enumerable.Repeat(chars, 3)
                                .Select(s => s[random.Next(s.Length)]).ToArray());
            string numero = new string(Enumerable.Repeat(numbers, 3)
                                .Select(s => s[random.Next(s.Length)]).ToArray());
            return carater + numero;
        }
    }
}