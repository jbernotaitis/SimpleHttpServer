using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Response
{
    class FileHttpResponse : AbstractHttpResponse
    {
        private string _path;

        public FileHttpResponse(string path)
        {
            _path = path;
        }

        protected override byte[] Content
        {
            get
            {
                return File.ReadAllBytes(_path);
            }
        }

        protected override string Extension
        {
            get
            {
                return Path.GetExtension(_path).TrimStart(".".ToCharArray());
            }
        }

        protected override string ResponseCode
        {
            get
            {
                return "200 OK";
            }
        }
    }
}
