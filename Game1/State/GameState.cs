using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Game1
{
	public class GameState
	{
		private bool enabled;
		public bool Enabled {
			get { return enabled; }
			set { enabled = value; }
		}

		public List<Entity> entities;
		public List<Entity> Entities {
			get { return entities; }
		}

		private Game1 game;
		public Game1 Game {
			get { return game; }
			set { game = value; }
		}

		//kind of a pseudo-event
		//anything added to this action will be done at the end of the frame
		//this is to prevent list lock conflicts
		private Action endFrameTasks;
		public Action EndFrameTasks {
			get {
				return endFrameTasks;
			}
			set {
				endFrameTasks = value;
			}
		}

		public GameState(Game g)
		{
			game = (Game1)g;
			entities = new List<Entity>();
		}
			
		//called upon activation
		public virtual void Initialize() {
			
		}

		public void RemoveEntity(Entity e) {
			endFrameTasks += () => {
				e.Enabled = false;
				entities.Remove(e);
			};
		}

		public void AddEntity(Entity e) {
			endFrameTasks += () => {
				entities.Add(e);
				e.Enabled = true;
				e.State = this;
				e.Initialize();
			};
		}

		public virtual void Update()
		{
			foreach (Entity e in entities)
				e.Update();

			//might actually be beginning-of-next-frame tasks
			//depending on where you put the base call
			if (endFrameTasks != null) {
				endFrameTasks();
				endFrameTasks = null;
			}
		}

		public virtual void Draw(SpriteBatch sb)
		{
			foreach (ISBDrawable d in entities)
				d.Draw(sb);
		}

		//not sure if this is really needed
		//any entity can just go up the chain to find the game
		public void Swap(GameState to)
		{
			game.CurrentState = to;
		}

		public abstract void CleanUp();
	}
}
