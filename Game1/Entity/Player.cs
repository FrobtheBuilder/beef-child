using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Game1;

namespace Game1
{
	public class Player : Entity
	{
		private float speed;
		private KeyboardState lastFrame;

		public float Speed {
			get { return speed; }
			set { speed = value; }
		}
		private int health;
		public int Health {
			get {
				return health;
			}
			set {
				health = value;
			}
		}

		private bool dead;
		public bool Dead {
			get {
				return dead;
			}
			set {
				dead = value;
			}
		}

		private List<Gun> guns;
		private Gun equipped;

		public Player(Sprite spr, List<Gun> guns, DelayedList<Entity> tCl) : base(spr, tCl)
		{
			Sprite.Origin = Sprite.Relative(
				new Vector2(0.5f, 0.6f),
				Sprite.Texture
			);

			speed = 3;
			health = 20;
			dead = false;
			this.guns = guns;
			if (guns.Count > 0)
				equipped = guns[0];

			lastFrame = Keyboard.GetState(); //we don't want this to be null

			Collide += (source, e) => {
				var gun = e.Other as Gun;
				if (gun != null) {
					gun.Held = true;
					guns.Add(gun);
					if (equipped == null)
						equipped = gun;
					// this ""singleton"" will have to do until I integrate gamestates
					// and yes I am just using "singleton" as a word for a global variable
					// because it makes me feel better about myself
					Program.Game.world.Remove(e.Other);
				}
				if (e.Other is Enemy || 
						(e.Other is Bullet && ((Bullet)e.Other).Unfriendly)) {
					health -= 1;
					if (Program.DEBUG)
						Console.WriteLine("Hit");
					if (health <= 0) {
						dead = true;
					}
					if (e.Other is Bullet && ((Bullet)e.Other).Unfriendly) {
						Program.Game.world.Remove(e.Other);
					}
				}
			};
		}

		public override void Update() 
		{
			if (!dead) {
				base.Update();

				Velocity *= new Vector2(0.5f, 0.5f); //"friction"
				KeyboardState thisFrame = Keyboard.GetState();

				if (thisFrame.IsKeyDown(Keys.W)) {
					Velocity += new Vector2(0, -speed);
				}
				if (thisFrame.IsKeyDown(Keys.S)) {
					Velocity += new Vector2(0, speed);
				}
				if (thisFrame.IsKeyDown(Keys.A)) {
					Velocity += new Vector2(-speed, 0);
				}
				if (thisFrame.IsKeyDown(Keys.D)) {
					Velocity += new Vector2(speed, 0);
				}

				// ghetto implementation of the key pressed event
				if (thisFrame.IsKeyDown(Keys.Q) && !lastFrame.IsKeyDown(Keys.Q)) {
					PrevGun();
				} else if (thisFrame.IsKeyDown(Keys.E) && !lastFrame.IsKeyDown(Keys.E)) {
					NextGun();
				}

				MouseState ms = Mouse.GetState();
				if (equipped != null) {
					if (ms.LeftButton == ButtonState.Pressed) {
						if (equipped.Ready)
							equipped.Fire();
					}

					equipped.Sprite.Position = Sprite.Position;
					equipped.Update();
				}


				lastFrame = thisFrame;
			}
		}

		public void NextGun() {
			if (equipped != null) {
				if (guns.IndexOf(equipped) < guns.Count - 1) {
					equipped = guns[guns.IndexOf(equipped) + 1];
				} else {
					equipped = guns[0];
				}
			}
		}

		public void PrevGun() {
			if (equipped != null) {
				if (guns.IndexOf(equipped) > 0) {
					equipped = guns[guns.IndexOf(equipped) - 1];
				}
				else {
					equipped = guns[guns.Count - 1];
				}
			}
		}

		public override void Draw(SpriteBatch sb) {
			if (!dead) {
				base.Draw(sb);
				if (equipped != null)
					equipped.Draw(sb);
			}
		}
	}
}

