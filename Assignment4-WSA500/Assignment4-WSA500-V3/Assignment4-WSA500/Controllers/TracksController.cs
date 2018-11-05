using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment4_WSA500.Controllers
{
    public class TracksController : ApiController
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: api/Tracks
        public IHttpActionResult Get()
        {
            return Ok(m.TracksGetAll());
        }

        // GET: api/Tracks/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to locate the matching object
            var o = m.TrackGetOne(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // POST: api/Tracks
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tracks/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tracks/5
        public void Delete(int id)
        {
        }
    }
}
