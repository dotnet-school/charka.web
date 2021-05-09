namespace Chakra.Web.Responses
{
  public class ConverterResponse
  {
    public object Json { get; }
    public string Error { get; }
    public int ErrorLineNumber { get; }
    
    private ConverterResponse(object json, string error, int errorLineNumber)
    {
      Json = json;
      Error = error;
      ErrorLineNumber = errorLineNumber;
    }

    public static ConverterResponse ForSuccess(object json)
    {
      return new ConverterResponse(json, null, 0);
    }
    
    public static ConverterResponse ForError(DynamicCompilationException exception)
    {
      return new ConverterResponse(null, exception.Message, exception.LineNumber);
    }
  }
}