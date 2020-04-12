using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solitare.Core.Model;

namespace Solitare.Core.Tests
{
	[TestClass]
	public class TestCypherParsing
	{
		[TestMethod]
		public void TestMessageParsingSingleValue1()
		{
			string cypherText = "A";

			var cryptoMessage = new EncryptedMessage(cypherText);
			cryptoMessage.GetValues().Should().Equal(1);
		}

		[TestMethod]
		public void TestMessageParsingSingleValue2()
		{
			string cypherText = "Z";

			var cryptoMessage = new EncryptedMessage(cypherText);
			cryptoMessage.GetValues().Should().Equal(26);
		}

		[TestMethod]
		public void TestMessageParsingTwoValues()
		{
			string cypherText = "AZ";

			var cryptoMessage = new EncryptedMessage(cypherText);
			cryptoMessage.GetValues().Should().Equal(1, 26);
		}
	}
}
