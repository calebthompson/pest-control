using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace pest_control
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Menu menu;
        MenuView menuView;
        EventQueue eventQueue;
        View currentView;
        Controller currentController;
        World world;
        WorldView worldView;
        Player player;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.ToggleFullScreen();
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.ApplyChanges();
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
            eventQueue = new EventQueue();

            player = new Player(1,this,eventQueue);

            menu = new Menu(eventQueue);
            menuView = new MenuView(GraphicsDevice,menu);

            currentView = menuView;
            currentController = menu;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            menuView.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            player.Update();
            currentController.Update();

            while (!eventQueue.IsQueueEmpty("SYSTEM"))
            {
                Event e = eventQueue.DequeueEvent("SYSTEM");
                switch(e.getName())
                {
                    case "NEW_GAME":
                        world = new World(30, 5000, 5000, 1, eventQueue);
                        worldView = new WorldView(GraphicsDevice,world);
                        worldView.LoadContent(Content);
                        currentController = world;
                        currentView = worldView;
                        break;
                    case "TERMINATE":
                        Exit();
                        break;
                    case "MENU":
                        if (currentController == menu)
                        {
                            if (world == null) break;

                            currentController = world;
                            currentView = worldView;
                        }
                        else
                        {
                            currentController = menu;
                            menuView.setChildView(worldView);
                            currentView = menuView;
                        }
                        break;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            currentView.Draw();

            base.Draw(gameTime);
        }

        public Controller getCurrentController() { return currentController; }
    }
}
