using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSA500_Assignment3.Controllers
{
    public class EmployeesController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/Employees
        public IHttpActionResult Get()
        {

            return Ok(m.EmployeeGetAll());
        }

        // GET: api/Employees/5
        public IHttpActionResult Get(int? id)
        {
            var o = m.EmployeeGetById(id.GetValueOrDefault());
            if(o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // Attention 06 - Add new
        // POST: api/Employees
        public IHttpActionResult Post([FromBody]EmployeeAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.EmployeeAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.EmployeeId });

            return Created(uri, addedItem);
        }

        // PUT: api/Employees/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employees/5
        public void Delete(int id)
        {
        }
    }
}
