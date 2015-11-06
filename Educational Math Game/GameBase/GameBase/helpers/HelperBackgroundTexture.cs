using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextureAtlas;


namespace EducationalMathGame
{
    class HelperBackgroundTexture
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */
        private Texture2D mBackgroundTexture;

        private Rectangle mBackgroundFrontAnim;
        private Rectangle mBackgroundBackAnim;

        private Vector2 mBackgroundFrontposition = Vector2.Zero;
        private Vector2 mBackgroundBackposition = new Vector2(GameMain.SCREEN_WIDTH, 0);
        
        private bool isBackgroundMoving;

        private ContentManager mContent;


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public HelperBackgroundTexture(ContentManager content)
        {
            mContent = content;
        }


        /*
         * *******************************
         * Game Logic
         * *******************************
         */
        public void updateBackground(GameTime gameTime, float animationSpeed)
        {
            if (isBackgroundMoving == true)
            {
                if (mBackgroundFrontposition.X <= GameMain.SCREEN_WIDTH * -1)
                {
                    // put background back to the start to animate the illusion of movement
                    mBackgroundFrontposition.X = 0;
                    mBackgroundBackposition.X = GameMain.SCREEN_WIDTH;
                }
                else
                {
                    // animate background
                    mBackgroundFrontposition.X -= animationSpeed;
                    mBackgroundBackposition.X -= animationSpeed;
                }

                mBackgroundFrontAnim = new Rectangle(
                    (int)mBackgroundFrontposition.X, (int)mBackgroundFrontposition.Y,
                    GameMain.SCREEN_WIDTH, GameMain.SCREEN_HEIGHT);
                mBackgroundBackAnim = new Rectangle(
                    (int)mBackgroundBackposition.X, (int)mBackgroundBackposition.Y,
                    GameMain.SCREEN_WIDTH, GameMain.SCREEN_HEIGHT);
            }
            else {
                // display a still background
                mBackgroundFrontAnim = new Rectangle(
                    (int)mBackgroundFrontposition.X, (int)mBackgroundFrontposition.Y,
                    GameMain.SCREEN_WIDTH, GameMain.SCREEN_HEIGHT);
            }
        }

        public void drawBackground(SpriteBatch spriteBatch)
        {
            if (isBackgroundMoving == true)
            {
                // display animated background
                spriteBatch.Draw(mBackgroundTexture, mBackgroundFrontAnim, Color.White);
                spriteBatch.Draw(mBackgroundTexture, mBackgroundBackAnim, Color.White);
            }
            else {
                // display a still background
                spriteBatch.Draw(mBackgroundTexture, mBackgroundFrontAnim, Color.White);
            }
        }


        /*
         * *******************************
         * Initialisations
         * *******************************
         */
        public void setBackgroundTexture(String texturePath, bool movingBackground)
        {
            mBackgroundTexture = mContent.Load<Texture2D>(texturePath);
            mBackgroundFrontAnim = new Rectangle(0, 0, GameMain.SCREEN_WIDTH, GameMain.SCREEN_HEIGHT);
            isBackgroundMoving = movingBackground;
            if (isBackgroundMoving == true)
            {
                mBackgroundBackAnim = new Rectangle(GameMain.SCREEN_WIDTH, 0, GameMain.SCREEN_WIDTH, GameMain.SCREEN_HEIGHT);
            }
        }
    }
}
