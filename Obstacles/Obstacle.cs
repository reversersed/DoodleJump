using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public abstract class Obstacle
	{
		public Bitmap sprite { get; protected set; }
		public int x, y;
		public IntersectType Type {get; protected set;}
		public event RemoveUnit RemoveBlock;
		
		protected Obstacle(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public virtual void Draw(DrawEventArgs g)
		{
			g.graphic.DrawImage(sprite, x+(sprite.Size.Width/2), y-(sprite.Size.Height/2), sprite.Size.Width, sprite.Size.Height);
		}
		public IntersectType OnIntersect(IntersectEventArgs e)
		{
			if(y < Scaling.clientSize.Height && y >= Scaling.topFloor &&
			  	(x <= e.hitBox.Y && e.hitBox.X <= x+sprite.Size.Width &&
			    e.bottom <= y+this.sprite.Size.Height && e.bottom+Math.Abs(e.gravity) >= y))
			{
				this.Intersect();
				return Type;
			}
			return IntersectType.Null;
		}
		public void PhysicMove(UnitMoveEventArgs args)
		{
			this.y += args.moveDistance;
			if((this.y > args.removePosition && args.moveDistance > 0) || (this.y < args.removePosition && args.moveDistance < 0))
				RemoveBlock.Invoke(new RemoveEventArgs(this));
		}
		protected abstract void Intersect();
	}
}
