using SGA.Dao.Dapper;
using SGA.Dominio;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SGA.WebAPI.Controllers
{
    public class CandidatoController : ApiController
    {
        // GET: api/Candidato   JsonResult<List<Candidato>>
        /// <summary>
        /// Obtener todos los Candidatos de la Eleccion X, Ronda Y, Papeleta Z
        /// </summary>
        /// <param name="p_id_eleccion">ID Eleccion</param>
        /// <param name="p_id_ronda">ID Ronda</param>
        /// <param name="p_id_papeleta">ID Papeleta</param>
        /// <returns>Retorna la lista de Candidatos</returns>
        /// 

        [HttpGet]
        [Route("api/Candidato/{id_eleccion}/{id_ronda}/{id_papeleta}")]
        public IEnumerable<Candidato> Get(int Id_eleccion, int Id_ronda, int Id_papeleta)
        {
            CandidatoRepositorio candidato = new CandidatoRepositorio();
            
            return candidato.BuscarTodos(Id_eleccion, Id_ronda, Id_papeleta);
            
        }

        // GET: api/Candidato/5
        [HttpGet]
        [ActionName("BuscarCandidato")]
        [Route("api/Candidato/{id_eleccion}/{id_ronda}/{id_papeleta}/{id_candidato}")]
        public HttpResponseMessage Get(int id_eleccion, int id_ronda, int id_papeleta, int id_candidato)
        {
            CandidatoRepositorio repositorio = new CandidatoRepositorio();
            Candidato candidato = repositorio.BuscarId(id_eleccion, id_ronda, id_papeleta, id_candidato);

            if (candidato == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id_candidato);
            }

            return Request.CreateResponse(HttpStatusCode.OK, candidato);
        }

        // POST: api/Candidato
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Candidato/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Candidato/5
        public void Delete(int id)
        {
        }
    }
}
