/*
 * Author: Karthikeyan
 * Date:   September 15, 2023
 * 
 * Description: Poco Generator
 * 
 * Copyright (c) 2023 OnlineMagic. All rights reserved.
 */

namespace OnlineMagic.Common.Core;

using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using Npgsql;

public static class PocoGenerator
{
    public static void GeneratePocoClasses(string connectionString, string schemaName)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var tables = connection.Query<string>(
            @"SELECT table_name 
              FROM information_schema.tables 
              WHERE table_schema = @SchemaName",
            new { SchemaName = schemaName })
            .ToList();

        foreach (var table in tables)
        {
            var columns = connection.Query<dynamic>(
                @"SELECT column_name, data_type 
                  FROM information_schema.columns 
                  WHERE table_name = @TableName 
                    AND table_schema = @SchemaName",
                new { TableName = table, SchemaName = schemaName })
                .ToList();

            var primaryKeyColumns = connection.Query<string>(
            @"SELECT column_name
            FROM information_schema.key_column_usage
            WHERE table_name = @TableName
            AND table_schema = @SchemaName
            AND constraint_name IN (
                SELECT constraint_name
                FROM information_schema.table_constraints
                WHERE table_name = @TableName
                AND table_schema = @SchemaName
                AND constraint_type = 'PRIMARY KEY')",
            new { TableName = table, SchemaName = schemaName })
            .ToList();

            var className = ToCamelCase(table);
            var classContent = GenerateClassCode(className, columns, primaryKeyColumns);

            // Save the class content to a file or use it as needed.
            // For example, you can save it to the "Models" folder:
            var filePath = Path.Combine("Models", $"{className}.cs");
             File.WriteAllText(filePath, classContent);
        }
    }

    private static string GenerateClassCode(string className, List<dynamic> columns, List<string> primaryKeyColumnNames)
    {
        var properties = columns.Select(c =>
        {
            var propertyName = ToTitleCase(c.column_name);

            if (className.ToLower().Equals(propertyName.ToLower()))
            {
                propertyName += "_1";
            }

            var propertyType = GetCSharpType(c.data_type);
            var keyAttribute = string.Empty;
            var ignoreAttribute = string.Empty;
            var columnAttribute = "[SqlKata.Column(\"" + c.column_name + "\")]" + Environment.NewLine;

            // Add [Key] attribute for the primary key columns
            if (primaryKeyColumnNames.Contains(c.column_name))
            {
                keyAttribute = $@"[Key]{Environment.NewLine}";
                ignoreAttribute = $@"[SqlKata.Ignore]{Environment.NewLine}";
            }

            return $"{keyAttribute}{ignoreAttribute}{columnAttribute}public {propertyType} {propertyName} {{ get; set; }}{Environment.NewLine}";
        });

        var classContent = $@"namespace OnlineMagic.Common.Models; {Environment.NewLine} public class {className}
                          {{
                              {string.Join(Environment.NewLine, properties)}
                          }}";

        return classContent;
    }


    private static string ToTitleCase(string input)
    {
        var titleCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        titleCase = titleCase.Replace("_", "");
        return titleCase;
    }

    private static string ToCamelCase(string input)
    {
        string[] words = Regex.Split(input, "(?<!^)(?=[A-Z])|(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])|(?<=\\d)(?=\\D)");
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (word.Length > 0)
            {
                if (i == 0)
                {
                    words[i] = char.ToUpper(word[0]) + word.Substring(1);
                }
                else
                {
                    words[i] = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                }
            }
        }
        return string.Join("", words);
    }

    private static string GetCSharpType(string dbType)
    {
        switch (dbType)
        {
            case "boolean":
            case "bit(1)":
                return "bool";
            case "smallint":
                return "short";
            case "integer":
                return "int";
            case "bigint":
                return "long";
            case "real":
                return "float";
            case "double precision":
                return "double";
            case "json":
            case "jsonb":
                return "JObject"; // Replace with your preferred JSON handling library, such as Newtonsoft.Json.Linq.JObject
            case "numeric":
            case "money":
                return "decimal";
            case "text":
            case "character varying":
            case "character":
            case "citext":
            case "xml":
            case "name":
                return "string";
            case "uuid":
                return "Guid";
            case "bytea":
                return "byte[]";
            case "timestamp without time zone":
            case "timestamp with time zone":
            case "date":
                return "DateTime";
            case "time without time zone":
                return "TimeSpan";
            case "time with time zone":
                return "DateTimeOffset";
            case "interval":
                return "TimeSpan";
            case "cidr":
            case "inet":
                return "IPAddress";
            case "macaddr":
                return "PhysicalAddress";
            case "tsquery":
                return "NpgsqlTsQuery";
            case "tsvector":
                return "NpgsqlTsVector";
            case "bit(n)":
            case "bit varying":
                return "BitArray";
            case "point":
                return "NpgsqlPoint";
            case "lseg":
                return "NpgsqlLSeg";
            case "path":
                return "NpgsqlPath";
            case "polygon":
                return "NpgsqlPolygon";
            case "line":
                return "NpgsqlLine";
            case "circle":
                return "NpgsqlCircle";
            case "box":
                return "NpgsqlBox";
            case "hstore":
                return "Dictionary<string, string>";
            case "oid":
            case "xid":
            case "cid":
                return "uint";
            case "oidvector":
                return "uint[]";
            case "(internal) char":
                return "char";
            case "geometry (PostGIS)":
                return "PostgisGeometry";
            case "record":
                return "object[]";
            case "composite types":
                return "T";
            case "range types":
                return "NpgsqlRange<TElement>";
            case "multirange types (PG14)":
                return "NpgsqlRange<TElement>[]";
            case "enum types":
                return "TEnum";
            case "array types":
                return "Array (of element type)";
            default:
                return "object";
        }
    }
}

