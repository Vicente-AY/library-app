using System;

namespace ProgramExceptions;

public class WhiteSpaceException : Exception
{
    public WhiteSpaceException(string message) : base(message){}
}