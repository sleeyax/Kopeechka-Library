using System;

namespace Kopeechka.Exceptions
{
	[Serializable]
	public class OrderExpiredException : Exception
	{
		public OrderExpiredException()
		{
		}

		public OrderExpiredException(string message)
			: base(message)
		{
		}
	}
}
