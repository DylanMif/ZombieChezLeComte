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
    public class Night3 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Night3(Game1 game) : base(game) { }

        private CommonNight commonNight = new CommonNight();

        private InteractObject bookshelfInteract = new InteractObject();
        private InteractObject[] kitchenPapers = new InteractObject[6];
        private InteractObject kitchenInteract = new InteractObject();
        private InteractObject hallInteract = new InteractObject();
        private InteractObject[] litInteraction = new InteractObject[4];
        private InteractObject deadZombieInteract = new InteractObject();

        private Charactere deadZombie = new Charactere();

        private RunGhost runGhost = new RunGhost();

        private TextInfo textInfo = new TextInfo();

        public override void Initialize()
        {
            commonNight.Initialize(Game.Window, Game.GraphicsDevice);

            bookshelfInteract.Initialize(new Vector2(4300, 6412), 142, 29, "rangerBiblio", "Vous rangez la bibliotheque");
            kitchenInteract.Initialize(new Vector2(3979, 6573), 33, 27, "rangerCuisine", "Vous rangez la cuisine");
            hallInteract.Initialize(new Vector2(4595, 6442), 115, 138, "rangerHall", "Vous rangez la hall");
            for(int i = 0; i < kitchenPapers.Length; i++)
            {
                kitchenPapers[i] = new InteractObject();
            }
            kitchenPapers[0].Initialize(new Vector2(3843, 6591), 23, 16, "papier1", "Ranger la bibliotheque");
            kitchenPapers[1].Initialize(new Vector2(3925, 6572), 31, 38, "papier2", "Ranger la cuisine");
            kitchenPapers[2].Initialize(new Vector2(3955, 6632), 42, 35, "papier3", "Ranger le hall");
            kitchenPapers[3].Initialize(new Vector2(4021, 6572), 30, 27, "papier4", "Faire les lits");
            kitchenPapers[4].Initialize(new Vector2(4086, 6571), 36, 27, "papier5", "Ne pas aller dans le stockage");
            kitchenPapers[5].Initialize(new Vector2(4120, 6605), 36, 27, "papier6", "");
            for (int i = 0; i < litInteraction.Length; i++)
            {
                litInteraction[i] = new InteractObject();
            }
            litInteraction[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "Ce lit est nickel");
            litInteraction[1].Initialize(new Vector2(4098, 5900), 63, 83, "lit2", "Parfaitement range");
            litInteraction[2].Initialize(new Vector2(4098, 6078), 47, 103, "lit3", "Bizarre... lit range");
            litInteraction[3].Initialize(new Vector2(4452, 6111), 37, 83, "lit4", "Personne est venu aujourd'hui ?");
            deadZombieInteract.Initialize(new Vector2(3901, 6382), 21, 28, "deadZombie", "C'est un mort, au moins il ne bougera plus...");
            base.Initialize();

            deadZombie.Initialize(new Vector2(-348, 0), 1);

            textInfo.Initialize(" ", Color.White, new Vector2(10, Constantes.WINDOW_HEIGHT - 150));
            runGhost.Initialize(new Vector2(10, 10), Constantes.RUNGHOST_SPEED);

            base.Initialize();
        }

        public override void LoadContent()
        {
            commonNight.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));

            deadZombie.LoadContent(Game.Content.Load<SpriteSheet>("zombie.sf", new JsonContentLoader()));
            runGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomeRun.sf", new JsonContentLoader()));

            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Check interaction
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if(bookshelfInteract.InteractWith(-commonNight.Camera.Position))
                {
                    textInfo.Text = bookshelfInteract.InteractText;
                    textInfo.ActiveText(2);
                    bookshelfInteract.InteractText = "Cette bibliotheque est rangee";
                }
                if(kitchenInteract.InteractWith(-commonNight.Camera.Position))
                {
                    textInfo.Text = kitchenInteract.InteractText;
                    textInfo.ActiveText(2);
                    kitchenInteract.InteractText = "Cette cuisine est nickel";
                }
                if(hallInteract.InteractWith(-commonNight.Camera.Position))
                {
                    textInfo.Text = hallInteract.InteractText;
                    textInfo.ActiveText(2);
                    hallInteract.InteractText = "Ce hall est presque propre";
                }
                if(deadZombieInteract.InteractWith(-commonNight.Camera.Position))
                {
                    textInfo.Text = deadZombieInteract.InteractText;
                    textInfo.ActiveText(2);
                }
                foreach(InteractObject paperInteract in kitchenPapers)
                {
                    if(paperInteract.InteractWith(-commonNight.Camera.Position))
                    {
                        textInfo.Text = paperInteract.InteractText;
                        textInfo.ActiveText(2);
                    }
                }
                foreach(InteractObject bedInteract in litInteraction)
                {
                    if(bedInteract.InteractWith(-commonNight.Camera.Position))
                    {
                        textInfo.Text = bedInteract.InteractText;
                        textInfo.ActiveText(2);
                    }
                }
            }
            commonNight.Update(gameTime);

            textInfo.Update(gameTime);

            
            deadZombie.MovementWithoutAnim(commonNight.CameraMove , commonNight.DeltaTime, false);
            deadZombie.Update(gameTime);
            runGhost.Update(gameTime, commonNight, Game);
        }

        public override void Draw(GameTime gameTime)
        {
            commonNight.Draw(Game.SpriteBatch);
            deadZombie.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
            commonNight.Player.Draw(Game.SpriteBatch);
            runGhost.Draw(Game.SpriteBatch);
        }
    }
}
