﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Classe servant à regrouper le code commun à toutes les nuits
    /// </summary>
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

        private SoundEffect walkSound;
        private float _timer;
        private float _maxTime;

        private float currentStamina;

        private bool _sword;

        private Texture2D miniMapTexture;
        private Rectangle miniMapDrawRect;
        public bool Sword
        {
            get
            {
                return this._sword;
            }

            set
            {
                this._sword = value;
            }
        }
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
            currentStamina = Constantes.MAX_STAMINA_TIME;
            this.Sword = false;
            _timer = 0;
            miniMapDrawRect = new Rectangle(Constantes.WINDOW_WIDTH - 350, 0, 350, 300);
        }
        public void LoadContent(GraphicsDevice _graphicsDevice, TiledMap _tilMap, SpriteSheet _spriteSheet, Game1 _game)
        {
            this.Player.LoadContent(_spriteSheet);
            this.TiledMap = _tilMap;
            this.TiledMapRenderer = new TiledMapRenderer(_graphicsDevice, this.TiledMap);
            this.MapLayer =  this.TiledMap.GetLayer<TiledMapTileLayer>("Collision");
            this.VisionBlock = _game.Content.Load<Texture2D>("vision");
            walkSound = _game.Content.Load<SoundEffect>("Walk");
            _maxTime = (float) walkSound.Duration.TotalSeconds *2.4f;
            miniMapTexture = _game.Content.Load<Texture2D>("miniMap");
        }
        public void Update(GameTime _gameTime)
        {
            Vector2 newPlayerPos = Additions.Normalize(Additions.GetAxis(Keyboard.GetState())) * 
                Constantes.VITESSE_JOUEUR * (float)_gameTime.ElapsedGameTime.TotalSeconds;
            // Prochaine case sur laquelle se déplacera le joueur
            float nextX = (-this.CameraPosition.X + 1080 - 180 + newPlayerPos.X * 2f) / TiledMap.TileWidth;
            float nextY = (-this.CameraPosition.Y + 720 + newPlayerPos.Y) / TiledMap.TileHeight + 0.4f;

            this.CameraMove = Vector2.Zero;
            
            // Le joueur se déplace uniquement s'il n'y a pas de collision
            if (!IsCollision((ushort)nextX, (ushort)nextY))
            {
                // On joue le son de bruit de pas uniquement si le joueur se déplace réellement
                if (_timer <= 0 && Additions.GetAxis(Keyboard.GetState()) != Vector2.Zero)
                {
                    walkSound.Play();
                    _timer = _maxTime;
                }
                // Cette méthode ne déplace pas réellement le joueur comme le paramètre "isPlayer" est à true
                // le but est uniquement de l'animer
                this.Player.Movement(Additions.Normalize(Additions.GetAxis(Keyboard.GetState())),
                    (float)_gameTime.ElapsedGameTime.TotalSeconds, true, Sword);
                // Pour le déplacer réellement on déplace la camera
                this.MoveCamera(_gameTime, -Additions.Normalize(Additions.GetAxis(Keyboard.GetState())));
                
            }
            // Conditions concernant la fonction de course du joueur
            if(Keyboard.GetState().IsKeyDown(Constantes.runKeys) && currentStamina > 0)
            {
                this.Player.Vitesse = Constantes.VITESSE_JOUEUR_RUN;
                currentStamina -= deltaTime;
                _maxTime = (float)walkSound.Duration.TotalSeconds * 1.4f;
            } else
            {
                this.Player.Vitesse = Constantes.VITESSE_JOUEUR;
                currentStamina += deltaTime / 10;
                _maxTime = (float)walkSound.Duration.TotalSeconds * 2.4f;
                if (currentStamina > Constantes.MAX_STAMINA_TIME)
                {
                    currentStamina = Constantes.MAX_STAMINA_TIME;
                }
            }
            _timer -= _gameTime.GetElapsedSeconds();
            this.Player.Update(_gameTime);
            this.Camera.LookAt(this.CameraPosition);
            this.TiledMapRenderer.Update(_gameTime);
        }

        
        public void Draw(SpriteBatch _spriteBatch)
        {
            this.TiledMapRenderer.Draw(this.Camera.GetInverseViewMatrix());
            this.Player.Draw(_spriteBatch);
            
        }


        /// <summary>
        /// Méthode dessinant le cadre noir autour du joueur
        /// Elle est séparé de la méthode draw car ce cadre doit être dessiner au dessus de tout le reste
        /// </summary>
        /// <param name="_sb">Le SpriteBatch</param>
        public void DrawVision(SpriteBatch _sb)
        {
            _sb.Draw(this.VisionBlock, new Rectangle(0, 0, Constantes.WINDOW_WIDTH, Constantes.WINDOW_HEIGHT), Color.White);
            // On la dessine ici car il faut qu'elle se dessine par dessus la zone noir
            _sb.Draw(miniMapTexture, miniMapDrawRect, new Color(100, 100, 100));
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

        /// <summary>
        /// Récupère la tuile de la map sur la quelle se trouve le joueur
        /// </summary>
        /// <returns>une instance de NodeCase donnant les informations sur la tuile du joueur</returns>
        public NodeCase GetPlayerCase()
        {
            int x = (int)(-this.CameraPosition.X + 1080 - 180) / TiledMap.TileWidth;
            int y = (int)(-this.CameraPosition.Y + 720) / TiledMap.TileHeight;
            return new NodeCase(x, y);
        }
    }
}
