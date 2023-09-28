using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public class Tramp : Obstacle
	{
		private int draw_iterations = 0;
		public Tramp(int x, int y) : base(x,y)
		{
			this.Type = IntersectType.Tramp;
			this.sprite = new Bitmap(Sources.tramp_tile);
		}
		protected override void Intersect()
		{
			this.sprite = new Bitmap(Sources.active_tramp_tile);
			this.draw_iterations = 1;
			this.y += Scaling.Round(2,"Height");
		}
		public override void Draw(DrawEventArgs g)
		{
			if(this.draw_iterations > 0 && this.draw_iterations <= 4)
			{
				if(++this.draw_iterations >= 4)
				{
					this.sprite = new Bitmap(Sources.final_tramp_tile);
					this.y -= Scaling.Round(2,"Height");
				}
			}
			base.Draw(g);
		}
	}
}
