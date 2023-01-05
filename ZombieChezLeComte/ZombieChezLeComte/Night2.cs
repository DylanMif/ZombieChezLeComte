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
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Serialization;

namespace ZombieChezLeComte
{
    public class Night2 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Night2(Game1 game) : base(game) { }
        private CommonNight _nuit2 = new CommonNight();

        

        public override void Initialize()
        {
            _nuit2.Initialize(Game.Window, Game.GraphicsDevice);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit2.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit2.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
               
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _nuit2.Draw(Game.SpriteBatch);
        }
    }
}
