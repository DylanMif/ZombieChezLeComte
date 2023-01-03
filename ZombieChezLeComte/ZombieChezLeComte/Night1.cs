using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Microsoft.Xna.Framework.Media;

namespace ZombieChezLeComte
{
    internal class Night1 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Night1(Game1 game) : base(game) { }

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);

        }
    }
}
