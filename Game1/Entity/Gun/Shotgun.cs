using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
	// shoots five pellets at once, high spread / slow reload
	public class Shotgun : Gun
	{
		private Random gen;
		public Shotgun(Sprite spr, List<Bullet> bullets, Texture2D bulletTex) : base(spr, bullets, bulletTex)
		{
			gen = new Random();
		}

		public override void Fire() {
			for (int i=0; i<5; i++) {
				Bullet b = new Bullet(new Sprite(BulletTex));
				b.Sprite.Position = RotatedTipPosition;

				b.Velocity = 20 * NormalizedShotVelocity + new Vector2((float)gen.Next(0, 10)-5, (float)gen.Next(0, 10)-5);
				b.Sprite.Rotation = Sprite.Rotation;
				Bullets.Add(b);
			}

			Ready = false;
			StartReloadTimer(1000);
		}
	}
}
