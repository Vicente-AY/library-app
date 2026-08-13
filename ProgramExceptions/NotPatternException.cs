using System;

namespace ProgramExceptions;

public class NotPatternException : Exception
{
    public NotPatternException(string message) : base(message){}
}