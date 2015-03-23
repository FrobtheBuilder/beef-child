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

		private List<Entity> bullets;
		public List<Entity> Bullets {
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

		private bool held;
		public bool Held {
			get { return held; }
			set { held = value; }
		}

		public Vector2 RotatedTipPosition {
			get {
				float hyp = Tip.X - Sprite.TopLeft.X;
				return Sprite.Position + new Vector2(
					hyp*(float)Math.Cos(Sprite.Rotation),
					hyp*(float)Math.Sin(Sprite.Rotation)
				);
				
				// I actually had to draw a triangle to 
				// figure this one out and it's still not completely correct
				// doesn't account for Y values in the Tip property.
				// I HATE TRIG
			}
		}

		// I know, I know, but this is actually really useful for the subclasses.
		// It's a vector you can use to get the 
		//velocity of a bullet shooting straight from the muzzel
		public Vector2 NormalizedShotVelocity {
			get {
				return Vector2.Normalize(new Vector2(
					(float)Math.Cos(Sprite.Rotation),
					(float)Math.Sin(Sprite.Rotation)
				));
			}
		}
		protected Timer reloading;

		protected Gun(Sprite spr, List<Entity> b) : base(spr)
		{
			Sprite.Origin = 
				Sprite.Relative(new Vector2(-0.2f, 0.5f), Sprite.Texture); 
			//we want it to rotate around the handle

			tip = 
				Sprite.Relative(new Vector2(1.0f, 0.1f), Sprite.Texture);
			this.bullets = b;
			this.bulletTex = Program.Game.Content.Load<Texture2D>("bullet");

			ready = true;
			held = false;
		}

		public override void Update() {
			base.Update();
			if (held) {
				MouseState ms = Mouse.GetState();

				// every gun is semi-automatic, okay
				if (ms.LeftButton == ButtonState.Released) {
					Ready = true;
				}

				if (ms.X < Sprite.Position.X)
					Sprite.Effect = SpriteEffects.FlipVertically;
				else
					Sprite.Effect = SpriteEffects.None;

				Sprite.Rotation = (float)Math.Atan2(
					ms.Y - Sprite.Position.Y,
					ms.X - Sprite.Position.X
				);
			}
		}

		public abstract void Fire();

		public void StartReloadTimer(int reloadTime) {
			if (reloading == null) {
				
				reloading = new Timer(state => {
					Ready = true;
					((Timer)state).Dispose();
					reloading = null; 
					// really just functioning as a flag 
					// that it's okay to make another timer.
				});
				reloading.Change(reloadTime, Timeout.Infinite);
			}
		}
	}
}