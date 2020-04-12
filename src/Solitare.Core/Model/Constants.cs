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
			{ JokerPrimaryCode, JokerPrimaryValue }, // Joker Primary
			{ JokerSecondaryCode, JokerSecondaryValue}, // Joker Secondary
			{ JokerInvariantCode, JokerInvariantValue } // Joker invariant - not primary, nor secondary - for alternative algorithm
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
			{ JokerPrimaryValue, JokerPrimaryCode }, // Joker Primary
			{ JokerSecondaryValue, JokerSecondaryCode } // Joker Secondary
		};

		public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public const string JokerSecondaryCode = "JA";
		public const int JokerSecondaryValue = 53;
		public const string JokerPrimaryCode = "JB";
		public const int JokerPrimaryValue = 54;
		public const string JokerInvariantCode = "JJ";
		public const int JokerInvariantValue = 53;

		public static Card JokerSecondaryCard = Card.Parse(JokerSecondaryCode);
		public static Card JokerPrimaryCard = Card.Parse(JokerPrimaryCode);
		public static Card JokerInvariantCard = Card.Parse(JokerInvariantCode);
	}
}
