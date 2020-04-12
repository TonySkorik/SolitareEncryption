using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solitare.Core.Model
{
	public class Deck : List<Card>
	{
		//private Card[] _cards;
		//public Card[] Cards => _cards;

		#region Ctor
		
		public Deck()
		{ }

		public Deck(IEnumerable<Card> cards)
		{
			AddRange(cards);
		}

		#endregion

		#region Factory methods
		
		public static Deck Parse(IEnumerable<string> cardCodes)
		{
			var ret = new Deck();

			foreach (var cardCode in cardCodes)
			{
				ret.Add(Card.Parse(cardCode));
			}

			return ret;
		}

		public static Deck Create(IEnumerable<int> cardValues, bool isDifferentJokers = true)
		{
			var ret = new Deck(cardValues.Select(v=>Card.Create(v, isDifferentJokers)));
			return ret;
		}

		#endregion

		#region ToString

		public override string ToString()
		{
			return string.Join("; ", this.Select(c => c.ToString()));
		}

		public string ToShortString()
		{
			return string.Join("; ", this.Select(c => c.Code));
		}

		#endregion
	}
}
