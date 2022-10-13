using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using SGA.Dao.Dapper;
using SGA.Dominio;
using System.Threading.Tasks;

namespace SGA.WebAPI.Controllers
{
    public class DelegadoController : ApiController
    {
        UsuarioRepositorio urn;
        BitacoraProcRepositorio bt = new BitacoraProcRepositorio();
        private static Random random = new Random();

        [ActionName("InsertarDelegado")]
        [HttpPost]
        public HttpResponseMessage InsertarDelegado([FromBody] Delegado objDelegado)
        {
            urn = new UsuarioRepositorio();
            DelegadoRepositorio dr = new DelegadoRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                objDelegado.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = dr.Insertar(objDelegado);

                ProcesoLN.getInstancia().AuditarProceso(34, objDelegado.Usuario, "Delegados.aspx", "Insertar Delegado " + objDelegado.Cedula + ": " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(34, objDelegado.Usuario, "Delegados.aspx", "Insertar Delegado " + objDelegado.Cedula + ": " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("ActualizarDelegado")]
        [HttpPost]
        public HttpResponseMessage ActualizarDelegado([FromBody] Delegado objDelegado)
        {
            urn = new UsuarioRepositorio();
            DelegadoRepositorio dr = new DelegadoRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                objDelegado.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = dr.Actualizar(objDelegado);

                ProcesoLN.getInstancia().AuditarProceso(10, objDelegado.Usuario, "Delegados.aspx", "Modificar Delegado " + objDelegado.Cedula + ": " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(10, objDelegado.Usuario, "Delegados.aspx", "Modificar Delegado " + objDelegado.Cedula + ": " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("EliminarDelegado")]
        [HttpPost]
        public HttpResponseMessage EliminarDelegado([FromBody] Delegado objDelegado)
        {
            urn = new UsuarioRepositorio();
            DelegadoRepositorio dr = new DelegadoRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                objDelegado.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = dr.Eliminar(objDelegado.Id_delegado);

                ProcesoLN.getInstancia().AuditarProceso(35, objDelegado.Usuario, "Delegados.aspx", "Eliminar Delegado " + objDelegado.Cedula + ": " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(34, objDelegado.Usuario, "Delegados.aspx", "Eliminar Delegado " + objDelegado.Cedula + ": " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("BuscarDelegado")]
        [HttpPost]
        public HttpResponseMessage BuscarDelegado([FromBody] Delegado objDelegado)
        {

            DelegadoRepositorio dr = new DelegadoRepositorio();

            try
            {
                var respuesta = dr.BuscarId(objDelegado);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("ListaDelegados")]
        [HttpPost]
        public HttpResponseMessage ListaDelegados()
        {

            DelegadoRepositorio dr = new DelegadoRepositorio();

            try
            {
                var respuesta = dr.ListaDelegados();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("TrasladarUsuarios")]
        [HttpPost]
        public HttpResponseMessage TrasladarUsuarios([FromBody] Delegado objDelegado)
        {
            urn = new UsuarioRepositorio();
            DelegadoRepositorio dr = new DelegadoRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                objDelegado.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = dr.Trasladar(objDelegado);

                ProcesoLN.getInstancia().AuditarProceso(11, objDelegado.Usuario, "Delegados.aspx", "Trasladar delegados " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(11, objDelegado.Usuario, "Delegados.aspx", "Trasladar delegados " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        //*** VALIDADO - TOTAL DE DELEGADOS - Obtener el total de delegados
        [ActionName("TotalDelegados")]
        [HttpPost]
        public HttpResponseMessage TotalDelegados()
        {
            DelegadoRepositorio dr = new DelegadoRepositorio();
            try
            {
                var respuesta = dr.TotalDelegados();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("CargarArchivo")]
        [HttpPost]
        public async Task<HttpResponseMessage> CargarArchivo()
        {
            urn = new UsuarioRepositorio();
            DelegadoRepositorio drn = new DelegadoRepositorio();
            string cadena = null;
            string localFileName;
            string serverFileName = "";
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            string mensaje = "";
            Delegado delegado;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    localFileName = file.LocalFileName;
                    serverFileName = Path.Combine(root, DateTime.Now.ToString("ddMMyyyy") + random.Next(1000).ToString() + ".txt");
                    File.Move(localFileName, serverFileName);
                }

                if (serverFileName == "")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se cargó la ruta del archivo, favor intente nuevamente.");
                }

                string[] lines = System.IO.File.ReadAllLines(@serverFileName);
                int cont = 1;
                int inserts = 0;

                foreach (string line in lines)
                {
                    var splLine = line.Split('|');
                    if (splLine.Length != 9)
                    {
                        mensaje = mensaje + "- La línea " + cont.ToString() + " no cumple con las columnas requeridas <br>";
                    }
                    else
                    {
                        delegado = new Delegado
                        {
                            Cedula = splLine[0],
                            Nombre = splLine[1],
                            Num_paleta = (splLine[2] == null || splLine[2].Trim() == "") ? 0 : Int32.Parse(splLine[2]),
                            Centro = splLine[3],
                            Institucion = splLine[4],
                            Lugar_Trabajo = splLine[5],
                            Tel_Celular = splLine[6],
                            Email = splLine[7],
                            I_estado = "I",
                            I_votar = splLine[8].ToUpper(),
                            Foto = "",
                            Usuario = cadena.Split(':')[0],
                            Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current)
                        };

                        var respuesta = drn.Insertar(delegado);
                        if (respuesta.Error != 0)
                        {
                            mensaje = mensaje + "- Cédula " + delegado.Cedula + " error: " + respuesta.Mensaje + "<br>";
                            ProcesoLN.getInstancia().AuditarProceso(33, delegado.Usuario, "Delegados.aspx", mensaje, "E", System.Web.HttpContext.Current);
                        }
                        else
                        {
                            ProcesoLN.getInstancia().AuditarProceso(33, delegado.Usuario, "Delegados.aspx", "Proceso masivo Insertar delegados " + delegado.Cedula + " ingresado correctamente.", "C", System.Web.HttpContext.Current);
                            inserts++;
                        }
                    }

                    cont++;
                }

                File.Delete(serverFileName);


                mensaje = mensaje + "<b>Se ingresaron " + inserts.ToString() + " registros.</b><br>";

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
    }
}
