using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		Random gen;

		Player ply;
		List<Enemy> enemies;
		List<Gun> arsenal;
		List<Bullet> bullets;

		Sprite crosshair;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			gen = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
			base.Initialize();
			IsFixedTimeStep = true; //don't want to worry about timing.
			graphics.PreferMultiSampling = false;

			bullets = new List<Bullet>();
			Texture2D bulletTex = Content.Load<Texture2D>("bullet");
			Texture2D enemyTex = Content.Load<Texture2D>("enemy");

			crosshair = new Sprite(Content.Load<Texture2D>("crosshair"));
			crosshair.Origin = Sprite.Relative(new Vector2(0.5f, 0.5f), crosshair.Texture);

			arsenal = new List<Gun>();
			arsenal.Add(
				new Rifle(
					new Sprite(Content.Load<Texture2D>("gun")),
					bullets,
					bulletTex
				)
			);
			arsenal.Add(
				new AssaultRifle(
					new Sprite(Content.Load<Texture2D>("ar")),
					bullets,
					bulletTex
				)
			);
			arsenal.Add(
				new Shotgun(
					new Sprite(Content.Load<Texture2D>("shotgun")),
					bullets,
					bulletTex
				)
			);

			ply = new Player(
				new Sprite(Content.Load<Texture2D>("player")),
				arsenal
			);

			enemies = new List<Enemy>();

			enemies.Add(
				new BrownianEnemy(
					new Sprite(enemyTex),
					GraphicsDevice.Viewport.Bounds
				)
			);
			enemies.Add(
				new LazyFollowingEnemy(
					new Sprite(enemyTex),
					GraphicsDevice.Viewport.Bounds,
					ply
				)
			);
			enemies.Add(
				new CrazyEnemy(
					new Sprite(enemyTex),
					GraphicsDevice.Viewport.Bounds
				)
			);

			//position the enemies randomly
			foreach (Enemy e in enemies) {
				e.Sprite.Position = new Vector2(
					(float)gen.Next(5, GraphicsDevice.Viewport.Width - e.Sprite.Texture.Width), 
					(float)gen.Next(5, GraphicsDevice.Viewport.Height - e.Sprite.Texture.Height)
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

            // TODO: use this.Content to load your game content here
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
			foreach (Bullet b in bullets) {
				b.Update();
			}
			foreach(Enemy e in enemies) {
				e.Update();
			}
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			//PointClamp disables anti-aliasing for that chunky pixel look
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);

			foreach (Enemy e in enemies) {
				e.Draw(spriteBatch);
			}
			foreach (Bullet b in bullets) {
				b.Draw(spriteBatch);
			}
			ply.Draw(spriteBatch);
			crosshair.Draw(spriteBatch);

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
