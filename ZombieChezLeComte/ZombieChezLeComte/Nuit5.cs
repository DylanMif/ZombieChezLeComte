﻿using System;
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
        private RunGhost runGhost = new RunGhost();
        private Zombie zombie = new Zombie();

        private InteractObject[] kitchenPapers = new InteractObject[6];
        private TextInfo textInfo = new TextInfo();

        public override void Initialize()
        {
            commonNight.Initialize(Game.Window, Game.GraphicsDevice);
            huntGhost.Initialize(new Vector2(0, 0), Constantes.HUNT_GHOST_SPEED);
            doorGhost.Initialize(new Vector2(0, 0), 0);
            runGhost.Initialize(new Vector2(10, 10), Constantes.RUNGHOST_SPEED);
            zombie.Initialiaze(new Vector2(40, -210), Constantes.ZOMBIE_SPEED);

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

            textInfo.Initialize(" ", Color.White, new Vector2(10, Constantes.WINDOW_HEIGHT - 150));

            base.Initialize();
        }

        public override void LoadContent()
        {
            commonNight.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));

            huntGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomeHunt.sf", new JsonContentLoader()));
            doorGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomePorte.sf", new JsonContentLoader()));
            runGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomeRun.sf", new JsonContentLoader()));
            zombie.LoadContent(Game.Content.Load<SpriteSheet>("zombie.sf", new JsonContentLoader()));

            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));
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
            }

            textInfo.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            commonNight.Draw(Game.SpriteBatch);
            huntGhost.Draw(Game.SpriteBatch, commonNight);
            doorGhost.Draw(Game.SpriteBatch);
            runGhost.Draw(Game.SpriteBatch);
            zombie.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }
    }
}
