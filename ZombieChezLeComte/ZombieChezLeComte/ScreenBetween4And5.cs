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
    /// Screen de transition entre la nuit 4 et 5
    /// </summary>
    public class ScreenBetween4And5 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenBetween4And5(Game1 game) : base(game) { }

        private string[] sentance;
        private TextInfo textInfo;
        private SoundEffect swordSound;
        private bool soundStart;

        public override void Initialize()
        {
            sentance = new string[]
            {
                "Vous plantez le zombie mais au meme moment vous...",
            };
            textInfo = new TextInfo();
            textInfo.Initialize(sentance[0], Color.White, new Vector2(0, Constantes.WINDOW_HEIGHT / 2));
            soundStart = false;
            base.Initialize();
        }

        public override void LoadContent()
        {
            SpriteFont font = Game.Content.Load<SpriteFont>("MeanFont");
            swordSound = Game.Content.Load<SoundEffect>("sword");
            textInfo.LoadContent(font);
            textInfo.ActiveText(5);
            swordSound.Play();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            textInfo.Update(gameTime);
            if (textInfo.IsFinished)
            {
                Game.LoadNight5();
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
