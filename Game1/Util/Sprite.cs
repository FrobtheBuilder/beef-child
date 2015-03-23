using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
	public class Sprite
	{
		public static Vector2 Relative(Vector2 vec, Texture2D tex)
		{
			Vector2 vec2 = Vector2.Multiply(
				vec,
				new Vector2((float)tex.Width, (float)tex.Height)
			);
			
			vec2 = new Vector2(
				(float)Math.Round(vec2.X),
				(float)Math.Round(vec2.Y)
			);
			return vec2;
		}

		private Texture2D texture;
		public Texture2D Texture {
			get { return texture; }
			set { texture = value; }
		}

		private Color color;
		public Color Color {
			get { return color; }
			set { color = value; }
		}

		private Vector2 position;
		public Vector2 Position {
			get { return position; }
			set { position = value; }
		}

		private Vector2 origin;
		public Vector2 Origin {
			get { return origin; }
			set { origin = value; }
		}

		public Vector2 TopLeft {
			get {
				return -origin;
			}
		}

		private float rotation;
		public float Rotation {
			get { return rotation; }
			set { rotation = value; }
		}

		private SpriteEffects effect;
		public SpriteEffects Effect {
			get { return effect; }
			set { effect = value; }
		}

		public Sprite() {
			this.texture = null;
			position = new Vector2(0, 0);
			origin = new Vector2(0, 0);
			rotation = 0.0f;
		}

		public Sprite(Texture2D t)
		{
			this.texture = t;
			position = new Vector2(0, 0);
			origin = new Vector2(0, 0);
			rotation = 0.0f;
			color = Color.White;
		}

		public void Draw(SpriteBatch sb) {
			sb.Draw(
				Texture,
				position,
				null,
				null,
				Origin,
				Rotation,
				null,
				this.Color,
				Effect
			);
		}
	}
}

