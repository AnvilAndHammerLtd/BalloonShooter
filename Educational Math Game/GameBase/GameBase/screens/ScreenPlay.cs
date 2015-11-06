using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using TextureAtlas;
using Microsoft.Xna.Framework.Media;

namespace EducationalMathGame
{
    class ScreenPlay : BaseGame, IScreen
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */
        // other
        private static readonly int TOTAL_BALLOONS = 12;
        private bool isCurrentEquationSuccess = false;
        private int mCurrentFrame = 0;
        private int mSuccessMessageDelay = 0;
        // making sure that the user does not shoot when they click Play or Continue
        private bool isFromAnotherScreen = false;

        // resources
        private Texture2D mDartTexture;
        private Texture2D mBalloonTexture;
        private SoundEffect mBalloonPopSound;
        private SoundEffect mBalloonRopeSwingSound;
        
        // dead lists
        private List<Balloon> mDestroyedBalloons = new List<Balloon>();
        private List<Dart> mDestroyedDarts = new List<Dart>();

        // interactable objects
        private List<Dart> mDarts = new List<Dart>();
        private List<Balloon> mBalloons = new List<Balloon>();

        // equations etc logic
        private int mScoreTotal = 0;
        private int mCurrentEquationPointsScored = 0;
        private String mScoreResult = "Result: ";
        private String[] mEquations = { "0+1", "2+1", "1+1", "3+6", "10", "6+7" };
        private static int[] mEquationResults = { 1, 3, 2, 9, 10, 13 };
        private Vector2[] mBalloonsPositions = new Vector2[TOTAL_BALLOONS];
        private int mCurrentEquationIndex = 0;


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public ScreenPlay(GameMain gameMain)
            : base(gameMain)
        {
            //SoundEffect.MasterVolume = 0.5f;

            setTextures();
            base.HelperMusic.setBackgroundMusic("audio/playscreen_background_music");
            base.HelperBackgroundTexture.setBackgroundTexture("sprites/playscreen_background", true);

            createBalloons();
            base.HelperMusic.playBackgroundMusic(true, 0.3f);

            isFromAnotherScreen = true;
        }

        /*
         * *******************************
         * Game Logic
         * *******************************
         */
        void IScreen.Update(GameTime gameTime)
        {
            mCurrentFrame++;
            mSuccessMessageDelay++;

            KeyboardState keyState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                HelperScreen.ChangeScreen(new ScreenPause(this.GameMain, this));
            }

            updateMouse(gameTime);
            updateBalloons(gameTime);
            updateDartsAndBalloonsCollision(gameTime);
            updateCheckEquation();
            updateClearDeadObjects();
            base.HelperBackgroundTexture.updateBackground(gameTime, 0.03f);


