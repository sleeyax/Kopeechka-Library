using System;

namespace Kopeechka.Exceptions
{
	[Serializable]
	public class NotEnoughProductQuantityException : Exception
	{
		public NotEnoughProductQuantityException()
		{
		}

		public NotEnoughProductQuantityException(string message)
			: base(message)
		{
		}
	}
}
