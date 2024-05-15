using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMagic.Common.Core.Extensions;

public static class SqlKataExtensionMethods
{
    public static Query Query<T>(this QueryFactory qf)
    {
        TableAttribute tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();

        if (tableAttribute != null)
        {
            return qf.Query("public." + tableAttribute.Name);
        }

        throw new Exception("Model do not have 'Table' attribute");
    }

    public static Query WhereNotDeleted(this Query qf)
    {
        return qf.WhereNot("record_status", "DELETED");
    }


    public static Query WhereNotDeletedEx(this Query qf)
    {
        return qf.WhereNot("isarchived", true);
    }
}