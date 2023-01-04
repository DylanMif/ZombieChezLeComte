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

        private string[] introSentance = new string[]
        {
            "Ce matin tu as été élu domestique du compte Bérenger, c'est plutôt une bonne nouvelle",
            "car tu cherchais justement du travail. Cependant tu l'as toujours trouvé bizarre, étrange"
        };
        //private TextInfo[] allTextInfo = new TextInfo[introSentance.Length];

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.End();
        }
    }
}
