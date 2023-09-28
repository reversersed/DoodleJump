using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public sealed class Moving : Platform
	{
		private int movingDirection = 1;
		private int movingSpeed = Scaling.clientSize.Width/5;
		private Point limitX = new Point(0, Scaling.clientSize.Width);
		public Moving(int y, int limitFirst = 0, int limitSecond = -1, int speed = 0) : base(limitFirst,y)
		{
			if(limitSecond == -1)
				limitSecond = Scaling.clientSize.Width;
			sprite = new Bitmap(Sources.moving_tile);
			sprite.MakeTransparent();
			tangible = true;
			limitX = new Point(limitFirst < limitX.X ? limitX.X : limitFirst, limitSecond > limitX.Y ? limitX.Y : limitSecond);
			movingSpeed = speed < 1 ? (limitSecond-limitFirst)/movingSpeed : speed;
			this.Type = platformType.Moving;
		}
		protected override void Behaviour()
		{
			this.x += movingDirection*movingSpeed;
			if(this.x+sprite.Size.Width > limitX.Y || this.x < limitX.X)
				movingDirection *= -1;
			return;
		}
		protected override void Intersect()
		{
			return;
		}
	}
}
