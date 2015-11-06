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
    abstract class BaseGame
    {

        /*
         * *******************************
         * Variables
         * *******************************
         */
        private GameMain mGameMain;
        private SpriteFont mNormalFont;
        private ContentManager mContent;

        // helpers
        private HelperBackgroundTexture mHelperBackgroundTexture;
        private HelperMusic mHelperMusic;
        private HelperScreen mHelperScreen;


        /*
         * *******************************
         * Getters
         * *******************************
         */
        public GameMain GameMain
        {
            get { return mGameMain; }
        }
        public ContentManager Content
        {
            get { return mContent; }
        }
        public HelperScreen HelperScreen
        {
            get { return mHelperScreen; }
        }
        public SpriteFont NormalFont
        {
            get { return mNormalFont; }
        }
        public HelperBackgroundTexture HelperBackgroundTexture
        {
            get { return mHelperBackgroundTexture; }
        }
        public HelperMusic HelperMusic
        {
            get { return mHelperMusic; }
        }

        public MouseState OldMouseState { get; set; }
        public MouseState CurrentMouseState { get; set; }


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        protected BaseGame(GameMain gameMain)
        {
            mGameMain = gameMain;
            mContent = gameMain.Content;
            mHelperScreen = gameMain.HelperScreen;

            mHelperBackgroundTexture = new HelperBackgroundTexture(gameMain.Content);
            mHelperMusic = new HelperMusic(gameMain.Content);

            setCommonUi();
        }


        /*
         * *******************************
         * Other
         * *******************************
         */
        protected void setCommonUi()
        {
            mNormalFont = mGameMain.Content.Load<SpriteFont>("fonts//normal");
        }
    }
}
