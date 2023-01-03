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

namespace ZombieChezLeComte
{
    public class MainMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public MainMenu(Game1 game) : base(game) { }

        private Button newGameButton = new Button();
        private int newGameButtonXPos = 50;
        private int newGameButtonYPos = 250;

        private Button continueButton = new Button();
        private int continueButtonXPos = 50;
        private int continueButtonYPos = 350;

        private Button quitButton = new Button();
        private int quitButtonXPos = 50;
        private int quitButtonYPos = 450;

        private SpriteFont pixelFont;
        private Texture2D buttonBgTex;

        public override void Initialize()
        {
            newGameButton.Initialize("Nouvelle Partie", Color.Transparent, new Vector2(newGameButtonXPos, newGameButtonYPos), 600, 50,
                Color.Transparent, 0, 0, Color.Gray, Color.White, Color.Red, "Nouvelle Partie <<");
            continueButton.Initialize("Continuer", Color.Transparent, new Vector2(continueButtonXPos, continueButtonYPos), 600, 50,
                Color.Transparent, 0, 0, Color.Gray, Color.White, Color.Red, "Continuer <<");
            quitButton.Initialize("Quitter", Color.Transparent, new Vector2(quitButtonXPos, quitButtonYPos), 600, 50,
                Color.Transparent, 0, 0, Color.Gray, Color.White, Color.Red, "Quitter <<");

            base.Initialize();
        }

        public override void LoadContent()
        {
            pixelFont = Content.Load<SpriteFont>("police");
            buttonBgTex = Content.Load<Texture2D>("rectangle");
            newGameButton.LoadContent(pixelFont, buttonBgTex);
            continueButton.LoadContent(pixelFont, buttonBgTex);
            quitButton.LoadContent(pixelFont, buttonBgTex);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            newGameButton.Update(Mouse.GetState());
            continueButton.Update(Mouse.GetState());
            quitButton.Update(Mouse.GetState());
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(new Color(0, 0, 0));
            newGameButton.Draw(Game.SpriteBatch);
            continueButton.Draw(Game.SpriteBatch);
            quitButton.Draw(Game.SpriteBatch);
        }
    }
}
