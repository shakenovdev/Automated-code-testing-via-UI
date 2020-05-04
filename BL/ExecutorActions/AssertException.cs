using System;

namespace BL.ExecutorActions
{
    internal class AssertException : Exception
    {
        public AssertException()
        {
            
        }

        public AssertException(string message)
            : base(message)
        {
        }
    }
}