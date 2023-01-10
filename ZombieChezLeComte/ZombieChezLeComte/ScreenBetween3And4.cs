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
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen de transition entre la nuit 3 et 4
    /// </summary>
    public class ScreenBetween3And4 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenBetween3And4(Game1 game) : base(game) { }

        private string[] sentance;
        private TextInfo textInfo;
        private SoundEffect ghostSound;
        private bool soundStart;

        public override void Initialize()
        {
            sentance = new string[]
            {
                "Esperons que vous ayez reussi a sortir...",
            };
            textInfo = new TextInfo();
            textInfo.Initialize(sentance[0], Color.White, new Vector2(0, Constantes.WINDOW_HEIGHT / 2));
            soundStart = false;
            base.Initialize();
        }

        public override void LoadContent()
        {
            SpriteFont font = Game.Content.Load<SpriteFont>("MeanFont");
            ghostSound = Game.Content.Load<SoundEffect>("GhostRun");
            textInfo.LoadContent(font);
            textInfo.ActiveText(12);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            textInfo.Update(gameTime);
            if (textInfo.TextFullyWritten && !soundStart)
            {
                ghostSound.Play();
                soundStart = true;
            }
            if (textInfo.IsFinished)
            {
                Game.LoadNight4();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }
    }
}
