using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public partial class MainForm : Form
	{
		private Keys keypressed = Keys.ProcessKey;
		private Doodle doodle;
		private List<Platform> block = new List<Platform>();
		private List<Obstacle> obstacles = new List<Obstacle>();
		//Кнопки, которые игрок может использовать во время игры
		private readonly List<Keys> ValidKeys = new List<Keys>(){Keys.A, Keys.D};
		private readonly string ValidAlphabet = "QWERTYUIOPASDFGHJKLZXCVBNM";
		private Bitmap background;
		private int background_y = -200;
		private System.Timers.Timer mainTimer = new System.Timers.Timer(20);
		private gameStatus gameStatus = gameStatus.inMenu;
		private Random systemRandom = new Random();
		private gameEvents GameEvents;
		private Point gameOverPosition = new Point(0,0);

		//Отрисовка объектов (кроме  игрока)
		private delegate void dUnitDraw(DrawEventArgs args);
		private event dUnitDraw onUnitDraw;
		
		//Перемещение объектов (кроме игрока)
		private delegate void dUnitMove(UnitMoveEventArgs args);
		private event dUnitMove onUnitMove;
		
		//Обновление рекордов
		public event EventHandler UpdateRecords;
		public MainForm()
		{			
			InitializeComponent();
			
			//Масштабирование окна
			Scaling.InitializeScreenScaling(Screen.PrimaryScreen.Bounds.Size);
			this.Width = (int)(Math.Round(this.Width*Scaling.scalingSize["Width"]));
			this.Height = (int)(Math.Round(this.Height*Scaling.scalingSize["Height"]));
			Scaling.clientSize = this.ClientSize;
			
			Sources.Initialize();
			
			RestartButton.Size = Scaling.Round(RestartButton.Size);
			RestartButton.Location = new Point(Scaling.clientSize.Width/2-RestartButton.Size.Width/2,Scaling.clientSize.Height-Scaling.clientSize.Height/3);
			
			OverCloseButton.Size = Scaling.Round(OverCloseButton.Size);
			OverCloseButton.Location = new Point(Scaling.clientSize.Width/2-OverCloseButton.Size.Width/2,Scaling.clientSize.Height-Scaling.clientSize.Height/6);
			
			ContinueButton.Size = Scaling.Round(ContinueButton.Size);
			ContinueButton.Location = new Point(Scaling.clientSize.Width/2-ContinueButton.Size.Width/2,Scaling.clientSize.Height-Scaling.clientSize.Height/3);
			
			PlayButton.Size = Scaling.Round(PlayButton.Size);
			PlayButton.Location = new Point(Scaling.clientSize.Width/2-PlayButton.Size.Width/2,Scaling.clientSize.Height-(int)(Scaling.clientSize.Height/1.8));
			
			ShutdownButton.Size = Scaling.Round(ShutdownButton.Size);
			ShutdownButton.Location = new Point(Scaling.clientSize.Width/2-ShutdownButton.Size.Width/2,Scaling.clientSize.Height-Scaling.clientSize.Height/4);
			
			recordsButton.Size = Scaling.Round(recordsButton.Size);
			recordsButton.Location = new Point(Scaling.clientSize.Width/2-recordsButton.Size.Width/2,Scaling.clientSize.Height-Scaling.clientSize.Height/3);
			
			recordsCancel.Size = Scaling.Round(recordsCancel.Size);
			recordsCancel.Location = new Point(Scaling.clientSize.Width/2-recordsCancel.Size.Width/2,Scaling.clientSize.Height-Scaling.clientSize.Height/4);
			
			background_y = Scaling.Round(-200, "Height");
			
			//Инициализация игрока
			doodle = new Doodle(OnGameOver, OnDoodleJump, this.RemoveBlock);
			UpdateRecords += doodle.UpdateRecords;
			background = new Bitmap(Sources.background);
			
			mainTimer.Elapsed += MainTimerTick;
			
    		this.SetStyle(ControlStyles.DoubleBuffer, true);
    		GameEvents.eventCompleted = new List<eventType>();
		}
		private void MainTimerTick(object source, System.Timers.ElapsedEventArgs e)
		{
			if(gameStatus != gameStatus.gameFalling && gameStatus != gameStatus.gameRunning)
				return;
			if(gameStatus == gameStatus.gameFalling)
			{
				if(onUnitMove != null)
					onUnitMove.Invoke(new UnitMoveEventArgs(-Scaling.Round(20,"Height"), -Scaling.Round(40,"Height")));
				if(block.Count > 0 || obstacles.Count > 0)
				{
					doodle.y = doodle.y > Scaling.clientSize.Height/2 ? doodle.y-Scaling.Round(20,"Height") : doodle.y > Scaling.topFloor ? doodle.y-Scaling.Round(5,"Height") : doodle.y;
					doodle.x = doodle.x > Scaling.clientSize.Width/2 ? doodle.x-Scaling.Round(10,"Width") < Scaling.clientSize.Width/2 ? Scaling.clientSize.Width/2 : doodle.x-Scaling.Round(10,"Width") : doodle.x < Scaling.clientSize.Width/2 ? doodle.x+Scaling.Round(10,"Width") > Scaling.clientSize.Width/2 ? Scaling.clientSize.Width/2 : doodle.x+Scaling.Round(10,"Width") : Scaling.clientSize.Width/2;
					background_y = background_y < Scaling.Round(-400, "Height") ? Scaling.Round(-200, "Height") : background_y-Scaling.Round(20,"Height"); 
				}
				else
				{
					doodle.y = doodle.y < Scaling.clientSize.Height ? doodle.y+Scaling.Round(20,"Height") : Scaling.clientSize.Height;
					gameOverPosition.X = gameOverPosition.X > Scaling.Round(20,"Height") ? gameOverPosition.X-Scaling.Round(25,"Height") : Scaling.Round(20,"Height");
					gameOverPosition.Y = gameOverPosition.Y > Scaling.clientSize.Height/8 ? gameOverPosition.Y-Scaling.Round(25,"Height") : Scaling.clientSize.Height/8;

					if(gameOverPosition.X == Scaling.Round(20,"Height") && gameOverPosition.Y == Scaling.clientSize.Height/8)
					{
						gameStatus = gameStatus.gameOver;
						mainTimer.Stop();
					}
				}
			}
			this.Invalidate();
		}
	}
	
}
