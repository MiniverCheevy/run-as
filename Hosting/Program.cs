﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Host.HttpListener;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Nancy;
using Nancy.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Voodoo;

namespace Hosting
{
    public class Program
    {
        static string url = "http://localhost:9999";

        public static void Main(string[] args)
        {
            try
            {
                using (WebApp.Start<Startup>(url))
                {
                    IoNic.ShellExecute(string.Format("{0}/index.html", url));
                    Console.WriteLine("Server running on {0}", url);
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is HttpListenerException)
                {
                    Console.WriteLine(
                        String.Format(
                            "Failed to start web server.  You may wish to change the port in the configuration file: {0}",
                            ex.InnerException.Message));
                    Console.ReadLine();
                }
                else
                {
                    throw;
                }
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {         
            var config = new HubConfiguration { EnableDetailedErrors = true };

            //app.MapSignalR(config);
            app.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem("Web"),
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = true
            });
            //app.UseNancy(opt =>
            //{
            //    // If nancy cannot find a handler for the route then pass the request through the pipleine
            //    opt.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            //});
            var apiConfig = new HttpConfiguration();
            apiConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var jsonSettings = apiConfig.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseWebApi(apiConfig);
        }

    }
}



//// Static Files are stored at /Web from the solution. JQuery and SignalR clients are stored there
//var exeFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
//var webFolder = Path.Combine(exeFolder, "Web");

//app.UseStaticFiles("/Web");


//}

public class MyHub : Hub
{
    // Very simple echo chat hub
    public void Send(string message)
    {
        Clients.All.addMessage(message.Trim());
    }
}

// Very simple Nancy Module
public class HomeModule : NancyModule
{
    public HomeModule()
    {
        Get["/"] = _ => "Hello from Owin!";
    }
}

