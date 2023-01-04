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

namespace ZombieChezLeComte
{
    internal class Introduction : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Introduction(Game1 game) : base(game) { }

        private string[] introSentance;
        private TextInfo[] allTextInfo;

        public override void Initialize()
        {
            introSentance = new string[]
            {
                "Ce matin tu as ete elu domestique du compte Berenger, c'est plutot une bonne nouvelle",
                "car tu cherchais justement du travail. Cependant tu l as toujours trouve bizarre, etrange"
            };
            allTextInfo = new TextInfo[introSentance.Length];
            for(int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i] = new TextInfo();
                allTextInfo[i].Initialize(introSentance[i], Color.White, new Vector2(0, 20*i + 10));
            }
            base.Initialize();
        }

        public override void LoadContent()
        {
            SpriteFont font = Game.Content.Load<SpriteFont>("LittleFont");
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i].LoadContent(font);
                allTextInfo[i].ActiveText(4);
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i].Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i].Draw(Game.SpriteBatch);
            }
            Game.SpriteBatch.End();
        }
    }
}
