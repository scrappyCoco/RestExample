namespace Coding4fun.Common.Utils
{
	public static class StringExtension
	{
		public static short? TryParseShort(this string it)
		{
			if (short.TryParse(it, out short number)) return number;

			return null;
		}
	}
}