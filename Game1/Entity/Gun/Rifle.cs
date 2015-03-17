using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
	// perfectly accurate, semi automatic
	public class Rifle : Gun
	{

		public Rifle(Sprite spr, List<Bullet> bullets, Texture2D bulletTex) : base(spr, bullets, bulletTex)
		{
			// uhhh
		}

		public override void Fire() {
			Bullet b = new Bullet(new Sprite(BulletTex));
			b.Sprite.Position = RotatedTipPosition;

			b.Velocity = 15 * NormalizedShotVelocity;
			b.Sprite.Rotation = Sprite.Rotation;
			Bullets.Add(b);

			Ready = false;
			StartReloadTimer(500);
		}
	}
}