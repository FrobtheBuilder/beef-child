using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
	//employs brownian motion, continues straight until it decides to change direction at random.
	public class BrownianEnemy : Enemy
	{
		private float angle;
		private Random gen;

		public BrownianEnemy(Sprite spr, Rectangle b, DelayedList<Entity> cW, Entity tgt)
			: base(spr, Color.BurlyWood, b, cW, tgt)
		{
			angle = 0;
			gen = new Random(Guid.NewGuid().GetHashCode());
		}

		public override void Move()
		{
			if (gen.Next(0, 50) >= 45) {
				angle += (((float)gen.Next()*MathHelper.PiOver2) - MathHelper.PiOver4);
			}

			Velocity = 3 * Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
		}

		public override void Attack(float angle) 
		{
			Bullet b = new Bullet(
				new Sprite(Program.Game.Content.Load<Texture2D>("bullet"))
			);
			b.Unfriendly = true;
			b.Position = this.Position;
			b.Sprite.Rotation = angle;
			b.Velocity = 20 * Vector2.Normalize(new Vector2(
				-(float)Math.Cos(angle), -(float)Math.Sin(angle)
			));
			CollidesWith.Add(b);
		}
	}
}

