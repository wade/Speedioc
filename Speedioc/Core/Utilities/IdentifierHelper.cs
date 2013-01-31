using System;
using System.Text;

namespace Speedioc.Core.Utilities
{
	public static class IdentifierHelper
	{
		public static string MakeSafeIdentifier(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException("text");
			}

			StringBuilder sb = new StringBuilder(text.Length);
			foreach (char c in text)
			{
				// Remove '.' characters or white space characters
				if (c == '.' || char.IsWhiteSpace(c))
					continue;

				// Convert any non-letter or digit characters to underscores
				if (false == char.IsLetterOrDigit(c))
				{
					sb.Append("_");
					continue;
				}
				sb.Append(c);
			}
			return sb.ToString();
		}
	}
}