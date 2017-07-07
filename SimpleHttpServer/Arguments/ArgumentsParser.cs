using System;
using System.IO;
using System.Net;


namespace SimpleHttpServer.Arguments
{
    static class ArgumentsParser
    {
        public static ArgumentsModel Parse(string[] args)
        {
            ArgumentsModel arguments = new ArgumentsModel();

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                switch (arg.ToLower())
                {
                    case "-port":
                        if (IsLastArgument(i, args.Length))
                            throw new ArgumentException("port", "-port");
                        arguments.Port = ParsePort(args[++i]);

                        break;
                    case "-ip":
                        if (IsLastArgument(i, args.Length))
                            throw new ArgumentException("ip address", "-ip");
                        arguments.IP = ParseIPAddress(args[++i]);

                        break;
                    case "-dir":
                        if (IsLastArgument(i, args.Length))
                            throw new ArgumentException("website directory", "-dir");
                        arguments.WebsiteDirectory = ParseWebSiteDirectory(args[++i]);
                        break;
                }
            }
            return arguments;
        }

        private static int ParsePort(string port)
        {
            int result;
            if (Int32.TryParse(port, out result))
            {
                return result;
            }

            throw new ArgumentException("port", "-port");
        }

        private static IPAddress ParseIPAddress(string ipAddress)
        {
            IPAddress result;
            if (IPAddress.TryParse(ipAddress, out result))
            {
                return result;
            }
            throw new ArgumentException("ip address", "-ip");
        }

        private static string ParseWebSiteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return path;
            }
            throw new ArgumentException("website directory", "-dir");
        }

        private static bool IsLastArgument(int index, int length)
        {
            return index + 1 == length;
        }

    }
}
