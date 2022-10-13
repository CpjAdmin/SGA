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
    public class BitacoraProcController : ApiController
    {
        [ActionName("ListadoProc")]
        [HttpPost]
        public HttpResponseMessage ListadoProc()
        {
            BitacoraProcRepositorio br = new BitacoraProcRepositorio();
            try
            {

                var respuesta = br.ListadoProcesos();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }
    }
}
