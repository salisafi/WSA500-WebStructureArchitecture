using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Assignment4_WSA500.Models;

namespace Assignment4_WSA500.Controllers
{
    /// <summary>
    /// Artist "add" resource model class
    /// </summary>
    public class ArtistAdd
    {
        public ArtistAdd()
        {
            Name = "";
        }

        [StringLength(120)]
        /// <summary>
        /// Artist name
        /// </summary>
        public string Name { get; set; }

    }

    /// <summary>
    /// Artist "base" resource model class
    /// </summary>
    public class ArtistBase : ArtistAdd
    {
        /// <summary>
        /// Artist Id
        /// </summary>
        public int ArtistId { get; set; }
    }

    /// <summary>
    /// Album associated artist
    /// </summary>
    public class ArtistWithAlbums : ArtistBase
    {
        public ArtistWithAlbums()
        {
            Albums = new List<AlbumBase>();
        }
        public IEnumerable<AlbumBase> Albums { get; set; }
    }
}