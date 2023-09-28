using System;
using System.Drawing;

namespace Doodle_Jump
{
	public sealed class Regular : Platform
	{
		public Regular(int x, int y) : base(x,y)
		{
			sprite = new Bitmap(Sources.static_tile);
			sprite.MakeTransparent();
			tangible = true;
			Type = platformType.Regular;
		}
		protected override void Behaviour()
		{
			return;
		}
		protected override void Intersect()
		{
			return;
		}
	}
}
