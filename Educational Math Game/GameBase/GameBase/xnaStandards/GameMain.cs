using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EducationalMathGame
{

    public class GameMain : Microsoft.Xna.Framework.Game
    {

        /*
         * *******************************
         * Variables
         * *******************************
         */
        public const String GAME_NAME = "Math Game Shooter";
        public const float FRAMES_PER_SECOND = 60.0f;

        public const int SCREEN_WIDTH = 640;
        public const int SCREEN_HEIGHT = 400;

        private GraphicsDeviceManager mGraphicsDeviceManager;
        private SpriteBatch mSpriteBatch;
        private HelperScreen mHelperScreen;


        /*
         * *******************************
         * Getters
         * *******************************
         */
        public HelperScreen HelperScreen 
        {
            get { return mHelperScreen; }
        }


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public GameMain()
        {
            mGraphicsDeviceManager = new GraphicsDeviceManager(this);
            mGraphicsDeviceManager.PreferredBackBufferWidth = SCREEN_WIDTH;
            mGraphicsDeviceManager.PreferredBackBufferHeight = SCREEN_HEIGHT;
            mGraphicsDeviceManager.IsFullScreen = false;
            mGraphicsDeviceManager.ApplyChanges();
            IsMouseVisible = true;
            
            Content.RootDirectory = "Content";
            Window.Title = GAME_NAME;

            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / FRAMES_PER_SECOND);
        }


        /*
         * *******************************
         * Game Logic
         * *******************************
         */
        protected override void Initialize()
        {
            mHelperScreen = new HelperScreen(this);
            mHelperScreen.ChangeScreen(new ScreenTitle(this));
            mSpriteBatch = new SpriteBatch(mGraphicsDeviceManager.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            mSpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            mHelperScreen.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            mSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            mHelperScreen.Draw(mSpriteBatch);
            mSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
