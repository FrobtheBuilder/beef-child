using System;
using Microsoft.Xna.Framework;

namespace Game1
{
	public class Bullet : Entity
	{
		public Bullet(Sprite spr) : base(spr)
		{
			Sprite.Origin = Sprite.Relative(new Vector2(0.0f, 0.5f), Sprite.Texture);
		}
	}
}

