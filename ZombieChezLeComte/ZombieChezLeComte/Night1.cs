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
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace ZombieChezLeComte
{
    internal class Night1 : GameScreen
    {
        
        private new Game1 Game => (Game1)base.Game;
        public Night1(Game1 game) : base(game) { }
        private CommonNight _nuit1 = new CommonNight();

        public override void Initialize()
        {
            _nuit1.Initialize();

            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit1.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit1.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _nuit1.Draw();
        }
    }
}
