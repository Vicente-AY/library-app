using System;

namespace ProgramExceptions;

public class NotValidLoginException : Exception
{
    public NotValidLoginException(string message) : base(message){}
}