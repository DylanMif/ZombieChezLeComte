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
        private TiledMapTileLayer mapLayer;

        private Charactere _player = new Charactere();

        private OrthographicCamera _camera;
        private Vector2 _cameraPosition;
        private Vector2 cameraMove;
        private float deltaTime;

        private Texture2D visionBlock;

        private int currentStamina;

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
        public Vector2 CameraMove {
            get
            {
                return this.cameraMove;
            }

            set
            {
                this.cameraMove = value;
            }
        }
        public float DeltaTime {
            get
            {
                return this.deltaTime;
            }

            set
            {
                this.deltaTime = value;
            }
        }

        public Texture2D VisionBlock
        {
            get
            {
                return visionBlock;
            }

            set
            {
                visionBlock = value;
            }
        }

        public void Initialize(GameWindow gameWindow, GraphicsDevice graphics)
        {
            this.Player.Initialize(new Vector2(360, 360), Constantes.VITESSE_JOUEUR);
            var viewportadapter = new BoxingViewportAdapter(gameWindow, graphics, 1080, 720);
            this.Camera = new OrthographicCamera(viewportadapter);
            this.CameraPosition = Constantes.POSITION_JOUEUR;
            currentStamina = Constantes.JOUEUR_STAMINA;
        }
        public void LoadContent(GraphicsDevice _graphicsDevice, TiledMap _tilMap, SpriteSheet _spriteSheet, Game1 _game)
        {
            this.Player.LoadContent(_spriteSheet);
            this.TiledMap = _tilMap;
            this.TiledMapRenderer = new TiledMapRenderer(_graphicsDevice, this.TiledMap);
            this.MapLayer =  this.TiledMap.GetLayer<TiledMapTileLayer>("Collision");
            this.VisionBlock = _game.Content.Load<Texture2D>("vision");
        }
        public void Update(GameTime _gameTime)
        {
            //Player.CurrentAnimation = "idle";
            Vector2 newPlayerPos = Additions.Normalize(Additions.GetAxis(Keyboard.GetState())) * 
                Constantes.VITESSE_JOUEUR * (float)_gameTime.ElapsedGameTime.TotalSeconds;
            float nextX = (-this.CameraPosition.X + 1080 - 180 + newPlayerPos.X * 2f) / TiledMap.TileWidth;
            float nextY = (-this.CameraPosition.Y + 720 + newPlayerPos.Y) / TiledMap.TileHeight + 0.4f;
            //Console.WriteLine(nextX + ", " + nextY);
            this.TiledMapRenderer.Update(_gameTime);
            this.CameraMove = Vector2.Zero;
            if (!IsCollision((ushort)nextX, (ushort)nextY))
            {
                this.Player.Movement(Additions.Normalize(Additions.GetAxis(Keyboard.GetState())),
                    (float)_gameTime.ElapsedGameTime.TotalSeconds, true);
                this.MoveCamera(_gameTime, -Additions.Normalize(Additions.GetAxis(Keyboard.GetState())));
                
            }
            if(Keyboard.GetState().IsKeyDown(Constantes.runKeys) && currentStamina > Constantes.STAMINA_DECREASE)
            {
                this.Player.Vitesse = Constantes.VITESSE_JOUEUR_RUN;
                currentStamina -= Constantes.STAMINA_DECREASE;
            } else
            {
                this.Player.Vitesse = Constantes.VITESSE_JOUEUR;
                currentStamina += Constantes.STAMINA_INCREASE;
                if(currentStamina > Constantes.JOUEUR_STAMINA)
                {
                    currentStamina = Constantes.JOUEUR_STAMINA;
                }
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

        public void DrawVision(SpriteBatch _sb)
        {
            _sb.Draw(this.VisionBlock, new Rectangle(0, 0, Constantes.WINDOW_WIDTH, Constantes.WINDOW_HEIGHT), Color.White);
        }
        public bool IsCollision(ushort x, ushort y)
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
            var speed = this.Player.Vitesse;
            var seconds = gameTime.GetElapsedSeconds();
            var movementDirection = _dir;
            this.CameraMove = speed * movementDirection;
            this.DeltaTime = seconds;
            this.CameraPosition += speed * movementDirection * seconds;
        }

        public NodeCase GetPlayerCase()
        {
            int x = (int)(-this.CameraPosition.X + 1080 - 180) / TiledMap.TileWidth;
            int y = (int)(-this.CameraPosition.Y + 720) / TiledMap.TileHeight;
            return new NodeCase(x, y);
        }
    }
}
