using System;
using System.Runtime.Serialization;

namespace YC.Microsoft.Cognitive.Luis.Programmatic
{
	[Serializable]
	internal class LuisIntegrateException : Exception
	{
		public LuisIntegrateException()
		{
		}

		public LuisIntegrateException(string message) : base(message)
		{
		}

		public LuisIntegrateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected LuisIntegrateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}