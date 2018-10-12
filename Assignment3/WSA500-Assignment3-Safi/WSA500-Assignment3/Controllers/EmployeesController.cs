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

        /*   // GET: api/Employees/5
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
            } */

        // Attention 05 - New version of the Get method, with content negotiation (conneg)
        // GET: api/Employee/5
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object, WITH media

            var o = m.EmployeeGetByIdWithMedia(id.GetValueOrDefault());

            // Continue?
            if (o == null)
            {
                return NotFound();
            }

            // Otherwise, continue...

            // Attention 06 - Here is the content negotiation code

            // Look for an Accept header that starts with "image"

            var imageHeader = Request.Headers.Accept
                .SingleOrDefault(a => a.MediaType.ToLower().StartsWith("image/"));

            if (imageHeader == null)
            {
                // Normal processing for a JSON result
                // Remove the "Photo" property
                return Ok(m.mapper.Map<EmployeeWithMediaInfo>(o));
            }
            else
            {
                // Special processing for an image result

                // Confirm that a media item exists
                if (o.PhotoLength > 0)
                {
                    // Return the result, using the custom media formatter
                    return Ok(o.Photo);
                }
                else
                {
                    // Otherwise, return "not found"
                    // Yes, this is correct. Read the RFC: https://tools.ietf.org/html/rfc7231#section-6.5.4
                    return NotFound();
                }
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
