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

                // ##########################       Invoice         ##################################
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithInvoiceLines>();
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithCustomerInfo>();
                cfg.CreateMap<Controllers.InvoiceAdd, Models.Invoice>();

                // ##########################       Track         ##################################
                cfg.CreateMap<Models.Track, Controllers.TrackBase>();

                // ##########################       Album         ##################################
                cfg.CreateMap<Models.Album, Controllers.AlbumBase>();
                cfg.CreateMap<Models.Album, Controllers.AlbumByIdWithTracks>();
                cfg.CreateMap<Controllers.AlbumAdd, Models.Album>();
                cfg.CreateMap<Models.Album, Controllers.AlbumWithArtist>();


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

        // ##########################       Invoice         ##################################
        // Invoice get all  - First 10 object
        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            return mapper.Map<IEnumerable<InvoiceBase>>(ds.Invoices.Take(20));
        }

        // Invoice get all  - First 10 object
        public IEnumerable<InvoiceLineBase> InvoiceLineGetAll()
        {
            return mapper.Map<IEnumerable<InvoiceLineBase>>(ds.InvoiceLines.Take(20));
        }

          // Invoice get all - Customer first name, Customer last name
            public IEnumerable<InvoiceWithCustomerInfo> InvoiceWithCustomerInfo()
            {
            //return mapper.Map<IEnumerable<InvoiceWithCustomerInfo>>(ds.Invoices.Include("Customers").OrderBy(p => p.InvoiceId));
            // Attention 17 - Fetch players, notice that we "include" the team object
            var c = ds.Invoices.Include("Customer").Take(20);

            // The mapper returns the whole team object with the results
            return mapper.Map<IEnumerable<InvoiceWithCustomerInfo>>(c);
        }  

        // Invoice get one with id 
        public InvoiceBase InvoiceGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Invoices.Find(id);

            return (o == null) ? null : mapper.Map<InvoiceBase>(o);

            // throw new NotImplementedException();
        }

        // Attention 16 - Employee get one with associated objects, new

        public InvoiceWithInvoiceLines InvoiceGetByIdWithInvoiceLines(int id)
        {
            // Attempt to fetch the object

            // Attention 17 - Notice that you must use SingleOrDefault() - cannot use Find()
  
            var o = ds.Invoices.Include("InvoiceLines")
                .SingleOrDefault(e => e.InvoiceId == id);

            return (o == null) ? null : mapper.Map<InvoiceWithInvoiceLines>(o);
        }

        // Add one Invoice 
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

        // Delete one Invoice
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
            return ds.SaveChanges() > 0 ? true : false;
        }

        // ##########################       Track         ##################################
        // Track get all  
        public IEnumerable<TrackBase> TracksGetAll()
        {
            return mapper.Map<IEnumerable<TrackBase>>(ds.Tracks);
        }

        // Track get one with id 
        public TrackBase TrackGetOne(int id)
        {
            // Attempt to fetch the object
            var o = ds.Tracks.Find(id);

            return (o == null) ? null : mapper.Map<TrackBase>(o);

            // throw new NotImplementedException();
        }

        // Add a Track
        public TrackBase TrackAdd(TrackAdd newItem)
        {
            // To add a Track, 
            // We must validate that Track first by attempting to fetch it
            // If successful, then we can continue

            var a = ds.Tracks;

            if (a == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the object
                var addedItem = ds.Tracks.Add(mapper.Map<Track>(newItem));

                ds.SaveChanges();

                // Return the result, or null if there was an error
                return (addedItem == null) ? null : mapper.Map<TrackBase>(addedItem);
            }
        }

        // Delete a Track with specific id
        public void TrackDelete(int id)
        {
            // Attempt to fetch the existing item
            var storedItem = ds.Tracks.Find(id);

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
                    ds.Tracks.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        // ##########################       Albums         ##################################     
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<AlbumBase>>(ds.Albums);
        }

        public AlbumBase AlbumGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Albums.Include("Tracks").SingleOrDefault(e => e.AlbumId == id);

            return (o == null) ? null : mapper.Map<AlbumBase>(o);
        }

        public AlbumByIdWithTracks AlbumGetByIdWithTracks(int id)
        {
            // Attempt to fetch the object

            // Attention 17 - Notice that you must use SingleOrDefault() - cannot use Find()

            var o = ds.Albums.Include("Tracks").SingleOrDefault(e => e.AlbumId == id);

            return (o == null) ? null : mapper.Map<AlbumByIdWithTracks>(o);
        }


        public AlbumWithArtist AlbumGetByIdWithArtist(int id)
        {
            var o = ds.Albums.Include("Artist").SingleOrDefault(e => e.AlbumId == id);

            return (o == null) ? null : mapper.Map<AlbumWithArtist>(o);
        }


        public AlbumBase AlbumAdd(AlbumAdd newItem)
        {
            var addedAlbum = ds.Albums.Add(mapper.Map<Album>(newItem));
            ds.SaveChanges();

            // Return the result, or null if there was an error
            return (addedAlbum == null) ? null : mapper.Map<AlbumBase>(addedAlbum);
        }

        public AlbumBase EditAlbum(AlbumEdit editedItem)
        {

            // Ensure that we can continue
            if (editedItem == null) { return null; }

            // Attempt to fetch the object
            var currentAlbum = ds.Albums.Find(editedItem.AlbumId);

            if (currentAlbum == null)
            {
                return null;
            }
            else
            {
                // Fetch the object from the data store - ds.Entry(storedItem)
                // Get its current values collection - .CurrentValues
                // Set those to the edited values - .SetValues(editedItem)
                ds.Entry(currentAlbum).CurrentValues.SetValues(editedItem);
                // The SetValues() method ignores missing properties and navigation properties
                ds.SaveChanges();

                return mapper.Map<AlbumEdit>(currentAlbum);
            }
        }

        public void AlbumeDelete(int id)
        {
            var storedItem = ds.Albums.Find(id);

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
                    ds.Albums.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }



    }
}