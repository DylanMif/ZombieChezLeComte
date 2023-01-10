using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen gérant la cave
    /// </summary>
    public class ScreenCave : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenCave(Game1 game) : base(game) { }

        private Charactere player = new Charactere();

        private Charactere endGhost = new Charactere();

        private bool isInJumpScare;
        private Texture2D jumpScareImage;
        private SoundEffect jumpScareSound;
        private Vector2 jumpScareDrawPos;
        private float currentTime;

        public override void Initialize()
        {
            player.Initialize(new Vector2(360, 360), Constantes.VITESSE_JOUEUR);
            endGhost.Initialize(new Vector2(0, 0), Constantes.VITESSE_JOUEUR);

            MediaPlayer.Volume = 0.1f;
            base.Initialize();
        }

        public override void LoadContent()
        {
            player.LoadContent(Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));
            endGhost.LoadContent(Game.Content.Load<SpriteSheet>("fantomeFin.sf", new JsonContentLoader()));

            jumpScareImage = Game.Content.Load<Texture2D>("endGhostJumpScareImage");
            jumpScareSound = Game.Content.Load<SoundEffect>("endGhostJumpScareSound");

            currentTime = (float)jumpScareSound.Duration.TotalSeconds;

            jumpScareDrawPos = new Vector2(
                (Constantes.WINDOW_WIDTH - jumpScareImage.Width) / 2,
                (Constantes.WINDOW_HEIGHT - jumpScareImage.Height) / 2
                );
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            player.Movement(Additions.GetAxis(Keyboard.GetState()), (float)gameTime.ElapsedGameTime.TotalSeconds, true);
            endGhost.Movement(-Additions.GetAxis(Keyboard.GetState()), (float)gameTime.ElapsedGameTime.TotalSeconds, false);
            endGhost.CurrentAnimation = "idle";
            endGhost.Update(gameTime);
            if(player.SpriteRect.Intersects(endGhost.SpriteRect) && !isInJumpScare)
            {
                isInJumpScare = true;
                MediaPlayer.Volume = 1f;
                jumpScareSound.Play();
            }

            if(isInJumpScare)
            {
                currentTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(currentTime <= 0)
                {
                    Game.LoadOutro();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if(isInJumpScare)
            {
                Game.SpriteBatch.Begin();
                Game.SpriteBatch.Draw(jumpScareImage, jumpScareDrawPos, Color.White);
                Game.SpriteBatch.End();
            } else
            {
                endGhost.Draw(Game.SpriteBatch);
                player.Draw(Game.SpriteBatch);
            }
        }
    }
}
