using System;

namespace ProgramExceptions;

public class ShortStringException : Exception
{
    public ShortStringException(string message) : base(message){}
}