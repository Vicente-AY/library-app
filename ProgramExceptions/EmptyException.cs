using System;

namespace ProgramExceptions;

public class EmptyException : Exception
{
    public EmptyException(string message) : base(message){}
}