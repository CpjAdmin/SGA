using Newtonsoft.Json.Linq;
using SGA.Dao.Dapper;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;


namespace SGA.WebAPI.Controllers
{

    public class ReportesController : ApiController
    {
        
        [ActionName("ListaReportes")]
        [HttpPost]
        public HttpResponseMessage ListaReportes([FromBody] JObject data)
        {
            try
            {
                Reporte objReporte = data["parametros"].ToObject<Reporte>();
                ReportesRepositorio resultado = new ReportesRepositorio();

                //***Auditoría
                ProcesoLN.getInstancia().AuditarProceso(35, objReporte.Usuario, "Reportes.aspx", "Ingreso al módulo de reportes.", "C", System.Web.HttpContext.Current);

                string Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                objReporte.Terminal = Terminal;

                var respuesta = resultado.ListadoReportes(objReporte.Usuario);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("GenerarReportePDF")]
        [HttpPost]
        public HttpResponseMessage GenerarReportePDF([FromBody] Reporte pDatosReporte)
        {
            try
            {
                string URLReporte = "http://alfa/ReportServer/Pages/ReportViewer.aspx?"; //Mandar esto como parametro tambien
                URLReporte += pDatosReporte.Ubicacion_ssrs.Trim();
                URLReporte += "&rs:Command=Render&rs:Format=pdf";
                string concatenaParametros = GenerarURLReporte(pDatosReporte.Parametros);
                URLReporte += concatenaParametros.Trim();

                HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URLReporte.Trim());
                Req.Credentials = ICredenciales();
                Req.UseDefaultCredentials = true;
                Req.PreAuthenticate = true;
                Req.Method = "GET";
                HttpWebResponse HttpWResp = (HttpWebResponse)Req.GetResponse();
                Stream fStream = HttpWResp.GetResponseStream();
                byte[] pdfBytes = ReadFully(fStream);
                HttpWResp.Close();

                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(System.Convert.ToBase64String(pdfBytes))
                };

                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(System.Net.Mime.DispositionTypeNames.Inline)
                {
                    FileName = "file.pdf"
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                return result;
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
           
        }

        [NonAction]
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        [NonAction]
        public static ICredentials ICredenciales()
        {
            // Usuario
            string usuario = ConfigurationManager.AppSettings["ReportViewerUsuario"];

            if (string.IsNullOrEmpty(usuario))
                throw new Exception(
                    "Falta el usuario del archivo web.config");
            // Clave
            string clave = ConfigurationManager.AppSettings["ReportViewerClave"];

            if (string.IsNullOrEmpty(clave))
                throw new Exception(
                    "Falta la contraseña del archivo web.config");
            // Dominio
            string dominio = ConfigurationManager.AppSettings["ReportViewerDominio"];

            if (string.IsNullOrEmpty(dominio))
                throw new Exception(
                    "Falta el dominio del archivo web.config");

            return new NetworkCredential(usuario, clave, dominio);

        }

        [NonAction]
        private string GenerarURLReporte(string pParametrosReporte = "")
        {
            if (pParametrosReporte.Length <= 0)
            {
                return "";
            }

            string[] auxParametros = pParametrosReporte.Split(',');
            string retornarURL = string.Empty;
            foreach (string item in auxParametros)
            {
                retornarURL += "&" + item;
            }
            return retornarURL;
        }


    }
}
