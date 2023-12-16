using System;

namespace Kopeechka.Exceptions
{
	[Serializable]
	public class HostingOrderException : Exception
	{
		public HostingOrderException()
		{
		}

		public HostingOrderException(string message)
			: base(message)
		{
		}
	}
}
