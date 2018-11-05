using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment4_WSA500.Controllers
{

    // Invoice resource models, Add, Base 

    /// <summary>
    /// Add a new Invoice class to the view model
    /// </summary>
    public class InvoiceAdd
    {
        public InvoiceAdd()
        {
            InvoiceDate = DateTime.Now;
        }

        /// <summary>
        /// Date of Invoice
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Billing Address of Invoice
        /// </summary>
        [StringLength(70)]
        public string BillingAddress { get; set; }

        /// <summary>
        /// Billing City of Invoice
        /// </summary>
        [StringLength(40)]
        public string BillingCity { get; set; }

        /// <summary>
        /// Billing City of Invoice
        /// </summary>
        [StringLength(40)]
        public string BillingState { get; set; }

        /// <summary>
        /// Billing Country of Invoice
        /// </summary>
        [StringLength(40)]
        public string BillingCountry { get; set; }

        /// <summary>
        /// Billing PostalCode of Invoice
        /// </summary>
        [StringLength(10)]
        public string BillingPostalCode { get; set; }

        /// <summary>
        /// Total Amount of Invoice
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal Total { get; set; }

    }

    /// <summary>
    /// Invoice view model class with Id and all other properties
    /// </summary>
    public class InvoiceBase : InvoiceAdd
    {
        // Notice, again, the [Key] attribute

        /// <summary>
        /// Unique Invoice identifier
        /// </summary>
        [Key]
        public int InvoiceId { get; set; }
    }

    /// <summary>
    /// Invoice view model class with InvoiceLines
    /// </summary>
    // Attention 05 - Inheritance works in this simple situation
    public class InvoiceWithInvoiceLines : InvoiceBase
    {
        public InvoiceWithInvoiceLines()
        {
            // Attention 06 - When there is a collection, initialize it in the default constructor
            InvoiceLines = new List<InvoiceLineBase>();
        }

        // Attention 07 - Notice IEnumerable (not ICollection); do NOT use "virtual" keyword
        public IEnumerable<InvoiceLineBase> InvoiceLines { get; set; }
    }

    /// <summary>
    /// Invoice view model class with Customer first name, customer last name information
    /// </summary>
    public class InvoiceWithCustomerInfo : InvoiceBase
    {
        [Required, StringLength(40)]
        public string CustomerFirstName { get; set; }

        [Required, StringLength(20)]
        public string CustomerLastName { get; set; }

        [StringLength(80)]
        public string CustomerCompany { get; set; }

    } 
}