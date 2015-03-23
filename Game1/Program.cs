#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Game1
{
	/// <summary>
	/// The main class.
	/// </summary>
	public static class Program
	{
		public static Game1 Game;
		public static bool DEBUG;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (var game = new Game1()) {
				Game = game;
				// uhh, call it a singleton.
				DEBUG = true;
				game.Run();
			}
		}
	}
}
