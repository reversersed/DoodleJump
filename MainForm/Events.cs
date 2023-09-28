using System;
using System.Drawing;
using System.Windows.Forms;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public delegate IntersectType HasIntersection(IntersectEventArgs args);
	public delegate void RemoveUnit(RemoveEventArgs sender);

	public partial class MainForm
	{
		void MainFormPaint(object sender, PaintEventArgs e)
		{
			switch(gameStatus)
			{
				//Игра идет
				case gameStatus.gameRunning:
					e.Graphics.DrawImage(background,0,background_y);
					try
					{
						if(onUnitDraw != null)
						   onUnitDraw.Invoke(new DrawEventArgs(e.Graphics));
					}
					catch(Exception ex)
					{
						MessageBox.Show("Application throwed an exception:\n"+ex.Message+"\n\nPress OK to abord application", "Exception", MessageBoxButtons.OK);
						this.Dispose();
						Environment.Exit(0);
						return;
					}
					finally
					{
						doodle.Draw(e.Graphics);
					}
					break;
				//Игрок в меню
				case gameStatus.inMenu:
					e.Graphics.DrawImage(Sources.background_menu,0,0,Scaling.clientSize.Width,Scaling.clientSize.Height);
					
					recordsButton.Enabled = true;
					recordsButton.Show();
					PlayButton.Enabled = true;
					PlayButton.Show();
					ShutdownButton.Enabled = true;
					ShutdownButton.Show();
					break;
				//Игрок падает (конец игры)
				case gameStatus.gameFalling:
					e.Graphics.DrawImage(background,0,background_y);
					e.Graphics.DrawImage(Sources.doodle_logo, Scaling.Round(10,"Height"), gameOverPosition.X);//20
				
					e.Graphics.DrawString(String.Format("Game Over!\n\nYour score: {0}\n\nEnter your name:",doodle.score), Sources.gameFont32, new SolidBrush(Color.Maroon), Scaling.clientSize.Width/2-(Sources.gameFont32.Size*4), gameOverPosition.Y);
					
					for(int i = 0; i < 15; i++)
					{
						if(doodle.doodleName.Length > i)
							e.Graphics.DrawString(doodle.doodleName[i].ToString(),Sources.gameFont32,new SolidBrush(Color.DarkRed),Scaling.clientSize.Width/2-(int)(Sources.gameFont32.Size*4.5)+(i*(int)(Sources.gameFont32.Size/1.5)), gameOverPosition.Y+Sources.gameFont32.Height*6,Sources.centeredFormat);
						e.Graphics.DrawString("_",Sources.gameFont32,new SolidBrush(Color.DarkRed),Scaling.clientSize.Width/2-(int)(Sources.gameFont32.Size*4.5)+(i*(int)(Sources.gameFont32.Size/1.5)), gameOverPosition.Y+Sources.gameFont32.Height*6,Sources.centeredFormat);
					}
					
					try
					{
						if(onUnitDraw != null)
						   onUnitDraw.Invoke(new DrawEventArgs(e.Graphics));
					}
					catch(Exception ex)
					{
						MessageBox.Show("Application throwed an exception:\n"+ex.Message+"\n\nPress OK to abord application", "Exception", MessageBoxButtons.OK);
						this.Dispose();
						Environment.Exit(0);
						return;
					}
					finally
					{
						doodle.Draw(e.Graphics);
					}
					break;
				//Конец игры, ввод имени
				case gameStatus.gameOver:
					e.Graphics.DrawImage(background,0,background_y);
					e.Graphics.DrawImage(Sources.doodle_logo, Scaling.Round(10,"Height"), gameOverPosition.X);

					e.Graphics.DrawString(String.Format("Game Over!\n\nYour score: {0}\n\nEnter your name:",doodle.score), Sources.gameFont32, new SolidBrush(Color.Maroon), Scaling.clientSize.Width/2-(Sources.gameFont32.Size*4), gameOverPosition.Y);
					
					for(int i = 0; i < 15; i++)
					{
						if(doodle.doodleName.Length > i)
							e.Graphics.DrawString(doodle.doodleName[i].ToString(),Sources.gameFont32,new SolidBrush(Color.DarkRed),Scaling.clientSize.Width/2-(int)(Sources.gameFont32.Size*4.55)+(i*(int)(Sources.gameFont32.Size/1.5)), gameOverPosition.Y+Sources.gameFont32.Height*6,Sources.centeredFormat);
						e.Graphics.DrawString("_",Sources.gameFont32,new SolidBrush(Color.DarkRed),Scaling.clientSize.Width/2-(int)(Sources.gameFont32.Size*4.5)+(i*(int)(Sources.gameFont32.Size/1.5)), gameOverPosition.Y+Sources.gameFont32.Height*6,Sources.centeredFormat);
					}
					RestartButton.Enabled = true;
					RestartButton.Show();
					OverCloseButton.Enabled = true;
					OverCloseButton.Show();
					break;
				//Игра на паузе
				case gameStatus.gamePaused:
					e.Graphics.DrawImage(Sources.background_paused, 0, 0, Scaling.clientSize.Width, Scaling.clientSize.Height);
					
					e.Graphics.DrawString("Score\n"+doodle.score, Sources.gameFont20, new SolidBrush(Color.Maroon), Scaling.clientSize.Width-(int)(Sources.gameFont20.Size*3.5)-Scaling.Round(10,"Width"), Scaling.clientSize.Height/2-Scaling.Round(80,"Height"), Sources.centeredFormat);
					
					ContinueButton.Enabled = true;
					ContinueButton.Show();
					OverCloseButton.Enabled = true;
					OverCloseButton.Show();
					break;
				//Таблица рекордов
				case gameStatus.recordsTable:
					e.Graphics.DrawImage(Sources.background_menu,0,0,Scaling.clientSize.Width,Scaling.clientSize.Height);
					doodle.DrawRecords(e.Graphics);
					
					recordsCancel.Enabled = true;
					recordsCancel.Show();
					break;
				default:
					throw new Exception("Game status is not valid");
			}
		}
		//Игрок прыгает вверх(летит)
		void OnDoodleJump(object sender, JumpingEventArgs e)
		{
			if(e.y < Scaling.topFloor)
			{
				background_y = background_y >= 0 ? -Scaling.Round(200,"Height") : background_y+e.gravity;
				
				if(onUnitMove != null)
					onUnitMove.Invoke(new UnitMoveEventArgs(doodle.gravity));
				
				if(block[block.Count-1].y >= -Scaling.Round(60,"Height"))
					NextMapGeneration(e.score, block[block.Count-1].y);
			}
		}
		//Игрок проиграл, события в doodle
		private void OnGameOver(object sender, EventArgs e)
		{
			if(gameStatus == gameStatus.gameFalling)
				return;
			doodle.setPressedKey(Keys.Attn);
			Sources.falling_sound.Play();
			gameStatus = gameStatus.gameFalling;
			
			gameOverPosition.X = Scaling.Round(50,"Height")+Scaling.clientSize.Height;
			gameOverPosition.Y = Scaling.clientSize.Height/8+Scaling.clientSize.Height;
			
			this.Invalidate();
		}
		void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				if(gameStatus == gameStatus.gamePaused)
					ContinueButtonClick(new Object(), new EventArgs());
				else if(gameStatus == gameStatus.gameRunning)
				{
					keypressed = Keys.ProcessKey;
					doodle.setPressedKey(Keys.ProcessKey);
					gameStatus = gameStatus.gamePaused;
					mainTimer.Stop();
					this.Invalidate();
				}
				return;
			}
			if(gameStatus == gameStatus.gameRunning && keypressed != e.KeyCode && ValidKeys.Contains(e.KeyCode))
			{
				keypressed = e.KeyCode;
				doodle.setPressedKey(e.KeyCode);
			}
			if(gameStatus == gameStatus.gameOver)
			{
				if(e.KeyCode == Keys.Back)
				{
					if(doodle.doodleName.Length > 0)
						doodle.doodleName = doodle.doodleName.Remove(doodle.doodleName.Length-1,1);
				}
				else if(doodle.doodleName.Length < 15 && ValidAlphabet.Contains(e.KeyCode.ToString()))
					doodle.doodleName += e.Shift ? char.ToUpper(e.KeyCode.ToString()[0]) : char.ToLower(e.KeyCode.ToString()[0]);
				this.Invalidate();
			}
	        e.Handled = true;
	        e.SuppressKeyPress = true;
		}
		void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			if(keypressed == e.KeyCode)
			{
				doodle.setPressedKey(Keys.ProcessKey);
				keypressed = Keys.ProcessKey;
			}
	        e.Handled = true;
	        e.SuppressKeyPress = true;
		}
		void RestartButtonClick(object sender, EventArgs e)
		{
			UpdateRecords.Invoke(sender, e);
			
			RecreateGame();
			
			Sources.start_sound.Play();
			
			gameStatus = gameStatus.gameRunning;
			mainTimer.Start();
			
			OverCloseButton.Enabled = false;
			OverCloseButton.Hide();
			RestartButton.Enabled = false;
			RestartButton.Hide();
		}
		private void RemoveBlock(RemoveEventArgs sender)
		{
			if(sender.platform != null)
			{
				doodle.handler -= sender.platform.OnIntersect;
				onUnitDraw -= sender.platform.Draw;
				onUnitMove -= sender.platform.PhysicMove;
				block.Remove(sender.platform);
			}
			else if (sender.obstacle != null)
			{
				doodle.handler -= sender.obstacle.OnIntersect;
				onUnitDraw -= sender.obstacle.Draw;
				onUnitMove -= sender.obstacle.PhysicMove;
				obstacles.Remove(sender.obstacle);
			}
		}
		void RecordsButtonClick(object sender, EventArgs e)
		{
			gameStatus = gameStatus.recordsTable;
			this.Invalidate();
			
			recordsButton.Enabled = false;
			recordsButton.Hide();
			PlayButton.Enabled = false;
			PlayButton.Hide();
			ShutdownButton.Enabled = false;
			ShutdownButton.Hide();
		}
		void RecordsCancelButtonClick(object sender, EventArgs e)
		{
			gameStatus = gameStatus.inMenu;
			this.Invalidate();
			
			recordsCancel.Enabled = false;
			recordsCancel.Hide();
		}
		void OverCloseButtonClick(object sender, EventArgs e)
		{
			if(gameStatus == gameStatus.gamePaused)
			{
				this.OnGameOver(this,new EventArgs());
				mainTimer.Start();
				
				ContinueButton.Enabled = false;
				ContinueButton.Hide();
				OverCloseButton.Enabled = false;
				OverCloseButton.Hide();
				RestartButton.Enabled = false;
				RestartButton.Hide();
				return;
			}
			
			
			UpdateRecords.Invoke(sender, e);
			
			gameStatus = gameStatus.inMenu;
			mainTimer.Stop();
			this.Invalidate();
			
			ContinueButton.Enabled = false;
			ContinueButton.Hide();
			OverCloseButton.Enabled = false;
			OverCloseButton.Hide();
			RestartButton.Enabled = false;
			RestartButton.Hide();
		}
		void ContinueButtonClick(object sender, EventArgs e)
		{
			if(gameStatus != gameStatus.gamePaused)
				throw new Exception("Game status exception: Game is not paused");
			
			gameStatus = gameStatus.gameRunning;
			mainTimer.Start();
			ContinueButton.Enabled = false;
			ContinueButton.Hide();
			OverCloseButton.Enabled = false;
			OverCloseButton.Hide();
		}
		void PlayButtonClick(object sender, EventArgs e)
		{
			RecreateGame();
			
			Sources.start_sound.Play();
			
	    	gameStatus = gameStatus.gameRunning;
			mainTimer.Start();
			
			recordsButton.Enabled = false;
			recordsButton.Hide();
			PlayButton.Enabled = false;
			PlayButton.Hide();
			ShutdownButton.Enabled = false;
			ShutdownButton.Hide();
		}
		void RecordsButtonMouseEnter(object sender, EventArgs e)
		{
			recordsButton.BackgroundImage = Sources.records_button_hovered;
		}
		void RecordsButtonMouseLeave(object sender, EventArgs e)
		{
			recordsButton.BackgroundImage = Sources.records_button;
		}
		void RecordsCancelButtonMouseEnter(object sender, EventArgs e)
		{
			recordsCancel.BackgroundImage = Sources.cancel_button_hovered;
		}
		void RecordsCancelButtonMouseLeave(object sender, EventArgs e)
		{
			recordsCancel.BackgroundImage = Sources.cancel_button;
		}
		void OverCloseButtonMouseEnter(object sender, EventArgs e)
		{
			OverCloseButton.BackgroundImage = Sources.menu_button_hovered;
		}
		void OverCloseButtonMouseLeave(object sender, EventArgs e)
		{
			OverCloseButton.BackgroundImage = Sources.menu_button;
		}
		void ContinueButtonMouseEnter(object sender, EventArgs e)
		{
			ContinueButton.BackgroundImage = Sources.done_button_hovered;
		}
		void ContinueButtonMouseLeave(object sender, EventArgs e)
		{
			ContinueButton.BackgroundImage = Sources.done_button;
		}
		void PlayButtonMouseEnter(object sender, EventArgs e)
		{
			PlayButton.BackgroundImage = Sources.play_button_hovered;
		}
		void PlayButtonMouseLeave(object sender, EventArgs e)
		{
			PlayButton.BackgroundImage = Sources.play_button;
		}
		void ShutdownButtonClick(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}
		void ShutdownButtonMouseEnter(object sender, EventArgs e)
		{
			ShutdownButton.BackgroundImage = Sources.cancel_button_hovered;
		}
		void ShutdownButtonMouseLeave(object sender, EventArgs e)
		{
			ShutdownButton.BackgroundImage = Sources.cancel_button;
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
		}
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("Молчанов Артём\nГруппа 2-42В\n\nИГЭУ © 2023","About",MessageBoxButtons.OK);
		}
		void RulesToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(gameStatus == gameStatus.gameRunning)
				gameStatus = gameStatus.gamePaused;
			this.Invalidate();
			while(true)
			{
				if(MessageBox.Show("Управление:\n\nA - движение влево\nD - движение вправо\nESC - пауза/продолжить\n\nПерейти на следующую страницу?","Rules",MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					if(MessageBox.Show("Цель игры:\n\nПродвинуться как можно выше и не упасть\n\nИгровые события:\n\nПо достижении 10.000 очков появляется специальный участок особых платформ\n\nПлатформы:\n\nЗеленые - обычная, без эффектов\nСиняя - передвигается по экрану\nКоричневая - ломается при наступании, может двигаться\nБелая - исчезает после одного прыжка\nОранжевая - все платформы этого типа меняют свое положение после прыжка по любой из них\n\nЗапись рекордов:\n\nПосле конца игры используйте латинские буквы на клавиатуре (A-Z) для записи своего имени. Для стирания букв используйте клавишу backspace.\n\nБонусы в игре:\n\nПружинка - игрок слегка подпрыгивает\nТрамплин - игрок подлетает на среднюю высоту\nДжетпак - игрок пролетает значительное расстояние вверх\n\nВернуться на предыдущую страницу?","Rules",MessageBoxButtons.YesNo) == DialogResult.Yes)
						continue;
				}
				break;
			}
		}
	}
}
