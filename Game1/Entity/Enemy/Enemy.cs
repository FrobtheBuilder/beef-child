using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics.Eventing.Reader;

namespace Game1
{
	public abstract class Enemy : Entity
	{
		private Rectangle bounds;

		private int health;
		public int Health {
			get {
				return health;
			}
			set {
				health = value;
			}
		}

		private Entity target;
		public Entity Target {
			get {
				return target;
			}
			set {
				target = value;
			}
		}
		protected bool attacking;

		protected Enemy(Sprite spr, Color c, Rectangle b, DelayedList<Entity> cW, Entity tgt)
			: base(spr, cW)
		{
			Sprite.Color = c;
			bounds = b;
			health = 5;
			target = tgt;

			Collide += (source, e) => {
				if (e.Other is Bullet) {
					CollidesWith.Remove(e.Other);
					health -= 1;
					if (health <= 0) {
						CollidesWith.Remove(this);
					}
				}
			};
		}

		public override void Update()
		{
			base.Update();
			Move();
			AvoidEdges();
			bool attackingLastFrame = attacking;
			float angle = 0;
			if(Vector2.Distance(this.Position, target.Position) < 100) {
				angle = (float)Math.Atan2(
					Sprite.Position.Y - target.Position.Y,
					Sprite.Position.X - target.Position.X
				);

				if (!attacking)
					attacking = true;
			}
			else {
				attacking = false;
			}
			if (attacking && !attackingLastFrame) {
				Attack(angle);
			}

		}

		public abstract void Move();

		public void AvoidEdges()
		{
			if (Sprite.Position.X > bounds.Width - Sprite.Texture.Width) {
				Sprite.Position += new Vector2(-5, 0);
			}
			if (Sprite.Position.X < 0) {
				Sprite.Position += new Vector2(5, 0);
			}
			if (Sprite.Position.Y > bounds.Height - Sprite.Texture.Height) {
				Sprite.Position += new Vector2(0, -5);
			}
			if (Sprite.Position.Y < 0) {
				Sprite.Position += new Vector2(0, 5);
			}
		}

		public abstract void Attack(float angle);
	}
}