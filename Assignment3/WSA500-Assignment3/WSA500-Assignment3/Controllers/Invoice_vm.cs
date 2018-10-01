using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSA500_Assignment3.Controllers
{
    public class InvoiceAdd
    {
        public InvoiceAdd(){
            DateTime InvoiceDate = new DateTime().now;
        }

        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        public string BillingCity { get; set; }

        [StringLength(40)]
        public string BillingState { get; set; }

        [StringLength(40)]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        public string BillingPostalCode { get; set; }

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
        public Customer Customer { get; set; }
    }

    public class InvoiceWithInvoiceLine : InvoiceAdd
    {
        public InvoiceWithInvoiceLine()
        {
            public List<InvoiceLine> InvoiceLines = new List<InvoiceLine>();
        }
        public IEnumerable<InvoiceLine> InvoiceLines { get; set; }
    }
}