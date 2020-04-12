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
			{ JokerPrimary, 54 }, // Joker Primary
			{ JokerSecondary, 53}, // Joker Secondary
			{ JokerInvariant, 53 } // Joker invariant - not primary, nor secondary - for alternative algorithm
		};

		public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public const string JokerSecondary = "JA";
		public const string JokerPrimary = "JB";
		public const string JokerInvariant = "JJ";
	}
}
