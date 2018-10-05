using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Attention 09 - InvoiceLine resource models, Add, Base, WithInvoice, and ...

namespace WSA500_Assignment3.Controllers
{
    public class InvoiceLineAdd
    {
        public InvoiceLineAdd() { }

        [Required]
        public int InvoiceId { get; set; }

        public int Quantity { get; set; }

        public int TrackId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }


      //  public Track Track { get; set; }
    }

    public class InvoiceLineBase : InvoiceLineAdd
    {
        public InvoiceLineBase() { }

        public int InvoiceLineId { get; set; }
    }

    public class InvoiceLineWithInvoice : InvoiceBase
    {
        public InvoiceBase Invoice { get; set; }
    }
}