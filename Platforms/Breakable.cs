using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public sealed class Breakable : Platform
	{
		private int break_state = 0;
		private bool breaking = false;
		private const int break_loop = 2;
		private int movingDirection = 1;
		private int movingSpeed = Scaling.clientSize.Height/5;
		private bool movable = false;
		private readonly int fallSpeed = Scaling.Round(10,"Height");
		private Point limitX = new Point(0, Scaling.clientSize.Width);
		public Breakable(int x, int y, bool movable = false, int mx = 0, int my = 385) : base(x,y)
		{
			sprite = new Bitmap(Sources.breakable_tile[0]);
			sprite.MakeTransparent();
			limitX = new Point(mx < limitX.X ? limitX.X : mx, my > limitX.Y ? limitX.Y : my);
			if(mx > x || x > my)
				this.x = mx;
			movingSpeed = (mx-my)/movingSpeed;
			this.movable = movable;
			tangible = false;
			this.Type = platformType.Breakable;
		}
		protected override void Behaviour()
		{
			if(movable)
			{
				this.x += movingDirection*movingSpeed;
				if(this.x+sprite.Size.Width > limitX.Y || this.x < limitX.X)
					movingDirection *= -1;
			}
			if(!breaking)
				return;
			if((break_state)/break_loop >= Sources.breakable_tile.Length)
				this.y += fallSpeed;
			else
				sprite = new Bitmap(Sources.breakable_tile[(break_state++)/break_loop]);
			return;
		}
		protected override void Intersect()
		{
			if(!breaking)
			{
				breaking = true;
				Sources.break_sound.Play();
			}
			if(movable)
				movable = false;
			return;
		}
	}
}
