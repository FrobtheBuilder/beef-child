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

		private List<Gun> guns;
		private Gun equipped;

		public Player(Sprite spr, List<Gun> guns) : base(spr)
		{
			Sprite.Origin = Sprite.Relative(new Vector2(0.5f, 0.6f), Sprite.Texture); //center origin
			speed = 3;

			this.guns = guns;
			equipped = guns[0];

			lastFrame = Keyboard.GetState(); //we don't want this to be null
		}

		public override void Update() 
		{
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
			}
			else if (thisFrame.IsKeyDown(Keys.E) && !lastFrame.IsKeyDown(Keys.E)) {
				NextGun();
			}

			MouseState ms = Mouse.GetState();

			if (ms.LeftButton == ButtonState.Pressed) {
				if (equipped.Ready)
					equipped.Fire();
			}

			equipped.Sprite.Position = Sprite.Position;
			equipped.Update();

			lastFrame = thisFrame;
		}

		public void NextGun() {
			if (guns.IndexOf(equipped) < guns.Count-1) {
				equipped = guns[guns.IndexOf(equipped) + 1];
			}
			else {
				equipped = guns[0];
			}
		}

		public void PrevGun() {
			if (guns.IndexOf(equipped) > 0) {
				equipped = guns[guns.IndexOf(equipped) - 1];
			}
			else {
				equipped = guns[guns.Count - 1];
			}
		}

		public override void Draw(SpriteBatch sb) {
			base.Draw(sb);
			equipped.Draw(sb);
		}
	}
}

