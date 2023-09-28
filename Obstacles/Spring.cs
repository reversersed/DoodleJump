using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public class Spring : Obstacle
	{
		int animate_tick = 0;
		public Spring(int x, int y) : base(x,y)
		{
			this.Type = IntersectType.Spring;
			this.sprite = new Bitmap(Sources.spring_tile);
		}
		protected override void Intersect()
		{
			this.sprite = new Bitmap(Sources.active_spring_tile);
			this.y -= Scaling.Round(10,"Height");
			this.animate_tick = 5;
		}
		public override void Draw(DrawEventArgs g)
		{
			if(animate_tick > 0 && animate_tick-- == 1)
			{
				this.sprite = new Bitmap(Sources.spring_tile);
				this.y += Scaling.Round(10,"Height");
			}
			
			base.Draw(g);
		}
	}
}
