using System;
using Microsoft.Xna.Framework;

namespace Game1
{
	public abstract class Enemy : Entity
	{
		private Rectangle bounds;
		protected Enemy(Sprite spr, Color c, Rectangle b) : base(spr)
		{
			Sprite.Color = c;
			bounds = b;
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