using System;

namespace Common.Utils
{
	public static class DateTimeExtension
	{
		public static long ToEpoch(this DateTime dateTime)
		{
			return (long) (dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
		}
	}
}