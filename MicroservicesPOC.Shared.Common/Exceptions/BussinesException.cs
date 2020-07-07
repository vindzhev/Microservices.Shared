namespace MicroservicesPOC.Shared.Common.Exceptions
{
    using System;

    public class BussinesException : Exception
    {
        public BussinesException(string message) : base(message) { }

        public BussinesException(string message, Exception ex) : base(message, ex) { }
    }
}
