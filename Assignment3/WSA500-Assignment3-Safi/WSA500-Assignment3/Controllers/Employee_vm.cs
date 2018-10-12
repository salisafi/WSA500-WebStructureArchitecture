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

    public class EmployeeWithMediaInfo : EmployeeBase
    {
        // Notice the name of this property - PhotoLength
        // In the source object - Employee - we do NOT have a matching property
        // However, its value does get configured correctly by AutoMapper
        // when mapping an Employee object to a EmployeeWithMediaInfo object
        // Why?
        // The Photo *property* is an object that has a "Length" property
        // Therefore, we can use AutoMapper's magic to set the value
        public int PhotoLength { get; set; }
        // public int Id { get; set; }
        public string ContentType { get; set; }
    }

    public class EmployeeWithMedia : EmployeeWithMediaInfo
    {
        // Attention 02 - Resource model class - deliver the media item bytes with this class
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


    public class EmployeeWithAssociations : EmployeeBase
    {
        public EmployeeWithAssociations()
        {
            Employee1 = new List<EmployeeBase>();
            Customers = new List<CustomerBase>();
        }

        public string Employee2FirstName { get; set; }

        public string Employee2LastName { get; set; }

        public IEnumerable<EmployeeBase> Employee1 { get; set; }

        public IEnumerable<CustomerBase> Customers { get; set; }
    }

    public class EmployeeWithAssociations2 : EmployeeBase
    {
        public EmployeeWithAssociations2()
        {
            EmployeeSupervised = new List<EmployeeBase>();
            Customers = new List<CustomerBase>();
        }

        public string SupervisorFirstName { get; set; }

        public string SupervisorLastName { get; set; }

        public IEnumerable<EmployeeBase> EmployeeSupervised { get; set; }

        public IEnumerable<CustomerBase> Customers { get; set; }
    }

    public class EmployeeSupervisor
    {
        public int Employee { get; set; }

        public int Supervisor { get; set; }
    }

    public class SupervisorEmployees
    {
        public SupervisorEmployees()
        {
            Employees = new List<int>();
        }

        public int Supervisor { get; set; }

        public List<int> Employees { get; set; }
    }

    public class EmployeeEditNames
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }
    }


} // end of namespace