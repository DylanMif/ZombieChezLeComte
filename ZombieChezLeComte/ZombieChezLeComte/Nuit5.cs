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
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    internal class Nuit5 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Nuit5(Game1 game) : base(game) { }
        private CommonNight commonNight = new CommonNight();

        private HuntGhost huntGhost = new HuntGhost();
        private DoorGhost doorGhost = new DoorGhost();
        private RunGhost runGhost = new RunGhost();
        private Zombie zombie = new Zombie();

        private InteractObject[] kitchenPapers = new InteractObject[6];
        private InteractObject[] keyFragementInteract = new InteractObject[5];
        private InteractObject caveDoorInteract = new InteractObject();
        private TextInfo textInfo = new TextInfo();

        private SoundEffect armorSound;
        private SoundEffect doorSound;

        public override void Initialize()
        {
            commonNight.Initialize(Game.Window, Game.GraphicsDevice);
            huntGhost.Initialize(new Vector2(0, 0), Constantes.HUNT_GHOST_SPEED);
            doorGhost.Initialize(new Vector2(0, 0), 0);
            runGhost.Initialize(new Vector2(10, 10), Constantes.RUNGHOST_SPEED);
            zombie.Initialiaze(new Vector2(40, -210), Constantes.ZOMBIE_SPEED);
            zombie.PeutTuer = true;
            zombie.PeutBouger = true;

            for (int i = 0; i < kitchenPapers.Length; i++)
            {
                kitchenPapers[i] = new InteractObject();
            }

            kitchenPapers[0].Initialize(new Vector2(3843, 6591), 23, 16, "papier1", "Ne mourrez pas et n'allez en aucun cas a la cave");
            kitchenPapers[1].Initialize(new Vector2(3925, 6572), 31, 38, "papier2", "Le sommeil est la cle");
            kitchenPapers[2].Initialize(new Vector2(3955, 6632), 42, 35, "papier3", "Lire revele la cle");
            kitchenPapers[3].Initialize(new Vector2(4021, 6572), 30, 27, "papier4", "Ranger pour mieux retrouver vos affaires comme une cle...");
            kitchenPapers[4].Initialize(new Vector2(4086, 6571), 36, 27, "papier5", "Se renforcer est la cle");
            kitchenPapers[5].Initialize(new Vector2(4120, 6605), 36, 27, "papier6", "L'eau est la cle de la vie");

            for (int i = 0; i < keyFragementInteract.Length; i++)
            {
                keyFragementInteract[i] = new InteractObject();
            }
            keyFragementInteract[0].Initialize(new Vector2(4450, 6124), 20, 100, "keyFrag1", "Un nouveau fragment de trouve");
            keyFragementInteract[1].Initialize(new Vector2(4158, 6412), 32, 29, "keyFrag2", "Un nouveau fragment de trouve");
            keyFragementInteract[2].Initialize(new Vector2(4436, 5933), 92, 19, "keyFrag3", "Un nouveau fragment de trouve");
            keyFragementInteract[3].Initialize(new Vector2(4799, 6365), 110, 50, "keyFrag4", "Un nouveau fragment de trouve");
            keyFragementInteract[4].Initialize(new Vector2(3868, 6595), 31, 57, "keyFrag5", "Un nouveau fragment de trouve");

            caveDoorInteract.Initialize(new Vector2(4917, 6617), 103, 46, "porte", "Ferme a cle! Il faut la cle, pas cassee...");

            textInfo.Initialize(" ", Color.White, new Vector2(10, Constantes.WINDOW_HEIGHT - 150));

            base.Initialize();
        }

        public override void LoadContent()
        {
            commonNight.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()), Game);

            huntGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomeHunt.sf", new JsonContentLoader()), Game);
            doorGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomePorte.sf", new JsonContentLoader()), Game);
            runGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomeRun.sf", new JsonContentLoader()),Game);
            zombie.LoadContent(Game.Content.Load<SpriteSheet>("zombie.sf", new JsonContentLoader()),Game);

            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));
            armorSound = Game.Content.Load<SoundEffect>("Armure");
            doorSound = Game.Content.Load<SoundEffect>("DoorClosed");

            textInfo.Text = "Je viens de me reveiller vers l'entree ???";
            textInfo.ActiveText(3);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            commonNight.Update(gameTime);
            huntGhost.Update(gameTime, commonNight, Game);
            doorGhost.Update(gameTime, commonNight, Game);
            runGhost.Update(gameTime, commonNight, Game);
            zombie.Update(gameTime, commonNight, Game);

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                foreach (InteractObject paperInteract in kitchenPapers)
                {
                    if (paperInteract.InteractWith(-commonNight.Camera.Position))
                    {
                        textInfo.Text = paperInteract.InteractText;
                        textInfo.ActiveText(2);
                    }
                }
                foreach (InteractObject keyFragInteract in keyFragementInteract)
                {
                    if (keyFragInteract.InteractWith(-commonNight.Camera.Position))
                    {
                        if(!textInfo.WritingText)
                            armorSound.Play();
                        keyFragInteract.HasAlreadyInteractable = true;
                        if(GetNbKeyFragment(keyFragementInteract) == 1)
                        {
                            textInfo.Text = "La cle semble cassee, il faudrait retrouver tous les fragments";
                        } else if(GetNbKeyFragment(keyFragementInteract) == keyFragementInteract.Length)
                        {
                            textInfo.Text = "Vous avez trouve tous les fragments, vous pouvez refaire la cle";
                        } else
                        {
                            textInfo.Text = keyFragInteract.InteractText + $" {GetNbKeyFragment(keyFragementInteract)}/{keyFragementInteract.Length}";
                        }
                        textInfo.ActiveText(2);
                    }
                }
                if(caveDoorInteract.InteractWith(-commonNight.Camera.Position))
                {
                    if(GetNbKeyFragment(keyFragementInteract) == keyFragementInteract.Length)
                    {
                        Game.LoadCave();
                    } else
                    {
                        if(!textInfo.WritingText)
                        {
                            doorSound.Play();
                        }
                        textInfo.Text = caveDoorInteract.InteractText;
                        textInfo.ActiveText(2);
                    }
                }
            }

            textInfo.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            commonNight.Draw(Game.SpriteBatch);
            doorGhost.Draw(Game.SpriteBatch);
            runGhost.Draw(Game.SpriteBatch);
            zombie.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            commonNight.DrawVision(Game.SpriteBatch);
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
            huntGhost.Draw(Game.SpriteBatch, commonNight);
        }

        private static int GetNbKeyFragment(InteractObject[] _allKeyFrag)
        {
            int res = 0;
            foreach(InteractObject keyFrag in _allKeyFrag)
            {
                if(keyFrag.HasAlreadyInteractable)
                {
                    res++;
                }
            }
            return res;
        }
    }
}
