using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WSA500_Assignment3.Controllers
{
    public class EmployeeAdd
    {
        public EmployeeAdd()
        {
            // Employee1 = new HashSet<Employee>();
        }

        [StringLength(70)]
        public string Address { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(60)]
        public string Email { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        public DateTime? HireDate { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        public int? ReportsTo { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(30)]
        public string Title { get; set; }


        //    public ICollection<Employee> Employee1 { get; set; }

        //    public Employee Employee2 { get; set; }
    }

    public class EmployeeBase : EmployeeAdd
    {
        public int EmployeeId { get; set; }


        [Display(Name = "Employee Photo")]
        public string PhotoUrl
        {
            get
            {
                return $"/employee/{EmployeeId}";
            }
        }
    }

    public class EmployeePhoto
    {
        public int Id { get; set; }
        public string PhotoContentType { get; set; }
        public byte[] Photo { get; set; }
    }

    public class EmployeeWithCustomer : EmployeeBase
    {
        public EmployeeWithCustomer()
        {
            Customers = new List<CustomerBase>();
        }
        public IEnumerable<CustomerBase> Customers { get; set; }
    }


} // end of namespace