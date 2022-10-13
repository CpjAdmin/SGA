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
    public class EleccionController : ApiController
    {
        UsuarioRepositorio urn = new UsuarioRepositorio();
        [ActionName("InsertarEleccion")]
        [HttpPost]
        public HttpResponseMessage InsertarEleccion([FromBody] Eleccion objEleccion)
        {
            EleccionRepositorio ern = new EleccionRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(1, objEleccion.Usuario, "Elecciones.aspx", "Insertar Elección: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                objEleccion.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = ern.AgregarE(objEleccion);
                ProcesoLN.getInstancia().AuditarProceso(1, objEleccion.Usuario, "Elecciones.aspx", "Insertar Elección " + objEleccion.Id_eleccion.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(1, objEleccion.Usuario, "Elecciones.aspx", "Insertar Elección " + objEleccion.Id_eleccion.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ActualizarEleccion")]
        [HttpPost]
        public HttpResponseMessage ActualizarEleccion([FromBody] Eleccion objEleccion)
        {

            EleccionRepositorio ern = new EleccionRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(2, objEleccion.Usuario, "Elecciones.aspx", "Modificar Elección: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                objEleccion.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = ern.ActualizarE(objEleccion);
                ProcesoLN.getInstancia().AuditarProceso(2, objEleccion.Usuario, "Elecciones.aspx", "Modificar Elección " + objEleccion.Id_eleccion.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(2, objEleccion.Usuario, "Elecciones.aspx", "Modificar Elección " + objEleccion.Id_eleccion.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


        [ActionName("EliminarEleccion")]
        [HttpPost]
        public HttpResponseMessage EliminarEleccion([FromBody] Eleccion objEleccion)
        {

            EleccionRepositorio ern = new EleccionRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(3, objEleccion.Usuario, "Elecciones.aspx", "Eliminar Elección: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                var respuesta = ern.EliminarE(objEleccion);
                ProcesoLN.getInstancia().AuditarProceso(3, objEleccion.Usuario, "Elecciones.aspx", "Eliminar Elección: " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(3, objEleccion.Usuario, "Elecciones.aspx", "Eliminar Elección " + objEleccion.Id_eleccion.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** VALIDADO - VotacionesProgreso.ASPX - Obtiene la lista Elecciones
        [ActionName("ListadoElecciones")]
        [HttpPost]
        public HttpResponseMessage ListadoElecciones()
        {
            EleccionRepositorio ern = new EleccionRepositorio();
            try
            {
                var respuesta = ern.ListadoElecciones();

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }


    }
}
