using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
	
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Random gen;

		Player ply;

		List<Enemy> enemies;
		List<Gun> arsenal;

		public DelayedList<Entity> world;
		Sprite crosshair;
		SpriteFont font;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			gen = new Random();
		}

		protected override void Initialize()
		{
			base.Initialize();
			font = Content.Load<SpriteFont>("Arial");
			IsFixedTimeStep = true; //don't want to worry about timing.
			graphics.PreferMultiSampling = false;

			Texture2D bulletTex = Content.Load<Texture2D>("bullet");
			Texture2D enemyTex = Content.Load<Texture2D>("enemy");

			crosshair = new Sprite(Content.Load<Texture2D>("crosshair"));

			crosshair.Origin = Sprite.Relative(
				new Vector2(0.5f, 0.5f),
				crosshair.Texture
			);
				
			world = new DelayedList<Entity>();

			world.ImmediateAdd(
				new Rifle(
					new Sprite(Content.Load<Texture2D>("gun")),
					world
				)
			);
			world.ImmediateAdd(
				new AssaultRifle(
					new Sprite(Content.Load<Texture2D>("ar")),
					world
				)
			);
			world.ImmediateAdd(
				new Shotgun(
					new Sprite(Content.Load<Texture2D>("shotgun")),
					world
				)
			);

			enemies = new List<Enemy>();

			arsenal = new List<Gun>();
			ply = new Player(
				new Sprite(Content.Load<Texture2D>("player")),
				arsenal,
				world
			);
			for (int i = 0; i < 10; i++) {
				enemies.Add(
					new BrownianEnemy(
						new Sprite(enemyTex),
						GraphicsDevice.Viewport.Bounds,
						world,
						ply
					)
				);
			}

			for (int i = 0; i < 3; i++) {
				enemies.Add(
					new LazyFollowingEnemy(
						new Sprite(enemyTex),
						GraphicsDevice.Viewport.Bounds,
						ply,
						world,
						ply
					)
				);
			}

			for (int i = 0; i < 12; i++) {
				enemies.Add(
					new CrazyEnemy(
						new Sprite(enemyTex),
						GraphicsDevice.Viewport.Bounds,
						world,
						ply
					)
				);
			}

			foreach (Enemy e in enemies) {
				world.ImmediateAdd(e);
			}

			foreach (Entity e in world) {
				e.Sprite.Position = new Vector2(
					(float)gen.Next(5, GraphicsDevice.Viewport.Width - 
						e.Sprite.Texture.Width), 
					(float)gen.Next(5, GraphicsDevice.Viewport.Height - 
						e.Sprite.Texture.Height)
				);
			}
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			ply.Update();

			MouseState ms = Mouse.GetState();
			crosshair.Position = new Vector2(ms.X, ms.Y);

			foreach (Entity e in world)
				e.Update();
			world.ApplyRemovals();
			world.ApplyAdditions();
			// this is disgusting.
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			//PointClamp disables anti-aliasing for that chunky pixel look
			spriteBatch.Begin(
				SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				SamplerState.PointClamp,
				null, null, null
			);

			foreach (Entity e in world)
				e.Draw(spriteBatch);


			ply.Draw(spriteBatch);
			crosshair.Draw(spriteBatch);
			spriteBatch.DrawString(font, ply.Health.ToString(), new Vector2(5, 5), Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}