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
            var viewportadapter = new BoxingViewportAdapter(gameWindow, graphics, 800, 600);
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
            this.TiledMapRenderer.Update(_gameTime);
            this.Player.Update(_gameTime);
            this.Player.Movement(Additions.Normalize(Additions.GetAxis(Keyboard.GetState())),
                (float)_gameTime.ElapsedGameTime.TotalSeconds, true, true);
            this.MoveCamera(_gameTime);
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
        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(this.Player.Position.X / this.TiledMap.TileWidth);
                ushort ty = (ushort)(this.Player.Position.Y / this.TiledMap.TileHeight - 1);
                if(!IsCollision(tx, ty))
                {
                    movementDirection += Vector2.UnitY;
                } 
            }
            if (state.IsKeyDown(Keys.S))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.D))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Q))
            {
                movementDirection += Vector2.UnitX;
            }

            // Can't normalize the zero vector so test for it before normalizing
            if (movementDirection != Vector2.Zero)
            {
                movementDirection.Normalize();
            }

            return movementDirection;
        }

        private void MoveCamera(GameTime gameTime)
        {
            var speed = 200;
            var seconds = gameTime.GetElapsedSeconds();
            var movementDirection = GetMovementDirection();
            this.CameraPosition += speed * movementDirection * seconds;
        }
    }
}
