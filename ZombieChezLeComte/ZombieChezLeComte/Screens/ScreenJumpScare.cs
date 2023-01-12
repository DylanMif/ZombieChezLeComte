using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen gérant les jumpscares
    /// </summary>
    public class ScreenJumpScare : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenJumpScare(Game1 game) : base(game) { }

        private Dictionary<string, Texture2D> jumpScareImage = new Dictionary<string, Texture2D>();
        private Dictionary<string, SoundEffect> jumpScareSound = new Dictionary<string, SoundEffect>();

        private float currentTime;
        private Vector2 drawPos;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            // On initialise un petit dictionnaire pour jouer le bon son et affichez la bonne image en fonction de quelle
            // monstre nous a tué
            jumpScareImage.Add("runGhost", Game.Content.Load<Texture2D>("runGhostJumpScare"));
            jumpScareSound.Add("runGhost", Game.Content.Load<SoundEffect>("runGhostJumpScareSound"));

            jumpScareImage.Add("huntGhost", Game.Content.Load<Texture2D>("huntGhostJumpScareImage"));
            jumpScareSound.Add("huntGhost", Game.Content.Load<SoundEffect>("huntGhostJumpScareSound"));

            jumpScareImage.Add("doorGhost", Game.Content.Load<Texture2D>("doorGhostJumpScareImage"));
            jumpScareSound.Add("doorGhost", Game.Content.Load<SoundEffect>("doorGhostJumpScareSound"));

            jumpScareImage.Add("zombie", Game.Content.Load<Texture2D>("zombieJumpScareImage"));
            jumpScareSound.Add("zombie", Game.Content.Load<SoundEffect>("zombieJumpScareSound"));


            currentTime = (float)jumpScareSound[Game.killBy].Duration.TotalSeconds;

            drawPos = new Vector2(
                (Constantes.WINDOW_WIDTH - jumpScareImage[Game.killBy].Width) / 2,
                (Constantes.WINDOW_HEIGHT - jumpScareImage[Game.killBy].Height) / 2
                );

            jumpScareSound[Game.killBy].Play();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            currentTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(currentTime <= 0)
            {
                // Le fantôme final "endGhost" nous tue forcément c'est prévu dans l'histoire.
                // C'est pour cela qu'il y a pas le même comportement
                if(Game.killBy != "endGhost")
                {
                    Game.LoadGameOver();
                } else
                {
                    Game.LoadMainMenu();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(jumpScareImage[Game.killBy], drawPos, Color.White);
            Game.SpriteBatch.End();
        }
    }
}
