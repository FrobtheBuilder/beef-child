using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
	public class Bullet : Entity
	{
		private float hitBoxCorner;
		public Bullet(Sprite spr) : base(spr)
		{
			hitBoxCorner = Sprite.Relative(new Vector2(0.6f, 0.0f), Sprite.Texture).X;
			Sprite.Origin = Sprite.Relative(new Vector2(0.0f, 0.5f), Sprite.Texture);
		}

		public override void Update() {
			base.Update();

			//arghhhh
			HitBox = new Rectangle(
				(int)(hitBoxCorner * Math.Cos(Sprite.Rotation)),
				(int)(hitBoxCorner * Math.Sin(Sprite.Rotation)),
				7,7
			);

			if (Sprite.Position.X > 
					Program.Game.GraphicsDevice.Viewport.Bounds.Width - Sprite.Texture.Width
			|| Sprite.Position.X < 0
			|| Sprite.Position.Y >
				Program.Game.GraphicsDevice.Viewport.Bounds.Height - Sprite.Texture.Height
			|| Sprite.Position.Y < 0) {
				Program.Game.world.Remove(this);
			}
		}
	}
}