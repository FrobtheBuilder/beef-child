using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1
{
	public class CrazyEnemy : Enemy
	{
		private Random gen;
		public CrazyEnemy(Sprite spr, Rectangle b, DelayedList<Entity> cW)
			: base(spr, Color.OrangeRed, b, cW)
		{
			gen = new Random();
		}

		public override void Move()
		{
			Velocity = new Vector2((float)gen.Next(0, 10) - 5f, (float)gen.Next(0, 10) - 5f);
			// beautiful.
		}
	}
}

