using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monster_Islands
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D winterBackground;
        private SpriteFont font;
        private SpriteFont titleFont;
        private int score = 0;
        private bool begin = true;
        Texture2D enemyTexture;
        List<Enemy> enemies;
        // The rate at which the enemies appear
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;
        // A random number generator
        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            enemies = new List<Enemy>();

            // Set the time keepers to zero

            previousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns

            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Initialize our random number generator

            random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("island");
            winterBackground = Content.Load<Texture2D>("winter");
            enemyTexture = Content.Load<Texture2D>("yeti");
            font = Content.Load<SpriteFont>("Score");
            titleFont = Content.Load<SpriteFont>("title");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.W))
            {
                this.begin = false;
            }
            UpdateEnemies(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            if (begin)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
                spriteBatch.DrawString(font, "CHOOSE AN ISLAND", new Vector2(230, 130), Color.White);
                spriteBatch.DrawString(titleFont, "WINTER ISLAND", new Vector2(560, 70), Color.White);
                spriteBatch.DrawString(titleFont, "DEATH ISLAND", new Vector2(300, 70), Color.White);
                spriteBatch.DrawString(titleFont, "DESERT ISLAND", new Vector2(300, 220), Color.White);
                spriteBatch.DrawString(titleFont, "ALIAN ISLAND", new Vector2(300, 380), Color.White);
                spriteBatch.DrawString(titleFont, "FIRE ISLAND", new Vector2(580, 220), Color.White);
                spriteBatch.DrawString(titleFont, "DRAGON ISLAND", new Vector2(30, 220), Color.White);
                spriteBatch.DrawString(titleFont, "ZOMBIE ISLAND", new Vector2(560, 380), Color.White);
                spriteBatch.DrawString(titleFont, "VAMPIRE ISLAND", new Vector2(30, 380), Color.White);
                spriteBatch.DrawString(titleFont, "GHOST ISLAND", new Vector2(50, 70), Color.White);
            }
            else
            {
                spriteBatch.Draw(winterBackground, new Rectangle(0, 0, 800, 480), Color.White);

                for (int i = 0; i < enemies.Count; i++)
                {

                    enemies[i].Draw(spriteBatch);

                }

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }


        private void AddEnemy()
        {

            // Create the animation object

            Animation enemyAnimation = new Animation();

            // Initialize the animation with the correct animation information

            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy

            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2,

            random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy

            Enemy enemy = new Enemy();

            // Initialize the enemy

            enemy.Initialize(enemyAnimation, position);

            // Add the enemy to the active enemies list

            enemies.Add(enemy);

        }


        private void UpdateEnemies(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {

                previousSpawnTime = gameTime.TotalGameTime;

                // Add an Enemy

                AddEnemy();

            }

            // Update the Enemies

            for (int i = enemies.Count - 1; i >= 0; i--)
            {

                enemies[i].Update(gameTime);

                if (enemies[i].Active == false)
                {

                    enemies.RemoveAt(i);

                }

            }
        }
    }
}
