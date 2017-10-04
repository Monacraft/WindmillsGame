using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		GraphicsDevice device;
		SpriteBatch spriteBatch;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			graphics.PreferredBackBufferWidth = 640;
			graphics.PreferredBackBufferHeight = 640;
			graphics.IsFullScreen = false;
			graphics.ApplyChanges();
			Window.Title = "WindMills Game - Alpha 0.1";
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>

		Texture2D triangle;
		Texture2D CrossHair;
		public static Texture2D pixel;
		public static Vector2 mouse;
		int screenHeight;
		int screenWidth;
		public static Data GameData;
		public static int MyID;
		public static string Name;
		public static string IP;
		public static Player myPlayer;
		Client client;
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			device = graphics.GraphicsDevice;
			//TODO: use this.Content to load your game content here 
			GameData = new Data();
			Name = "Jash";
			MyID = -1;

			// ToDo: Load all textures in Data or Player and add Load() to them
			triangle = Content.Load<Texture2D>("Graphics\\triangle-tip");
			CrossHair = Content.Load<Texture2D>("Graphics\\crosshair");
			pixel = new Texture2D(GraphicsDevice, 1, 1);
			pixel.SetData<Color>(new Color[] { Color.White });

			screenWidth = device.PresentationParameters.BackBufferWidth;
			screenHeight = device.PresentationParameters.BackBufferHeight;

			myPlayer = new Player();
			myPlayer.Activate(Name, MyID);
			//myPlayer.colID = -1;
			//Console.WriteLine("Select Colour: ");
			Console.Write("IP: ");
			IP = Console.ReadLine();
			client = new Client(IP, 11000);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>

		public KeyboardState k;
		public MouseState m;
		public KeyboardState ok;
		public MouseState om;
		public static bool update;
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif
			ok = k;                         // Store Old States
			om = m;
			k = Keyboard.GetState();        // Get New States
			m = Mouse.GetState();
			if (MyID == -1)
			{
				Console.WriteLine("NB: NO ID Assigned");
			}
			else
			{
				GameData.Update(MyID, ref k, ref m, ref ok, ref om);
			}
			mouse.X = m.X;
			mouse.Y = m.Y;
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			//spriteBatch.Draw(triangle, new Vector2(screenWidth / 2, screenHeight / 2), null, null,
			//				 Vector2.Zero, 0, new Vector2(0.1f), Color.Red, SpriteEffects.None, 1);


			GameData.Draw(ref spriteBatch, ref triangle);

			spriteBatch.Draw(CrossHair, mouse, new Rectangle(108,108,40,40),
							 Color.Black, 0f, new Vector2(20,20), new Vector2(1f), SpriteEffects.None, 1);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
