using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Response
{
    class NotFoundHttpResponse : AbstractHttpResponse
    {
        protected override byte[] Content
        {
            get
            {
                return Encoding.ASCII.GetBytes("<html><body><h1>404 Page Not Found</h1></body></html>");
            }
        }

        protected override string Extension
        {
            get
            {
                return "html";
            }
        }

        protected override string ResponseCode
        {
            get
            {
                return "404 Not found";
            }
        }
    }
}
