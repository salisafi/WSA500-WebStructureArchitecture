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
}