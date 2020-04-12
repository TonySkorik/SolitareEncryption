using System;
using System.Collections.Generic;
using System.Text;
using Solitare.Core.Model;

namespace Solitare.Core.Interfaces
{
	public interface IShuffle
	{
		List<int> Shuffle(Deck input, int keyLength);

		public int GetSymbolNumber(char symbol)
		{
			return Constants.Alphabet.IndexOf(char.ToUpper(symbol)) + 1;
		}

		public char GetSymbolForNumber(int number)
		{
			if (number > 26)
			{
				return GetSymbolForNumber(number - 26);
			}

			if (number <= 0)
			{
				return GetSymbolForNumber(number + 26);
			}

			return Constants.Alphabet[number - 1];
		}
	}
}
