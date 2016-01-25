using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using Voodoo.Validation.Infrastructure;

namespace Hosting
{
    public class Program
    {
        static string url = "http://localhost:{0}";

        public static void Main(string[] args)
        {
            try
            {
                if (ConfigurationManager.AppSettings["debug"].To<bool>())
                    System.Diagnostics.Debugger.Launch();

                var options = new Microsoft.Owin.Hosting.StartOptions();
                var port = ConfigurationManager.AppSettings["port"] ?? "9999";
                url = string.Format(url, port);
                options.Urls.Add(url);
                options.ServerFactory = "Microsoft.Owin.Host.HttpListener";
                options.AppStartup = "Hosting.Startup";

                VoodooGlobalConfiguration.RegisterValidator(new Voodoo.Validation.Infrastructure.DataAnnotationsValidatorWithFirstErrorAsMessage());
                using (WebApp.Start<Startup>(options))
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
        private string findTheWebFolder()
        {
            var rootDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            const string webDirectory = "web";           
            var directory = Path.Combine(rootDirectory, webDirectory);
            var count = 0;

            while (!Directory.Exists(directory))
            {
               
                if (directory.EndsWith(@"ra\web"))
                {
                    directory = Path.Combine(rootDirectory, @"RunAsWrapper.Core\web");
                }
                else
                {
                    
                    rootDirectory = Directory.GetParent(rootDirectory).ToString();
                    if (count > 5)
                        throw new Exception("Cannot find web folder");
                    directory = Path.Combine(rootDirectory,  webDirectory);
                }               
                count++;                
            }
            return directory;
        }

        public void Configuration(IAppBuilder app)
        {
           
            
            // options.ServerFactory = "Microsoft.Owin.Host.HttpListener"
            //app.MapSignalR(config);
            app.UseFileServer(new FileServerOptions()
            {
                FileSystem = new PhysicalFileSystem(findTheWebFolder()),
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = true
            });
            //app.UseNancy(opt =>
            //{
            //    // If nancy cannot find a handler for the route then pass the request through the pipleine
            //    opt.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            //});

            //app.Map("/signalr", map =>
            //{
            //    map.UseCors(CorsOptions.AllowAll);

            //    var hubConfiguration = new HubConfiguration
            //    {
            //        EnableDetailedErrors = true,
            //        EnableJSONP = true
            //    };

            //    map.RunSignalR(hubConfiguration);
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
            VoodooGlobalConfiguration.RegisterValidator(new DataAnnotationsValidatorWithGenericMessage());
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

