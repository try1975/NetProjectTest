using System;
using Nancy.Hosting.Self;

namespace Persons.Service
{
    public class PersonService
    {
        private readonly NancyHost _host;

        public PersonService(string url)
        {
            var hostConfigs = new HostConfiguration
            {
                UrlReservations = new UrlReservations() {CreateAutomatically = true}
            };
            _host = new NancyHost(hostConfigs, new Uri(url));
        }

        public void Start()
        {
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
        }

    }
}