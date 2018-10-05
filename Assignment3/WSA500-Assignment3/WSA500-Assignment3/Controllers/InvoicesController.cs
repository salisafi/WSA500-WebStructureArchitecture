using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WSA500_Assignment3.Controllers
{
    public class InvoicesController : ApiController
    {
        // Attention 06 - Reference to a manager object
        private Manager m = new Manager();


        // Attention 04 - Get all
        // GET: api/Invoices
        public IHttpActionResult Get()
        {
            return Ok(m.InvoiceGetAll());
        }

        // Attention 05 - Get one by its identifier
        // GET: api/Invoices/5
        public IHttpActionResult Get(int? id)
        {
            var o = m.InvoiceGetById(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // POST: api/Invoices
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Invoices/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Invoices/5
        public void Delete(int id)
        {
        }
    }
}
