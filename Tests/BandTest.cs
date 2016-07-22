using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace /*NAMESPACE.OBJECTS*/
{
  public class /*NAME*/ : IDisposable
  {
    public ToDoTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=*****;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      ****.DeleteAll();
    }
  }
}
