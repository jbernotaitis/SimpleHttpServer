using SimpleHttpServer.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ArgumentsModel arguments = ArgumentsParser.Parse(args);

                HttpListener listener = new HttpListener(Console.Out, 
                                                arguments.WebsiteDirectory, 
                                                arguments.IP, 
                                                arguments.Port);
                listener.ListenLoop();
            }
            catch (Exception exc)
            {
                Console.WriteLine(" >>Error: " + exc.Message);
                Console.WriteLine(" >>Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
