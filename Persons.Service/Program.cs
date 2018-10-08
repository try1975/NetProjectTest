using System;
using System.Configuration;
using Serilog;
using Topshelf;

namespace Persons.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = ConfigurationManager.AppSettings["ServicePort"];
            var url = $"http://localhost:{port}";

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger()
                ;

            Log.Logger.Information($"Start {nameof(PersonService)} at {url}");
            var paramStartAsService = ConfigurationManager.AppSettings["StartAsService"];
            var startAsService = (!string.IsNullOrWhiteSpace(paramStartAsService) && paramStartAsService.Trim().ToUpper().Equals("TRUE"));
            if (startAsService)
            {
                HostFactory.Run(x =>
                {
                    x.Service<PersonService>(s =>
                    {
                        s.ConstructUsing(name => new PersonService(url));
                        s.WhenStarted(svc => svc.Start());
                        s.WhenStopped(svc => svc.Stop());
                    });
                    x.RunAsLocalSystem();
                    x.SetDescription("PersonService REST API selfhosting Windows Service Example");
                    x.SetDisplayName("PersonService Example");
                    x.SetServiceName("PersonService");
                });
            }
            else
            {

               var personService = new PersonService(url);
                personService.Start();
                Console.WriteLine("Press any key to stop");
                Console.ReadKey();
                personService.Stop();
            }
        }
    }
}
