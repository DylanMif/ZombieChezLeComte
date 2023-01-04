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

        private TextInfo introText = new TextInfo();

        public override void Initialize()
        {
            introText.Initialize("Salut C'est moi",
                Color.White, new Vector2(10, 10));
            base.Initialize();
        }

        public override void LoadContent()
        {
            introText.LoadContent(Game.Content.Load<SpriteFont>("police"));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            introText.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                introText.ActiveText(5);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            introText.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }
    }
}
