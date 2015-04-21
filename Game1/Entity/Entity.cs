using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1
{
	// an Entity has a Sprite, velocity and a hitbox rectangle.
	// Collision should be able to be determined between any two entities.
	// uh, just use the sprite's metrics for positioning right now.
	// It's even got an origin and everything

	//the hitbox's location is actually an offset to the top left of the sprite.
	//use PositionedHitBox to determine where it actually is

	public class CollideEventArgs : EventArgs
	{
		

		public Entity Other { get; set; }

		public CollideEventArgs(Entity other)
		{
			Other = other;
		}
	}

	public delegate void CollisionHandler(object source, CollideEventArgs e);
	public abstract class Entity : ISBDrawable
	{
		private bool enabled;
		public bool Enabled {
			get {
				return enabled;
			}
			set {
				enabled = value;
			}
		}

		private int collisionLayer;
		public int CollisionLayer {
			get { return collisionLayer; }
			set { collisionLayer = value; }
		}

		public Rectangle PositionedHitBox {
			get {
				return new Rectangle(
					(int)(Sprite.TopLeft.X + Position.X + hitBox.X),
					(int)(Sprite.TopLeft.Y + Position.Y + hitBox.Y),
					hitBox.Width,
					hitBox.Height
				);
			}
		}

		private Vector2 velocity;
		public Vector2 Velocity {
			get { return velocity; }
			set { velocity = value; }
		}

		protected Rectangle hitBox;
		public Rectangle HitBox {
			get { return hitBox; }
			set { hitBox = value; }
		}

		public Vector2 Position {
			get { return Sprite.Position; }
			set { Sprite.Position = value; }
		}

		private Sprite sprite;
		public Sprite Sprite {
			get { return sprite; }
			set { sprite = value; }
		}

		private DelayedList<Entity> collidesWith;
		public DelayedList<Entity> CollidesWith {
			get { return collidesWith; }
			set { collidesWith = value; }
		}

		private GameState state;
		public GameState State {
			get { return state; }
			set { state = value; }
		}

		protected List<Entity> collidingWith;
		public event CollisionHandler Collide;
		public event CollisionHandler StopCollide;

		protected Entity(Sprite spr, Rectangle hbox)
		{
			sprite = spr;
			hitBox = hbox;
		}

		protected Entity(Sprite spr, GameState g)
		{
			CollisionLayer = 0;
			enabled = false; //we will always set this when added to state
			sprite = spr;
			collidingWith = new List<Entity>();

			//set the hitbox to the sprite texture bounds by default.
			hitBox = spr.Texture.Bounds;
			//this way, by default, the hitbox will always just match the sprite.
		}

		public virtual void Initialize() {
			
		}

		public virtual void Update() {
			
			List<Entity> toRemove = new List<Entity>();

			foreach (var e in collidingWith) {
				if (!PositionedHitBox.Intersects(e.PositionedHitBox)) {
					if (StopCollide != null)
						StopCollide(this, new CollideEventArgs(e));
					toRemove.Add(e);
				}
			}

			foreach (Entity e in toRemove) {
				collidingWith.Remove(e);
			}

			foreach (var e in state.Entities) {
				if (PositionedHitBox.Intersects(e.PositionedHitBox)) {
					
					if (!collidingWith.Contains(e) && e != this 
						&& e.CollisionLayer == this.CollisionLayer) {
						if (Collide != null)
							Collide(this, new CollideEventArgs(e));
						collidingWith.Add(e);
					}
				}
			}

			Position += velocity;
		}

		//by default, just draw the sprite.
		public virtual void Draw(SpriteBatch sb)
		{
			if (Program.DEBUG)
				Debug.DrawRectangle(sb, PositionedHitBox, Color.Red);
			sprite.Draw(sb);
		}
	}
}