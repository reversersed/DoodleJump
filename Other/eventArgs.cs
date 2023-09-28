using System;
using System.Drawing;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	//События пересечений
	public sealed class IntersectEventArgs : EventArgs
	{
		public Point hitBox { get; private set; }
		public int bottom { get; private set; }
		public int gravity { get; private set; }
		public IntersectEventArgs(Point hitBox, int bottom, int gravity)
		{
			this.hitBox = hitBox;
			this.bottom = bottom;
			this.gravity = gravity;
		}
	}
	//Прыжок в Doodle
	public sealed class JumpingEventArgs : EventArgs
	{
		public int y;
		public int gravity;
		public int score;
		public JumpingEventArgs(int y, int gravity, int score)
		{
			this.y = y;
			this.score = score;
			this.gravity = gravity;
		}
	}
	//Событие на отрисовку
	public sealed class DrawEventArgs : EventArgs
	{
		public Graphics graphic;
		public DrawEventArgs(Graphics e)
		{
			graphic = e;
		}
	}
	//Событие на удаление платформ и бонусов
	public sealed class RemoveEventArgs : EventArgs
	{
		public Platform platform;
		public Obstacle obstacle;
		public RemoveEventArgs(Platform platform)
		{
			this.platform = platform;
			this.obstacle = null;
		}
		public RemoveEventArgs(Obstacle obstacle)
		{
			this.platform = null;
			this.obstacle = obstacle;
		}
	}
	//Событие на перемещение платформ и бонусов
	public sealed class UnitMoveEventArgs : EventArgs
	{
		public int moveDistance;
		public int removePosition;
		public UnitMoveEventArgs(int distance)
		{
			moveDistance = distance;
			removePosition = Scaling.clientSize.Height;
		}
		public UnitMoveEventArgs(int distance, int removePosition)
		{
			moveDistance = distance;
			this.removePosition = removePosition;
		}
	}
}
