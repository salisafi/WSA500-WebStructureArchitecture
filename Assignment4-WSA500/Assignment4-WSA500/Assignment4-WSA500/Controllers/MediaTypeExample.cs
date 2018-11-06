using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment4_WSA500.Controllers
{
    public class MediaTypeExample
    {
        public MediaTypeExample()
        {
            timestamp = DateTime.Now;
            count = 0;
            version = "1.0.0";
            data = new List<dynamic>();
            links = new List<link>();
        }
        public DateTime timestamp { get; set; }
        public string version { get; set; }
        public int count { get; set; }
        public ICollection<dynamic> data { get; set; }
        public List<link> links { get; set; }

        /// <summary>
        /// A hypermedia link
        /// </summary>
        public class link
        {
            public link()
            {
                //fields = new List<field>();
            }

            /// <summary>
            /// Relation kind
            /// </summary>
            public string rel { get; set; } = "";

            /// <summary>
            /// Hypermedia reference URL segment
            /// </summary>
            public string href { get; set; } = "";
        }
    }
}