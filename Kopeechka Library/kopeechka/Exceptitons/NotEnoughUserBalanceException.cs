using System;

namespace Kopeechka.Exceptions
{
	[Serializable]
	public class NotEnoughUserBalanceException : Exception
	{
		public NotEnoughUserBalanceException()
		{
		}

		public NotEnoughUserBalanceException(string message)
			: base(message)
		{
		}
	}
}
