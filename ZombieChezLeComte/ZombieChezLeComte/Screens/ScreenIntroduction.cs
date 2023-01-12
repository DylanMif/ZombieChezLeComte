using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen gérant le texte introductif au début du jeu
    /// </summary>
    public class ScreenIntroduction : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenIntroduction(Game1 game) : base(game) { }

        // Pour que les phrases s'affiche une par une nous les mettons déjà toutes dans une liste
        // Chaque phrase aura son TextInfo associé
        private string[] introSentance;
        private TextInfo[] allTextInfo;

        public override void Initialize()
        {
            introSentance = new string[]
            {
                "Ce matin tu as ete elu domestique du comte Berenger, c'est plutot une bonne nouvelle",
                "car tu cherchais justement du travail. Cependant tu l as toujours trouve bizarre, etrange comme",
                "s'il jouait un double jeu. Tu en avais deja parle a ton pere mais il ne t'avait pas cru.",
                "Apres avoir ete assigne comme domestique du comte Berenger tu lui en as reparle mais il t'a",
                "repondu ainsi :",
                "-Ne te fais point d'inquietude je le connais bien, en plus tu cherchais du travail et ",
                "celui-ci te ferais acceder a la noblesse. Ne laisse pas quelques pensees noires te gacher la ",
                "vie.",
                "Tu decides donc d'ecouter ton pere malgre toutes tes peurs et tu commences ta premiere ",
                "semaine de travail.",
                " ",
                "Les taches que tu devras realiser seront inscrites sur des papiers laisses dans la cuisine, ",
                "Attention elles changeront chaque nuit",
                " ",
                " ",
                " ",
                "[Appuyez sur espace pour continuer]"
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
            }

            // On lance la première phrase
            allTextInfo[0].ActiveText(50000);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i].Update(gameTime);
                // Lorsque le précedant est fini on lance le suivant
                if(i != allTextInfo.Length-1 && allTextInfo[i].TextFullyWritten && !allTextInfo[i+1].WritingText)
                {
                    allTextInfo[i+1].ActiveText(50000);
                }
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space) && allTextInfo[allTextInfo.Length-1].TextFullyWritten)
            {
                Game.LoadNight1();
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
