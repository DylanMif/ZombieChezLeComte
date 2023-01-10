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
    /// <summary>
    /// Screen pour le GameOver
    /// </summary>
    public class ScreenGameOver : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenGameOver(Game1 game) : base(game) { }

        private Vector2 positionGameOver;
        private AnimatedSprite gameOverSprite;
        private float currentTime;
        

        public override void Initialize()
        {
            currentTime = 2;
            base.Initialize();
        }

        public override void LoadContent()
        {
            gameOverSprite = new AnimatedSprite(Game.Content.Load<SpriteSheet>("gameover.sf", new JsonContentLoader()));
            gameOverSprite.Origin = Vector2.Zero;
            positionGameOver = new Vector2(
                (Constantes.WINDOW_WIDTH - gameOverSprite.GetBoundingRectangle(new Transform2(0, 0)).Width) / 2,
                (Constantes.WINDOW_HEIGHT - gameOverSprite.GetBoundingRectangle(new Transform2(0, 0)).Height) / 2
                );
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Le temps sur cette scène est limité donc un timer réduit petit à petit
            currentTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(currentTime <= 0)
            {
                Game.LoadMainMenu();
            }
            gameOverSprite.Play("idle");
            gameOverSprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(gameOverSprite, positionGameOver);
            Game.SpriteBatch.End();
        }
    }
}
