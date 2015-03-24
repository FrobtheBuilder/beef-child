using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1
{
	// will move towards an entity at various intervals.
	public class LazyFollowingEnemy : Enemy
	{
		private bool move;
		private float angle;
		private Random gen;
		private Entity following;
		public LazyFollowingEnemy(Sprite spr, Rectangle b, Entity e, DelayedList<Entity> cW, Entity tgt)
			: base(spr, Color.AliceBlue, b, cW, tgt)
		{
			gen = new Random();
			following = e;
			angle = 0;
		}

		public override void Move() {
			angle = (float)Math.Atan2(
				Sprite.Position.Y - following.Sprite.Position.Y,
				Sprite.Position.X - following.Sprite.Position.X
			);

			if (gen.Next(0, 50) >= 45) {
				move = true;
			}

			if (move && !attacking) {
				Velocity = 5 * Vector2.Normalize(new Vector2(
					-(float)Math.Cos(angle), -(float)Math.Sin(angle)
				));
				move = false;
			}

			Velocity *= new Vector2(0.9f, 0.9f); //slow down
		}

		public override void Attack(float angle) 
		{
			Velocity = 30 * Vector2.Normalize(new Vector2(
				-(float)Math.Cos(angle), -(float)Math.Sin(angle)
			));
			// GO IN FOR THE KILL
		}
	}
}

