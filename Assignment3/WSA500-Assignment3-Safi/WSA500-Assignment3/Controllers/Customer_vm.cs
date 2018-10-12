using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WSA500_Assignment3.Controllers
{
    public class CustomerAdd
    {
        public CustomerAdd() {  }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(80)]
        public string Company { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        public int? SupportRepId { get; set; }
    }

    public class CustomerBase : CustomerAdd
    {
        public int CustomerId { get; set; }
    }

    public class CustomerWithEmployee : CustomerBase
    {
        public EmployeeBase Employee { get; set; }
    }

    public class CustomerWithInvoice : CustomerBase
    {
        public CustomerWithInvoice()
        {
            Invoices = new List<InvoiceBase>();
        }
        public IEnumerable<InvoiceBase> Invoices { get; set; }
     }
    
} // end of namkespace
