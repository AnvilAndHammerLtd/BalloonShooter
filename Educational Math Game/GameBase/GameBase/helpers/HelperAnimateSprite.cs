using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TextureAtlas
{
    public class HelperAnimateSprite
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */
        private Texture2D mTexture { get; set; }
        private int mSpriteRows { get; set; }
        private int mSpriteColumns { get; set; }
        private int mCurrentFrame;
        private int mTotalFrames;

        //sprite size
        private int mWidth;
        private int mHeight;

        private Vector2 mOrigin;
        private Rectangle mSourceRectangle;
        private Rectangle mDestinationRectangle;
        //private Rectangle mBodyRectangle;
        private float mElapsed;
        private float mDelay;

        /*
         * *******************************
         * Getters
         * *******************************
         */
        public int CurrentFrame
        {
            get { return mCurrentFrame; }
        }
        public int Width
        {
            get { return mWidth; }
        }
        public int Height
        {
            get { return mHeight; }
        }

        public Vector2 Origin
        {
            get { return mOrigin; }
        }

        public Rectangle SourceRectangle
        {
            get { return mSourceRectangle; }
        }


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public HelperAnimateSprite(Texture2D texture, int rows, int columns, float delay)
        {
            mTexture = texture;
            mSpriteRows = rows;
            mSpriteColumns = columns;
            mCurrentFrame = 0;
            mTotalFrames = mSpriteRows * mSpriteColumns;
            this.mDelay = delay;

            mWidth = mTexture.Width / mSpriteColumns;
            mHeight = mTexture.Height / mSpriteRows;
        }


        /*
         * *******************************
         * Game Logic
         * *******************************
         */
        public void Update(GameTime gameTime, Vector2 position)
        {
            //int row = (int)((float)currentFrame / (float)Columns);
            //int column = currentFrame % Columns;

            mOrigin = new Vector2(mWidth / 2, mHeight / 2);

            mElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (mElapsed >= mDelay)
            {
                if (mCurrentFrame >= mTotalFrames - 1)
                {
                    mCurrentFrame = 0;
                }
                else {
                    mCurrentFrame++;
                }

                mElapsed = 0;
            }

            //Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            //Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //width*currentFrame moves to the correct location of the frame width on the spritemap
            mSourceRectangle = new Rectangle(mWidth * mCurrentFrame, 0, mWidth, mHeight);
            mDestinationRectangle = new Rectangle(((int)position.X), ((int)position.Y), mWidth, mHeight);
            //mBodyRectangle = new Rectangle(((int)position.X - width/2), ((int)position.Y * -1), width, height/2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                mTexture,
                mDestinationRectangle,
                mSourceRectangle,
                Color.White,
                0,
                mOrigin,
                SpriteEffects.None,
                0);
        }

        public void Draw(SpriteBatch spriteBatch, Color color, float rotate, SpriteEffects effects, float layerDepth)
        {
            //spriteBatch.Draw(Texture,destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.Draw(
                mTexture,
                mDestinationRectangle,
                mSourceRectangle,
                Color.White,
                rotate,
                mOrigin,
                effects,
                layerDepth);
        }

    }
}