using System;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
	// for things that are drawable with a spritebatch
	public interface ISBDrawable
	{
		void Draw(SpriteBatch sb);
	}
}
