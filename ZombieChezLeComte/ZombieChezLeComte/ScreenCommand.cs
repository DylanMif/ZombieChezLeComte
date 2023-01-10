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
    /// <summary>
    /// Screen pour la page des commandes
    /// </summary>
    public class ScreenCommand : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenCommand(Game1 game) : base(game) { }

        private string[] commands;
        private TextInfo[] allTextInfo;

        public override void Initialize()
        {
            commands = new string[]
            {
                "ZQSD : Se deplacer",
                "",
                "",
                "",
                "",
                "Espace : Interagir",
                "",
                "",
                "",
                "",
                "Left Maj : Courir",
                "",
                "",
                "",
                "", // Ceci permet de laisser plus d'espace entre les lignes de "vrai" texte
                "",
                "",
                "",
                "[Espace] Retourner au menu principale"
            };
            // Plusieurs textes infos pour afficher les lignes petits à petits
            allTextInfo = new TextInfo[commands.Length];
            for (int i = 0; i < allTextInfo.Length; i++)
            {
                allTextInfo[i] = new TextInfo();
                allTextInfo[i].Initialize(commands[i], Color.White, new Vector2(0, 20 * i + 10));
            }
            base.Initialize();
        }

        public override void LoadContent()
        {
            SpriteFont font = Game.Content.Load<SpriteFont>("MeanFont");
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
                if (i != allTextInfo.Length - 1 && allTextInfo[i].TextFullyWritten && !allTextInfo[i + 1].WritingText)
                {
                    allTextInfo[i + 1].ActiveText(50000);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && allTextInfo[allTextInfo.Length - 1].TextFullyWritten)
            {
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
