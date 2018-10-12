using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSA500_Assignment3.Controllers
{
    public class PhotosController : ApiController
    {
        // GET: api/Photos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Photos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Photos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Photos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Photos/5
        public void Delete(int id)
        {
        }
    }
}
