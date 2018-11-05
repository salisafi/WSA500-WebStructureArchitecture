using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// Attention 01 - This was the originally-created web service controller
// It was renamed in Solution Explorer, to include an "Api" string

// Attention 02 - Change the namespace to include the ".Api" suffix

namespace Assignment4_WSA500.Controllers.Api
{
    public class InvoicesApiController : ApiController
    {
        // Reference to the manager object
        private Manager m = new Manager();

        // Return types are IHttpActionResult for all except the Delete() method, which  void
/*
        /// <summary>
        /// Returns all the Invoices
        /// </summary>
        // GET: api/invoices
        [Route("api/invoices")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(m.InvoiceGetAll());
        }
*/

        /// <summary>
        /// Returns all the invoices with their Customer name
        /// GET: api/Invoices
        /// </summary>
        [Route("api/invoices")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(m.InvoiceWithCustomerInfo());
        }



        /// <summary>
        /// Returns one invoice with specified id    
        /// </summary>
        // GET: api/invoices/5
        [Route("api/invoices/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object
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

        // Attention 32 - Get one Invoice, with associated objects, use attribute routing

        /// <summary>
        /// Returns one invoice with invoiceline obejct for the specified id
        /// </summary>
        // GET: api/invoices/5/invoiceLines
        [Route("api/invoices/{id}/invoiceLines")]
        [HttpGet]
        public IHttpActionResult GetInvoiceLines(int? id)
        {
            // Attempt to fetch the object
            var o = m.InvoiceGetByIdWithInvoiceLines(id.GetValueOrDefault());

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

        /// <summary>
        /// Creates one invoice obejct  
        /// </summary>
        // POST: api/invoices
        [Route("api/invoices")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]InvoiceAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.InvoiceAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.InvoiceId });

            return Created(uri, addedItem);
        }

        /// <summary>
        /// Updates one invoice obejct   
        /// </summary>
        // PUT: api/invoices/5
        [Route("api/invoicesApi/{id}")]
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Delete one invoice obejct  
        /// </summary>
        // DELETE: api/invoices/5
        [Route("api/invoicesApi/{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
            // In a controller 'Delete' method, a void return type will
            // automatically generate a HTTP 204 "No content" response
            m.InvoiceDelete(id);
        }
    }
}
