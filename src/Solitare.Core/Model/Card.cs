using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Solitare.Core.Model
{
	public class Card : IEquatable<Card>
	{
		#region Props

		public int Value { get; }
		public Suite Suite { get; }
		public string Code { get; }
	
		#endregion

		#region Ctor

		private Card(string code)
		{
			Code = code;
		}

		private Card(string code, int value, Suite suite) : this(code)
		{
			Value = value;
			Suite = suite;
		}

		#endregion

		#region Factory methods

		public static Card Create(int cardValue, bool isDifferentJokers = true)
		{
			if (cardValue == Constants.JokerSecondaryValue
				|| cardValue == Constants.JokerPrimaryValue
				|| cardValue == Constants.JokerInvariantValue)
			{
				var cardCode = cardValue switch
				{
					Constants.JokerSecondaryValue => isDifferentJokers
						? Constants.JokerSecondaryCode
						: Constants.JokerInvariantCode,
					Constants.JokerPrimaryValue => Constants.JokerPrimaryCode,
					_ => throw new InvalidOperationException($"Card value {cardValue} is too large to be a joker.")
				};

				var joker = new Card(cardCode, cardValue, Suite.Joker);

				return joker;
			}

			Suite cardSuite = (cardValue % 13) switch
			{
				0 => Suite.Club,
				1 => cardValue == 13 ? Suite.Club : Suite.Diamond,
				3 => cardValue == 26 ? Suite.Diamond : Suite.Heart,
				4 => cardValue == 39 ? Suite.Heart : Suite.Spade,
				_ => throw new InvalidOperationException($"Card value {cardValue} is too large to compute suite.")
			};

			var value = cardValue - (int)cardSuite;
			var valueCode = Constants.CardCodes[value];
			var suiteCode = char.ToUpperInvariant(cardSuite.ToString().First());

			var card = new Card($"{valueCode}{suiteCode}", cardValue, cardSuite);

			return card;
		}

		public static Card Parse(string cardCode)
		{
			// card code : Kc - King of Clubs or JJ - invariant joker or 10s - 10 of spades
			var fixedCode = cardCode.Trim().ToUpperInvariant();

			//joker case
			if (fixedCode == Constants.JokerSecondaryCode
				|| fixedCode == Constants.JokerPrimaryCode
				|| fixedCode == Constants.JokerInvariantCode)
			{
				return new Card(fixedCode, Constants.CardValues[fixedCode], Suite.Joker);
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

			var ret = new Card(fixedCode, value, suite);

			return ret;
		}

		#endregion

		#region Overrides
		
		public override string ToString()
		{
			return $"{Code}: Suite = {Suite}, Value = {Value}";
		}

		#endregion

		#region Equality members

		public bool Equals(Card other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Value == other.Value && Suite == other.Suite;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != this.GetType())
			{
				return false;
			}

			return Equals((Card)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Value, (int)Suite);
		} 

		#endregion
	}
}
