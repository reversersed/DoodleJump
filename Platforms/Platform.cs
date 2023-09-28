using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public abstract class Platform
	{
		public Bitmap sprite {get; protected set; }
		public int x, y;
		public bool tangible {get; protected set; }
		public platformType Type { get; protected set; }
		public event RemoveUnit RemoveBlock;
		protected float scale;
		
		protected Platform(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.scale = (float)1.0;
		}
		public void Draw(DrawEventArgs g)
		{
			this.Behaviour();
			g.graphic.DrawImage(sprite, x+(sprite.Size.Width*(1-scale)/2), y-(sprite.Size.Height*(1-scale)/2), sprite.Size.Width*scale, sprite.Size.Height*scale);
		}
		public IntersectType OnIntersect(IntersectEventArgs e)
		{
			if(y < Scaling.clientSize.Height && y >= Scaling.topFloor &&
			  	(x <= e.hitBox.Y && e.hitBox.X <= x+sprite.Size.Width &&
			    e.bottom <= y+this.sprite.Size.Height && e.bottom+Math.Abs(e.gravity) >= y))
			{
				this.Intersect();
				return (this.Type != platformType.Breakable ? IntersectType.Platform : IntersectType.Null);
			}
			return IntersectType.Null;
		}
		public void PhysicMove(UnitMoveEventArgs args)
		{
			this.y += args.moveDistance;
			if((this.y > args.removePosition && args.moveDistance > 0) || (this.y < args.removePosition && args.moveDistance < 0))
				RemoveBlock.Invoke(new RemoveEventArgs(this));
		}
		protected abstract void Behaviour();
		protected abstract void Intersect();
	}
}
