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
    public class Night3 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Night3(Game1 game) : base(game) { }

        private CommonNight commonNight = new CommonNight();

        private InteractObject bookshelfInteract = new InteractObject();

        public override void Initialize()
        {
            commonNight.Initialize(Game.Window, Game.GraphicsDevice);

            bookshelfInteract.Initialize(new Vector2(4300, 6412), 142, 29, "rangerBiblio");

            base.Initialize();
        }

        public override void LoadContent()
        {
            commonNight.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Check interaction
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if(bookshelfInteract.InteractWith(-commonNight.Camera.Position))
                {
                    Console.WriteLine("Biblio");
                }
            }
            commonNight.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            commonNight.Draw(Game.SpriteBatch);
        }
    }
}
