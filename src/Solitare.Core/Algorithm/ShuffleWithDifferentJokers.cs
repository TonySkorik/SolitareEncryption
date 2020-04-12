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
				key.Add(keyElementValue);

				//var shuffledDeck = new Deck(_cards);
				//Debug.WriteLine($"Iteration : {i}, Deck: [{shuffledDeck.ToShortString()}]");
			}

			return key;
		}

		private void Step1(int iteration)
		{
			/*
			 * 1 шаг. Передвиньте младшего джокера на 1 карту вниз колоды. Если он окажется последним, то поставьте его после 1 карты.
			 */

			// calculate new JA position
			var jaStartIndex = Array.IndexOf(_cards, Constants.JokerSecondaryCard);
			var jaNewIndex = jaStartIndex + 1;
			if (jaStartIndex == _cards.Length - 1)
			{
				jaNewIndex = 1; // after the first card
			}

			_cards.Move(jaStartIndex, jaNewIndex);

			DebugWriteDeck(iteration, 1);
		}

		private void Step2(int iteration)
		{
			/*
			 * 2 шаг. Передвиньте старшего джокера на 2 позиции вниз колоды. Если он последний, то поместите его после 2 карты, если предпоследний то после первой картой.
			 */

			// calculate new JB position
			var jaStartIndex = Array.IndexOf(_cards, Constants.JokerPrimaryCard);
			var jaNewIndex = jaStartIndex + 2;

			if (jaStartIndex == _cards.Length - 1) // if joker is last
			{
				jaNewIndex = 2; // after the second card
			}

			if (jaStartIndex == _cards.Length - 2) // if joker is second before last
			{
				jaNewIndex = 2; // after the first card
			}
			
			_cards.Move(jaStartIndex, jaNewIndex);

			DebugWriteDeck(iteration, 2);
		}

		private void Step3(int iteration)
		{
			/*
			 * 3 шаг. Поменяйте местами 2 крайние части колоды, отделяемые 2 джокерами. В данном случае число 1 пойдет в конец колоды.
			 */
			// swap deck parts outside of two jokers
			var primaryJokerIndex = Array.IndexOf(_cards, Constants.JokerPrimaryCard);
			var secondaryJokerIndex = Array.IndexOf(_cards, Constants.JokerSecondaryCard);

			_cards.SwapParts(
				0,
				Math.Min(primaryJokerIndex, secondaryJokerIndex) - 1,
				Math.Max(primaryJokerIndex, secondaryJokerIndex) + 1,
				_cards.Length - 1);

			DebugWriteDeck(iteration, 3);
		}

		private void Step4(int iteration)
		{
			/*
			 * 4 шаг. Посмотрите на последнее число. Отсчитайте такое количество карт от начала колоды и поместите их перед последней картой.
			 * Последнюю карту намеренно оставляют на месте для обратимости алгоритма.
			 */
			// swap head
			var lastCardValue = _cards[^1].Value;
			var rangeToMoveEnd = lastCardValue - 1;
			_cards.SwapParts(0, rangeToMoveEnd, _cards.Length-1, _cards.Length - 1);
			_cards.Move(0, _cards.Length - 1);

			DebugWriteDeck(iteration, 4);
		}

		private int Step5()
		{
			/*
			 * 5 шаг. Посмотрите на 1 число. Отсчитайте такое количество карт после нее и запомните это число. Это и есть первое число ключевой последовательности. 
			 */
			// get key element value
			var keyElementIndex = _cards[0].Value;
			return _cards[keyElementIndex].Value;
		}

		private void DebugWriteDeck(int iteration, int step)
		{
			var shuffledDeck = new Deck(_cards);
			Debug.WriteLine($"Iteration : {iteration}, Step: {step}, Deck: [{shuffledDeck.ToShortString()}]");
		}
	}
}
