using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSA500_Assignment3.Controllers
{
    public class InvoiceAdd
    {
        public InvoiceAdd(){
            InvoiceDate = new DateTime();
        }

        [StringLength(70)]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        public string BillingCity { get; set; }

        [StringLength(40)]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        public string BillingPostalCode { get; set; }

        [StringLength(40)]
        public string BillingState { get; set; }

        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Total { get; set; }
    }

    public class InvoiceBase : InvoiceAdd
    {
        public int InvoiceId { get; set; }
    } 

    public class InvoiceWithCustomer : InvoiceAdd
    {
       [Required]
       public CustomerBase Customer { get; set; }
    }

    public class InvoiceWithInvoiceLine : InvoiceAdd
    {
        public InvoiceWithInvoiceLine()
        {
            InvoiceLines = new List<InvoiceLineBase>();
        }
        public IEnumerable<InvoiceLineBase> InvoiceLines { get; set; }
    }
}