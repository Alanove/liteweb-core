using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Core.Cte;

public class RegularExpressions
{

	/// <summary>
	/// Validates a correct email address
	/// </summary>
	public const string Email = "^[^@%<>?.]([\\.]*[^@%<>?.])*@[a-z]([\\.\\-_]{0,1}[a-z0-9])*\\.[a-z]{2,}$";

	/// <summary>
	/// Validates a strong password
	/// </summary>
	public const string Password = "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})";

	/// <summary>
	/// Validates and integer
	/// </summary>
	public const string Integer = "\\-?\\d+";

	/// <summary>
	/// Validates a decimal entry
	/// </summary>
	public const string Decimal = "\\-?\\d+(\\.\\d*)?";

	/// <summary>
	/// Validates an image
	/// </summary>
	public const string Image = "[^\\\"<>/]+\\.(jpg|bmp|jpeg|gif|png|tif)$";

	/// <summary>
	/// Vaidates and HTML tag
	/// </summary>
	public const string HTMLTags = "<[^>]+>";

	/// <summary>
	/// Matches all hash tags in a text. ex: "#ab alsd #abc" will return ab and abc
	/// </summary>
	public const string HashTagMatcher = @"(?<=\s|^)#(\w*[A-Za-z_-]+\w*)";

	/// <summary>
	/// If called will remove all the query string and hash values from a Url
	/// </summary>
	public const string RemoveQueryStringFromURI = "(\\?.*|\\#.*)";
}
