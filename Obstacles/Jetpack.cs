using System;
using System.Drawing;

namespace Doodle_Jump
{
	public class Jetpack : Obstacle
	{
		public delegate void OnAttachToDoodle(Jetpack obj);
		public event OnAttachToDoodle OnJetpackAttach;
		public Jetpack(int x, int y, OnAttachToDoodle attachHandler) : base(x,y)
		{
			this.Type = IntersectType.Jetpack;
			this.sprite = new Bitmap(Sources.jetpack_tile);
			this.OnJetpackAttach += attachHandler;
		}
		~Jetpack()
		{
			this.OnJetpackAttach = null;
		}
		protected override void Intersect()
		{
			if(OnJetpackAttach != null)
				OnJetpackAttach.Invoke(this);
		}
	}
}