            if(mCurrentFrame >= 60){
                mCurrentFrame = 0;
            }

        }

        void IScreen.Draw(SpriteBatch spriteBatch)
        {
            base.HelperBackgroundTexture.drawBackground(spriteBatch);
            drawBalloons(spriteBatch);
            drawDarts(spriteBatch);
            drawScore(spriteBatch);
            drawEquation(spriteBatch);
            drawResult(spriteBatch);
        }


        /*
         * *******************************
         * Initialisation methods
         * *******************************
         */
        private void setTextures()
        {
            mBalloonTexture = base.Content.Load<Texture2D>("sprites/balloon");
            mDartTexture = base.Content.Load<Texture2D>("sprites/dart");

            mBalloonPopSound = base.Content.Load<SoundEffect>("audio/balloon_pop");
            mBalloonRopeSwingSound = base.Content.Load<SoundEffect>("audio/balloon_rope_swing");
        }

        private void createBalloons()
        {

            Random rand = new Random();

            // initialise balloon positions, the wave
            int startingPosition = 100;

            //initialise positions for half of the balloons
            for (int i = 0; i < mBalloonsPositions.Length / 2; i++)
            {
                mBalloonsPositions[i] =
                    new Vector2(
                        GameMain.SCREEN_WIDTH + (startingPosition * i),
                        100 + (20 * i));
            }


            //initialise positions for the other half balloons
            for (int i = mBalloonsPositions.Length / 2; i < mBalloonsPositions.Length; i++)
            {
                mBalloonsPositions[i] =
                    new Vector2(
                        GameMain.SCREEN_WIDTH + (startingPosition * i),
                        GameMain.SCREEN_HEIGHT - (20 * i));
            }

            // initialise the balloons
            for (int i = 0; i < TOTAL_BALLOONS; i++)
            {
                mBalloons.Add(
                    new Balloon(
                        mBalloonsPositions[i],
                        mBalloonTexture,
                        1,
                        4,
                        NormalFont,
                        i,
                        1f)
                        );
            }
        }

        /*
         * *******************************
         * update methods
         * *******************************
         */
        private void updateMouse(GameTime gameTime)
        {

            if (isFromAnotherScreen == true && mCurrentFrame < 45)
            {
                return;
            }
            else if (isFromAnotherScreen == true){
                isFromAnotherScreen = false;
            }

            OldMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();


            if (CurrentMouseState.LeftButton == ButtonState.Pressed &&
                OldMouseState.LeftButton == ButtonState.Released)
            {
                // restrict mouse clicks only within the game window
                if (CurrentMouseState.X >= 0 && CurrentMouseState.X <= GameMain.SCREEN_WIDTH
                    &&
                    CurrentMouseState.Y >= 0 && CurrentMouseState.Y <= GameMain.SCREEN_HEIGHT
                    )
                {
                    playerShoot(CurrentMouseState.X, CurrentMouseState.Y);
                }
            }
        }

        private void updateBalloons(GameTime gameTime)
        {
            foreach (Balloon balloon in mBalloons)
            {
                balloon.Update(gameTime);
            }
        }

        private void updateClearDeadObjects()
        {
            foreach (Dart dart in mDestroyedDarts)
            {
                if (mDarts.Contains(dart))
                {
                    mDarts.Remove(dart);
                }
            }

            foreach (Balloon balloon in mDestroyedBalloons)
            {
                if (mBalloons.Contains(balloon))
                {
                    mBalloons.Remove(balloon);
                }
            }

            mDestroyedDarts.Clear();
            mDestroyedBalloons.Clear();
        }

        private void updateDartsAndBalloonsCollision(GameTime gameTime)
        {
            foreach (Dart dart in mDarts)
            {
                dart.Update(gameTime);
                // remove for the game any darts that are out of the screen
                if (dart.IsVisible == false)
                    mDestroyedDarts.Add(dart);

                foreach (Balloon balloon in mBalloons)
                {
                    if (dart.IsVisible == true)
                    {
                        //dart with balloon upper body
                        if (dart.Body.Intersects(balloon.BodyBalloon))
                        {
                            /*
                             * Destroy objects (bye bye)
                             * Update equation result
                             * Update total points for this current equation
                             * Play sound
                             */
                            mDestroyedBalloons.Add(balloon);
                            mDestroyedDarts.Add(dart);
                            mCurrentEquationPointsScored += balloon.Points;
                            mScoreResult += balloon.Points + "+";
                            mBalloonPopSound.Play();
                        }
                        // dart with balloon rope collision
                        if (dart.Body.Intersects(balloon.BodyRope))
                        {
                            mDestroyedDarts.Add(dart);
                            // up we go balloon
                            balloon.mState = Balloon.State.NoRope;
                            mBalloonRopeSwingSound.Play();
                        }
                    }
                }
            }
        }
        
        private void updateCheckEquation()
        {
            if (mCurrentEquationPointsScored > mEquationResults[mCurrentEquationIndex])
            {
                // FAILED TO SOLVE EQUATION
                //reset points;
                isCurrentEquationSuccess = false;
                mCurrentEquationPointsScored = 0;
                mScoreResult = "Result: ";
            }
            // EQUATION SOLVED
            else if (mCurrentEquationPointsScored == mEquationResults[mCurrentEquationIndex])
            {
                mCurrentEquationIndex++;
                isCurrentEquationSuccess = true;
                mSuccessMessageDelay = 0;
                mCurrentEquationPointsScored = 0;
                mScoreTotal++;
                mScoreResult = "Success, Result: " + mCurrentEquationPointsScored;
            }

            //let the SUCCESS message be displayed for some time before removing it
            if (mSuccessMessageDelay >= 60 && isCurrentEquationSuccess == true)
            {
                mScoreResult = "Result: ";
                isCurrentEquationSuccess = false;
            }
        }


        /*
         * *******************************
         * draw methods
         * *******************************
         */
        private void drawBalloons(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mBalloons.Count; i++)
            {
                Balloon balloon = mBalloons[i];
                balloon.Draw(spriteBatch);
            }
        }

        private void drawDarts(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < mDarts.Count; i++)
            {
                Dart dart = mDarts[i];
                dart.Draw(spriteBatch);
            }
        }

        private void drawScore(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                NormalFont,
                "Score: " + mScoreTotal,
                new Vector2(
                    0,
                    0),
                    Color.White
            );
        }

        private void drawEquation(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                NormalFont,
                "Equation: " + mEquations[mCurrentEquationIndex] + " = ?",
                new Vector2(
                    GameMain.SCREEN_WIDTH / 2,
                    0),
                    Color.White
            );
        }

        private void drawResult(SpriteBatch spriteBatch)
        {
            if (isCurrentEquationSuccess == true)
            {
                spriteBatch.DrawString(
                    NormalFont,
                    "Result: SUCCESS",
                    new Vector2(
                        GameMain.SCREEN_WIDTH / 2 - NormalFont.MeasureString("Result: SUCCESS").X,
                        GameMain.SCREEN_HEIGHT - NormalFont.MeasureString("Result: SUCCESS").Y),
                        Color.White
                );
            }
            else
            {
                spriteBatch.DrawString(
                    NormalFont,
                    mScoreResult,
                    new Vector2(
                        GameMain.SCREEN_WIDTH / 2 - NormalFont.MeasureString(mScoreResult).X,
                        GameMain.SCREEN_HEIGHT - NormalFont.MeasureString(mScoreResult).Y),
                        Color.White
                );
            }
        }


        /*
         * *******************************
         * Other
         * *******************************
         */
        private void playerShoot(int x, int y)
        {
            mDarts.Add(new Dart(new Vector2(x, y), mDartTexture, 1, 1));
        }

        public void fromPauseScreen()
        {
            isFromAnotherScreen = true;
            base.HelperMusic.playBackgroundMusic(true, 0.3f);
            HelperScreen.ChangeScreen(this);
        }
    }
}
