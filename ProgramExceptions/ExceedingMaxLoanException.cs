using System;

namespace ProgramExceptions;

public class ExceedingMaxLoanException : Exception
{
    public ExceedingMaxLoanException(string message) : base(message){}
}