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
    internal class Night1 : GameScreen
    {
        
        private new Game1 Game => (Game1)base.Game;
        public Night1(Game1 game) : base(game) { }
        private CommonNight _nuit1 = new CommonNight();

        private InteractObject[] litInteraction = new InteractObject[4];
        private int nombreLitFait = 0;

        public override void Initialize()
        {
            _nuit1.Initialize(Game.Window, Game.GraphicsDevice);
            for(int i = 0; i < litInteraction.Length; i++)
            {
                litInteraction[i]= new InteractObject();
            }
            litInteraction[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "lit 1");
            litInteraction[1].Initialize(new Vector2(4098, 5900), 37, 73, "lit2", "lit 2");
            litInteraction[2].Initialize(new Vector2(4098, 6088), 37, 73, "lit3", "lit 3");
            litInteraction[3].Initialize(new Vector2(4452, 6131), 37, 73, "lit4", "lit 4");
            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit1.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"), 
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit1.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                for(int i = 0; i < litInteraction.Length; i++)
                {
                    if (litInteraction[i].InteractWith(-_nuit1.Camera.Position))
                    {

                        Console.WriteLine("Interaction");
                    }
                    else
                    {
                        Console.WriteLine("Non");
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _nuit1.Draw(Game.SpriteBatch);
        }
    }
}
