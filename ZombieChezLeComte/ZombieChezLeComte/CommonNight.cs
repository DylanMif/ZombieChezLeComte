using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace ZombieChezLeComte
{
    public class CommonNight
    {
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Charactere _player = new Charactere();


        public TiledMap TiledMap
        {
            get
            {
                return this._tiledMap;
            }

            set
            {
                this._tiledMap = value;
            }
        }
        public TiledMapRenderer TiledMapRenderer
        {
            get
            {
                return this._tiledMapRenderer;
            }

            set
            {
                this._tiledMapRenderer = value;
            }
        }
        public Charactere Player
        {
            get
            {
                return this._player;
            }

            set
            {
                this._player = value;
            }
        }

        public void Initialize()
        {
            this.Player.Initialize(Constantes.POSITION_JOUEUR, Constantes.VITESSE_JOUEUR);
        }
        public void LoadContent(GraphicsDevice _graphicsDevice, TiledMap _tilMap, SpriteSheet _spriteSheet)
        {
            this.Player.LoadContent(_spriteSheet);
            this.TiledMap = _tilMap;
            this.TiledMapRenderer = new TiledMapRenderer(_graphicsDevice, this.TiledMap);
        }
        public void Update(GameTime _gameTime)
        {
            this.TiledMapRenderer.Update(_gameTime);
            this.Player.Update(_gameTime);
            this.Player.Movement(Additions.Normalize(Additions.GetAxis(Keyboard.GetState())), (float) _gameTime.ElapsedGameTime.TotalSeconds);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            this.TiledMapRenderer?.Draw();
            this.Player.Draw(_spriteBatch);
        }
    }
}
