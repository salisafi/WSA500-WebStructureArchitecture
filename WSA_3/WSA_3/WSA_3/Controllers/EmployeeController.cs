using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WSA_3.Controllers
{
    public class EmployeeController : ApiController
    {
        private Manager m = new Manager();
        // GET: api/Employees
        public IHttpActionResult Get()
        {
            return Ok(m.EmployeeGetAll());
        }

        // GET: api/Employees/5
        //public IHttpActionResult Get(int id)
        //{
        //    var result = m.GetEmployee(id);
        //    if (result == null)
        //        return NotFound();
        //    else
        //        return Ok(result);

        //}

        // POST: api/Employees
        public IHttpActionResult Post([FromBody]EmployeeAdd newItem)
        {
            if (newItem == null) { return null; }
            if (ModelState.IsValid)
            {
                var addedItem = m.EmployeeAdd(newItem);
                if (addedItem == null)
                {
                    return BadRequest("Cannot add the object");
                }
                else
                {
                    var uri = Url.Link("DefaultApi", new { id = addedItem.EmployeeId });
                    return Created<EmployeeBase>(uri, addedItem);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [Route("api/employees/{id}/setsupervisor")]
        public void PutSetSupervisor(int id, [FromBody]EmployeeSupervisor item)
        {
            // Ensure that an "editedItem" is in the entity body
            if (item == null) { return; }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id != item.Employee) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                m.SetEmployeeSupervisor(item);
            }
            else
            {
                return;
            }
        }

        // PUT: api/Employees/5/ClearSupervisor
        [Route("api/employees/{id}/clearsupervisor")]
        public void PutClearSupervisor(int id, [FromBody]EmployeeSupervisor item)
        {
            // Ensure that an "editedItem" is in the entity body
            if (item == null) { return; }

            // Ensure that the id value in the URI matches the id value in the entity body
            if (id != item.Employee) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid)
            {
                // Attempt to update the item
                m.ClearEmployeeSupervisor(item);
            }
            else
            {
                return;
            }
        }



        /*Photos*/

        //sets the photo
        [Route("api/employee/{id}/setphoto")]
        [HttpPut]
        public IHttpActionResult EmployeePhoto(int id, [FromBody]byte[] photo)
        {
            if (photo.Length > 100000)
            {
                return StatusCode(HttpStatusCode.NotAcceptable);
            }
            var contentType = Request.Content.Headers.ContentType.MediaType;

            if (m.EmployeeSetPhoto(id, contentType, photo))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest("Unable to set the photo");
            }
        }

        //Retreives the photo and passes it back with conneg
        public IHttpActionResult GetPhoto(int? id)
        {
            var o = m.EmployeeGetByIdWithMedia(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }

            var imageHeader = Request.Headers.Accept
                .SingleOrDefault(a => a.MediaType.ToLower().StartsWith("image/"));

            if (imageHeader == null)
            {
                return Ok(m.mapper.Map<EmployeeWithMediaInfo>(o));
            }
            else
            {
                if (o.PhotoLength > 0)
                {
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


        // PUT: api/Employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }
    }
}
