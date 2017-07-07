using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace SimpleHttpServer.Arguments
{
    class ArgumentsModel
    {
        public ArgumentsModel()
        {
            IP = IPAddress.Parse("127.0.0.1");
            Port = 80;
        }

        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public string WebsiteDirectory { get; set; }

    }
}
