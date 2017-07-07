using System;

namespace SimpleHttpServer.Arguments
{
    class ArgumentException : Exception
    {
        public ArgumentException(string commandName, string command) 
            : base($"expected {commandName} after {command} command") { }

    }
}
