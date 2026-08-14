using System;

namespace ProgramExceptions;

public class NotMatchException : Exception
{
    public NotMatchException(string message) : base(message){}
}