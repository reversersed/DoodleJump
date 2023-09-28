using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public sealed class Depending : Platform
	{
		private int newPosition;
		private int movingDirection;
		private int movingSpeed = Scaling.Round(10,"Height");
		private static event EventHandler internalBehaviour;
		public Depending(int x, int y) : base(x,y)
		{
			sprite = new Bitmap(Sources.depending_tile);
			sprite.MakeTransparent();
			tangible = true;
			newPosition = 0;
			this.Type = platformType.Depending;
			movingSpeed = Scaling.Round((new Random().Next(1000000)%10+6),"Height");
			internalBehaviour += this.DoInternalThing;
		}
		~Depending()
		{
			internalBehaviour -= this.DoInternalThing;
		}
		protected override void Behaviour()
		{
			if(newPosition == 0)
				return;
			newPosition--;
			
			if(movingDirection == -1)
			{
				if(x-movingSpeed < 0)
					x = 0;
				else
					x -= movingSpeed;
			}
			else
			{
				if(x+movingSpeed > Scaling.clientSize.Width-sprite.Size.Width)
					x = Scaling.clientSize.Width-sprite.Size.Width;
				else
					x += movingSpeed;
			}
			return;
		}
		private void DoInternalThing(object sender, EventArgs e)
		{
			this.newPosition = new Random().Next(60,120)/10;
			if(x < Scaling.Round(50,"Width"))
				this.movingDirection = 1;
			else if(x > Scaling.clientSize.Width-sprite.Size.Width-Scaling.Round(50,"Width"))
				this.movingDirection = -1;
			else
				this.movingDirection = new Random().Next(10,100)%new Random().Next(3,8) <= 2 ? 1 : -1;
		}
		protected override void Intersect()
		{
			if(internalBehaviour != null)
				internalBehaviour.Invoke(this, new EventArgs());
			return;
		}
	}
}
