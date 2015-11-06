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
    class HelperMusic
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */
        private Song mBackgroundMusic;
        private ContentManager mContent;


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public HelperMusic(ContentManager content)
        {
            mContent = content;
        }


        /*
         * *******************************
         * Other
         * *******************************
         */
        public void setBackgroundMusic(String path)
        {
            mBackgroundMusic = mContent.Load<Song>(path);
        }

        public void playBackgroundMusic(Boolean repeat, float volume)
        {
            MediaPlayer.Play(mBackgroundMusic);
            MediaPlayer.IsRepeating = repeat;
            MediaPlayer.Volume = volume;
        }

    }
}
