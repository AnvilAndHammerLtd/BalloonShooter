using EducationalMathGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationalMathGame
{
    abstract class BaseScreenMenu : BaseGame, IScreen
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */
        protected Color ON_MOUSE_OVER_COLOR = Color.Yellow;
        protected Color DEFAULT_OPTION_COLOR = Color.White;

        protected String[] mOptionsText;
        protected Vector2[] mOptionsPosition;
        protected Rectangle[] mOptionsBody;


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        /// <summary>
        /// Base class for all menu screens.
        /// - Use the mOptionsText[] and the mOptionsPosition[] arrays to defines the menu options text and position.
        /// - Call "initialiseOptions" method after options text and positions have been set 
        /// - Override "optionSelectAction" method to define unique actions for when an option is clicked, use the mOptionsBody[]
        /// array to get the collision for each option
        /// </summary>
        /// <param name="gameMain"></param> 
        /// <param name="backgroundTexturePath"></param> give null for no bacground texture
        /// <param name="backgroundMoving"></param> //true for a moving background
        /// <param name="backgroundMusicPath"></param> give null for no sound
        public BaseScreenMenu(
            GameMain gameMain,
            String backgroundTexturePath,
            bool backgroundMoving,
            String backgroundMusicPath,
            bool backgroundMusicRepeat
            )
            : base(gameMain)
        {
            if (backgroundTexturePath != null) {
                base.HelperBackgroundTexture.setBackgroundTexture(backgroundTexturePath, backgroundMoving);
            }

            if (backgroundMusicPath != null)
            {
                base.HelperMusic.setBackgroundMusic(backgroundMusicPath);
                base.HelperMusic.playBackgroundMusic(backgroundMusicRepeat, 0.3f);
            }
        }

        public void initialiseOptions()
        {
            mOptionsBody = new Rectangle[mOptionsText.Length];

            //initialise options
            for (int i = 0; i < mOptionsText.Length; i++)
            {
                Vector2 optionfontDimensions = NormalFont.MeasureString(mOptionsText[i]);
                float fontHeight = optionfontDimensions.Y;

                mOptionsBody[i] = new Rectangle((int)mOptionsPosition[i].X, (int)mOptionsPosition[i].Y, (int)optionfontDimensions.X, (int)optionfontDimensions.Y);
            }
        }


        /*
         * *******************************
         * Game Logic
         * *******************************
         */
        void IScreen.Update(GameTime gameTime)
        {
            CurrentMouseState = Mouse.GetState();

            if (CurrentMouseState.LeftButton == ButtonState.Pressed)
            {
                Point mousePosition = new Point(CurrentMouseState.X, CurrentMouseState.Y);
                optionSelectAction(mousePosition);
            }

            base.HelperBackgroundTexture.updateBackground(gameTime, 0.03f);
        }

        void IScreen.Draw(SpriteBatch spriteBatch)
        {
            base.HelperBackgroundTexture.drawBackground(spriteBatch);

            // options
            Point mousePosition = new Point(CurrentMouseState.X, CurrentMouseState.Y);
            optionHighlight(mousePosition, spriteBatch);
        }


        /*
         * *******************************
         * Other
         * *******************************
         */
        protected abstract void optionSelectAction(Point mousePosition);

        private void optionHighlight(Point mousePosition, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mOptionsText.Length; i++)
            {
                onOptionMouseOver(mOptionsBody[i], mOptionsText[i], mousePosition, mOptionsPosition[i], spriteBatch);
            }
        }

        private void onOptionMouseOver(Rectangle optionBody, String optionText, Point mousePosition, Vector2 position, SpriteBatch spriteBatch)
        {
            if (!optionBody.Contains(mousePosition))
            {
                spriteBatch.DrawString(
                    NormalFont,
                    optionText,
                    position,
                    DEFAULT_OPTION_COLOR
                );
            }
            else
            {
                spriteBatch.DrawString(
                    NormalFont,
                    optionText,
                    position,
                    ON_MOUSE_OVER_COLOR
                );
            }
        }
    }
}
