using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core;


[Serializable]
public class RecordNotFoundException : ApplicationException
{
  public RecordNotFoundException(string message) : base(message)
  {
  }
}

