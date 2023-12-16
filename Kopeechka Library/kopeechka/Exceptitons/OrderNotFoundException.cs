using System;

namespace Kopeechka.Exceptions
{
	[Serializable]
	public class OrderNotFoundException : Exception
	{
		public OrderNotFoundException()
		{
		}

		public OrderNotFoundException(string message)
			: base(message)
		{
		}
	}
}
