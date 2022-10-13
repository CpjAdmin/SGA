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
    public class VotoController : ApiController
    {
        
        //*** VALIDADO - A api/Voto/RegistrarVoto
        [ActionName("RegistrarVoto")]
        [HttpPost]
        public HttpResponseMessage RegistrarVoto([FromBody] Voto objVoto)
        {
            try
            {
                // Repositorio de Usuario
                UsuarioRepositorio urn = new UsuarioRepositorio();

                string cadena = null;
                bool resultado;

                VotoRepositorio votos = new VotoRepositorio();

                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }

                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso. Su TOKEN no es válido!");
                }
                else
                {
                    //*** Obtener la Terminal
                    objVoto.Terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                    //*** Guardar el Voto
                    resultado = votos.Agregar(objVoto);

                    //*** Si es la última papeleta
                    if (objVoto.Id_papeleta == objVoto.Id_voto)
                    {
                        string usuario = objVoto.Usuario;
                        int id_proceso = 37;
                        string tipoResultado;
                        string descripcion = "Voto Guardado,  Usuario: " + objVoto.Usuario + " | Terminal : " + objVoto.Terminal;

                        //*** Si el voto fue guardado correctamente ( TRUE )
                        //*** C = Correcto, E = Error
                        if (resultado)
                        {
                            tipoResultado = "C";
                        }
                        else
                        {
                            tipoResultado = "E";
                        }

                        ProcesoLN.getInstancia().AuditarProceso(id_proceso, usuario, "Votaciones.aspx", descripcion , tipoResultado , System.Web.HttpContext.Current);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, resultado);
                  
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }
    }
}
