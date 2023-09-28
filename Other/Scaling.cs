using System;
using System.Collections.Generic;
using System.Drawing;

namespace Doodle_Jump.Other
{
	public static class Scaling
	{
		public static Dictionary<string, double> scalingSize { get; private set; }
		public static Size clientSize;
		public static int topFloor { get; private set; }
		
		private static Size screenSize;
		private static Size defaultScreenSize = new Size(1366, 768);
		
		//Инициализация коэффициентов
		public static void InitializeScreenScaling(Size screensize)
		{
			scalingSize = new Dictionary<string, double>();
			
			screenSize = screensize;
			
			scalingSize["Width"] = (double)screenSize.Width/defaultScreenSize.Width;
			scalingSize["Height"] = (double)screenSize.Height/defaultScreenSize.Height;
			
			topFloor = (int)(Math.Round(scalingSize["Height"]*100));
		}
		//Применение масштабирования к значениям и типам Size и Point
		public static int Round(int value, string align)
		{
			return (int)(Math.Round(scalingSize[align]*value));
		}
		public static Size Round(Size value)
		{
			return new Size((int)(Math.Round(scalingSize["Width"]*value.Width)),(int)(Math.Round(scalingSize["Height"]*value.Height)));
		}
		public static Point Round(Point value)
		{
			return new Point((int)(Math.Round(scalingSize["Width"]*value.X)),(int)(Math.Round(scalingSize["Height"]*value.Y)));
		}
	}
}
