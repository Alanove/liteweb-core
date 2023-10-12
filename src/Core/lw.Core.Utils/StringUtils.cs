using lw.Core.Cte;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace lw.Core.Utils;
/// <summary>
/// A utility class containing static methods for manipulating and transforming strings.
/// These methods include ensuring an object is treated as a string, removing HTML tags,
/// and converting strings into Url-friendly formats.
/// </summary>
public static class StringUtils
{
	/// <summary>
	/// Ensures that the object is is a string
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static string? EnsureString(this object obj)
	{
		if (obj == null)
		{
			return string.Empty;
		}
		else
		{
			return obj.ToString();
		}
	}


	/// <summary>
	/// Removed HTML tags from the string
	/// </summary>
	/// <param name="s">The entry string</param>
	/// <returns>Output, stripped out from any HTML tags</returns>
	public static string StripOutHtmlTags(string s)
	{
		if (s == null)
			return "";
		var r = new Regex(RegularExpressions.HTMLTags);
		return r.Replace(s, "");
	}

	/// <summary>
	/// Converts the string into a url friendly, by replacing illegal characters with the parameter replacement
	/// </summary>
	/// <param name="str">Input string</param>
	/// <param name="replacement">Replacement String</param>
	/// <returns>Url Friendly string</returns>
	public static string ToUrl(string str, string replacement = "-")
	{
		if (replacement == null)
			replacement = "-";
		str = str.ToLower();
		str = StripOutHtmlTags(str);

		str = str.Replace("&reg;", "");

		var r = new Regex("\\W");
		var r1 = new Regex("\\s+");

		string before = "àÀâÂäÄáÁéÉèÈêÊëËìÌîÎïÏòÒôÔöÖùÙûÛüÜçÇ’ñ/ó:";
		string after = "aAaAaAaAeEeEeEeEiIiIiIoOoOoOuUuUuUcC-n-o ";

		string cleaned = str;

		for (int i = 0; i < before.Length; i++)
		{
			cleaned = Regex.Replace(cleaned, before[i].ToString(), after[i].ToString());
		}


		cleaned = r1.Replace(r.Replace(cleaned, " ").Trim(), " ");

		cleaned = cleaned.Replace(" ", replacement);
		string ret = cleaned;


		if (replacement != "")
		{
			var r3 = new Regex(replacement + "+");

			ret = r3.Replace(cleaned, replacement);
		}
		return ret.Length > 55 ? ret.Substring(0, 55) : ret;
	}

	/// <summary>
	/// Extracts hashtags from the input string.
	/// </summary>
	/// <param name="input">The input string from which hashtags will be extracted.</param>
	/// <returns>An array of strings containing the extracted hashtags.</returns>
	public static string[] ExtractHashtags(string input)
	{
		var matches = Regex.Matches(input, RegularExpressions.HashTagMatcher);

		var hashtags = new string[matches.Count];
		for (int i = 0; i < matches.Count; i++)
		{
			hashtags[i] = matches[i].Value.Replace("#", "");
		}

		return hashtags;
	}

	/// <summary>
	/// Inject tge tag links into the string, the tags inside the string will then be clickable.
	/// </summary>
	/// <param name="inputStr">The Input String</param>
	/// <param name="tagsPath">The absolute path for tags in the website, ex: /tags. The tag link will be added after</param>
	/// <returns>The modified string</returns>
	public static string InjectHashTagLinks(string inputStr, string tagsPath)
	{
		Regex r = new Regex(RegularExpressions.HashTagMatcher, 
			RegexOptions.IgnoreCase);

		return r.Replace(inputStr, delegate (Match m)
		{
			if (m.Length == 1)
				return m.Value;

			return $"<a href=\"{tagsPath}/{m.Value.Substring(1)}\">{m.Value}</a>";
		});
	}

    /// <summary>
    /// Trankates a string by removed the last set of characters and limiting it to length.
    /// If the string length is less than <paramref name="length"/> it will be returned as is
    /// If the string length is more it will be trankated.
    /// The last word will be removed and replaced with <paramref name="end"/>
    /// </summary>
    /// <param name="s">Entry string</param>
    /// <param name="length">The length to be transkated</param>
    /// <param name="end">The closing sentence (can be: ...)</param>
    /// <returns>Returns the trankated string</returns>
    public static string Trankate(string s, int length, string end)
    {
        s = StripOutHtmlTags(s);
        if (s.Length > length)
        {
            s = s.Substring(0, length - 3);
            var regex = new Regex("\\s([a-z_0-9.&;])*$", RegexOptions.IgnoreCase);
            return regex.Replace(s, end);
        }
        return s;
    }

    /// <summary>
    /// Trankates a string by removed the last set of characters and limiting it to length.
    /// If the string length is less than <paramref name="length"/> it will be returned as is
    /// If the string length is more it will be trankated.
    /// The last word will be removed
    /// </summary>
    /// <param name="s">Entry string</param>
    /// <param name="length">The length to be transkated</param>
    /// <returns>Returns the trankated string</returns>
    public static string Trankate(string s, int length)
    {
        return Trankate(s, length, "");
    }
}
