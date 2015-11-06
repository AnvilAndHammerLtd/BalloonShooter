using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextureAtlas;

namespace EducationalMathGame
{
    class Dart
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */
        private Vector2 mPosition;
        private Texture2D mTexture;
        private const float SPEED = 10f;
        private const float GRAVITY = 10;
        private float mRotation;
        private Rectangle mBody;

        private bool isVisible = true;
        private HelperAnimateSprite mHelperAnimateSprite;

        private float mOldX, mOldY;
        private float mTotalTimePassed;


        /*
         * *******************************
         * Getters
         * *******************************
         */
        public Rectangle Body
        {
            get { return mBody; }
        }

        public bool IsVisible
        {
            get { return isVisible; }
        }


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public Dart(
            Vector2 position, Texture2D texture,
            int spriteRows, int spriteColumns)
        {
            this.mPosition = position;
            this.mTexture = texture;

            mHelperAnimateSprite = new HelperAnimateSprite(texture, spriteRows, spriteColumns, 10f);
            mBody = new Rectangle(((int)position.X), ((int)position.Y), mHelperAnimateSprite.Width, mHelperAnimateSprite.Height);
        }


        /*
         * *******************************
         * Game Logic
         * *******************************
         */
        public void Update(GameTime gameTime)
        {
            gravity(gameTime);
            moveRight(gameTime);

            if (mPosition.X > GameMain.SCREEN_WIDTH)
            {
                isVisible = false;
            }

            mBody = new Rectangle(
                (int)mPosition.X - (mHelperAnimateSprite.Width / 2),
                ((int)mPosition.Y - (mHelperAnimateSprite.Height / 2)),
                mHelperAnimateSprite.Width,
                mHelperAnimateSprite.Height
                );

            mHelperAnimateSprite.Update(gameTime, mPosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mHelperAnimateSprite.Draw(spriteBatch, Color.White, mRotation, SpriteEffects.None, 0);
        }


        /*
         * *******************************
         * Other
         * *******************************
         */
        private void moveRight(GameTime gameTime)
        {
            mOldX = mPosition.X;
            mPosition.X += SPEED;
        }

        private void gravity(GameTime gameTime)
        {
            mTotalTimePassed += (float)gameTime.ElapsedGameTime.Milliseconds / 4096.0f;

            // gravity effect
            mOldY = mPosition.Y;
            mPosition.Y += SPEED * GRAVITY * mTotalTimePassed * mTotalTimePassed;

            calculateRotation();
        }

        private void calculateRotation()
        {
            double tempx = mPosition.X - mOldX;
            double tempy = mPosition.Y - mOldY;

            mRotation =
                (float) Math.Acos
                (
                    tempy / Math.Sqrt
                    (
                        Math.Pow(tempy, 2) +
                        Math.Pow(tempx, 2)
                    )

                ) * -1;

            mRotation += 90;
        }

    }
}