using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextureAtlas;

namespace EducationalMathGame
{
    class Balloon
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */
        private int mPoints;
        private Vector2 mPosition;
        private Texture2D mTexture;
        //private Texture2D mTextureBody;
        //private Color[] colorBalloon;
        private Rectangle mBodyBalloon;
        private Rectangle mBodyRope;

        private HelperAnimateSprite mHelperAnimateSprite;

        private float mSpeed = 0.6f;
        private SpriteFont mFont;
        public State mState = new State();

        public enum State { WithRope = 1, NoRope = 2 };


        /*
         * *******************************
         * Getters
         * *******************************
         */
        public Rectangle BodyBalloon
        {
            get { return mBodyBalloon; }
        }

        public Rectangle BodyRope
        {
            get { return mBodyRope; }
        }

        public int Points
        {
            get { return mPoints; }
        }

        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public Balloon(
            Vector2 position, Texture2D texture,
            int spriteRows, int spriteColumns,
            SpriteFont font, int points,
            float speed)
        {
            this.mPosition = position;
            this.mTexture = texture;
            this.mFont = font;
            this.mPoints = points;
            this.mSpeed = speed;

            mState = State.WithRope;
            mHelperAnimateSprite = new HelperAnimateSprite(mTexture, spriteRows, spriteColumns, 200f);

            //colorBalloon = new Color[64 * 64];
            //for (int i = 0; i < 64 * 64; i++)
            //{
            //    colorBalloon[i] = Color.Red;
            //colorRope[i] = Color.Yellow;
            //}

            //mTextureBody = new Texture2D(mGraphics,64,64);

            //mTextureBody.SetData(colorBalloon);
        }

        /*
         * *******************************
         * Game Logic
         * *******************************
         */
        public void Update(GameTime gameTime)
        {
            moveLeft(gameTime);
            if (mState == State.NoRope)
            {
                moveUp(gameTime);
            }

            mBodyBalloon = new Rectangle(
                (int)mPosition.X - (mHelperAnimateSprite.Width / 2),
                ((int)mPosition.Y - (mHelperAnimateSprite.Height / 2)),
                mHelperAnimateSprite.Width,
                ((mHelperAnimateSprite.Height / 2) + 10));


            mBodyRope = new Rectangle(
                   ((int)mPosition.X - mHelperAnimateSprite.Width / 2),
                   ((int)mPosition.Y + 10),
                   mHelperAnimateSprite.Width,
                   mHelperAnimateSprite.Height / 2);

            mHelperAnimateSprite.Update(gameTime, mPosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            /*
            Rectangle mSourceRectangle = new Rectangle(
                mHelperAnimateSprite.Width * mHelperAnimateSprite.CurrentFrame,
                0,
                mHelperAnimateSprite.Width,
                mHelperAnimateSprite.Height);

            
            spriteBatch.Draw(
                mTextureBody,
                mBodyBalloon,
                mSourceRectangle,
                Color.White,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                0);


            spriteBatch.Draw(
                mTextureBody,
                mBodyRope,
                mSourceRectangle,
                Color.SkyBlue,
                0,
                Vector2.Zero,
                SpriteEffects.None,
                0);
            */

            mHelperAnimateSprite.Draw(spriteBatch);
            // balloon points
            spriteBatch.DrawString(
                mFont,
                "" + mPoints,
                mPosition,
                Color.White,
                0,
                new Vector2(mFont.MeasureString("" + mPoints).X / 2, mFont.MeasureString("" + mPoints).Y),
                1f,
                SpriteEffects.None,
                0f);



        }


        /*
         * *******************************
         * Other
         * *******************************
         */
        private void moveLeft(GameTime gameTime)
        {
            mPosition.X -= mSpeed;
        }

        private void moveUp(GameTime gameTime)
        {
            mPosition.Y -= mSpeed;
        }
    }



}