using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using SGA.Dao.Dapper;
using SGA.Dominio;

namespace SGA.WebAPI.Controllers
{
    public class DispositivoController : ApiController
    {
        [ActionName("InsertarDispositivo")]
        [HttpPost]
        public HttpResponseMessage InsertarDispositivo([FromBody] JObject data)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            DispositivoRepositorio drn = new DispositivoRepositorio();
            string cadena = null;
            Dispositivo dispositivo = new Dispositivo();
            try
            {
                dynamic objDispositivo = data.ToObject<dynamic>();
                dispositivo.Nombre = objDispositivo.Nombre.ToString();
                dispositivo.Descripcion = objDispositivo.Descripcion.ToString();
                dispositivo.Telefono = objDispositivo.Telefono.ToString();
                dispositivo.I_estado = objDispositivo.I_estado.ToString();
                dispositivo.User_creacion = objDispositivo.User_creacion.ToString();

                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(28, dispositivo.User_creacion, "Dispositivo.aspx", "Insertar Dispositivo, acceso no autorizado ", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");

                }


                dispositivo.Terminal_creacion = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = drn.InsertarD(dispositivo);
                ProcesoLN.getInstancia().AuditarProceso(28, dispositivo.User_creacion, "Dispositivo.aspx", "Insertar dispositivo " + dispositivo.Nombre + ": " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(28, dispositivo.User_creacion, "Dispositivo.aspx", "Insertar dispositivo " + dispositivo.Nombre + ": " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ModificarDispositivo")]
        [HttpPost]
        public HttpResponseMessage ModificarDispositivo([FromBody] JObject data)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            DispositivoRepositorio drn = new DispositivoRepositorio();
            string cadena = null;
            Dispositivo dispositivo = new Dispositivo();
            try
            {
                dynamic objDispositivo = data.ToObject<dynamic>();
                dispositivo.Id_dispositivo = Int32.Parse(objDispositivo.Id_dispositivo.ToString());
                dispositivo.Nombre = objDispositivo.Nombre.ToString();
                dispositivo.Descripcion = objDispositivo.Descripcion.ToString();
                dispositivo.Telefono = objDispositivo.Telefono.ToString();
                dispositivo.I_estado = objDispositivo.I_estado.ToString();
                dispositivo.User_creacion = objDispositivo.User_creacion.ToString();

                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(29, dispositivo.User_creacion, "Dispositivo.aspx", "Modificar Dispositivo, acceso no autorizado ", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");

                }
                dispositivo.Terminal_creacion = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = drn.ActualizarD(dispositivo);
                ProcesoLN.getInstancia().AuditarProceso(29, dispositivo.User_creacion, "Dispositivo.aspx", "Modificar dispositivo " + dispositivo.Nombre + ": " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(29, dispositivo.User_creacion, "Dispositivo.aspx", "Modificar dispositivo " + dispositivo.Nombre + ": " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


        [ActionName("EliminarDispositivo")]
        [HttpPost]
        public HttpResponseMessage EliminarDispositivo([FromBody] JObject data)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            DispositivoRepositorio drn = new DispositivoRepositorio();
            string cadena = null;
            Dispositivo dispositivo = new Dispositivo();
            try
            {
                dynamic objDispositivo = data.ToObject<dynamic>();
                int id = Int32.Parse(objDispositivo.Id_dispositivo.ToString());


                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(30, objDispositivo.User_creacion.ToString(), "Dispositivo.aspx", "Eliminar Dispositivo, acceso no autorizado ", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");

                }
                dispositivo.Terminal_creacion = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = drn.EliminarD(id);
                ProcesoLN.getInstancia().AuditarProceso(30, objDispositivo.User_creacion.ToString(), "Dispositivo.aspx", "Eliminar dispositivo " + id.ToString() + ": " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(30, "ERROR", "Dispositivo.aspx", "Eliminar dispositivo " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ListaDispositivos")]
        [HttpPost]
        public HttpResponseMessage ListaDispositivos()
        {
            DispositivoRepositorio dr = new DispositivoRepositorio();
            try
            {
                var respuesta = dr.ListaDispositivos();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ListaDispositivosActivos")]
        [HttpPost]
        public HttpResponseMessage ListaDispositivosActivos()
        {
            DispositivoRepositorio dr = new DispositivoRepositorio();
            try
            {
                var respuesta = dr.ListaDispositivos();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

    }
}
