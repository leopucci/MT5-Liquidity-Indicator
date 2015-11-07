﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT5LiquidityIndicator.Net.Core
{
	internal unsafe class Quote
	{
		internal Quote(byte* ptr)
		{
			int bidsNumber = *(int*)(ptr);
			Debug.WriteLine("bids number = {0}", bidsNumber);
			int asksNumber = *(int*)(ptr + sizeof(int));
			Debug.WriteLine("asks number = {0}", asksNumber);

			this.Bids = new QuoteEntry[bidsNumber];
			this.Asks = new QuoteEntry[asksNumber];

			double* current = (double*)(ptr + 2 * sizeof(int));

			for (int index = 0; index < this.Bids.Length; ++index)
			{
				this.Bids[index].Price = *(current++);
				this.Bids[index].Volume = *(current++);
			}

			for (int index = 0; index < this.Asks.Length; ++index)
			{
				this.Asks[index].Price = *(current++);
				this.Asks[index].Volume = *(current++);
			}

			this.CreatingTime = DateTime.UtcNow;
		}

		internal DateTime CreatingTime { get; private set; }
		internal QuoteEntry[] Bids { get; private set; }
		internal QuoteEntry[] Asks { get; private set; }
		internal double Bid
		{
			get
			{
				return Bids[0].Price;
			}
		}
		internal double Ask
		{
			get
			{
				return Asks[0].Price;
			}
		}
	}
}
