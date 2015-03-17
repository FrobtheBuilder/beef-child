using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Game1
{
	public abstract class Gun : Entity
	{

		private Vector2 tip;
		public Vector2 Tip {
			get { return tip; }
			set { tip = value; }
		}

		private List<Bullet> bullets;
		public List<Bullet> Bullets {
			get { return bullets; }
			set { bullets = value; }
		}
		private Texture2D bulletTex;
		public Texture2D BulletTex {
			get { return bulletTex; }
			set { bulletTex = value; }
		}

		private bool ready;
		public bool Ready {
			get { return ready; }
			set { ready = value; }
		}

		public Vector2 RotatedTipPosition {
			get {
				float hyp = Tip.X - Sprite.TopLeft.X;
				return Sprite.Position + new Vector2(hyp*(float)Math.Cos(Sprite.Rotation), hyp*(float)Math.Sin(Sprite.Rotation));
				// I actually had to draw a triangle to figure this one out and it's still not completely correct
				// doesn't account for Y values in the Tip property.
				// I HATE TRIG
			}
		}

		// I know, I know, but this is actually really useful for the subclasses.
		// It's a vector you can use to get the velocity of a bullet shooting straight from the muzzel
		public Vector2 NormalizedShotVelocity {
			get {
				return Vector2.Normalize(new Vector2((float)Math.Cos(Sprite.Rotation), (float)Math.Sin(Sprite.Rotation)));
			}
		}
		protected Timer reloading;

		protected Gun(Sprite spr, List<Bullet> bullets, Texture2D bulletTex) : base(spr)
		{
			Sprite.Origin = Sprite.Relative(new Vector2(-0.2f, 0.5f), Sprite.Texture); //we want it to rotate around the handle
			tip = Sprite.Relative(new Vector2(1.0f, 0.1f), Sprite.Texture);
			this.bullets = bullets;
			this.bulletTex = bulletTex; //stupid, stupid, stupid. I need a giant hash table or something to hold all my resources.
			ready = true;
		}

		public override void Update() {
			base.Update();

			MouseState ms = Mouse.GetState();

			// every gun is semi-automatic, okay
			if (ms.LeftButton == ButtonState.Released) {
				Ready = true;
			}

			if (ms.X < Sprite.Position.X)
				Sprite.Flipped = true;
			else
				Sprite.Flipped = false;

			Sprite.Rotation = (float)Math.Atan2(ms.Y - Sprite.Position.Y, ms.X - Sprite.Position.X);


		}

		public abstract void Fire();

		// initially this code was in the subclasses, but ya gotta reuse dat functionality
		public void StartReloadTimer(int reloadTime) {
			if (reloading == null) {
				// This is extremely nice. Asynchronous timing! No need to do it frame by frame!
				reloading = new Timer(state => {
					Ready = true;
					((Timer)state).Dispose();
					reloading = null; // ...kinda stupid actually, really just functioning as a flag that it's okay to make another timer.
				});
				reloading.Change(reloadTime, Timeout.Infinite);
			}
		}
	}
}