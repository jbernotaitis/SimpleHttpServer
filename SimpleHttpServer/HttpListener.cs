using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer
{
    class HttpListener
    {
        private string _websiteDirectory;
        private int _port;
        private TextWriter _writer;
        private IPAddress _ipAddress;

        public HttpListener(TextWriter logWriter, 
                    string websiteDirectory, 
                    IPAddress ipAddress, 
                    int port)
        {
            _port = port;
            _writer = logWriter;
            _ipAddress = ipAddress;
            _websiteDirectory = websiteDirectory;
        }

        public void ListenLoop()
        {
            TcpListener serverSocket = new TcpListener(_ipAddress, _port); ;
            _writer?.WriteLine(" >> Server Started"); serverSocket.Start();

            Socket clientSocket = null;   
            
            while (true)
            {
                try
                {
                    clientSocket = serverSocket.AcceptSocket();
                    _writer?.WriteLine($" >> Client accepted");
                    ClientHandler client = new ClientHandler();
                    client.StartClient(clientSocket, _websiteDirectory, _writer);
                }
                catch (Exception ex)
                {
                    _writer?.WriteLine(ex.ToString());
                }
            }
        }


    }
}
