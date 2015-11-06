using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationalMathGame
{
    public class HelperScreen
    {
        /*
         * *******************************
         * Variables
         * *******************************
         */
        private GameMain gameMain;
        private List<IScreen> screens;


        /*
         * *******************************
         * Constructors
         * *******************************
         */
        public HelperScreen(GameMain gameMain)
        {
            this.gameMain = gameMain;
            this.screens = new List<IScreen>();
        }

        /*
         * *******************************
         * Game Logic
         * *******************************
         */
        public void Update(GameTime gameTime)
        {
            if (screens.Count == 0)
            {
                return;
            }

            IScreen[] tempScreens = new IScreen[screens.Count];
            screens.CopyTo(tempScreens);
            foreach (IScreen screen in tempScreens)
            {
                screen.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (screens.Count == 0)
            {
                return;
            }

            IScreen[] tempScreens = new IScreen[screens.Count];
            screens.CopyTo(tempScreens);
            foreach (IScreen screen in tempScreens)
            {
                screen.Draw(spriteBatch);
            }
        }

        /*
         * Public methods
         */
        public void ChangeScreen(IScreen screen)
        {
            screens.Clear();
            screens.Add(screen);
        }
    }
}
