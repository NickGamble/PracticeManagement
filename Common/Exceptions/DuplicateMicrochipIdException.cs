using System;

namespace Common.Exceptions
{
    public class DuplicateMicrochipIdException : Exception
    {
        public DuplicateMicrochipIdException(string message) : base(message)
        {
        }
    }
}
