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
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace ZombieChezLeComte
{
    public class CommonNight
    {
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Charactere _player = new Charactere();
        private OrthographicCamera _camera;
        private Vector2 _cameraPosition;
        private TiledMapTileLayer mapLayer;
        


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
        public OrthographicCamera Camera
        {
            get
            {
                return this._camera;
            }

            set
            {
                this._camera = value;
            }
        }
        public Vector2 CameraPosition
        {
            get
            {
                return this._cameraPosition;
            }

            set
            {
                this._cameraPosition = value;
            }
        }
        public TiledMapTileLayer MapLayer
        {
            get
            {
                return this.mapLayer;
            }

            set
            {
                this.mapLayer = value;
            }
        }

        public void Initialize(GameWindow gameWindow, GraphicsDevice graphics)
        {
            this.Player.Initialize(new Vector2(360, 360), Constantes.VITESSE_JOUEUR);
            var viewportadapter = new BoxingViewportAdapter(gameWindow, graphics, 1080, 720);
            this.Camera = new OrthographicCamera(viewportadapter);
            this.CameraPosition = Constantes.POSITION_JOUEUR;
        }
        public void LoadContent(GraphicsDevice _graphicsDevice, TiledMap _tilMap, SpriteSheet _spriteSheet)
        {
            this.Player.LoadContent(_spriteSheet);
            this.TiledMap = _tilMap;
            this.TiledMapRenderer = new TiledMapRenderer(_graphicsDevice, this.TiledMap);
            this.MapLayer =  this.TiledMap.GetLayer<TiledMapTileLayer>("Collision");
        }
        public void Update(GameTime _gameTime)
        {
            Player.CurrentAnimation = "idle";
            Vector2 newPlayerPos = Additions.Normalize(Additions.GetAxis(Keyboard.GetState())) * 
                Constantes.VITESSE_JOUEUR * (float)_gameTime.ElapsedGameTime.TotalSeconds;
            float nextX = (-this.CameraPosition.X + 1080 - 180 + newPlayerPos.X * 7f) / TiledMap.TileWidth;
            float nextY = (-this.CameraPosition.Y + 720 + newPlayerPos.Y) / TiledMap.TileHeight + 0.4f;

            this.TiledMapRenderer.Update(_gameTime);
            
            if (!IsCollision((ushort)nextX, (ushort)nextY))
            {
                this.Player.Movement(Additions.Normalize(Additions.GetAxis(Keyboard.GetState())),
                    (float)_gameTime.ElapsedGameTime.TotalSeconds, true);
                this.MoveCamera(_gameTime, -Additions.Normalize(Additions.GetAxis(Keyboard.GetState())));
            }
            //Console.WriteLine(-Camera.Position);
            this.Player.Update(_gameTime);
            this.Camera.LookAt(this.CameraPosition);

        }

        
        public void Draw(SpriteBatch _spriteBatch)
        {
            this.TiledMapRenderer.Draw(this.Camera.GetInverseViewMatrix());
            this.Player.Draw(_spriteBatch);
            
        }
        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        } 

        private void MoveCamera(GameTime gameTime, Vector2 _dir)
        {
            var speed = 200;
            var seconds = gameTime.GetElapsedSeconds();
            var movementDirection = _dir;
            this.CameraPosition += speed * movementDirection * seconds;
        }
    }
}
