using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solitare.Core.Interfaces;
using Solitare.Core.Model;

namespace Solitare.Core
{
	public class SolitareCryptoProvider
	{
		private readonly IShuffle _shuffleAlgorithm;

		public SolitareCryptoProvider(IShuffle shuffleAlgorithm)
		{
			_shuffleAlgorithm = shuffleAlgorithm;
		}
		
		public string Encrypt(string message, Deck deck)
		{
			var key = _shuffleAlgorithm.Shuffle(deck, message.Length);
			var cypherTextChars = message.Zip(
				key,
				(messageChar, keyCahrValue) =>
				{
					var messageCharValue = _shuffleAlgorithm.GetSymbolNumber(messageChar);
					var sum = messageCharValue + keyCahrValue;
					return _shuffleAlgorithm.GetSymbolForNumber(sum);
				}).ToArray();

			return new string(cypherTextChars);
		}

		public string Decrypt(string cypherText, Deck deck)
		{
			var key = _shuffleAlgorithm.Shuffle(deck, cypherText.Length);
			var messageChars = cypherText.Zip(
				key,
				(messageChar, keyCahrValue) =>
				{
					var messageCharValue = _shuffleAlgorithm.GetSymbolNumber(messageChar);
					var sum = messageCharValue - keyCahrValue;
					return _shuffleAlgorithm.GetSymbolForNumber(sum);
				}).ToArray();

			return new string(messageChars);
		}
	}
}
