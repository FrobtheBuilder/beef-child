using System;
using System.Collections.Generic;
using System.Collections;

namespace Game1
{
	// the point of this class is event-safety
	// so that my entities can remove themselves and others from the world
	// during their update cycle without running the risk of a
	// modified world list mid-loop

	// this wouldn't be needed if I had a sane gamestate based architecture
	// but that comes later, so for now, this

	public class DelayedList<T> : List<T>
	{
		private List<T> toRemove;
		private List<T> toAdd;

		public DelayedList() : base() 
		{
			toRemove = new List<T>();
			toAdd = new List<T>();
		}

		new public bool Remove(T item)
		{
			toRemove.Add(item);
			return true;
		}

		new public void Add(T item)
		{
			toAdd.Add(item);
		}

		public void ImmediateAdd(T item) {
			base.Add(item);
		}

		public void ApplyRemovals()
		{
			foreach(T i in toRemove) {
				base.Remove(i);
			}
		}
		public void ApplyAdditions() {
			foreach(T i in toAdd) {
				base.Add(i);
			}
		}
	}
}