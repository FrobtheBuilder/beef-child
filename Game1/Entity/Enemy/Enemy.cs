using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;

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

		protected Enemy(Sprite spr, Color c, Rectangle b, DelayedList<Entity> cW) : base(spr, cW)
		{
			Sprite.Color = c;
			bounds = b;
			health = 5;

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
	}
}