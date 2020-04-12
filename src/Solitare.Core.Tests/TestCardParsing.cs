using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitare.Core.Model;

namespace Solitare.Core.Tests
{
	[TestClass]
	public class TestCardParsing
	{

		[TestMethod]
		public void TestCardParsingClubs1()
		{
			string cardCode = "Ac"; // ace of clubs == 1

			var card = Card.Parse(cardCode);

			AssertCard(card, Suite.Club, 1);
		}

		[TestMethod]
		public void TestCardParsingClubs2()
		{
			string cardCode = "Kc"; // ace of clubs == 1

			var card = Card.Parse(cardCode);

			AssertCard(card, Suite.Club, 13);
		}

		[TestMethod]
		public void TestCardParsingDiamonds1()
		{
			string cardCode = "Ad"; // ace of diamonds == 14

			var card = Card.Parse(cardCode);

			AssertCard(card, Suite.Diamond, 14);
		}

		[TestMethod]
		public void TestCardParsingDiamonds2()
		{
			string cardCode = "Kd"; // king of diamonds == 26

			var card = Card.Parse(cardCode);

			AssertCard(card, Suite.Diamond, 26);
		}

		[TestMethod]
		public void TestCardParsingHearts1()
		{
			string cardCode = "Ah"; // ace of hearts == 27

			var card = Card.Parse(cardCode);

			AssertCard(card, Suite.Heart, 27);
		}

		[TestMethod]
		public void TestCardParsingHearts2()
		{
			string cardCode = "Kh"; // king of hearts == 39

			var card = Card.Parse(cardCode);

			AssertCard(card, Suite.Heart, 39);
		}

		[TestMethod]
		public void TestCardParsingSpades1()
		{
			string cardCode = "As"; // ace of spades \w/ == 40

			var card = Card.Parse(cardCode);

			AssertCard(card, Suite.Spade, 40);
		}

		[TestMethod]
		public void TestCardParsingSpades2()
		{
			string cardCode = "Ks"; // king of spades == 52

			var card = Card.Parse(cardCode);

			AssertCard(card, Suite.Spade, 52);
		}

		[TestMethod]
		public void TestCardParsingJokers()
		{
			string cardJokerA = "JA"; // Joker Primary == 54
			string cardJokerB = "JB"; // Joker Secondary == 53
			string cardJokerInvariant = "JJ"; // Joker Invariant == 53

			var jokerA = Card.Parse(cardJokerA);
			var jokerB = Card.Parse(cardJokerB);
			var jokerInvariant = Card.Parse(cardJokerInvariant);

			AssertCard(jokerA, Suite.Joker, 54);
			AssertCard(jokerB, Suite.Joker, 53);
			AssertCard(jokerInvariant, Suite.Joker, 53);
		}

		[TestMethod]
		public void TestDeckParsing1()
		{
			List<string> cardCodes = new List<string>()
			{
				"Ac", "Kc", "ah", "kh"
			};

			Deck d = Deck.Parse(cardCodes);

			d.Select(c => c.Value).Should().Equal(1, 13, 27, 39);
		}

		public void AssertCard(Card card, Suite suite, int value)
		{
			card.Suite.Should().Be(suite);
			card.Value.Should().Be(value);
		}
	}
}
