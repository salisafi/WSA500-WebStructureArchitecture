using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment4_WSA500.Controllers
{
    public class InvoicesController : Controller
    {
        // Reference to the manager object
        private Manager m = new Manager();

        // GET: Invoices
        public ActionResult Index()
        {
            return View(m.InvoiceGetAll());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.InvoiceGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

    }
}
