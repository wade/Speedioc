using System;

namespace Speedioc.TestDomain
{
	public class Color : IColor
	{
		private string _assignedColorName;
		private IColor _innerColor;
		private string _name;

		public Color()
		{
		}

		public Color(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException("name");
			}
			_name = name;
		}

		public Color(IColor color)
		{
			if (null == color)
			{
				throw new ArgumentNullException("color");
			}
			_innerColor = color;
		}

		public IColor InnerColor
		{
			get { return _innerColor; }
		}

		public string Name
		{
			get { return ((null != _innerColor) ? _innerColor.Name : _name) ?? _assignedColorName; }
			set
			{ 
				_assignedColorName = value;
				_innerColor = null;
				_name = null;
			}
		}
	}
}