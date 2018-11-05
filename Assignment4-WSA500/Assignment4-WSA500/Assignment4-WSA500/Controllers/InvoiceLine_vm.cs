using Assignment4_WSA500.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment4_WSA500.Controllers
{
    public class InvoiceLineBase
    {
            public int InvoiceLineId { get; set; }

            public int InvoiceId { get; set; }

            public int TrackId { get; set; }

            [Column(TypeName = "numeric")]
            public decimal UnitPrice { get; set; }

            public int Quantity { get; set; }

            public Invoice Invoice { get; set; }

            public Track Track { get; set; }
    }
}