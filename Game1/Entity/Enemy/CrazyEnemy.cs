using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1
{
	public class CrazyEnemy : Enemy
	{
		private Random gen;
		public CrazyEnemy(Sprite spr, Rectangle b, DelayedList<Entity> cW, Entity tgt)
			: base(spr, Color.OrangeRed, b, cW, tgt)
		{
			gen = new Random(Guid.NewGuid().GetHashCode());
			// if I don't specify a non-time seed
			// I just get a bunch of identical randoms
		}

		public override void Move()
		{
			if (gen.Next(50) >= 45) {
				Velocity = new Vector2(
					(float)gen.Next(0, 10) - 5f, (float)gen.Next(0, 10) - 5f
				);
			}
		}

		public override void Attack(float angle) 
		{

		}
	}
}

