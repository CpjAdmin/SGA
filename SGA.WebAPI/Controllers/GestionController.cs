using SGA.Dao.Dapper;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SGA.WebAPI.Controllers
{
    public class GestionController : ApiController
    {
        UsuarioRepositorio urn;

        //*** VALIDADO - Ingresar Gestión de Delegado
        [ActionName("IngresarGestion")]
        [HttpPost]
        public HttpResponseMessage IngresarGestion([FromBody] Gestion objGestion)
        {
            urn = new UsuarioRepositorio();
            GestionRepositorio gr = new GestionRepositorio();
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

                //*** Validar Terminal   
                objGestion.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                //*** Registrar Respuesta   
                var respuesta = gr.Ingresar(objGestion);

                if (respuesta.Error == 0)
                {
                    //*** Registrar Auditoría  
                    ProcesoLN.getInstancia().AuditarProceso(20, objGestion.Usuario, "Gestion.aspx", "Ingresar Gestión Delegado - " + respuesta.Mensaje, "C", System.Web.HttpContext.Current);
                }
                else
                {
                    //*** Registrar Auditoría  
                    ProcesoLN.getInstancia().AuditarProceso(20, objGestion.Usuario, "Gestion.aspx", "Ingresar Gestión Delegado - " + respuesta.Mensaje, "E", System.Web.HttpContext.Current);
                }

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(20, objGestion.Usuario, "Gestion.aspx", "Ingresar Delegado - " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        //*** VALIDADO - Ingresar Registro de Gestión Candidato
        [ActionName("IngresarGestionC")]
        [HttpPost]
        public HttpResponseMessage IngresarGestionC([FromBody] Gestion objGestion)
        {
            urn = new UsuarioRepositorio();
            GestionRepositorio gr = new GestionRepositorio();
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

                //*** Validar Terminal 
                objGestion.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                //*** Registrar Respuesta  
                var respuesta = gr.IngresarC(objGestion);

                if (respuesta.Error == 0)
                {
                    //*** Registrar Auditoría  
                    ProcesoLN.getInstancia().AuditarProceso(21, objGestion.Usuario, "Gestion.aspx", "Ingresar Gestión Candidato - " + respuesta.Mensaje, "C", System.Web.HttpContext.Current);
                }
                else
                {
                    //*** Registrar Auditoría  
                    ProcesoLN.getInstancia().AuditarProceso(21, objGestion.Usuario, "Gestion.aspx", "Ingresar Gestión Candidato - " + respuesta.Mensaje, "E", System.Web.HttpContext.Current);
                }

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(21, objGestion.Usuario, "Gestion.aspx", "Ingresar Gestión Candidato - " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** VALIDADO - Actualizar Registro de Gestión Delegado
        [ActionName("ModificarGestion")]
        [HttpPost]
        public HttpResponseMessage ModificarGestion([FromBody] Gestion objGestion)
        {
            urn = new UsuarioRepositorio();
            GestionRepositorio gr = new GestionRepositorio();
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

                //*** Validar Terminal   
                objGestion.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                //*** Registrar Respuesta   
                var respuesta = gr.Actualizar(objGestion);

                if (respuesta.Error == 0)
                {
                    //*** Registrar Auditoría  
                    ProcesoLN.getInstancia().AuditarProceso(21, objGestion.Usuario, "Gestion.aspx", "Modificar Gestión Delegado - " + respuesta.Mensaje, "C", System.Web.HttpContext.Current);
                }
                else
                {
                    //*** Registrar Auditoría  
                    ProcesoLN.getInstancia().AuditarProceso(21, objGestion.Usuario, "Gestion.aspx", "Modificar Gestión Delegado - " + respuesta.Mensaje, "E", System.Web.HttpContext.Current);
                }

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(21, objGestion.Usuario, "Gestion.aspx", "Modificar Gestión Delegado - " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        //*** VALIDADO - Eliminar Registro de Gestión Delegado
        [ActionName("EliminarGestionD")]
        [HttpPost]
        public HttpResponseMessage EliminarGestionD([FromBody] Gestion objGestion)
        {
            urn = new UsuarioRepositorio();
            GestionRepositorio gr = new GestionRepositorio();
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

                //*** Validar Terminal  
                objGestion.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                //*** Registrar Respuesta  
                var respuesta = gr.Eliminar(objGestion);

                if (respuesta.Error == 0)
                {
                    //*** Registrar Auditoría  
                    ProcesoLN.getInstancia().AuditarProceso(22, objGestion.Usuario, "Gestion.aspx", "Eliminar Gestión Delegado - " + respuesta.Mensaje, "C", System.Web.HttpContext.Current);
                }
                else
                {
                    //*** Registrar Auditoría  
                    ProcesoLN.getInstancia().AuditarProceso(22, objGestion.Usuario, "Gestion.aspx", "Eliminar Gestión Delegado - " + respuesta.Mensaje, "E", System.Web.HttpContext.Current);
                }

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(22, objGestion.Usuario, "Gestion.aspx", "Eliminar Gestión " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** VALIDADO - Lista de Uso de la Palabra - Delegados
        [ActionName("ListaGestion")]
        [HttpPost]
        public HttpResponseMessage ListaGestion()
        {

            GestionRepositorio gr = new GestionRepositorio();

            try
            {
                var respuesta = gr.ListaGestion();

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        //*** VALIDADO - Lista de Uso de la Palabra - Candidatos
        [ActionName("ListaGestionC")]
        [HttpPost]
        public HttpResponseMessage ListaGestionC([FromBody] int id)
        {

            GestionRepositorio gr = new GestionRepositorio();

            try
            {
                var respuesta = gr.ListaGestionC(id);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }


    }
}
