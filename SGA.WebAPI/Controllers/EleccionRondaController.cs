using Newtonsoft.Json.Linq;
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
    public class EleccionRondaController : ApiController
    {
        //*** VALIDADO - INICIO.ASPX - Obtiene la lista de Rondas de la Elección
        [ActionName("ListadoRondas")]
        [HttpGet]
        public HttpResponseMessage ListadoRondas()
        {
            EleccionRondaRepositorio ern = new EleccionRondaRepositorio();
            try
            {
                var ObjRondas = ern.ListadoRondas();
                var respuesta = ObjRondas.Count;

                return Request.CreateResponse(HttpStatusCode.OK, new { respuesta, ObjRondas });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        UsuarioRepositorio urn = new UsuarioRepositorio();
        [ActionName("InsertarEleccionR")]
        [HttpPost]
        public HttpResponseMessage InsertarEleccionR([FromBody] EleccionRonda objEleccionR)
        {
            EleccionRondaRepositorio err = new EleccionRondaRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(4, objEleccionR.Usuario, "Elecciones.aspx", "Insertar Elección Ronda: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                objEleccionR.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = err.AgregarER(objEleccionR);
                ProcesoLN.getInstancia().AuditarProceso(4, objEleccionR.Usuario, "Elecciones.aspx", "Insertar Ronda de Elección, Eleccion " + objEleccionR.Id_eleccion.ToString() + ", Ronda " + objEleccionR.Id_ronda.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(4, objEleccionR.Usuario, "Elecciones.aspx", "Insertar Ronda de Elección, Eleccion " + objEleccionR.Id_eleccion.ToString() + ", Ronda " + objEleccionR.Id_ronda.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ActualizarEleccionR")]
        [HttpPost]
        public HttpResponseMessage ActualizarEleccionR([FromBody] EleccionRonda objEleccionR)
        {

            EleccionRondaRepositorio err = new EleccionRondaRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(5, objEleccionR.Usuario, "Elecciones.aspx", "Modificar Elección Ronda: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                objEleccionR.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = err.ActualizarER(objEleccionR);
                ProcesoLN.getInstancia().AuditarProceso(5, objEleccionR.Usuario, "Elecciones.aspx", "Modificar Ronda de Elección, Eleccion " + objEleccionR.Id_eleccion.ToString() + ", Ronda " + objEleccionR.Id_ronda.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(5, objEleccionR.Usuario, "Elecciones.aspx", "Modificar Ronda de Elección, Eleccion " + objEleccionR.Id_eleccion.ToString() + ", Ronda " + objEleccionR.Id_ronda.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


        [ActionName("EliminarEleccionR")]
        [HttpPost]
        public HttpResponseMessage EliminarEleccionR([FromBody] EleccionRonda objEleccionR)
        {

            EleccionRondaRepositorio err = new EleccionRondaRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(6, objEleccionR.Usuario, "Elecciones.aspx", "Insertar Elección Ronda: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                var respuesta = err.EliminarER(objEleccionR);
                ProcesoLN.getInstancia().AuditarProceso(6, objEleccionR.Usuario, "Elecciones.aspx", "Eliminar Ronda de Elección, Eleccion " + objEleccionR.Id_eleccion.ToString() + ", Ronda " + objEleccionR.Id_ronda.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(6, objEleccionR.Usuario, "Elecciones.aspx", "Eliminar Ronda de Elección, Eleccion " + objEleccionR.Id_eleccion.ToString() + ", Ronda " + objEleccionR.Id_ronda.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** VALIDADO - VotacionesResumen.ASPX - Obtiene la lista de Rondas por Elección
        [ActionName("ListadoEleccionesR")]
        [HttpPost]
        public HttpResponseMessage ListadoEleccionesR([FromBody] EleccionRonda objEleccionR)
        {
            EleccionRondaRepositorio err = new EleccionRondaRepositorio();
            try
            {
                var respuesta = err.ListadoEleccionesR(objEleccionR);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        //*** VALIDADO - VotacionesProgreso.ASPX - Obtiene la lista de Rondas por Elección
        [ActionName("ListaRondasPorEleccion")]
        [HttpPost]
        public HttpResponseMessage ListaRondasPorEleccion([FromBody] JObject data)
        {
            try
            {
                EleccionRondaRepositorio err = new EleccionRondaRepositorio();
                EleccionRonda objEleccionR = data["DatosER"].ToObject<EleccionRonda>();

                var respuesta = err.ListadoEleccionesR(objEleccionR);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
    }
}
