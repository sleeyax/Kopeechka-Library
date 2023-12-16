using System;

namespace Kopeechka.Exceptions
{
	[Serializable]
	public class OrderHasSmsException : Exception
	{
		public OrderHasSmsException()
		{
		}

		public OrderHasSmsException(string message)
			: base(message)
		{
		}
	}
}
