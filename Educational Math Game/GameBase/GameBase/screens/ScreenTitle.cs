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
    class ScreenTitle : BaseScreenMenu
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public ScreenTitle(GameMain gameMain)
            : base(gameMain,
                "sprites/playscreen_background",
                false,
                "audio/playscreen_background_music",
                true
            )
        {
            // define total options
            mOptionsText = new String[4];
            mOptionsPosition = new Vector2[4];

            // set options text
            mOptionsText[0] = "Play";
            mOptionsText[1] = "Settings";
            mOptionsText[2] = "Help";
            mOptionsText[3] = "Exit";

            // set options position
            Vector2 optionfontDimensions = NormalFont.MeasureString(mOptionsText[0]);
            float fontHeight = optionfontDimensions.Y;

            mOptionsPosition[0] = new Vector2((GameMain.SCREEN_WIDTH / 2) - (optionfontDimensions.X), (GameMain.SCREEN_HEIGHT / 2) - (fontHeight));

            mOptionsPosition[1] =
                new Vector2(
                    (GameMain.SCREEN_WIDTH / 2) - (optionfontDimensions.X),
                    (mOptionsPosition[0].Y) + (fontHeight)
           );

            mOptionsPosition[2] =
                new Vector2(
                    (GameMain.SCREEN_WIDTH / 2) - (optionfontDimensions.X),
                    (mOptionsPosition[1].Y) + (fontHeight)
           );

            mOptionsPosition[3] =
                new Vector2(
                    (GameMain.SCREEN_WIDTH / 2) - (optionfontDimensions.X),
                    (mOptionsPosition[2].Y) + (fontHeight)
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
            // play
            if (mOptionsBody[0].Contains(mousePosition))
            {
                MediaPlayer.Stop();
                HelperScreen.ChangeScreen(new ScreenPlay(GameMain));
            }

            // settings
            if (mOptionsBody[1].Contains(mousePosition))
            {

            }

            // help
            if (mOptionsBody[2].Contains(mousePosition))
            {

            }

            // exit
            if (mOptionsBody[3].Contains(mousePosition))
            {
                MediaPlayer.Stop();
                this.GameMain.Exit();
            }
        }
    }
}
