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
    public class PapeletaCandidatoController : ApiController
    {
        UsuarioRepositorio urn = new UsuarioRepositorio();
        [ActionName("InsertarPapeletaC")]
        [HttpPost]
        public HttpResponseMessage InsertarPapeletaC([FromBody] PapeletaCandidato objPapeletaC)
        {
            PapeletaCandidatoRepositorio pcr = new PapeletaCandidatoRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(9, objPapeletaC.Usuario, "Elecciones.aspx", "Insertar Elección Papeleta Ronda candidato: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                objPapeletaC.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = pcr.AgregarPC(objPapeletaC);
                ProcesoLN.getInstancia().AuditarProceso(9, objPapeletaC.Usuario, "Elecciones.aspx", "Insertar Elección Papeleta Ronda candidato:, Eleccion " + objPapeletaC.Id_eleccion.ToString() + ", Ronda " + objPapeletaC.Id_ronda.ToString() + ", Papeleta " + objPapeletaC.Id_papeleta.ToString() + ", Candidato " + objPapeletaC.Id_candidato.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(9, objPapeletaC.Usuario, "Elecciones.aspx", "Insertar Elección Papeleta Ronda candidato:, Eleccion " + objPapeletaC.Id_eleccion.ToString() + ", Ronda " + objPapeletaC.Id_ronda.ToString() + ", Papeleta " + objPapeletaC.Id_papeleta.ToString() + ", Candidato " + objPapeletaC.Id_candidato.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ActualizarPapeletaC")]
        [HttpPost]
        public HttpResponseMessage ActualizarPapeletaC([FromBody] PapeletaCandidato objPapeletaC)
        {

            PapeletaCandidatoRepositorio pcr = new PapeletaCandidatoRepositorio();
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
                objPapeletaC.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = pcr.ActualizarPC(objPapeletaC);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


        [ActionName("EliminarPapeletaC")]
        [HttpPost]
        public HttpResponseMessage EliminarPapeletaC([FromBody] PapeletaCandidato objPapeletaC)
        {

            PapeletaCandidatoRepositorio pcr = new PapeletaCandidatoRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(9, objPapeletaC.Usuario, "Elecciones.aspx", "Eliminar Papeleta Ronda candidato: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                objPapeletaC.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                var respuesta = pcr.EliminarPC(objPapeletaC);
                ProcesoLN.getInstancia().AuditarProceso(9, objPapeletaC.Usuario, "Elecciones.aspx", "Eliminar Elección Papeleta Ronda candidato:, Eleccion " + objPapeletaC.Id_eleccion.ToString() + ", Ronda " + objPapeletaC.Id_ronda.ToString() + ", Papeleta " + objPapeletaC.Id_papeleta.ToString() + ", Candidato " + objPapeletaC.Id_candidato.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(9, objPapeletaC.Usuario, "Elecciones.aspx", "Eliminar Elección Papeleta Ronda candidato:, Eleccion " + objPapeletaC.Id_eleccion.ToString() + ", Ronda " + objPapeletaC.Id_ronda.ToString() + ", Papeleta " + objPapeletaC.Id_papeleta.ToString() + ", Candidato " + objPapeletaC.Id_candidato.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


        [ActionName("ListadoCandidatosS")]
        [HttpPost]
        public HttpResponseMessage ListadoCandidatosS([FromBody] PapeletaCandidato objPapeletaC)
        {
            PapeletaCandidatoRepositorio pcr = new PapeletaCandidatoRepositorio();
            try
            {

                var respuesta = pcr.Listados(objPapeletaC);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ListadoCandidatos")]
        [HttpPost]
        public HttpResponseMessage ListadoCandidatos([FromBody] PapeletaCandidato objPapeletaC)
        {
            PapeletaCandidatoRepositorio pcr = new PapeletaCandidatoRepositorio();
            try
            {

                var respuesta = pcr.Listado(objPapeletaC);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


        [ActionName("ActualizarPapeletaPos")]
        [HttpPost]
        public HttpResponseMessage ActualizarPapeletaPos([FromBody] PapeletaCandidato objPapeletaC)
        {

            PapeletaCandidatoRepositorio pcr = new PapeletaCandidatoRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(19, objPapeletaC.Usuario, "Elecciones.aspx", "Asignación de posiciones de papeleta: Aceedió a un servicio no autorizado", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                var respuesta = pcr.ActualizarPosicion(objPapeletaC);
                ProcesoLN.getInstancia().AuditarProceso(19, objPapeletaC.Usuario, "Elecciones.aspx", "Asignación de posiciones de papeleta: Eleccion " + objPapeletaC.Id_eleccion.ToString() + ", Ronda " + objPapeletaC.Id_ronda.ToString() + ", Papeleta " + objPapeletaC.Id_papeleta.ToString() + ":" + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(19, objPapeletaC.Usuario, "Elecciones.aspx", "Asignación de posiciones de papeleta:  Eleccion " + objPapeletaC.Id_eleccion.ToString() + ", Ronda " + objPapeletaC.Id_ronda.ToString() + ", Papeleta " + objPapeletaC.Id_papeleta.ToString() + ":" + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


    }
}
