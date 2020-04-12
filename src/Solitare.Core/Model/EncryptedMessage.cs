using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solitare.Core.Model
{
	public class EncryptedMessage
	{
		private readonly string _cypherText;
		
		public EncryptedMessage(string cypherText)
		{
			_cypherText = cypherText;
		}

		public List<int> GetValues()
		{
			return _cypherText.Select(ParseSymbol).ToList();
		}

		private int ParseSymbol(char symbol)
		{
			return Constants.Alphabet.IndexOf(char.ToUpperInvariant(symbol)) + 1;
		}
	}
}
