using System;
using System.IO;
using System.Reflection;

namespace Speedioc.Core.Utilities
{
	public class ResourceUtility
	{
		public static string GetManifestResourceAsString(string manifestResourceName, Assembly assembly = null)
		{
			if (string.IsNullOrWhiteSpace(manifestResourceName))
			{
				throw new ArgumentNullException("manifestResourceName");
			}
			if (null == assembly)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			Stream stream = assembly.GetManifestResourceStream(manifestResourceName);
			if (null != stream)
			{
				string output;
				using (StreamReader reader = new StreamReader(stream))
				{
					output = reader.ReadToEnd();
					reader.Close();
				}
				return output;
			}
			return null;
		} 
	}
}