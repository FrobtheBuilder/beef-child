using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
	
	
	public static class Debug
	{
		public static Texture2D SimpleTexture = 
			new Texture2D(
				Program.Game.GraphicsDevice,
				1,
				1, 
				false,
				SurfaceFormat.Color
			);
		public static void DrawRectangle(SpriteBatch sb, Rectangle r, Color c)
		{
			SimpleTexture.SetData<Color>(new Color[] { Color.White });
			sb.Draw(SimpleTexture, r, c);
		}
	}
}