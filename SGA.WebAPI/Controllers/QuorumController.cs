using Newtonsoft.Json.Linq;
using SGA.Dao.Dapper;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;

namespace SGA.WebAPI.Controllers
{
    public class QuorumController : ApiController
    {
        UsuarioRepositorio urn;
        List<int> ListRol = new List<int> { 1, 2, 7 };

        //*** VALIDADO - VERIFICAR CÉDULA - Para registar desde el Sistema
        [ActionName("RegistrarIS")]
        [HttpPost]
        public HttpResponseMessage RegistrarIS([FromBody] JObject data)
        {
            urn = new UsuarioRepositorio();
            dynamic objQuorum = data.ToObject<dynamic>();

            string cedula = objQuorum.cedula.ToString();
            string usuario = objQuorum.usuario.ToString();

            QuorumRepositorio qr = new QuorumRepositorio();
            string cadena = null;

            try
            {
                //*** Validar TOKEN
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                //*** Validar ROL
                if (!ListRol.Contains(urn.ObtenerRol(cadena)))
                {
                    ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "Sistema - El rol del usuario no permite registrar ingresos y salidas del Quorum.", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "El rol del usuario no permite registrar ingresos y salidas del Quorum.");
                }
               
                //*** Validar Terminal   
                string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                //*** Registrar Respuesta   
                int respuesta = qr.IngresarSE(cedula, usuario, terminal);
                if (respuesta == 3)
                {
                    //*** Registrar Auditoría   
                    ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "Sistema - Registro en Quorum DESCONOCIDO ( " + cedula + " ).", "E", System.Web.HttpContext.Current);
                }
                else
                {
                    //*** Registrar Auditoría   
                    ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "Sistema - Registro en Quorum ( " + cedula + " ).", "C", System.Web.HttpContext.Current);
                }

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "Sistema - Error - Registro en Quorum - " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
        //*** VALIDADO - APP VERIFIQUESE CEDULA - Para registar desde la APP Verifiquese Cédula
        [ActionName("RegistrarISAPP")]
        [HttpPost]
        public HttpResponseMessage RegistrarISAPP([FromBody] JObject data)
        {
            urn = new UsuarioRepositorio();
            dynamic objQuorum = data.ToObject<dynamic>();

            string cedula = objQuorum.cedula.ToString();
            string usuario = objQuorum.usuario.ToString();

            QuorumRepositorio qr = new QuorumRepositorio();
            try
            {
                //*** Validar ROL
                if (!ListRol.Contains(urn.ObtenerRol(usuario)))
                {
                    ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "WebAPI - El rol del usuario no permite registrar ingresos y salidas del Quorum.", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "WebAPI - El rol del usuario no permite registrar ingresos y salidas del Quorum");
                }

                //*** Validar Terminal   
                string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                //*** Registrar Respuesta
                int respuesta = qr.IngresarSE(cedula, usuario, terminal);
                if (respuesta == 3)
                {
                    //*** Registrar Auditoría   
                    ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "WebAPI - Registro en Quorum DESCONOCIDO ( " + cedula + " ).", "E", System.Web.HttpContext.Current);
                    
                }
                else
                {
                    //*** Registrar Auditoría   
                    ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "WebAPI - Registro en Quorum ( " + cedula + " ).", "C", System.Web.HttpContext.Current);

                }

                string resultString = "OK";
                var resultCode = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(resultString, System.Text.Encoding.UTF8, "text/plain")
                };

                return resultCode;
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "WebAPI - Error - Registro en Quorum - " + ex.Message, "E", System.Web.HttpContext.Current);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
        //*** VALIDADO - APP VERIFIQUESE CEDULA - Para registar desde la APP Verifiquese Cédula
        [ActionName("VerificarCedulaISAPP")]
        [HttpPost]
        public HttpResponseMessage VerificarCedulaISAPP([FromBody] JObject data)
        {
            urn = new UsuarioRepositorio();
            dynamic objQuorum = data.ToObject<dynamic>();

            string cedula = objQuorum.cedula.ToString();
            string usuario = objQuorum.usuario.ToString();

            QuorumRepositorio qr = new QuorumRepositorio();

            HttpResponseMessage Respuesta = new HttpResponseMessage();

            try
            {

                string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = qr.ValidarCedulaQuorum(cedula, usuario, terminal);
                string imagen;
                string msg1 = "";
                string msg2 = "";
                string msg3 = "";
                string msg4 = "";

                if (respuesta != null)
                {
                    imagen = "iconoConfirmar.png";

                    msg1 = "PALETA: " + respuesta.Id_delegado.ToString();
                    msg2 = respuesta.Nombre;
                    msg3 = "CÉDULA: " + respuesta.Cedula;
                    msg4 = respuesta.Tipo;
                }
                else
                {
                    imagen = "iconoDenegar.png";

                    msg4 = "LA CÉDULA NO EXISTE EN EL SISTEMA - " + cedula;
                }

                string dominio = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                string origenImagen = "/Images/VerifiqueseApp/" + imagen;
                string src = dominio + origenImagen;

                string html = "<!DOCTYPE html>"+
                              "<html>" +
                              "<head>" +
                              "<meta charset='utf-8'/>" +
                              "<title>VOTACIONES SGA</title>" +
                              "</head>" +
                              "<body> <div class='error-page' style='text-align: center'> <header class='error-page_header'>" +
                              "<img class='error-page_header-image' src='"+ src + "' alt='"+ imagen + "'>" +
                              "<h1 style='font-size: 70px; color:red;' class='error-page_title nolinks'>" + msg1 + "</h1>" +
                              "<h1 class='error-page_title nolinks'>" + msg2 + "</h1>" +
                              "<h1 class='error-page_title nolinks'>" + msg3 + "</h2>" +
                              "</header> " +
                              "<p class='error-page_message' style='font-size: 100px;'>" + msg4 + "</p>" +
                              "</div>" +
                              "</body>" +
                              "</html>";

                Respuesta.Content = new StringContent(html, System.Text.Encoding.UTF8);
                Respuesta.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                Respuesta.StatusCode = HttpStatusCode.OK;

                return Respuesta;
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(23, usuario, "Quorum.aspx", "Verificación de Cédula desde APP ( FORMULARIO )" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
        //*** VALIDADO - Obtener la Lista de Quorum ( Ingreso y Salida )
        [ActionName("ListaQuorum")]
        [HttpPost]
        public HttpResponseMessage ListaQuorum([FromBody] string proceso)
        {

            QuorumRepositorio qr = new QuorumRepositorio();

            try
            {
                var respuesta = qr.ListaQuorum(proceso);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
        //*** VALIDADO - Eliminar Registro Quorum 
        [ActionName("EliminarD")]
        [HttpPost]
        public HttpResponseMessage EliminarD([FromBody] string cedula)
        {
            QuorumRepositorio qr = new QuorumRepositorio();
            urn = new UsuarioRepositorio();
            string cadena = null;
            string usuario = "";
            try
            {
                //*** Validar TOKEN
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                //*** Validar ROL
                if (!ListRol.Contains(urn.ObtenerRol(cadena)))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "El rol del usuario no permite eliminar delegados del Quorum.");
                }

                var respuesta = qr.EliminarD(cedula);

                //*** Registrar Auditoría   
                usuario = cadena.Split(':')[0];
                ProcesoLN.getInstancia().AuditarProceso(24, usuario, "Quorum.aspx", "Eliminar Registro de Quorum ( " + cedula + " ).", "C", System.Web.HttpContext.Current);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(24, usuario, "Quorum.aspx", "Error - Eliminar Registro de Quorum - " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }
        //*** VALIDADO - Abrir Quorum 
        [ActionName("AbrirQ")]
        [HttpPost]
        public HttpResponseMessage AbrirQ([FromBody] string usuario)
        {
            urn = new UsuarioRepositorio();
            QuorumRepositorio qr = new QuorumRepositorio();
            string cadena = null;
            try
            {
                //*** Validar TOKEN
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                //*** Validar ROL
                if (!ListRol.Contains(urn.ObtenerRol(cadena)))
                {
                    ProcesoLN.getInstancia().AuditarProceso(25, usuario, "Quorum.aspx", "Abrir Quorum ( Rol no permitido )", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "El rol del usuario no permite abrir el Quorum.");
                }
                //*** Validar Terminal   
                string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = qr.AbrirQuorum(usuario, terminal);
                if (respuesta.Error == 0)
                {
                    ProcesoLN.getInstancia().AuditarProceso(25, usuario, "Quorum.aspx", "Abrir Quorum.", "C", System.Web.HttpContext.Current);
                }

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(25, usuario, "Quorum.aspx", "Abrir Quorum " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }
        //*** VALIDADO - Cerrar Quorum 
        [ActionName("CerrarQ")]
        [HttpPost]
        public HttpResponseMessage CerrarQ([FromBody] string usuario)
        {
            urn = new UsuarioRepositorio();
            QuorumRepositorio qr = new QuorumRepositorio();
            string cadena = null;
            try
            {
                //*** Validar TOKEN
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                //*** Validar ROL
                if (!ListRol.Contains(urn.ObtenerRol(cadena)))
                {
                    ProcesoLN.getInstancia().AuditarProceso(27, usuario, "Quorum.aspx", "Intento de Cerrar Quorum.", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "El rol del usuario no permite cerrar el Quorum.");
                }
                //*** Validar Terminal   
                string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = qr.CerrarQuroum(usuario, terminal);
                if (respuesta.Error == 0)
                {
                    ProcesoLN.getInstancia().AuditarProceso(27, usuario, "Quorum.aspx", "Cerrar Quorum.", "C", System.Web.HttpContext.Current);
                }

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(27, usuario, "Quorum.aspx", "Cerrar Quorum " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }
        //*** VALIDADO - Validar Quorum 
        [ActionName("ValidarQ")]
        [HttpPost]
        public HttpResponseMessage ValidarQ()
        {
            urn = new UsuarioRepositorio();
            QuorumRepositorio qr = new QuorumRepositorio();
            string cadena = null;
            try
            {
                //*** Validar TOKEN
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                var respuesta = qr.ValidaQuorum();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("UltimoQuorumCerrado")]
        [HttpPost]
        public HttpResponseMessage UltimoQuorumCerrado()
        {
            urn = new UsuarioRepositorio();
            QuorumRepositorio qr = new QuorumRepositorio();
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
                var respuesta = qr.UltimoQuorumC();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
    }
}
