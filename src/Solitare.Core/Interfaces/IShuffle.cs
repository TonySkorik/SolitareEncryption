using System;
using System.Collections.Generic;
using System.Text;
using Solitare.Core.Model;

namespace Solitare.Core.Interfaces
{
	public interface IShuffle
	{
		List<int> Shuffle(Deck input);
	}
}
