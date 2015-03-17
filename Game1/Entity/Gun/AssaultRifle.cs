using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
	// rapid fire full auto, moderate spread
	public class AssaultRifle : Gun
	{
		private Random gen;
		public AssaultRifle(Sprite spr, List<Bullet> bullets, Texture2D bulletTex) : base(spr, bullets, bulletTex)
		{
			gen = new Random();
		}

		public override void Fire() {
			Bullet b = new Bullet(new Sprite(BulletTex));
			b.Sprite.Position = RotatedTipPosition;

			b.Velocity = 20 * NormalizedShotVelocity + new Vector2((float)gen.Next(0, 3)-1.5f, (float)gen.Next(0, 3)-1.5f); //ghetto spread factor
			b.Sprite.Rotation = Sprite.Rotation;
			Bullets.Add(b);

			Ready = false;
			StartReloadTimer(30);
		}
	}
}