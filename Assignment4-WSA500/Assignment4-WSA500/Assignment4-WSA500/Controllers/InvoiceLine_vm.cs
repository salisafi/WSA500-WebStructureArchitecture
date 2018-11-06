using Assignment4_WSA500.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment4_WSA500.Controllers
{
    /// <summary>
    /// InvoiceLine "base" class
    /// </summary>
    public class InvoiceLineBase
    {
        /// <summary>
        /// Unique InvoiceLine identifier
        /// </summary>
        public int InvoiceLineId { get; set; }

        /// <summary>
        /// Unique Invoice identifier
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Unique Track identifier
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// Unit price of each InvoiceLine item
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Number of InvoiceLine
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Which Invoice this InvoiceLine belongs to
        /// </summary>
        public Invoice Invoice { get; set; }

        /// <summary>
        /// Which Track is associated with this InvoiceLine
        /// </summary>
        public Track Track { get; set; }
    }
}