using System.Collections.Generic;

namespace Solitare.Core.Model
{
	public class Constants
	{
		public static readonly Dictionary<string, int> CardValues = new Dictionary<string, int>()
		{
			{ "A", 1 },
			{ "2", 2 },
			{ "3", 3 },
			{ "4", 4 },
			{ "5", 5 },
			{ "6", 6 },
			{ "7", 7 },
			{ "8", 8 },
			{ "9", 9 },
			{ "10", 10 },
			{ "J", 11 },
			{ "Q", 12 },
			{ "K", 13 },
			{ JokerBCode, 53 }, // Joker A
			{ JokerACode, 53 }, // Joker B
		};

		public static readonly Dictionary<int, string> CardCodes = new Dictionary<int, string>()
		{
			{ 1, "A" },
			{ 2, "2" },
			{ 3, "3" },
			{ 4, "4" },
			{ 5, "5" },
			{ 6, "6" },
			{ 7, "7" },
			{ 8, "8" },
			{ 9, "9" },
			{ 10, "10" },
			{ 11, "J" },
			{ 12, "Q" },
			{ 13, "K" },
			{ JokerValue, JokerBCode }, // Joker B ???
		};

		public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public const string JokerACode = "JA";
		public const string JokerBCode = "JB";
		public const int JokerValue = 53;

		public static Card JokerACard = Card.Parse(JokerACode);
		public static Card JokerBCard = Card.Parse(JokerBCode);
	}
}
