using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Assignment4_WSA500.Controllers.Api
{
    /// <summary>
    /// Actions for Albums
    /// </summary>
    public class AlbumsApiController : ApiController
    {

        // Reference to the manager object
        private Manager m = new Manager();

        // Return types are IHttpActionResult for all except the Delete() method, which  void

        // Attention 06 - Documentation for each public method
        /// <summary>
        /// All Albums
        /// </summary>
        /// <returns>Collection of Album objects, sorted</returns>
        // GET: api/AlbumsApi
        [ResponseType(typeof(AlbumBase))]
        public IHttpActionResult Get()
        {
            return Ok(m.AlbumGetAll());
        }

        /// <summary>
        /// Specific Album, using its identifier
        /// </summary>
        /// <param name="id">Album identifier</param>
        /// <returns>Album object</returns>
        // GET: api/AlbumsApi/5
        [ResponseType(typeof(AlbumBase))]
        public IHttpActionResult Get(int? id)
        {
            // Attempt to fetch the object
            var o = m.AlbumGetById(id.GetValueOrDefault());

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

        // Get one, with associated object, use attribute routing

        // GET: api/AlbumsApi/5/WithTrack
        /// <summary>
        /// Specific Album and associated employee data, using its identifier
        /// </summary>
        /// <param name="id">Album identifier</param>
        /// <returns>Customer object</returns>
        [Route("api/albums/{id}/tracks")]
        [ResponseType(typeof(AlbumWithTracks))]
        public IHttpActionResult GetWithEmployee(int? id)
        {
            // Attempt to fetch the object
            var o = m.AlbumGetByIdWithTracks(id.GetValueOrDefault());

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

        // POST: api/AlbumsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AlbumsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AlbumsApi/5
        public void Delete(int id)
        {
        }
    }
}
