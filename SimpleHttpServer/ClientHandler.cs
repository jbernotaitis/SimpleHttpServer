using Microsoft.Win32;
using SimpleHttpServer.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleHttpServer
{
    class ClientHandler
    {
        private string _websiteDirectory;
        private NetworkStream _networkStream;
        private MemoryStream _memoryStream = new MemoryStream();
        private StreamReader _streamReader;
        private TextWriter _writer;

        public void StartClient(Socket clientSocket, string websiteDirectory, TextWriter logWriter)
        {
            _websiteDirectory = websiteDirectory;
            _writer = logWriter;
            _streamReader = new StreamReader(_memoryStream);
            _networkStream = new NetworkStream(clientSocket, true);
            Thread clientThread = new Thread(Run);
            clientThread.Start();
        }

        private void Run()
        {
            byte[] buffer = new byte[4096];
            
            while (true)
            {
                int bytesRead = _networkStream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                    return;

                _memoryStream.Seek(0, SeekOrigin.End);
                _memoryStream.Write(buffer, 0, bytesRead);
                bool done = ProcessHeader();
                if (done)
                    break;
            }
        }

        private bool ProcessHeader()
        {
            while (true)
            {
                _memoryStream.Seek(0, SeekOrigin.Begin);
                string line = _streamReader.ReadLine();
                if (line == null)
                    break;

                if (line.ToUpperInvariant().StartsWith("GET "))
                {
                    string file = line.Split(' ')[1].TrimStart('/');

                    if (string.IsNullOrWhiteSpace(file))
                        file = "index.html";

                    _writer?.WriteLine("Request: GET " + file);
                    string filepath = $"{_websiteDirectory}/{file}".TrimStart('/');
                    SendFile(filepath);
                    return true;
                }

            }
            return false;
        }

        private void SendFile(string path)
        {
            IHttpResponse response = null;
            try
            {
                if (File.Exists(path))
                {
                    response = new FileHttpResponse(path);
                }
                else
                {
                    response = new NotFoundHttpResponse();
                }
            }
            catch (Exception exception)
            {
                response = new InternalServerErrorHttpResponse(exception.Message);
            }
            response.SendResponse(_networkStream);
        }

    }
}
