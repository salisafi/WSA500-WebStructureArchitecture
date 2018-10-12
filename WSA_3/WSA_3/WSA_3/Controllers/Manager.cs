using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using WSA_3.Models;

namespace WSA_3.Controllers
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
            // If necessary, add your own code here

            // Configure AutoMapper...
            config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();

                cfg.CreateMap<Models.Invoice, Controllers.InvoicewithCustomer>();
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithInvoiceLine>();
                cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceWithTrack>();
                cfg.CreateMap<Models.Customer, Controllers.CustomerBase>();
                cfg.CreateMap<Models.Employee, Controllers.EmployeeBase>();

                cfg.CreateMap<Models.Employee, Controllers.EmployeeWithAssociations>();
                cfg.CreateMap<Controllers.EmployeeWithAssociations, Controllers.EmployeeWithAssociations2>();
                cfg.CreateMap<Controllers.EmployeeAdd, Models.Employee>();


                cfg.CreateMap<Models.Employee, Controllers.EmployeeWithMedia>();
                cfg.CreateMap<Models.Employee, Controllers.EmployeeWithMediaInfo>();


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

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()


        // Method templates, used by the ExampleController class

        //Get All Methods
        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            var c = ds.Invoices.Include("Customer.Employee");
            return mapper.Map<IEnumerable<InvoicewithCustomer>>(c);
        }

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            var c = ds.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
            return mapper.Map<IEnumerable<EmployeeBase>>(c);
        }

        //Get One Methods
        public InvoiceWithInvoiceLine GetInvoice(int id)
        {
            var c = ds.Invoices.Include("Customer.Employee").Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(e => e.InvoiceId == id);
            return mapper.Map<InvoiceWithInvoiceLine>(c);
        }

        public EmployeeWithAssociations2 GetEmployee(int id)
        {

            var c = ds.Employees.Include("Employee1").Include("Employee2").Include("Customers").SingleOrDefault(p => p.EmployeeId == id);

            if (c == null)
                return null;

            var a = mapper.Map<EmployeeWithAssociations>(c);

            EmployeeWithAssociations2 result = new EmployeeWithAssociations2();


            result = mapper.Map<EmployeeWithAssociations2>(a);

            result.EmployeeSupervised = a.Employee1;

            result.SupervisorFirstName = a.Employee2FirstName;

            result.SupervisorLastName = a.Employee2LastName;

            return (result == null) ? null : result;

        }
        //Add new Methods

        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            if (newItem == null) { return null; }
            else
            {
                Employee addedItem = mapper.Map<Employee>(newItem);
                ds.Employees.Add(addedItem);
                ds.SaveChanges();

                //Find Manager
                var Manager = ds.Employees.Find(newItem.ReportsTo);
                if (Manager == null)
                {
                    mapper.Map<EmployeeBase>(addedItem);
                }


                // Make the changes, save, and exit
                //adds Employee to Manager's List
                Manager.Employee1.Add(addedItem);
                //adds Manager to employee object
                addedItem.Employee2 = Manager;
                //adds managerId to Employee object
                addedItem.ReportsTo = Manager.EmployeeId;
                ds.SaveChanges();

                return mapper.Map<EmployeeBase>(addedItem);
            }
        }

        public void SetEmployeeSupervisor(EmployeeSupervisor Item)
        {   // Get a reference to the employee
            var employee = ds.Employees.Find(Item.Employee);
            if (employee == null) { return; }

            // Get a reference to the supervisor
            var supervisor = ds.Employees.Find(Item.Supervisor);
            if (supervisor == null) { return; }

            //find old supervisor
            var prevSupervisor = ds.Employees.Find(employee.ReportsTo);
            if (prevSupervisor != null) { prevSupervisor.Employee1.Remove(employee); }
            // Make the changes, save, and exit
            supervisor.Employee1.Add(employee);
            employee.Employee2 = supervisor;
            employee.ReportsTo = supervisor.EmployeeId;
            ds.SaveChanges();
        }

        public EmployeeWithMedia EmployeeGetByIdWithMedia(int id)
        {
            var o = ds.Employees.Find(id);

            return (o == null) ? null : mapper.Map<EmployeeWithMedia>(o);
        }

        public bool EmployeeSetPhoto(int id, string contentType, byte[] photo)
        {

            if (string.IsNullOrEmpty(contentType) | photo == null) { return false; }

            var storedItem = ds.Employees.Find(id);

            if (storedItem == null) { return false; }

            storedItem.ContentType = contentType;
            storedItem.Photo = photo;

            return (ds.SaveChanges() > 0) ? true : false;
        }

        public void ClearEmployeeSupervisor(EmployeeSupervisor item)
        {
            // Get a reference to the employee
            var employee = ds.Employees.Find(item.Employee);
            if (employee == null) { return; }

            // Get a reference to the supervisor
            var supervisor = ds.Employees.Find(item.Supervisor);
            if (supervisor == null) { return; }

            // Make the changes, save, and exit
            if (employee.ReportsTo == supervisor.EmployeeId)
            {
                employee.ReportsTo = null;
                employee.Employee2 = null;
                ds.SaveChanges();
            }
        }


        public EmployeeBase EmployeeEditNames(EmployeeEditNames editedItem)
        {
            // Ensure that we can continue
            if (editedItem == null)
            {
                return null;
            }

            // Attempt to fetch the underlying object
            var storedItem = ds.Employees.Find(editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                // Fetch the object from the data store - ds.Entry(storedItem)
                // Get its current values collection - .CurrentValues
                // Set those to the edited values - .SetValues(editedItem)
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                // The SetValues() method ignores missing properties and navigation properties
                ds.SaveChanges();

                return mapper.Map<EmployeeBase>(storedItem);
            }
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