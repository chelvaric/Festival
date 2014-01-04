using models;
using RestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestService.Controllers
{
    public class DataController : ApiController
    {
        // GET api/data
        public IEnumerable<Genre> Get()
        {
            return GenreRepository.Waardes();
        }

        // GET api/data/5
        public string Get(int id)
        {

            return "value";
        }

        // POST api/data
        public void Post([FromBody]string value)
        {
        }

        // PUT api/data/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/data/5
        public void Delete(int id)
        {
        }
    }
}
