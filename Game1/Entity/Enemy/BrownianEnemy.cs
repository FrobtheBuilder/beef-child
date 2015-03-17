using System;
using Microsoft.Xna.Framework;

namespace Game1
{
	//employs brownian motion, continues straight until it decides to change direction at random.
	public class BrownianEnemy : Enemy
	{
		private float angle;
		private Random gen;

		public BrownianEnemy(Sprite spr, Rectangle b) : base(spr, Color.BurlyWood, b)
		{
			angle = 0;
			gen = new Random();
		}

		public override void Move()
		{
			if (gen.Next(0, 50) >= 45) {
				angle += (((float)gen.Next()*MathHelper.PiOver2) - MathHelper.PiOver4);
			}

			Velocity = 3 * Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
		}
	}
}

