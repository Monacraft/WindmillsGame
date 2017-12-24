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

		public static Vector2 screenCenter;
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			graphics.PreferredBackBufferWidth = 640;
			graphics.PreferredBackBufferHeight = 640;
			screenCenter = new Vector2(320, 320);
			graphics.IsFullScreen = false;
			graphics.ApplyChanges();
			Window.Title = "WindMills Game - Alpha 0.1";
			disconnect = false;
			canexit = false;
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>

		public static Texture2D triangle;
		Texture2D CrossHair;
		public static Texture2D circle;
		public static Texture2D[] bg;
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
			circle = Content.Load<Texture2D>("Graphics\\Circle");
			Arrow.tex = Content.Load<Texture2D>("Graphics\\Arrow");

			bg = new Texture2D[4];
			bg[0] = Content.Load<Texture2D>("Graphics\\rock-bg");
			bg[1] = Content.Load<Texture2D>("Graphics\\brown-rock-bg");
			bg[2] = Content.Load<Texture2D>("Graphics\\green-rock-bg");
			bg[3] = Content.Load<Texture2D>("Graphics\\magma-rock-bg");

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
		public static bool disconnect;
		public static bool canexit;
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{

				Exit();
			}
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
		protected override void OnExiting(Object sender, EventArgs args)
		{
            CloseGame();
			base.OnExiting(sender, args);
		}
		public void CloseGame()
		{
			disconnect = true;
			while (!canexit)
			{
			}
			Exit();
		}
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();
			//spriteBatch.Draw(triangle, new Vector2(screenWidth / 2, screenHeight / 2), null, null,
			//				 Vector2.Zero, 0, new Vector2(0.1f), Color.Red, SpriteEffects.None, 1);
			/// ###########
			/// HOW TO DRAW:
			/// s.Draw(texture, Position to draw, SourceRectangle, Colour, Rotation, Origin, Scale (Vector2), SpriteEffects, depth);
			/// ###########

			GameData.Draw(ref spriteBatch, ref CrossHair);
			spriteBatch.Draw(CrossHair, mouse, new Rectangle(108, 108, 40, 40),
							 Color.White, 0f, new Vector2(20, 20), new Vector2(0.8f), SpriteEffects.None, 1);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
