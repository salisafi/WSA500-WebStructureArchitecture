using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSA500_Assignment3.Controllers
{
    public class InvoiceLinesController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/InvoiceLines
        public IHttpActionResult Get()
        {
            return Ok(m.InvoiceLineGetAll());
        }

        // GET: api/InvoiceLines/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InvoiceLines
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/InvoiceLines/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/InvoiceLines/5
        public void Delete(int id)
        {
        }
    }
}
