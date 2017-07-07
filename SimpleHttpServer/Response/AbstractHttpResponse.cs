using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleHttpServer.Response
{
    abstract class AbstractHttpResponse : IHttpResponse
    {
        public void SendResponse(NetworkStream networkStream)
        {
            networkStream.Write(Header, 0, Header.Length);
            networkStream.Write(Content, 0, Content.Length);
            networkStream.Flush();
            networkStream.Dispose();
        }

        protected abstract string ResponseCode { get; }

        protected abstract byte[] Content { get; }

        protected abstract string Extension { get; }

        private byte[] Header
        {
            get
            {
                string header = $"HTTP/1.1 {ResponseCode}\r\nContent-Length: {Content.Length}\r\nContent-Type: {GetContentType(Extension)}\r\nKeep-Alive: Close\r\n\r\n";

                return Encoding.ASCII.GetBytes(header);
            }
        }

        private string GetContentType(string extension)
        {
            if (Regex.IsMatch(extension, "^[a-z0-9]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled))
            {
                return Registry.GetValue(@"HKEY_CLASSES_ROOT\." + extension, "Content Type", null) as string
                                        ?? "application/octet-stream";
            }
            return "application/octet-stream";

        }


    }

}

