using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using System.Text;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Text.Json;

namespace lw.Core.Utils;
/// <summary>
/// A utility class containing static methods for common tasks in web application development.
/// These methods include object serialization and deserialization, SQL query generation, and digit conversion.
/// </summary>
public static class Tools
{
	/// <summary>
	/// Converts an object into a byte array using UTF-8 encoding and JSON serialization.
	/// </summary>
	/// <param name="obj">The object to be converted.</param>
	/// <returns>The byte array representing the serialized object, or null if the input object is null.</returns>
	public static byte[]? ObjectToByteArray(object Obj)
	{
		if (Obj == null)
			return null;

		return Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(Obj, GetJsonSerializerOptions()));
	}
	/// <summary>
	/// Retrieves the configured JsonSerializerOptions for JSON serialization.
	/// </summary>
	/// <returns>The JsonSerializerOptions instance.</returns>
	private static System.Text.Json.JsonSerializerOptions GetJsonSerializerOptions()
	{
		return new JsonSerializerOptions()
		{
			PropertyNamingPolicy = null,
			WriteIndented = true,
			AllowTrailingCommas = true//,
									  //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		};
	}
	/// <summary>
	/// Converts a byte array back into an object using JSON deserialization.
	/// </summary>
	/// <param name="bytes">The byte array to be deserialized.</param>
	/// <returns>The deserialized object, or null if the input byte array is null or empty.</returns>
	public static object? ByteArrayToObject(byte[] Bytes)
	{
		if (Bytes == null || !Bytes.Any())
			return null;

		return JsonSerializer.Deserialize<object>(Bytes, GetJsonSerializerOptions());
	}
	/// <summary>
	/// Generates an SQL query string representation from an IEnumerable query.
	/// </summary>
	/// <typeparam name="TEntity">The type of entity in the query.</typeparam>
	/// <param name="query">The query to be converted to SQL.</param>
	/// <returns>The SQL query string.</returns>
	public static string ToSql<TEntity>(IEnumerable<TEntity> query)
	{
		var enumerator = query.GetEnumerator();
		var enumeratorType = enumerator.GetType();
		var selectFieldInfo = enumeratorType.GetField("_selectExpression", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException($"cannot find field _selectExpression on type {enumeratorType.Name}");
		var sqlGeneratorFieldInfo = enumeratorType.GetField("_querySqlGeneratorFactory", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException($"cannot find field _querySqlGeneratorFactory on type {enumeratorType.Name}");
		var selectExpression = selectFieldInfo.GetValue(enumerator) as SelectExpression ?? throw new InvalidOperationException($"could not get SelectExpression");
		var factory = sqlGeneratorFieldInfo.GetValue(enumerator) as IQuerySqlGeneratorFactory ?? throw new InvalidOperationException($"could not get IQuerySqlGeneratorFactory");
		var sqlGenerator = factory.Create();
		var command = sqlGenerator.GetCommand(selectExpression);
		var sql = command.CommandText;
		return sql;
	}
	/// <summary>
	/// Converts Latin digits in a string to their corresponding Arabic or Indi numeral Unicode characters.
	/// </summary>
	/// <param name="input">The input string containing Latin digits.</param>
	/// <returns>The string with replaced Arabic numeral digits.</returns>
	public static string ToArabicDigits(this string input)
	{
		return input.Replace('0', '\u0660')
				.Replace('1', '\u0661')
				.Replace('2', '\u0662')
				.Replace('3', '\u0663')
				.Replace('4', '\u0664')
				.Replace('5', '\u0665')
				.Replace('6', '\u0666')
				.Replace('7', '\u0667')
				.Replace('8', '\u0668')
				.Replace('9', '\u0669');
	}
}

