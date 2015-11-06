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
    class ScreenPause : BaseScreenMenu
    {

        /*
         * *******************************
         * Variables
         * *******************************
         */
        private ScreenPlay mCurrentPlayScreenState;


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public ScreenPause(GameMain gameMain, ScreenPlay currentPlayScreenState)
            : base(
            gameMain,
            "sprites/playscreen_background",
            false,
            "audio/pausescreen_background_music",
            true
            )
        {

            this.mCurrentPlayScreenState = currentPlayScreenState;

            // define total options
            mOptionsText = new String[2];
            mOptionsPosition = new Vector2[2];

            // set options text
            mOptionsText[0] = "Continue";
            mOptionsText[1] = "Back to Main Menu";

            // set options position
            Vector2 optionfontDimensions = NormalFont.MeasureString(mOptionsText[0]);
            float fontHeight = optionfontDimensions.Y;

            mOptionsPosition[0] = new Vector2((GameMain.SCREEN_WIDTH / 2) - (optionfontDimensions.X), (GameMain.SCREEN_HEIGHT / 2) - (fontHeight));
            mOptionsPosition[1] =
                new Vector2(
                    (GameMain.SCREEN_WIDTH / 2) - (optionfontDimensions.X),
                    (mOptionsPosition[0].Y) + (fontHeight)
           );

            base.initialiseOptions();
        }

        /*
         * *******************************
         * Other
         * *******************************
         */
        protected override void optionSelectAction(Point mousePosition)
        {

            // continue
            if (mOptionsBody[0].Contains(mousePosition))
            {
                MediaPlayer.Stop();
                mCurrentPlayScreenState.fromPauseScreen();
            }

            // to main menu screen
            if (mOptionsBody[1].Contains(mousePosition))
            {
                HelperScreen.ChangeScreen(new ScreenTitle(GameMain));
            }
        }

    }
}
