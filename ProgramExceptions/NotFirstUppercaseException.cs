using System;

namespace ProgramExceptions;

public class NotFirstUppercaseException : Exception
{
    public NotFirstUppercaseException(string message) : base(message){}
}