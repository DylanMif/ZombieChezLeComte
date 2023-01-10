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
    public class ScreenMainMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenMainMenu(Game1 game) : base(game) { }

        private Button newGameButton = new Button();
        private int newGameButtonXPos = 50;
        private int newGameButtonYPos = 250;

        private Button continueButton = new Button();
        private int continueButtonXPos = 50;
        private int continueButtonYPos = 350;

        private Button commandButton = new Button();
        private int commandButtonXPos = 50;
        private int commandButtonYPos = 450;

        private Button quitButton = new Button();
        private int quitButtonXPos = 50;
        private int quitButtonYPos = 550;

        private Vector2 titlePosition;

        private Vector2 mainMenuImagePosition;
        private Texture2D mainMenuTexture;

        private SpriteFont pixelFont;
        private SpriteFont pixelTitleFont;
        private Texture2D buttonBgTex;

        private Rectangle titleRect;

        private int nightNumber;
        private int end;

        public override void Initialize()
        {
            newGameButton.Initialize("Nouvelle Partie", Color.Transparent, new Vector2(newGameButtonXPos, newGameButtonYPos), 600, 50,
                Color.Transparent, 0, 0, Color.Gray, Color.White, Color.Red, "Nouvelle Partie <<");
            continueButton.Initialize("Continuer", Color.Transparent, new Vector2(continueButtonXPos, continueButtonYPos), 600, 50,
                Color.Transparent, 0, 0, Color.Gray, Color.White, Color.Red, "Continuer <<");
            commandButton.Initialize("Commandes", Color.Transparent, new Vector2(commandButtonXPos, commandButtonYPos), 600, 50,
                Color.Transparent, 0, 0, Color.Gray, Color.White, Color.Red, "Commandes <<");
            quitButton.Initialize("Quitter", Color.Transparent, new Vector2(quitButtonXPos, quitButtonYPos), 600, 50,
                Color.Transparent, 0, 0, Color.Gray, Color.White, Color.Red, "Quitter <<");

            titlePosition = new Vector2(0, 0);

            mainMenuImagePosition = new Vector2(600, 250);

            nightNumber = DataSaver.LoadNight();
            end = DataSaver.LoadEnd();

            titleRect = new Rectangle(0, 0, 858, 41);
            base.Initialize();
        }

        public override void LoadContent()
        {
            pixelFont = Content.Load<SpriteFont>("police");
            pixelTitleFont = Content.Load<SpriteFont>("TitleFont");

            // La texture de fond des boutons
            buttonBgTex = Content.Load<Texture2D>("rectangle");

            newGameButton.LoadContent(pixelFont, buttonBgTex);
            continueButton.LoadContent(pixelFont, buttonBgTex);
            commandButton.LoadContent(pixelFont, buttonBgTex);
            quitButton.LoadContent(pixelFont, buttonBgTex);

            
            mainMenuTexture = Content.Load<Texture2D>("mainMenuImage");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            newGameButton.Update(Mouse.GetState(),Game);
            continueButton.Update(Mouse.GetState(), Game);
            commandButton.Update(Mouse.GetState(), Game);
            quitButton.Update(Mouse.GetState(), Game);

            // Si on clique sur le titre est que l'on a finit au moins une fois la nuit 5 alors on lance la nuit 6
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && 
                titleRect.Intersects(new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1)) && 
                DataSaver.LoadEnd() == 1)
            {
                Game.LoadNight6();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(new Color(0, 0, 0));
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.DrawString(pixelTitleFont, Constantes.GAME_TITLE, titlePosition, end == 1 ? Color.Red : Color.White);
            newGameButton.Draw(Game.SpriteBatch);
            if(nightNumber != 0)
                continueButton.Draw(Game.SpriteBatch);
            commandButton?.Draw(Game.SpriteBatch);
            quitButton.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Draw(mainMenuTexture, mainMenuImagePosition, 
                Color.White);
            Game.SpriteBatch.End();
        }
    }
}
