using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitare.Core.Algorithm;
using Solitare.Core.Model;

namespace Solitare.Core.Tests
{
	[TestClass]
	public class TestDecrypt
	{
		private string _cypherText = "MHVMRKJQIHLWZVGXLXEXOJARSTWHQHEYTDCABORZTQLXOGNSBPLOYTILOCOPEQYFYJJLMQFSIROF";

		private List<string> _deck = new List<string>(){
			"JA",
			"Kc",
			"5h",
			"8h",
			"9s",
			"Ac",
			"9c",
			"As",
			"Ah",
			"Jd",
			"8d",
			"2s",
			"Kh",
			"9h",
			"Qd",
			"10d",
			"5c",
			"5d",
			"10h",
			"4h",
			"2d",
			"6d",
			"7s",
			"5s",
			"6s",
			"JB",
			"Ks",
			"9d",
			"Qs",
			"3s",
			"3h",
			"4c",
			"8s",
			"Kd",
			"3d",
			"7h",
			"2h",
			"3c",
			"8c",
			"Jh",
			"6h",
			"Jc",
			"4s",
			"7c",
			"Qc",
			"Js",
			"6c",
			"2c",
			"10c",
			"Qh",
			"10s",
			"Ad",
			"7d",
			"4d"
		};
		
		[TestMethod]
		public void TestDecrypt1()
		{
			var deck = Deck.Parse(_deck);
			var csp = new SolitareCryptoProvider(new ShuffleWithDifferentJokers());
			var message = csp.Decrypt(_cypherText, deck);

		}

		[TestMethod]
		public void TestShuffle()
		{

		}
	}
}
