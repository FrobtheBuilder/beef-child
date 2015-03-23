using System;
using System.Collections.Generic;
using System.Collections;

namespace Game1
{
	// the point of this class is event-safety
	// so that my entities can remove themselves and others from the world
	// during their update cycle without running the risk of a
	// modified world list mid-loop

	public class DelayedList<T> : List<T>
	{
		private List<T> toRemove;
		public DelayedList() : base() 
		{
			toRemove = new List<T>();
		}

		new public bool Remove(T item)
		{
			toRemove.Add(item);
			return true;
		}

		public void ApplyRemovals()
		{
			foreach(T i in toRemove) {
				base.Remove(i);
			}
		}
	}
}