using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace ZombieChezLeComte
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;

        public SpriteBatch SpriteBatch
        {
            get
            {
                return _spriteBatch;
            }

            set
            {
                _spriteBatch = value;
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferHeight = Constantes.WINDOW_HEIGHT;
            _graphics.PreferredBackBufferWidth = Constantes.WINDOW_WIDTH;
            _graphics.ApplyChanges();
            base.Initialize();

            LoadMainMenu();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.I))
                LoadIntro();
            if ( Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                LoadNight1();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                LoadNight2();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3))
                LoadNight3();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
                LoadNight4();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void LoadMainMenu()
        {
            _screenManager.LoadScreen(new MainMenu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        private void LoadIntro()
        {
            _screenManager.LoadScreen(new Introduction(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadNight1()
        {
            _screenManager.LoadScreen(new Night1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadNight2()
        {
            _screenManager.LoadScreen(new Night2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadNight3()
        {
            _screenManager.LoadScreen(new Night3(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadNight4()
        {
            _screenManager.LoadScreen(new Nuit4(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
    }
}