using System;
using System.Configuration;
using Nancy.Hosting.Self;
using Serilog;

namespace Persons.Service
{
    class Program
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger()
                ;

            Log.Logger.Information("Start program info message");

            //Console.ReadKey();

            //IPersonFactory personFactory = new PersonFactory();
            //IPerson person = personFactory.CreatePerson("Test", new DateTime(1975, 4, 26));
            //Console.WriteLine(person != null ? $"Age = {person.Age}" : "Person object not created");
            //Console.ReadKey();
            //return;

            //HostFactory.Run(x => 
            //{ 
            //    x.Service<ProductService>(s => 
            //    { 
            //        s.SetServiceName("ProductService Example"); 
            //        s.ConstructUsing(name => new ProductService()); 
            //        s.WhenStarted(svc => svc.Start()); 
            //        s.WhenStopped(svc => svc.Stop()); 
            //    }); 

            //    x.RunAsLocalSystem(); 
            //    x.SetDescription("ProductService WebAPI selfhosting Windows Service Example"); 
            //    x.SetDisplayName("ProductService Example"); 
            //    x.SetServiceName("ProductService"); 
            //}); 

            var port = ConfigurationManager.AppSettings["ServicePort"];
            var url = $"http://localhost:{port}";
            var hostConfigs = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            //StaticConfiguration.DisableErrorTraces = false;

            using (var host = new NancyHost(hostConfigs, new Uri(url)))
            {
                host.Start();
                Console.WriteLine($"Running on {url}");
                Console.ReadKey();
                host.Stop();
            }
        }
    }
}
