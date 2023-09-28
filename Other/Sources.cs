using System;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Linq;
using System.Media;
using Doodle_Jump.Other;

namespace Doodle_Jump
{
	public static class Sources
	{
		private readonly static ResourceManager resources = new ResourceManager("Doodle_Jump.Resources", Assembly.GetExecutingAssembly());
		private readonly static System.Drawing.Text.PrivateFontCollection FontCollection = new System.Drawing.Text.PrivateFontCollection();
		
		public static Bitmap doodle_right {get;private set;}
		public static Bitmap static_tile {get;private set;}
		public static Bitmap moving_tile {get;private set;}
		public static Bitmap fading_tile {get;private set;}
		public static Bitmap[] breakable_tile {get;private set;}
		public static Bitmap depending_tile {get;private set;}
		public static Bitmap spring_tile {get;private set;}
		public static Bitmap active_spring_tile {get;private set;}
		public static Bitmap tramp_tile {get;private set;}
		public static Bitmap active_tramp_tile {get;private set;}
		public static Bitmap final_tramp_tile {get;private set;}
		public static Bitmap jetpack_tile {get;private set;}

		public static Bitmap background {get;private set;}
		public static Bitmap background_paused {get;private set;}
		public static Bitmap background_menu {get;private set;}
		public static Bitmap doodle_logo {get;private set;}
		
		public static Bitmap cancel_button {get;private set;}
		public static Bitmap cancel_button_hovered {get;private set;}
		
		public static Bitmap done_button {get;private set;}
		public static Bitmap done_button_hovered {get;private set;}

		public static Bitmap play_button {get;private set;}
		public static Bitmap play_button_hovered {get;private set;}
		
		public static Bitmap menu_button {get;private set;}
		public static Bitmap menu_button_hovered {get;private set;}
		
		public static Bitmap records_button {get;private set;}
		public static Bitmap records_button_hovered {get;private set;}
		
		public readonly static SoundPlayer jump_sound = new SoundPlayer(new System.IO.MemoryStream((byte[])resources.GetObject("jump-sound")));
		public readonly static SoundPlayer break_sound = new SoundPlayer(new System.IO.MemoryStream((byte[])resources.GetObject("lomise")));
		public readonly static SoundPlayer start_sound = new SoundPlayer(new System.IO.MemoryStream((byte[])resources.GetObject("start")));
		public readonly static SoundPlayer falling_sound = new SoundPlayer(new System.IO.MemoryStream((byte[])resources.GetObject("pada")));
		public readonly static SoundPlayer spring_sound = new SoundPlayer(new System.IO.MemoryStream((byte[])resources.GetObject("feder")));
		public readonly static SoundPlayer jetpack_sound = new SoundPlayer(new System.IO.MemoryStream((byte[])resources.GetObject("jetpack")));
		public readonly static SoundPlayer tramp_sound = new SoundPlayer(new System.IO.MemoryStream((byte[])resources.GetObject("tramp")));
		
		public static Font gameFont20 {get;private set;}
		public static Font gameFont32 {get;private set;}
		public static StringFormat centeredFormat = new StringFormat();

