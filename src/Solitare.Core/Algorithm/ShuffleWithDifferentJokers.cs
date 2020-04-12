using System;
using System.Collections.Generic;
using System.Linq;
using Solitare.Core.Helpers;
using Solitare.Core.Interfaces;
using Solitare.Core.Model;

namespace Solitare.Core.Algorithm
{
	public class ShuffleWithDifferentJokers
	{
		private Deck _input;
		private Card[] _cards;

		public List<int> Shuffle(Deck input, int inputCount)
		{
			_input = input;
			_cards = _input.ToArray();
			
			List<int> ret = new List<int>(inputCount);

			for (int i = 0; i < inputCount; i++)
			{
				Step1();
				Step2();
				Step3();
				Step4();
				var keyElementValue = Step5();

				ret.Add(keyElementValue);
			}

			return ret;
		}

		private void Step1()
		{
			/*
			 * 1 шаг. Передвиньте младшего джокера на 1 карту вниз колоды. Если он окажется последним, то поставьте его после 1 карты.
			 */

			// calculate new JA position
			var jaStartIndex = Array.IndexOf(_cards, Constants.JokerSecondary);
			var jaNewIndex = jaStartIndex + 1;
			if (jaNewIndex == _cards.Length - 1)
			{
				jaNewIndex = 1; // after the first card
			}

			_cards.Move(jaStartIndex, jaNewIndex);
		}

		private void Step2()
		{
			/*
			 * 2 шаг. Передвиньте старшего джокера на 2 позиции вниз колоды. Если он последний, то поместите его после 2 карты, если предпоследний то после первой картой.
			 */

			// calculate new JB position
			var jaStartIndex = Array.IndexOf(_cards, Constants.JokerPrimary);
			var jaNewIndex = jaStartIndex + 1;
			if (jaNewIndex == _cards.Length - 1)
			{
				jaNewIndex = 2; // after the first card
			}

			_cards.Move(jaStartIndex, jaNewIndex);
		}

		private void Step3()
		{
			/*
			 * 3 шаг. Поменяйте местами 2 крайние части колоды, отделяемые 2 джокерами. В данном случае число 1 пойдет в конец колоды.
			 */
			// swap deck parts outside of two jokers
			var primaryJokerIndex = Array.IndexOf(_cards, Constants.JokerPrimary);
			var secondaryJokerIndex = Array.IndexOf(_cards, Constants.JokerSecondary);

			_cards.SwapParts(
				0,
				Math.Min(primaryJokerIndex, secondaryJokerIndex) - 1,
				Math.Max(primaryJokerIndex, secondaryJokerIndex) + 1,
				_cards.Length - 1);
		}

		private void Step4()
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
	}
}
