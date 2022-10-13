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
    public class ResultadoController : ApiController
    {
        //*** Repositorio de Usuario
        UsuarioRepositorio urn;

        //*** VALIDADO - VotacionesResumen.ASPX - Cargar los resultados en resumen de votación
        [HttpPost]
        [ActionName("ResultadoResumen")]
        public HttpResponseMessage ResultadoResumen([FromBody] JObject data)
        {
            try
            {
                ResultadoEleccion objResultado = data["parametros"].ToObject<ResultadoEleccion>();
                ResultadoRepositorio resultado = new ResultadoRepositorio();

                string Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                objResultado.Terminal = Terminal;

                var respuesta = resultado.ResultadoResumen(objResultado);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        //*** VALIDADO - VotacionesProgreso.ASPX - Cargar los resultados en progreso de la votación
        [HttpPost]
        [ActionName("ResultadoProgreso")]
        public HttpResponseMessage ResultadoProgreso([FromBody] JObject data)
        {
            try
            {
                string cadena = null;
                urn = new UsuarioRepositorio();

                //*** Obtener valores del encabezado en la variable = Auth
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                //*** Validar si el usuario tiene un TOKEN valido y cuanta con el Rol requerido
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }
                else
                {
                    //*** Obtener los parametros del llamado AJAX
                    ResultadoProgreso objResultado = data["parametros"].ToObject<ResultadoProgreso>();
                    ResultadoRepositorio resultado = new ResultadoRepositorio();

                    //*** Obtener la Terminal ( Dirección IP )
                    string Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
                    objResultado.Terminal = Terminal;

                    //*** Ejecutar el proceso ResultadoProgreso
                    var respuesta = resultado.ResultadoProgreso(objResultado);

                    return Request.CreateResponse(HttpStatusCode.OK, respuesta);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
    }
}
