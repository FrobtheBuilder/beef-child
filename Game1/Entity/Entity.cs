using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
	//an Entity has a Sprite, velocity and a hitbox rectangle. Collision should be able to be determined between any two entities.
	//uh, just use the sprite's metrics for positioning right now. It's even got an origin and everything

	//the hitbox's location is actually an offset to the top left of the sprite.
	//use PositionedHitBox to determine where it actually is

	public abstract class Entity
	{
		private Rectangle hitBox;
		//reeaaaaaaad onlyyyyyyyy
		public Rectangle PositionedHitBox {
			get {
				return new Rectangle((int)(sprite.TopLeft.X + hitBox.X), (int)(sprite.TopLeft.Y + hitBox.Y), hitBox.Width, hitBox.Height);
			}
		}

		private Vector2 velocity;
		public Vector2 Velocity {
			get {
				return velocity;
			}
			set {
				velocity = value;
			}
		}

		public Rectangle HitBox {
			get {
				return hitBox;
			}
			set {
				hitBox = value;
			}
		}



		private Sprite sprite;
		public Sprite Sprite {
			get {
				return sprite;
			}
			set {
				sprite = value;
			}
		}

		protected Entity(Sprite spr, Rectangle hbox)
		{
			sprite = spr;
			hitBox = hbox;
		}

		protected Entity(Sprite spr)
		{
			sprite = spr;

			//set the hitbox to the sprite texture bounds by default.
			hitBox = spr.Texture.Bounds; 
			//this way, by default, the hitbox will always just match the sprite.
		}
			
		//just apply velocity
		public virtual void Update() {
			sprite.Position += velocity;
		}

		//by default, just draw the sprite.
		public virtual void Draw(SpriteBatch sb)
		{
			sprite.Draw(sb);
		}

		public Entity Colliding(Entity other) {
			if (PositionedHitBox.Intersects(other.PositionedHitBox))
				return other;
			else
				return null;
		}
	}
}