using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;

namespace ZombieChezLeComte
{
    public class ScreenCave : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenCave(Game1 game) : base(game) { }

        private Charactere player = new Charactere();

        public override void Initialize()
        {
            player.Initialize(new Vector2(360, 360), Constantes.VITESSE_JOUEUR);
            base.Initialize();
        }

        public override void LoadContent()
        {
            player.LoadContent(Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            
        }
    }
}
