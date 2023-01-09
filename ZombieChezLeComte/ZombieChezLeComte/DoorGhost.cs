using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    public class DoorGhost
    {
        private Charactere ghost;
        private int speed;
        private Vector2 direction;
        private float currentStayTime;
        private int currentPos;
        private SoundEffect ghostSounds;
        private Random aleatoire;
        private float maxTemps;
        private float _timer;


        public static Vector2[] positions = new Vector2[]
        {
            new Vector2(200, 45),
            new Vector2(40, -210),
            new Vector2(770, -240),
            new Vector2(-50, 250),
        };

        public Charactere Ghost
        {
            get
            {
                return this.ghost;
            }

            set
            {
                this.ghost = value;
            }
        }
        public int Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                this.speed = value;
            }
        }
        public int CurrentPos
        {
            get
            {
                return this.currentPos;
            }

            set
            {
                this.currentPos = value;
            }
        }
        public float CurrentStayTime
        {
            get
            {
                return this.currentStayTime;
            }

            set
            {
                this.currentStayTime = value;
            }
        }
        public Vector2 Direction
        {
            get
            {
                return this.direction;
            }

            set
            {
                this.direction = value;
            }
        }
        public SoundEffect GhostSounds
        {
            get
            {
                return this.ghostSounds;
            }

            set
            {
                this.ghostSounds = value;
            }
        }
        public Random Aleatoire
        {
            get
            {
                return this.aleatoire;
            }

            set
            {
                this.aleatoire = value;
            }
        }
        public float MaxTemps
        {
            get
            {
                return this.maxTemps;
            }

            set
            {
                this.maxTemps = value;
            }
        }
        public float Timer
        {
            get
            {
                return this._timer;
            }

            set
            {
                this._timer = value;
            }
        }

        public void Initialize(Vector2 _position, int _speed)
        {
            Random random = new Random();
            this.Ghost = new Charactere();
            this.Ghost.Initialize(_position, 1);
            this.Speed = _speed;
            this.currentPos = random.Next(0, DoorGhost.positions.Length);
            this.Ghost.Position += DoorGhost.positions[this.CurrentPos];
            this.CurrentStayTime = random.Next(Constantes.DOOR_GHOST_MIN_STAY_TIME, Constantes.DOOR_GHOST_MAX_STAY_TIME);
            this.Aleatoire = new Random();
        }

        public void LoadContent(SpriteSheet _spritesheet, Game1 _game)
        {
            this.Aleatoire = new Random();
            this.Timer = 0;
            this.MaxTemps = Aleatoire.Next(5, 6);
            this.Ghost.LoadContent(_spritesheet);
            this.GhostSounds=  _game.Content.Load<SoundEffect>("GhostDoor");
        }

        public void Update(GameTime _gameTime, CommonNight _commonNight, Game1 _game)
        {
            this.Ghost.Update(_gameTime);
            this.Ghost.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime, false);
            if(this.Ghost.SpriteRect.Intersects(_commonNight.Player.SpriteRect) && !Constantes.GOD_MOD)
            {
                _game.killBy = "doorGhost";
                _game.LoadJumpScare();
            }
            _timer -= (float)_gameTime.GetElapsedSeconds();
            if (this.Timer <= 0 && Vector2.Distance(_commonNight.Player.Position, this.Ghost.Position) <= 150)
            {
                GhostSounds.Play();
                MaxTemps = Aleatoire.Next(5, 6);
                Timer = MaxTemps;
            }
            this.CurrentStayTime -= (float)_gameTime.ElapsedGameTime.TotalSeconds;
            if(this.CurrentStayTime < 0)
            {
                this.ChangePos();
            }
        }

        public void Draw(SpriteBatch _sb)
        {
            this.Ghost.Draw(_sb);
        }
        public void ChangePos()
        {
            Random random = new Random();
            this.Ghost.Position -= DoorGhost.positions[this.CurrentPos];
            this.currentPos = random.Next(0, DoorGhost.positions.Length);
            this.Ghost.Position += DoorGhost.positions[this.CurrentPos];
            this.CurrentStayTime = random.Next(Constantes.DOOR_GHOST_MIN_STAY_TIME, Constantes.DOOR_GHOST_MAX_STAY_TIME);
        }
        
    }
}
