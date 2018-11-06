using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment4_WSA500.Controllers
{
    // Track resource models, Add, Base 

    /// <summary>
    /// Add a new Track class to the view model
    /// </summary>
    public class TrackAdd
    {

        /// <summary>
        /// Name of Track
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Id of Album
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// Id of Media Type
        /// </summary>
        public int MediaTypeId { get; set; }

        /// <summary>
        /// Id of Genre
        /// </summary>
        public int? GenreId { get; set; }

        /// <summary>
        /// Name of Composer
        /// </summary>
        [StringLength(220)]
        public string Composer { get; set; }

        /// <summary>
        /// Duration of Track
        /// </summary>
        public int Milliseconds { get; set; }

        /// <summary>
        /// Number of Bytes In The Track
        /// </summary>
        public int? Bytes { get; set; }

        /// <summary>
        /// Unit Price of Track
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }

    }

    /// <summary>
    /// Track view model class with Id and all other properties
    /// </summary>
    public class TrackBase : TrackAdd
    {
        // Notice, again, the [Key] attribute

        /// <summary>
        /// Unique Track identifier
        /// </summary>
        [Key]
        public int TrackId { get; set; }
    }
}