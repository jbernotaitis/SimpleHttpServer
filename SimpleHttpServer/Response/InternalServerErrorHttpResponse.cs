using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Response
{
    class InternalServerErrorHttpResponse : AbstractHttpResponse
    {
        private string _errorMessage;

        public InternalServerErrorHttpResponse(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        protected override byte[] Content
        {
            get
            {
                return Encoding.ASCII.GetBytes($@"<html>
                    <body>
                    <h1>500 Internal server error</h1>
                    <pre> {_errorMessage} </pre></body>
                    </html>");
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
                return "500 Internal server error";
            }
        }
    }
}
