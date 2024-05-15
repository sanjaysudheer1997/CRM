namespace CRM.Common.Core.Models;

public class DataTablesResponse<T>
{
  public int Draw { get; set; }
  public int RecordsTotal { get; set; }
  public int RecordsFiltered { get; set; }
  public IEnumerable<T> Data { get; set; }

  public DataTablesResponse(int draw, int recordsTotal, int recordsFiltered, IEnumerable<T> data)
  {
    Draw = draw;
    RecordsTotal = recordsTotal;
    RecordsFiltered = recordsFiltered;
    Data = data;
  }
}
