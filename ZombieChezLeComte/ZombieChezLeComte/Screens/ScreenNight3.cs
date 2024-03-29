﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Serialization;
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen gérant la nuit 3
    /// </summary>
    public class ScreenNight3 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenNight3(Game1 game) : base(game) { }

        private CommonNight commonNight = new CommonNight();

        private InteractObject bookshelfInteract = new InteractObject();
        private InteractObject[] kitchenPapers = new InteractObject[6];
        private InteractObject kitchenInteract = new InteractObject();
        private InteractObject hallInteract = new InteractObject();
        private InteractObject[] litInteraction = new InteractObject[4];
        private InteractObject deadZombieInteract = new InteractObject();
        private InteractObject enterDoor = new InteractObject();

        private Charactere deadZombie = new Charactere();

        private RunGhost runGhost = new RunGhost();

        private TextInfo textInfo = new TextInfo();

        private RunGhost[] endRunGhosts = new RunGhost[Constantes.END_NIGHT3_NB_GHOST];
        private bool endRunGhostSpawn;
        private SoundEffect balaisSound;

        public override void Initialize()
        {
            commonNight.Initialize(Game.Window, Game.GraphicsDevice);

            // On initialise tous les objets interactifs
            bookshelfInteract.Initialize(new Vector2(4300, 6412), 142, 29, "rangerBiblio", "Vous rangez la bibliotheque");
            kitchenInteract.Initialize(new Vector2(3979, 6563), 33, 37, "rangerCuisine", "Vous rangez la cuisine");
            hallInteract.Initialize(new Vector2(4595, 6442), 115, 138, "rangerHall", "Vous rangez le hall");
            for(int i = 0; i < kitchenPapers.Length; i++)
            {
                kitchenPapers[i] = new InteractObject();
            }
            kitchenPapers[0].Initialize(new Vector2(3843, 6591), 23, 16, "papier1", "Ranger la bibliotheque");
            kitchenPapers[1].Initialize(new Vector2(3925, 6572), 31, 38, "papier2", "Ranger la cuisine");
            kitchenPapers[2].Initialize(new Vector2(3955, 6632), 42, 35, "papier3", "Ranger le hall");
            kitchenPapers[3].Initialize(new Vector2(4021, 6572), 30, 27, "papier4", "Faire les lits");
            kitchenPapers[4].Initialize(new Vector2(4086, 6571), 36, 27, "papier5", "Ne pas aller dans le stockage");
            kitchenPapers[5].Initialize(new Vector2(4120, 6605), 36, 27, "papier6", "En cas de probleme partez");
            for (int i = 0; i < litInteraction.Length; i++)
            {
                litInteraction[i] = new InteractObject();
            }
            litInteraction[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "Ce lit est nickel");
            litInteraction[1].Initialize(new Vector2(4098, 5900), 63, 83, "lit2", "Parfaitement range");
            litInteraction[2].Initialize(new Vector2(4098, 6078), 47, 103, "lit3", "Bizarre... lit range");
            litInteraction[3].Initialize(new Vector2(4452, 6111), 37, 83, "lit4", "Personne n'est venu aujourd'hui ?");
            deadZombieInteract.Initialize(new Vector2(3901, 6382), 21, 28, "deadZombie", "C'est un mort, au moins il ne bougera plus...");
            base.Initialize();

            enterDoor.Initialize(new Vector2(4572, 6678), 52, 86, "enterDoor", "");

            deadZombie.Initialize(new Vector2(-348, 0), 1);

            textInfo.Initialize(" ", Color.White, new Vector2(10, Constantes.WINDOW_HEIGHT - 150));

            // On initialise le fantôme coureur
            runGhost.Initialize(new Vector2(10, 10), Constantes.RUNGHOST_SPEED);


            // A la fin de la nuit plusieurs fantômes coureurs apparaîssent on va donc les initialiser
            for (int i = 0; i < endRunGhosts.Length; i++)
            {
                endRunGhosts[i] = new RunGhost();
                endRunGhosts[i].Initialize(new Vector2(0, 0), Constantes.END_NIGHT3_GHOST_SPEED);
            }
            endRunGhostSpawn = false;

            base.Initialize();
        }

        public override void LoadContent()
        {
            commonNight.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()), Game);

            deadZombie.LoadContent(Game.Content.Load<SpriteSheet>("zombie.sf", new JsonContentLoader()));
            runGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomeRun.sf", new JsonContentLoader()),Game);

            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));

            for (int i = 0; i < endRunGhosts.Length; i++)
            {
                endRunGhosts[i].LoadContent((Game.Content.Load<SpriteSheet>("fantomeRun.sf", new JsonContentLoader())),Game);
            }

            balaisSound = Game.Content.Load<SoundEffect>("Balais");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Check des interactions
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if(bookshelfInteract.InteractWith(-commonNight.Camera.Position))
                {
                    if(!textInfo.WritingText)
                        balaisSound.Play();
                    Additions.InteractionObjet(bookshelfInteract, textInfo, "Cette bibliotheque est rangee");
                }
                if(kitchenInteract.InteractWith(-commonNight.Camera.Position))
                {
                    Additions.InteractionObjet(kitchenInteract, textInfo, "Cette cuisine est nickel");
                }
                if(hallInteract.InteractWith(-commonNight.Camera.Position))
                {
                    if (!textInfo.WritingText)
                        balaisSound.Play();
                    Additions.InteractionObjet(hallInteract, textInfo, "Ce hall est presque propre");
                }
                if(deadZombieInteract.InteractWith(-commonNight.Camera.Position))
                {
                    Additions.InteractionObjet(deadZombieInteract, textInfo, deadZombieInteract.InteractText);
                }
                if(enterDoor.InteractWith(-commonNight.Camera.Position))
                {
                    if(endRunGhostSpawn)
                    {
                        Game.LoadBetween3And4();
                    }
                }
                foreach(InteractObject paperInteract in kitchenPapers)
                {
                    if(paperInteract.InteractWith(-commonNight.Camera.Position))
                    {
                        Additions.InteractionObjet(paperInteract, textInfo, paperInteract.InteractText);
                    }
                }
                foreach(InteractObject bedInteract in litInteraction)
                {
                    if(bedInteract.InteractWith(-commonNight.Camera.Position))
                    {
                        Additions.InteractionObjet(bedInteract, textInfo, bedInteract.InteractText);
                    }
                }
            }


            commonNight.Update(gameTime);
            textInfo.Update(gameTime);
            deadZombie.MovementWithoutAnim(commonNight.CameraMove , commonNight.DeltaTime);
            deadZombie.Update(gameTime);

            // On fait appparaître tout les fantômes coureurs si toutes les tâches sont finies
            if (bookshelfInteract.HasAlreadyInteractable && kitchenInteract.HasAlreadyInteractable &&
                        hallInteract.HasAlreadyInteractable && deadZombieInteract.HasAlreadyInteractable && !endRunGhostSpawn &&
                        this.nbGoodBed() == litInteraction.Length)
            {
                endRunGhostSpawn = true;
                for (int i = 0; i < endRunGhosts.Length; i++)
                {
                    endRunGhosts[i].Spawn(commonNight);
                }
            }

            for (int i = 0; i < endRunGhosts.Length; i++)
            {
                if(endRunGhostSpawn)
                    endRunGhosts[i].Update(gameTime, commonNight, Game);
            }
           runGhost.Update(gameTime, commonNight, Game);
        }

        public override void Draw(GameTime gameTime)
        {
            commonNight.Draw(Game.SpriteBatch);
            deadZombie.Draw(Game.SpriteBatch);
            runGhost.Draw(Game.SpriteBatch);
            for (int i = 0; i < endRunGhosts.Length; i++)
            {
                if (endRunGhostSpawn)
                    endRunGhosts[i].Draw(Game.SpriteBatch);
            }
            Game.SpriteBatch.Begin();
            commonNight.DrawVision(Game.SpriteBatch);
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
            commonNight.Player.Draw(Game.SpriteBatch);
            
            
        }

        /// <summary>
        /// Méthode donnant le nombre de lit fait
        /// </summary>
        /// <returns></returns>
        public int nbGoodBed()
        {
            int res = 0;
            foreach (InteractObject bedInteract in litInteraction)
            {
                if(bedInteract.HasAlreadyInteractable)
                {
                    res++;
                }
            }
            return res;
        }
    }
}
