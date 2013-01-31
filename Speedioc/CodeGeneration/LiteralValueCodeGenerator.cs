using System;
using System.Collections.Generic;
using System.Globalization;

namespace Speedioc.CodeGeneration
{
	public static class LiteralValueCodeGenerator
	{
		private static readonly Dictionary<Type, Func<object, string>> _handlerMap = InitializeHandlerMap();

		public static string GenerateCodeForLiteralValue(object value)
		{
			if (null == value)
			{
				return string.Empty;
			}

			Func<object, string> handler;
			
			if (_handlerMap.TryGetValue(value.GetType(), out handler))
			{
				return handler(value);
			}
			
			throw new InvalidOperationException(
				string.Format(
					"An unsupported type '{0}' was passed to the {1}.GenerateCodeForLiteralValue(object value) method.",
					value.GetType().FullName,
					typeof(LiteralValueCodeGenerator).FullName
					)
				);
		}

		private static Dictionary<Type, Func<object, string>> InitializeHandlerMap()
		{
			return
				new Dictionary<Type, Func<object, string>>
					{
						{ typeof(bool), GenerateCodeForBoolean }, 
						{ typeof(byte), GenerateCodeForByte }, 
						{ typeof(char), GenerateCodeForChar }, 
						{ typeof(decimal), GenerateCodeForDecimal }, 
						{ typeof(double), GenerateCodeForDouble }, 
						{ typeof(float), GenerateCodeForFloat }, 
						{ typeof(short), GenerateCodeForInt16 }, 
						{ typeof(int), GenerateCodeForInt32 }, 
						{ typeof(long), GenerateCodeForInt64 }, 
						{ typeof(sbyte), GenerateCodeForSByte },
						{ typeof(string), GenerateCodeForString },
						{ typeof(ushort), GenerateCodeForUInt16 },
						{ typeof(uint), GenerateCodeForUInt32 },
						{ typeof(ulong), GenerateCodeForUInt64 },
					};
		}

		private static string GenerateCodeForBoolean(object value)
		{
			bool b = (bool)value;
			return b.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
		}

		private static string GenerateCodeForByte(object value)
		{
			byte b = (byte)value;
			return b.ToString(CultureInfo.InvariantCulture);
		}

		private static string GenerateCodeForChar(object value)
		{
			char c = (char) value;
			return string.Format("'{0}'", c);
		}

		private static string GenerateCodeForDecimal(object value)
		{
			decimal m = (decimal)value;
			return m.ToString(CultureInfo.InvariantCulture) + "M";
		}

		private static string GenerateCodeForDouble(object value)
		{
			double d = (double)value;
			return d.ToString(CultureInfo.InvariantCulture) + "D";
		}

		private static string GenerateCodeForFloat(object value)
		{
			float f = (float)value;
			return f.ToString(CultureInfo.InvariantCulture) + "F";
		}

		private static string GenerateCodeForInt16(object value)
		{
			short s = (short) value;
			return s.ToString(CultureInfo.InvariantCulture);
		}

		private static string GenerateCodeForInt32(object value)
		{
			int i = (int)value;
			return i.ToString(CultureInfo.InvariantCulture);
		}

		private static string GenerateCodeForInt64(object value)
		{
			long l = (long)value;
			return l.ToString(CultureInfo.InvariantCulture) + "L";
		}

		private static string GenerateCodeForSByte(object value)
		{
			sbyte sb = (sbyte)value;
			return sb.ToString(CultureInfo.InvariantCulture);
		}

		private static string GenerateCodeForString(object value)
		{
			string s = (string) value;
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			return string.Format("\"{0}\"", s);
		}

		private static string GenerateCodeForUInt16(object value)
		{
			ushort us = (ushort)value;
			return us.ToString(CultureInfo.InvariantCulture);
		}

		private static string GenerateCodeForUInt32(object value)
		{
			uint ui = (uint)value;
			return ui.ToString(CultureInfo.InvariantCulture) + "U";
		}

		private static string GenerateCodeForUInt64(object value)
		{
			ulong ul = (ulong)value;
			return ul.ToString(CultureInfo.InvariantCulture) + "UL";
		}
	}
}