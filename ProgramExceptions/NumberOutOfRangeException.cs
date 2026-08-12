using System;

namespace ProgramExceptions;

public class NumberOutOfRangeException : Exception
{
    public NumberOutOfRangeException(string message) : base(message){}
}