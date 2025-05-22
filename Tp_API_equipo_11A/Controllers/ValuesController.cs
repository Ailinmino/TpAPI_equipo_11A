using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio;
using Negocio;

namespace Tp_API_equipo_11A.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()//LISTAR
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 BUSCAR
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values FORMULARIO DE ALTA
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5 ACTUALIZAR
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5 BAJA
        public void Delete(int id)
        {
        }
    }
}
