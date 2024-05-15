namespace CRM.Common.Core.Exceptions;


[Serializable]
public class RecordNotFoundException : ApplicationException
{
  public RecordNotFoundException(string message) : base(message)
  {
  }
}

[Serializable]
public class DuplicateUserException : ApplicationException
{
  public DuplicateUserException(string message) : base(message)
  {

  }
}