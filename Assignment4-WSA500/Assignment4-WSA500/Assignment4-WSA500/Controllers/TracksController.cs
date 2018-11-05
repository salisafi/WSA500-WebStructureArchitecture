using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Assignment4_WSA500.Controllers
{
    /// <summary>
    /// Actions for Tracks
    /// </summary>
    public class TracksController : ApiController
    {

        // Reference to the data manager
        private Manager m = new Manager();

        // GET: api/Tracks
        // Attention 06 - Documentation for each public method
        /// <summary>
        /// All Tracks
        /// </summary>
        /// <returns>Collection of Track objects, sorted</returns>
        public IHttpActionResult Get()
        {
            return Ok(m.TracksGetAll());
        }

        // GET: api/Tracks/5
        /// <summary>
        /// Specific Track, using its identifier
        /// </summary>
        /// <param name="id">Tracks identifier</param>
        /// <returns>Tracks object</returns>
        public IHttpActionResult Get(int? id)
        {
            // Attempt to locate the matching object
            var o = m.TrackGetOne(id.GetValueOrDefault());

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
/*
        /// <summary>
        /// Specific Track, using its identifier
        /// </summary>
        /// <param name="id">Tracks identifier</param>
        /// <returns>Tracks object</returns>
        // GET: api/Tracks/5/WithEmployee
        [Route("api/Tracks/{id}/withAlbum")]
        public IHttpActionResult GetwithAlbum(int? id)
        {
            // Attempt to locate the matching object
            var o = m.TrackGetOnewithAlbum(id.GetValueOrDefault(id.GetValueOrDefault()));

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

*/
        // POST: api/Tracks
        /// <summary>
        /// Add new Track
        /// </summary>
        /// <param name="newItem">New Track object</param>
        /// <returns>Track object</returns>
        [ResponseType(typeof(TrackBase))]
        public IHttpActionResult Post([FromBody]TrackAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = m.TrackAdd(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.TrackId });

            return Created(uri, addedItem);
        }

        /*  // PUT: api/Tracks/5
         public void Put(int id, [FromBody]string value)
         {
         } */

        // DELETE: api/Tracks/5
        /// <summary>
        /// Delete Track
        /// </summary>
        /// <param name="id">Track identifier</param>
        public void Delete(int id)
        {
            // In a controller 'Delete' method, a void return type will
            // automatically generate a HTTP 204 "No content" response
            m.TrackDelete(id);
        }

    }
}
