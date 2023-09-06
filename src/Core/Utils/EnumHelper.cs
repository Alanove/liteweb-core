using System;
using System.Reflection;

namespace lw.Core.Utils;

/// <summary>
/// Provides a static utility object of methods and properties to interact
/// with enumerated types.
/// </summary>
public static class EnumHelper
{
	/// <summary>
	/// Gets the <see cref="Description" /> of an <see cref="Enum" />
	/// type value. 
	/// [Description="value"]
	/// </summary>
	/// <param name="en">The <see cref="System.Enum" /> type value.</param>
	/// <returns>A string containing the text of the <see cref="Description"/>.</returns> Attribute
	public static string GetDescription(System.Enum en)
	{
		Type type = en.GetType();
		MemberInfo[] memInfo = type.GetMember(en.ToString());
		if (memInfo != null && memInfo.Length > 0)
		{
			object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);
			if (attrs != null && attrs.Length > 0)
				return ((Description)attrs[0]).Text;
		}
		return en.ToString();
	}

	/// <summary>
	/// Gets the <see cref="Name" /> of an <see cref="System.Enum" />
	/// type value. 
	/// [Name="value"]
	/// </summary>
	/// <param name="en">The <see cref="System.Enum" /> type value.</param>
	/// <returns>A string containing the text of the <see cref="Name"/>.</returns> Attribute
	public static string GetName(System.Enum en)
	{
		Type type = en.GetType();
		MemberInfo[] memInfo = type.GetMember(en.ToString());
		if (memInfo != null && memInfo.Length > 0)
		{
			object[] attrs = memInfo[0].GetCustomAttributes(typeof(Name), false);
			if (attrs != null && attrs.Length > 0)
				return ((Name)attrs[0]).Text;
		}
		return en.ToString();
	}

	public static T EnsureEnum<T>(this object obj) where T : struct
	{
		if (!typeof(T).IsEnum)
		{
			throw new ArgumentException("T must be an enumerated type");
		}

		string str = obj.EnsureString();
		T retVal = (T)System.Enum.Parse(typeof(T), str, true);
		return retVal;
	}
}

/// <summary>
/// Returns the Description attribute of the enum
/// </summary>
public class Description : Attribute
{
	public string Text;
	public Description(string text)
	{
		Text = text;
	}
}

/// <summary>
/// Returns the Name attribute of the Enum
/// </summary>
public class Name : Attribute
{
	public string Text;
	public Name(string text)
	{
		Text = text;
	}
}

/// <summary>
/// Returns the Name attribute of the Enum
/// </summary>
public class PermissionGroup : Attribute
{
	public int Text;
	public PermissionGroup(int text)
	{
		Text = text;
	}
}
