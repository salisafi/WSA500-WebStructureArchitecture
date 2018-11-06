using Assignment4_WSA500.Areas.HelpPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.IO;
// added...
using System.Web.Http.ExceptionHandling;

namespace Assignment4_WSA500
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services


            // Attention - Help page configuration; must also change the project's properties
                config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/bin/Assignment4-WSA500.xml")));
           
            // Web API routes
            config.MapHttpAttributeRoutes();


            // Add ByteFormatter to the pipeline
            config.Formatters.Add(new ServiceLayer.ByteFormatter());

            // Add HRFormatter to the pipeline
            config.Formatters.Add(new ServiceLayer.HRFormatterICT());

            // Add HandleError to the pipeline
            config.Services.Replace(typeof(IExceptionHandler), new ServiceLayer.HandleError());

            // Attention 11 - Configure a default controller 
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
