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
			triangle = Content.Load<Texture2D>("Graphics\\triangle");
			screenWidth = device.PresentationParameters.BackBufferWidth;
			screenHeight = device.PresentationParameters.BackBufferHeight;

			myPlayer = new Player();
			myPlayer.Activate(Name, MyID);
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
		public static bool update;
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			k = Keyboard.GetState();
			if (MyID == -1)
			{
				update = true;
				if (MyID == -1)
				{
					Console.WriteLine("Error, NO ID Assigned");
				}
			}
			else
			{
				//Console.WriteLine("But a chance");
				//AsynchronousClient.SendData(GameData.Players[MyID].ClientSend());				
				if (k.IsKeyDown(Keys.W))
					GameData.Players[MyID].y-=3;
				if (k.IsKeyDown(Keys.S))
					GameData.Players[MyID].y+=3;
				if (k.IsKeyDown(Keys.A))
					GameData.Players[MyID].x-=3;		
				if (k.IsKeyDown(Keys.D))
					GameData.Players[MyID].x+=3;
				/*if (k.IsKeyDown(Keys.W))
					myPlayer.y-=3;
				if (k.IsKeyDown(Keys.S))
					myPlayer.y+=3;
				if (k.IsKeyDown(Keys.A))
					myPlayer.x-=3;		
				if (k.IsKeyDown(Keys.D))
					myPlayer.x+=3;*/
			}
			update = true;
			//client.Send(GameData.Players[MyID].ClientSend());
			// TODO: Add your update logic here
			//AsynchronousClient.UpdateSync.Set();
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
			spriteBatch.Draw(triangle, new Vector2(screenWidth / 2, screenHeight / 2), null, null, Vector2.Zero, 0, new Vector2(0.1f), Color.Red,SpriteEffects.None, 1);
			GameData.Draw(ref spriteBatch, ref triangle);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
