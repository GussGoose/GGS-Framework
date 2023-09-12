// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using System.Collections.Generic;

namespace GGS_Framework
{
	public static partial class ExtensionMethods
	{
		#region Class Implementation
		public static string SplitByCapital (this string value)
		{
			string tempString = value;
			string newString = null;

			for (int i = 0; i < tempString.Length; i++)
			{
				char currChar = tempString[i];

				if (char.IsUpper (currChar) && i != 0)
					newString += " ";

				newString += currChar;
			}

			value = newString;
			return value;
		}

		public static string ToTitleCase (this string value)
		{
			if (value == string.Empty)
				return string.Empty;

			char[] chars = value.ToCharArray ();
			List<char> result = new List<char> ();

			result.Add (Char.ToUpper (chars[0]));

			for (int i = 1; i < chars.Length; i++)
			{
				char currentChar = chars[i];
				char nextChar = chars[i < chars.Length - 1 ? i + 1 : i];

				result.Add (currentChar);

				if (Char.IsNumber (currentChar) && Char.IsUpper (nextChar))
					result.Add (' ');
				else if (Char.IsLower (currentChar) && (Char.IsUpper (nextChar) || Char.IsNumber (nextChar)))
					result.Add (' ');
			}

			return new string (result.ToArray ());
		}

		public static string ToCamelCase (this string value)
		{
			if (value == string.Empty)
				return string.Empty;

			char[] chars = value.ToCharArray ();
			List<char> result = new List<char> ();

			result.Add (Char.ToLower (chars[0]));

			for (int i = 1; i < chars.Length; i++)
				result.Add (chars[i]);

			return new string (result.ToArray ());
		}

		public static string ToTitleLowerCase (this string value)
		{
			value = value.ToTitleCase ();
			value = value.ToLower ();

			char[] tempChars = value.ToCharArray ();
			List<char> chars = new List<char> ();

			for (int i = 0; i < tempChars.Length; i++)
			{
				char c = tempChars[i];

				chars.Add (c);
				if (Char.IsNumber (c))
					chars.Insert (chars.Count - 1, ' ');
			}

			return new string (chars.ToArray ());
		}

		public static string ToPascalCase (this string value)
		{
			List<char> chars = new List<char> ();

			chars.Add (Char.ToUpper (value[0]));

			for (int i = 1; i < value.Length; i++)
			{
				char c = value[i];

				if (value[i - 1] == ' ')
				{
					chars.Add (Char.ToUpper (c));
					continue;
				}

				if (c != ' ')
					chars.Add (c);
			}

			return new string (chars.ToArray ());
		}
		#endregion
	}
}