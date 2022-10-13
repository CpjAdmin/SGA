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
    public class PapeletaRondaController : ApiController
    {
        UsuarioRepositorio urn = new UsuarioRepositorio();
        [ActionName("InsertarPapeletaR")]
        [HttpPost]
        public HttpResponseMessage InsertarPapeletaR([FromBody] PapeletaRonda objPapeletaR)
        {
            PapeletaRondaRepositorio prr = new PapeletaRondaRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(7, objPapeletaR.Usuario, "Elecciones.aspx", "Insertar Elección Papeleta Ronda: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                objPapeletaR.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = prr.AgregarPR(objPapeletaR);
                ProcesoLN.getInstancia().AuditarProceso(7, objPapeletaR.Usuario, "Elecciones.aspx", "Insertar Elección Papeleta Ronda:, Eleccion " + objPapeletaR.Id_eleccion.ToString() + ", Ronda " + objPapeletaR.Id_ronda.ToString() + ", Papeleta " + objPapeletaR.Id_papeleta.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(7, objPapeletaR.Usuario, "Elecciones.aspx", "Insertar Elección Papeleta Ronda:, Eleccion " + objPapeletaR.Id_eleccion.ToString() + ", Ronda " + objPapeletaR.Id_ronda.ToString() + ", Papeleta " + objPapeletaR.Id_papeleta.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("ActualizarPapeletaR")]
        [HttpPost]
        public HttpResponseMessage ActualizarPapeletaR([FromBody] PapeletaRonda objPapeletaR)
        {

            PapeletaRondaRepositorio prr = new PapeletaRondaRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(8, objPapeletaR.Usuario, "Elecciones.aspx", "Actualizar Elección Papeleta Ronda: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                objPapeletaR.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = prr.ActualizarPR(objPapeletaR);
                ProcesoLN.getInstancia().AuditarProceso(8, objPapeletaR.Usuario, "Elecciones.aspx", "Modificar Elección Papeleta Ronda:, Eleccion " + objPapeletaR.Id_eleccion.ToString() + ", Ronda " + objPapeletaR.Id_ronda.ToString() + ", Papeleta " + objPapeletaR.Id_papeleta.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(8, objPapeletaR.Usuario, "Elecciones.aspx", "Modificar Elección Papeleta Ronda:, Eleccion " + objPapeletaR.Id_eleccion.ToString() + ", Ronda " + objPapeletaR.Id_ronda.ToString() + ", Papeleta " + objPapeletaR.Id_papeleta.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


        [ActionName("EliminarPapeletaR")]
        [HttpPost]
        public HttpResponseMessage EliminarPapeletaR([FromBody] PapeletaRonda objPapeletaR)
        {

            PapeletaRondaRepositorio prr = new PapeletaRondaRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(18, objPapeletaR.Usuario, "Elecciones.aspx", "Eliminar Elección Papeleta Ronda: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                var respuesta = prr.EliminarPR(objPapeletaR);
                ProcesoLN.getInstancia().AuditarProceso(18, objPapeletaR.Usuario, "Elecciones.aspx", "Eliminar Elección Papeleta Ronda:, Eleccion " + objPapeletaR.Id_eleccion.ToString() + ", Ronda " + objPapeletaR.Id_ronda.ToString() + ", Papeleta " + objPapeletaR.Id_papeleta.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(7, objPapeletaR.Usuario, "Elecciones.aspx", "Eliminar Elección Papeleta Ronda:, Eleccion " + objPapeletaR.Id_eleccion.ToString() + ", Ronda " + objPapeletaR.Id_ronda.ToString() + ", Papeleta " + objPapeletaR.Id_papeleta.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** VALIDADO - VotacionesResumen.ASPX - Obtiene la lista de Papeletas por Elección
        [ActionName("ListadoPapeletasR")]
        [HttpPost]
        public HttpResponseMessage ListadoPapeletasR([FromBody] PapeletaRonda objPapeletaR)
        {
            PapeletaRondaRepositorio prr = new PapeletaRondaRepositorio();
            try
            {
                var respuesta = prr.ListadoPapeletasR(objPapeletaR);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("ListadoPapeletas")]
        [HttpPost]
        public HttpResponseMessage ListadoPapeletas()
        {
            PapeletaRondaRepositorio prr = new PapeletaRondaRepositorio();
            try
            {

                var respuesta = prr.ListadoPapeletas();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** VALIDADO - INICIO.ASPX Y VOTACIONES.ASPX - Obtiene la papeleta actual del delegado
        [ActionName("PapeletaActual")]
        [HttpPost]
        public HttpResponseMessage PapeletaActual([FromBody] JObject data)
        {
            try
            {
                Papeleta objPapeleta = data["parametros"].ToObject<Papeleta>();

                PapeletaRondaRepositorio ern = new PapeletaRondaRepositorio();

                string Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                objPapeleta.Terminal = Terminal;

                var respuesta = ern.PapeletaActual(objPapeleta);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }


    }
}
