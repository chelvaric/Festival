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
    public class LineUpController : ApiController
    {
        // GET api/lineup
        public IEnumerable<LineUp> Get()
        {
            return LineUpRepository.GetLineUps();
        }

        // GET api/lineup/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/lineup
        public void Post([FromBody]string value)
        {
        }

        // PUT api/lineup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/lineup/5
        public void Delete(int id)
        {
        }
    }
}
