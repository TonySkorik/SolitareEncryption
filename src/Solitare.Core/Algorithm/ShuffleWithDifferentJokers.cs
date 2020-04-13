using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Solitare.Core.Helpers;
using Solitare.Core.Interfaces;
using Solitare.Core.Model;

namespace Solitare.Core.Algorithm
{
	public class ShuffleWithDifferentJokers : IShuffle
	{
		private Deck _input;
		private Card[] _cards;

		public List<int> Shuffle(Deck input, int keyLength)
		{
			Debug.WriteLine($"Initial deck: [{input.ToShortString()}]");
			_input = input;
			_cards = _input.ToArray();
			
			List<int> key = new List<int>(keyLength);

			for (int i = 0; i < keyLength; i++)
			{
				Step1(i);
				Step2(i);
				Step3(i);
				Step4(i);
				var keyElementValue = Step5();

				while (keyElementValue == null)
				{
					// means we hit a joker - calculate output key again
					Step1(i);
					Step2(i);
					Step3(i);
					Step4(i);
					keyElementValue = Step5();
				}

				key.Add(keyElementValue.Value);
			}

			return key;
		}

		private void Step1(int iteration)
		{
			/*
			 * 1. Find the A joker. Move it one card down. (That is, swap it with the card beneath it.)
			 * If the joker is the bottom card of the deck, move it just below the top card.
			 */

			var jaStartIndex = Array.IndexOf(_cards, Constants.JokerACard);
			var jaNewIndex = jaStartIndex + 1;
			if (jaStartIndex == _cards.Length - 1) // if joker is last
			{
				jaNewIndex = 1; // place after the first card
			}

			_cards.Move(jaStartIndex, jaNewIndex);

			DebugWriteDeck(iteration, 1);
		}

		private void Step2(int iteration)
		{
			/*
			 * 2. Find the B joker. Move it two cards down.
			 * If the joker is the bottom card of the deck, move it just below the second card.
			 * If the joker is one up from the bottom card, move it just below the top card.
			 */
			var jbStartIndex = Array.IndexOf(_cards, Constants.JokerBCard);
			var jbNewIndex = jbStartIndex + 2;

			if (jbStartIndex == _cards.Length - 1) // if joker is last
			{
				jbNewIndex = 2; // place after the second card
			}

			if (jbStartIndex == _cards.Length - 2) // if joker is second before last
			{
				jbNewIndex = 1; // place after the first card
			}
			
			_cards.Move(jbStartIndex, jbNewIndex);

			DebugWriteDeck(iteration, 2);
		}

		private void Step3(int iteration)
		{
			/*
			 * 3. Perform a triple cut. That is, swap the cards above the first joker with the cards below the second joker.
			 * "First" and "second" jokers refer to whatever joker is nearest to, and furthest from, the top of the deck. Ignore the "A" and "B" designations for this step.
			 * If there are no cards in one of the three sections (either the jokers are adjacent, or one is on top or the bottom), just treat that section as empty and move it anyway. 
			 */

			var jokerAIndex = Array.IndexOf(_cards, Constants.JokerACard);
			var jokerBIndex = Array.IndexOf(_cards, Constants.JokerBCard);

			_cards.SwapParts(
				0,
				Math.Min(jokerBIndex, jokerAIndex) - 1,
				Math.Max(jokerBIndex, jokerAIndex) + 1,
				_cards.Length - 1);

			DebugWriteDeck(iteration, 3);
		}

		private void Step4(int iteration)
		{
			/*
			 * Perform a count cut. Look at the bottom card. Convert it into a number from 1 through 53.
			 * Count down from the top card that number.
			 * Cut after the card that you counted down to, leaving the bottom card on the bottom.
			 * A deck with a joker as the bottom card will remain unchanged by this step.
			 * Be sure not to reverse the order when counting cards off the top. The correct way to count is to pass the cards, one at a time, from one hand to another.
			 * Don't make piles on the table.
			 */
			var bottomCardValue = _cards[^1].Value;
			var rangeToMoveEnd = bottomCardValue - 1;

			//following is a bit hacky - we swap first part with the last card, then we move last card back in place
			_cards.SwapParts(0, rangeToMoveEnd, _cards.Length-1, _cards.Length - 1);
			_cards.Move(0, _cards.Length - 1); //move last card back in place

			DebugWriteDeck(iteration, 4);
		}

		private int? Step5()
		{
			/*
			 * Find the output card. To do this, look at the top card.
			 * Convert it into a number from 1 through 53 in the same manner as step 4.
			 * Count down that many cards. (Count the top card as number one.)
			 * Write the card after the one you counted to on a piece of paper; don't remove it from the deck.
			 * (If you hit a joker, don't write anything down and start over again with step 1.) This is the next output card.
			 */
			var keyElementIndex = _cards[0].Value;
			var outputValue = _cards[keyElementIndex].Value;

			if (outputValue == Constants.JokerValue)
			{
				return null;
			}

			return outputValue;
		}

		private void DebugWriteDeck(int iteration, int step)
		{
			var shuffledDeck = new Deck(_cards);
			Debug.WriteLine($"#{iteration}, Step: {step}, Deck: [{shuffledDeck.ToShortString()}]");
		}
	}
}
