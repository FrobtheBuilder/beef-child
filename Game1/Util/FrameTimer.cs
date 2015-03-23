using System;
using Microsoft.Xna.Framework;

namespace Game1
{
	public class TickEvent
	{
		public bool End { get; set; }
		public int Previous { get; set; }
		public int Current { get; set; }

		public TickEvent(int current, int previous, bool end)
		{
			Current = current;
			Previous = previous;
			End = end;
		}
	}

	public delegate void TickEventHandler(object sender, TickEvent e);

	public class FrameTimer
	{
		public int Length { get; set; }
		public bool Enabled { get; set; }

		private int current;
		public int Current { 
			get { return current; }
			set {
				frameCounter = 0;
				current = value;
			}
		}

		public int FramesPerTick { get; set; }
		private int frameCounter;

		public event TickEventHandler Tick;

		public FrameTimer(int length, int framesPerTick)
		{
			frameCounter = 0;
			current = 0;
			Length = length;
			FramesPerTick = framesPerTick;

			Enabled = true;
		}

		public void Update(GameTime gameTime)
		{
			if (Enabled) {
				frameCounter++;

				if (frameCounter >= FramesPerTick) {
					int previous = Current;
					bool end = false;

					frameCounter = 0;
					if (Current < Length) {
						current++;
					}
					else {
						current = 0;
						end = true;
					}
					
					if (Tick != null) {
						Tick(this, new TickEvent(Current, previous, end));
					}
				}
			}
		}

		public void Reset() {
			if (Tick != null) {
				Tick(this, new TickEvent(0, Current, true));
			}
			Current = 0;
		}

		public void Start() {
			Enabled = true;
		}

		public void Stop() {
			Enabled = false;
			if (Tick != null) {
				Tick(this, new TickEvent(Current, Current, true));
			}
		}
	}
}