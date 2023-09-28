using System;
using System.Windows.Forms;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public partial class MainForm
	{
		private void NextMapGeneration(int score, int start)
		{
			const int patterns_num = 4;
			//Увеличиваем дистанцию по мере повышения очков
			int distance = score < 1000 ? Sources.static_tile.Size.Height*2 : score < 2000 ? Sources.static_tile.Size.Height*3 : score < 5000 ? Sources.static_tile.Size.Height*4 : score < 10000 ? Sources.static_tile.Size.Height*5 : Sources.static_tile.Size.Height*6;
			int position = start-distance;
			//Особые игровые события = раз в 10000 очков
			if(Math.Abs(GameEvents.lastEventScore - score) > 9999)
			{
				switch(systemRandom.Next(0,2))
				{
					case 0:
						if(GameEvents.lastEvent == eventType.eventFading)
							break;
						distance = Scaling.Round(80,"Height");
						position = start-distance;
						
						for(int i = 1; i <= 30; i++)
						{
							CreateBlock(new Fading(systemRandom.Next(0,Scaling.clientSize.Width-Sources.static_tile.Size.Width), position));
							position -= distance;
						}
						
						GameEvents.lastEvent = eventType.eventFading;
						if(!GameEvents.eventCompleted.Contains(eventType.eventFading))
							GameEvents.eventCompleted.Add(eventType.eventFading);
						GameEvents.lastEventScore = score;
						return;
					case 1:
						if(GameEvents.lastEvent == eventType.eventMoving)
							break;
						
						distance = Scaling.Round(100,"Height");
						position = start-distance;
						for(int i = 1; i <= 30; i++)
						{
							CreateBlock(new Depending(systemRandom.Next(0,Scaling.clientSize.Width-Sources.static_tile.Size.Width), position));
							position -= distance;
						}
						
						GameEvents.lastEvent = eventType.eventMoving;
						if(!GameEvents.eventCompleted.Contains(eventType.eventMoving))
							GameEvents.eventCompleted.Add(eventType.eventMoving);
						GameEvents.lastEventScore = score;
						return;
				}
			}

			switch(systemRandom.Next(0,(score/3000 > patterns_num-2) ? patterns_num : score/3000+2))
			{
				case 0:
					for(int i = 1; i <= 5; i++)
					{
						int x_pos = systemRandom.Next(0,Scaling.clientSize.Width-Sources.static_tile.Size.Width);
						CreateBlock(new Regular(x_pos, position));
						position -= distance;
					}
					break;
				case 1:
					for(int i = 1; i <= 10; i++)
					{
						if(i%3 == 0)
						{
							int x_pos = systemRandom.Next(0,Scaling.clientSize.Width-Sources.static_tile.Size.Width);
							CreateBlock(new Breakable(x_pos, position));
							CreateBlock(new Regular(x_pos > Scaling.clientSize.Width/2 ? systemRandom.Next(0,x_pos-Sources.static_tile.Size.Width) : systemRandom.Next(x_pos+Sources.static_tile.Size.Width,Scaling.clientSize.Width-Sources.static_tile.Size.Width), position+systemRandom.Next(-distance/2,distance/2)));
						}
						else
							CreateBlock(new Regular(systemRandom.Next(0,Scaling.clientSize.Width-Sources.static_tile.Size.Width), position));
						position -= distance;
					}
					CreateBlock(new Breakable(systemRandom.Next(Scaling.Round(20,"Width")+Sources.static_tile.Size.Width,Scaling.clientSize.Width-Sources.static_tile.Size.Width-Scaling.Round(20,"Width")), position, true, Scaling.Round(10,"Width"), Scaling.clientSize.Width-Scaling.Round(10,"Width")));
					break;
				case 2:
					CreateBlock(new Moving(position, systemRandom.Next(this.Width-Scaling.clientSize.Width, Scaling.clientSize.Width/2-Sources.moving_tile.Size.Width), systemRandom.Next(Scaling.clientSize.Width/2+Sources.moving_tile.Size.Width, Scaling.clientSize.Width-Sources.moving_tile.Size.Width)));
					break;
				case 3:
					for(int i = 1; i <= 11; i++)
					{
						if(i%3 == 0)
							CreateBlock(new Regular(systemRandom.Next(0,Scaling.clientSize.Width-Sources.static_tile.Size.Width), position));
						else
							CreateBlock(new Moving(position, systemRandom.Next(this.Width-Scaling.clientSize.Width, Scaling.clientSize.Width/2-Sources.moving_tile.Size.Width), systemRandom.Next(Scaling.clientSize.Width/2+Sources.moving_tile.Size.Width, Scaling.clientSize.Width-Sources.moving_tile.Size.Width)));
						position -= distance;
					}
					break;
			}
			GenerateObstacle(score);
		}
		private void GenerateObstacle(int score)
		{
			for(int i = block.Count-1; i >= 0; i--)
			{				
				if(block[i].y >= 0 || block[i].Type != platformType.Regular)
					continue;
				if(new Random().Next(30*((score/3000)+1)) <= 5)
				{
					CreateBlock(new Jetpack(block[i].x+systemRandom.Next(0,Sources.static_tile.Size.Width-(Sources.jetpack_tile.Size.Width+Sources.static_tile.Width/4)), block[i].y-Sources.jetpack_tile.Height/2,doodle.OnJetpackAttached));
					return;
				}
				if(new Random().Next(15*((score/3000)+1)) <= 5)
				{
					CreateBlock(new Tramp(block[i].x-Sources.tramp_tile.Width/4, block[i].y-Sources.tramp_tile.Height/2));
					return;
				}
				if(new Random().Next(5*((score/3000)+1)) <= 3)
				{
					CreateBlock(new Spring(block[i].x+systemRandom.Next(0,Sources.static_tile.Size.Width-(Sources.spring_tile.Size.Width+Scaling.Round(15,"Width"))), block[i].y-Sources.spring_tile.Height/2));
					return;
				}
			}
		}
		private void CreateBlock(Platform p)
		{
			block.Add(p);
			p.RemoveBlock += RemoveBlock;
			onUnitDraw += p.Draw;
			onUnitMove += p.PhysicMove;
			doodle.handler += p.OnIntersect;
		}
		private void CreateBlock(Obstacle p)
		{
			obstacles.Add(p);
			p.RemoveBlock += RemoveBlock;
			onUnitDraw += p.Draw;
			onUnitMove += p.PhysicMove;
			doodle.handler += p.OnIntersect;
		}		
		private void ClearBlock()
		{
			foreach(Platform p in block)
			{
				doodle.handler -= p.OnIntersect;
				onUnitDraw -= p.Draw;
				onUnitMove -= p.PhysicMove;
			}
			foreach(Obstacle p in obstacles)
			{
				doodle.handler -= p.OnIntersect;
				onUnitDraw -= p.Draw;
				onUnitMove -= p.PhysicMove;
			}
			block.Clear();
			obstacles.Clear();
		}
	 	private void RecreateGame()
	 	{			
	 		int step = Sources.static_tile.Size.Height*2;
			int generate_y = Scaling.clientSize.Height-step-Sources.static_tile.Size.Height;
			
			//Сбрасываем игрока в начальное состояние
	 		doodle.setPressedKey(Keys.ProcessKey);
	 		doodle.Move(Scaling.clientSize.Width/2, generate_y-Sources.doodle_right.Size.Height);
	 		doodle.gravity = -5;
	 		doodle.score = 0;
	 		keypressed = Keys.ProcessKey;
	 		GameEvents.lastEventScore = 0;
	 		GameEvents.lastEvent = eventType.eventNull;
	 		GameEvents.eventCompleted.Clear();

	 		//Очистка карты и создание первых блоков
	 		ClearBlock();

			CreateBlock(new Regular(doodle.x-Sources.static_tile.Size.Width/4, generate_y));
			generate_y -= step;
			
			while(generate_y >= -Scaling.Round(100,"Height"))
			{
				CreateBlock(new Regular(systemRandom.Next(this.Width-Scaling.clientSize.Width, Scaling.clientSize.Width-Sources.static_tile.Size.Width), generate_y));
				generate_y -= step;
			}
	 	}
	}
}