	 	private static Bitmap CropImage(Image source, int x,int y,int width,int height)
	 	{
		    Rectangle crop = new Rectangle(x, y, width, height);
		
		    var bmp = new Bitmap(crop.Width, crop.Height);
		    using (var gr = Graphics.FromImage(bmp))
		    {
		        gr.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
		    }
		    return new Bitmap(bmp,Scaling.Round(bmp.Size));
		}
	 	public static void Initialize()
	 	{	
			doodle_right = CropImage((Image)resources.GetObject("soccer-right"),16,15,46,45);
			static_tile = CropImage((Image)resources.GetObject("game-tiles"), 0,0,60,18);
			moving_tile = CropImage((Image)resources.GetObject("game-tiles"), 0,18,60,18);
			fading_tile = CropImage((Image)resources.GetObject("game-tiles"), 0,18*3,60,18);
			breakable_tile = new Bitmap[]{CropImage((Image)resources.GetObject("game-tiles"), 0,72,62,18),CropImage((Image)resources.GetObject("game-tiles"), 0,90,62,26),CropImage((Image)resources.GetObject("game-tiles"), 0,116,62,28),CropImage((Image)resources.GetObject("game-tiles"), 0,144,62,38)};
			depending_tile = CropImage((Image)resources.GetObject("game-tiles"), 513,129,57,15);
			spring_tile = CropImage((Image)resources.GetObject("game-tiles"), 404,98,18,13);
			active_spring_tile = CropImage((Image)resources.GetObject("game-tiles"), 404,115,18,28);
			tramp_tile = CropImage((Image)resources.GetObject("game-tiles"), 186,96,38,16);
			active_tramp_tile = CropImage((Image)resources.GetObject("game-tiles"), 472,52,38,18);
			final_tramp_tile = CropImage((Image)resources.GetObject("game-tiles"), 148,92,38,20);
			jetpack_tile = CropImage((Image)resources.GetObject("game-tiles"), 197,264,26,36);
	 		
			background = new Bitmap((Image)resources.GetObject("background"),Scaling.Round(((Image)resources.GetObject("background")).Size));
			background_paused = new Bitmap((Image)resources.GetObject("pause-cover"),Scaling.Round(((Image)resources.GetObject("pause-cover")).Size));
			background_menu = new Bitmap((Image)resources.GetObject("default-cover"),Scaling.Round(((Image)resources.GetObject("default-cover")).Size));
			doodle_logo = new Bitmap((Image)resources.GetObject("doodle-jump"),Scaling.Round(((Image)resources.GetObject("doodle-jump")).Size));
	 		
			cancel_button = new Bitmap((Image)resources.GetObject("cancel"),Scaling.Round(((Image)resources.GetObject("cancel")).Size));
			cancel_button_hovered = new Bitmap((Image)resources.GetObject("cancel-on"),Scaling.Round(((Image)resources.GetObject("cancel-on")).Size));
		
			done_button = new Bitmap((Image)resources.GetObject("done"),Scaling.Round(((Image)resources.GetObject("done")).Size));
			done_button_hovered = new Bitmap((Image)resources.GetObject("done-on"),Scaling.Round(((Image)resources.GetObject("done-on")).Size));

			play_button = new Bitmap((Image)resources.GetObject("play"),Scaling.Round(((Image)resources.GetObject("play")).Size));
			play_button_hovered = new Bitmap((Image)resources.GetObject("play-on"),Scaling.Round(((Image)resources.GetObject("play-on")).Size));
		
			menu_button = new Bitmap((Image)resources.GetObject("menu"),Scaling.Round(((Image)resources.GetObject("menu")).Size));
			menu_button_hovered = new Bitmap((Image)resources.GetObject("menu-on"),Scaling.Round(((Image)resources.GetObject("menu-on")).Size));
			
			records_button = new Bitmap((Image)resources.GetObject("records"),Scaling.Round(((Image)resources.GetObject("records")).Size));
			records_button_hovered = new Bitmap((Image)resources.GetObject("records-on"),Scaling.Round(((Image)resources.GetObject("records-on")).Size));
			
	 		centeredFormat.Alignment = StringAlignment.Center;
			centeredFormat.LineAlignment = StringAlignment.Center;
	 		
	 		int fontLength = ((byte[])resources.GetObject("GameFont")).Length;
        	byte[] fontdata = (byte[])resources.GetObject("GameFont");
        	
        	System.IntPtr data = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontLength);
        	System.Runtime.InteropServices.Marshal.Copy(fontdata, 0, data, fontLength);
        	
        	FontCollection.AddMemoryFont(data, fontLength);
        	gameFont20 = new Font(FontCollection.Families.First(), Scaling.Round(20,"Width"), FontStyle.Bold, GraphicsUnit.Pixel);
	 		gameFont32 = new Font(FontCollection.Families.First(), Scaling.Round(32,"Width"), FontStyle.Bold, GraphicsUnit.Pixel);
	 	}
	};
}
