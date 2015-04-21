using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game1
{
	public class TestState : GameState
	{

		public TestState(Game game) : base(game)
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public override void Initialize() 
		{
			Console.WriteLine("Initialized");
		}
	}
}
