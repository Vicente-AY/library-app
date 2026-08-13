using System;

namespace ProgramExceptions;

public class SameLoginException : Exception
{
    public SameLoginException(string message) : base(message){}
}