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
    public class BandController : ApiController
    {
        // GET api/band
        public IEnumerable<Band> Get()
        {
            return BandRepositorycs.Bands();
        }

        // GET api/band/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/band
        public void Post([FromBody]string value)
        {
        }

        // PUT api/band/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/band/5
        public void Delete(int id)
        {
        }
    }
}
