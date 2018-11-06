using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4_WSA500.Controllers
{
    public class AlbumAdd
    {
        public AlbumAdd()
        {
            Tracks = new List<TrackBase>();
        }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        public int ArtistId { get; set; }

        // public  Artist Artist { get; set; }

        public IEnumerable<TrackBase> Tracks { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {

        [Key]
        public int AlbumId { get; set; }
    }

    /// <summary>
    /// One specific album, and its associated track, by its identifier
    /// </summary>
    public class AlbumByIdWithTracks : AlbumBase
    {
        public AlbumByIdWithTracks()
        {
            // Attention 06 - When there is a collection, initialize it in the default constructor
            Tracks = new List<TrackBase>();
        }

        // Attention 07 - Notice IEnumerable (not ICollection); do NOT use "virtual" keyword
        public IEnumerable<TrackBase> Tracks { get; set; }
    }

    /// <summary>
    /// Edit specific album by id
    /// </summary>
    public class AlbumEdit : AlbumBase
    {
        /// <summary>
        /// Unique album identifier
        /// </summary>
        [Key]
        public int AlbumId { get; set; }
        /// <summary>
        ///  Album titel
        /// </summary>
        [Required, StringLength(30)]
        public string Title { get; set; }

    }

    /// <summary>
    /// One specific album, and its associated artist, by its identifier
    /// </summary>
    // Attention 05 - Inheritance works in this simple situation
    public class AlbumWithArtist : AlbumBase
    {
        public AlbumWithArtist()
        {
            Artists = new List<ArtistBase>();
        }
        public IEnumerable<ArtistBase> Artists = new List<ArtistBase>();
    }
}