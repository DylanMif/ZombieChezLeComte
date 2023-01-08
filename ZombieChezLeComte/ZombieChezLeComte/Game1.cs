﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;

namespace ZombieChezLeComte
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;

        public string killBy;
        public bool isInOutro;
        public bool isInMainMenu;

        private Song mainSong;
        private Song outroMusic;


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

            killBy = "zombie";
            isInOutro = false;
            isInMainMenu = false;

            LoadMainMenu();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Console.WriteLine(DataSaver.LoadNight());
            Console.WriteLine(DataSaver.LoadEnd());
            mainSong = Content.Load<Song>("mainMusic");
            outroMusic = Content.Load<Song>("outroMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(mainSong);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad0))
                LoadIntro();
            if ( Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                LoadNight1();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                LoadNight2();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3))
                LoadNight3();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
                LoadNight4();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad5))
                LoadNight5();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
                LoadCave();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad7))
                LoadOutro();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
                LoadJumpScare();
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad9))
                LoadNight6();
            // TODO: Add your update logic here
            if (isInOutro)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(outroMusic);
                isInOutro = false;
            }
            if (isInMainMenu)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(mainSong);
                isInMainMenu = false;
            }

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
            isInMainMenu = true;
            _screenManager.LoadScreen(new MainMenu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadIntro()
        {
            _screenManager.LoadScreen(new Introduction(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadNight1()
        {
            _screenManager.LoadScreen(new Night1(this), new FadeTransition(GraphicsDevice, Color.Black));
            DataSaver.SaveNight(1);
        }
        public void LoadNight2()
        {
            _screenManager.LoadScreen(new Night2(this), new FadeTransition(GraphicsDevice, Color.Black));
            DataSaver.SaveNight(2);
        }

        public void LoadNight3()
        {
            _screenManager.LoadScreen(new Night3(this), new FadeTransition(GraphicsDevice, Color.Black));
            DataSaver.SaveNight(3);
        }

        public void LoadNight4()
        {
            _screenManager.LoadScreen(new Nuit4(this), new FadeTransition(GraphicsDevice, Color.Black));
            DataSaver.SaveNight(4);
        }

        public void LoadNight5()
        {
            _screenManager.LoadScreen(new Nuit5(this), new FadeTransition(GraphicsDevice, Color.Black));
            DataSaver.SaveNight(5);
        }
        public void LoadCommand()
        {
            _screenManager.LoadScreen(new ScreenCommand(this), new FadeTransition(GraphicsDevice, Color.Black));
            DataSaver.SaveNight(5);
        }
        public void LoadCave()
        {
            _screenManager.LoadScreen(new ScreenCave(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadOutro()
        {
            isInOutro = true;
            _screenManager.LoadScreen(new ScreenOutro(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadJumpScare()
        {
            _screenManager.LoadScreen(new ScreenJumpScare(this));
        }

        public void LoadNight6()
        {
            _screenManager.LoadScreen(new Nuit6(this), new FadeTransition(GraphicsDevice, Color.Black));
            DataSaver.SaveNight(5);
        }

    }
}