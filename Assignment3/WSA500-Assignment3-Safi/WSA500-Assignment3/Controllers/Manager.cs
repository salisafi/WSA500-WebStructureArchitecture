using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
// using AllHttpMethods.Models;
using System.Web.Http;
using System.Net.Http;
using WSA500_Assignment3.Models;

// using System.Data.Entity.Migrations;


namespace WSA500_Assignment3.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper components
        MapperConfiguration config;
        public IMapper mapper;

        public Manager()
        {
            // Configure AutoMapper...
            config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();

                // Invoice entity mappers
                 cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
                 cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithCustomerInfo>();


                // InvoiceLine entity mappers
                 cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineBase>();

                // Employee entity mappers
                cfg.CreateMap<Models.Employee, Controllers.EmployeeBase>();
                cfg.CreateMap<Models.Employee, Controllers.EmployeeWithMediaInfo>();
  //              cfg.CreateMap<Models.Employee, Controllers.EmployeeWithPhoto>();

             //   cfg.CreateMap<Controllers.EmployeeWithMedia, Controllers.EmployeeWithMediaInfo>();

                cfg.CreateMap<Controllers.EmployeeAdd, Models.Employee>();
            });

            mapper = config.CreateMapper();

            // Data-handling configuration

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>


        // Method templates, used by the ExampleController class

        public IEnumerable<InvoiceWithCustomerInfo> InvoiceGetAll()
        {
            return mapper.Map<IEnumerable<InvoiceWithCustomerInfo>>(ds.Invoices.Include("Customers.Employees"));
        }

        public InvoiceBase InvoiceGetById(int id)
        {
            var o = ds.Invoices.Include("Customers.Employees").Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(e => e.InvoiceId == id);
            // var o = ds.Invoices.Include(InvoiceLines).Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<InvoiceBase>(o);
        }

        public string ExampleAdd(string newItem)
        {
            return $"new item {newItem} was added";
        }

        public string ExampleEditSomething(string editedItem)
        {
            return $"item was edited with {editedItem}";
        }

        public bool ExampleDelete(int id)
        {
            return true;
        }


        public IEnumerable<InvoiceLineBase> InvoiceLineGetAll()
        {
            return mapper.Map<IEnumerable<InvoiceLineBase>>(ds.InvoiceLines);
        }


        public IEnumerable<EmployeeWithMediaInfo> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<EmployeeWithMediaInfo>>(ds.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName));
        }

        public EmployeeBase EmployeeGetById(int id)
        {
            var o = ds.Employees.Find(id);

            return (o == null) ? null :  mapper.Map<EmployeeBase>(o);
        }

        // Add employee
        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            // Attempt to add the object
            var addedItem = ds.Employees.Add(mapper.Map<Employee>(newItem));
            ds.SaveChanges();

            // Return the result, or null if there was an error
            return (addedItem == null) ? null : mapper.Map<EmployeeBase>(addedItem);
        }

 /*       public EmployeePhoto EmployeePhotoGetById(int id)
        {
            var o = ds.Employees.Find(id);

            return (o == null) ? null : mapper.Map<EmployeePhoto>(o);
        }
*/
        public bool EmployeeSetPhoto(int id, string contentType, byte[] photo)
        {
            // Notice the return type of this method - bool
            // This is an incremental attempt at improving the command pattern
            // In the controller, we could use the return value, if we wished

            // Ensure that we can continue
            if (string.IsNullOrEmpty(contentType) | photo == null) { return false; }

            // Attempt to find the matching object
            var storedItem = ds.Employees.Find(id);

            // Ensure that we can continue
            if (storedItem == null) { return false; }

            // Save the photo
     //       storedItem.PhotoContentType = contentType;
     //       storedItem.Photo = photo;

            // Attempt to save changes
            // Do you understand the following? If not, ask
            return (ds.SaveChanges() > 0) ? true : false;
        }

        // Programmatically-generated objects

        // Can do this in one method, or in several
        // Call the method(s) from a controller method

        public bool LoadData()
        {
            /*
            // Return immediately if there's existing data
            if (ds.[entity collection].Courses.Count() > 0) { return false; }

            // Otherwise, add objects...

            ds.[entity collection].Add(new [whatever] { Property1 = "value", Property2 = "value" });
            */

            return ds.SaveChanges() > 0 ? true : false;
        }

    }
}