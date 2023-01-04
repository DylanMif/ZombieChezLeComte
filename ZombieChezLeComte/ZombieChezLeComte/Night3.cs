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
        private InteractObject[] kitchenPapers = new InteractObject[6];
        private SpriteFont pixelFont;

        public override void Initialize()
        {
            commonNight.Initialize(Game.Window, Game.GraphicsDevice);

            bookshelfInteract.Initialize(new Vector2(4300, 6412), 142, 29, "rangerBiblio", "Vous avez range la bibliotheque");
            for(int i = 0; i < 6; i++)
            {
                kitchenPapers[i] = new InteractObject();
            }
            kitchenPapers[0].Initialize(new Vector2(3843, 6591), 23, 16, "papier1", "tache 1");
            kitchenPapers[1].Initialize(new Vector2(3925, 6572), 31, 38, "papier2", "tache 2");
            kitchenPapers[2].Initialize(new Vector2(3955, 6632), 42, 35, "papier3", "tache 3");
            kitchenPapers[3].Initialize(new Vector2(4021, 6572), 30, 27, "papier4", "tache 4");
            kitchenPapers[4].Initialize(new Vector2(4086, 6571), 36, 27, "papier5", "tache 5");
            kitchenPapers[5].Initialize(new Vector2(4120, 6605), 36, 27, "papier6", "tache 6");

            base.Initialize();
        }

        public override void LoadContent()
        {
            commonNight.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));
            pixelFont = Game.Content.Load<SpriteFont>("police");

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
                    bookshelfInteract.Destroy();
                }
                foreach(InteractObject paperInteract in kitchenPapers)
                {
                    if(paperInteract.InteractWith(-commonNight.Camera.Position))
                    {
                        Console.WriteLine(paperInteract.InteractName);
                    }
                }
            }
            commonNight.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            commonNight.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.End();
        }
    }
}
