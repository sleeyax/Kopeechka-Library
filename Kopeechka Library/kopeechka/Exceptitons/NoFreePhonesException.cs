using System;

namespace Kopeechka.Exceptions
{
	[Serializable]
	public class NoFreePhonesException : Exception
	{
		public NoFreePhonesException()
		{
		}

		public NoFreePhonesException(string message)
			: base(message)
		{
		}
	}
}
