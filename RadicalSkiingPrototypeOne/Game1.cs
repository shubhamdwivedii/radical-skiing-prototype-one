using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RadicalSkiingPrototypeOne.Core;
using RadicalSkiingPrototypeOne.Sprites;
using System.Collections.Generic;

namespace RadicalSkiingPrototypeOne
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ResolutionManager resolutionManager;
        Camera camera;
        Vector2 position;
        Texture2D sprite;
        Player player;
        Stage playStage;
        List<Sprite> backgrounds;
        Texture2D treelog;
        List<Sprite> treelogs;
        Texture2D tree;
        List<Sprite> trees;

        
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

            base.Initialize();

            resolutionManager = new ResolutionManager(this, graphics);
            resolutionManager.Initialize();
            position = new Vector2(0, 0);
            camera = new Camera(resolutionManager);
            

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgrounds = new List<Sprite>();
            sprite = Content.Load<Texture2D>("Sprites/Tilemap");

            treelog = Content.Load<Texture2D>("Sprites/blockage64x12");

            tree = Content.Load<Texture2D>("Sprites/tree");

            treelogs = new List<Sprite>();
            trees = new List<Sprite>();

            for(int i = 0; i <20; i++)
            {
                backgrounds.Add(new Sprite(sprite));
                backgrounds[i].Position = new Vector2(0, i * sprite.Height);
            }

            for(int i = 0; i < 100; i++)
            {
                trees.Add(new Sprite(tree));
                trees[i].Position = new Vector2(500 + (800 * (i % 2)), i * 150);
                treelogs.Add(new Sprite(treelog));
                treelogs[i].Position = new Vector2(1920/2 -(704) + (600 * (i % 2)), 2000 + (i * 400));
                treelogs[i].Rectangle = new Rectangle((int)treelogs[i].Position.X,(int) treelogs[i].Position.Y, treelog.Width, treelog.Height);
            }

            player = new Player(Content.Load<Texture2D>("Sprites/skier64x96"),new Vector2(1920/2,1080/2),this);
            playStage = new Stage();
            
            playStage.Add(backgrounds);
            playStage.Add(treelogs);
            playStage.Add(trees);
            playStage.Add(player);
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
           
            // TODO: Add your update logic here
            player.Update(gameTime);
            player.CheckCollision(treelogs);
            camera.Update(player.CameraPosition);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            playStage.Draw(gameTime, spriteBatch,camera.GetTransformMatrix());
            base.Draw(gameTime);
        }
    }
}
