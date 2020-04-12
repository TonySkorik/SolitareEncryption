using System;

namespace Solitare.Core.Model
{
	public class Card
	{
		private readonly string _code;
		
		public int Value { get; private set; }
		public Suite Suite { get; private set; }
		public string Code => _code;

		private Card(string code)
		{
			_code = code;
		}

		public static Card Parse(string cardCode)
		{
			// card code : Kc - King of Clubs or JJ - invariant joker or 10s - 10 of spades
			var fixedCode = cardCode.Trim().ToUpperInvariant();
			
			//joker case
			if (fixedCode == Constants.JokerSecondary
				|| fixedCode == Constants.JokerPrimary
				|| fixedCode == Constants.JokerInvariant)
			{
				return new Card(fixedCode)
				{
					Value = Constants.CardValues[fixedCode],
					Suite = Suite.Joker
				};
			}

			string cardSuiteString = fixedCode.Substring(fixedCode.Length - 1, 1);
			string cardValueString = fixedCode.Substring(0, fixedCode.Length - 1);

			var cardValueUnmodified = Constants.CardValues[cardValueString];

			var suite = cardSuiteString switch
			{
				"C" => Suite.Club,
				"D" => Suite.Diamond,
				"H" => Suite.Heart,
				"S" => Suite.Spade,
				_ => throw new Exception($"Unknown card suit : {cardSuiteString}")
			};

			var value = cardValueUnmodified + (int)suite;

			var ret = new Card(fixedCode)
			{
				Suite = suite,
				Value = value
			};

			return ret;
		}

		public override string ToString()
		{
			return $"{_code}: Suite = {Suite}, Value = {Value}";
		}
	}
}
