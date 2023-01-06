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
                "car tu cherchais justement du travail. Cependant tu l as toujours trouve bizarre, etrange comme",
                "s'il jouait un double jeu. Tu en avais deja parler a ton pere mais il ne t'avais pas cru.",
                "Apres avoir ete assigne comme domestique du compte Berenger tu lui en a reparle mais il t'a",
                "repondu ainsi :",
                "-Ne te fais point d'inquietude je le connais bien, en plus tu cherchais du travail et ",
                "celui-ci te ferais acceder a la noblesse. Ne laisse pas quelques pensees noires te gacher la ",
                "vie.",
                "Tu decide donc d'ecouter ton pere malgre toutes tes peurs et tu commences ta premiere ",
                "semaine de travail.",
                " ",
                "Les taches que tu devras realiser seront inscrites sur des papiers laisse dans la cuisine, ",
                "Attention elles changeront chaque nuit",
                " ",
                " ",
                " ",
                "[Appuyer sur espace pour continuer]"
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
            allTextInfo[0].ActiveText(50000);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i].Update(gameTime);
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
