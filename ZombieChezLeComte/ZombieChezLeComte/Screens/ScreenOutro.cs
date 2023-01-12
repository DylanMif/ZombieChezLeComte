using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;


namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen gérant la phase final du jeu
    /// </summary>
    public class ScreenOutro : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenOutro(Game1 game) : base(game) { }

        private string[] introSentance;
        private TextInfo[] allTextInfo;

        public override void Initialize()
        {
            introSentance = new string[]
            {
                "HAHAHA ! Ton travail ce termine ici !",
                "Les choses auraient peut-etre pu se finir differemment...",
                "La mort aurait peut-etre pu etre eviter...",
                "[Appuyez sur espace pour retourner au menu principale]",
            };
            // On utilise plusieurs textInfo pour afficher les phrase une par une
            allTextInfo = new TextInfo[introSentance.Length];
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i] = new TextInfo();
                allTextInfo[i].Initialize(introSentance[i], Color.White, new Vector2(50, Constantes.WINDOW_HEIGHT / 2));
            }
            DataSaver.SaveNight(0);
            base.Initialize();
        }

        public override void LoadContent()
        {
            SpriteFont font = Game.Content.Load<SpriteFont>("MeanFont");
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i].LoadContent(font);
            }
            allTextInfo[0].ActiveText(10);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i].Update(gameTime);
                if (i != allTextInfo.Length - 1 && allTextInfo[i].IsFinished && !allTextInfo[i + 1].IsFinished)
                {
                    allTextInfo[i + 1].ActiveText(10);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && allTextInfo[allTextInfo.Length - 1].TextFullyWritten)
            {
                DataSaver.SaveEnd(1);
                DataSaver.SaveNight(0);
                Game.LoadMainMenu();
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
