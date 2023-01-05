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
using MonoGame.Extended.Sprites;
using MonoGame.Extended;

namespace ZombieChezLeComte
{
    internal class Nuit4 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Nuit4(Game1 game) : base(game) { }
        private CommonNight commonNight = new CommonNight();

        private Zombie zombie = new Zombie();

        public override void Initialize()
        {
            commonNight.Initialize(Game.Window, Game.GraphicsDevice);
            zombie.Initialiaze(new Vector2(100, 100), Constantes.ZOMBIE_SPEED);
            base.Initialize();
        }

        public override void LoadContent()
        {
            commonNight.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));

            zombie.LoadContent(Game.Content.Load<SpriteSheet>("zombie.sf", new JsonContentLoader()));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            zombie.Update(gameTime, commonNight);
            commonNight.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            commonNight.Draw(Game.SpriteBatch);
            zombie.Draw(Game.SpriteBatch);
        }
    }
}
