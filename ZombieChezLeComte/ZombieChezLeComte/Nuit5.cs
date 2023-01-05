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
    internal class Nuit5 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Nuit5(Game1 game) : base(game) { }
        private CommonNight commonNight = new CommonNight();

        private HuntGhost huntGhost = new HuntGhost();
        private DoorGhost doorGhost = new DoorGhost();

        public override void Initialize()
        {
            commonNight.Initialize(Game.Window, Game.GraphicsDevice);
            huntGhost.Initialize(new Vector2(0, 0), Constantes.HUNT_GHOST_SPEED);
            doorGhost.Initialize(new Vector2(0, 0), 0);
            base.Initialize();
        }

        public override void LoadContent()
        {
            commonNight.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));

            huntGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomeHunt.sf", new JsonContentLoader()));
            doorGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomePorte.sf", new JsonContentLoader()));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            commonNight.Update(gameTime);
            huntGhost.Update(gameTime, commonNight, Game);
            doorGhost.Update(gameTime, commonNight, Game);
        }

        public override void Draw(GameTime gameTime)
        {
            commonNight.Draw(Game.SpriteBatch);
            huntGhost.Draw(Game.SpriteBatch, commonNight);
            doorGhost.Draw(Game.SpriteBatch);
        }
    }
}
