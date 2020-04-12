using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solitare.Core.Model
{
	public class Deck : List<Card>
	{
		#region Ctor
		
		private Deck()
		{ }

		public Deck(List<Card> cards)
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

		#endregion

		#region Overrides

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(Count);

			foreach (var card in this)
			{
				sb.AppendLine(card.ToString());
			}

			return sb.ToString();
		} 

		#endregion
	}
}
