using System;

namespace Kopeechka.Exceptions
{
	[Serializable]
	public class NotEnoughRatingException : Exception
	{
		public NotEnoughRatingException()
		{
		}

		public NotEnoughRatingException(string message)
			: base(message)
		{
		}
	}
}
