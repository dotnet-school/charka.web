using System;
using Chakra;

namespace Chakra.Web.Responses
{
  public class CompileResponse
  {
    public string[] ConsoleOutput { get; }
    public string Error { get; }
    public int ErrorLineNumber { get; }
    
    private CompileResponse(string[] consoleOutput, string error, int errorLineNumber)
    {
      ConsoleOutput = consoleOutput;
      Error = error;
      ErrorLineNumber = errorLineNumber;
    }

    public static CompileResponse ForSuccess(string consoleOutput)
    {
      return new CompileResponse((consoleOutput ?? "").Split(Environment.NewLine), null, 0);
    }
    
    public static CompileResponse ForError(DynamicCompilationException exception)
    {
      return new CompileResponse(null, exception.Message, exception.LineNumber);
    }
  }
}