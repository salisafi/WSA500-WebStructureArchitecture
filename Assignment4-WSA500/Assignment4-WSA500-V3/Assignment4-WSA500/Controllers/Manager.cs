using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment4_WSA500.Models;

namespace Assignment4_WSA500.Controllers
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

                 cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
                 cfg.CreateMap<Controllers.InvoiceAdd, Models.Invoice>();


                 cfg.CreateMap<Models.Track, Controllers.TrackBase>();


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

        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            return mapper.Map<IEnumerable<InvoiceBase>>(ds.Invoices);
        }

        public InvoiceBase InvoiceGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Invoices.Find(id);

            return (o == null) ? null : mapper.Map<InvoiceBase>(o);

            // throw new NotImplementedException();
        }

        public InvoiceBase InvoiceAdd(InvoiceAdd newItem)
        {
            // To add a Invoice 
            var a = ds.Invoices;

            if (a == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the object
                var addedItem = ds.Invoices.Add(mapper.Map<Invoice>(newItem));
                ds.SaveChanges();

                // Return the result, or null if there was an error
                return (addedItem == null) ? null : mapper.Map<InvoiceBase>(addedItem);
            }
        }

        public void InvoiceDelete(int id)
        {
            // Attempt to fetch the existing item
            var storedItem = ds.Invoices.Find(id);

            // Interim coding strategy...

            if (storedItem == null)
            {
                // Throw an exception, and you will learn how soon
            }
            else
            {
                try
                {
                    // If this fails, throw an exception (as above)
                    // This implementation just prevents an error from bubbling up
                    ds.Invoices.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
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




        public IEnumerable<TrackBase> TracksGetAll()
        {
            return mapper.Map<IEnumerable<TrackBase>>(ds.Tracks);
        }

        public TrackBase TrackGetOne(int id)
        {
            // Attempt to fetch the object
            var o = ds.Tracks.Find(id);

            return (o == null) ? null : mapper.Map<TrackBase>(o);

            // throw new NotImplementedException();
        }

    }
}