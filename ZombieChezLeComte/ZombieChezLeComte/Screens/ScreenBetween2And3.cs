using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen de transition entre la nuit 2 et 3
    /// </summary>
    public class ScreenBetwween2And3 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenBetwween2And3(Game1 game) : base(game) { }

        private string[] sentance;
        private TextInfo textInfo;
        private SoundEffect openDoor;
        private bool soundStart;

        public override void Initialize()
        {
            sentance = new string[]
            {
                "Lors de votre sommeil vous semblez entendre une porte s'ouvrir...",
            };
            textInfo = new TextInfo();
            textInfo.Initialize(sentance[0], Color.White, new Vector2(0, Constantes.WINDOW_HEIGHT / 2));
            soundStart = false;
            base.Initialize();
        }

        public override void LoadContent()
        {
            SpriteFont font = Game.Content.Load<SpriteFont>("MeanFont");
            openDoor = Game.Content.Load<SoundEffect>("DoorClosed");
            textInfo.LoadContent(font);
            textInfo.ActiveText(7);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            textInfo.Update(gameTime);
            if (textInfo.TextFullyWritten && !soundStart)
            {
                openDoor.Play();
                soundStart = true;
            }
            if (textInfo.IsFinished)
            {
                Game.LoadNight3();
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
