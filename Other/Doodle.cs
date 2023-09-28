using System;
using System.Drawing;
using System.Linq;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public sealed class Doodle
	{
		private readonly int basicGravity = Scaling.Round(20,"Height");
		private readonly int movingSpeed = Scaling.Round(6,"Width");
		private Bitmap sprite;
		private float scoreScale = (float)1.0;
		private int lastScore = 0;
		private System.Windows.Forms.Keys lastDirection;
		private int flyGravity;
		private event EventHandler<EventArgs> gameLose;
		private event EventHandler<JumpingEventArgs> doodleJumping;
		private readonly string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+(@"\DoodleJumpSave.bin");
		private System.Windows.Forms.Keys direction = System.Windows.Forms.Keys.ProcessKey;
		private Jetpack attachedPack = null;
		private string[] bestScore = {"- 0","- 0","- 0","- 0","- 0","- 0","- 0","- 0","- 0","- 0"};
		public string doodleName = String.Empty;
		public RemoveUnit removeJetpack = null;
		public event HasIntersection handler;
		public int score;
		public int gravity;
		public int x;
		public int y;
		public Doodle(EventHandler<EventArgs> OnGameLose, EventHandler<JumpingEventArgs> OnDoodleJump, RemoveUnit remove)
		{
			this.x = Scaling.clientSize.Width/2;
			this.y = Scaling.clientSize.Height-Scaling.clientSize.Height/3;
			sprite = new Bitmap(Sources.doodle_right);
			sprite.MakeTransparent();
			lastDirection = System.Windows.Forms.Keys.D;
			doodleJumping = OnDoodleJump;
			gameLose = OnGameLose;
			removeJetpack = remove;
			
			this.loadData();
		}
		~Doodle()
		{
			this.saveData();
		}
		public void OnJetpackAttached(Jetpack pack)
		{
			attachedPack = pack;
		}
		//Перемещение и проверка пересечений, отрабатывает перед отрисовкой
		private void Move()
		{
			if(direction == System.Windows.Forms.Keys.Attn)
				return;
			if(y >= Scaling.topFloor || gravity < 0)
				y -= gravity;
			if(gravity > -basicGravity && flyGravity <= 0)
				gravity--;
			else if(flyGravity > 0)
			{
				if(--flyGravity <= 0 && attachedPack != null)
				{
					removeJetpack.Invoke(new RemoveEventArgs(attachedPack));
					attachedPack = null;
				}
			}
			if(this.gravity > 0 && y < Scaling.topFloor)
				this.score += gravity/2;
			
			if(direction == System.Windows.Forms.Keys.A)
			{
				if(lastDirection != direction)
					sprite.RotateFlip(RotateFlipType.RotateNoneFlipX);
				lastDirection = direction;
				
				x -= movingSpeed;
				if(x < -sprite.Size.Width)
					x = Scaling.clientSize.Width;
			}
			else if(direction == System.Windows.Forms.Keys.D)
			{
				if(lastDirection != direction)
					sprite.RotateFlip(RotateFlipType.RotateNoneFlipX);
				lastDirection = direction;
			
				x += movingSpeed;
				if(x > Scaling.clientSize.Width)
					x = -sprite.Size.Width;
			}
			if(attachedPack != null)
			{
				attachedPack.x = lastDirection == System.Windows.Forms.Keys.D ? this.x-(int)(Sources.jetpack_tile.Size.Width/1.2)+(direction == System.Windows.Forms.Keys.ProcessKey ? 0 : movingSpeed) : this.x+(int)(Sources.jetpack_tile.Size.Width/1.6)-(direction == System.Windows.Forms.Keys.ProcessKey ? 0 : movingSpeed);
				attachedPack.y = this.y;
			}
			
			if(gravity > 0 && doodleJumping != null)
				doodleJumping.Invoke(this, new JumpingEventArgs(this.y, this.gravity, this.score));
			if(gravity < 0 && handler != null)
			{
				IntersectEventArgs args = new IntersectEventArgs(
					new Point(x+(lastDirection == System.Windows.Forms.Keys.D ? 0 : Scaling.Round(16,"Width")),
					          x+(lastDirection == System.Windows.Forms.Keys.D ? Scaling.Round(30,"Width") : sprite.Size.Width)),
					y+sprite.Size.Height, gravity);
				foreach(HasIntersection i in handler.GetInvocationList())
				{
					switch(i.Invoke(args))
					{
						case IntersectType.Jetpack:
							this.setFly(Scaling.Round(130,"Height"));
							Sources.jetpack_sound.Play();
							break;
						case IntersectType.Tramp:
							this.setFly(Scaling.Round(25,"Height"));
							Sources.tramp_sound.Play();
							break;
						case IntersectType.Spring:
							this.setFly(Scaling.Round(15,"Height"));
							Sources.spring_sound.Play();
							break;
						case IntersectType.Platform:
							gravity = basicGravity;
							Sources.jump_sound.Play();
							break;
					}
				}
			}
			if(y > Scaling.clientSize.Height+Sources.doodle_right.Size.Height && gameLose != null)
			{
				gameLose.Invoke(this, new EventArgs());
			}
		}
		public void UpdateRecords(object sender,EventArgs e)
		{
			if(doodleName.Length < 1)
				return;
			
			int index = -1;
			if((index = Array.FindIndex(bestScore, w => w.Split(' ')[0].Equals(doodleName))) != -1)
			{
				if(Convert.ToInt32(bestScore[index].Split(' ')[1]) < score)
				{
					bestScore[index] = "- 0";
					for(int i = index+1; i < 10; i++)
						bestScore[i-1] = bestScore[i];
					bestScore[9] = "- 0";
				}
				else
					return;
			}
			
			int idx = Array.FindIndex(bestScore, w => Convert.ToInt32(w.Split(' ')[1]) < score);
			if(idx == -1)
				return;
			for(int i = 9; i > idx; i--)
				bestScore[i] = bestScore[i-1];
			bestScore[idx] = string.Format("{0} {1}",doodleName,score);
		}
		public void DrawRecords(Graphics g)
		{
			g.DrawString("BEST PLAYERS:",Sources.gameFont32, new SolidBrush(Color.Maroon), Scaling.clientSize.Width/2,Scaling.clientSize.Height/5, Sources.centeredFormat);

			for(int i = 0; i < 10; i++)
			{
				g.DrawString(String.Format("{0}. {1}",i+1, bestScore[i].Split(' ')[0]),Sources.gameFont20, new SolidBrush(Color.DarkRed), Scaling.clientSize.Width/6, Scaling.clientSize.Height/5+((i+1)*Sources.gameFont20.Height));
				g.DrawString(String.Format("\t\t\t{0}",bestScore[i].Split(' ')[1]),Sources.gameFont20, new SolidBrush(Color.DarkRed), Scaling.clientSize.Width/6, Scaling.clientSize.Height/5+((i+1)*Sources.gameFont20.Height));
			}
		}
		public void setPressedKey(System.Windows.Forms.Keys key)
		{
			this.direction = key;
		}
		public void Move(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public void Draw(Graphics g)
		{
			this.Move();
			
			g.DrawImage(sprite, x, y);
			
			if(lastScore < score || scoreScale > 1.0)
				scoreScale += (float)(lastScore < score ? scoreScale < 1.3 ? 0.05 : -0.01 : -0.08);
			if(scoreScale < 1)
				scoreScale = 1;
			
			g.RotateTransform((float)(scoreScale-1)*30);
			g.ScaleTransform(scoreScale, scoreScale);
			g.DrawString("Score: " + this.score,Sources.gameFont20,new SolidBrush(Color.DimGray), 5, 25);
			g.ResetTransform();
			
			lastScore = score;
		}
		private void loadData()
		{
			try {
				using(System.IO.BinaryReader reader = new System.IO.BinaryReader(new System.IO.FileStream(dataPath,System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read)))
				{
					if(reader.PeekChar() != -1)
					{
						for(int i = 0; i < 10; i++)
							bestScore[i] = reader.ReadString();
						doodleName = reader.ReadString();
					}
				}
			}
			catch(Exception)
			{
				System.IO.File.Delete(dataPath);
				System.IO.File.Create(dataPath);
			}
		}
		private void saveData()
		{
			using(System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.FileStream(dataPath,System.IO.FileMode.Truncate, System.IO.FileAccess.Write)))
			{
				foreach(string s in bestScore)
					writer.Write(s);
				writer.Write(doodleName);
			}
		}
		private void setFly(int flyGravity)
		{
			this.gravity = basicGravity;
			this.flyGravity = flyGravity;
		}
	}
}