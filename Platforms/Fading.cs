using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public sealed class Fading : Platform
	{
		private bool fading = false;
		private const float maxScale = (float)1.2;
		private const float minScale = (float)0.1;
		private float scaleStep = (float)0.1;
		public Fading(int x, int y) : base(x,y)
		{
			sprite = new Bitmap(Sources.fading_tile);
			sprite.MakeTransparent();
			tangible = true;
			this.Type = platformType.Fading;
		}
		protected override void Behaviour()
		{
			if(fading)
			{
				this.scale += scaleStep;
				if(this.scale >= maxScale)
					scaleStep *= -1;
				if(this.scale <= minScale)
				{
					this.y = Scaling.clientSize.Height+Sources.fading_tile.Size.Height;
					fading = false;
				}
			}
			return;
		}
		protected override void Intersect()
		{
			fading = true;
			return;
		}
	}
}
